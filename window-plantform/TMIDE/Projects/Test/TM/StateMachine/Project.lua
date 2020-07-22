
local json = require("dkjson")
local common = require("SMCommon")
-- local MtcpClient = require("MtcpReqClient")
local tc = require("TestContext.TestContext");
local df = require("TestContext.DebugFlag");
local time_utils = require("utils.time_utils")
local project = {}

local function SendLoadToSeq(uut,path,slot)
	local isLoaded = true;
	if uut.uutEnable then
		local msg = common.generateReqMsg("load",{path})
		logger:info("Project.lua->>SM send load msg: %s to Sequencer %d", msg,slot)
		uut.seqReq:send(msg)

		local resp = uut.seqReq:recv()
		if not string.find(tostring(resp),"has been loaded") then
			isLoaded = false;
			logger:info("Project.lua->>Load test script fail, SM receive msg: %s from Sequencer %d ", resp, slot)
		else
			logger:info("Project.lua->>Load test script successful, SM receive msg: %s from Sequencer %d ", resp, slot)
		end
	end

	return isLoaded;
end

local function SendStepToSeq(uut,slot)
	local isPass = true;
	if uut.uutEnable then
		local msg = common.generateReqMsg("step")
		logger:info("Project.lua->>SM send step msg: %s to Sequencer %d", msg,slot)
		uut.seqReq:send(msg)

		local resp = uut.seqReq:recv()
		if string.find(tostring(resp),"error") then
			isPass = false;
			logger:info("Project.lua->>step test script fail, SM receive msg: %s from Sequencer %d ", resp, slot)
		else
			logger:info("Project.lua->>step test script successful, SM receive msg: %s from Sequencer %d ", resp, slot)
		end
	end

	return isPass;
end

local function FixtureReset(times)
	-- body
	local Count = (times and tonumber(times)) or 1;
	local parseRet = {errorCode = -777 ,errorMsg = "unknow reset error"};
	
	for i=1,Count do
		local ret = sm.fixtureObj:release();
		if ret and json.decode(ret) then
			parseRet = json.decode(ret);
			if parseRet.errorCode >= 0 then
				-- time_utils.delay(10);
				ret = sm.fixtureObj:open();
				if ret and json.decode(ret) then
					parseRet = json.decode(ret);
					if parseRet.errorCode >= 0 then
						sm.fixtureObj:remove();
						return true,{errorCode = 100, errorMsg = "Fixture reset successful"};
					end
				end
			end
		end

		time_utils.delay(50);
	end
	
	return false,parseRet;
end

-- 在这里添加各个项目自己特殊化的初始化逻辑
-- 如果初始化失败可以退出整个程序
-- ctx：创建成功的ZMQ content
function project.Init(ctx)
	-- MtcpClient:InitClient();
	if args.motion and args.motion > 0 then
		local _isResetOK,resetInfo = FixtureReset(args.motion);
		if _isResetOK then
			sm.fixtureObj.isResetOK = true;
			logger:info("fixture reset successful while start TM");
		else
			sm.fixtureObj.isResetOK = false;
			logger:info("fixture reset fail while start TM",resetInfo.errorMsg);
		end
	end
end


-- 在这里添加各个项目跟IDE定制化协议处理流程, 
-- 在该函数里面不能调用os.exit类似退出整个程序的代码

-- json_obj:json对象
-- triggerRep: ZMQ RESP for IDE;用来回复json格式的消息给IDE，必须有回复,
--             调用方法eg：triggerRep:send(json.encode({["status"] = "OK"}))

