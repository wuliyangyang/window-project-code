print("< zmq_proxy > Load functions.zmq_proxy.lua ...")

local zmq = require("lzmq")
local _zmq = {}
_zmq.connect_table = {}
_zmq.ip_table = {}
local context = zmq.context();
local time_utils = require("utils.time_utils")
function _zmq.open(connect_name, ip, timeout)

	_zmq.close(connect_name);

	-- local context = zmq.context()
	local connect, err = context:socket{zmq.REQ, connect=ip}
	if err ~= nil then
		print("connect fail: ", connect_name,"err: ",err)
		print("try to reconnect ", connect_name)
		connect, err = context:socket{zmq.REQ, connect=ip}
		if err ~= nil then
			print("reconnect fail: ", connect_name,"err: ",err)
			return false;
		end
	end
	_zmq.connect_table[connect_name] = connect
	_zmq.ip_table[connect_name] = ip
	local t = 20000
	if timeout then
		t = timeout
	end
	connect:set_rcvtimeo(t)
	print("[client] create a client: ", ip, "timeout", t)
	return true;
end

function _zmq.send_and_recv(connect_name, msg,timeout)
	local connect = _zmq.connect_table[connect_name]
	print("< zmq_proxy > zmq socket : ",connect_name," message : ",msg,"connect : ",connect);

	if connect == nil then
		return nil,"--FAIL--zmq for '"..tostring(connect_name).."' do not connect yet"
	end

	local ret, err = connect:send(msg)
	print("< zmq_proxy > Send: ", ret,"error:",err)
	if err ~= nil then
		print("send : ", ret, ", error:", err)
		time_utils.delay(30);
		local connect_ip = _zmq.ip_table[connect_name];
		local isOpen = _zmq.open(connect_name,connect_ip,100);
		if not isOpen then
			isOpen = _zmq.open(connect_name,connect_ip,100);
		end

		if isOpen then
			connect = _zmq.connect_table[connect_name]
			ret, err = connect:send(msg)
			print("< zmq_proxy > reSend: ", ret,"error:",err)
			if err ~= nil then
				return nil, err
			end
		else
			print("zmq_proxy->send_and_recv : reopen socket fail")
			return nil,"reopen socket fail"
		end
	end
	if timeout ~= nil and tonumber(timeout) then
		if tonumber(timeout) >= 200 then
			connect:set_rcvtimeo(timeout - 100)
		else
			connect:set_rcvtimeo(timeout)
		end
	else
		connect:set_rcvtimeo(5000)
	end

	local responde, err = connect:recv()	
	print("< zmq_proxy > receive:",responde, "error:", err);

	return responde, err;
end

function _zmq.send(connect_name, msg)
	local connect = _zmq.connect_table[connect_name]
	connect:send(msg)
end

function _zmq.recv(connect_name)
	local connect = _zmq.connect_table[connect_name]
	return connect:recv()
end

function _zmq.close(connect_name)
	local connect = _zmq.connect_table[connect_name];
	if connect ~= nil then
		connect:close()
		_zmq.connect_table[connect_name] = nil
		_zmq.ip_table[connect_name] = nil
		return true;
	end

	return true;
end	

print("< zmq_proxy > Load functions.zmq_proxy.lua Success")

return _zmq