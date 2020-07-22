local _CsvHandle = {}

-- local time_utils = require("utils.time_utils")

local header = "index,group,groupinfo,item,starttime,endtime,testtime,low,high,unit,value,result"
local content = {}
local index = 0

local default = {--this is for groupinfo
	TSCR = "test session creation",
	SCAN = "scan barcode",
	TSED = "test session ending",
	LOTS = "Information to start a new lot",
	LOTE = "Information for ending a lot",
}

local function getTimeMs()
	-- local t = time_utils.get_unix_time_ms()
	-- local t_ms = t - 1000 * math.floor(t/1000)
	-- return os.date("%m-%d_%H:%M:%S.", os.time())..string.format("%03d",t_ms)
	return os.date("%m-%d_%H:%M:%S.000", os.time())
end

function _CsvHandle.initCsv()
	index = 0
	content = {}
	collectgarbage("collect")
end

local function formatString(par)
	if par then
		local group,groupinfo,item,starttime,endtime,testtime,low,high,unit,value,result = "","","","","","0","","","","",""
		if par.group then
			group = tostring(par.group)
		end
		if par.groupinfo then
			groupinfo = tostring(par.groupinfo)
		elseif default[group] then
			groupinfo = default[group]
		end
		if par.item then
			item = tostring(par.item)
		end
		if par.starttime then
			starttime = tostring(par.starttime)
		else
			starttime = getTimeMs()
		end
		if par.endtime then
			endtime = tostring(par.endtime)
		else
			endtime = getTimeMs()
		end
		if par.testtime then
			testtime = tostring(par.testtime)
		else
			testtime = "0"
		end
		if par.low then
			low = tostring(par.low)
		end
		if par.high then
			high = tostring(par.high)
		end
		if par.unit then
			unit = tostring(par.unit)
		end
		if par.value then
			value = tostring(par.value)
		end
		if par.result then
			result = tostring(par.result)
		else
			result = "PASS"
		end

		return string.format("%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s",group,groupinfo,item,starttime,endtime,testtime,low,high,unit,value,result)
	else
		return nil
	end
end

function _CsvHandle.addCsv(par)--group,groupinfo,item,value,result
	local str = formatString(par)
	if str then
		index = index + 1
		content[index] = tostring(index) .. "," .. str
	end
end

function _CsvHandle.insertCsv(row,par)--group,groupinfo,item,value,result
	local str = formatString(par)
	if str then
		content[row] = tostring(row) .. "," .. str
	end
end

function _CsvHandle.getCsv()
	table.insert(content,1,header)
	return table.concat(content, "\n")
end

return _CsvHandle