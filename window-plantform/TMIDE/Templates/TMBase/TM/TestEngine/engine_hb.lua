
local zthreads = require "lzmq.threads"

--===========================--
-- Watchdog looper thread
--===========================--
local hb = {}


function hb.HeartBeat(context, addr)

	local watchdog_thread, watchdog_pipe = zthreads.fork(context, function(pipe, address)

	        require("pathManager")
	        require("utils.zhelpers")
	        require("logging.console")
	        local zmq = require "lzmq"
	        local zthreads = require "lzmq.threads"
	        local context = zthreads.get_parent_ctx()
	        local logger = logging.console()
			logger:setLevel(logging.INFO)
	        
	        local time_utils = require("utils.time_utils")
	        local  t = require("lzmq.timer")

	        logger:info(" < TE > Set Engine HeartBeat PUB at: %s", address)
	        local watchdog_zmq, err = context:socket(zmq.PUB, {bind = address})
	        zassert(watchdog_zmq, err)
	        local lastTime = os.time()

	        while(1) do
	            if pipe:poll(1000) then
	                local msg = pipe:recv()
	                watchdog_zmq:send_all{"101",time_utils.get_time_string_ms(),3,"TestEngine",msg..'\0'}
	            end

	            local currentTime = os.time()
	            if currentTime - lastTime >= 5 then
	                lastTime = currentTime
	                watchdog_zmq:send_all{"101",time_utils.get_time_string_ms(),3,"TestEngine","FCT_HEARTBEAT"}
	            end
	        end
	    end, addr)

	assert(watchdog_thread, watchdog_pipe)

	return watchdog_thread, watchdog_pipe
end


return hb