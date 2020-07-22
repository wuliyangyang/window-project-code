
local M = {}
local zmq = require("interface.ZMQPub")
local config = require("config_devices");
local timer = require("lzmq.timer")
local ConnectState = false;

M.ModuleName = "Lua"
M.DeviceAddress = "tcp://*:"..tostring((config[M.ModuleName]+args.uut));
M.DeviceName = M.ModuleName..tostring(args.uut);

--local address= "tcp://*:6720"

function M.Bind(address)
	-- body
	zmq.Bind(M.DeviceName,address)
	ConnectState = true
end

function M.DgbOut(str)
	-- body
	zmq.Send(M.DeviceName,str)
end


if  not ConnectState then
	 M.Bind(M.DeviceAddress );
	 M.DgbOut("start log...")
	 --timer.sleep(500);
end	

return M;