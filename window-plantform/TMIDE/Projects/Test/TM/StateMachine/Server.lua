
local zmq = require("lzmq")
local zpoller = require("lzmq.poller")
local zthreads = require("lzmq.threads")
local ztimer  = require("lzmq.timer")
local json = require("dkjson")
local tc = require("TestContext.TestContext");
local df = require("TestContext.DebugFlag");

local fixtureSM = require("fixtureSM")

--local tcpClient = require("TcpClient")

local project = require("Project")
local common = require("SMCommon")

local server = {singleFlag = false}


function server:CheckAllFlag()
	for _,uut in ipairs(self.uuts) do
		if uut.flag == false then
			return false
		end
	end

	return true
end

function server:ClearAllFlag()
	for _,uut in ipairs(self.uuts) do
		if uut.uutEnable then
			uut.flag = false
		end
	end
end

function server:ClearAllSN()
	for _,uut in ipairs(self.uuts) do
		uut.uutSN = ""
		uut.socketSN = ""
	end
end

function server:SetUutSN(slotNum, SN)
	self.uuts[slotNum].uutSN = SN
end

function server:SetSocketSN(slotNum, SN)
	self.uuts[slotNum].socketSN = SN
end

function server:HandleSetUutSNMsg(json_obj)

	for _,item in pairs(json_obj) do
		slotNum = tonumber(item.uutNum)
		if slotNum < 0 or slotNum >= self.uuts.n then
			logger:error("slot number %d is wrong in \"setsn\" msg", slotNum)
			
			return false
		end

		self:SetUutSN(slotNum + 1, item.SN)
		if item.SocketSN then
			self:SetSocketSN(slotNum + 1, item.SocketSN)
		end

		local uut = self.uuts[slotNum + 1]

		uut.uutEnable = true
		uut.flag = not uut.uutEnable 
	end

	return true
end

function server:SendAbortToSeq()
	self.stopLoopFlag = true
	if self.loopInfo.totalLoopTimes > 1 then
		sm:SetLoopTestFlag(false)
	end

	if not self.testingFlag then
		return;
	end

	for i,uut in ipairs(self.uuts) do
		if uut.uutEnable then
			local msg = common.generateReqMsg("abort")
			logger:info("SM send abort msg: %s to Sequencer %d", msg, i - 1)
			uut.seqReq:send(msg)
		end
	end

	for i,uut in ipairs(self.uuts) do
		if uut.uutEnable then
			local resp = uut.seqReq:recv()
			logger:info("SM receive msg: %s from Sequencer %d ", resp, i - 1)
		end
	end

	self.testingFlag = false;

end

function server:SendLoadToSeq(path)
	local isLoaded = true;
	for i,uut in ipairs(self.uuts) do
		if uut.uutEnable then
			local msg = common.generateReqMsg("load",{path})
			logger:info("SM send load msg: %s to Sequencer %d", msg, i - 1)
			uut.seqReq:send(msg)
		end
	end

	for i,uut in ipairs(self.uuts) do
		if uut.uutEnable then
			local resp = uut.seqReq:recv()
			if not string.find(tostring(resp),"has been loaded") then
				isLoaded = false;
				logger:info("Load test script fail, SM receive msg: %s from Sequencer %d ", resp, i - 1)
			else
				logger:info("Load test script successful, SM receive msg: %s from Sequencer %d ", resp, i - 1)
			end
		end
	end

	return isLoaded;
end

function server:SendStartToSeq()
	
	local isFix = sm:state_switch(fixtureSM.EVENT.START)
	if not isFix then
		return false;
	end
	self.testingFlag = true

	for i,uut in ipairs(self.uuts) do
		if uut.uutEnable then

			local params = nil
			if uut.uutSN ~= "" and uut.socketSN ~= "" then 
				params = { { attributes = {MLBSN = uut.uutSN,SocketSN = uut.socketSN} } }
				local arr = {};
				arr[tc.MLBSN] = uut.uutSN;
				arr[tc.SocketSN] = uut.socketSN;
				tc:SetValueArr(arr,args.module,(i-1));
			elseif uut.uutSN ~= "" then
				params = { { attributes = {MLBSN = uut.uutSN} } }
				tc:SetMLBSN(uut.uutSN,args.module,(i-1));
			elseif uut.socketSN ~= "" then
				params = { { attributes = {SocketSN = uut.socketSN} } }
				tc:SetSocketSN(uut.uutSN,args.module,(i-1));
			end

			local msg = common.generateReqMsg("run", params)
			logger:info("SM send start msg to Sequencer %d is: %s", i - 1, msg)
			uut.seqReq:send(msg)

		end
	end

	for i,uut in ipairs(self.uuts) do
		if uut.uutEnable then
			local resp = uut.seqReq:recv()
			logger:info("SM receive msg from Sequencer %d is: %s", i - 1, resp)
			if resp == nil or string.match(tostring(resp), "error") then
				uut.flag = true
			end
		end
	end

	return true;
