

local callback = {}
local printf = require("DbgOut").DgbOut

function callback.start_test(par)    --Initial function for startup test; You can add test initial code in here
	printf("start_test".."uut:"..args.uut.."\r\n");
	return true
end

function callback.end_test(par)   --Clear function for normal test finish.you can add clear function code in there when test normally finish.
	
	printf("end_test".."uut:"..args.uut.."\r\n");
	return true
end



return callback
