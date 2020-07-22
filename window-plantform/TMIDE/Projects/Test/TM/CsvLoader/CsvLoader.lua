#!/usr/bin/env lua

io.stdout:setvbuf("no")

local json = require("dkjson")
local zmq = require("lzmq")
local uuid = require("utils.uuid")

require("logging.console")
require("pathManager")
require("utils.zhelpers")

local ctx = zmq.context();

package.path = package.path..";"..JoinPath(JoinPath(CurrentDir(), "CsvLoader"), "?.lua")

logger = logging.console()
logger:setLevel(logging.INFO)

local __CSVLOADER_VERSION = "2.1.0 updated on 2018-03-17"
logger:info("*************************************************************************")
logger:info("           Load CsvLoader version is " .. __CSVLOADER_VERSION)
logger:info("           ZMQ version is %d.%d.%d", zmq.version(true))
logger:info("*************************************************************************")

args = require("LoaderParseArgu")

zmqConf = require("utils.ZmqConf")
zmqConf.load_zmq_port(args.file)

local function generateReqMsg(func, in_params)
	local msgTbl = {
		["method"] = func,
		jsonrpc = "2.0",
		id = uuid(),
	}


	if in_params then
		msgTbl.args = in_params
	else
		msgTbl.args = {}
	end
	
	local msg = json.encode(msgTbl)
	logger:info("< CsvLoader > Req msg is %s", msg)
	return msg
end



for i=1,args.modules * args.slots do

	local addr = zmqConf.get_addr("STATEMACHINE_REQ_ADDR", "SEQUENCER_PORT", 0, 0, i - 1)
	logger:info("< CsvLoader > Sequencer REQ addr: %s", tostring(addr))
	local seqReq, err = ctx:socket{zmq.REQ, connect = addr}
	zmq.assert(seqReq, err)

	local msg = generateReqMsg("load",{args.profile});
	seqReq:set_rcvtimeo(5000);
	print("[client] create a client: ", ip, "timeout", t)
	seqReq:send(msg);
	logger:info("< CsvLoader > send load msg: %s to Sequencer %d", tostring(msg), i - 1)
	local resp = seqReq:recv();
	logger:info("< CsvLoader > receive msg: %s from Sequencer %d ", tostring(resp), i - 1)

	seqReq:close();

end