end


function server:HandleStartMsg(json_obj)
	if self.testingFlag then
		logger:warn("SM in Testing status")
		return false, "SM in Testing status"
	else
		if json_obj.times == nil or type(json_obj.times) ~= "number"  then
			return false, "times is invalid params"
		else
			self.loopInfo.totalLoopTimes = tonumber(json_obj.times)
			if self.loopInfo.totalLoopTimes > 1 then
				sm:SetLoopTestFlag(true)
				self.stopLoopFlag = false
			end
		end

		if json_obj.uutinfo then
			for _,uut in ipairs(self.uuts) do
				uut.uutEnable = false
				uut.flag = not uut.uutEnable
			end

			if not self:HandleSetUutSNMsg(json_obj.uutinfo) then
				return false, "invalid params in uutinfo"
			end
		else
			for _,uut in ipairs(self.uuts) do
				uut.uutEnable = true
				uut.flag = not uut.uutEnable
			end
		end

		self:SendStartToSeq()
		return true
	end
end

--[[

function server:HandleUutEnableMsg(json_obj)
	for _,item in pairs(json_obj) do
		slotNum = item.uutNum
		if slotNum < 0 or slotNum >= self.uuts.n then
			logger:error("slot number %d is wrong in \"setsn\" msg", slotNum)
			
			return false
		end

		if type(item.enable) ~= "boolean" then
			logger:error("enable is not type of boolean at slot number %d", slotNum)
			return false
		end

		uut = self.uuts[slotNum + 1]

		uut.uutEnable = item.enable
		uut.flag = not uut.uutEnable
	end

	for i,v in ipairs(self.uuts) do
		print(v.uutNum, v.uutSN, v.uutEnable, v.flag)
	end

	return true

end

--]]




function server:TriggerRepHandler(json_obj)

	local Result, isRep = pcall(project.TriggerRepHandler, json_obj, self.triggerRep, self.uuts)
	if Result == false then
		-- self.triggerRep:send(common.generateErrorRespMsg(json_obj["function"], json_obj["id"], -21004, "server internal error"))
		logger:error("project.TriggerRepHandler %s : %s", json_obj["method"], tostring(ret));
	end

	-- if json_obj["function"] == "setsn" then
	-- 	if self:HandleSetUutSNMsg(json_obj["params"]) then
	-- 		self.triggerRep:send(common.generateSuccRespMsg(json_obj["function"], json_obj["id"], true))
	-- 	else
	-- 		self.triggerRep:send(common.generateErrorRespMsg(json_obj["function"], json_obj["id"], -21001, "invalid params of request"))
	-- 	end

	if json_obj["method"] == "load" then
		logger:info("file--->>>>>"..tostring(json_obj["params"]["file"]))
		self.triggerRep:send(common.generateSuccRespMsg(json_obj["method"], json_obj["id"], self:SendLoadToSeq(tostring(json_obj["params"]["file"]))))

	elseif json_obj["method"]=="start" then
		self.triggerRep:send(common.generateSuccRespMsg(json_obj["method"], json_obj["id"], true))
		local result, err_msg = self:HandleStartMsg(json_obj["params"])

		-- if result then
		-- 	self.triggerRep:send(common.generateSuccRespMsg(json_obj["function"], json_obj["id"], true))
		-- else
		-- 	self.triggerRep:send(common.generateErrorRespMsg(json_obj["function"], json_obj["id"], -21002, err_msg))
		-- end

	elseif json_obj["method"] == "abort" then
		self:SendAbortToSeq()
		self.triggerRep:send(common.generateSuccRespMsg(json_obj["method"], json_obj["id"], true))

	-- elseif json_obj["function"] == "uutenable" then
	-- 	if self:HandleUutEnableMsg(json_obj["params"]) then
	-- 		self.triggerRep:send(common.generateSuccRespMsg(json_obj["function"], json_obj["id"], true))
	-- 	else
	-- 		self.triggerRep:send(common.generateErrorRespMsg(json_obj["function"], json_obj["id"], -21003, "invalid params of request"))
	-- 	end

	elseif json_obj["method"] == "PROJECT_CMD" then
		local result, ret = pcall(project.DealWithSpecialMsg, json_obj, self.triggerRep, self.uuts)
		if result == false then
			self.triggerRep:send(common.generateErrorRespMsg(json_obj["method"], json_obj["id"], -21004, "server internal error"))
			logger:error("%s", ret)
		end
	elseif not isRep then
		logger:warn("unknown msg type from IDE")
		self.triggerRep:send(common.generateErrorRespMsg(json_obj["method"], json_obj["id"], -21005, "unknown msg type"))
	end
