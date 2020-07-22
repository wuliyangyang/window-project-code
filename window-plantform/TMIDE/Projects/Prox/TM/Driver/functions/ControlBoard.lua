print("< CB >"..tostring(args.uut).." Load functions.ControlBoard.lua....")

local _CB = {}
local _zmq = require("functions.zmq_proxy")
local time_utils = require("utils.time_utils")
local TMSync = "TMSync"
_CB.port = nil;

function _CB.SyncOpen()
	_CB.port = zmqConf.zmqport("CONTROL_BOARD_PORT", args.module, args.slots, args.uut)
	return _zmq.open(TMSync,string.format("%s:%d","tcp://127.0.0.1", _CB.port))
end

function _CB.SyncClose()
	return _zmq.close(TMSync)
end

function _CB.SyncSendRecv(sequence)
	local command=tostring(sequence.param1).."\r\n";
	if _zmq.connect_table[TMSync] == nil then
		return "--FAIL--please connect sync first"
	end
	local recv,err = _zmq.send_and_recv(TMSync, command, (sequence and sequence.timeout))
	if recv then
		recv = string.gsub(tostring(recv),"[\r\n]*","");
		print("ControlBoard->>TMSync_SendRecv : ",recv);
		return recv;
	else
		print("ControlBoard->>TMSync_SendRecv : recieve time out,err : ",err);
		return "--FAIL--"..tostring(err);
	end
end

function _CB.init()
  logger:info("ControlBoard  init")
  return true
end

function _CB.self_test()
  logger:info("ControlBoard self_test")
  return true
end

function _CB.clear_buffer()
  logger:info("ControlBoard  clear_buffer")
  return true
end

print("< CB > Load functions.ControlBoard.lua Success")

return _CB