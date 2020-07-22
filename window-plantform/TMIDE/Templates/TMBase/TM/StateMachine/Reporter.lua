
local json = require("dkjson")


local _M = {}


function _M.CreateStateReportMsg(inState)
	local fixtureSM = require("FixtureSM")
	local state_to_eventID = {
		[fixtureSM.STATE.IDLE] = 0, 
		[fixtureSM.STATE.LOADED] = 1,
		[fixtureSM.STATE.TESTING] = 2,
		[fixtureSM.STATE.DONE] = 3,
		[fixtureSM.STATE.UNLOAD] = 4,
		[fixtureSM.STATE.ERROR] = 5
	}

	inState = tostring(inState)

	local t = {event = state_to_eventID[inState], data = {state = inState}} 
	local msg = json.encode(t)
	
	logger:info("state report msg is:%s", msg)

	return msg
end


return _M