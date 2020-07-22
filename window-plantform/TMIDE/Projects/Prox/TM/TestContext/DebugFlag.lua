require("pathManager");
local json = require("dkjson");
local _DF = {};

_DF.Data = {};
_DF.Handle = {};
_DF.Slot = nil;

_DF.SkipMtcp = "SkipMtcp";
_DF.UseMtcp  = "UseMtcp";

_DF.Flags = {
	SkipMtcp = false,
	UseMtcp  = true,
};

_DF.ConfigFolder = "C:\\vault\\Intelli_Config"
MakeNFolder(_DF.ConfigFolder);

function _DF:GetFileHandle(module,slot)
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

	local Path = JoinPath(_DF.ConfigFolder, "DF_Module")..tostring(module).."_SLot"..tostring(slot)..".df";
	self.Handle[module][slot] = io.open(Path,"w+");
	self.Handle[module][slot]:setvbuf("no");
	self.Handle[module][slot]:write(tostring(json.encode(self.Flags)));
	return self.Handle[module][slot];
end

function _DF:CloseFileHandle()
	-- body
	for k,v in pairs(self.Handle) do
		for m,n in pairs(v) do
			n:close();
		end
	end
end

function _DF:GetFlag(key,Module,Slot)
	-- body
	local slot = Slot or self.Slot;
	local _module = Module or self.Module;
	assert(self.Handle[_module][slot],"Module"..tostring(_module).." Slot"..tostring(slot).." Test Context Handle is nil"):seek("set");
	local df = string.match(tostring(self.Handle[_module][slot]:read("*a")),"%{.-%}");
	self.Data[_module][slot] = json.decode(tostring(df));
	if self.Data[_module][slot] == nil or type(self.Data[_module][slot]) ~= "table" then
		self.Data[_module][slot] = {};
	end
		
	return self.Data[_module][slot][key];
end

function _DF:GetFlagArr(keyArr,Module,Slot)
	-- body
	local slot = Slot or self.Slot;
	local _module = Module or self.Module;
	assert(self.Handle[_module][slot],"Test Context Handle is nil"):seek("set");
	local df = string.match(tostring(self.Handle[_module][slot]:read("*a")),"{.-}");
	self.Data[_module][slot] = json.decode(tostring(df));
	if self.Data[_module][slot] == nil or type(self.Data[_module][slot]) ~= "table" then
		self.Data[_module][slot] = {};
	end
	
	local RetArr = {};
	for k,v in pairs(keyArr) do
		RetArr[v] = self.Data[_module][slot][v]
	end
	return RetArr;
end

return _DF;