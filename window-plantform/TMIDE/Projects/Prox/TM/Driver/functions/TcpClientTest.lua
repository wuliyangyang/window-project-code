print("< ".."instrument.MCO > Load mco.lua!!!!!!....")

local _MCO = {}
-- local time_utils = require("utils.time_utils")
local mco_device = require("ZmqTcpClient");
local iter ={};
_MCO.DeviceName = "MCO_TCP"--..tostring(args.uut);
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

_MCO.Open("127.0.0.1:7600");
  local cmd = "ver";
  local data = mco_device.SendReceive(_MCO.DeviceName,cmd.."\r\n",1000);
  print("data:",data);

local ztimer = require("lzmq.timer")
while true do
  ztimer.sleep(1000);
  -- print(mco_device.ReadString(_MCO.DeviceName));
  print(mco_device.SendReceive(_MCO.DeviceName,"sdfgh",5000));
end
-- _MCO.Close();

return _MCO