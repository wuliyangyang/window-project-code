#!/usr/bin/env lua

io.stdout:setvbuf("no")

local json = require("dkjson")
local zmq = require("lzmq")
local zpoller = require("lzmq.poller")
local zthreads = require("lzmq.threads")
require("logging.file")

require("pathManager")
require("utils.zhelpers")
local time_utils = require("utils.time_utils")
local lapp = require("pl.lapp")
local path = require("pl.path")
local enginepipe = require("utils.EnginePub")
local config_utils = require("utils.config_utils")
local TConfig = require("TestContext.infoHandle")

package.path = package.path..";"..JoinPath(JoinPath(CurrentDir(), "TestEngine"), "?.lua")

function declare (name, initval)
	rawset(_G, name, initval)
end

setmetatable(_G, {
    __newindex = function (_, n)
        error("attempt to define a global variable "..tostring(n), 2)
    end,
})

declare("args", {})
args = require("parse_argu")

declare("zmqConf", {})
zmqConf = require("utils.ZmqConf")
zmqConf.load_zmq_port(args.file)


TConfig.getTesterConfigInfo(args.uut + args.module * args.slots)
local EngineLogPath = TConfig.LogPath.. "\\" .. TConfig.LogPrefix .. "_TestEngineLog_%s.txt";
declare("logger", {})
logger = logging.file(EngineLogPath,os.date("%Y-%m-%d"));
logger:setLevel(logging.INFO)

local __ENGINE_VERSION = "2.1.0 updated on 2017-08-31"
logger:info("*************************************************************************")
logger:info("           Load Engine version is " .. __ENGINE_VERSION)
logger:info("           ZMQ version is %d.%d.%d", zmq.version(true))
logger:info("*************************************************************************")

print = function (...)
	local logStr = "";
	for i,v in ipairs({...}) do
	    -- print(i,v)
	    logStr = logStr.." "..tostring(v);
	end
	logger:info(logStr);
end

local ENGINE_RESP_ADDR = zmqConf.get_addr("TEST_ENGINE_RESP_ADDR", "TEST_ENGINE_PORT", args.module, args.slots, args.uut)
local ENGINE_HB_ADDR = zmqConf.get_addr("TEST_ENGINE_HB_ADDR", "TEST_ENGINE_PUB", args.module, args.slots, args.uut)

local DriverPath = JoinPath(args.dir, "Driver")  
DriverPath = string.gsub(DriverPath,"/","\\")
package.path = package.path..";"..JoinPath(DriverPath, "?.lua")

local functions_path = JoinPath(DriverPath, "functions")
package.path = package.path..";"..JoinPath(functions_path, "?.lua")

local console_path = JoinPath(DriverPath, "console")
package.path = package.path..";"..JoinPath(console_path, "?.lua")

local config_path = JoinPath(DriverPath, "config")
package.path = package.path..";"..JoinPath(config_path, "?.lua")

package.cpath = package.cpath..";"..JoinPath(JoinPath(DriverPath, "lib"), "?.dll")


if arg[-1] then
	AddModuleSearchPath(arg[-1])
end

logger:info(package.path, "\n")
logger:info(package.cpath, "\n")


--===========================--
-- Setup ZMQ sockets
--===========================--
local context = zmq.context()
zthreads.set_context(context)



--===========================--
-- do start up 
--===========================--

declare("cal", {})
cal = require("start_up")


local device_list = require("devices")
assert(type(device_list) == "table", "device_list is not a table!!")

local device_modules = require("devices_operation")

device_modules.get_modules_of_device(device_list)
device_list = nil
device_modules.init_devices()
device_modules.device_self_test()



local tc = require("TestContext.TestContext");
local df = require("TestContext.DebugFlag");
tc:GetFileHandle(args.module,args.uut);
df:GetFileHandle(args.module,args.uut);

--===========================--
-- Watchdog looper thread
--===========================--
local watchdog_thread, pipe = require("engine_hb").HeartBeat(context, ENGINE_HB_ADDR)
enginepipe.pipe = pipe
watchdog_thread:start(true, false)


--===========================--
-- Sequence resp server
--===========================--
local server = require("resp_server"):new(context, ENGINE_RESP_ADDR, enginepipe)
server:run()
