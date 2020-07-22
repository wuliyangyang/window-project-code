local M={};
-- package.cpath = package.cpath..";"..".\\?.dll"
require("libSerial");

local luaSerialPortTable   = {}
local luaSerialPortSetting = {}
local Parity   = {};
Parity["n"]  = 0; --NOPARITY
Parity["o"]  = 1; --ODDPARITY
Parity["e"]  = 2; --EVENPARITY
Parity["m"]  = 3; --MARKPARITY
Parity["s"]  = 4; --SPACEPARITY

local StopBit  = {};
StopBit["1"]   = 0;
StopBit["1.5"] = 1;
StopBit["2"]   = 2;

function M.CreateSerialPort(szDeviceName)
	if luaSerialPortTable[szDeviceName] then
		print("ZmqSerialPort->>CreateSerialPort : SerialPort Device has been existed : ",szDeviceName);
		return nil;
	end

	local SerialPort = Serial:new();
	if SerialPort then
		luaSerialPortTable[szDeviceName] = SerialPort;
	end
	return SerialPort;
end

function M.DeleteSerialPort(szDeviceName)
	local SerialPort = luaSerialPortTable[szDeviceName]
	if SerialPort == nil then
		print("DeleteSerialPort : can not fond device name("..szDeviceName..")\n");
		return;
	end

	luaSerialPortTable[szDeviceName] = nil;
	luaSerialPortSetting[szDeviceName] = nil;
    return --SerialPort:delete();
end

function M.DeleteAll()
	for k,v in pairs(M.luaSerialPortTable) do
		v:close();
		--v:delete();
	end

	luaSerialPortSetting = nil;
	luaSerialPortSetting = {};
end

function M.OpenSerialPort(szDeviceName,sportname)
	local SerialPort = luaSerialPortTable[szDeviceName]
	if SerialPort == nil then
		print("OpenSerialPort : can not fond device name("..szDeviceName..")\n");
		return false;
	end

	local Setting = luaSerialPortSetting[szDeviceName]
	if Setting == nil then
		print("OpenSerialPort : can not fond device setting name("..szDeviceName..")\n");
		return false;
	end

	local ret = SerialPort:Open(sportname,Setting[1],Setting[2],Setting[3],Setting[4]);
	if ret == 0 then
		return true;
	else
		M.DeleteSerialPort(szDeviceName);
		return false;
	end
end

function M.SetSerialPort(szDeviceName,setting)
	local SerialPort = luaSerialPortTable[szDeviceName]
	if SerialPort == nil then
		print("SetSerialPort : can not fond device name("..szDeviceName..")\n");
		return false;
	end

	luaSerialPortSetting[szDeviceName] = {};
	local SetArr = luaSerialPortSetting[szDeviceName];
    for v in string.gmatch(tostring(setting)..",","(%w+),") do
      SetArr[#SetArr+1] = v;
    end

	luaSerialPortSetting[szDeviceName] = {tonumber(SetArr[1]),Parity[SetArr[2]],tonumber(SetArr[3]),StopBit[SetArr[4]]};
	return true;
end

function M.CloseSerialPort(szDeviceName)
	local SerialPort = luaSerialPortTable[szDeviceName]
	if SerialPort == nil then
		print("CloseSerialPort : can not fond device name("..szDeviceName..")\n");
		return;
	end

    return SerialPort:Close(); 
end

function M.ClearBuffer(szDeviceName)
	local SerialPort = luaSerialPortTable[szDeviceName]
	if SerialPort == nil then
		print("ClearBuffer : can not fond device name("..szDeviceName..")\n");
		return;
	end
    return SerialPort:ClearBuffer();
end

function M.SetDetectString(szDeviceName,Regex)
	local SerialPort = luaSerialPortTable[szDeviceName]
	if SerialPort == nil then
		print("SetDetectString : can not fond device name("..szDeviceName..")\n");
		return;
	end
    return SerialPort:SetDetectString(tostring(Regex));
end

function M.WaitForString(szDeviceName,timeout)

	local SerialPort = luaSerialPortTable[szDeviceName]
	if SerialPort == nil then
		print("WaitForString : can not fond device name("..szDeviceName..")\n");
		return -2;
	end
    return SerialPort:WaitDetect(timeout)
end

function M.WriteString(szDeviceName,str)
	local SerialPort = luaSerialPortTable[szDeviceName]
	if SerialPort == nil then
		print("WriteString : can not fond device name("..szDeviceName..")\n");
		return false;
	end
    return SerialPort:WriteString(str);
end

function M.ReadString(szDeviceName)
	local SerialPort = luaSerialPortTable[szDeviceName]
	if SerialPort == nil then
		print("ReadString : can not fond device name("..szDeviceName..")\n");
		return;
	end
    return SerialPort:ReadString();
end

function M.SendReceive(szDeviceName,cmd,timeout)
	local SerialPort = luaSerialPortTable[szDeviceName]
	if SerialPort == nil then
		print("SerialPort SendReceive : can not fond device name("..szDeviceName..")\n");
		return;
	end
	M.ClearBuffer(szDeviceName);
	M.WriteString(szDeviceName,cmd);
	M.WaitForString(szDeviceName,timeout);
	return M.ReadString(szDeviceName);
end

function M.CreateIPC(szDeviceName,reply,publisher)
	-- body
	local SerialPort = luaSerialPortTable[szDeviceName]
	if SerialPort == nil then
		print("CreateIPC : can not fond device name("..szDeviceName..")\n");
		return;
	end

	return SerialPort:CreateIPC(reply,publisher);
end

function M.SetRepOpt(szDeviceName,needReply,timeout)
	-- body
	local SerialPort = luaSerialPortTable[szDeviceName]
	if SerialPort == nil then
		print("SetRepOpt : can not fond device name("..szDeviceName..")\n");
		return;
	end

	return SerialPort:SetRepOpt(needReply,timeout);
end

function M.SetPubOpt(szDeviceName,needPub)
	-- body
	local SerialPort = luaSerialPortTable[szDeviceName]
	if SerialPort == nil then
		print("SetPubOpt : can not fond device name("..szDeviceName..")\n");
		return;
	end

	return SerialPort:SetPubOpt(needPub);
end

return M