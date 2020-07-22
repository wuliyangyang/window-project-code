
local zmq = require("lzmq")
local zpoller = require("lzmq.poller")
local zthreads = require("lzmq.threads")
local ztimer  = require("lzmq.timer")
local json = require("dkjson")
local DLContext = require("DLContext")
local PivotLog = require("PivotLog")
local StatisticsLog = require("StatisticsLog")
local MyProject = require("MyProject")
local server = {singleFlag = false}
local tc = require("TestContext.TestContext");
local df = require("TestContext.DebugFlag");

function DataProcess(Time,Content)
	-- body
	DLContext:ResolveMessage(Time,Content);
	PivotLog:ResolveMessage(Time,Content);
	MyProject:ResolveMessage(Time,Content);
	StatisticsLog:ResolveMessage(Time,Content);
end

function server:RegisterSeqSubHandler()
	for i,uut in ipairs(self.uuts) do
		self.zmqPoller:add(uut.seqSub, zmq.POLLIN, function()
			local msg = uut.seqSub:recv_all()

			-- deal with SEQUENCE_END msg
			if msg[5] ~= nil and msg[5] ~= "FCT_HEARTBEAT" and msg[2] ~= nil then

				local strOfJson =string.match(tostring(msg[5]),"%{.+%}");
				if strOfJson ~= nil then
					--print("Get Message:",strOfJson);
					--decode string to table of json
					local MsgContent = json.decode(strOfJson);
					if MsgContent and MsgContent.event then
						pcall(DataProcess,msg[2],MsgContent)
					end
				end
			end
		end)	
	end
end

function server:New(ctx, args)
	if self.singleFlag then
		logger:fatal("Server class can have only one instance") 
		return nil, "Server class can have only one instance"
	end

	obj = {loopInfo = {totalLoopTimes = 1, currentLoopTimes = 0},
	       testingFlag = false, zmqPoller = nil, uuts = {}, stopLoopFlag = true }

	setmetatable(obj, self)
	self.__index = self

	self.singleFlag = true

	local uut = {flag = false, uutEnable = true, uutSN = "", seqSub = nil}

	local addr = zmqConf.get_addr("DATALOGGER_SUB_ADDR", "SEQUENCER_PUB", args.module, args.slots, args.uut)
	logger:info("< DATALOGGER > Sequencer SUB addr: %s", addr)
	sc, err = ctx:socket{zmq.SUB, connect = addr, subscribe = ""}
	zmq.assert(sc, err)
	uut.seqSub = sc

	table.insert(obj.uuts, uut)

	obj.uuts.n = args.slots

	obj.zmqPoller = zpoller.new(1)
	self.singleFlag = true

	tc:GetFileHandle(args.module,args.uut);
	df:GetFileHandle(args.module,args.uut);

	return obj
end

function server:Run()
    logger:info("DL Started successful and wait for data")
    self.zmqPoller:start()
end


return server


