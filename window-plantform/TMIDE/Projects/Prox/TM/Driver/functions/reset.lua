--- Reset Function
-- @module functions.reset
-- @alias reset_module

local MCU = require("hw.MCU")
local HWIO = require("hw.HWIO")
local time_utils = require("utils.time_utils")
local relay = require("functions.relay").relay_from_connections
local disconnect = require("functions.relay").disconnect_from_connections
local led = require("functions.relay").relay
--===========================--
-- Reset exported module
--===========================--
local reset_module = {}
-- function reset_module.reset(sequence)
-- function reset_module.reset_raw()

--===========================--
-- Public Reset Functions
------------------------------

-- FIXME: Maybe encapsulate this better.
function reset_module.reset_raw()
disconnect(HWIO.RelayTable.MUX_RESET_L) 
relay(HWIO.RelayTable.MUX_RESET_L) 
relay(HWIO.RelayTable.DIGIPOT_RESET) 
relay(HWIO.RelayTable.SOL_DAC) 
disconnect(HWIO.EloadTable)               --152,151,150,149,82=0,0,0,0,0
relay(HWIO.RelayTable.CTRLMUX.POWER_DIS)         --148 = 1
--disconnect(HWIO.RelayTable.SEAJAY_BOX)    --126,134 = 0,0
disconnect(HWIO.RelayTable.PP_VDD_MAIN)   --90,88 = 0,0
disconnect(HWIO.RelayTable.PP_VRECT)      --91,131,130,129=0,1,0,0
disconnect(HWIO.DMMCurrentTable)               --84,85,86,87=0,0,0,1
disconnect(HWIO.RelayTable.PP_BATT_VCC)   -- 89,87 = 0,0

MCU.RawInstrumentCmd("dac set(A,0)")
MCU.RawInstrumentCmd("dac set(B,0)")
MCU.RawInstrumentCmd("dac set(C,0)")
MCU.RawInstrumentCmd("dac set(D,0)")
MCU.RawInstrumentCmd("resistor set(0)")
time_utils.delay(800)


disconnect(HWIO.RelayTable.SOCKET_I2C_WP)                --81 = 0
-- disconnect(HWIO.RelayTable.ELOAD_CTRL)             --82 Only for debug use.

relay(HWIO.RelayTable.DUT_ARM_UART)           --83 = 1


disconnect(HWIO.MeasureTable)               --92,93,94,95,97,98,99,100,101,113,114,115,116,117,118,119,120,121=0
disconnect(HWIO.RelayTable.SEAJAY_BOX)               --145,146,147=0,0,0

disconnect(HWIO.THDNFrequencyTable)               --165,166,167,168=0,0,0,0
disconnect(HWIO.RelayTable.POWER_SEQUENCE_TRIGGER)                --169 = 0
--disconnect(HWIO.RelayTable.AP_BI_GG_TO_PMU_HDQ_CONN) --171,172=0,0
--disconnect(HWIO.RelayTable.PP4V8_HV_ANODE)               --177,178,179=0,0,0
--disconnect(HWIO.RelayTable.PP2V8_LV_ANODE)               --180,185,86=0,0,0
--disconnect(HWIO.RelayTable.PLAT_TIA01)               --181,182=0,0

--relay(HWIO.RelayTable.MIPI_OVP) 
time_utils.delay(1)
--disconnect(HWIO.RelayTable.MIPI_OVP)                --189
--disconnect(HWIO.RelayTable.CRICKET_UART_EN)                --190 = 0
disconnect(HWIO.RelayTable.CTRLMUX)         --148 = 0
--disconnect(HWIO.RelayTable.AP_FORCE_DFU) 
disconnect(HWIO.RelayTable.UUT_HUB) 

  --------------------------------------------------------------------------------------------------------
  --------------------------------------------------------------------------------------------------------
 led({param1="LED_CTRL",param2="LIGHT_BLUE"})
  return true, true;
end

--- NOT IMPLEMENTED: Reset the fixture to its default state
-- @tparam sequence_object sequence FCT test sequence table
-- @param global_var_table table full of "global" test variables
-- @return result (0)
-- @return passing status (true)
-- @raise error
function reset_module.reset(sequence, global_var_table)
  return reset_module.reset_raw(), true
end

-----------------------------------
-- Export eload to module users
-----------------------------------
return reset_module
