
local DLCommon = require("DLCommon")
local DLContext = require("DLContext")
local _PivotLog = {};
local path = require("pl.path")

_PivotLog.Path = "C:/vault/Intelli_log/Pivot_Log";
_PivotLog.Name = "";
_PivotLog.Content = {};
_PivotLog.Item = {0};

function _PivotLog:SavePivotLog(dir,filename,content)
	dir = string.gsub(dir,"/","\\")
	if not path.isdir(dir) then
		MakeNFolder(dir);
		logger:info("mkdir "..dir.."successful")
		time_utils.delay(20);
	end

	DLContext.PivotLogFullPath = tostring(dir).."\\"..filename
	logger:info("DLContext.PivotLogFullPath :"..DLContext.PivotLogFullPath);
	local file,msg = io.open(DLContext.PivotLogFullPath,"w+")
	if file then
		file:write(tostring(content))
		file:flush()
		file:close()
		logger:info("write povit csv successful\n");
	else
		logger:info("can not write file:"..DLContext.PivotLogFullPath);
		logger:info("error msg is:"..tostring(msg));
	end

	collectgarbage("collect")
end


function _PivotLog:ResolveMessage(MsgTime,MsgContent)
	if MsgContent.event == DLCommon.MsgType.MSG_FILE_HEAD then
		self.Path = "C:/vault/Intelli_log/Pivot_Log_Module"..tostring(args.module).."_Slot"..tostring(args.uut);
		return;
	elseif MsgContent.event == DLCommon.MsgType.MSG_SEQUENCE_START then
		self.Item = nil;
		self.Item = {};
		self.Content = nil;
		self.Content = {};

		self.Item[1] = 0;  --Item index
		self.Content[1] = "index,group,groupinfo,item,starttime,endtime,testtime,low,high,unit,value,result";
		self.Name = "Module"..tostring(args.module).."_Slot"..tostring(args.uut).."_MLBSN_".."SOCKETSN".."_"..os.date("%Y-")..tostring(string.gsub(tostring(MsgTime),"[:%.]+","-")).."_pivot";
		return;

	elseif MsgContent.event == DLCommon.MsgType.MSG_ITEM_START then
		self.Item[1] = self.Item[1] + 1;
		self.Item[2] = tostring(MsgContent.data.group)
		self.Item[3] = tostring(MsgContent.data.description)
		self.Item[4] = tostring(MsgContent.data.tid)
		self.Item[5] = tostring(MsgTime)
		self.Item[6] = tostring(MsgTime)
		self.Item[7] = ""
		self.Item[8] = tostring(MsgContent.data.low)
		self.Item[9] = tostring(MsgContent.data.high)
		self.Item[10] = tostring(MsgContent.data.unit)
		self.Item[11] = ""
		self.Item[12] = ""
		return;

	elseif MsgContent.event == DLCommon.MsgType.MSG_ITEM_END then
		self.Item[6] = tostring(MsgTime)

		local st = os.date("%Y-")..self.Item[5];
		local et = os.date("%Y-")..self.Item[6];
		local x = DLCommon:getDeltaTime(st,et);
		self.Item[7] = tostring(x);
		self.Item[11] = string.gsub(tostring(MsgContent.data.value),",",";");
		if MsgContent.data.result == true or MsgContent.data.result == 1 then
			self.Item[12] = "PASS";
		else
			self.Item[12] = "FAIL";
		end
		self.Content[#self.Content+1] = table.concat(self.Item, ",");
		return;

	elseif MsgContent.event == DLCommon.MsgType.MSG_SEQUENCE_END then
		self.Name = string.gsub(self.Name,"MLBSN",tostring(DLContext.MLBSN));
		self.Name = string.gsub(self.Name,"SOCKETSN",tostring(DLContext.SocketSN));
		self.Name = DLCommon:ParseFileOrFolderName(self.Name);

		self:SavePivotLog(self.Path, self.Name..".csv", table.concat(self.Content, "\n"));
		return;

	end
end

return _PivotLog;