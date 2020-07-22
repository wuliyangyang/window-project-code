local _Version = {}
local json = require "dkjson"
require "pathManager"

_Version.swversion = "V1.7.3"

function _Version.GetTMVersion()
	-- body
	return _Version.swversion;
end

function _Version.init()
  logger:info("infoHandle init")
  return true
end

function _Version.self_test()
  logger:info("infoHandle self_test")
  return true
end

function _Version.clear_buffer()
  logger:info("infoHandle clear_buffer")
  return true
end

return _Version;
