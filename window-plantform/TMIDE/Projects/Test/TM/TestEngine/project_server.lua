local zpoller = require("lzmq.poller")
local zmq = require("lzmq")
local json = require("dkjson")

local dispatch = require("dispatch")
require("rpc_protocol")

local myServer = {}

local function CreateMotionReportMsg(cmd,errCode, errMsg)
  local t = {method = cmd, errorCode = errCode, errorMsg = errMsg};
  local msg = json.encode(t)
  return msg
end
--===========================--
--resp myServer
--===========================--
function myServer:RegisterFixtureRepHandler(poller, context, publish)
    local protocol = JSONRPCProtocol()
    local FIXTURE_REP_ADDR = zmqConf.get_addr("FIXTURE_CTRL_REQ_ADDR", "FIXTURE_CTRL_PORT", args.module, 1, 0)
    local fixtureRep, err = context:socket(zmq.REP, {bind = FIXTURE_REP_ADDR})
    zassert(fixtureRep, err)
    logger:info("Create FIXTURE CONTROL ZMQ RESP at:%s sucessful.", FIXTURE_REP_ADDR)

    poller:add(fixtureRep, zmq.POLLIN, function() 
        local msg = fixtureRep:recv()
        if args.echo ~=0 then logger:info(" < Fixture Control Received > %s", tostring(msg)) end
        publish.send("<  Fixture Control Received > \t"..tostring(msg))

        local fixtureReq = {};
        fixtureReq["method"] = "MCO.FixtureControl";
        fixtureReq["param1"] = tostring(msg);

        local sendToFixtureMsg = nil
        local dispatch_result, result_code,result_value = pcall(dispatch.dispatch_function, fixtureReq)
        if dispatch_result then
            sendToFixtureMsg = CreateMotionReportMsg(tostring(msg),result_code,result_value);
        else
            sendToFixtureMsg = "deal control fixture cmd \""..tostring(msg).."\" fail";
        end

        if args.echo ~=0 then logger:info(" < Fixture Control Result > %s", sendToFixtureMsg) end
        publish.send("< Fixture Control Result > \t"..sendToFixtureMsg)
        fixtureRep:send(sendToFixtureMsg)

    end)
end

-- Process messages from both sockets

return myServer
