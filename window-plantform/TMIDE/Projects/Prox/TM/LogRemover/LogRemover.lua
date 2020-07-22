local lfs = require "lfs"
--------------------------------------[[
-- Mark: This will remove folder or file which is created "duration" days ago
-- duration is the days
-- folder: define the source folder

-- Version   : V1.0
-- Author    : Ivan Gan
-- Date      : 2017/3/21
-- Changlist : First Release

-- Version   : V1.1
-- Author    : Yuekie
-- Date      : 2017/3/21
-- Changlist : + add folder valid check function

-- Version   : V1.3
-- Author    : Yuekie
-- Date      : 2017/3/21
-- Changlist : * check "folder" list every day 00:00

-- Version   : V1.4
-- Author    : Yuekie
-- Date      : 2017/3/22
-- Changlist : x use remove file cmd "DEL" instead of "rm".
--			   + add more log info about file/directory create/modify date.
--			   + check "folder" list one time while start test SW.

-- Version   : V1.5
-- Author    : Yuekie
-- Date      : 2017/3/26
-- Changlist : * user can set check folder list time during all day,not only 00:00
--			   + add more judgement:Traverse sub directory while directory modify date less than check time.

-- Version   : V1.6
-- Author    : Yuekie
-- Date      : 2017/3/27
-- Changlist : * modify folder path from "C:\\vault" to "C:\\vault\\Intelli_Log".

-- Version   : V1.7
-- Author    : Yuekie
-- Date      : 2017/7/9
-- Changlist : - remove print info.

--------------------------------------]]
local ztimer = require "lzmq.timer"
local infoHandle = require "TestContext.infoHandle"
local duration = infoHandle.LogValidDay;
local currDir = lfs.currentdir();
print("current dir:",currDir);
require("pathManager")

local launcherLogPath = JoinPath(deleteLastPathComponent(deleteLastPathComponent(currDir)),"log");

io.stdout:setvbuf("no")

local folder = {
					"C:\\vault\\Intelli_Log",
					launcherLogPath,
			   };
--------------------------------------
function getCurrentUtcTime(time)
	local s = os.date("%Y,%m,%d,")
	if time then
		s = os.date("%Y,%m,%d,",time)
	end
	local y,m,d = string.match(s,"(%d+),(%d+),(%d+),")
	return os.time({year=tonumber(y),month=tonumber(m),day=tonumber(d),hour=0,minute=0,second=0})
end

local day_time = duration * 86400
local check_time = 1 * 86400 + 0 * 3600 + 0 * 60 + 0 --1day 0hour 0min 0second
local last_time = getCurrentUtcTime();
local current_time = os.time();

local function folderCheck(folder)
	-- body
	local folerTable = {};
	for k,v in pairs(folder) do
		-- print(k,v)
		if v == "" or string.find(v,"[%*<>/|?\"]") then
		else
			folerTable[#folerTable+1] = v;
		end
	end

	return folerTable;
end

folder = folderCheck(folder);
for k,v in pairs(folder) do
	print(k,v)
end

function attrdir(path,currTime,dayTime)--return {{path, file?,days}}
	local list = {};
	for file in lfs.dir(path) do
		if file ~= "." and file ~= ".." then
			local f = path.."\\"..file
		--	print("\t"..f,file)
			local attr = lfs.attributes(f)
			if type(attr) == "table" then
				local t = {}
				local delDirFlag = true;
				t[1] = f
				if attr.mode == "directory" then
					t[2] = false
					local Modify_time = getCurrentUtcTime(attr.modification)
					if currTime - Modify_time <= dayTime then
						local list2 = attrdir(f,currTime,dayTime);
						for i,v in ipairs(list2) do
							-- print(i,v)
							list[#list+1] = v;
						end
						delDirFlag = false;
					end
				else
					t[2] = true
				end
				
				if delDirFlag then
					local time = getCurrentUtcTime(attr.change)
					if currTime - time > dayTime then
						t[3] = true
					else
						t[3] = false
					end
					list[#list+1] = t

					-- print("---------------------------->>>>>>>>>>>>>>>>>>>>>>>>>>>>>")
					-- if t[2] == true then 
					-- 	print("file name      : ",t[1])
					-- 	print("create date    : ",os.date("%Y-%m-%d %H:%M:%S",attr.change))
					-- else
					-- 	print("directory name : ",t[1])
					-- 	print("modify date    : ",os.date("%Y-%m-%d %H:%M:%S",attr.modification))
					-- end
					
					-- print("remove flag    : ",t[3])
					-- print("remove upper   : ",duration)
					-- print("<<<<<<<<<<<<<<<<<<<<<<<<<<<<-----------------------------\n")
				end
			end
		end
	end
	return list;
end

function remove(list)
	if type(list) == "table" then
		for i=1, #list do
			local tmp = list[i]
			if tmp[3] == true then --remove
				if tmp[2] == true then --file
					-- print("***********remove file********** ",tmp[1])
					os.execute("DEL ".."\""..tmp[1].."\"")
				else--folder
					-- print("***********remove folder******** ",tmp[1])
					os.execute("RD /Q /S " .."\""..tmp[1].."\"")
				end
			end
		end
	end
end

function checkFolderList()
	-- body
	for i=1, #folder do
		-- print("check folder index,name:",i,folder[i])
		local x = attrdir(folder[i],current_time,day_time);
		remove(x);
	end
end

pcall(checkFolderList);

while 1 do
	-- print("now time:",os.date())
	-- print("logremover heart beat");
	current_time = os.time();
	if current_time - last_time >= check_time then
		current_time = getCurrentUtcTime();
		last_time = current_time;
		pcall(checkFolderList);
	end
	ztimer.sleep(5*1000);--5 seconds
end