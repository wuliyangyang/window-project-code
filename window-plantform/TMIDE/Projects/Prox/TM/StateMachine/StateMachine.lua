#!/usr/bin/env lua

local zmq = require("lzmq")
local zpoller = require("lzmq.poller")
local zthreads = require("lzmq.threads")
-- local ztimer  = require("lzmq.timer")
local json = require("dkjson")
local lapp = require("pl.lapp")
local path = require("pl.path")
local time_utils = require("utils.time_utils")
local smPipe = require("utils.EnginePub")
local TConfig = require("TestContext.infoHandle")

require("logging.file")
require("utils.zhelpers")
require("pathManager")


io.stdout:setvbuf("no")

local __VERSION__ = "3.1.0 updated on 2017-09-31"
local currPath = CurrentDir()
AddModuleSearchPath(arg[0])

args = require("ParseArgu")

-- load zmq port config
zmqConf = require("utils.ZmqConf")
zmqConf.load_zmq_port(args.file)

TConfig.getTesterConfigInfo(args.module)
local SMLogPath = TConfig.LogPath.. "\\" .. TConfig.LogPrefix .. "_StateMachineLog_%s.txt";
logger = logging.file(SMLogPath,os.date("%Y-%m-%d"));
logger:setLevel(logging.INFO)

function PrintVersion()
	logger:info("*************************************************************************")
	logger:info("         State Machine version is " .. __VERSION__)
	logger:info("         ZMQ version is %d.%d.%d", zmq.version(true))
	logger:info("*************************************************************************")
end

PrintVersion(__VERSION__)

local ctx = zmq.context()
zthreads.set_context(ctx)

-- Start HB thread
local watchdog_thread, pipe = require("HB").HeartBeat(ctx, zmqConf.get_addr("STATEMACHINE_HEARTBEAT_ADDR", "SM_PUB", args.module, 1, 0)) 
smPipe.pipe = pipe
watchdog_thread:start(true, false)


local fixtureSM = require("FixtureSM")
sm = fixtureSM:new()
sm:CreateFixture(ctx, smPipe, args)
sm:InitSM()

project = require("Project")
project.Init(ctx, args)

local server = require("Server"):New(ctx, args)
server:RegisterSocketsHandler()
server:Run()



