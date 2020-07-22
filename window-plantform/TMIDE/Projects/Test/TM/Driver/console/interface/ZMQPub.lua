

local  M = {}
local PubObjs = {}
local zmq = require("lzmq")
local timer = require("lzmq.timer")
local context = zmq.init(1)

--publisher:bind("tcp://*:6720")
function M.Bind(ObjName,address)
    -- body
    local publisher = context:socket(zmq.PUB)
    publisher:bind(address)
    PubObjs[ObjName]=publisher;

end
--  Initialize random number generator
function M.Send(ObjName,str)
    -- body
    PubObjs[ObjName]:send(str)
end

function M.Close()
    -- body
    PubObjs[ObjName]:close()
    context:term()
end

return M