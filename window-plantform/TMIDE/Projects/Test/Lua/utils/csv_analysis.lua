
print("< csv_analysis > Load utils.csv_analysis.lua ...")
--[[---------------------------------
--Version : V1.0
--Author  : Yuekie
--Date    : 20170122
--Comment : First Relese

--Version : V1.1
--Author  : Yuekie
--Date    : 20170122
--Comment : -- add LINE_KEY invalid judgement.
--]]---------------------------------

local LINE_KEY = "TID"
local _CSV_ALS_ = {};

local function split(str, delimiter)  
  if (delimiter=='') then return false end  
  local pos,arr = 0, {}  
  -- for each divider found  
  for st,sp in function() return string.find(str, delimiter, pos, true) end do  
    table.insert(arr, string.sub(str, pos, st - 1))  
    pos = sp + 1  
  end  
  table.insert(arr, string.sub(str, pos))  
  return arr  
end

local function getIndexForValue(table,value)
	-- body
	for k,v in pairs(table) do
		-- print(k,v)
		if value == v then
			return k;
		end
	end

	return nil;
end

function _CSV_ALS_.csvDataAnalysis(csvData,line_key)
	local split_key = "\n";
	local keyTable = {};
	local retTable = {};
	local keyIndex = 1;
	if csvData == nil or csvData == "" then 
		print("csv_analysis->>csvDataAnalysis: invalid csv data");
		return nil,"invalid csv data";
	end

	csvData = tostring(csvData);
	if string.find(csvData,"\r\n") then
		split_key = "\r\n";
	elseif string.find(csvData,"\r") then
		split_key = "\r";
	elseif string.find(csvData,"\n") then
		split_key = "\n";
	end

	if line_key then
		LINE_KEY = line_key;
	end

	local lineDataTable = split(csvData,split_key);
	if #lineDataTable > 1 then
		for i,v in ipairs(lineDataTable) do
			-- print(i,v)
			if i == 1 then
				keyTable = split(v,",");
				keyIndex = getIndexForValue(keyTable,LINE_KEY);

				if keyIndex == nil then
					print("csv_analysis->>invalid param2,can not find this string from csv title");
					return nil,"invalid param2,can not find this string from csv title"
				end
			else
				local contTbale = split(v,",");
				local lineKey = contTbale[keyIndex];
				if lineKey then
					lineKey = split(lineKey," ")[1];
					retTable[lineKey] = {};

					for i,v in ipairs(contTbale) do
						-- print(i,v)
						retTable[lineKey][keyTable[i]] = v;
					end
				else
					print("csv_analysis->>line ",i,"do not have valid data");
				end
			end
		end

		print("csv_analysis->>csvDataAnalysis complete");
		return retTable;
	else
		print("csv_analysis->>csvDataAnalysis: csv data item less than 2");
		return nil,"csv data item less than 2";
	end
end


print("< csv_analysis > Load utils.csv_analysis.lua Success")

return _CSV_ALS_;