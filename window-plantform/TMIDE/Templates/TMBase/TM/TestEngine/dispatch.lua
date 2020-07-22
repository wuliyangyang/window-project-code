
require("utils.zhelpers")

local path = require("pl.path")
local device_modules = require("devices_operation")

local dispatch_table = nil
if path.exists(JoinPath(JoinPath(args.dir, "Driver"), "function_table.lua")) then
    dispatch_table = require("function_table")
end


local _M = {}

--===========================--
-- Function dispatch
--===========================--

function _M.dispatch_function(sequence_obj)

    local value = nil

    -- Get the function from the dispatch table
    local t = sequence_obj["method"]:split("%.")

    if #t ~= 1 and #t ~= 2 then
        error(string.format("%s is wrong format", sequence_obj["method"]))
    end

    local device_name = nil
    local func = t[#t]
    local m = nil

    if func == "start_test" or func == "end_test" then
        device_name = "callback"
        m = require(device_name)
    elseif #t == 2 then
        device_name = table.concat(t, ".", 1, #t - 1)
    	m = device_modules.get_module_of_device(device_name)
    elseif #t == 1 and dispatch_table then
        m = dispatch_table
    end

    if not m or type(m) ~= "table" then
        error(string.format("%s is not exist or a type of table", device_name))
    elseif not m[func] then
        error(string.format("function %s is not exist in file %s", func, device_name))
    end

    -- print(string.format("%s type is %s", tostring(m[func]), type(m[func])))

    if type(m[func]) == "function" then
        if func == "start_test" then
            device_modules.clear_devices_buffer()
        end

        value = m[func](sequence_obj)

        if value == true then
            value = "--PASS--"
        elseif value == false then
            value = "--FAIL--"
        elseif value == nil then 
            value = ""  
        end

    else
        error(string.format("%s is not function type", func))
    end

    -- Return the results
    return value
end

return _M

