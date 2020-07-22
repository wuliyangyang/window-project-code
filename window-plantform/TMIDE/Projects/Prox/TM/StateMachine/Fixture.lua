
local zmq = require("lzmq")


local fixture = {}

function fixture:new(ctx, sub_addr, req_addr)

	local obj = {state = nil, req = nil, sub = nil, loopTestFlag = false, needZmqFLag = false, isResetOK = false}
	setmetatable(obj, self)
	self.__index = self
	
	logger:info("< Fixture Client > Set SUB : %s", sub_addr)
	obj.sub, err = ctx:socket{zmq.SUB, connect = sub_addr, subscribe = ""}
	zmq.assert(obj.sub, err)
	
	logger:info("< Fixture Client > Set REQ : %s", req_addr)
	obj.req, err = ctx:socket{zmq.REQ, connect = req_addr}
	zmq.assert(obj.req, err)

	obj.req:set_rcvtimeo(3000)

	obj.needZmqFLag = (args.motion > 0);

	return obj
end



function fixture:send_msg(msg)
	if (self.needZmqFLag) then
		self.req:send(msg)

		logger:info("msg send to fixture is: %s", msg)


		local recv_msg, more = self.req:recv()
		if recv_msg then
			logger:info("msg receive from fixture is: %s", recv_msg)
			return recv_msg
		else
			logger:warn("receive msg from fixture failure; error msg: %s", tostring(more))
			return nil
		end

	else
		return nil
	end
end



function fixture:close()
	return self:send_msg("in")
end

function fixture:press()
	return self:send_msg("down")
end

function fixture:remove()
	return self:send_msg("remove")
end

function fixture:release()
	logger:info("fixture loop mode is: %s", tostring(self.loopTestFlag))

	if self.loopTestFlag then
		return self:send_msg("out")
	else
		return self:send_msg("release")
	end
end


function fixture:open()
	return self:send_msg("out")
end

-- {"state":"SM_TESTING_DONE","errorMsg":"control Pogo Up successful"},"event":3}

function fixture:start()
	local ret1 = self:close()
	local ret2 = self:press()
	if ret1 and ret2 then
		if string.find(ret1,"\"errorCode\":%-") then
			return ret1;
		elseif string.find(ret2,"\"errorCode\":%-") then
			return ret2;
		else
			return "{\"method\":\"start\",\"errorCode\":0,\"errorMsg\":\"start successful\"}";
		end
	else
		return nil;
	end
end

function fixture:get_fixture_id()
	local r = self:send_msg("readid")
	return string.match(r, "ReadID[\s\t](.+)[\r\n])")
end

function fixture:SetLoopTestFlag(flag)
	self.loopTestFlag = flag
end


return fixture

