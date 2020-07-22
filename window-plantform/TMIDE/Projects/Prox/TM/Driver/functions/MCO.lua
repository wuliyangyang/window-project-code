print("< "..tostring(args.uut).."instrument.MCO > Load mco.lua!!!!!!....")

local _MCO = {}
local time_utils = require("utils.time_utils")
local mco_device = require("console.interface.ZmqSerialPort");
local tc = require("TestContext.TestContext");
local iter ={};
_MCO.PortName   = "McoBoard"..tostring(args.uut + args.module * args.slots);
_MCO.DeviceName = "MCO_RS232"..tostring(args.uut + args.module * args.slots);
_MCO.port = nil;
_MCO.waitCnt = 10;
local ConnectState = false;
--===========================================================================--
--===========================================================================--
function _MCO.Open(sportname,setting)
  print("====================================================>>>")
  print("       Opening MCO Port            ");
  print("MCO Port: ".._MCO.DeviceName, sportname);
  local isObject = mco_device.CreateSerialPort(_MCO.DeviceName);
  if isObject == nil then
    print("Create MCO Port fail");
    return false,"Create MCO Port fail";
  end

  mco_device.SetSerialPort(_MCO.DeviceName,setting or "115200,n,8,1");
  local flag = mco_device.OpenSerialPort(_MCO.DeviceName,sportname);

  if not flag then
    ConnectState = false
    assert(false,"Open MCO Port fail");
    return false,"Open MCO Port fail";
  end
  ConnectState = true;
  mco_device.SetDetectString(_MCO.DeviceName,"tweety >> ");--KEITHLEY

  print("        Open MCO Port complete!        ")
  print("<<<====================================================")
  return true;
end

function _MCO.Close()
  if _MCO.DeviceName == nil or #_MCO.DeviceName == 0 then
    return;
  end
  print("====================================================>>>")
  print("       Close MCO Port         ")
  print("MCO Port: ".._MCO.DeviceName);
  if ConnectState then
    ConnectState = false;
    mco_device.CloseSerialPort(_MCO.DeviceName);
    print("MCO Port Disconnect");
  end
  mco_device.DeleteSerialPort(_MCO.DeviceName);
  _MCO.DeviceName = "";

  print("       Close MCO Port complete      ")
  print("<<<====================================================")

  return true;
end

function _MCO.Poweron(sequence)
  local timeout = 2000;
  if sequence.timeout then
       timeout = sequence.timeout;
  end
  local cmd = "dutpwr on";
  local data = mco_device.SendReceive(_MCO.DeviceName,cmd.."\r\n",timeout);
  print("POWERON:",data);
  if string.find(tostring(data),"DUT Power = on") then
     return true;
  else
     return false;
  end
end

function _MCO.Poweroff(sequence)
  local timeout = 2000;
  if sequence.timeout then
       timeout = sequence.timeout;
  end
  local cmd = "dutpwr off";
  local data = mco_device.SendReceive(_MCO.DeviceName,cmd.."\r\n",timeout);
  print("POWEROFF:",data);
  if string.find(tostring(data),"DUT Power = off") then
     return true;
  else
     return false;
  end
end

function _MCO.start(sequence)
  local timeout = 8000;
  if sequence.timeout then
       timeout = sequence.timeout;
  end
  iter = nil;
  iter = {};
  --Test Time:   7.0560 s
  local cmd = "dpflextest";
  local result_value = mco_device.SendReceive(_MCO.DeviceName,cmd.."\r\n",timeout); 
  print("MCO->Start: raw data:\n",result_value);
  if result_value and string.find(tostring(result_value),"tweety >>") then
      local isFail = string.find(tostring(result_value),"Doppler module did not ACK on i2c");
      if not isFail then
        local count = 0;
        for key,value in string.gmatch(result_value, "([%u%d_]+)[ ]*=[ ]*([%u%d%.%-]+);") do
            -- print("MCO->Start key:",key,"value:",value);
            count = count + 1;
            iter[key] = value;
        end
        iter["FW_TEST_TIME"] = string.match(tostring(result_value),"Test Time:[ ]*([%d%.]+)");
        print("item count:",count,"MCO->FW_TEST_TIME: ",iter["FW_TEST_TIME"]);

        --Error: write to led board (addr = 0x2C) failed
        local errStr = "";
        for err in string.gmatch(result_value, "Error: ([^\r^\n]+)") do
            if not string.find(errStr,err) then
              print("MCO->Start err:",err);
              errStr = errStr..err..";";
            end
        end

        errStr = string.gsub(errStr,",",".");
        if #errStr == 0 then
          iter["ERROR_STRING"] = "NONE";
        else
          iter["ERROR_STRING"] = "--FAIL--"..errStr;
        end
        return true;
      else
        return "--FAIL--Doppler module did not ACK on i2c;seems like a connection issue";
      end
  else
      return false;
  end
  
