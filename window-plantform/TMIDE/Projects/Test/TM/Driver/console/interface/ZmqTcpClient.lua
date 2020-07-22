local M={};
-- package.cpath = package.cpath..";"..".\\?.dll"
require("libTcp");

local luaTcpClientTable={}

function M.CreateTcpClient(szDeviceName)
	if luaTcpClientTable[szDeviceName] then
		print("ZmqSerialPort->>CreateTcpClient : TcpClient Device has been existed : ",szDeviceName);
		return nil;
	end

	local Client = TcpClient:new();
	if Client then
		luaTcpClientTable[szDeviceName] = Client;
	end
	return Client;
end

function M.DeleteTcpClient(szDeviceName)
	local Client = luaTcpClientTable[szDeviceName]
	if Client == nil then
		print("DeleteTcpClient : can not fond device name("..szDeviceName..")\n");
		return;
	end

	luaTcpClientTable[szDeviceName] = nil;
    return --Client:delete();
end

function M.DeleteAll()
	for k,v in pairs(M.luaTcpClientTable) do
		v:Close();
		--v:delete();
	end
end

function M.OpenTcpClient(szDeviceName,remoteip_Port)
	local Client = luaTcpClientTable[szDeviceName]
	if Client == nil then
		print("OpenTcpClient : can not fond device name("..szDeviceName..")\n");
		return false;
	end
	local ret = Client:Open(remoteip_Port); 
	if ret == 0 then

		--M.CreateIPC(szDeviceName,pubAddress,replyAddress)
		return true;
	else
		M.DeleteTcpClient(szDeviceName)
		return false;
	end
end

function M.CloseTcpClient(szDeviceName)
	local Client = luaTcpClientTable[szDeviceName]
	if Client == nil then
		print("CloseTcpClient : can not fond device name("..szDeviceName..")\n");
		return;
	end
    return Client:Close(); 
end

function M.ClearBuffer(szDeviceName)
	local Client = luaTcpClientTable[szDeviceName]
	if Client == nil then
		print("ClearBuffer : can not fond device name("..szDeviceName..")\n");
		return;
	end
    return Client:ClearBuffer();
end

function M.SetDetectString(szDeviceName,Regex)
	local Client = luaTcpClientTable[szDeviceName]
	if Client == nil then
		print("SetDetectString : can not fond device name("..szDeviceName..")\n");
		return;
	end
    return Client:SetDetectString(tostring(Regex))
end

function M.WaitForString(szDeviceName,timeout)

	local Client = luaTcpClientTable[szDeviceName]
	if Client == nil then
		print("WaitForString : can not fond device name("..szDeviceName..")\n");
		return -2;
	end
    return Client:WaitDetect(timeout)
end

function M.SetTCPInfo(szDeviceName,trigger,msgCmd)
	local Client = luaTcpClientTable[szDeviceName]
	local  ret = Client:TCPInfo(trigger,msgCmd)
end

function M.WriteString(szDeviceName,str)
	-- M.ClearBuffer(szDeviceName);
	local Client = luaTcpClientTable[szDeviceName]
	if Client == nil then
		print("WriteString : can not fond device name("..szDeviceName..")\n");
		return false;
	end
    return Client:WriteBytes(str,#str);
end

function M.ReadString(szDeviceName)
	local Client=luaTcpClientTable[szDeviceName]
	if Client==nil then
		print("ReadString : can not fond device name("..szDeviceName..")\n");
		return;
	end
    return Client:ReadString();
end

function M.SendReceive(szDeviceName,cmd,timeout)
	local Client = luaTcpClientTable[szDeviceName]
	if Client == nil then
		print("TcpClient SendReceive : can not fond device name("..szDeviceName..")\n");
		return;
	end

	M.WriteString(szDeviceName,cmd);
	M.WaitForString(szDeviceName,timeout);
	return M.ReadString(szDeviceName);
end

function M.CreateIPC(szDeviceName,publisher,reply)
	-- body
	local Client = luaTcpClientTable[szDeviceName]
	if Client == nil then
		print("CreateIPC : can not fond device name("..szDeviceName..")\n");
		return;
	end

	--("tcp://127.0.0.1:6820","tcp://127.0.0.1:6495")
	--return Client:CreateIPC(reply,publisher);
	return Client:CreateZmqServer(publisher,reply)
end


return M