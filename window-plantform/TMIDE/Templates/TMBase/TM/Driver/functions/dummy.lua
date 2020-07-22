
local ztimer  = require "lzmq.timer"

local _test = {}

function _test.add(sequence)
	-- return sequence.param1
	return (sequence.param1 + sequence.param2)
end

function _test.skip( sequence)
	return sequence.param1
end

function _test.Sub( sequence)
	-- ztimer.sleep(2000)
	return (sequence.param1 - sequence.param2)
end

function _test.mul( sequence)
	-- return "--FAIL--"
	-- return "--PASS--"
	return (sequence.param1 * sequence.param2)
end

function _test.div( sequence)
	return (sequence.param1 / sequence.param2)
end


function _test.init()
	logger:info("dummy  init")
	return ture
end

function _test.self_test()
	logger:info("dummy self_test")
	return ture
end

function _test.clear_buffer()
	logger:info("dummy  clear_buffer")
end


return _test