end

function _MCO.GetValue(sequence)
  local vaule = 0.0;
  if sequence and sequence.param1 then
      local key = sequence.param1;
      vaule = iter[key];
      print("MCO->>GetValue Key: "..tostring(key).."Value: "..tostring(vaule));
      if vaule then
          return vaule;
      else
          return false;
      end
  else
      return vaule;
  end
end

local DoFix     = {"do","1",1};
local DoIn      = {"do","5",1};
local DoDown    = {"do","2",1};
local DiInS1    = {"di","4",1};
local DiInS2    = {"di","5",0};
local DiDownS1  = {"di","6",1};
local DiDownS2  = {"di","7",0};

local DoChip1In   = {"do","4",1};
local DoChip2In   = {"do","0",1};
local DiChip1InS1 = {"di","2",0};
local DiChip1InS2 = {"di","3",1};
local DiChip2InS1 = {"di","0",0};
local DiChip2InS2 = {"di","1",1};

local function TranDoCmd(cmd,isReverse)
  if isReverse then
    return cmd[1].." "..cmd[2].." "..tostring(1-cmd[3]).."\r\n";
  else
    return cmd[1].." "..cmd[2].." "..tostring(cmd[3]).."\r\n";
  end
end

local function TranDiCmd(cmd)
    return cmd[1].." "..cmd[2].."\r\n";
end

local function CheckCmdRet(ret,cmd,isReverse)
  local flag = "";
  if isReverse then
    flag = cmd[1]..cmd[2].." = "..tostring(1-cmd[3]);
  else
    flag = cmd[1]..cmd[2].." = "..tostring(cmd[3]);
  end

  if string.find(tostring(ret),flag) then
    return true;
  else
    return false;
  end
end

function _MCO.DUTFix()
  -- body
  local ret = mco_device.SendReceive(_MCO.DeviceName,TranDoCmd(DoFix),500);
  return CheckCmdRet(ret,DoFix);
end

function _MCO.DUTUnFix()
  -- body
  local ret = mco_device.SendReceive(_MCO.DeviceName,TranDoCmd(DoFix,true),500);
  return CheckCmdRet(ret,DoFix,true);
end

function _MCO.isDUTIn()
  -- body
  local S1 = CheckCmdRet(mco_device.SendReceive(_MCO.DeviceName,TranDiCmd(DiInS1),500),DiInS1);
  local S2 = CheckCmdRet(mco_device.SendReceive(_MCO.DeviceName,TranDiCmd(DiInS2),500),DiInS2);
  return S1 and S2;
end

function _MCO.DUTIn()
  -- body
  local ret = mco_device.SendReceive(_MCO.DeviceName,TranDoCmd(DoIn),500);
  if CheckCmdRet(ret,DoIn) then
    local isIn = _MCO.isDUTIn();
    local cnt = 0;
    while (not isIn) and cnt <= _MCO.waitCnt do 
      isIn = _MCO.isDUTIn();
      cnt = cnt + 1;
      time_utils.delay(100);
    end
    return isIn;
  else
    return false;
  end
end

function _MCO.isDUTOut()
  -- body
  local S1 = CheckCmdRet(mco_device.SendReceive(_MCO.DeviceName,TranDiCmd(DiInS1),500),DiInS1,true);
  local S2 = CheckCmdRet(mco_device.SendReceive(_MCO.DeviceName,TranDiCmd(DiInS2),500),DiInS2,true);
  return S1 and S2;
end

function _MCO.DUTOut()
  -- body
  local ret = mco_device.SendReceive(_MCO.DeviceName,TranDoCmd(DoIn,true),500);
  if CheckCmdRet(ret,DoIn,true) then
    local isOut = _MCO.isDUTOut();
    local cnt = 0;
    while (not isOut) and cnt <= _MCO.waitCnt do 
      isOut = _MCO.isDUTOut();
      cnt = cnt + 1;
      time_utils.delay(100);
    end
    return isOut;
  else
    return false;
  end
end

function _MCO.isDUTDown()
  -- body
  local S1 = CheckCmdRet(mco_device.SendReceive(_MCO.DeviceName,TranDiCmd(DiDownS1),500),DiDownS1);
  local S2 = CheckCmdRet(mco_device.SendReceive(_MCO.DeviceName,TranDiCmd(DiDownS2),500),DiDownS2);
  return S1 and S2;
end

function _MCO.DUTDown()
  -- body
  local ret = mco_device.SendReceive(_MCO.DeviceName,TranDoCmd(DoDown),500);
  if CheckCmdRet(ret,DoDown) then
    local isDown = _MCO.isDUTDown();
    local cnt = 0;
    while (not isDown) and cnt <= _MCO.waitCnt do 
      isDown = _MCO.isDUTDown();
      cnt = cnt + 1;
      time_utils.delay(100);
    end
    return isDown;
  else
    return false;
  end
