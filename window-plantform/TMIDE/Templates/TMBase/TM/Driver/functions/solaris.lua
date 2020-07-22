--- solaris Function

local solaris_module = {}

local HWIO = require("hw.HWIO")
local MCU = require("hw.MCU")
local global_data = require("utils.global_data") -- global data mangement library
local sequence_utils = require("utils.sequence_utils")
local time_utils = require("utils.time_utils")
local relay = require("functions.relay").relay_from_connections
local dmm = require("functions.dmm").dmm
local disconnect = require("functions.relay").disconnect_from_connections


function solaris_module.dmm(sequence, global_var_table)
  local solaris_out="Y0"
  local solaris_in =""
  local solaris_bus=""
  local solaris_ic =""
  local result=""
  local cmd1=""
  local cmd2=""
  local arry={}
  time_utils.delay(100)
  relay(HWIO.RelayTable.MUX_RESET_L) 
  if (sequence.param1) then
        --net_name=sequence.param1
       if (HWIO.MeasureTable[sequence.param1].ARR) then
             arry=HWIO.MeasureTable[sequence.param1].ARR
             for k,v in pairs(arry) do 
                 if (v[1]=="X") then
                      solaris_in=v[2]
                 elseif(v[1]=="BUS") then
                        solaris_bus=v[2]
                 elseif(v[1]=="IC") then
                         solaris_ic=v[2]
                end
            end
            cmd1=string.format("switch matrix set(%s,%s%s%s=1)",solaris_bus,solaris_ic,solaris_in,solaris_out)
            ret1=MCU.InstrumentCmd(cmd1)
            result=dmm(sequence, global_var_table)
            time_utils.delay(100)
            cmd2=string.format("switch matrix set(%s,%s%s%s=0)",solaris_bus,solaris_ic,solaris_in,solaris_out)
            MCU.InstrumentCmd(cmd2)            
        return result
      else
         return dmm(sequence, global_var_table)
      end
  else
    return dmm(sequence, global_var_table)
  end
  
end
function solaris_module.solaris_disconnect(point)
  local cmd=""
  local solaris_out=point
  time_utils.delay(100)
  relay(HWIO.RelayTable.MUX_RESET_L) 
  for t=0,2 do
   for i=0,7 do 
      
        for j=0, 11 do
          if (t==2) and (i>=2) then
              break
          end
         cmd=string.format("switch matrix set(i2c%d,C%dX%d%s=0)",t,i,j,solaris_out)
         print(cmd)
         ret=MCU.InstrumentCmd(cmd)
         time_utils.delay(100)
        end
      end
   end 
 
  return true
end


function solaris_module.loopback(sequence, global_var_table)
  local solaris_out=""
  local solaris_in =""
  local solaris_bus=""
  local solaris_ic =""
  local result=""
  local cmd1=""
  local cmd2=""
  local arry={}
  time_utils.delay(100)
  relay(HWIO.RelayTable.MUX_RESET_L) 
  if (sequence.param1) then
       solaris_out=sequence.param1
  else
     return "--FAIL--NO valid param1"
  end
  if (sequence.param2)  then
      if (string.upper(sequence.param2)=="DISCONNECT") then
                ret=solaris_module.solaris_disconnect(sequence.param1)
                return ret
      end
      str_testpoint=sequence.param2
      test_table={}
      start_num=1
       while(1) do

            end_num=string.find(str_testpoint,";")
            if (end_num==nil) and (str_testpoint) then
                table.insert(test_table,str_testpoint)
                break
            end
            str=string.sub(str_testpoint,start_num,end_num-1)
            table.insert(test_table,str)
            str_testpoint=string.sub(str_testpoint,end_num+1,string.len(str_testpoint))
       end
       for k,v in pairs(test_table) do
        if (HWIO.MeasureTable[v].ARR) then
             arry=HWIO.MeasureTable[v].ARR
             for k,v in pairs(arry) do 
                 if (v[1]=="X") then
                      solaris_in=v[2]
                 elseif(v[1]=="BUS") then
                        solaris_bus=v[2]
                 elseif(v[1]=="IC") then
                         solaris_ic=v[2]
                end
            end
            cmd1=string.format("switch matrix set(%s,%s%s%s=0)",solaris_bus,solaris_ic,solaris_in,solaris_out)
            ret1=MCU.InstrumentCmd(cmd1)
            cmd2=string.format("switch matrix set(%s,%s%s%s=1)",solaris_bus,solaris_ic,solaris_in,solaris_out)
            ret2=MCU.InstrumentCmd(cmd2)
        end
      end
      return true
  else
    return "--FAIL--NO valid param2"
  end
end


