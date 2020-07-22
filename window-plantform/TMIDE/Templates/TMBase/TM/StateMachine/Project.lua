
local json = require("dkjson")
local common = require("SMCommon")

local project = {}

-- 在这里添加各个项目自己特殊化的初始化逻辑
-- 如果初始化失败可以退出整个程序
-- ctx：创建成功的ZMQ content
function project.Init(ctx)
	
end


-- 在这里添加各个项目跟IDE定制化协议处理流程, 
-- 在该函数里面不能调用os.exit类似退出整个程序的代码

-- json_obj:json对象
-- triggerRep: ZMQ RESP for IDE;用来回复json格式的消息给IDE，必须有回复,
--             调用方法eg：triggerRep:send(json.encode({["status"] = "OK"}))

function project.DealWithSpecialMsg(json_obj, triggerRep)

end

-- 测试完成，Sequence public test end 消息；SM会处理该消息
-- 各个项目可以在此添加特殊化处理流程
-- 在该函数里面不能调用os.exit类似退出整个程序的代码

-- json_obj:json对象

function project.DealWithTestingEndMsg(json_obj)
	-- body
end

return project