function project.DealWithSpecialMsg(json_obj, triggerRep, uuts)	
	-- triggerRep:send(common.generateErrorRespMsg(json_obj["function"], json_obj["id"], -999, "SM do not deal the '"..tostring(json_obj["params"] and json_obj["params"]["key"]).."'"))
	if json_obj["cmd"] == "reset" then
		-- local slotNum = json_obj["uutNum"] and tonumber(json_obj["uutNum"]);
		-- if slotNum == nil or slotNum < 0 or slotNum >= uuts.n then
		-- 	logger:error("project.DealWithSpecialMsg : slot number %d is wrong in \"PROJECT_CMD\" msg", slotNum)
		-- 	return false;
		-- end

		-- local ResetTestScript = JoinPath(CurrentDir(), "profile/Fixture__Reset.csv")
		-- if SendLoadToSeq(uuts[slotNum+1],ResetTestScript,slotNum) then
		-- 	for i=1,4 do
		-- 		SendStepToSeq(uuts[slotNum+1],slotNum);
		-- 		time_utils.delay(5);
		-- 	end
		-- end

		-- SendLoadToSeq(uuts[slotNum+1],args.profile,slotNum)

		triggerRep:send(common.generateSuccRespMsg(json_obj["cmd"], json_obj["id"], true))
		local _isResetOK,resetInfo = FixtureReset(args.motion);
		sm:PubState("SM_RESET",resetInfo.errorCode,resetInfo.errorMsg);

		-- if _isResetOK then
		-- 	triggerRep:send(common.generateSuccRespMsg(json_obj["cmd"], json_obj["id"], true, resetInfo))
		-- else
		-- 	triggerRep:send(common.generateErrorRespMsg(json_obj["cmd"], json_obj["id"], resetInfo.errorCode, resetInfo.errorMsg))
		-- end
	elseif json_obj["cmd"] == "isReset" then

		if sm.fixtureObj.isResetOK then
			triggerRep:send(common.generateSuccRespMsg(json_obj["cmd"], json_obj["id"], true))
		else
			triggerRep:send(common.generateErrorRespMsg(json_obj["cmd"], json_obj["id"], -200 , "Fixture Init Reset Fail"))
		end
	else
		triggerRep:send(common.generateSuccRespMsg(json_obj["cmd"], json_obj["id"], true))
	end

end

project.isUpload = {};
for i=1,args.slots do
	project.isUpload[i-1] = false; 
end
function project.TriggerRepHandler(json_obj, triggerRep, uuts)
	
	if json_obj["function"] == "start" and json_obj["params"] then

		for _,item in pairs(json_obj["params"].uutinfo) do
			local slotNum = tonumber(item.uutNum)
			if slotNum < 0 or slotNum >= uuts.n then
				logger:error("project.TriggerRepHandler : slot number %d is wrong in \"setsn\" msg", slotNum)
				return false;
			end

			local isUpload = false;
			if (item.isUpload == true and not df:GetFlag(df.SkipMtcp,args.module,slotNum)) or df:GetFlag(df.UseMtcp,args.module,slotNum) then

				isUpload = true;
				-- local flag,path = MtcpClient:SendRequest2Mtcp(args.module,slotNum);
				-- if flag then
				-- 	SendLoadToSeq(uuts[slotNum+1],string.gsub(path,"\\","/"),slotNum);
				-- end
			end

			if project.isUpload[slotNum] ~= isUpload then
				project.isUpload[slotNum] = isUpload
				tc:SetIsUpload(isUpload,args.module,slotNum);
			end

			return false;
		end
	end
	
	return false;
end

-- 测试完成，Sequence public test end 消息；SM会处理该消息
-- 各个项目可以在此添加特殊化处理流程
-- 在该函数里面不能调用os.exit类似退出整个程序的代码

-- json_obj:json对象

-- local tcpClient = require("TcpClient")

function project.DealWithTestingEndMsg(json_obj)
	-- 
	-- local msg = "{\"msgType\":\"REQ\",\"msgName\":\"register\",\"msgChannel\":1,\"msgParam\":null,\"msgResult\":"..result..",\"errMsg\":null}"
	-- local testResult = json_obj.data.result
	-- local msg = "{\"msgType\":\"REQ\",\"msgName\":\"register\",\"msgChannel\":1,\"msgParam\":null,\"msgResult\":"..testResult..",\"errMsg\":null}"
	-- tcpClient.TCPWriteString(msg)
end

return project
