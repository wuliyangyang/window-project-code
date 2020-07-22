
local json = require("dkjson")
local zmq = require("lzmq")
require("utils.zhelpers")
local time_utils = require("utils.time_utils")
local Reporter = require("Reporter")



local _State = {}

function _State:new(initial_state)
	local obj = {curr_state = initial_state, fixture = nil, pipe = nil, sm = {}}
	setmetatable(obj, self)
	self.__index = self

	return obj
end

function _State:set_initial_state(initial_state)
	self.curr_state = initial_state
end

function _State:set_fixture(in_fixture, in_pipe)

	self.fixture = in_fixture
	self.pipe = in_pipe
end

function _State:PubState(inState, errorcode, errormsg)
	self.pipe.send(Reporter.CreateStateReportMsg(inState, errorcode, errormsg))
end
 
function _State:add_transition(old_state, in_new_state, event, in_func)
	self.sm[old_state] = self.sm[old_state] or {}
	self.sm[old_state][event] = {func = in_func, new_state = in_new_state}
end


function _State:state_switch(event, ...)
	local fixtureSM = require("FixtureSM")
	logger:info("curr_state:%s event:%s", self.curr_state, event)
	local new_state = self.sm[self.curr_state][event].new_state
	local func = self.sm[self.curr_state][event].func
	local errorcode = 0;
	local errormsg = "";
	local isMoOk = true;

	if func then
		if args.motion > 0 then 
			isMoOk = false;
			errorcode = -999;
			errormsg = "unknow error";
			for i=1,args.motion do
				local MotionRet = func(self.fixture, unpack(arg));
				if MotionRet then
					local MoRet = json.decode(MotionRet);
					errorcode = MoRet.errorCode;
					errormsg = MoRet.errorMsg;
					if errorcode >= 0 then
						isMoOk = true;
					end
				end

				if isMoOk then
					break;
				else
					time_utils.delay(5);
				end
			end
		end
	else
		logger:warn("there is no function for state:%s event:%s", self.curr_state, event)
	end

	--if not (new_state == fixtureSM.STATE.TESTING and (not isMoOk)) then
	if new_state ~= fixtureSM.STATE.TESTING or isMoOk then
		logger:info("sm state %s --> %s", self.curr_state, new_state)
		self.curr_state = new_state
	else
		logger:info("start fail, keep the current state: %s", self.curr_state)
	end

	self:PubState(self.curr_state, errorcode, errormsg)

	return isMoOk;
end

function _State:printf()
	for state,v in pairs(self.sm) do
		logger:info("state:%s", state)
		for event,v in pairs(v) do
			logger:info("\tevent:%s", event)
			for k,v in pairs(v) do
				logger:info("\t\t %s\t%s", tostring(k),tostring(v))
			end
		end

		logger:info("\n")
	end
end




return _State
