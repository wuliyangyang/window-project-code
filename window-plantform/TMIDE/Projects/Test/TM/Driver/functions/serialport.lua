print("< ".."serialPort.MCO > Load mco.lua!!!!!!....")

local _MCO = {}
-- local time_utils = require("utils.time_utils")
local mco_device = require("interface.ZmqSerialPort");
local ztimer = require("lzmq.timer")
local config = require("config_devices");
local iter ={};
--_MCO.PortName   = "McoBoard"--..tostring(args.uut);

local pubPort = 7020
local pubAddress = "tcp://127.0.0.1:"..tostring(pubPort+args.uut);

local setting = "115200,n,8,1"
_MCO.ModuleName = "TestSerlPort"
_MCO.DevicePort = config[_MCO.DeviceName][args.uut+1]
_MCO.DeviceName = _MCO.ModuleName..tostring(args.uut);
_MCO.port = nil;
local ConnectState = false;
--===========================================================================--
--===========================================================================--
function _MCO.Open(sportname,setting)
  print("====================================================>>>")
  print("        Opening MCO Port            ");
  print("MCO Port: ".._MCO.DeviceName, sportname);
  local isObject = mco_device.CreateSerialPort(_MCO.DeviceName);
  if isObject == nil then
    print("Create MCO Port fail");
    return false,"Create MCO Port fail";
  end

  mco_device.SetSerialPort(_MCO.DeviceName,setting or "115200,n,8,1");
  local flag = mco_device.OpenSerialPort(_MCO.DeviceName,sportname);

  -- print("flag",flag)
  if not flag then
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



function _MCO.SetDetectString(Regex)
    return mco_device.SetDetectString(_MCO.DeviceName,Regex)
end

function _MCO.WaitForString(timeout)
    return mco_device.WaitForString(_MCO.DeviceName,timeout)
end

function _MCO.WriteString(cmd)
  -- M.ClearBuffer(szDeviceName);
    return mco_device.WriteString(_MCO.DeviceName,cmd.."\r\n",3000)
end

function _MCO.ReadString()
  local data = mco_device.ReadString(_MCO.DeviceName)
  return data
end

function _MCO.SendReceive(cmd,timeout)
    if timeout == nil then
        timeout = 3000;
    end    
    _MCO.WriteString(cmd);
    _MCO.WaitForString(timeout);
    local data = _MCO.ReadString();
    return data

end

function _MCO.Test(sequence)
  local cmd = sequence.param1
  --mco_device.SetDetectString("\r\n")
  local ret = mco_device.WriteString(_MCO.DeviceName,cmd.."\r\n",3000);
  mco_device.WaitForString(_MCO.DeviceName,5000);
  --ztimer.sleep(100);
  local data = mco_device.ReadString(_MCO.DeviceName)
  if data ~= nil then
		return 0;
  end  
  return -1
end 

if ConnectState then
  _MCO.Open(_MCO.DevicePort ,setting);
  mco_device.CreateIPC(_MCO.DeviceName,nil,pubAddress);
  mco_device.SetPubOpt(_MCO.DeviceName,1);
end 

return _MCO