end

local snList = {};
-- local uiMsg = {};
local uutEnableList = {};
function Parse_UI_MSG(par,tabNum)
	-- body
	local tab = {}
	local uiMsg = common.split(par,":");
	for i=1,#uiMsg-1 do
		local x = common.split(uiMsg[i+1],"_");
		tab[x[1]] = x[2];
	end
	if tabNum==1 then
		snList = tab;
	elseif tabNum==2 then
		uutEnableList = tab;
	end		
end

function GenerateStartMsg(tab)
	-- body
	local slotMsg = ""

	for slotNum,SN in pairs(tab) do
		SN = "\""..SN.."\""
		slotMsg = slotMsg..",".."{\"SN\":"..SN..",\"uutNum\":"..slotNum.."}"
		
	end
	logger:info("slotMsg-->>>"..slotMsg)
	local slotMsginfo = string.sub(slotMsg,2,-1)
	local msgCmd = "{\"params\":{\"uutinfo\":["..slotMsginfo.."],\"times\":1},\"function\":\"start\"}"
	logger:info("msgCmd-->>>"..msgCmd)
	return msgCmd
end



function  server:RegisterTriggerRepHandler()
	self.zmqPoller:add(self.triggerRep, zmq.POLLIN, function()
		local msg, more = self.triggerRep:recv()
		if msg then
			logger:info("SM received msg from IDE is: %s", msg)
				local json_obj = json.decode(msg)
				if json_obj ~= nil then
					local result, ret = pcall(self.TriggerRepHandler, self, json_obj)
					if result == false then
						self.triggerRep:send(common.generateErrorRespMsg(json_obj["function"], json_obj["id"], -21004, "server internal error"))
						logger:error("%s", ret)
					end

				else 
					logger:error("decode IDE json msg failed")
					self.triggerRep:send(common.generateErrorRespMsg(json_obj["function"], json_obj["id"], -21007, "invalid msg request"))
				end
			-- if string.find(msg,"UISN") then
			-- 	Parse_UI_MSG(msg,1);
			-- 	self.triggerRep:send("ok")
			-- elseif string.find(msg,"UIUUT") then
			-- 	Parse_UI_MSG(msg,2);
			-- 	self.triggerRep:send("ok")		
			-- else
			-- 	if next(snList) ~= nil then
			-- 		msg = GenerateStartMsg(snList);
			-- 		logger:info("Trigger Start Msg is: %s", msg)
			-- 		snList = {};
			-- 	elseif next(uutEnableList) ~= nil then
			-- 		msg = GenerateStartMsg(uutEnableList);
			-- 		logger:info("Trigger Start Msg is: %s", msg)
			-- 		uutEnableList = {};
			-- 	end

			-- 	local json_obj = json.decode(msg)
			-- 	if json_obj ~= nil then
			-- 		local result, ret = pcall(self.TriggerRepHandler, self, json_obj)
			-- 		if result == false then
			-- 			self.triggerRep:send(common.generateErrorRespMsg(json_obj["function"], json_obj["id"], -21004, "server internal error"))
			-- 			logger:error("%s", ret)
			-- 		end

			-- 	else 
			-- 		logger:error("decode IDE json msg failed")
			-- 		self.triggerRep:send(common.generateErrorRespMsg(json_obj["function"], json_obj["id"], -21007, "invalid msg request"))
			-- 	end
			-- end	
		else
			logger:error("receive msg from IDE failure; error msg: %s", tostring(more))
			self.triggerRep:send(common.generateErrorRespMsg(json_obj["function"], json_obj["id"], -21008, "receive msg from IDE failure"))
		end
	end)
end


