
require("libTcp")
local json = require("dkjson")
local M={};
local _Device = nil
local time_utils=require("utils.time_utils");	
M._Lib = TcpClient:new();

function M.OpenTCPport(sportsetting)
	-- local sportsetting = "127.0.0.1:4444"
	local op = M._Lib:Open(sportsetting)
	return op
end

function M.SetTCPInfo(trigger,slots)
	
	local msgCmd = GenerateStartMsg(slots)
	local  ret = M._Lib:TCPInfo(trigger,msgCmd)
end

function M.TCPWriteString(str)
	print("send "..str)
    return M._Lib:WriteBytes(str,#str)
end

function M.TCPCreateZmqClient(Addreq)
	--local x = _Lib:ReadString()
	
	-- local Addreq = "tcp://127.0.0.1:6480" 
	local timeout = 3000;
	local repd =  M._Lib:CreateZmqClient(Addreq,timeout)
	return repd
end
function M.TCPSetDetectString(str)
	if str==nil then
		print("DetectString is nil\n");
		str = "\r\n"
	end
	local  setd = M._Lib:SetDetectString(str)
	print("setd =="..setd)
    return setd
end

function  M.ReadString( )
	time_utils.delay(500)
	local ret = M._Lib:ReadString()
	print("ReadString>:"..ret)
	return ret
	
end

function GenerateStartMsg(slots)
	-- body
	local slotMsg = ""

	for i=1,slots do
		SN = "\"\""
		slotMsg = slotMsg..",".."{\"SN\":"..SN..",\"uutNum\":"..tostring(i-1).."}"
	end
	local slotMsginfo = string.sub(slotMsg,2,-1)
	local msgCmd = "{\"params\":{\"uutinfo\":["..slotMsginfo.."],\"times\":1},\"function\":\"start\"}"
	return msgCmd
end

local triggerStr = "{\"msgChannel\":0,\"msgType\":\"REQ\",\"msgName\":\"starttest\",\"msgParam\":null,\"msgResult\":0,\"errMsg\":null}"
local registerMsg = "{\"msgType\":\"REQ\",\"msgName\":\"register\",\"msgChannel\":0,\"msgParam\":{\"moduletype\":\"TM\",\"moduleid\":\"S1\"},\"msgResult\":0,\"errMsg\":null}"

M.OpenTCPport("127.0.0.1:8899")
M.TCPCreateZmqClient("tcp://127.0.0.1:6480")
M.SetTCPInfo(triggerStr,1)
M.TCPWriteString(registerMsg.."\r\n")
M.ReadString()

return M
