
local zmq = require("lzmq")


local fixture = {}



function fixture:new(ctx, req_addr, sub_addr)

	local obj = {state = nil, req = nil, sub = nil, loopTestFlag = false, needZmqFLag = false}
	setmetatable(obj, self)
	self.__index = self
	
	logger:info("< Fixture Client > Set SUB : %s", sub_addr)
	obj.sub, err = ctx:socket{zmq.SUB, connect = sub_addr, subscribe = ""}
	zmq.assert(obj.sub, err)
	
	logger:info("< Fixture Client > Set REQ : %s", req_addr)
	obj.req, err = ctx:socket{zmq.REQ, connect = req_addr}
	zmq.assert(obj.req, err)

	obj.req:set_rcvtimeo(3000)

	obj.needZmqFLag = true

	return obj
end



function fixture:send_msg(msg)
	if (self.needZmqFLag) then
		self.req:send(msg..'\0')

		logger:info("msg send to fixture is: %s", msg..'\0')


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
	self:send_msg("in")
end

function fixture:press()
	self:send_msg("down")
end

function fixture:release()
	logger:info("fixture loop mode is: %s", tostring(self.loopTestFlag))

	if self.loopTestFlag then
		self:send_msg("out")
	else
		self:send_msg("release")
	end
end


function fixture:open()
	self:send_msg("out")
end


function fixture:start()
	self:close()
	self:press()
end

function fixture:get_fixture_id()
	local r = self:send_msg("readid")
	return string.match(r, "ReadID[\s\t](.+)[\r\n])")
end

function fixture:SetLoopTestFlag(flag)
	self.loopTestFlag = flag
end


return fixture

