print("< ".."instrument.MCO > Load mco.lua!!!!!!....")

local _MCO = {}
-- local time_utils = require("utils.time_utils")
local mco_device = require("ZmqSerialPort");
local iter ={};
_MCO.PortName   = "McoBoard"--..tostring(args.uut);
_MCO.DeviceName = "MCO_RS232"--..tostring(args.uut);
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
  mco_device.ClearBuffer(_MCO.DeviceName);
  local cmd = "dpflextest";
  local result_value = mco_device.SendReceive(_MCO.DeviceName,cmd.."\r\n",timeout); 
  print("MCO_Start_result_value:",result_value);
  if result_value  and string.find(tostring(result_value),"tweety >>") then
      for s in string.gfind(result_value, "[%w_]+%s*=%s*.-;") do
          print("MCO_Start_S:",s);
          local key,value = string.match(s, "(.-)%s*=%s*(.-);");
          print("MCO_Start_KEY:",key);
          print("MCO_Start_Value:",value);
          iter[key] = value;
      end
      return true;
  else
      return false;
  end
  
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

_MCO.Open("McoBoard0","115200,n,8,1");
  local cmd = "ver";
  local data = mco_device.SendReceive(_MCO.DeviceName,cmd.."\r\n",1000);
  print("data:",data);
_MCO.Close();

return _MCO