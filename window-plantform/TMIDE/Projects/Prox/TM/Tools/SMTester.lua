
-- ../Lua/Mac/lua Tools/SMTester.lua


local zmq = require("lzmq")
local lapp = require("pl.lapp")
local ztimer  = require "lzmq.timer"
require("logging.console")

require "pathManager"

package.path = package.path .. ";" .. JoinPath(JoinPath(CurrentDir(), "StateMachine"), "?.lua")

local common = require("SMCommon")


local args = lapp [[
    -p,--port     (default 6480)                           Port of sm zmq resp
    <updates...>    (default "X=Y")                    Series of X=Y pairs to update the CONFIG table with
]]

local logger = logging.console()
logger:setLevel(logging.INFO)


local STATEMACHINE_REP_ADDR = "tcp://127.0.0.1"
local addr = STATEMACHINE_REP_ADDR .. ":" .. tostring(args.port)
print("Trigger ZMQ REQ's addr is " .. addr)

local context = zmq.context()
local triger, err = context:socket{zmq.REQ, connect = addr}
zmq.assert(triger, err)


while true do
	help_info = [[You can input messages as follow which are sent to SM: 
start
abort
setsn
uutenable
]]

	print(help_info)
	print("Please input cmd:")

	local mesg = io.read()

	if (string.match(mesg, "start")) then
		triger:send(common.generateReqMsg("start", {times = 1}))

	elseif (string.match(mesg, "abort")) then
		triger:send(common.generateReqMsg("abort"))

	elseif (string.match(mesg, "uutenable")) then
		triger:send(common.generateReqMsg("uutenable", {{uutNum = 0, enable = true}}))

	elseif (string.match(mesg, "setsn")) then
		triger:send(common.generateReqMsg("setsn", {{uutNum = 0, SN = "fea819c0-5111-44c2"}}))

	end

	print(triger:recv())
end