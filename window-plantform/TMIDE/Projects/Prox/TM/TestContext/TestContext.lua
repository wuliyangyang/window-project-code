require("pathManager");
local json = require("dkjson");
local _TC = {};

_TC.Data = {};
_TC.Handle = {};
_TC.Module = nil;
_TC.Slot = nil;

_TC.MLBSN     = "MLBSN";
_TC.SocketSN  = "SocketSN";
_TC.LogFolder = "LogFolder";
_TC.StartTime = "StartTime";
_TC.StopTime  = "StopTime";
_TC.TotalTime = "TotalTime";

_TC.IsUpload  = "IsUpload";

_TC.ConfigFolder = "C:\\vault\\Intelli_Config"

MakeNFolder(_TC.ConfigFolder);

function _TC:GetFileHandle(module,slot)
	-- body
	if self.Handle[module] then
		if self.Handle[module][slot] then
			self.Handle[module][slot]:close();
		end
	else
		self.Handle[module] = {};
	end

	if self.Data[module] == nil then
		self.Data[module] = {};
	end

	if self.Data[module][slot] == nil then
		self.Data[module][slot] = {};
	end

	if not self.Module then
		self.Module = module;
	end

	if not self.Slot then
		self.Slot = slot;
	end

	local Path = JoinPath(_TC.ConfigFolder, "TC_Module")..tostring(module).."_SLot"..tostring(slot)..".tc";
	self.Handle[module][slot] = io.open(Path,"w+");
	self.Handle[module][slot]:setvbuf("no");

	return self.Handle[module][slot];
end

function _TC:CloseFileHandle()
	-- body
	for k,v in pairs(self.Handle) do
		for m,n in pairs(v) do
			n:close();
		end
	end
end

function _TC:GetValue(key,Module,Slot)
	-- body
	local slot = Slot or self.Slot;
	local _module = Module or self.Module;
	assert(self.Handle[_module][slot],"Module"..tostring(_module).." Slot"..tostring(slot).." Test Context Handle is nil"):seek("set");
	local tc = string.match(tostring(self.Handle[_module][slot]:read("*a")),"%{.-%}");
	self.Data[_module][slot] = json.decode(tostring(tc));
	if self.Data[_module][slot] == nil or type(self.Data[_module][slot]) ~= "table" then
		self.Data[_module][slot] = {};
	end
		
	return self.Data[_module][slot][key];
end

function _TC:GetValueArr(keyArr,Module,Slot)
	-- body
	local slot = Slot or self.Slot;
	local _module = Module or self.Module;
	assert(self.Handle[_module][slot],"Module"..tostring(_module).." Slot"..tostring(slot).." Test Context Handle is nil"):seek("set");
	local tc = string.match(tostring(self.Handle[_module][slot]:read("*a")),"{.-}");
	self.Data[_module][slot] = json.decode(tostring(tc));
	if self.Data[_module][slot] == nil or type(self.Data[_module][slot]) ~= "table" then
		self.Data[_module][slot] = {};
	end
	
	local RetArr = {};
	for k,v in pairs(keyArr) do
		RetArr[v] = self.Data[_module][slot][v]
	end
	return RetArr;
end

function _TC:SetValue(key,value,Module,Slot)
	-- body
	local slot = Slot or self.Slot;
	local _module = Module or self.Module;
	assert(self.Handle[_module][slot],"Module"..tostring(_module).." Slot"..tostring(slot).." Test Context Handle is nil"):seek("set");
	self.Data[_module][slot][key] = value;
	self.Handle[_module][slot]:write(tostring(json.encode(self.Data[_module][slot])));
end

function _TC:SetValueArr(DataArr,Module,Slot)
	-- body
	local slot = Slot or self.Slot;
	local _module = Module or self.Module;
	assert(self.Handle[_module][slot],"Module"..tostring(_module).." Slot"..tostring(slot).." Test Context Handle is nil"):seek("set");
	for k,v in pairs(DataArr) do
		self.Data[_module][slot][k] = v;
	end
	self.Handle[_module][slot]:write(tostring(json.encode(self.Data[_module][slot])));
end

function _TC:ClearContent(Module,Slot)
	-- body
	local slot = Slot or self.Slot;
	local _module = Module or self.Module;
	assert(self.Handle[_module][slot],"Module"..tostring(_module).." Slot"..tostring(slot).." Test Context Handle is nil"):seek("set");
	self.Data[_module][slot] = nil;
	self.Data[_module][slot] = {};
	self.Handle[_module][slot]:write(tostring(json.encode(self.Data[_module][slot])));
end

function _TC:SetMLBSN(value,Module,Slot)
	-- body
	return self:SetValue(self.MLBSN,value,Module,Slot);
end

function _TC:GetMLBSN(Module,Slot)
	-- body
	return self:GetValue(self.MLBSN,Module,Slot);
end

function _TC:SetSocketSN(value,Module,Slot)
	-- body
	return self:SetValue(self.SocketSN,value,Module,Slot);
end

function _TC:GetSocketSN(Module,Slot)
	-- body
	return self:GetValue(self.SocketSN,Module,Slot);
end

function _TC:SetIsUpload(value,Module,Slot)
	-- body
	return self:SetValue(self.IsUpload,value,Module,Slot);
end

function _TC:GetIsUpload(Module,Slot)
	-- body
	return self:GetValue(self.IsUpload,Module,Slot);
end

return _TC;