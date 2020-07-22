
local path = require("pl.path")

local devices = {modules_of_device = {}}

local prefix_path = "functions"


--===========================--
-- load devices 
--===========================--
function devices.get_modules_of_device(device_list)

    for _,device in ipairs(device_list) do
    	
    	local s, t = string.find(device, "%.lua")
    	assert(s ~= nil, "device's name in devices.lua must be format of *.lua")
    	device = string.sub(device, 1, s - 1)
    	
        local file = device
        local m = require(file)
        assert(type(m) == "table", string.format("return value of require %s is not a table!!", file))
        devices.modules_of_device[device] = m
    end
end

function devices.get_module_of_device(device_name)
    return devices.modules_of_device[device_name]
end

local function modules_common_func(modules, func)
    for _,m in pairs(modules) do
        if type(m[func]) == "function" then
            local ret, msg = pcall(m[func])
            if not ret then
                logger:error("error msg is: %s", msg)
            end
        end
    end
end

--===========================--
-- do devices init 
--===========================--
function devices.init_devices()
    local func = "init"
    modules_common_func(devices.modules_of_device, func)
end


--===========================--
-- do devices self test 
--===========================--
function devices.device_self_test()
    local func = "self_test"
    modules_common_func(devices.modules_of_device, func)
end


--===========================--
-- clear devices buffer
--===========================--
function devices.clear_devices_buffer()
    local func = "clear_buffer"
    modules_common_func(devices.modules_of_device, func)
end


return devices