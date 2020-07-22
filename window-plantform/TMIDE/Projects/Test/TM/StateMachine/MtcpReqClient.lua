local _MtcpClient = {};

package.cpath = package.cpath..";"..JoinPath(JoinPath(JoinPath(CurrentDir(), "Driver"),"lib"),"?.dll")

local json        = require("dkjson")
local tscrHandler = require("csvHandle")
local common      = require("SMCommon")
local tc          = require("TestContext.TestContext")

_MtcpClient.Client   = {};
_MtcpClient.TcpClass = require("MTCProto");
_MtcpClient.Address  = "169.254.1.111";       --address for connect MTCP.
_MtcpClient.Port     = 61806;				  --port for connect MTCP.
_MtcpClient.TimeOut  = 3000;				  --timeout for connect MTCP.
_MtcpClient.RepCsvPath = JoinPath(JoinPath(CurrentDir(),"Profile"), "MtcpCsvProx__Module"..tostring(args.module).."Slot");
_MtcpClient.LogPath = "C:\\vault\\Intelli_log\\MTCProto_StateMachine_Module"..tostring(args.module).."_Slot";
_MtcpClient.ReqCsvPath = JoinPath(JoinPath(CurrentDir(),"TesterConfig"), "RequestCsv_Module"..tostring(args.module).."_".."Slot");
_MtcpClient.isInit = false;

function _MtcpClient:InitClient()
	-- body
	if self.isInit then
		logger:fatal("Mtcp client can have only one instance") 
		return nil, "Mtcp client can have only one instance"
	end

	for i=1,args.slots do
		self.Client[i-1] = self.TcpClass.NEW_OBJECT();--NEW_OBJECT();--GLOBAL_MTCP;
		self.Client[i-1]:setLogPath(self.LogPath..tostring(i-1));
		self.Client[i-1]:setTestCSVPath(self.RepCsvPath..tostring(i-1)..".csv");
	end
	self.isInit = true;
	return true;
end

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

-- function _MtcpClient:GetMtcpUploadFlag()
-- 	return true;
-- end

function _MtcpClient:SendRequest2Mtcp(Module,Slot)
	
	local slot = Slot
	local reqCsvPath = self.ReqCsvPath..tostring(slot)..".csv";
	local repCsvPath = self.RepCsvPath..tostring(slot)..".csv";
	local mtcp = self.Client[slot];

	tscrHandler.initCsv()
	tscrHandler.addCsv({group="TSCF",item="#STATION_TYPE",value=tscrInfo.stationtype})
	tscrHandler.addCsv({group="TSCF",item="#STATION_SN",value=tscrInfo.stationsn})
	tscrHandler.addCsv({group="TSCF",item="#TM_SW_VER",value=tscrInfo.swversion})
	tscrHandler.addCsv({group="TSCF",item="#FW_SW_VER",value=tscrInfo.fwversion})
	tscrHandler.addCsv({group="TSCF",item="#MODULE_SN",value=tostring(tc:GetMLBSN(Module,Slot))})
	tscrHandler.addCsv({group="TSCF",item="#SOCKET_SN",value=tostring(tc:GetSocket(Module,Slot))})
	tscrHandler.addCsv({group="TSCF",item="#TESTER_NAME",value=tscrInfo.testername})
	tscrHandler.addCsv({group="TSCF",item="#TESTER_ID",value=tscrInfo.testerid})
	tscrHandler.addCsv({group="TSCF",item="#OPERATOR",value=tscrInfo.operator})
	tscrHandler.addCsv({group="TSCF",item="#TEST_MODE",value=tscrInfo.testmode})
	tscrHandler.addCsv({group="TSCF",item="#SEQ",value=1})

	tscrHandler.addCsv({group="TSED",item="TSED",value="tsed"})
	local str = tscrHandler.getCsv()
	logger:info("< SendRequest2Mtcp: > Create TSCF request csv :\n" .. tostring(str))

	---------------------------------------------------
	--  mtcp.SendGENL(arg1,arg2)
	--  arg1:csv file path  arg2: 0-request  1-report
	---------------------------------------------------
	local fp = io.open(reqCsvPath,"w");
	if fp then
		fp:write(str);
		fp:flush();
		fp:close();
		logger:info("< SendRequest2Mtcp > : Write request file to path "..tostring(reqCsvPath));
	else
		logger:info("< SendRequest2Mtcp > : Write file fail to path "..tostring(reqCsvPath));
		return false;
	end

	if mtcp:isOpen() then
		mtcp:Close();
	end

	local IsOpen = mtcp:Open(self.Address,self.Port,self.TimeOut);
	logger:info("< SendRequest2Mtcp > : ".."Current Mtcp setting Address:"..tostring(self.Address).." Port:"..tostring(self.Port).." TimeOut:"..tostring(self.TimeOut));
	logger:info("< SendRequest2Mtcp > : ".."Current MTCProto.dll version:"..tostring(mtcp:version()));
	logger:info("< SendRequest2Mtcp > : ".."Connect Mtcp server before request sequence:"..tostring(IsOpen));

	if tonumber(IsOpen) ~= 0 then
		IsOpen = mtcp:Open(mtcp_address,mtcp_port,mtcp_timeout);
		logger:info("< SendRequest2Mtcp > : ".."retry Connect MTCP before request sequence: "..tostring(IsOpen));
		if tonumber(IsOpen) ~= 0 then
			IsOpen = mtcp:Open(mtcp_address,mtcp_port,mtcp_timeout);
			logger:info("< SendRequest2Mtcp > : ".."retry again Connect MTCP before request sequence: "..tostring(IsOpen));
		end
	end

	if mtcp:isOpen() then
		logger:info("< SendRequest2Mtcp > : ".."Connect MTCP Successful,code:"..tostring(IsOpen));
		local mtcp_ret = mtcp:SendGENL(reqCsvPath,0);
		logger:info("< SendRequest2Mtcp > : SendGENL error string: "..tostring(mtcp_ret));
		local errCode = mtcp:rspERRC();
		local errStr  = mtcp:rspERRS();
		logger:info("< SendRequest2Mtcp > : rspERRC : "..tostring(errCode));
		logger:info("< SendRequest2Mtcp > : rspERRS : "..tostring(errStr));

		local fileSize = mtcp:csvSize();
		mtcp:Close();
		logger:info("< SendRequest2Mtcp > : request file Size :"..tostring(fileSize));
		if fileSize <= 0 then
			logger:info("< SendRequest2Mtcp > : MTCP server have no test file return,skip test!");
			return false;
		else
			logger:info("< SendRequest2Mtcp > : request test csvFile from MTCP Successful");
			return true,repCsvPath;
		end

	else
		logger:info("< SendRequest2Mtcp > : connect MTCP server fail,please check your setting")
		return false;
	end

end

return _MtcpClient;