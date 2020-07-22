local M={};
-- package.cpath = package.cpath..";".."TestEngine\\lib\\?.dll"
--package.cpath..";"..".\\?.dll"
require("SerialWithFixture")
--require("libTcp")
M._Device = SerialWithFixture:new()
function M.OpenSerialport(sportname,sportsetting)
	-- M._Device = SerialWithFixture:new();
	print(M._Device)
	local SerialPort = sportname;
	local Setting = sportsetting;
	return M._Device:Open(SerialPort,Setting)
end

function M.DisconetSerialport(sportname)
	local SerialPort = sportname
	if SerialPort==nil then
		print("can not fond device ("..SerialPort..")\n")
		return;
	end
    return M._Device:Close()
end

function M.SetDetectString(str)
	if str==nil then
		print("DetectString is nil\n");
		str = "\r\n"
	end
    return M._Device:SetDetectString(str)
end


function M.WaitForString(timeout)

	if timeout==nil then
		timeout = 3000;
	end
    return M._Device:WaitDetect(timeout)
end

function M.WriteString(str)
	if str==nil then
		print("command can not be empt\n");
		return false
	end
	str = str.."\r\n"
    return M._Device:WriteString(str)
end

function M.ReadString()
    return M._Device:ReadString();
end

function M.SendReceive(cmd,timeout)
	M.WriteString(cmd)
	M.WaitForString(timeout)
	return M.ReadString()
end



function M.CreateZmqServer( Addpub, Addrep )
	return M._Device:CreateZmqServer(Addpub,Addrep)
end
function M.CreateZmqClient(Addreq)

	if Addreq==nil or Addreq == "" then
		Addreq = "tcp://127.0.0.1:6480" 
	end	
	
	local timeout = 3000;
	return M._Device:CreateZmqClient(Addreq,timeout)
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

function M.Setfixtureinfo(trigger,slots)
	
	local msgCmd = GenerateStartMsg(slots)
	return M._Device:SetFixtureInfo(trigger,msgCmd)
end

function M.SetNeedRep( intN,timeout )
	return M._Device:SetNeedDetect(intN,timeout)
end



local  triggerStr= "Ready Ok"
local msg = "{\"params\":{\"uutinfo\":[{\"SN\":\"\",\"uutNum\":0},{\"SN\":\"\",\"uutNum\":1}],\"times\":1},\"function\":\"start\"}"
print(M.OpenSerialport("COM6","115200,0,8,2"))
-- print("SetDetect:",M._Device:SetDetectString("\r\n"))
-- print("CreateZmqServer",M._Device:CreateZmqServer("tcp://127.0.0.1:6465","tcp://127.0.0.1:6470"))
print("CreateZmqClient",M._Device:CreateZmqClient("tcp://127.0.0.1:6480",3000))
-- print("SetInfo",M._Device:SetFixtureInfo("Ready Ok","{\"function\":\"start\"}"))
 -- M.Setfixtureinfo("Ready Ok",1)
-----------------------------TCP---------------------------------
return M
