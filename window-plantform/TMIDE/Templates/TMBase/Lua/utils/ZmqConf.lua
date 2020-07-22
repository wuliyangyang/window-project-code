
local json = require("dkjson")
require("pathManager")


local config = {}

config.addr = {}
config.addr.TEST_ENGINE_RESP_ADDR = "tcp://*"
config.addr.TEST_ENGINE_HB_ADDR = "tcp://*"

config.addr.STATEMACHINE_SUB_ADDR = "tcp://127.0.0.1"
config.addr.STATEMACHINE_REQ_ADDR = "tcp://127.0.0.1"
config.addr.STATEMACHINE_REP_ADDR = "tcp://*"
config.addr.STATEMACHINE_HEARTBEAT_ADDR = "tcp://*"
-- config.addr.STATEMACHINE_PUB_ADDR  = "tcp://*"

config.addr.FIXTURE_CTRL_SUB_ADDR = "tcp://127.0.0.1"
config.addr.FIXTURE_CTRL_REQ_ADDR = "tcp://127.0.0.1"

function config.load_zmq_port(portFile)
	print("read zmq port from file:", portFile)

	local f = assert(io.open(portFile, "r"), string.format("open zmq port file:%s failure!", portFile))
	local str = f:read("*a")
	f:close()

	config.port = assert(json.decode(str), string.format("json decode error from file:%s!", portFile))
end

function config.zmqport(portName, port_name, module, slots, uut)
	return config.port[portName] + tonumber(module) * tonumber(slots) + tonumber(uut)
end


function config.get_addr(addr_name, port_name, module, slots, uut)
	uut = uut or 0
	return config.addr[addr_name] .. ":" .. tostring(config.port[port_name] + tonumber(module) * tonumber(slots) + tonumber(uut))
end


return config
