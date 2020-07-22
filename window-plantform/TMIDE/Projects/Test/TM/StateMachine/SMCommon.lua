
local json = require("dkjson")
local uuid = require("utils.uuid")

uuid.seed()


local M = {}

local STATEMACHINE_RPC_VERSION = "1.0"
local SEQUENCER_RPC_VERSION = '2.0'


function M.generateReqMsg(func, in_params)
	local msgTbl = {
		["method"] = func,
		jsonrpc = SEQUENCER_RPC_VERSION,
		id = uuid(),
	}


	if in_params then
		msgTbl.args = in_params
	else
		msgTbl.args = {}
	end
	
	local msg = json.encode(msgTbl)
	logger:info("Req msg is %s", msg)
	return msg
end

function M.generateSuccRespMsg(func, id, in_result, par)

	local msgTbl = {
		["function"] = func,
		result = in_result == nil or in_result,
		jsonrpc = STATEMACHINE_RPC_VERSION,
		id = id or uuid(),
		params = par or {},
	}
	msgTbl["module"] = args.module;
	
	local msg = json.encode(msgTbl)
	logger:info("Succ resp msg is %s", msg)
	return msg
end


function M.generateErrorRespMsg(func, id, error_code, error_msg)

	local msgTbl = {
		["function"] = func,
		jsonrpc = STATEMACHINE_RPC_VERSION,
		id = id or uuid(),
		["error"] = {
	        message = error_msg or "",
	        code = error_code or -20000
	    	}
	}
	msgTbl["module"] = args.module;

	local msg = json.encode(msgTbl)
	logger:info("Error resp msg is %s", msg)
	return msg
end

function M.split(s, delimt)
    if type(delimt) ~= "string" or string.len(delimt) <= 0 then
        return
    end
    local start = 1
    local t = {}
    while true do
    local pos = string.find (s, delimt, start, true)
        if not pos then
          break
        end
        table.insert (t, string.sub (s, start, pos - 1))
        start = pos + string.len (delimt)
    end
    table.insert (t, string.sub (s, start))
    return t
end

return M