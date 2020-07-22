
--===========================--
-- add devices here. device name must be file suffix of ".lua" where places in "Driver\functions"
--===========================--
local device_list = {
	"dummy.lua"
}


local device_list_dfu = {
	"dummy.lua",
	"ser.lua"	
}

local station = nil  --parseStation()

if station == "FCT" then
	return device_list_dfu
else
	return device_list
end


