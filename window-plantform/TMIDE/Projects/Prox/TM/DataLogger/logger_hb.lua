
local zthreads = require "lzmq.threads"


--===========================--
-- HB thread
--===========================--
local hb = {}
	



function hb.HeartBeat(ctx, addr)

	local watchdog_thread, watchdog_pipe = zthreads.fork(ctx, function(pipe, address)

	        require("pathManager")
	        require("utils.zhelpers")
	        require("logging.console")
	        local zmq = require("lzmq")
	        local zthreads = require("lzmq.threads")
	        local json = require("dkjson")
	        local ctx = zthreads.get_parent_ctx()
	        local ztimer = require("lzmq.timer")
	        local time_utils = require("utils.time_utils")
	        
	        local logger = logging.console()
			logger:setLevel(logging.INFO)

	        logger:info(" < DataLogger > Set Logger HeartBeat PUB at: %s", address)
	        local watchdog_zmq, err = ctx:socket(zmq.PUB, {bind = address})
	        zassert(watchdog_zmq, err)
	        local lastTime = os.time()

	        while true do
	            if pipe:poll(1000) then
	                local msg = pipe:recv()
	                if #msg > 0 then
	                	watchdog_zmq:send_all{"101",time_utils.get_time_string_ms(),3,"DataLogger",msg}
	            	end
	            end

	            local currentTime = os.time()
	            if currentTime - lastTime >= 5 then
	                lastTime = currentTime
	                watchdog_zmq:send_all{"101",time_utils.get_time_string_ms(),3,"DataLogger","FCT_HEARTBEAT"}
	            end
	        end
	    end, addr)

	assert(watchdog_thread, watchdog_pipe)

	return watchdog_thread, watchdog_pipe
end


return hb