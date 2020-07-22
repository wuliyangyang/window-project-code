local _DUT_ = {}
require "pathManager"
-- package.cpath = package.cpath..";"..deleteLastPathComponent(CurrentDir()).."/lib/?.dylib"

require  "libSocketDev"

local time_utils = require("utils.time_utils")
local config_utils = require("utils.config_utils")

local ip = "169.254.1.33" -- get ip here
local port = 7601 -- get port 
local rep = "tcp://*:7003"
local pub = "tcp://*:6803"

_DUT_.device = nil
_DUT_.allRespponse = {}

local _dut_detect_ = "%] %:%-%)"
local _dut_response_ = ""
local _dut_sent_command_ = false


function _DUT_._DUT_Socket_Connect_()
	_DUT_.device = CSocketDevice:new();
	os.execute("ping -c 1 "..ip); 
	print("< ".. tostring(CONFIG.ID).." libSocketDev > DUT Port connect to: " .. tostring(ip)..":"..tostring(port))
	local ret = _DUT_.device:Open(ip,port)
	if(ret < 0) then
		error("< ".. tostring(CONFIG.ID).." libSocketDev > DUT Port connect Fail (xxxx)")
	else 
		print("< ".. tostring(CONFIG.ID).." libSocketDev > DUT Port connect successfully")
	end
	print("< ".. tostring(CONFIG.ID).." libSocketDev > DUT REP . SUB: " .. tostring(rep).." . "..tostring(pub))
	print(_DUT_.device:CreateIPC(rep,pub))
end

function _DUT_._Dut_Send_String_(str)
	_dut_sent_command_ = true
	return _DUT_.device:WriteString(string.format("%s\r", str))
end

function _DUT_._Dut_Set_Detect_String_(det)
	if(det) then 
		_dut_detect_ = det 
		_DUT_.device:SetDetectString(det)
	end
end

function _DUT_._Dut_Wait_For_String_(timeout)
	local ret = _DUT_.device:WaitDetect(timeout)
	local errmsg = nil
	if(ret==-1) then
		errmsg = "connection disconnect"
	elseif(ret==-2) then
		errmsg = "Timeout"
	elseif(ret~=0) then
		errmsg = "Unknow Error occur _Dut_Wait_For_String_"
	end
	return ret, errmsg
end

function _DUT_._Dut_Send_Cmd_(str, timeout)
	_DUT_._Dut_Set_Detect_String_("] :-)")
	-- local s1, t1 = time_utils.get_time_string_ms()
  	-- if(TestFlowOut) then TestFlowOut.write( "\t< DUT send: > " .. str) end
  	_DUT_._Dut_Send_String_(str)
  	local ret, errmsg = _DUT_._Dut_Wait_For_String_(timeout)
  	
  	-- local s2, t2 = time_utils.get_time_string_ms()
 	-- if(TestFlowOut) then TestFlowOut.write("\t< DUT elapsed: > (sec)".. tostring((t2 - t1)/1000)) end
  	return ret, errmsg
end

function _DUT_._Dut_Read_String_()
	local tmp = _DUT_.device:ReadString()
	table.insert(_DUT_.allRespponse,tmp)
	return tmp
end

function _DUT_._Dut_Socket_Receive_()
	local tmp = _DUT_.device:ReadString()
	table.insert(_DUT_.allRespponse,tmp)
	return tmp
end

function _DUT_._Dut_Socket_Close_()
	_DUT_.device:Close()
end

function _DUT_._CLear_Response_Buf_()
	_DUT_.device:ReadString()
	_DUT_.allRespponse = {}
end

function _DUT_._Get_All_Response_()
	return table.concat(_DUT_.allRespponse)
end

function _DUT_._WriteCB(sd, cmd)
	return _DUT_.device:WritePassControlBit(sd,cmd)
end

return _DUT_