end

function _MCO.isDUTUp()
  -- body
  local S1 = CheckCmdRet(mco_device.SendReceive(_MCO.DeviceName,TranDiCmd(DiDownS1),500),DiDownS1,true);
  local S2 = CheckCmdRet(mco_device.SendReceive(_MCO.DeviceName,TranDiCmd(DiDownS2),500),DiDownS2,true);
  return S1 and S2;
end

function _MCO.DUTUp()
  -- body
  local ret = mco_device.SendReceive(_MCO.DeviceName,TranDoCmd(DoDown,true),500);
  if CheckCmdRet(ret,DoDown,true) then
    local isUp = _MCO.isDUTUp();
    local cnt = 0;
    while (not isUp) and cnt <= _MCO.waitCnt do 
      isUp = _MCO.isDUTUp();
      cnt = cnt + 1;
      time_utils.delay(100);
    end
    return isUp;
  else
    return false;
  end
end

function _MCO.isChip1In()
  -- body
  local S1 = CheckCmdRet(mco_device.SendReceive(_MCO.DeviceName,TranDiCmd(DiChip1InS1),500),DiChip1InS1);
  local S2 = CheckCmdRet(mco_device.SendReceive(_MCO.DeviceName,TranDiCmd(DiChip1InS2),500),DiChip1InS2);
  return S1 and S2;
end

function _MCO.Chip1In()
  -- body
  local ret = mco_device.SendReceive(_MCO.DeviceName,TranDoCmd(DoChip1In),500);
  if CheckCmdRet(ret,DoChip1In) then
    local isIn = _MCO.isChip1In();
    local cnt = 0;
    while (not isIn) and cnt <= _MCO.waitCnt do 
      isIn = _MCO.isChip1In();
      cnt = cnt + 1;
      time_utils.delay(100);
    end
    return isIn;
  else
    return false;
  end
end

function _MCO.isChip1Out()
  -- body
  local S1 = CheckCmdRet(mco_device.SendReceive(_MCO.DeviceName,TranDiCmd(DiChip1InS1),500),DiChip1InS1,true);
  local S2 = CheckCmdRet(mco_device.SendReceive(_MCO.DeviceName,TranDiCmd(DiChip1InS2),500),DiChip1InS2,true);
  return S1 and S2;
end

function _MCO.Chip1Out()
  -- body
  local ret = mco_device.SendReceive(_MCO.DeviceName,TranDoCmd(DoChip1In,true),500);
  if CheckCmdRet(ret,DoChip1In,true) then
    local isOut = _MCO.isChip1Out();
    local cnt = 0;
    while (not isOut) and cnt <= _MCO.waitCnt do 
      isOut = _MCO.isChip1Out();
      cnt = cnt + 1;
      time_utils.delay(100);
    end
    return isOut;
  else
    return false;
  end
end

function _MCO.isChip2In()
  -- body
  local S1 = CheckCmdRet(mco_device.SendReceive(_MCO.DeviceName,TranDiCmd(DiChip2InS1),500),DiChip2InS1);
  local S2 = CheckCmdRet(mco_device.SendReceive(_MCO.DeviceName,TranDiCmd(DiChip2InS2),500),DiChip2InS2);
  return S1 and S2;
end

function _MCO.Chip2In()
  -- body
  local ret = mco_device.SendReceive(_MCO.DeviceName,TranDoCmd(DoChip2In),500);
  if CheckCmdRet(ret,DoChip2In) then
    local isIn = _MCO.isChip2In();
    local cnt = 0;
    while (not isIn) and cnt <= _MCO.waitCnt do 
      isIn = _MCO.isChip2In();
      cnt = cnt + 1;
      time_utils.delay(100);
    end
    return isIn;
  else
    return false;
  end
end

function _MCO.isChip2Out()
  -- body
  local S1 = CheckCmdRet(mco_device.SendReceive(_MCO.DeviceName,TranDiCmd(DiChip2InS1),500),DiChip2InS1,true);
  local S2 = CheckCmdRet(mco_device.SendReceive(_MCO.DeviceName,TranDiCmd(DiChip2InS2),500),DiChip2InS2,true);
  return S1 and S2;
end

function _MCO.Chip2Out()
  -- body
  local ret = mco_device.SendReceive(_MCO.DeviceName,TranDoCmd(DoChip2In,true),500);
  if CheckCmdRet(ret,DoChip2In,true) then
    local isOut = _MCO.isChip2Out();
    local cnt = 0;
    while (not isOut) and cnt <= _MCO.waitCnt do 
      isOut = _MCO.isChip2Out();
      cnt = cnt + 1;
      time_utils.delay(100);
    end
    return isOut;
  else
    return false;
  end
