local LOGGER_RS = {};

io.stdout:setvbuf("no")
collectgarbage("setpause",100);
collectgarbage("setstepmul",500);

require("pathManager")
local zmq = require "lzmq";
local zpoller = require "lzmq.poller";
local lthread = require("llthreads2")
local poller = zpoller.new(1);
local context = zmq.context()
local json = require('dkjson');
local lapp = require("pl.lapp")
local content_MLBSN = "";

local args = lapp [[
Data Logger
    -f,--file       (default TesterConfig/zmqports.json)   The path of zmqports
    -u,--uut        (number default 0)                     UUT slot number (used for IP/Port selection)
    -e,--echo       (default 1)                            Echo receive and result in terminal
    -m,--module     (number default 0)                     The number of fixture
    -s,--slots      (number default 1)                     Slots of the fixture
    <updates...>    (default "X=Y")                        Series of X=Y pairs to update the CONFIG table with
]]

local sendPivot_log = "C:\\vault\\Intelli_log\\sendPivot_Log_Module"..tostring(args.module).."_Slot"..tostring(args.uut)..os.date("_%Y-%m-%d")..".txt";
local sendPivot_fp = io.open(sendPivot_log,"a+");
if not sendPivot_fp then
	error("open send Pivot file handle fail,please check");
end

function printer(...)
    -- body
    -- print(...);
    local logStr = "";
    for i,v in ipairs({...}) do
        -- print(i,v)
        logStr = logStr..tostring(v).." ";
    end
    
    local new_sendPivot_log = "C:\\vault\\Intelli_log\\sendPivot_Log_Module"..tostring(args.module).."_Slot"..tostring(args.uut)..os.date("_%Y-%m-%d")..".txt";
    if sendPivot_fp then
    	if new_sendPivot_log ~= sendPivot_log then
    		sendPivot_log = new_sendPivot_log;
    		sendPivot_fp:flush();
    		sendPivot_fp:close();
    		sendPivot_fp = nil;
    		sendPivot_fp = io.open(new_sendPivot_log,"a+");
    	end
    else
    	sendPivot_fp = nil;
    	sendPivot_fp = io.open(new_sendPivot_log,"a+");
	end

	if sendPivot_fp then
		sendPivot_fp:write(os.date("%Y-%m-%d_%H:%M:%S_")..tostring(content_MLBSN).." : "..tostring(logStr).."\r\n");
    	sendPivot_fp:flush();
    end
end


local __LOGGER_VERSION = "1.0.0 updated on 2017-08-31"
--printer("*************************************************************************")
--printer("           Load Logger version is " .. __LOGGER_VERSION)
--printer("           ZMQ version is %d.%d.%d", zmq.version(true))
--printer("*************************************************************************")


local MSG_SEQUENCE_START = "SEQUENCE_START";
local MSG_SEQUENCE_END = "SEQUENCE_END";
local MSG_ITEM_START = "ITEM_START";
local MSG_ITEM_END = "ITEM_END";
local MSG_ATIRIBUTE_FOUND = "ATIRIBUTE_FOUND"
local MSG_REPORT_ERROR = "REPORT_ERROR"
local MSG_UOP_DETECT = "UOP_DETECT"
local MSG_FILE_HEAD = "FILE_HEAD"

local MSG_TYPE_ENABLE = {
	[MSG_SEQUENCE_START] = true,
	[MSG_SEQUENCE_END] = true,
	[MSG_ITEM_START] = true,
	[MSG_ITEM_END] = true,
	[MSG_ATIRIBUTE_FOUND] = true,
	[MSG_REPORT_ERROR] = true,
	[MSG_UOP_DETECT] = true,
	[MSG_FILE_HEAD]  = true
};

local MessageStructure = {
	Msg_Type = "",
	Msg_Content = "",
	--Add other parameter of data item;
}


local ConfigFolder = "C:\\vault\\Intelli_Config"
local DBDataFolder = "C:\\dbdata\\"

-- if not path.isdir(ConfigFolder) then
	MakeNFolder(ConfigFolder);
-- end

-- if not path.isdir(DBDataFolder) then
	MakeNFolder(DBDataFolder);
-- end

local current_slot = args.module * args.slots + args.uut;

