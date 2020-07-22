
local json = require("dkjson")
local zmq = require("lzmq")
require("utils.zhelpers")

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

function _State:PubState(inState)
	self.pipe.send(Reporter.CreateStateReportMsg(inState))
end


function _State:add_transition(old_state, in_new_state, event, in_func)
	self.sm[old_state] = self.sm[old_state] or {}
	self.sm[old_state][event] = {func = in_func, new_state = in_new_state}
end


function _State:state_switch(event, ...)

	logger:info("curr_state:%s event:%s", self.curr_state, event)
	local new_state = self.sm[self.curr_state][event].new_state
	local func = self.sm[self.curr_state][event].func

	if func then
		func(self.fixture, unpack(arg))
	else
		logger:warn("there is no function for state:%s event:%s", self.curr_state, event)
	end

	logger:info("sm state %s --> %s", self.curr_state, new_state)
	self.curr_state = new_state

	self:PubState(self.curr_state)
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