function  server:RegisterSeqSubHandler()
	for i,uut in ipairs(self.uuts) do
		self.zmqPoller:add(uut.seqSub, zmq.POLLIN, function()
  			local msg = uut.seqSub:recv()
  			local _sn = ""
  			-- deal with SEQUENCE_END msg
  			if string.match(msg,"\"event\": 4") then 
  				local json_obj = json.decode(msg)
				local SN = json_obj.data.value
				_sn = SN
				local dataMsg = "{\"msgType\":\"REQ\",\"msgName\":\"mlbsn\",\"msgChannel\":0,\"msgParam\":\""..SN.."\",\"msgResult\":0,\"errMsg\":null}"

  			elseif string.match(msg,"\"event\": 1") then  
  				logger:info("SM receive Sequencer %d sub msg: %s", i - 1, msg)
  				uut.flag = true

				result, ret = pcall(project.DealWithTestingEndMsg, json.decode(msg))
				if result == false then
					logger:error("%s", ret)
				end

				--yangyang
				local result = ""
				local json_obj = json.decode(msg)
				local testResult = json_obj.data.result
				if testresult==1 then
					result = "1"
				else
					result = "0"
				end	

	  			if self:CheckAllFlag() then
	  				sm:state_switch(fixtureSM.EVENT.FINISH)
	  				sm:state_switch(fixtureSM.EVENT.WILL_UNLOAD)
	  				sm:state_switch(fixtureSM.EVENT.REMOVED)

	  				self:ClearAllFlag()

	  				-- fixtureSM:press()

	  				self.testingFlag = false
	  				self.loopInfo.currentLoopTimes = self.loopInfo.currentLoopTimes + 1
	  				logger:info("currentLoopTimes: %d, totalLoopTimes: %d", 
	  					       self.loopInfo.currentLoopTimes, self.loopInfo.totalLoopTimes)

	  				if self.loopInfo.totalLoopTimes > self.loopInfo.currentLoopTimes and not self.stopLoopFlag then
	  					ztimer.sleep(200)
	  					self:SendStartToSeq()
	  				else
	  					self:ClearAllSN()
	  					self.loopInfo.currentLoopTimes = 0
	  					logger:info("SM testing finished and Wait for Trigger")
	  				end
	  			end

	  			-- tc:ClearContent(args.module,i-1);
	  		end
		end)	
	end	
end

function server:RegisterSocketsHandler()
	self:RegisterSeqSubHandler()
	self:RegisterTriggerRepHandler()
end


function server:New(ctx, args)
	if self.singleFlag then
		logger:fatal("Server class can have only one instance") 
		return nil, "Server class can have only one instance"
	end

	obj = {loopInfo = {totalLoopTimes = 1, currentLoopTimes = 0},
	       testingFlag = false, triggerRep = nil, zmqPoller = nil, uuts = {}, stopLoopFlag = true }

	setmetatable(obj, self)
	self.__index = self

	self.singleFlag = true

	for i=1, args.slots do
		local uut = {flag = false, uutEnable = true, uutSN = "", socketSN = "", seqSub = nil, seqReq = nil}

		local addr = zmqConf.get_addr("STATEMACHINE_SUB_ADDR", "SEQUENCER_PUB", args.module, args.slots, i - 1)
		logger:info("< StateMachine > Sequencer SUB addr: %s", addr)
		sc, err = ctx:socket{zmq.SUB, connect = addr, subscribe = ""}
		zmq.assert(sc, err)
		uut.seqSub = sc

		addr = zmqConf.get_addr("STATEMACHINE_REQ_ADDR", "SEQUENCER_PORT", args.module, args.slots, i - 1)
		logger:info("< StateMachine > Sequencer REQ addr: %s", addr)
		sc, err = ctx:socket{zmq.REQ, connect = addr}
		zmq.assert(sc, err)
		uut.seqReq = sc

		table.insert(obj.uuts, uut)

		tc:GetFileHandle(args.module,(i-1));
		df:GetFileHandle(args.module,(i-1));
	end

	obj.uuts.n = args.slots

	--StateMachine Trigger Rep, IDE, Fixture...
	local addr = zmqConf.get_addr("STATEMACHINE_REP_ADDR", "SM_PORT", args.module, 1, 0)
	logger:info("< StateMachine > Trigger REP addr: %s", addr)
	obj.triggerRep, err = ctx:socket{zmq.REP, bind = addr}
	zmq.assert(obj.triggerRep, err)

	obj.zmqPoller = zpoller.new(args.slots)
	self.singleFlag = true

	return obj
end

function server:Run()
    logger:info("SM Started successful and wait for trigger")
    self.zmqPoller:start()
end

return server


