local _MtcpClient = {};

package.cpath = package.cpath..";"..JoinPath(JoinPath(JoinPath(CurrentDir(), "Driver"),"lib"),"?.dll")

local DLCommon = require("DLCommon")
local DLContext = require("DLContext")
local json = require("dkjson")
local LogPub = require("LoggerPub")
local tc   = require("TestContext.TestContext")
local CurrDir = JoinPath(CurrentDir(),"DataLogger");
-- local csvAnalysis = require("utils.csv_analysis")

_MtcpClient.Client  = require("MTCProto").NEW_OBJECT();--NEW_OBJECT();--GLOBAL_MTCP;
_MtcpClient.Address = "169.254.1.111";       --address for connect MTCP.
_MtcpClient.Port    = 61806;				--port for connect MTCP.
_MtcpClient.TimeOut = 3000;				    --timeout for connect MTCP.
_MtcpClient.MtcpRspPath = CurrDir.."\\MtcpResponse_Module"..tostring(args.module).."_Slot"..tostring(args.uut)..".csv";
_MtcpClient.Client:setLogPath("C:\\vault\\Intelli_log\\MTCProto_DataLogger_Module"..tostring(args.module).."_Slot"..tostring(args.uut));
_MtcpClient.Client:setTestCSVPath(_MtcpClient.MtcpRspPath);

function _MtcpClient:IsFileEmpty(filePath)
	local file = io.open(filePath,"rb")
    if file then
        local len = assert(assert(file):seek("end"))
        file:close()
        if len == 0 then
           return true
        else
           return false
        end
 	end
 	return true
end

function _MtcpClient:SendPivot2Mtcp(path)
	-- logger:info("< SendPivot2Mtcp -> MTCP > Begain Send Pivot ")
	-- logger:info("mtcp type:"..tostring(type(_MtcpClient.Client)));
	-- logger:info("< SendPivot2Mtcp -> MTCP > Begain excute isOpen")
	-- if mtcp:isOpen() then
	-- 	logger:info("< SendPivot2Mtcp -> MTCP > still connect with MTCP, Begain disconnect")
	-- 	mtcp:Close();
	-- 	logger:info("< SendPivot2Mtcp -> MTCP > still connect with MTCP, finish disconnect")
	-- end
	-- logger:info("< SendPivot2Mtcp -> MTCP > Finish excute isOpen")


	logger:info("< SendPivot2Mtcp -> MTCP > try connect MTCP ".." "..tostring(self.Address).." "..tostring(self.Port).." "..tostring(self.TimeOut));
	local IsOpen = self.Client:Open(self.Address,self.Port,self.TimeOut);

	if tonumber(IsOpen) ~= 0 then
		logger:info("< SendPivot2Mtcp -> MTCP > connect MTCP fail,retry")
		IsOpen = self.Client:Open(self.Address,self.Port,self.TimeOut);
		if tonumber(IsOpen) ~= 0 then
			logger:info("< SendPivot2Mtcp -> MTCP > still connect MTCP fail,retry again")
			IsOpen = self.Client:Open(self.Address,self.Port,self.TimeOut);
		end
	end

	if tonumber(IsOpen) ~= 0 then
		logger:info("< SendPivot2Mtcp -> MTCP > connect MTCP fail!!!")
	else
		logger:info("< SendPivot2Mtcp -> MTCP > connect MTCP Successful")
	end

	---------------------------------------------------
	--  mtcp.SendGENL(arg1,arg2)
	--  arg1:csv file str  arg2: 0-request  1-report
	---------------------------------------------------
	if IsOpen==0 then
		local mtcp_ret = self.Client:SendGENL(path,1);
		local ErrCode = self.Client:rspERRC();
		local ErrStr  = self.Client:rspERRS();
		local FileSize = self.Client:csvSize();

		local BinCode = nil;
		-- if FileSize > 0 then
		-- 	local csvData = DLCommon:readFile(self.MtcpRspPath);
		-- 	if csvData then
		-- 		local BinData = csvAnalysis.csvDataAnalysis(tostring(csvData),"TID");
		-- 		if BinData and BinData["#TEST_BIN"] and BinData["#TEST_BIN"]["PARAM2"] then
		-- 			BinCode=tonumber(BinData["#TEST_BIN"]["PARAM2"]);
		-- 		end
		-- 	end
		-- end

		DLContext.isPubResult = true;
		LogPub:pubMsg("MtcpResult",{result=ErrCode,errStr=ErrStr,binCode=BinCode});

		self.Client:Close();
		if ErrCode ~= 0 then
			DLContext.TestResult = "FAIL";
			DLContext.ItemsErrMsg = tostring(ErrStr);
			logger:info("< SendPivot2Mtcp -> MTCP > : send pivot csvFile to MTCP with errCode:"..tostring(ErrCode).." errStr:"..tostring(ErrStr));
			return false;
		else
			DLContext.TestResult = "PASS";
			DLContext.ItemsErrMsg = "";
			logger:info("< SendPivot2Mtcp -> MTCP > : send pivot csvFile to MTCP Successful,errCode:"..tostring(ErrCode).." errStr:"..tostring(ErrStr));
			return true;
		end

	else
		logger:info("< SendPivot2Mtcp -> MTCP > : connect MTCP server fail,please check your setting")
		return false;
	end
	return false;
end

function _MtcpClient:ResolveMessage(MsgTime,MsgContent)
	-- body
	if MsgContent.event == DLCommon.MsgType.MSG_SEQUENCE_END then
		if tc:GetIsUpload() then
			logger:info("begin upload pivot log to MTCP");
			if self:IsFileEmpty(DLContext.PivotLogFullPath) == false then
				local MtcpRet = self:SendPivot2Mtcp(DLContext.PivotLogFullPath);
				if MtcpRet == false then
					-- SendPivot2Mtcp(DLContext.PivotLogFullPath);
				else
					-- logger:info("save pivot log Successful");
				end
			else
				logger:info("pivot csv is empty,skip SendPivot2Mtcp");
			end	
			-- tc:SetIsUpload(false);
		else
			logger:info("skip pivot log upload to MTCP");
		end

		return;
	end
end

return _MtcpClient;