LOGGER_RS.IP = "127.0.0.1";
LOGGER_RS.PORT = 6250 + current_slot;
LOGGER_RS.SLEEP = 50;
LOGGER_RS.LOCAL_PUB_PORT = 9595 + current_slot;

local sequence_address = "tcp://127.0.0.1:"..tostring(6250 + current_slot)
local logger_pub_address = "tcp://127.0.0.1:"..tostring(6900 + current_slot)

local log_path = "C:/vault/Intelli_log/";
local pivot_csv_path = log_path.."Pivot_Log_Module"..tostring(args.module).."_Slot"..tostring(args.uut)..os.date("_%Y-%m-%d");

MakeNFolder(string.gsub(pivot_csv_path,"/","\\"));
--local log_path = "/Users/mac/Desktop/logger/Zlog/"
local script_Name = "";
local log_file_type = ".csv";
local log_file_full_path = "";

local content_seq_name = "";
local content_SOCKETSN = "";
local content_ModuleSN = "";
local seq_starttime = "";
local startdate = "";
local seq_stoptime = "";
local msg_error = "";
local mtcp_result = "";
-- local msg_test_status = "";

local isPubResult = false;

local items_names = {};
local items_upper = {};
local items_lower = {};
local items_units = {};
local items_value = {};
local items_listOfFail = {};

local CurrDir = JoinPath(CurrentDir(),"DataLogger");
--printer("CurrDir: %s",CurrDir)

local libDir  = string.gsub(CurrDir,"DataLogger","Driver\\lib\\?.dll")
local luaDir1 = string.gsub(CurrDir,"DataLogger","?.lua")
local luaDir2 = string.gsub(CurrDir,"DataLogger","TestEngine\\?.lua")
local luaDir3 = string.gsub(CurrDir,"DataLogger","Driver\\?.lua")
local Debug_Flag_Path = "C:\\vault\\Intelli_Config\\Debug_Flag.json";
package.path = package.path..";"..luaDir1..";"..luaDir2..";"..luaDir3
package.cpath = package.cpath..";"..libDir

local jsonFilePath = CurrDir;
jsonFilePath = string.gsub(jsonFilePath,"DataLogger","TesterConfig")


local function readFile(path)
	-- body
	local fp = io.open(path,"r");
	if fp == nil then
		--printer("< readFile > : read file fail");
		return nil;
	end
	local Data = fp:read("*all");
	fp:close();

	return Data;
end

function split(str, delimiter)  
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

--Only can be used to calculated not larger than one day delta time;
function getDeltaTime(time1,time2)
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

	local timeArray1 = split(realtime1,":");
	local timeArray2 = split(realtime2,":");
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

--demoTime1 = "10-24_15:13:49.601000";
--demoTime2 = "10-25_14:13:50.91000";

--创建Csv文件头
function CombineCSVHead()
	-- body
	local csv_header_1 = ""..",".."Intelligent"..",".."SoftWare Version: "..""..","..""..",".."V0.1_20161022_M0".."\n";		--first Line
	local csv_header_2 = "SerialNumber,Config,Station ID,Site ID,Test PASS/FAIL STATUS,Error Message,List of Failing Tests,Test Start Time,Test Stop Time,Total Test Time"..","..table.concat(items_names,",").."\n";
	local csv_header_3 = "Upper Limit ----->,,,,,,,,,,"..table.concat(items_upper,",").."\n";
	local csv_header_4 = "Lower Limit ----->,,,,,,,,,,"..table.concat(items_lower,",").."\n";
	local csv_header_5 = "Measurement Unit ----->,,,,,,,,,,"..table.concat(items_units,",").."\n";

	local head = csv_header_1..csv_header_2..csv_header_3..csv_header_4..csv_header_5;
	-- items_names = {};
	-- items_upper = {};
	-- items_lower = {};
	-- items_units = {};
	return head;

end

function isLogExisted(scriptName,dateTime)
	-- --printer("scriptName : ",scriptName)
	local realName = string.match(tostring(scriptName),".+[/\\](.+)%.") or "Test_Script";
	local fullLogName = "TestManager_Profile_"..realName.."_Module"..tostring(args.module).."_Slot"..tostring(args.uut).."_"..dateTime..log_file_type;
	fullLogName = string.gsub(fullLogName, "[%*<>/|?:\"\\]*", "")
	log_file_full_path = log_path..fullLogName;
	--log_file_full_path = realName.."_"..dateTime..log_file_type;
	local fileFlag = io.open(log_file_full_path, "r");
	if	fileFlag then
		fileFlag:close();
		return true;
	else
		return false;
	end