end

function _MCO.AllChipResetOut()
  -- body
  local _isChip1Out = _MCO.Chip1Out();
  local _isChip2Out = _MCO.Chip2Out();
  if _isChip1Out and _isChip2Out then
    return 1,"All grey chip Reset successful";
  elseif (not _isChip1Out) and (not _isChip2Out) then
    return -61,"All grey chip Reset fail";
  elseif (not _isChip1Out) then
    return -62,"grey chip1 Out fail";
  elseif (not _isChip2Out) then
    return -63,"grey chip2 Out fail";
  else
    return "unknow error";
  end
end

function _MCO.AllChipResetIn()
  -- body
  local _isChip1In = _MCO.Chip1In();
  local _isChip2Out = _MCO.Chip2Out();
  if _isChip1In and _isChip2Out then
    return 1,"All grey chip Reset successful";
  elseif (not _isChip1In) and (not _isChip2Out) then
    return -64,"All grey chip Reset fail";
  elseif (not _isChip1In) then
    return -65,"grey chip1 In fail";
  elseif (not _isChip2Out) then
    return -66,"grey chip2 Out fail";
  else
    return "unknow error";
  end
end

function _MCO.Chip1ResetIn()
  local _isChip1In = _MCO.Chip1In();
  if _isChip1In then
    return 1,"grey chip1 reset in successful";
  elseif (not _isChip1In) then
    return -67,"grey chip1 In fail";
  else
    return "unknow error"
  end
end

function _MCO.Chip1ResetOut()
  local _isChip1Out = _MCO.Chip1Out();
  if _isChip1Out then
    return 1,"grey chip1 reset out successful";
  elseif(not _isChip1Out) then
    return -68,"grey chip1 Out fail";
  else
    return "unknow error";
  end
end

function _MCO.FixtureControl(sequence)
  -- body
  if not ConnectState then
    return -99,"MCO McoBoard Disconnect";
  end
  local cmd = tostring(sequence.param1);
  if cmd == "release" then
    if _MCO.isDUTUp() then
      return 10,"Pogo is Up,skip Up control";
    else
      if _MCO.DUTUp() then
        return 11,"control Pogo Up successful";
      else
        return -10,"control Pogo Up fail";
      end
    end

  elseif cmd == "out" then
    -- local code,str = _MCO.Chip1ResetOut();
    -- if code < 0 then
    --   return code,str;
    -- end
    
    if _MCO.isDUTOut() then
      return 20,"Pogo is Out,skip Out control";
    else
      if _MCO.isDUTUp() then
        if _MCO.DUTOut() then
          _MCO.DUTUnFix();
          return 21,"control Pogo Out successful";
        else
          return -20,"control Pogo Out fail";
        end
      else
        return -21,"Pogo is Not Up,skip Out control";
      end
    end

  elseif cmd == "in" then
    _MCO.DUTFix();
    -- local code, str = _MCO.Chip1ResetIn();
    -- if code < 0 then
    --   return code,str;
    -- end
    time_utils.delay(20);

    if _MCO.isDUTIn() then
      return 30,"Pogo is In,skip In control";
    else
      if _MCO.isDUTUp() then
        if _MCO.DUTIn() then
          return 31,"control Pogo In successful";
        else
          return -30,"control Pogo In fail";
        end
      else
        return -31,"Pogo is Not Up,skip In control";
      end
    end

  elseif cmd == "down" then
    if _MCO.isDUTDown() then
      return 40,"Pogo is Down,skip Down control";
    else
      if _MCO.isDUTIn() then
        time_utils.delay(200);
        if _MCO.DUTDown() then
          return 41,"control Pogo Down successful";
        else
          return -40,"control Pogo Down fail";
        end
      else
        return -41,"Pogo is Not In,skip Down control";
      end
    end

  elseif cmd == "remove" then
    if _MCO.DUTUnFix() then
      return 50,"unfix successful";
    else
      return -50,"unfix fail";
    end
  else
    return "error fixtrue control cmd";
  end
end

function _MCO.GetSlotNumber(sequence)
  -- body
  return "Slot"..tostring(args.uut + args.module * args.slots);
end

function _MCO.GetModuleSN(sequence)
  return tc:GetMLBSN();
end

function _MCO.MTCP_Debug(sequence)
  if sequence.param1 then
    local Str = tostring(sequence.param1);
    Str = string.gsub(Str,";",",");
    return Str;
  end 
end

function _MCO.init()
  logger:info("McoBard  init")
  _MCO.Open(_MCO.PortName,"115200,n,8,1")
  return true
end

function _MCO.self_test()
  logger:info("McoBard self_test")
  return true
end

function _MCO.clear_buffer()
  logger:info("McoBard  clear_buffer")
  return true
end

return _MCO