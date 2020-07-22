
local DLCommon = require("DLCommon")
local DLContext = require("DLContext")
local LogPub = require("LoggerPub")
local json = require("dkjson")
local _StatisticsLog = {};


_StatisticsLog.Name = "";
_StatisticsLog.Path = "C:\\vault\\Intelli_log";
_StatisticsLog.FullPath = _StatisticsLog.Path.."\\Module"..tostring(args.module).."_Slot"..tostring(args.uut).."_TestScript.csv";

_StatisticsLog.ItemsName     = {};
_StatisticsLog.ItemsUpper    = {};
_StatisticsLog.ItemsLower    = {};
_StatisticsLog.ItemsUnit     = {};
_StatisticsLog.ItemsValue    = {};

_StatisticsLog.ItemsErrMsg   = "";
_StatisticsLog.ItemsFailList = {};

function _StatisticsLog:isLogExisted(dateTime)
	self.FullPath = self.Path.."\\"..self.Name.."_"..dateTime..".csv"

	logger:info("self.FullPath %s",self.FullPath);
	local fileFlag = io.open(self.FullPath, "r");
	if	fileFlag then
		fileFlag:close();
		return true;
	else
		return false;
	end
end

function _StatisticsLog:WriteLogContent(filepath,content)
	--Appended the content at the end of the file	
	local csvFile,msg= io.open(filepath, "a");
	if csvFile then
		assert(csvFile);
		csvFile:write(content);
		csvFile:flush();
		csvFile:close();
	else
		logger:info("can not write file:"..filepath);
		logger:info("error msg is:"..tostring(msg))
	end

	collectgarbage("collect")
end

function _StatisticsLog:CombineCSVHead()
	-- body
	local csv_header_1 = ""..",".."Intelligent"..",".."SoftWare Version: "..""..","..""..",".."V1.0_20180203_M0".."\n";		--first Line
	local csv_header_2 = "SerialNumber,Config,Station ID,Site ID,Test PASS/FAIL STATUS,Error Message,List of Failing Tests,Test Start Time,Test Stop Time,Total Test Time"..","..table.concat(self.ItemsName,",").."\n";
	local csv_header_3 = "Upper Limit ----->,,,,,,,,,,"..table.concat(self.ItemsUpper,",").."\n";
	local csv_header_4 = "Lower Limit ----->,,,,,,,,,,"..table.concat(self.ItemsLower,",").."\n";
	local csv_header_5 = "Measurement Unit ----->,,,,,,,,,,"..table.concat(self.ItemsUnit,",").."\n";

	local head = csv_header_1..csv_header_2..csv_header_3..csv_header_4..csv_header_5;
	return head;
end

function _StatisticsLog:ResolveMessage(MsgTime,MsgContent)
	if MsgContent.event == DLCommon.MsgType.MSG_FILE_HEAD then
		self.ItemsName  = nil;
		self.ItemsUpper = nil;
		self.ItemsLower = nil;
		self.ItemsUnit  = nil;
		self.ItemsName  = {};
		self.ItemsUpper = {};
		self.ItemsLower = {};
		self.ItemsUnit  = {};
		local tableOfHeader = json.decode(MsgContent.data);
		if #self.ItemsUnit == 0 then
			for i=1,tableOfHeader[1][3] do
        		local str2 = string.gsub(tableOfHeader[i+1][2],"'","\"");
        		local tableInHeader = json.decode(str2);  --print(tableInHead)
        		self.ItemsName[#self.ItemsName+1] = tableInHeader.GROUP.."_"..tableInHeader.TID;
        		if tableInHeader.HIGH == "" then
        		    self.ItemsUpper[#self.ItemsUpper+1] = "N/A";
        		else
        		    self.ItemsUpper[#self.ItemsUpper+1] = tableInHeader.HIGH;
        		end
        		if tableInHeader.LOW == "" then
        		    self.ItemsLower[#self.ItemsLower+1] = "N/A";
        		else
        		    self.ItemsLower[#self.ItemsLower+1] = tableInHeader.LOW;
        		end
        		if tableInHeader.UNIT == "" then
        		    self.ItemsUnit[#self.ItemsUnit+1] = "N/A";
        		else
        		    self.ItemsUnit[#self.ItemsUnit+1] = tableInHeader.UNIT;
        		end
    		end
    	end

    	self.Name = "TestManager_Profile_Module"..tostring(args.module).."_Slot"..tostring(args.uut).."_"..tostring(DLContext.ScriptName);
    	self.Name = DLCommon:ParseFileOrFolderName(self.Name);
		return;

	elseif MsgContent.event == DLCommon.MsgType.MSG_SEQUENCE_START then
		self.ItemsErrMsg   = "";
		self.ItemsFailList = nil;
		self.ItemsFailList = {};
		self.ItemsValue    = nil;
		self.ItemsValue    = {};
		return;

	elseif MsgContent.event == DLCommon.MsgType.MSG_ITEM_START then

		return;

	elseif MsgContent.event == DLCommon.MsgType.MSG_ITEM_END then
		if MsgContent.data.result == -1 then
			self.ItemsErrMsg = string.gsub(MsgContent.data.error,",",";");
			self.ItemsErrMsg = string.gsub(self.ItemsErrMsg,"\r","\t");
			self.ItemsErrMsg = string.gsub(self.ItemsErrMsg,"\n","\t");
			self.ItemsFailList[#self.ItemsFailList+1] =  MsgContent.data.tid;
		elseif MsgContent.data.result == false then
			self.ItemsFailList[#self.ItemsFailList+1] =  MsgContent.data.tid;
		end

		local DataValue = MsgContent.data.value;
		if DataValue then
			DataValue = string.gsub(DataValue,",",";");
			DataValue = string.gsub(DataValue,"\r","\t");
			DataValue = string.gsub(DataValue,"\n","\t");
			self.ItemsValue[#self.ItemsValue+1] = DataValue;
		else
		    self.ItemsValue[#self.ItemsValue+1] = "This item value is nil";
		end
		return;

	elseif MsgContent.event == DLCommon.MsgType.MSG_SEQUENCE_END then
		local MLBConfig = "";
		local SNLen = #DLContext.MLBSN;
		if SNLen > 0 then
			MLBConfig = string.sub(DLContext.MLBSN,SNLen-1,SNLen-1);
		end
		local CsvContent = DLContext.MLBSN..","..MLBConfig..",,,"..DLContext.TestResult..","..self.ItemsErrMsg..","..table.concat(self.ItemsFailList,";")..","..DLContext.StartTime..","..DLContext.StopTime..","..DLContext.TotalTime..","..table.concat(self.ItemsValue,",").."\n";		
		if self:isLogExisted(DLContext.TestDate) then			
			self:WriteLogContent(self.FullPath,CsvContent);
		else			
			local CsvHeader = self:CombineCSVHead();					
			self:WriteLogContent(self.FullPath,CsvHeader..CsvContent);			
		end	

		if not DLContext.isPubResult then	
			if MsgContent.data.result == 1 then
				LogPub:pubMsg("SequencerResult",{result=0,errStr=""});
			else
				LogPub:pubMsg("SequencerResult",{result=1,errStr=table.concat(self.ItemsFailList,";")});
			end
		end

		return;

	end
end

return _StatisticsLog;