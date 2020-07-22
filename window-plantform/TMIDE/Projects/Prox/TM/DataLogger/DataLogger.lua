#!/usr/bin/env lua

io.stdout:setvbuf("no")
collectgarbage("setpause",100);
collectgarbage("setstepmul",500);

local json = require("dkjson")
local zmq = require("lzmq")
local zpoller = require("lzmq.poller")
local zthreads = require("lzmq.threads")
require("logging.console")
require("lfs")
require("pathManager")
require("utils.zhelpers")
local time_utils = require("utils.time_utils")
local lapp = require("pl.lapp")
local path = require("pl.path")
local loggerpipe = require("utils.EnginePub")
local config_utils = require("utils.config_utils")


package.path = package.path..";"..JoinPath(JoinPath(CurrentDir(), "DataLogger"), "?.lua")

logger = logging.console()
logger:setLevel(logging.INFO)


function declare (name, initval)
	rawset(_G, name, initval)
end


-- setmetatable(_G, {
--     __newindex = function (_, n)
--         error("attempt to define a global variable "..tostring(n), 2)
--     end,
-- })

local __LOGGER_VERSION = "1.0.0 updated on 2017-08-31"
logger:info("*************************************************************************")
logger:info("           Load Logger version is " .. __LOGGER_VERSION)
logger:info("           ZMQ version is %d.%d.%d", zmq.version(true))
logger:info("*************************************************************************")

declare("args", {})
args = require("ParseArgs")



declare("zmqConf", {})
zmqConf = require("utils.ZmqConf")
zmqConf.load_zmq_port(args.file)

local DATALOGGER_PUB_ADDR = zmqConf.get_addr("DATALOGGER_PUB_ADDR", "LOGGER_PUB", args.module, args.slots, args.uut)


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

-- declare("cal", {})
-- cal = require("start_up")


-- local device_list = require("devices")
-- assert(type(device_list) == "table", "device_list is not a table!!")

-- local device_modules = require("devices_operation")

-- device_modules.get_modules_of_device(device_list)
-- device_list = nil
-- device_modules.init_devices()
-- device_modules.device_self_test()

--===========================--
-- Watchdog looper thread
--===========================--
-- local watchdog_thread, pipe = require("logger_hb").HeartBeat(context, DATALOGGER_PUB_ADDR)
-- loggerpipe.pipe = pipe
-- watchdog_thread:start(true, false)

require("LoggerPub"):init(context,DATALOGGER_PUB_ADDR);

--===========================--
-- Start sequence sub thread
--===========================--
local server = require("DataSubServer"):New(context, args)
server:RegisterSeqSubHandler()
server:Run()

