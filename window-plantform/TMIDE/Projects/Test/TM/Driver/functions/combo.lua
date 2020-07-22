
local _Combo = {}
local ztimer  = require "lzmq.timer"
local printf = require("DbgOut").DgbOut
--local tcp = require("tcp")
--local ser = require("serialport")
if args.uut==0 then
	local tcp = require("tcp")
end	

 
local SN = 1
function _Combo.Test(sequence)
	SN = SN+1;
	ztimer.sleep(3000)
	return 22+SN;
end
function _Combo.Test1(sequence)
	ztimer.sleep(500)
	local Msg="{\"msgName\":\"Test\",\"msgParam\":\""..SN.."\"}"
	local seq = {}
	seq.param1=Msg.."uut:"..args.uut.."\r\n"
	SN=SN+1;
	local ret = _Combo.TCPSendReceive(seq)
	printf(Msg.."uut:"..args.uut.."\r\n")
	if ret ==nil or ret =="" then
		printf("Fail to Get Recv result:"..ret)
		error("Fail")
	end
	if string.find(ret,"\r\n") then
		--ret = string.gsub(ret, "[\r\n%*<>/|?:\"\\]*", "")
		ret = string.gsub(ret,"\r\n","");
	end	
	return ret

end
function _Combo.TCPSendReceive(sequence)
	local cmd = sequence.param1
	local data = tcp.SendReceive(cmd)
	return data
end

function _Combo.SerSendReceive(sequence)
	local cmd = sequence.param1
	local data = ser.SendReceive(cmd)
	return data
end

return _Combo