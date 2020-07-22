print("< ".."TCP.MCO > Load mco.lua!!!!!!....")

local _MCO = {}
-- local time_utils = require("utils.time_utils")
local ztimer = require("lzmq.timer")
local config = require("config_devices");
local mco_device = require("interface.ZmqTcpClient");
local iter ={};

_MCO.ModuleName = "TestTCP"
_MCO.DeviceAddress = config[_MCO.ModuleName][args.uut+1]
_MCO.DeviceName = _MCO.ModuleName..tostring(args.uut);

local replyAddress= nil
local pubPort = 6820
local pubAddress = "tcp://127.0.0.1:"..tostring(pubPort+args.uut);


local ConnectState = false;
--===========================================================================--
--===========================================================================--
function _MCO.Open(setting)
  print("====================================================>>>")
  print("        Opening MCO Port            ");
  print("MCO Port: ".._MCO.DeviceName,setting);
  local isObject = mco_device.CreateTcpClient(_MCO.DeviceName);
  if isObject == nil then
    print("Create MCO Port fail");
    return false,"Create MCO Port fail";
  end

  local flag = mco_device.OpenTcpClient(_MCO.DeviceName,setting);

  -- print("flag",flag)
  if not flag then
    assert(false,"Open MCO Port fail");
    return false,"Open MCO Port fail";
  end
  ConnectState = true;
  mco_device.SetDetectString(_MCO.DeviceName,"\r\n");--"tweety >> ");--KEITHLEY

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
    mco_device.CloseTcpClient(_MCO.DeviceName);
    print("MCO Port Disconnect");
  end
  mco_device.DeleteTcpClient(_MCO.DeviceName);
  _MCO.DeviceName = "";

  print("       Close MCO Port complete      ")
  print("<<<====================================================")

  return true;
end


function _MCO.GetValue(sequence)
  local vaule = 0.0;
  if sequence and sequence.param1 then
      local key = sequence.param1;
      vaule = iter[key];
      print("MCO_GetValue_Vaule:",vaule);
      if vaule then
          return vaule;
      else
          return false;
      end
  else
      return vaule;
  end
  
end


function _MCO.SetDetectString(Regex)
    return mco_device.SetDetectString(_MCO.DeviceName,Regex)
end

function _MCO.WaitForString(timeout)
    return mco_device.WaitForString(_MCO.DeviceName,timeout)
end

function _MCO.WriteString(cmd)
  -- M.ClearBuffer(szDeviceName);
    return mco_device.WriteString(_MCO.DeviceName,cmd,3000)
end

function _MCO.ReadString()
  local data = mco_device.ReadString(_MCO.DeviceName)
  return data
end

function _MCO.SendReceive(cmd,timeout)
    if timeout == nil then
        timeout = 3000;
    end 
  
    mco_device.WriteString(_MCO.DeviceName,cmd,timeout)
    ztimer.sleep(100);
    local data = mco_device.ReadString(_MCO.DeviceName)  
    return data

end
 
if not ConnectState then
  _MCO.Open(_MCO.DeviceAddress)
  mco_device.CreateIPC(_MCO.DeviceName,pubAddress,replyAddress)
end  

return _MCO