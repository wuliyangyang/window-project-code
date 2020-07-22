

local callback = {}



function callback.start_test(par)    --Initial function for startup test; You can add test initial code in here
	
	return true
end

function callback.end_test(par)   --Clear function for normal test finish.you can add clear function code in there when test normally finish.
	
	return true
end



return callback
