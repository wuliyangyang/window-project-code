--- DMM Function
-- @module functions.dmm
-- @alias dmm_module
local dmm_module = {}

local HWIO = require("hw.HWIO")
local MCU = require("hw.MCU")
local global_data = require("utils.global_data") -- global data mangement library
local sequence_utils = require("utils.sequence_utils")
local time_utils = require("utils.time_utils")
local relay = require("functions.relay").relay_from_IO_only_item

--===========================--
--- Private Functions
-- @section private
--===========================--
dmm_module.factor={
  LowCurr = {GAIN=1, OFFSET=0},
  HighCurr = {GAIN=1, OFFSET=0},
  Volt = {GAIN=1, OFFSET=0},
  AI = {GAIN=1, OFFSET=0},
}


local function dmm_current(net_name, read_current_in_ua)
    local f = {GAIN=1,OFFSET=0}
    if read_current_in_ua then
        net_name = net_name .. "_UA"
        MCU.InstrumentCmd("dmm set(100 uA)")
        f.GAIN = dmm_module.factor.LowCurr.GAIN
        f.OFFSET = dmm_module.factor.LowCurr.OFFSET
    else
        net_name = net_name .. "_MA"
        MCU.InstrumentCmd("dmm set(2 mA)")
        f.GAIN = dmm_module.factor.HighCurr.GAIN
        f.OFFSET = dmm_module.factor.HighCurr.OFFSET
    end
    if f.GAIN==nil or f.OFFSET==nil then
      f.GAIN = 1
      f.OFFSET = 0
    end
    -- print("DMM Current Gain Offer", f.GAIN, f.OFFSET)
    relay(HWIO.DMMCurrentTable[net_name])
    time_utils.delay(50)

    local response = MCU.InstrumentCmd("dmm read(curr)")
    local current = string.match(response,"ACK%s*%((.-)uA;")
    relay(HWIO.DMMCurrentTable["DISCONNECT"])
    if current == nil then
        error("ERRCODE[-2]Bad response from the MCU: "..tostring(response))
    end

    return f.GAIN * current + f.OFFSET
end

--- Get voltage value from DMM (in mV)
-- @param net_name the net to measure
-- @return voltage in mV
function dmm_module.dmm_voltage(net_name)
  local f = {GAIN=1,OFFSET=0}
  relay(HWIO.MeasureTable[net_name])
  local ai_channel = HWIO.MeasureTable[net_name].CH --this need to support AI measure
  local ai_gnd =  HWIO.MeasureTable[net_name].GND --this need to support AI measure
  local ai_hw_gain =  HWIO.MeasureTable[net_name].HW_GAIN --this need to support AI measure
  
  if(ai_gnd) then
      if(HWIO.DMMSwitchGNDTable[ai_gnd] == nil) then 
          return error("ERRCODE[-3]Bad HWIO table. Need fix the GND remark.") 
      else    
          if(ai_gnd == "BKLT") then
              time_utils.delay(1000)
          end    
          relay(HWIO.DMMSwitchGNDTable[ai_gnd])
      end
  end  

  if(ai_channel) then
      relay(HWIO.AISwtichTable[ai_channel])
      f.GAIN = HWIO.MeasureTable[net_name].GAIN--dmm_module.factor.AI.GAIN
      f.OFFSET = HWIO.MeasureTable[net_name].OFFSET --Unit is "mV"
  else
      f.GAIN = dmm_module.factor.Volt.GAIN
      f.OFFSET = dmm_module.factor.Volt.OFFSET
  end

  if f.GAIN==nil or f.OFFSET==nil then
      f.GAIN = 1
      f.OFFSET = 0
  end

  time_utils.delay(50)
  local response = MCU.InstrumentCmd("dmm measure(volt)")
  local voltage = string.match(response,"ACK%s*%((.-)mv;")
  relay(HWIO.MeasureTable["DISCONNECT"]) 
  if(voltage == nil) then error("ERRCODE[-3]Bad response from the MCU: "..tostring(response)) end
  
  if(tonumber(ai_hw_gain) ~= nil) then voltage = tonumber(voltage)*tonumber(ai_hw_gain) end

  return  f.GAIN * voltage  + f.OFFSET --Unit is "mV"
end 

--===========================--
--- Public API
-- @section public
--===========================--

--- Use the DMM to read current or voltage for a net
-- @tparam sequence_object sequence FCT test item table
-- @param global_var_table table full of "global" test variables
-- @return value read from the DMM in the correct units
-- @return passing status (true if unit is within limits, false otherwise)
function dmm_module.dmm(sequence, global_var_table)
    local result = nil
    if(sequence.param1 == "DISCONNECT") then
        relay(HWIO.MeasureTable["DISCONNECT"])
        return true, true
    end
    if sequence.unit and  (string.upper(sequence.unit) == "MV" or 
            string.upper(sequence.unit) == "V" or string.upper(sequence.unit) == "UV") then
        dmmv = dmm_module.dmm_voltage(sequence.param1)
        result = sequence_utils.convert_units(dmmv, "mV", sequence.unit)

    elseif sequence.unit and  (string.upper(sequence.unit) == "MA" or 
            string.upper(sequence.unit) == "A" or string.upper(sequence.unit) == "UA") then
        dmmc = dmm_current(sequence.param1, string.upper(sequence.unit) == "UA")
        result = sequence_utils.convert_units(dmmc, "ua", sequence.unit)
    else
        error("ERRCODE[-4]Invalid Measurement Unit: "..tostring(sequence.unit))
    end

    -- set global variables if they exist
    global_data.set_from_param(global_var_table, sequence.param2, result)

    -- Value, unit-passed
    return result, sequence_utils.check_numerical_limits(result, sequence)
end


--===========================--
-- Export dmm to module users
--===========================--
-- dmmVersionCheck()

return dmm_module
