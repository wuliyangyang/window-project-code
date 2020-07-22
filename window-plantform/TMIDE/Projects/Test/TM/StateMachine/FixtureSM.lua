
local json = require("dkjson")
local zmq = require("lzmq")
require("utils.zhelpers")

local fixture = require("Fixture")
local _State = require("StateSwitch")



local FixtureState = _State:new()


FixtureState.EVENT = {
	ABORT = "ABORT",
	LOADED = "UUT_LOAD",
	START = "TEST_START",
	FINISH = "TEST_FINISH",
	ERROR = "ERROR",
	WILL_UNLOAD = "UUT_WILL_UNLOAD",
	REMOVED = "UUT_REMOVED"
}

FixtureState.STATE = {
	IDLE = "SM_IDLE",
	LOADED = "SM_UUT_LOADED",
	TESTING = "SM_TESTING",
	DONE ="SM_TESTING_DONE",
	UNLOAD = "SM_UUT_UNLOADED",
	ERROR = "SM_ERROR",
	RESET = "SM_RESET"
}



function FixtureState:CreateFixture(ctx, in_pipe, args)
	self:set_initial_state(FixtureState.STATE.IDLE)

	-- self.fixtureObj = fixture:new(ctx, zmqConf.get_addr("FIXTURE_CTRL_SUB_ADDR", "FIXTURE_CTRL_PUB", args.module, 1, 0),
	--     zmqConf.get_addr("FIXTURE_CTRL_REQ_ADDR", "FIXTURE_CTRL_PORT", args.module, 1, 0)
	-- )
	self.fixtureObj = fixture:new(ctx, "tcp://127.0.0.1:6465","tcp://127.0.0.1:6470")

	self:set_fixture(self.fixtureObj, in_pipe)
end

function FixtureState:SetLoopTestFlag(flag)
	logger:info("set fixture's loopTestFlag to : %s", tostring(flag))
	self.fixtureObj:SetLoopTestFlag(flag)
end



function FixtureState:InitSM()
	self:add_transition(FixtureState.STATE.IDLE, FixtureState.STATE.LOADED, FixtureState.EVENT.LOADED, self.fixtureObj.close)
	self:add_transition(FixtureState.STATE.IDLE, FixtureState.STATE.TESTING, FixtureState.EVENT.START, self.fixtureObj.start)
	
	self:add_transition(FixtureState.STATE.LOADED, FixtureState.STATE.TESTING, FixtureState.EVENT.START, self.fixtureObj.start)
	self:add_transition(FixtureState.STATE.LOADED, FixtureState.STATE.IDLE, FixtureState.EVENT.ABORT, nil)
	
	self:add_transition(FixtureState.STATE.TESTING, FixtureState.STATE.DONE, FixtureState.EVENT.FINISH, self.fixtureObj.release)

	self:add_transition(FixtureState.STATE.DONE, FixtureState.STATE.UNLOAD, FixtureState.EVENT.WILL_UNLOAD, self.fixtureObj.open)
	self:add_transition(FixtureState.STATE.DONE, FixtureState.STATE.TESTING, FixtureState.EVENT.START, self.fixtureObj.press)
	
	self:add_transition(FixtureState.STATE.UNLOAD, FixtureState.STATE.IDLE, FixtureState.EVENT.REMOVED, self.fixtureObj.remove)
	self:add_transition(FixtureState.STATE.UNLOAD, FixtureState.STATE.TESTING, FixtureState.EVENT.START, self.fixtureObj.close)

	self:printf()
end

function FixtureState:press()
	return fixture:press()
end


return FixtureState
