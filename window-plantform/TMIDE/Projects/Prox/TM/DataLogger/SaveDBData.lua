local _DBData = {};

local DLCommon = require("DLCommon")
local DLContext = require("DLContext")
local CurrDir = JoinPath(CurrentDir(),"DataLogger");

_DBData.FolderPath = "C:\\dbdata\\"
_DBData.ModuleSN   = "";

os.execute("mkdir \"".._DBData.FolderPath.."\"");

function _DBData:Save()
	local nowTime = os.time();
	local filename =  DLContext.MLBSN .. os.date("%Y_%m_%d%H_%M_%S",nowTime) .. ".csv"
	local content = DLContext.MLBSN .. "," .. os.date("%Y/%m/%d %H:%M:%S",nowTime) .. "," .. DLContext.TestResult .. "," .. tostring(self.ModuleSN)
	
	local file,msg = io.open(self.FolderPath..filename,"w")
	if file then
		file:write(tostring(content));
		file:flush();
		file:close();
		logger:info("write dbdata csv successful\n");
	else
		logger:info("can not write file:"..self.FolderPath..filename);
		logger:info("error msg is:"..tostring(msg));
	end

	collectgarbage("collect")
end

function _DBData:ResolveMessage(MsgTime,MsgContent)
	-- body
	if MsgContent.event == DLCommon.MsgType.MSG_SEQUENCE_START then
		self.ModuleSN = "";
		return;

	elseif MsgContent.event == DLCommon.MsgType.MSG_ITEM_END then
		
		if tostring(MsgContent.data.tid) == "#ROSALINE_MODULE_SN" then
			self.ModuleSN = tostring(MsgContent.data.value);
		end
		return;

	elseif MsgContent.event == DLCommon.MsgType.MSG_SEQUENCE_END then

		self:Save();
		self.ModuleSN = "";
		return;
	end
end

return _DBData;