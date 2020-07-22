
local DLCommon = require("DLCommon")
local _DLContext = {};

_DLContext.ScriptName   = ""
_DLContext.ScriptPath   = ""
_DLContext.StartTime  = ""
_DLContext.StopTime   = ""
_DLContext.TotalTime  = ""
_DLContext.MLBSN      = ""
_DLContext.SocketSN   = ""
_DLContext.TestResult = ""
_DLContext.ErrorMsg   = ""
_DLContext.FailItems  = ""

_DLContext.PivotLogFullPath = "";

function _DLContext:ClearContext()
	-- body
	self.ScriptName = ""
	self.ScriptPath = ""
	self.StartTime  = ""
	self.StopTime   = ""
	self.TotalTime  = ""
	self.TestDate   = ""
	self.MLBSN      = ""
	self.SocketSN   = ""
	self.TestResult = ""
	self.isPubResult = false;
end

function _DLContext:ResolveMessage(MsgTime,MsgContent)
	if MsgContent.event == DLCommon.MsgType.MSG_FILE_HEAD then
		local CsvFile = tostring(MsgContent.file);
		self.ScriptPath,self.ScriptName = string.match(CsvFile,"(.+)/(.+)%.csv"); 
		if self.ScriptPath == nil then
			self.ScriptPath,self.ScriptName = string.match(CsvFile,"(.+)\\(.+)%.csv");
		end
		return;

	elseif MsgContent.event == DLCommon.MsgType.MSG_ATIRIBUTE_FOUND then
		if MsgContent.data.name == "MLBSN" then
        	self.MLBSN = MsgContent.data.value;
        elseif MsgContent.data.name == "SocketSN" then
        	self.SocketSN = MsgContent.data.value;
		end
		return;

	elseif MsgContent.event == DLCommon.MsgType.MSG_SEQUENCE_START then
		self:ClearContext();
		self.StartTime = os.date("%Y").."-"..tostring(MsgTime);
		self.TestDate  = string.match(self.StartTime,"(.+)_");
		return;

	elseif MsgContent.event == DLCommon.MsgType.MSG_SEQUENCE_END then
		self.StopTime = os.date("%Y").."-"..tostring(MsgTime);
		self.TotalTime = DLCommon:getDeltaTime(self.StartTime,self.StopTime);
		if MsgContent.data.result == 1 then
			self.TestResult = "PASS";
		else
			self.TestResult = "FAIL"
		end
		return;

	end
end

return _DLContext;