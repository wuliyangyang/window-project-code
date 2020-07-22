
local _DLCommon = {};

_DLCommon.MsgType = {
	MSG_SEQUENCE_START  = 0,
	MSG_SEQUENCE_END    = 1,
	MSG_ITEM_START      = 2,
	MSG_ITEM_END        = 3,
	MSG_ATIRIBUTE_FOUND = 4,
	MSG_REPORT_ERROR    = 5,
	MSG_UOP_DETECT      = 6,
	MSG_FILE_HEAD       = 9,
};

function _DLCommon:readFile(path)
	-- body
	local fp = io.open(path,"r");
	if fp == nil then
		logger:info("< readFile > : read file fail");
		return nil;
	end
	local Data = fp:read("*all");
	fp:close();

	return Data;
end

function _DLCommon:split(str, delimiter)  
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

function _DLCommon:ParseFileOrFolderName(fileName)
    -- body
    local newName = tostring(fileName);
    newName = string.gsub(newName,"[%*<>/|?:\"\\]*","");
    return newName;
end

function _DLCommon:getDeltaTime(time1,time2)
	local flag1;
	local startIndex1;
	local realtime1;
	local flag2;
	local startIndex2;
	local realtime2;

	flag1,startIndex1 = string.find(time1,"_");
	if	flag1 then
		realtime1 = string.sub(time1,startIndex1+1,#time1);
	else
		return "-999999";
	end

	flag2,startIndex2 = string.find(time2,"_");
	if	flag2 then
		realtime2 = string.sub(time2,startIndex2+1,#time2);
	else
		return "-999999";		
	end

	local timeArray1 = self:split(realtime1,":");
	local timeArray2 = self:split(realtime2,":");
	local hour1 = tonumber(timeArray1[1]);
	local min1 = tonumber(timeArray1[2]);
	local sec1 = tonumber(timeArray1[3]);
	local hour2 = tonumber(timeArray2[1]);
	local min2 = tonumber(timeArray2[2]);
	local sec2 = tonumber(timeArray2[3]);

	local converToSecondTime1 = 0;
	converToSecondTime1 = hour1*60*60 + min1*60 + sec1;
	local converToSecondTime2 = hour2*60*60 + min2*60 + sec2;
	if hour2 < hour1 then
		converToSecondTime2 = converToSecondTime2 + 24*60*60;
	end

	--modify
	local s_ms = string.match(time1,"%.%d+")
	local e_ms = string.match(time2,"%.%d+")
	local deltaTime = converToSecondTime2 - converToSecondTime1 + (e_ms - s_ms)/1000000
	return string.format("%.3f",deltaTime)
	-- local deltaTime = converToSecondTime2 - converToSecondTime1;
	-- return deltaTime;
end

return _DLCommon;