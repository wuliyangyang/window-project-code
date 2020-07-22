local _infoHandle = {}
json = require "dkjson"
require "pathManager"

_infoHandle.LogPath = ""
_infoHandle.ProjectName = ""
_infoHandle.RoomName = ""
_infoHandle.LineName = ""
_infoHandle.TesterName = ""
_infoHandle.TesterIndex = 0
_infoHandle.LogPrefix = ""
_infoHandle.LogValidDay = 5

MakeNFolder("C:\\vault\\Intelli_Config");
local global_config_path = "C:\\vault\\Intelli_Config\\TesterConfig.json";

function _infoHandle.getTesterConfigInfo(slot)

	local fp = io.open(global_config_path,"r")
	if(fp) then
		local str = fp:read("*a")
		fp:close()
		print("\t< Get String From TesterConfig.json > :",str);
		local data=json.decode(tostring(str))
		if data and type(data) == "table" then
			_infoHandle.ProjectName = data["ProjectName"] or "";
			_infoHandle.RoomName   = data["RoomName"] or "";
			_infoHandle.LineName   = data["LineName"] or "";
			_infoHandle.TesterName = data["TesterName"] or "";
			_infoHandle.LogPath    = data["LogPath"] or "C:\\vault\\intelli_log";
			_infoHandle.LogValidDay= data["LogValidDay"] or 5;

			 print("\t< ProjectName,RoomName,LineName,TesterName,LogValidDay> :",_infoHandle.ProjectName,_infoHandle.RoomName,_infoHandle.LineName,_infoHandle.TesterName,_infoHandle.LogPath,_infoHandle.LogValidDay);
		end
	end

	_infoHandle.TesterIndex = (string.match(_infoHandle.TesterName,"(%d+)") or 0) + slot;
	_infoHandle.LogPrefix = _infoHandle.ProjectName .. "_" .. _infoHandle.RoomName .. "_" .. _infoHandle.LineName .. "_Prox" .. string.format("%02d",_infoHandle.TesterIndex)
	return _infoHandle.ProjectName,_infoHandle.RoomName,_infoHandle.LineName,_infoHandle.TesterName,_infoHandle.LogPath,_infoHandle.LogValidDay;
end

local function updateTesterConfigInfo()
	local jsonFilePath = JoinPath(JoinPath(CurrentDir(),"TesterConfig"),"TesterConfig.json")
	
	local fp = io.open(jsonFilePath,"r");
	local TesterConfig_data = "";
	if fp then
		TesterConfig_data = fp:read("*a")
		fp:close()
		if TesterConfig_data == nil or #TesterConfig_data == 0 then
			TesterConfig_data = "";
		end
	end

	fp = io.open(global_config_path,"r");
	local config_info_data = "";
	if fp then
		config_info_data = fp:read("*a")
		fp:close()
		if config_info_data == nil or #config_info_data == 0 then
			config_info_data = "";
		end
	end

	TesterConfig_data = json.decode(TesterConfig_data);
	config_info_data = json.decode(config_info_data);

	if TesterConfig_data == nil or type(TesterConfig_data) ~= "table" then
		TesterConfig_data = {};
	end

	if config_info_data == nil or type(config_info_data) ~= "table" then
		config_info_data = {};
	end

	local write_flag = false;
	for k,v in pairs(TesterConfig_data) do
		if config_info_data[k] == nil then
			print("update config data to TesterConfig config file",k,v)
			config_info_data[k] = v;
			write_flag = true;
		end
	end

	if write_flag then 
		local str = json.encode(config_info_data);
		local writefp = io.open(global_config_path,"w");
		if writefp then
			writefp:write(tostring(str));
			writefp:flush();
			writefp:close();
		end
	end
end

updateTesterConfigInfo();

return _infoHandle
