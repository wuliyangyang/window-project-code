

local zpoller = require("lzmq.poller")
local zmq = require("lzmq")
local json = require("dkjson")

local dispatch = require("dispatch")
local pServer = require("project_server")
require("rpc_protocol")

local server = {}



--===========================--
-- Sequence resp server
--===========================--
function server:new(context, addr, publish)
    local protocol = JSONRPCProtocol()
    local poller = zpoller.new(1)
    local obj = {poller = poller}

    local sequence_zmq, err = context:socket(zmq.REP, {bind = addr})
    zassert(sequence_zmq, err)
    logger:info("Create SEQUENCE ZMQ RESP at:%s sucessful.", addr)

    poller:add(sequence_zmq, zmq.POLLIN, function() 
        local msg = sequence_zmq:recv()
        if args.echo ~=0 then logger:info(" < Received > %s", tostring(msg)) end

        publish.send("< Received > \t"..tostring(msg))
        
        if string.match(tostring(msg), '"method":%s*"start_test"') and cal.data then
            publish.send(tostring(cal.data))
        end

        local sendToSeqMsg = nil
        
        local err, msg_obj = pcall(protocol.parse_request, self, msg)
        -- msg = nil

        if not msg_obj then
            sendToSeqMsg = json.encode({jsonrpc = "2.0", error = "request msg's format is error"})
        else
            local dispatch_result, result_value

            if msg_obj["method"] == "_my_rpc_server_is_ready" then
                dispatch_result = true
                result_value = "--PASS--"
            else
                dispatch_result, result_value = pcall(dispatch.dispatch_function, msg_obj)
            end
            
            if dispatch_result then
                sendToSeqMsg = msg_obj:respond(result_value):serialize()
            else
                sendToSeqMsg = msg_obj:error_respond(result_value):serialize()
            end


        end

        -- if string.match(tostring(msg), '"method":%s*"end_test"') then
        --     print('collectgarbage("collect")')
        --     collectgarbage("collect")
        -- end

        if args.echo ~=0 then logger:info(" < Result > %s", sendToSeqMsg) end
        publish.send("< Result > \t"..sendToSeqMsg)

        sequence_zmq:send(sendToSeqMsg)

        msg_obj = nil
        sendToSeqMsg = nil

    end)

    if args.uut == 0 then
        pServer:RegisterFixtureRepHandler(poller, context, publish);
    end

    setmetatable(obj, {__index = server})

    return obj
end



-- Process messages from both sockets
function server:run()
    logger:info("TE is running now.")
    self.poller:start()
end


return server