end


function WriteLogContent(filepath,content)
	--Appended the content at the end of the file	
	local csvFile,msg= io.open(filepath, "a");
	if csvFile then
		assert(csvFile);
		csvFile:write(content);
		csvFile:flush();
		time_utils.delay(50);
		csvFile:close();
	else
		--printer("can not write file:"..filepath);
		--printer("error msg is:"..tostring(msg))
	end

	collectgarbage("collect")
end

function ClearMessageStructure()
	MessageStructure.Msg_Type = nil;
	MessageStructure.Msg_Content = nil;
end

function ClearContentMsg()
	-- body
	content_MLBSN = "";
	content_SOCKETSN = "";
	content_ModuleSN = "";

	msg_error = "";

	items_value = nil;
	items_value = {};
	items_listOfFail = nil;
	items_listOfFail = {};

	isPubResult = false;
end

local pivot_file_name = ""
local pivot_item = {0,1,2,3,4,5,6,7,8,9}
local pivot_index = 0
local pivot_time = ""
local pivot_data = {}

local function writeStringFile(dir,filename, content)
	dir = string.gsub(dir,"/","\\")
	
	local filePath=tostring(dir).."\\"..filename
	--printer("filePath:"..filePath)
	local file,msg= io.open(filePath,"w+")
	if file then
		file:write(tostring(content))
		file:flush()
		time_utils.delay(20);
		file:close()
		--printer("write povit csv successful\n")
	else
		--printer("can not write file:"..filePath);
		--printer("error msg is:"..tostring(msg))
	end
end


local function fileIsEmpty(filePath)
	local file = io.open(filePath,"rb")
    if file then
        local len = assert(assert(file):seek("end"))
        file:close()
        if len == 0 then
           return true
        else
           return false
        end

 	end
 	return true
end

-----------------------------------------------

