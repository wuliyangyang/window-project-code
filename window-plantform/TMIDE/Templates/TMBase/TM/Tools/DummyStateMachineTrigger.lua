

local zmq = require("lzmq")
local json = require "dkjson"
local lapp = require("pl.lapp")
local ztimer  = require "lzmq.timer"


local zassert = zmq.assert

local args = lapp [[
State Machine Test
    
    -a,--address    (default tcp://127.0.0.1:6480)   Config SM addr to connect.
    <updates...>    (default "X=Y")                    Series of X=Y pairs to update the CONFIG table with
]]


local JSON_RPC_VERSION = "2.0"

local context = zmq.context()
local triger, err = context:socket(zmq.REQ, {connect = args.address})
zassert(triger, err)



local function CreateStartAMsg()
	local jsonMsg = {}
	jsonMsg["function"] = "start"
	jsonMsg.jsonrpc = JSON_RPC_VERSION
	jsonMsg.id = "9124c999053411e7b6b4acbc32b21e9f"
	jsonMsg.params = {times = 1}

	local msg = json.encode(jsonMsg)
	print("Req msg is:", msg)
	return msg
end

local function CreateStartBMsg()
	local jsonMsg = {}
	jsonMsg["function"] = "start"
	jsonMsg.jsonrpc = JSON_RPC_VERSION
	jsonMsg.id = "9124c999053411e7b6b4acbc32b21e9f"
	jsonMsg.params = {times = 20, uutinfo = {{SN="SNABCD123456",uutNum=0}}}

	local msg = json.encode(jsonMsg)
	print("Req msg is:", msg)
	return msg
end

local function CreateStartCMsg()
	local jsonMsg = {}
	jsonMsg["function"] = "start"
	jsonMsg.jsonrpc = JSON_RPC_VERSION
	jsonMsg.id = "9124c999053411e7b6b4acbc32b21e9f"
	jsonMsg.params = {times = 1, uutinfo = {{SN="SNABCD12345678912300",uutNum=0}, {SN="SNABCD12345678912301",uutNum=1}, {SN="SNABCD12345678912302",uutNum=2},
	{SN="SNABCD12345678912303",uutNum=3}, {SN="SNABCD12345678912304",uutNum=4}, {SN="SNABCD12345678912305",uutNum=5}}}

	local msg = json.encode(jsonMsg)
	print("Req msg is:", msg)
	return msg
end

local function CreateAbortMsg()
	local jsonMsg = {}
	jsonMsg["function"] = "abort"
	jsonMsg.jsonrpc = JSON_RPC_VERSION
	jsonMsg.id = "9124c999053411e7b6b4acbc32b21e9f"
	jsonMsg.params = nil

	local msg = json.encode(jsonMsg)
	print("Req msg is:", msg)
	return msg
end


while true do
	print("Please input cmd which you want to send to SM;Ctrl + c to exit.Cmds are as follow:")
	print("STARTA\tSTARTB\tSTARTC\tABORT")

	local t = io.read()

	if string.match(t, "STARTA") then
		print(triger:send(CreateStartAMsg()))
		print(triger:recv())
	elseif string.match(t, "ABORT") then
		print(triger:send(CreateAbortMsg()))
		print(triger:recv())
	elseif string.match(t, "STARTB") then
		print(triger:send(CreateStartBMsg()))
		print(triger:recv())
	elseif string.match(t, "STARTC") then
		print(triger:send(CreateStartCMsg()))
		print(triger:recv())
	else
		print("invalid cmd")
	end

	
end