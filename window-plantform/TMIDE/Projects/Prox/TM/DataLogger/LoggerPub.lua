-- local loggerpipe = require("utils.EnginePub");
local json = require('dkjson');
local zmq = require("lzmq")
local DLContext = require("DLContext")
local logger_pub, err = nil,nil;
local _LoggerPub = {};

local function CreatePubMsgString(fun,data)
	-- body
	local x = {}
	x["function"] = tostring(fun)
	data["dutSn"] = DLContext.MLBSN;
	data["socketSn"] = DLContext.SocketSN;
	x["params"] = data;
	x["slot"] = args.uut + args.module * args.slots;
	local msg = json.encode(x);
	return msg;
end

function _LoggerPub:init(context,address)
	logger:info("< logger Set PUB > : ".. address);
	logger_pub, err = context:socket( zmq.PUB, {bind=address})
	zmq.assert(logger_pub, err);
end

--{"function":"MtcpResult","params":{"dutSn":"","socketSn":"","result":0,"errStr":"","BinCode":55},"slot":"CH1"}
--{"function":"SequencerResult","params":{"dutSn":"","socketSn":"","result":0,"errStr":""},"slot":"CH1"}
function _LoggerPub:pubMsg(fun,data)

	local msg = CreatePubMsgString(fun,data);

	-- if loggerpipe.pipe then
	-- 	logger:info("<DataLogger pub> : "..tostring(msg));
	-- 	loggerpipe.send(tostring(msg));
	-- end

	if logger_pub then
		logger:info("<DataLogger pub> : "..tostring(msg));
		logger_pub:send_all{"101",os.date("%m-%d %H:%M:%S",os.time()),3,"DataLogger",msg};
	end
end

return _LoggerPub;