pivot_data[1] = "index,group,groupinfo,item,starttime,endtime,testtime,low,high,unit,value,result"
local description = ""
local function ResolveJsonMsgString(message)
	-- body
	----printer("Get Message:",message);	
	ClearMessageStructure();	
	
	local msg = tostring(message);
	--Extract the json character
	local strOfJson =string.match(msg,"%{.+%}");
	
	if(strOfJson == nil) then
		----printer("The current error message is :"..msg);
		return;
	else
		----printer("Get Message:",strOfJson);
		--decode string to table of json
		tableOfKeyValue=nil;
		tableOfKeyValue = json.decode(strOfJson);
		if(tableOfKeyValue == nil) then
			local tmp = string.gsub(strOfJson,"'",'"')
			local tbl = json.decode(tmp)
			if tbl and tbl.DESCRIPTION then
				description = tbl.DESCRIPTION
			end
			return;
		end
	end

	----printer("json Message:",strOfJson);	
	if	(tableOfKeyValue.event == 0) then
		ClearContentMsg();
		MessageStructure.Msg_Type = MSG_SEQUENCE_START;
		--printer("The current message type is :"..MSG_SEQUENCE_START);
		pivot_index = 0
		local time = string.gsub(tostring(pivot_time),":","-")
		time = string.gsub(tostring(time),"%.","-")
		pivot_file_name = "Module"..tostring(args.module).."_Slot"..tostring(args.uut).."_MLBSN_".."SOCKETID_"..time.."_pivot"

		pivot_data = nil;
		pivot_data = {}
		description = ""
		pivot_data[1] = "index,group,groupinfo,item,starttime,endtime,testtime,low,high,unit,value,result"
		return;

	elseif	(tableOfKeyValue.event == 1) then
		--printer("test finish event 1")
		MessageStructure.Msg_Type = MSG_SEQUENCE_END;
		--printer("The current message type is :"..MSG_SEQUENCE_END);

		pivot_index = 0
		pivot_file_name = string.gsub(pivot_file_name,"MLBSN",tostring(content_MLBSN))

		if tostring(content_SOCKETSN) == "" then
			pivot_file_name = string.gsub(pivot_file_name,"SOCKETID_","");
		else
			pivot_file_name = string.gsub(pivot_file_name,"SOCKETID_",tostring(content_SOCKETSN).."_");
		end


		pivot_file_name = string.gsub(pivot_file_name,"[%*<>/|?:\"\\]*","");
		writeStringFile(pivot_csv_path, pivot_file_name..".csv", table.concat(pivot_data, "\n"))

		return;

	elseif	(tableOfKeyValue.event == 2) then   --
		MessageStructure.Msg_Type = MSG_ITEM_START;
		-- --printer("The current message type is :"..MSG_ITEM_START);
		pivot_index = pivot_index + 1
		pivot_item[1] = tostring(pivot_index)
		pivot_item[2] = tostring(tableOfKeyValue.data.group)
		pivot_item[3] = tostring(description)
		pivot_item[4] = tostring(tableOfKeyValue.data.tid)
		pivot_item[5] = tostring(pivot_time)
		pivot_item[6] = tostring(pivot_time)
		pivot_item[7] = ""
		pivot_item[8] = tostring(tableOfKeyValue.data.low)
		pivot_item[9] = tostring(tableOfKeyValue.data.high)
		pivot_item[10] = tostring(tableOfKeyValue.data.unit)
		pivot_item[11] = ""
		pivot_item[12] = ""
		return;

	elseif	(tableOfKeyValue.event == 3) then
		MessageStructure.Msg_Type = MSG_ITEM_END;
		-- --printer("The current message type is :"..MSG_ITEM_END); 
		pivot_item[6] = tostring(pivot_time)
		local st = os.date("%Y-")..pivot_item[5]
		local et = os.date("%Y-")..pivot_time
		----printer(st,et)
		local x = getDeltaTime(st,et)
		pivot_item[7] = tostring(x)
		pivot_item[11] = string.gsub(tostring(tableOfKeyValue.data.value),",",";")
		if tableOfKeyValue.data.result == true or tableOfKeyValue.data.result == 1 then
			pivot_item[12] = "PASS"
		else
			pivot_item[12] = "FAIL"
		end
		pivot_data[#pivot_data+1] = table.concat(pivot_item, ",")

		if tostring(tableOfKeyValue.data.tid) == "#ROSALINE_MODULE_SN" then
			content_ModuleSN = tostring(tableOfKeyValue.data.value);
		end

		return;

	elseif	(tableOfKeyValue.event == 4) then
		MessageStructure.Msg_Type = MSG_ATIRIBUTE_FOUND;
		--printer("The current message type is :"..MSG_ATIRIBUTE_FOUND); 
		return;

	elseif	(tableOfKeyValue.event == 5) then
		MessageStructure.Msg_Type = MSG_REPORT_ERROR;
		--printer("The current message type is :"..MSG_REPORT_ERROR);
		pivot_item[12] = "ERROR"
		return;

	elseif	(tableOfKeyValue.event == 6) then
		MessageStructure.Msg_Type = MSG_UOP_DETECT;
		--printer("The current message type is :"..MSG_UOP_DETECT);
		return;

	elseif	(tableOfKeyValue.event == 9) then
		MessageStructure.Msg_Type = MSG_FILE_HEAD;
		--printer("The current message type is :"..MSG_FILE_HEAD);
		return;
	else
		return;
	end
end

function LOGGER_RS.GetZLoggerConfigInfo()
	-- CreatReqToSm()
	-- --printer("CreatReqToSm() Line:334")
	-- CreatReqToSm()

  	return LOGGER_RS.IP..":"..LOGGER_RS.PORT..":"..LOGGER_RS.LOCAL_PUB_PORT..":"..LOGGER_RS.SLEEP;
end

function LOGGER_RS.init()
  --printer("Running the init function from Logger.lua");
end

local function ResolveMessage(message)
	local msg = message;
	if msg == nil then
		return ;
	end
	local separator="!@#";
	local sequncerMsg=tostring(msg[1])..separator..tostring(msg[2])..separator..tostring(msg[3])..separator..tostring(msg[4])..separator..tostring(msg[5]);
	local msgArray = split(sequncerMsg,"!@#");
	if	msgArray == nil or #msgArray ~=5 then
		return ;
	end
	local msgTime = msgArray[2];
	local msgData = msgArray[5];

	pivot_time = msgTime

	ResolveJsonMsgString(msgData);

	local msgType = MessageStructure.Msg_Type;
	if MSG_TYPE_ENABLE[msgType] ~= true then
		----printer("Current Msg not enabled or not in the list!");
		return;
	end


	if	msgType == MSG_FILE_HEAD then
		-- --printer("Resolved MSG_FILE_HEAD");
		items_names = nil;
		items_upper = nil;
		items_lower = nil;
		items_units = nil;
		items_names = {};
		items_upper = {};
		items_lower = {};
		items_units = {};
		script_Name = tableOfKeyValue.file;

		local tableOfHead = json.decode(tableOfKeyValue.data);
		if #items_units == 0 then
			for i=1,tableOfHead[1][3] do
        		local str2 = string.gsub(tableOfHead[i+1][2],"'","\"");
        		local tableInHead = json.decode(str2);  ----printer(tableInHead)
        		items_names[#items_names+1] = tableInHead.GROUP.."_"..tableInHead.TID;
        		if(tableInHead.HIGH == "") then
        		    items_upper[#items_upper+1] = "N/A";
        		else
        		    items_upper[#items_upper+1] = tableInHead.HIGH;
        		end
        		if(tableInHead.LOW == "") then
        		    items_lower[#items_lower+1] = "N/A";
        		else
        		    items_lower[#items_lower+1] = tableInHead.LOW;
        		end
        		if(tableInHead.UNIT == "") then
        		    items_units[#items_units+1] = "N/A";
        		else
        		    items_units[#items_units+1] = tableInHead.UNIT;
        		end
    		end
    	end
    	return;

	elseif	msgType == MSG_SEQUENCE_START then	
		-- --printer("Resolved MSG_SEQUENCE_START");  	
		content_seq_name = tableOfKeyValue.data.name;
		seq_starttime = os.date("%Y").."-"..msgTime;
		-- local dataArray = split(seq_starttime,":");
		local dataArray = split(seq_starttime,"_");
		startdate = dataArray[1];  
		return;

	elseif	msgType == MSG_SEQUENCE_END then
		-- --printer("DEBUG_INFO_6\n");

		local nowTime = os.time();
		local filename =  content_MLBSN .. os.date("%Y_%m_%d%H_%M_%S",nowTime) .. ".csv"
		local content = content_MLBSN .. "," .. os.date("%Y/%m/%d %H:%M:%S",nowTime) .. "," .. msg_test_status .. "," .. tostring(content_ModuleSN)
		
		local file,msg = io.open(DBDataFolder..filename,"w")
		if file then
			file:write(tostring(content));
			file:flush();
			file:close();
			--printer("write dbdata csv successful\n");
		else
			--printer("can not write file: "..DBDataFolder..filename);
			--printer("error msg is:"..tostring(msg));
		end

		seq_stoptime = os.date("%Y").."-"..msgTime;  ----printer("----------------------------------------->",tonumber(stoptime));			
		totaltime = getDeltaTime(seq_starttime,seq_stoptime);	

		-- --printer("DEBUG_INFO_8\n");	
		local DUT_Config = string.sub(content_MLBSN,#content_MLBSN-1,#content_MLBSN-1);
		local csv_content = content_MLBSN..","..DUT_Config..",,,"..msg_test_status..","..msg_error..","..table.concat(items_listOfFail,";")..","..seq_starttime..","..seq_stoptime..","..totaltime..","..table.concat(items_value,",").."\n";		
		-- --printer("DEBUG_INFO_9\n");
		if	isLogExisted(script_Name,startdate) then			
			WriteLogContent(log_file_full_path,csv_content);
			-- --printer("DEBUG_INFO_10\n");
		else			
			local csv_header = CombineCSVHead();					
			WriteLogContent(log_file_full_path,csv_header..csv_content);	
			-- --printer("DEBUG_INFO_11\n");		
		end		

		ClearContentMsg();
		return;

	elseif	msgType == MSG_ITEM_START then

		return;

	elseif	msgType == MSG_ITEM_END then
		-- --printer("Resolved MSG_ITEM_END");	
		if(tableOfKeyValue.data.result == -1) then
			msg_error = string.gsub(tableOfKeyValue.data.error,",",";");
			msg_error = string.gsub(msg_error,"\r","\t");
			msg_error = string.gsub(msg_error,"\n","\t");
			--msg_error_name = tableOfKeyValue.data.tid;	
			items_listOfFail[#items_listOfFail+1] =  tableOfKeyValue.data.tid;
		elseif tableOfKeyValue.data.result == false then
			--msg_error_name = tableOfKeyValue.data.tid;
			items_listOfFail[#items_listOfFail+1] =  tableOfKeyValue.data.tid;
		else
			--msg_error_name = "";
			--items_listOfFail[#items_listOfFail+1] =  "";
		end
		local datavalue = tableOfKeyValue.data.value;
		if datavalue then
			datavalue = string.gsub(datavalue,",",";");
			datavalue = string.gsub(datavalue,"\r","\t");
			datavalue = string.gsub(datavalue,"\n","\t");
			--datavalue = string.gsub(datavalue,"\'",";");
			--datavalue = string.gsub(datavalue,"\"",";");
			items_value[#items_value+1] = datavalue;
		else
		   items_value[#items_value+1] = "this item_value is nil";
		-- 	items_value[#items_value+1] = "";
		end
		-- local datavalue = string.gsub(tableOfKeyValue.data.value,",",";");
		-- datavalue = ","..datavalue;
		-- content_itemend = content_itemend..datavalue;
		return;

	elseif	msgType == MSG_ATIRIBUTE_FOUND then
		-- --printer("Resolved MSG_ATIRIBUTE_FOUND");   			
		if(tableOfKeyValue.data.name == "MLBSN") then
        	content_MLBSN = tableOfKeyValue.data.value;
        elseif tableOfKeyValue.data.name == "SocketSN" then
        	content_SOCKETSN = tableOfKeyValue.data.value;
		end
		return;

	elseif	msgType == MSG_REPORT_ERROR then
		-- --printer("Resolved MSG_REPORT_ERROR");		
		return;
		
	elseif	msgType == MSG_UOP_DETECT then
		-- --printer("Resolved MSG_UOP_DETECT");
		return;
	else 
		return;
	end
end

local functionStr = 
[[
	package.cpath = package.cpath..";".."..\\lua\\lib\\lua\\?.dll"
	local address = "DL_HEARTBEAT_ADDRESS" 
    local zmq = require "lzmq"
    local context = zmq.context()
    local  t = require "lzmq.timer"

    print(" < DataLogger > Set HeartBeat PUB : "..tostring(address))
    local watchdog_zmq, err = context:socket(zmq.PUB, {bind = address})
    zmq.assert(watchdog_zmq, err)

    while(1) do
        t.sleep(5000)
        watchdog_zmq:send_all{"101",os.date("%m-%d %H:%M:%S",os.time()),3,"DataLogger","FCT_HEARTBEAT"}
    end
]]


local datalogUrl=string.format("tcp://%s:%d",LOGGER_RS.IP,LOGGER_RS.LOCAL_PUB_PORT);
-- --printer("datalogUrl:",datalogUrl);
functionStr = string.gsub(functionStr,"DL_HEARTBEAT_ADDRESS",datalogUrl);
local DLHeartBeat = lthread.new(functionStr);
DLHeartBeat:start(true);

local function initSequncerSub()
	--printer("< Set sequence sub > : ".. sequence_address);
	sequence_sub, err = context:socket{	zmq.SUB, connect = sequence_address, subscribe = ""};
	zmq.assert(sequence_sub, err);
end

local function initLoggerPub()
	--printer("< logger Set PUB > : ".. logger_pub_address);
	-- logger_pub, err = context:socket( zmq.PUB, {bind=logger_pub_address})
	-- zmq.assert(logger_pub, err);
end

local function addPoller()
	-- 接收 sequence 消息
	poller:add(sequence_sub, zmq.POLLIN, function()
		local msg = sequence_sub:recv_all();
		ResolveMessage(msg);
	end)
	
end

initSequncerSub();
initLoggerPub();
addPoller();
poller:start()


return LOGGER_RS;