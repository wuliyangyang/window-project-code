local _MyProject = {};

local MtcpClient = require("MtcpRepClient")
local Dbdata = require("SaveDBData");

function _MyProject:ResolveMessage(MsgTime,MsgContent)
	-- body
	MtcpClient:ResolveMessage(MsgTime,MsgContent);
	Dbdata:ResolveMessage(MsgTime,MsgContent);
end

return _MyProject;