function solaris_module.digipot(sequence, global_var_table)

  local solaris_out="Y4"
  local solaris_in =""
  local solaris_bus=""
  local solaris_ic =""
  local result=""
  local cmd1=""
  local cmd2=""
  local arry={}
  time_utils.delay(100)
  relay(HWIO.RelayTable.MUX_RESET_L) 
  if (sequence.param1)  then
         if (string.upper(sequence.param1)=="DISCONNECT") then
                ret=solaris_module.solaris_disconnect(solaris_out)
                return ret
         end
        if (HWIO.MeasureTable[sequence.param1].ARR) then
             arry=HWIO.MeasureTable[sequence.param1].ARR
             for k,v in pairs(arry) do 
                 if (v[1]=="X") then
                      solaris_in=v[2]
                 elseif(v[1]=="BUS") then
                        solaris_bus=v[2]
                 elseif(v[1]=="IC") then
                         solaris_ic=v[2]
                end
            end
            cmd1=string.format("switch matrix set(%s,%s%s%s=0)",solaris_bus,solaris_ic,solaris_in,solaris_out)
            ret1=MCU.InstrumentCmd(cmd1)
            time_utils.delay(100)
            cmd2=string.format("switch matrix set(%s,%s%s%s=1)",solaris_bus,solaris_ic,solaris_in,solaris_out)
            ret2=MCU.InstrumentCmd(cmd2)
           return true
        end
  else
    return "--FAIL--NO valid param1"
  end
end


function solaris_module.soladc(sequence, global_var_table)
  local solaris_out1="Y5"
  local solaris_out2="Y6"
  local solaris_in1 =""
  local solaris_bus1=""
  local solaris_ic1 =""
  local solaris_in2 =""
  local solaris_bus2=""
  local solaris_ic2 =""
  local result=""
  local cmd1=""
  local cmd2=""
  local arry={}
  time_utils.delay(100)
  relay(HWIO.RelayTable.MUX_RESET_L) 
  if (string.upper(sequence.param1)=="DISCONNECT") and (string.upper(sequence.param2)=="DISCONNECT") then
                ret=solaris_module.solaris_disconnect(solaris_out1)
                 ret=solaris_module.solaris_disconnect(solaris_out2)
                return ret
  end
  if (sequence.param1)  then
         
        if (HWIO.MeasureTable[sequence.param1].ARR) then
             arry=HWIO.MeasureTable[sequence.param1].ARR
             for k,v in pairs(arry) do 
                 if (v[1]=="X") then
                      solaris_in=v[2]
                 elseif(v[1]=="BUS") then
                        solaris_bus=v[2]
                 elseif(v[1]=="IC") then
                         solaris_ic=v[2]
                end
            end
            cmd1=string.format("switch matrix set(%s,%s%sY5=0)",solaris_bus,solaris_ic,solaris_in,solaris_out1)
            ret1=MCU.InstrumentCmd(cmd1)
            time_utils.delay(100)
            cmd2=string.format("switch matrix set(%s,%s%sY5=1)",solaris_bus,solaris_ic,solaris_in,solaris_out1)
            ret2=MCU.InstrumentCmd(cmd2)
        end
  else
    return "--FAIL--NO valid param1"
  end
  if (sequence.param2)  then
        
        if (HWIO.MeasureTable[sequence.param2].ARR) then
             arry=HWIO.MeasureTable[sequence.param2].ARR
             for k,v in pairs(arry) do 
                 if (v[1]=="X") then
                      solaris_in=v[2]
                 elseif(v[1]=="BUS") then
                        solaris_bus=v[2]
                 elseif(v[1]=="IC") then
                         solaris_ic=v[2]
                end
            end
            cmd1=string.format("switch matrix set(%s,%s%s%s=0)",solaris_bus,solaris_ic,solaris_in,solaris_out2)
            ret3=MCU.InstrumentCmd(cmd1)
            time_utils.delay(100)
            cmd2=string.format("switch matrix set(%s,%s%s%s=1)",solaris_bus,solaris_ic,solaris_in,solaris_out2)
            ret4=MCU.InstrumentCmd(cmd2)
        end
  else
    return "--FAIL--NO valid param2"
  end
  time_utils.delay(100)
  local response = MCU.InstrumentCmd("solaris voltage measure()")
  local voltage = string.match(response,"ACK%s*%((.-) mV;")
    print("soladc voltage:"..voltage)
    result = sequence_utils.convert_units(voltage, "mV", sequence.unit)    
    solaris_module.solaris_disconnect(solaris_out1)
    time_utils.delay(100)
    solaris_module.solaris_disconnect(solaris_out2)
  return result
end

function solaris_module.soldac(sequence, global_var_table)
  local solaris_out="Y7"
  local solaris_in =""
  local solaris_bus=""
  local solaris_ic =""
  local result=""
  local cmd1=""
  local cmd2=""
  local arry={}
  
  if (sequence.param1)  then
        if (HWIO.MeasureTable[sequence.param1].ARR) then
             arry=HWIO.MeasureTable[sequence.param1].ARR
             for k,v in pairs(arry) do 
                 if (v[1]=="X") then
                      solaris_in=v[2]
                 elseif(v[1]=="BUS") then
                        solaris_bus=v[2]
                 elseif(v[1]=="IC") then
                         solaris_ic=v[2]
                end
            end
           -- cmd1=string.format("switch matrix set(%s,%s%s%s=0)",solaris_bus,solaris_ic,solaris_in,solaris_out)
           -- MCU.InstrumentCmd(cmd1)
           -- cmd2=string.format("switch matrix set(%s,%s%s%s=1)",solaris_bus,solaris_ic,solaris_in,solaris_out)
           -- MCU.InstrumentCmd(cmd2)
        end
  else
    return "--FAIL--NO valid param1"
  end
end


    



--===========================--
-- Export dmm to module users
--===========================--
-- dmmVersionCheck()

return solaris_module
