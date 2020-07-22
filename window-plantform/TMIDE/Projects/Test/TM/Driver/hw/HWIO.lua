---------------------History Record---------------------

-- 20161120   Create first version A1.0. Use A1.0 control table from peter.
-- 20161219   Change style
-- 20170107   P0 at china build.

--------------------------------------------------------
local HWIO = {}

HWIO.critical = "55, 56, 63, 65, 69, 71, 76, 95, 126, 133, 134, 135, 142, 308, 310, 324, 326"

HWIO.MeasureTable = {
           -- Connect to channel board TP voltage
           --    channel 1 
                    NA1                         = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 0 }, { 53, 0 }, { 52, 0 }, }, CH = "AI0", GND = "FIXTURE", HW_GAIN = nil,}, 
                    NA2                         = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 0 }, { 53, 0 }, { 52, 1 }, }, CH = "AI0", GND = "FIXTURE", HW_GAIN = nil,}, 
                    IMAC_PWR_BTN_R              = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 0 }, { 53, 1 }, { 52, 0 }, }, CH = "AI0", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PSU_P12V_EN_DIV             = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 0 }, { 53, 1 }, { 52, 1 }, }, CH = "AI0", GND = "FIXTURE", HW_GAIN = "3",}, 
                    PSU_P12V_EN_L               = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 0 }, { 53, 1 }, { 52, 1 }, }, CH = "AI0", GND = "FIXTURE", HW_GAIN = "3",}, 
                    PSU_P12V_EN_DIVIDE          = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 0 }, { 53, 1 }, { 52, 1 }, }, CH = "AI0", GND = "FIXTURE", HW_GAIN = "3",}, 
                    NA3                         = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 1 }, { 53, 0 }, { 52, 0 }, }, CH = "AI0", GND = "FIXTURE", HW_GAIN = nil,}, 
                    NA4                         = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 1 }, { 53, 0 }, { 52, 1 }, }, CH = "AI0", GND = "FIXTURE", HW_GAIN = nil,}, 
                    VCCP_12V_DIV                = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 1 }, { 53, 1 }, { 52, 0 }, }, CH = "AI0", GND = "FIXTURE", HW_GAIN = "3",}, 
                    VCCN_12V_DIV                = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 1 }, { 53, 1 }, { 52, 1 }, }, CH = "AI0", GND = "FIXTURE", HW_GAIN = "3",}, 

                    PP12V_BKLT_OUT              = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 0 }, { 53, 0 }, { 52, 0 }, }, CH = "AI1", GND = "FIXTURE", HW_GAIN = "0.0102183931",}, 
                    PP12V_BKLT_FUSED_CURRENT    = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 0 }, { 53, 0 }, { 52, 0 }, }, CH = "AI1", GND = "FIXTURE", HW_GAIN = "0.0040322580",}, 
                    ACDC_ID_R                   = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 0 }, { 53, 0 }, { 52, 1 }, }, CH = "AI1", GND = "FIXTURE", HW_GAIN = nil,}, 
                    TSNS_ACDC_P                 = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 0 }, { 53, 1 }, { 52, 0 }, }, CH = "AI1", GND = "FIXTURE", HW_GAIN = nil,}, 
                    TSNS_ACDC_N                 = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 0 }, { 53, 1 }, { 52, 1 }, }, CH = "AI1", GND = "FIXTURE", HW_GAIN = nil,}, 
                    VCCP_5V_DIV                 = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 1 }, { 53, 0 }, { 52, 0 }, }, CH = "AI1", GND = "FIXTURE", HW_GAIN = "3",}, 
                    NA5                         = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 1 }, { 53, 0 }, { 52, 1 }, }, CH = "AI1", GND = "FIXTURE", HW_GAIN = nil,}, 
                    SOC_SWD_MUX_SEL             = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 1 }, { 53, 1 }, { 52, 0 }, }, CH = "AI1", GND = "FIXTURE", HW_GAIN = nil,}, 
                    BLC_EN                      = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 1 }, { 53, 1 }, { 52, 1 }, }, CH = "AI1", GND = "FIXTURE", HW_GAIN = nil,}, 

                    PP12V_S0_FAN_0_DIV          = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 0 }, { 53, 0 }, { 52, 0 }, }, CH = "AI2", GND = "FIXTURE", HW_GAIN = "3",}, 
                    PP12V_S0_FAN_1_DIV          = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 0 }, { 53, 0 }, { 52, 1 }, }, CH = "AI2", GND = "FIXTURE", HW_GAIN = "3",}, 
                    VDD1P5                      = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 0 }, { 53, 1 }, { 52, 0 }, }, CH = "AI2", GND = "FIXTURE", HW_GAIN = nil,}, 
                    VDD1P2                      = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 0 }, { 53, 1 }, { 52, 1 }, }, CH = "AI2", GND = "FIXTURE", HW_GAIN = nil,}, 
                    NA6                         = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 1 }, { 53, 0 }, { 52, 0 }, }, CH = "AI2", GND = "FIXTURE", HW_GAIN = "3",}, 
                    NA7                         = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 1 }, { 53, 0 }, { 52, 1 }, }, CH = "AI2", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PPMVDD_S0_GPU               = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 1 }, { 53, 1 }, { 52, 0 }, }, CH = "AI2", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PMU_ONOFF_L                 = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 1 }, { 53, 1 }, { 52, 1 }, }, CH = "AI2", GND = "FIXTURE", HW_GAIN = nil,}, 

                    VIN_RFLDO                   = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 0 }, { 53, 0 }, { 52, 0 }, }, CH = "AI3", GND = "FIXTURE", HW_GAIN = nil,}, 
                    DP_INT_PIN_54               = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 0 }, { 53, 0 }, { 52, 1 }, }, CH = "AI3", GND = "FIXTURE", HW_GAIN = nil,}, 
                    VIN_RFLDO                   = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 0 }, { 53, 1 }, { 52, 0 }, }, CH = "AI3", GND = "FIXTURE", HW_GAIN = nil,}, 
                    NA8                         = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 0 }, { 53, 1 }, { 52, 1 }, }, CH = "AI3", GND = "FIXTURE", HW_GAIN = nil,}, 
                    P3V3S5_EN                   = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 1 }, { 53, 0 }, { 52, 0 }, }, CH = "AI3", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PVPP_PVDDQ_EN_CALPE         = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 1 }, { 53, 0 }, { 52, 1 }, }, CH = "AI3", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PVDDQ_PGOOD                 = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 1 }, { 53, 1 }, { 52, 0 }, }, CH = "AI3", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PP3V3_S5                    = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 1 }, { 53, 1 }, { 52, 1 }, }, CH = "AI3", GND = "FIXTURE", HW_GAIN = nil,}, 

                    SOC_USB_VBUS                = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 0 }, { 53, 0 }, { 52, 0 }, }, CH = "AI4", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PMU_ACTIVE_READY            = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 0 }, { 53, 0 }, { 52, 1 }, }, CH = "AI4", GND = "FIXTURE", HW_GAIN = nil,}, 
                    SOC_FORCE_DFU               = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 0 }, { 53, 1 }, { 52, 0 }, }, CH = "AI4", GND = "FIXTURE", HW_GAIN = nil,}, 
                    SOC_DFU_STATUS              = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 0 }, { 53, 1 }, { 52, 1 }, }, CH = "AI4", GND = "FIXTURE", HW_GAIN = nil,}, 
                    NA9                         = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 1 }, { 53, 0 }, { 52, 0 }, }, CH = "AI4", GND = "FIXTURE", HW_GAIN = nil,}, 
                    NA10                        = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 1 }, { 53, 0 }, { 52, 1 }, }, CH = "AI4", GND = "FIXTURE", HW_GAIN = nil,}, 
                    POWER_CURRENT_P             = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 1 }, { 53, 1 }, { 52, 0 }, }, CH = "AI4", GND = "POWER_CURRENT_N", HW_GAIN = nil,},  
                    ILOAD_A                     = { IO = { { 86, 1 }, { 85, 0 }, { 84, 0 }, { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 1 }, { 53, 1 }, { 52, 0 }, }, CH = "AI4", GND = "POWER_CURRENT_N", HW_GAIN = nil,},  
                    ILOAD_B                     = { IO = { { 86, 1 }, { 85, 0 }, { 84, 1 }, { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 1 }, { 53, 1 }, { 52, 0 }, }, CH = "AI4", GND = "POWER_CURRENT_N", HW_GAIN = nil,},  
                    ILOAD_C                     = { IO = { { 86, 1 }, { 85, 1 }, { 84, 0 }, { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 1 }, { 53, 1 }, { 52, 0 }, }, CH = "AI4", GND = "POWER_CURRENT_N", HW_GAIN = nil,},  
                    ILOAD_D                     = { IO = { { 86, 1 }, { 85, 1 }, { 84, 1 }, { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 1 }, { 53, 1 }, { 52, 0 }, }, CH = "AI4", GND = "POWER_CURRENT_N", HW_GAIN = nil,},  
                    NA11                        = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 1 }, { 53, 1 }, { 52, 1 }, }, CH = "AI4", GND = "FIXTURE", HW_GAIN = nil,}, 

                    SOC_DOCK_CONNECT            = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 0 }, { 53, 0 }, { 52, 0 }, }, CH = "AI5", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PP12V_DIV                   = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 0 }, { 53, 0 }, { 52, 1 }, }, CH = "AI5", GND = "FIXTURE", HW_GAIN = "3.2",}, 
                    PP12V_DIVIDE                = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 0 }, { 53, 0 }, { 52, 1 }, }, CH = "AI5", GND = "DUT",     HW_GAIN = "3.2",}, 
                    PP12V_ACDC                  = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 0 }, { 53, 0 }, { 52, 1 }, }, CH = "AI5", GND = "DUT",     HW_GAIN = "3.2",}, 
                    PP11V_DIV                   = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 0 }, { 53, 1 }, { 52, 0 }, }, CH = "AI5", GND = "FIXTURE", HW_GAIN = "3.2",}, 
                    PP11V_ACDC                  = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 0 }, { 53, 1 }, { 52, 0 }, }, CH = "AI5", GND = "DUT",     HW_GAIN = "3.2",}, 
                    PP12V_GPU                   = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 0 }, { 53, 1 }, { 52, 1 }, }, CH = "AI5", GND = "FIXTURE", HW_GAIN = "3.2",}, 
                    PP12V_GPU_DIV               = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 0 }, { 53, 1 }, { 52, 1 }, }, CH = "AI5", GND = "FIXTURE", HW_GAIN = "3.2",}, 
                    PPVDDCPU_AWAKE_SOC          = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 1 }, { 53, 0 }, { 52, 0 }, }, CH = "AI5", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PP1V8_S5                    = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 1 }, { 53, 0 }, { 52, 1 }, }, CH = "AI5", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PP1V0_S0                    = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 1 }, { 53, 1 }, { 52, 0 }, }, CH = "AI5", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PP3V3_S0                    = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 1 }, { 53, 1 }, { 52, 1 }, }, CH = "AI5", GND = "FIXTURE", HW_GAIN = nil,}, 

                    PP3V3_G3H                   = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 0 }, { 53, 0 }, { 52, 0 }, }, CH = "AI6", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PP3V3_G3W                   = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 0 }, { 53, 0 }, { 52, 1 }, }, CH = "AI6", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PP3V3_G3S                   = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 0 }, { 53, 1 }, { 52, 0 }, }, CH = "AI6", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PP1V8_SLPS2R                = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 0 }, { 53, 1 }, { 52, 1 }, }, CH = "AI6", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PPVCCIN_S0                  = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 1 }, { 53, 0 }, { 52, 0 }, }, CH = "AI6", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PPVCORE_S0_GPU              = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 1 }, { 53, 0 }, { 52, 1 }, }, CH = "AI6", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PPVCORE_S0                  = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 1 }, { 53, 0 }, { 52, 1 }, }, CH = "AI6", GND = "FIXTURE", HW_GAIN = nil,}, 
                    NA12                        = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 1 }, { 53, 1 }, { 52, 0 }, }, CH = "AI6", GND = "FIXTURE", HW_GAIN = nil,}, 
                    SOC_SOCHOT_L                = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 1 }, { 53, 1 }, { 52, 1 }, }, CH = "AI6", GND = "FIXTURE", HW_GAIN = nil,}, 

                    PP0V82_SLPDDR               = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 0 }, { 53, 0 }, { 52, 0 }, }, CH = "AI7", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PP1V2_AWAKE                 = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 0 }, { 53, 0 }, { 52, 1 }, }, CH = "AI7", GND = "FIXTURE", HW_GAIN = nil,}, 
                    NA13                        = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 0 }, { 53, 1 }, { 52, 0 }, }, CH = "AI7", GND = "DUT",     HW_GAIN = nil,}, 
                    NA14                        = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 0 }, { 53, 1 }, { 52, 1 }, }, CH = "AI7", GND = "DUT",     HW_GAIN = nil,}, 
                    DFUMUX_SEL                  = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 1 }, { 53, 0 }, { 52, 0 }, }, CH = "AI7", GND = "FIXTURE", HW_GAIN = nil,}, 
                    BKLT_VIN_N                  = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 1 }, { 53, 0 }, { 52, 1 }, }, CH = "AI7", GND = "FIXTURE", HW_GAIN = nil,}, 
                    BKLT_VIN_P                  = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 1 }, { 53, 1 }, { 52, 0 }, }, CH = "AI7", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PP12V_BKLT_DIV              = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 1 }, { 53, 1 }, { 52, 1 }, }, CH = "AI7", GND = "FIXTURE", HW_GAIN = "3",}, 
                    PP12V_BKLT_FUSED_VOLT       = { IO = { { 55, 1 }, { 118, 0 }, { 117, 0 }, { 54, 1 }, { 53, 1 }, { 52, 1 }, }, CH = "AI7", GND = "FIXTURE", HW_GAIN = "3",}, 




              --channel 2
                    PCH_PM_SLP_S0_L             = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 0 }, { 53, 0 }, { 52, 0 }, }, CH = "AI0", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PCH_THRMTRIP_L              = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 0 }, { 53, 0 }, { 52, 1 }, }, CH = "AI0", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PLT_RESET_L                 = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 0 }, { 53, 1 }, { 52, 0 }, }, CH = "AI0", GND = "FIXTURE", HW_GAIN = nil,}, 
                    SMC_PCH_SYS_PWROK           = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 0 }, { 53, 1 }, { 52, 1 }, }, CH = "AI0", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PPVCCSA_S0                  = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 1 }, { 53, 0 }, { 52, 0 }, }, CH = "AI0", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PP1V8_S0                    = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 1 }, { 53, 0 }, { 52, 1 }, }, CH = "AI0", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PPVDDQ01_S3                 = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 1 }, { 53, 1 }, { 52, 0 }, }, CH = "AI0", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PPVDDQ23_S3                 = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 1 }, { 53, 1 }, { 52, 1 }, }, CH = "AI0", GND = "FIXTURE", HW_GAIN = nil,}, 

                    SMC_PCH_PWROK               = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 0 }, { 53, 0 }, { 52, 0 }, }, CH = "AI1", GND = "FIXTURE", HW_GAIN = nil,}, 
                    SMC_SYSRST_L                = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 0 }, { 53, 0 }, { 52, 1 }, }, CH = "AI1", GND = "FIXTURE", HW_GAIN = nil,}, 
                    NA15                        = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 0 }, { 53, 1 }, { 52, 0 }, }, CH = "AI1", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PCH_SRTCRST_PCH_L           = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 0 }, { 53, 1 }, { 52, 1 }, }, CH = "AI1", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PP1V0_PCH_CORE_S5           = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 1 }, { 53, 0 }, { 52, 0 }, }, CH = "AI1", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PP1V8_S0_GPU                = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 1 }, { 53, 0 }, { 52, 1 }, }, CH = "AI1", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PP3V3_G3H_ENET_FET          = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 1 }, { 53, 1 }, { 52, 0 }, }, CH = "AI1", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PPVCCIO_S0                  = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 1 }, { 53, 1 }, { 52, 1 }, }, CH = "AI1", GND = "FIXTURE", HW_GAIN = nil,}, 
                  
                    PCH_PROCPWRGD               = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 0 }, { 53, 0 }, { 52, 0 }, }, CH = "AI2", GND = "FIXTURE", HW_GAIN = nil,}, 
                    VDDQ_AB_PWRGD_BUFFER        = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 0 }, { 53, 0 }, { 52, 1 }, }, CH = "AI2", GND = "FIXTURE", HW_GAIN = nil,}, 
                    VDDQ_CD_PWRGD_BUFFER        = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 0 }, { 53, 1 }, { 52, 0 }, }, CH = "AI2", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PGOOD_VDDQ_01_R_INV         = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 0 }, { 53, 1 }, { 52, 1 }, }, CH = "AI2", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PP1V8_G3S                   = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 1 }, { 53, 0 }, { 52, 0 }, }, CH = "AI2", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PP3V3_S0SW_SD               = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 1 }, { 53, 0 }, { 52, 1 }, }, CH = "AI2", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PP1V2_SD                    = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 1 }, { 53, 1 }, { 52, 0 }, }, CH = "AI2", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PP3V3_S0_TBT                = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 1 }, { 53, 1 }, { 52, 1 }, }, CH = "AI2", GND = "FIXTURE", HW_GAIN = nil,}, 

                    PGOOD_VDDQ_23_R_INV         = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 0 }, { 53, 0 }, { 52, 0 }, }, CH = "AI3", GND = "FIXTURE", HW_GAIN = nil,}, 
                    CPU_PWRGD                   = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 0 }, { 53, 0 }, { 52, 1 }, }, CH = "AI3", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PMU_COLD_RESET_L            = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 0 }, { 53, 1 }, { 52, 0 }, }, CH = "AI3", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PVCCIO_PGOOD                = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 0 }, { 53, 1 }, { 52, 1 }, }, CH = "AI3", GND = "FIXTURE", HW_GAIN = nil,}, 
                    NA16                        = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 1 }, { 53, 0 }, { 52, 0 }, }, CH = "AI3", GND = "FIXTURE", HW_GAIN = nil,}, 
                    NA17                        = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 1 }, { 53, 0 }, { 52, 1 }, }, CH = "AI3", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PP1V2_ENET                  = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 1 }, { 53, 1 }, { 52, 0 }, }, CH = "AI3", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PP2V1_ENET                  = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 1 }, { 53, 1 }, { 52, 1 }, }, CH = "AI3", GND = "FIXTURE", HW_GAIN = nil,}, 

                    PM_S0_PGOOD                 = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 0 }, { 53, 0 }, { 52, 0 }, }, CH = "AI4", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PVCCIO_EN                   = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 0 }, { 53, 0 }, { 52, 1 }, }, CH = "AI4", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PM_PCH_PWROK                = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 0 }, { 53, 1 }, { 52, 0 }, }, CH = "AI4", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PSU12VPGOOD                 = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 0 }, { 53, 1 }, { 52, 1 }, }, CH = "AI4", GND = "FIXTURE", HW_GAIN = nil,}, 
                    CPU_ISNS_VCCIO_S0_OUT       = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 1 }, { 53, 0 }, { 52, 0 }, }, CH = "AI4", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PPVBUS_USBC_XA              = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 1 }, { 53, 0 }, { 52, 1 }, }, CH = "AI4", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PPVBUS_USBC_XB              = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 1 }, { 53, 1 }, { 52, 0 }, }, CH = "AI4", GND = "FIXTURE", HW_GAIN = nil,}, 
                    XDP_CPU_PREQ_L              = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 1 }, { 53, 1 }, { 52, 1 }, }, CH = "AI4", GND = "FIXTURE", HW_GAIN = nil,}, 

                    P1V1_SLPDDR_SOCFET_EN       = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 0 }, { 53, 0 }, { 52, 0 }, }, CH = "AI5", GND = "FIXTURE", HW_GAIN = nil,}, 
                    P1V8G3S_3V3G3S_PGOOD        = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 0 }, { 53, 0 }, { 52, 1 }, }, CH = "AI5", GND = "FIXTURE", HW_GAIN = nil,}, 
                    ESPI_RESET_L                = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 0 }, { 53, 1 }, { 52, 0 }, }, CH = "AI5", GND = "FIXTURE", HW_GAIN = nil,}, 
                    NA18                        = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 0 }, { 53, 1 }, { 52, 1 }, }, CH = "AI5", GND = "FIXTURE", HW_GAIN = nil,},  
                    XDP_CPU_PRDY_L              = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 1 }, { 53, 0 }, { 52, 0 }, }, CH = "AI5", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PP1V0_PCH_IO_S5             = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 1 }, { 53, 0 }, { 52, 1 }, }, CH = "AI5", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PM_RSMRST_PCH_L             = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 1 }, { 53, 1 }, { 52, 0 }, }, CH = "AI5", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PM_PWRBTN_L                 = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 1 }, { 53, 1 }, { 52, 1 }, }, CH = "AI5", GND = "FIXTURE", HW_GAIN = nil,},              
                    
                    PP1V8_GL_SDCONN             = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 0 }, { 53, 0 }, { 52, 0 }, }, CH = "AI6", GND = "FIXTURE", HW_GAIN = nil,}, 
                    NA19                        = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 0 }, { 53, 0 }, { 52, 1 }, }, CH = "AI6", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PP5V_S4_EXTA_F              = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 0 }, { 53, 1 }, { 52, 0 }, }, CH = "AI6", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PP5V_S4_EXTB_F              = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 0 }, { 53, 1 }, { 52, 1 }, }, CH = "AI6", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PM_PCH_SYS_PWROK            = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 1 }, { 53, 0 }, { 52, 0 }, }, CH = "AI6", GND = "FIXTURE", HW_GAIN = nil,}, 
                    XDP_CPU_PWR_DEBUG_L         = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 1 }, { 53, 0 }, { 52, 1 }, }, CH = "AI6", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PM_SYSRST_L                 = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 1 }, { 53, 1 }, { 52, 0 }, }, CH = "AI6", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PM_SLP_SUS_L                = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 1 }, { 53, 1 }, { 52, 1 }, }, CH = "AI6", GND = "FIXTURE", HW_GAIN = nil,},  
                     
                    PP5V_S4_EXTC_F              = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 0 }, { 53, 0 }, { 52, 0 }, }, CH = "AI7", GND = "FIXTURE", HW_GAIN = nil,}, 
                    NA20                        = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 0 }, { 53, 0 }, { 52, 1 }, }, CH = "AI7", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PPVBUS_USBC_TB              = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 0 }, { 53, 1 }, { 52, 0 }, }, CH = "AI7", GND = "FIXTURE", HW_GAIN = nil,},
                    NA21                        = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 0 }, { 53, 1 }, { 52, 1 }, }, CH = "AI7", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PM_SLP_S0_L                 = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 1 }, { 53, 0 }, { 52, 0 }, }, CH = "AI7", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PM_SLP_S3_L                 = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 1 }, { 53, 0 }, { 52, 1 }, }, CH = "AI7", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PM_SLP_S4_L                 = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 1 }, { 53, 1 }, { 52, 0 }, }, CH = "AI7", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PM_SLP_S5_L                 = { IO = { { 55, 1 }, { 118, 0 }, { 117, 1 }, { 54, 1 }, { 53, 1 }, { 52, 1 }, }, CH = "AI7", GND = "FIXTURE", HW_GAIN = nil,},





           --channel 3
                    PM_PGOOD_FET_P3V3_S0        = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 0 }, { 53, 0 }, { 52, 0 }, }, CH = "AI0", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PM_PGOOD_FET_P1V8_SLPS2R    = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 0 }, { 53, 0 }, { 52, 1 }, }, CH = "AI0", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PM_PGOOD_FET_P3V3G3S        = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 0 }, { 53, 1 }, { 52, 0 }, }, CH = "AI0", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PM_PGOOD_FET_P5V_S0         = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 0 }, { 53, 1 }, { 52, 1 }, }, CH = "AI0", GND = "FIXTURE", HW_GAIN = nil,}, 
                    CPU_VR_READY                = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 1 }, { 53, 0 }, { 52, 0 }, }, CH = "AI0", GND = "FIXTURE", HW_GAIN = nil,}, 
                    TP_PM_PGOOD_REG_CPU_VCCSA   = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 1 }, { 53, 0 }, { 52, 1 }, }, CH = "AI0", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PM_PGOOD_VTT01_S0           = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 1 }, { 53, 1 }, { 52, 0 }, }, CH = "AI0", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PM_PGOOD_VTT23_S0           = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 1 }, { 53, 1 }, { 52, 1 }, }, CH = "AI0", GND = "FIXTURE", HW_GAIN = nil,}, 

                    P3V3G3W_PGOOD               = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 0 }, { 53, 0 }, { 52, 0 }, }, CH = "AI1", GND = "FIXTURE", HW_GAIN = nil,}, 
                    REG_P5VG3S_USBC_PGOOD       = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 0 }, { 53, 0 }, { 52, 1 }, }, CH = "AI1", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PM_PGOOD_REG_1V2_ENET       = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 0 }, { 53, 1 }, { 52, 0 }, }, CH = "AI1", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PM_PGOOD_REG_2V1_ENET       = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 0 }, { 53, 1 }, { 52, 1 }, }, CH = "AI1", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PM_PGOOD_REG_CPU_VDDQ_23    = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 1 }, { 53, 0 }, { 52, 0 }, }, CH = "AI1", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PM_PGOOD_REG_CPU_VDDQ_01    = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 1 }, { 53, 0 }, { 52, 1 }, }, CH = "AI1", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PM_PGOOD_REG_VPP01          = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 1 }, { 53, 1 }, { 52, 0 }, }, CH = "AI1", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PM_PGOOD_REG_VPP23          = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 1 }, { 53, 1 }, { 52, 1 }, }, CH = "AI1", GND = "FIXTURE", HW_GAIN = nil,},

                    PM_PGOOD_REG_0V85_ENET      = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 0 }, { 53, 0 }, { 52, 0 }, }, CH = "AI2", GND = "FIXTURE", HW_GAIN = nil,}, 
                    P3V3G3H_PGOOD               = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 0 }, { 53, 0 }, { 52, 1 }, }, CH = "AI2", GND = "FIXTURE", HW_GAIN = nil,}, 
                    P5VG3S_PGOOD                = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 0 }, { 53, 1 }, { 52, 0 }, }, CH = "AI2", GND = "FIXTURE", HW_GAIN = nil,}, 
                    P3V3G3HRTC_PGOOD            = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 0 }, { 53, 1 }, { 52, 1 }, }, CH = "AI2", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PM_PGOOD_REG_GPUVCORE       = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 1 }, { 53, 0 }, { 52, 0 }, }, CH = "AI2", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PM_PGOOD_REG_GPU_MVDD       = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 1 }, { 53, 0 }, { 52, 1 }, }, CH = "AI2", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PM_PGOOD_REG_GPU_VDDCI      = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 1 }, { 53, 1 }, { 52, 0 }, }, CH = "AI2", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PM_PGOOD_REG_GPU_P0V8       = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 1 }, { 53, 1 }, { 52, 1 }, }, CH = "AI2", GND = "FIXTURE", HW_GAIN = nil,}, 

                    PM_DRVCPU_PGOOD             = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 0 }, { 53, 0 }, { 52, 0 }, }, CH = "AI3", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PPMVDD_SO_GPU               = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 0 }, { 53, 0 }, { 52, 1 }, }, CH = "AI3", GND = "FIXTURE", HW_GAIN = nil,}, 
                    P5VG3S_EN                   = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 0 }, { 53, 1 }, { 52, 0 }, }, CH = "AI3", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PP5VG3S_EN                  = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 0 }, { 53, 1 }, { 52, 0 }, }, CH = "AI3", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PMU_COLD_RESET_L            = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 0 }, { 53, 1 }, { 52, 1 }, }, CH = "AI3", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PM_PGOOD_REG_GPU_P1V8       = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 1 }, { 53, 0 }, { 52, 0 }, }, CH = "AI3", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PM_PGOOD_REG_GPU_MVPP       = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 1 }, { 53, 0 }, { 52, 1 }, }, CH = "AI3", GND = "FIXTURE", HW_GAIN = nil,}, 
                    ENET_PWR_EN                 = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 1 }, { 53, 1 }, { 52, 0 }, }, CH = "AI3", GND = "FIXTURE", HW_GAIN = nil,}, 
                    GPU_RESET_L                 = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 1 }, { 53, 1 }, { 52, 1 }, }, CH = "AI3", GND = "FIXTURE", HW_GAIN = nil,}, 

                    SD_PWR_EN                   = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 0 }, { 53, 0 }, { 52, 0 }, }, CH = "AI4", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PM_EN_FET_P3V3_S0           = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 0 }, { 53, 0 }, { 52, 1 }, }, CH = "AI4", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PM_G3S_EN                   = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 0 }, { 53, 1 }, { 52, 0 }, }, CH = "AI4", GND = "FIXTURE", HW_GAIN = nil,}, 
                    TBT_PWR_EN                  = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 0 }, { 53, 1 }, { 52, 1 }, }, CH = "AI4", GND = "FIXTURE", HW_GAIN = nil,}, 
                    NA22                        = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 1 }, { 53, 0 }, { 52, 0 }, }, CH = "AI4", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PM_VCCIO_EN_R               = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 1 }, { 53, 0 }, { 52, 1 }, }, CH = "AI4", GND = "FIXTURE", HW_GAIN = nil,}, 
                    REG_CPU_VDDQ23_EN           = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 1 }, { 53, 1 }, { 52, 0 }, }, CH = "AI4", GND = "FIXTURE", HW_GAIN = nil,}, 
                    REG_CPU_VDDQ01_EN           = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 1 }, { 53, 1 }, { 52, 1 }, }, CH = "AI4", GND = "FIXTURE", HW_GAIN = nil,}, 
                                           
                    PM_PGOOD_0V85_1V2_ENET      = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 0 }, { 53, 0 }, { 52, 0 }, }, CH = "AI5", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PM_EN_FET_P5V_S0            = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 0 }, { 53, 0 }, { 52, 1 }, }, CH = "AI5", GND = "FIXTURE", HW_GAIN = nil,}, 
                    P3V3G3W_EN                  = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 0 }, { 53, 1 }, { 52, 0 }, }, CH = "AI5", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PM_EN_REG_P5V_G3H_USBC      = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 0 }, { 53, 1 }, { 52, 1 }, }, CH = "AI5", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PM_EN_REG_VPP01_R           = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 1 }, { 53, 0 }, { 52, 0 }, }, CH = "AI5", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PM_EN_REG_VPP23_R           = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 1 }, { 53, 0 }, { 52, 1 }, }, CH = "AI5", GND = "FIXTURE", HW_GAIN = nil,}, 
                    IOX_LCD_PWR_EN              = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 1 }, { 53, 1 }, { 52, 0 }, }, CH = "AI5", GND = "FIXTURE", HW_GAIN = nil,}, 
                    BKLT_BL_EN                  = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 1 }, { 53, 1 }, { 52, 1 }, }, CH = "AI5", GND = "FIXTURE", HW_GAIN = nil,},  

                    PM_EN_REG_1V2_ENET_R        = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 0 }, { 53, 0 }, { 52, 0 }, }, CH = "AI6", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PM_EN_REG_2V1_ENET_R        = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 0 }, { 53, 0 }, { 52, 1 }, }, CH = "AI6", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PM_EN_REG_0V85_ENET_R       = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 0 }, { 53, 1 }, { 52, 0 }, }, CH = "AI6", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PM_EN_REG_P3V3_G3H_R        = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 0 }, { 53, 1 }, { 52, 1 }, }, CH = "AI6", GND = "FIXTURE", HW_GAIN = nil,}, 
                    VP_GPU_ENABLE               = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 1 }, { 53, 0 }, { 52, 0 }, }, CH = "AI6", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PM_EN_REG_GPU_MVDD          = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 1 }, { 53, 0 }, { 52, 1 }, }, CH = "AI6", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PM_EN_REG_GPU_VDDCT         = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 1 }, { 53, 1 }, { 52, 0 }, }, CH = "AI6", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PM_EN_REG_GPU_P0V8          = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 1 }, { 53, 1 }, { 52, 1 }, }, CH = "AI6", GND = "FIXTURE", HW_GAIN = nil,}, 

                    PM_EN_REG_P5V_G3S_R         = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 0 }, { 53, 0 }, { 52, 0 }, }, CH = "AI7", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PM_EN_P3V3_G3H_RTC          = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 0 }, { 53, 0 }, { 52, 1 }, }, CH = "AI7", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PM_S0_EN                    = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 0 }, { 53, 1 }, { 52, 0 }, }, CH = "AI7", GND = "FIXTURE", HW_GAIN = nil,},
                    REG_CPU_VCCIN_EN            = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 0 }, { 53, 1 }, { 52, 1 }, }, CH = "AI7", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PM_EN_REG_GPU_P1V8          = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 1 }, { 53, 0 }, { 52, 0 }, }, CH = "AI7", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PM_EN_REG_GPU_MVPP          = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 1 }, { 53, 0 }, { 52, 1 }, }, CH = "AI7", GND = "FIXTURE", HW_GAIN = nil,}, 
                    SSD_PMU_RESET_L             = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 1 }, { 53, 1 }, { 52, 0 }, }, CH = "AI7", GND = "FIXTURE", HW_GAIN = nil,}, 
                    NA23                        = { IO = { { 55, 1 }, { 118, 1 }, { 117, 0 }, { 54, 1 }, { 53, 1 }, { 52, 1 }, }, CH = "AI7", GND = "FIXTURE", HW_GAIN = nil,},    
                              


                --channel 4
                    NA24                        = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 0 }, { 53, 0 }, { 52, 0 }, }, CH = "AI0", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PPVDDCPUSRAM_AWAKE          = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 0 }, { 53, 0 }, { 52, 1 }, }, CH = "AI0", GND = "FIXTURE", HW_GAIN = nil,}, 
                    CPU_PROCHOT_L               = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 0 }, { 53, 1 }, { 52, 0 }, }, CH = "AI0", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PCH_RTC_RESET_L             = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 0 }, { 53, 1 }, { 52, 1 }, }, CH = "AI0", GND = "FIXTURE", HW_GAIN = nil,}, 
                    NA25                        = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 1 }, { 53, 0 }, { 52, 0 }, }, CH = "AI0", GND = "FIXTURE", HW_GAIN = nil,}, 
                    NA26                        = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 1 }, { 53, 0 }, { 52, 1 }, }, CH = "AI0", GND = "FIXTURE", HW_GAIN = nil,}, 
                    NA27                        = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 1 }, { 53, 1 }, { 52, 0 }, }, CH = "AI0", GND = "FIXTURE", HW_GAIN = nil,}, 
                    NA28                        = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 1 }, { 53, 1 }, { 52, 1 }, }, CH = "AI0", GND = "FIXTURE", HW_GAIN = nil,},

                    NA29                        = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 0 }, { 53, 0 }, { 52, 0 }, }, CH = "AI1", GND = "FIXTURE", HW_GAIN = nil,}, 
                    ALL_SYS_PWRGD               = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 0 }, { 53, 0 }, { 52, 1 }, }, CH = "AI1", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PP0V8_S0_GPU                = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 0 }, { 53, 1 }, { 52, 0 }, }, CH = "AI1", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PPVDDCI_S0_GPU              = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 0 }, { 53, 1 }, { 52, 1 }, }, CH = "AI1", GND = "FIXTURE", HW_GAIN = nil,}, 
                    NA30                        = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 1 }, { 53, 0 }, { 52, 0 }, }, CH = "AI1", GND = "FIXTURE", HW_GAIN = nil,}, 
                    NA31                        = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 1 }, { 53, 0 }, { 52, 1 }, }, CH = "AI1", GND = "FIXTURE", HW_GAIN = nil,}, 
                    NA32                        = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 1 }, { 53, 1 }, { 52, 0 }, }, CH = "AI1", GND = "FIXTURE", HW_GAIN = nil,}, 
                    NA33                        = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 1 }, { 53, 1 }, { 52, 1 }, }, CH = "AI1", GND = "FIXTURE", HW_GAIN = nil,},

                    PPMVPP_S0_GPU               = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 0 }, { 53, 0 }, { 52, 0 }, }, CH = "AI2", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PSU_P12V_EN_L               = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 0 }, { 53, 0 }, { 52, 1 }, }, CH = "AI2", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PP1V8_G3S_SPKRAMP           = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 0 }, { 53, 1 }, { 52, 0 }, }, CH = "AI2", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PMU_CLK32K_WLANBT_R         = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 0 }, { 53, 1 }, { 52, 1 }, }, CH = "AI2", GND = "FIXTURE", HW_GAIN = nil,}, 
                    NA34                        = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 1 }, { 53, 0 }, { 52, 0 }, }, CH = "AI2", GND = "FIXTURE", HW_GAIN = nil,}, 
                    ELOAD_D                     = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 1 }, { 53, 0 }, { 52, 1 }, }, CH = "AI2", GND = "FIXTURE", HW_GAIN = nil,}, 
                    NA36                        = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 1 }, { 53, 1 }, { 52, 0 }, }, CH = "AI2", GND = "FIXTURE", HW_GAIN = nil,}, 
                    ELOAD_C                     = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 1 }, { 53, 1 }, { 52, 1 }, }, CH = "AI2", GND = "FIXTURE", HW_GAIN = nil,},

                    PMU_SYS_ALIVE               = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 0 }, { 53, 0 }, { 52, 0 }, }, CH = "AI3", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PP12VR11V_MAIN_DIV          = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 0 }, { 53, 0 }, { 52, 1 }, }, CH = "AI3", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PP0V8_SLPS2R                = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 0 }, { 53, 1 }, { 52, 0 }, }, CH = "AI3", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PP_DIV                      = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 0 }, { 53, 1 }, { 52, 1 }, }, CH = "AI3", GND = "FIXTURE", HW_GAIN = "3",}, 
                    PPVBS_USBC_XA               = { IO = { { 108, 0 }, { 107, 0 }, { 106, 0 }, { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 0 }, { 53, 1 }, { 52, 1 }, { 105, 1 }, }, CH = "AI3", GND = "FIXTURE", HW_GAIN = "3",}, 
                    PP5V_G3S_USBA               = { IO = { { 108, 0 }, { 107, 0 }, { 106, 1 }, { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 0 }, { 53, 1 }, { 52, 1 }, { 105, 1 }, }, CH = "AI3", GND = "FIXTURE", HW_GAIN = "3",}, 
                    PP5V_G3H                    = { IO = { { 108, 0 }, { 107, 1 }, { 106, 0 }, { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 0 }, { 53, 1 }, { 52, 1 }, { 105, 1 }, }, CH = "AI3", GND = "FIXTURE", HW_GAIN = "3",}, 
                    PP5V_G3S                    = { IO = { { 108, 0 }, { 107, 1 }, { 106, 1 }, { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 0 }, { 53, 1 }, { 52, 1 }, { 105, 1 }, }, CH = "AI3", GND = "FIXTURE", HW_GAIN = "3",}, 
                    PP5V_S0                     = { IO = { { 108, 1 }, { 107, 0 }, { 106, 0 }, { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 0 }, { 53, 1 }, { 52, 1 }, { 105, 1 }, }, CH = "AI3", GND = "FIXTURE", HW_GAIN = "3",}, 
                    PP5V_G3S_USBC_REG           = { IO = { { 108, 1 }, { 107, 0 }, { 106, 1 }, { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 0 }, { 53, 1 }, { 52, 1 }, { 105, 1 }, }, CH = "AI3", GND = "FIXTURE", HW_GAIN = "3",}, 
                    PP12V_LCD_FET_DIV           = { IO = { { 108, 1 }, { 107, 1 }, { 106, 0 }, { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 0 }, { 53, 1 }, { 52, 1 }, { 105, 1 }, }, CH = "AI3", GND = "FIXTURE", HW_GAIN = "3",}, 
                    PP12V_LCD_FET               = { IO = { { 108, 1 }, { 107, 1 }, { 106, 0 }, { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 0 }, { 53, 1 }, { 52, 1 }, { 105, 1 }, }, CH = "AI3", GND = "FIXTURE", HW_GAIN = "3",}, 
                    PP12V_MAIN                  = { IO = { { 108, 1 }, { 107, 1 }, { 106, 1 }, { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 0 }, { 53, 1 }, { 52, 1 }, { 105, 1 }, }, CH = "AI3", GND = "FIXTURE", HW_GAIN = "3",},
                    NA38                        = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 1 }, { 53, 0 }, { 52, 0 }, }, CH = "AI3", GND = "FIXTURE", HW_GAIN = nil,}, 
                    ELOAD_B                     = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 1 }, { 53, 0 }, { 52, 1 }, }, CH = "AI3", GND = "FIXTURE", HW_GAIN = nil,}, 
                    NA40                        = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 1 }, { 53, 1 }, { 52, 0 }, }, CH = "AI3", GND = "FIXTURE", HW_GAIN = nil,}, 
                    ELOAD_A                     = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 1 }, { 53, 1 }, { 52, 1 }, }, CH = "AI3", GND = "FIXTURE", HW_GAIN = nil,},

                    PP0V85_ENET                 = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 0 }, { 53, 0 }, { 52, 0 }, }, CH = "AI4", GND = "FIXTURE", HW_GAIN = nil,}, 
                    -- PP5V_G3S                    = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 0 }, { 53, 0 }, { 52, 1 }, }, CH = "AI4", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PP3V3_G3H_RTC               = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 0 }, { 53, 1 }, { 52, 0 }, }, CH = "AI4", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PPDRV_CPUVR                 = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 0 }, { 53, 1 }, { 52, 1 }, }, CH = "AI4", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PP3V3_TBT_X_LC              = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 1 }, { 53, 0 }, { 52, 0 }, }, CH = "AI4", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PP1V1_SLPDDR                = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 1 }, { 53, 0 }, { 52, 1 }, }, CH = "AI4", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PP0V9_SLPDDR                = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 1 }, { 53, 1 }, { 52, 0 }, }, CH = "AI4", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PP1V8_AWAKE                 = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 1 }, { 53, 1 }, { 52, 1 }, }, CH = "AI4", GND = "FIXTURE", HW_GAIN = nil,}, 
                                                  
                    PPDDRVTT01_S0               = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 0 }, { 53, 0 }, { 52, 0 }, }, CH = "AI5", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PPDDRVTT23_S0               = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 0 }, { 53, 0 }, { 52, 1 }, }, CH = "AI5", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PPVPP01_S3                  = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 0 }, { 53, 1 }, { 52, 0 }, }, CH = "AI5", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PPVPP23_S3                  = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 0 }, { 53, 1 }, { 52, 1 }, }, CH = "AI5", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PP1V1_SLPS2R                = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 1 }, { 53, 0 }, { 52, 0 }, }, CH = "AI5", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PP3V3_AWAKE                 = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 1 }, { 53, 0 }, { 52, 1 }, }, CH = "AI5", GND = "FIXTURE", HW_GAIN = nil,}, 
                    NA32                        = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 1 }, { 53, 1 }, { 52, 0 }, }, CH = "AI5", GND = "FIXTURE", HW_GAIN = nil,}, 
                    NA43                        = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 1 }, { 53, 1 }, { 52, 1 }, }, CH = "AI5", GND = "FIXTURE", HW_GAIN = nil,},  

                    PP0V9_TBT_T_SVR             = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 0 }, { 53, 0 }, { 52, 0 }, }, CH = "AI6", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PP3V3_TBT_T_S0              = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 0 }, { 53, 0 }, { 52, 1 }, }, CH = "AI6", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PP3V3_TBT_T_F               = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 0 }, { 53, 1 }, { 52, 0 }, }, CH = "AI6", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PP3V3_TBT_T_SX              = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 0 }, { 53, 1 }, { 52, 1 }, }, CH = "AI6", GND = "FIXTURE", HW_GAIN = nil,}, 
                    NA44                        = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 1 }, { 53, 0 }, { 52, 0 }, }, CH = "AI6", GND = "FIXTURE", HW_GAIN = nil,}, 
                    NA45                        = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 1 }, { 53, 0 }, { 52, 1 }, }, CH = "AI6", GND = "FIXTURE", HW_GAIN = nil,}, 
                    NA46                        = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 1 }, { 53, 1 }, { 52, 0 }, }, CH = "AI6", GND = "FIXTURE", HW_GAIN = nil,}, 
                    NA47                        = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 1 }, { 53, 1 }, { 52, 1 }, }, CH = "AI6", GND = "FIXTURE", HW_GAIN = nil,}, 

                    PP0V9_TBT_X_SVR             = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 0 }, { 53, 0 }, { 52, 0 }, }, CH = "AI7", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PP0V9_TBT_X_LVR             = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 0 }, { 53, 0 }, { 52, 1 }, }, CH = "AI7", GND = "FIXTURE", HW_GAIN = nil,}, 
                    PP3V3_TBT_X_S0              = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 0 }, { 53, 1 }, { 52, 0 }, }, CH = "AI7", GND = "FIXTURE", HW_GAIN = nil,},
                    PP3V3_TBT_X_SX              = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 0 }, { 53, 1 }, { 52, 1 }, }, CH = "AI7", GND = "FIXTURE", HW_GAIN = nil,}, 
                    NA48                        = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 1 }, { 53, 0 }, { 52, 0 }, }, CH = "AI7", GND = "FIXTURE", HW_GAIN = nil,}, 
                    NA49                        = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 1 }, { 53, 0 }, { 52, 1 }, }, CH = "AI7", GND = "FIXTURE", HW_GAIN = nil,}, 
                    NA50                        = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 1 }, { 53, 1 }, { 52, 0 }, }, CH = "AI7", GND = "FIXTURE", HW_GAIN = nil,}, 
                    NA51                        = { IO = { { 55, 1 }, { 118, 1 }, { 117, 1 }, { 54, 1 }, { 53, 1 }, { 52, 1 }, }, CH = "AI7", GND = "FIXTURE", HW_GAIN = nil,},    

                    DISCONNECT                  = { IO = { { 118, 0 }, { 117, 0 }, { 56, 0 }, { 55, 0 }, { 54, 0 }, { 53, 0 }, { 52, 0 }, { 59, 0 }, { 58, 0 }, { 57, 0 }, { 63, 0 }, { 60, 0 }, { 61, 0 }, { 62, 0 }, }, CH = "", }, }





HWIO.channel_map={
                    CH1                         = {"PMU_SYS_ALIVE",1,0,1,600},       
                    CH2                         = {"PPVDDCPUSRAM_AWAKE",1,0,1,400},                
                    CH3                         = {"PP3V3_G3H",1,0,1,1650},         
                    CH4                         = {"PP1V8_SLPS2R",1,0,1,900},   
                    CH5                         = {"PMU_ACTIVE_READY",1,0,1,900},   
                    CH6                         = {"PP3V3_G3W",1,0,1,1650},  
                    CH7                         = {"PP0V82_SLPDDR",1,0,1,410},                
                    CH8                         = {"PP1V2_AWAKE",1,0,1,600},            
                    CH9                         = {"PPVDDCPU_AWAKE_SOC",1,0,1,350},                
                    CH10                        = {"PMU_COLD_RESET_L",1,0,1,440},    --disable      
                    CH11                        = {"PP1V1_SLPDDR",1,0,1,550},                
                    CH12                        = {"PP0V9_SLPDDR",1,0,1,440},          
                    CH13                        = {"PP1V8_AWAKE",1,0,1,900},                
                    CH14                        = {"PP1V1_SLPS2R",1,0,1,550},            
                    CH15                        = {"PP3V3_AWAKE",1,0,1,1650},                
                    CH16                        = {"PM_G3S_EN",1,0,1,900},  
                    CH17                        = {"P3V3G3W_EN",1,0,1,1650},  
                    CH18                        = {"SSD_PMU_RESET_L",1,0,1,900},  --disable
                    CH19                        = {"P3V3G3W_PGOOD",1,0,1,600},                
                    CH20                        = {"P3V3G3H_PGOOD",1,0,1,600},                
                    CH21                        = {"PP3V3_G3S",1,0,1,1650},                
                    CH22                        = {"PP1V8_SLPS2R_PMUVDDGPIO",1,0,1,900},                
                    CH23                        = {"SOC_SOCHOT_L",1,0,1,330},    --disable            
                    CH24                        = {"P1V1_SLPDDR_SOCFET_EN",1,0,1,900},         
                    CH25                        = {"PP0V8_SLPS2R",1,0,1,400},         
                    CH26                        = {"P1V8G3S_3V3G3S_PGOOD",1,0,1,900},         
                    CH27                        = {"PP1V8_G3S_SPKRAMP",1,0,1,900},               
                    CH28                        = {"PMU_CLK32K_WLANBT_R",1,0,1,800},       
                    CH29                        = {"SOC_WDOG",1,0,1,400},              
                    CH30                        = {"PMU_CLK32K_SOC",1,0,1,900},    
                    CH31                        = {"P5VG3S_PGOOD",11/10,0,1,900},    
                    CH32                        = {"PP5VG3S_EN",5/2,0,1,660},     
                    CH33                        = {"PSU12VPGOOD",5/2,0,1,320},     
                    CH34                        = {"NONE",5/2,0,1,1000},  
                    CH35                        = {"PP5V_G3H",5/2,0,1,1000},  
                    CH36                        = {"PP5V_G3S",5/2,0,1,1000},    
                    CH37                        = {"PP11V_ACDC",5/2,0,1,2200},
                    CH38                        = {"PSU_P12V_EN_L",5/2,0,1,2000},
                    CH39                        = {"PP12VR11V_MAIN",5/2,0,1,400},       
                    CH40                        = {"PP12V_MAIN",5/2,0,1,2400},       
                    CH41                        = {"PP1V8_S5",1,0,1,900}, 
                    CH42                        = {"PP3V3_G3H_ENET_FET",1,0,1,1650}, 
                    CH43                        = {"PPVCCIN_S0",1,0,1,600}, 
                    CH44                        = {"PPVCORE_S0_GPU",1,0,1,600}, 
                    CH45                        = {"PP1V0_PCH_IO_S5",1,0,1,500}, 
                    CH46                        = {"PM_RSMRST_PCH_L",1,0,1,1650}, 
                    CH47                        = {"PM_PWRBTN_L",1,0,1,1650}, 
                    CH48                        = {"PM_PCH_SYS_PWROK",1,0,1,1650}, 
                    CH49                        = {"PLT_RESET_L",1,0,1,900},  
                    CH50                        = {"SMC_SYSRST_L",1,0,1,900}, 
                    CH51                        = {"PVCCIO_PGOOD",1,0,1,1650}, 
                    CH52                        = {"PPVCCSA_S0",1,0,1,600}, 
                    CH53                        = {"PP1V8_S0",1,0,1,900}, 
                    CH54                        = {"PPVDDQ01_S3",1,0,1,600}, 
                    CH55                        = {"PPVDDQ23_S3",1,0,1,600}, 
                    CH56                        = {"PP1V0_PCH_CORE_S5",1,0,1,500}, 
                    CH57                        = {"PP1V8_S0_GPU",1,0,1,900}, 
                    CH58                        = {"PPVCCIO_S0",1,0,1,500}, 
                    CH59                        = {"ENET_PWR_EN",1,0,1,1650}, 
                    CH60                        = {"PPDDRVTT01_S0",1,0,1,500}, 
                    CH61                        = {"PPDDRVTT23_S0",1,0,1,500}, 
                    CH62                        = {"PPVPP01_S3",1,0,1,1200}, 
                    CH63                        = {"PPVPP23_S3",1,0,1,1200}, 
                    CH64                        = {"PPMVDD_S0_GPU",1,0,1,600}, 
                    CH65                        = {"PM_S0_EN",1,0,1,1650}, 
                    CH66                        = {"CPU_VR_READY",1,0,1,1650}, 
                    CH67                        = {"PMU_ONOFF_L",1,0,1,1650}, 
                    CH68                        = {"P3V3S5_EN",1,0,1,1650}, 
                    CH69                        = {"PVPP_PVDDQ_EN_CALPE",1,0,1,1650}, 
                    CH70                        = {"GPU_RESET_L",1,0,1,900}, 
                    CH71                        = {"PM_PCH_PWROK",11/10,0,1,1500}, 
                    CH72                        = {"PM_S0_PGOOD",5/2,0,1,600}, 
                    CH73                        = {"PVCCIO_EN",5/2,0,1,200}, 
                    CH74                        = {"ALL_SYS_PWRGD",5/2,0,1,360}, 
                    CH75                        = {"PP0V8_S0_GPU",5/2,0,1,160}, 
                    CH76                        = {"PPVDDCI_S0_GPU",5/2,0,1,200}, 
                    CH77                        = {"PPMVPP_S0_GPU",5/2,0,1,600}, 
                    CH78                        = {"PP3V3_S0",5/2,0,1,600}, 
                    CH79                        = {"PP3V3_S5",5/2,0,1,600}, 
                    CH80                        = {"PP12V_MAIN",5/2,0,1,2200}, 
                }

HWIO.DMMSwitchGNDTable = {
                                DUT                         = { IO = { { 63, 1 }, { 60, 0 }, { 61, 1 }, { 62, 0 }, }, },
                                FIXTURE                     = { IO = { { 63, 1 }, { 60, 0 }, { 61, 0 }, { 62, 1 }, }, },
                                BKLT                        = { IO = { { 63, 1 }, { 60, 0 }, { 61, 0 }, { 62, 0 }, }, },
                                POWER_CURRENT_N             = { IO = { { 63, 1 }, { 62, 1 }, { 61, 1 }, { 60, 0 }, }, },
                                DISCONNECT                  = { IO = { { 63, 0 }, { 60, 0 }, { 61, 0 }, { 62, 0 }, }, }, }
HWIO.AISwtichTable = {
                                AI0                         = { IO = { { 56, 1 }, { 59, 0 }, { 58, 0 }, { 57, 0 }, }, },
                                AI1                         = { IO = { { 56, 1 }, { 59, 0 }, { 58, 0 }, { 57, 1 }, }, },
                                AI2                         = { IO = { { 56, 1 }, { 59, 0 }, { 58, 1 }, { 57, 0 }, }, },
                                AI3                         = { IO = { { 56, 1 }, { 59, 0 }, { 58, 1 }, { 57, 1 }, }, },
                                AI4                         = { IO = { { 56, 1 }, { 59, 1 }, { 58, 0 }, { 57, 0 }, }, },
                                AI5                         = { IO = { { 56, 1 }, { 59, 1 }, { 58, 0 }, { 57, 1 }, }, },
                                AI6                         = { IO = { { 56, 1 }, { 59, 1 }, { 58, 1 }, { 57, 0 }, }, },
                                AI7                         = { IO = { { 56, 1 }, { 59, 1 }, { 58, 1 }, { 57, 1 }, }, },
                                DISCONNECT                  = { IO = { { 56, 0 }, { 59, 0 }, { 58, 0 }, { 57, 0 }, }, }, }
HWIO.THDNFrequencyTable = {
                                FAN_PWM_DUT_OUTPUT          = { IO = { { 12, 1 }, { 79, 0 }, { 78, 0 }, { 77, 0 }, { 76, 1 }, }, SETTINGS = "900",TYPE = "B"}, 
                                FAN_PWM_FPGA_OUTPUT         = { IO = { { 12, 1 }, { 79, 0 }, { 78, 0 }, { 77, 1 }, { 76, 1 }, }, SETTINGS = "900",TYPE = "B"}, 
                                FAN_PWM_BUFFER_DUT_OUTPUT   = { IO = { { 12, 1 }, { 79, 0 }, { 78, 1 }, { 77, 0 }, { 76, 1 }, }, SETTINGS = "900",TYPE = "B"}, 
                                FAN_PWM_BUFFER_FPGA_OUTPUT  = { IO = { { 12, 1 }, { 79, 0 }, { 78, 1 }, { 77, 1 }, { 76, 1 }, }, SETTINGS = "900",TYPE = "B"}, 
                                SPEAKER_P_FREQ              = { IO = { { 12, 1 }, { 79, 1 }, { 78, 1 }, { 77, 0 }, { 76, 1 }, }, SETTINGS = "200",TYPE = ""},
                                SPEAKER_N_FREQ              = { IO = { { 12, 1 }, { 79, 1 }, { 78, 1 }, { 77, 1 }, { 76, 1 }, }, SETTINGS = "200",TYPE = ""},
                                DISCONNECT                  = { IO = { { 76, 0 }, { 79, 0 }, { 78, 0 }, { 77, 0 }, }, SETTINGS = "900",},}
HWIO.EloadTable = {
                                USBA_A                      = { IO = { { 89, 0 }, }, DAC = "1", },
                                USBA_B                      = { IO = { { 88, 0 }, }, DAC = "2", },
                                USBA_C                      = { IO = { { 96, 0 }, }, DAC = "3", },
                                USBA_D                      = { IO = { { 104, 0 }, }, DAC = "4", },
                                USBA                        = { IO = { { 89, 0 }, { 88, 0 }, { 96, 0 }, { 104, 0 }, }, DAC = "", },
                                USBC_A                      = { IO = { { 89, 1 }, }, DAC = "1", },
                                USBC_B                      = { IO = { { 88, 1 }, }, DAC = "2", },
                                USBC_C                      = { IO = { { 96, 1 }, }, DAC = "3", },
                                USBC_D                      = { IO = { { 104, 1 }, }, DAC = "4", },
                                USBC                        = { IO = { { 89, 1 }, { 88, 1 }, { 96, 1 }, { 104, 1 }, }, DAC = "", },
                                DISCONNECT                  = { IO = { { 88, 0 }, { 89, 0 }, { 96, 0 }, { 104, 0 }, }, DAC = "", }, }
HWIO.FixtureControlTable = {
                                GREEN_LED_ON                = {F_CTRL_CMD = "4LED:GREEN:ON 1",},
                                GREEN_LED_OFF               = {F_CTRL_CMD = "4LED:GREEN:OFF 1",},
                                RED_LED_ON                  = {F_CTRL_CMD = "4LED:RED:ON 1",},
                                RED_LED_OFF                 = {F_CTRL_CMD = "4LED:RED:OFF 1",},
                                BLUE_LED_ON                 = {F_CTRL_CMD = "4LED:BLUE:ON 1",},
                                BLUE_LED_OFF                = {F_CTRL_CMD = "4LED:BLUE:OFF 1",},
                                RUN_LED_ON                  = {F_CTRL_CMD = "4LED:RUN 1",},
                                RUN_LED_OFF                 = {F_CTRL_CMD = "4LED:RUN 0",},
                                START_TEST                  = {F_CTRL_CMD = "FT:START",},
                                RELEASE                     = {F_CTRL_CMD = "FT:RESET",},}
                                                                      
HWIO.RelayTable = {

POWERSEQUENCE_CTRL = {
                                CONNECT                     = { IO = { { 1, 1 }, }, },     
                                DISCONNECT                  = { IO = { { 1, 0 }, }, }, },
POWERSEQUENCE_SWITCH_CTRL = {
                                CONNECT                     = { IO = { { 11, 1 }, }, },    
                                DISCONNECT                  = { IO = { { 11, 0 }, }, }, },
FREQUENCY_SWITCH_CTRL = {
                                CONNECT                     = { IO = { { 12, 0 }, }, },    
                                DISCONNECT                  = { IO = { { 12, 1 }, }, }, },
DMIC_PDM_CTRL = {
                                CONNECT                     = { IO = { { 34, 0 }, { 36, 1 }, }, },    
                                DISCONNECT                  = { IO = { { 34, 1 }, { 36, 0 }, }, }, },
AUDIO_PDM_CTRL = {
                                CONNECT                     = { IO = { { 35, 0 }, { 37, 1 }, }, },    
                                DISCONNECT                  = { IO = { { 35, 1 }, { 37, 0 }, }, }, },
SPKR_ID0 = {
                                CONNECT                     = { IO = { { 38, 1 }, }, },    
                                DISCONNECT                  = { IO = { { 38, 0 }, }, }, },
SPKR_ID1 = {
                                CONNECT                     = { IO = { { 39, 1 }, }, },    
                                DISCONNECT                  = { IO = { { 39, 0 }, }, }, },
DFU_MUX_CTRL = {
                                CONNECT                     = { IO = { { 40, 1 }, }, },    
                                DISCONNECT                  = { IO = { { 40, 0 }, }, }, },
PSU_500W_CTRL = {
                                CONNECT                     = { IO = { { 50, 1 }, }, },    
                                DISCONNECT                  = { IO = { { 50, 0 }, }, }, },
BUTTON_IMAC_PWR_BTN = {
                                CONNECT                     = { IO = { { 51, 1 }, }, }, 
                                DISCONNECT                  = { IO = { { 51, 0 }, }, }, },
POWER_750W_CTRL = {
                                CONNECT                     = { IO = { { 83, 1 }, }, },    
                                DISCONNECT                  = { IO = { { 83, 0 }, }, }, },
POWER_BOARD_CTRL = {
                                CONNECT                     = { IO = { { 82, 1 }, }, },    
                                DISCONNECT                  = { IO = { { 82, 0 }, }, }, },
POTASSIUM_BOX = { 
                                CONNECT                     = { IO = { { 126, 1 }, { 48, 1 }, { 81, 1 }, { 95, 1 }, { 65, 0 }, }, },  --Power Off and then On
                                DISCONNECT                  = { IO = { { 65, 1 }, { 48, 0 }, { 81, 0 }, { 95, 0 }, { 126, 0 }, }, }, },
PP_DIV_SWITCH = {
                                PPVBS_USBC_XA               = { IO = { { 105, 1 }, { 108, 0 }, { 107, 0 }, { 106, 0 }, }, },
                                PP5V_G3S_USBA               = { IO = { { 105, 1 }, { 108, 0 }, { 107, 0 }, { 106, 1 }, }, },
                                PP5V_G3H                    = { IO = { { 105, 1 }, { 108, 0 }, { 107, 1 }, { 106, 0 }, }, },
                                PP5V_G3S                    = { IO = { { 105, 1 }, { 108, 0 }, { 107, 1 }, { 106, 1 }, }, },
                                PP5V_S0                     = { IO = { { 105, 1 }, { 108, 1 }, { 107, 0 }, { 106, 0 }, }, },
                                PP5V_G3S_USBC_REG           = { IO = { { 105, 1 }, { 108, 1 }, { 107, 0 }, { 106, 1 }, }, },
                                PP12V_LCD_FET               = { IO = { { 105, 1 }, { 108, 1 }, { 107, 1 }, { 106, 0 }, }, },
                                PP12V_MAIN                  = { IO = { { 105, 1 }, { 108, 1 }, { 107, 1 }, { 106, 1 }, }, },
                                DISCONNECT                  = { IO = { { 105, 0 }, { 108, 0 }, { 107, 0 }, { 106, 0 }, }, }, },

AUD_CONN_J1_CTRL = {
                                AUD_TIP_SENSE               = { IO = { { 43, 1 }, }, }, 
                                AUD_RING_SENSE              = { IO = { { 44, 1 }, }, }, 
                                CABLE_DETECT                = { IO = { { 45, 1 }, }, }, 
                                DISCONNECT                  = { IO = { { 43, 0 }, { 44, 0 }, { 45, 0 }, }, }, },
FORCE_DFU_1V8 = {
                                CONNECT                     = { IO = { { 49 , 0 }, { 64 , 1 }, }, },  
                                DISCONNECT                  = { IO = { { 49 , 0 }, { 64 , 0 }, }, }, },
SOC_SWD_MUX_SEL = {
                                CONNECT                     = { IO = { { 113 , 1 }, { 114 , 0 }, }, },  
                                DISCONNECT                  = { IO = { { 113 , 0 }, { 114 , 0 }, }, }, },
SOC_USB_VBUS =  {                      
                                CONNECT                     = { IO = { { 125 , 1 }, { 124 ,0 }, }, },
                                DISCONNECT                  = { IO = { { 125 , 0 }, { 124 ,1 }, }, }, },
PMU_COLD_RESET_L =  {                      
                                CONNECT                     = { IO = { { 115 , 1 }, }, },
                                DISCONNECT                  = { IO = { { 115 , 0 }, }, }, },

AUD_CONN_J1_SWITCH_CTRL =  {                      
                                MIKEY                       = { IO = { { 127 , 1 }, { 128 , 1 }, }, },
                                HP_RT_TO_HS_MICP            = { IO = { { 127 , 0 }, { 128 , 0 }, { 119 , 1 }, { 120 ,1 }, { 121 ,1 }, }, },
                                HP_LT_TO_HS_MICP            = { IO = { { 127 , 0 }, { 128 , 0 }, { 119 , 1 }, { 120 ,1 }, { 121 ,0 }, }, },
                                HP_RT_TO_HS_MICN            = { IO = { { 127 , 0 }, { 128 , 0 }, { 119 , 0 }, { 120 ,1 }, { 121 ,1 }, }, },
                                HP_LT_TO_HS_MICN            = { IO = { { 127 , 0 }, { 128 , 0 }, { 119 , 0 }, { 120 ,1 }, { 121 ,0 }, }, },
                                DISCONNECT                  = { IO = { { 127 , 0 }, { 128 , 0 }, { 119 , 0 }, { 120 ,0 }, { 121 ,0 }, }, },},
EEPROM_I2C_CTRL = {
                                I2C_DP_TCON                 = { IO = { { 41, 0 }, { 42, 0 }, { 80, 1 }, { 122, 0 }, { 123, 0 }, }, },    
                                I2C_FTCAM                   = { IO = { { 41, 1 }, { 42, 0 }, { 80, 1 }, { 122, 0 }, { 123, 1 }, }, },    
                                I2C_ALS                     = { IO = { { 41, 0 }, { 42, 1 }, { 80, 1 }, { 122, 1 }, { 123, 0 }, }, },    
                                DISCONNECT                  = { IO = { { 41, 0 }, { 42, 0 }, { 80, 0 }, { 122, 0 }, { 123, 0 }, }, }, },  
DEBUG_UART = {     
                                SMC                         = { IO = { { 97, 0 }, { 101, 1 }, { 98, 0 }, { 102, 0 }, { 99, 0 }, { 103, 0 }, }, },
                                PCH                         = { IO = { { 97, 0 }, { 101, 0 }, { 98, 0 }, { 102, 1 }, { 99, 0 }, { 103, 0 }, }, },
                                SOC                         = { IO = { { 97, 0 }, { 101, 0 }, { 98, 0 }, { 102, 0 }, { 99, 0 }, { 103, 1 }, }, },
                                DISCONNECT                  = { IO = { { 97, 0 }, { 101, 0 }, { 98, 0 }, { 102, 0 }, { 99, 0 }, { 103, 0 }, }, }, },
FAN_CTRL = { 
                                FAN0_PWM_TO_FPGA            = { IO = { { 71, 1 }, { 72, 1 }, { 73, 0 }, { 74, 1 }, { 75, 0 }, }, }, 
                                FAN1_PWM_TO_FPGA            = { IO = { { 71, 1 }, { 72, 1 }, { 73, 1 }, { 74, 1 }, { 75, 0 }, }, }, 
                                FAN0_TACH_TO_UUT            = { IO = { { 71, 1 }, { 72, 1 }, { 73, 0 }, { 74, 0 }, { 75, 1 }, }, }, 
                                FAN1_TACH_TO_UUT            = { IO = { { 71, 1 }, { 72, 1 }, { 73, 1 }, { 74, 0 }, { 75, 1 }, }, }, 
                                FAN0_CONNECT                = { IO = { { 71, 1 }, { 72, 1 }, { 73, 0 }, { 74, 1 }, { 75, 1 }, }, }, 
                                FAN1_CONNECT                = { IO = { { 71, 1 }, { 72, 1 }, { 73, 1 }, { 74, 1 }, { 75, 1 }, }, }, 
                                DISCONNECT                  = { IO = { { 71, 0 }, { 72, 0 }, { 73, 0 }, { 74, 0 }, { 75, 0 }, }, }, },
BKLT_CHANNEL_SELECT = { 
                                LED_RETURN_1                = { IO = { { 130, 0 }, { 131, 0 }, { 132, 0 }, { 133, 0 }, }, }, 
                                LED_RETURN_2                = { IO = { { 130, 1 }, { 131, 0 }, { 132, 0 }, { 133, 0 }, }, }, 
                                LED_RETURN_3                = { IO = { { 130, 0 }, { 131, 1 }, { 132, 0 }, { 133, 0 }, }, }, 
                                LED_RETURN_4                = { IO = { { 130, 1 }, { 131, 1 }, { 132, 0 }, { 133, 0 }, }, }, 
                                LED_RETURN_5                = { IO = { { 130, 0 }, { 131, 0 }, { 132, 1 }, { 133, 0 }, }, }, 
                                LED_RETURN_6                = { IO = { { 130, 1 }, { 131, 0 }, { 132, 1 }, { 133, 0 }, }, }, 
                                LED_RETURN_7                = { IO = { { 130, 0 }, { 131, 1 }, { 132, 1 }, { 133, 0 }, }, }, 
                                LED_RETURN_8                = { IO = { { 130, 1 }, { 131, 1 }, { 132, 1 }, { 133, 0 }, }, }, 
                                LED_RETURN_9                = { IO = { { 130, 0 }, { 131, 0 }, { 132, 0 }, { 133, 1 }, }, }, 
                                DISCONNECT                  = { IO = { { 134, 0 }, { 135, 0 }, { 136, 0 }, }, }, },
BKLT_MEASURE_CHANNEL_SELECT = { 
                                POWERSEQUENCE_CURRENT       = { IO = { { 11 , 1 }, }, }, 
                                BKLT_BOOST                  = { IO = { { 134, 1 }, { 135, 1 }, { 136, 0 }, }, }, 
                                LED_RETURN                  = { IO = { { 134, 1 }, { 135, 0 }, { 136, 1 }, }, }, 
                                RES_TEST                    = { IO = { { 134, 1 }, { 135, 1 }, { 136, 1 }, }, },
                                DISCONNECT                  = { IO = { { 134, 0 }, { 135, 0 }, { 136, 0 }, }, }, }, 
LED_CURR_VOLT_SWITCH = { 

                                VOLTAGE                     = { IO = { { 137 , 0 }, }, },
                                CURRENT                     = { IO = { { 137 , 1 }, }, }, },         
BKLT_MODULE = { 

                                CONNECT                     = { IO = { { 129 , 1 }, }, },
                                DISCONNECT                  = { IO = { { 129 , 0 }, }, }, }, 
BKLT_LOW_CURRENT_AMP = { 

                                AMPLIFICATION_1             = { IO = { { 142 , 1 }, { 143 , 0 }, { 144 , 0 }, { 137 , 1 }, { 134 , 1 }, { 135 , 1 }, { 136 , 1 }, { 142 , 0 }, }, },
                                AMPLIFICATION_10            = { IO = { { 142 , 1 }, { 143 , 0 }, { 144 , 1 }, { 137 , 1 }, { 134 , 1 }, { 135 , 1 }, { 136 , 1 }, { 142 , 0 }, }, },
                                AMPLIFICATION_100           = { IO = { { 142 , 1 }, { 143 , 1 }, { 144 , 0 }, { 137 , 1 }, { 134 , 1 }, { 135 , 1 }, { 136 , 1 }, { 142 , 0 }, }, },
                                AMPLIFICATION_1000          = { IO = { { 142 , 1 }, { 143 , 1 }, { 144 , 1 }, { 137 , 1 }, { 134 , 1 }, { 135 , 1 }, { 136 , 1 }, { 142 , 0 }, }, },
                                DISCONNECT                  = { IO = { { 142 , 1 }, { 144 , 0 }, { 143 , 0 }, { 138 , 0 }, { 139 , 0 }, { 142 , 0 }, }, }, }, 
DISCHARGER = { 

                                CONNECT                     = { IO = { { 70 , 1 }, { 87 , 1 }, { 116 , 1 }, }, },
                                DISCONNECT                  = { IO = { { 70 , 0 }, { 87 , 0 }, { 116 , 0 }, }, }, },

PS_CHANNEL_SWTICH = {
                                PS_IN0_CTRL                 = { IO = { { 69 , 0 }, { 68 , 0 }, { 69 , 1 }, }, },
                                PS_IN1_CTRL                 = { IO = { { 69 , 0 }, { 68 , 1 }, { 69 , 1 }, }, },
                                DISCONNECT                  = { IO = { { 69 , 0 }, { 68 , 0 }, }, }, },
FREQ_SWITCH = {   
                                AUD_SPKR_LTWT               = { IO = { { 91, 0 }, { 92, 0 }, { 93, 1 }, { 94, 0 }, }, },
                                AUD_SPKR_LWFR               = { IO = { { 91, 1 }, { 92, 0 }, { 93, 0 }, { 94, 0 }, }, },
                                AUD_SPKR_RWFR               = { IO = { { 91, 0 }, { 92, 1 }, { 93, 0 }, { 94, 0 }, }, },
                                AUD_SPKR_RTWT               = { IO = { { 91, 0 }, { 92, 0 }, { 93, 0 }, { 94, 1 }, }, }, 
                                DISCONNECT                  = { IO = { { 91, 0 }, { 92, 0 }, { 93, 0 }, { 94, 0 }, }, }, },                                                      
DFU_USB_SWITCH =  {
                                CONNECT                     = { IO = { { 90, 1 }, }, },     
                                DISCONNECT                  = { IO = { { 90, 0 }, }, }, },
BLC_LSYNC = {
                                CONNECT                     = { IO = { { 100 , 1 }, { 33 , 1 }, }, },  
                                DISCONNECT                  = { IO = { { 100 , 0 }, { 33 , 0 }, }, }, },
USB_ELOAD_CTRL = {
                                USBA_A                      = { IO = { { 89, 0 }, }, },
                                USBA_B                      = { IO = { { 88, 0 }, }, },
                                USBA_C                      = { IO = { { 96, 0 }, }, },
                                USBA_D                      = { IO = { { 104, 0 }, }, },
                                USBC_A                      = { IO = { { 89, 1 }, }, },
                                USBC_B                      = { IO = { { 88, 1 }, }, },
                                USBC_C                      = { IO = { { 96, 1 }, }, },
                                USBC_D                      = { IO = { { 104, 1 }, }, },
                                DISCONNECT                  = { IO = { { 88, 0 }, { 89, 0 }, { 96, 0 }, { 104, 0 }, }, }, },
USBC_CHARGE_CTRL1 = {
                                USBC_ELOAD_XA               = { IO = { { 109, 1 }, }, },
                                USBC_ELOAD_XB               = { IO = { { 110, 1 }, }, },
                                USBC_ELOAD_XC               = { IO = { { 111, 1 }, }, },
                                USBC_ELOAD_XD               = { IO = { { 112, 1 }, }, },
                                DISCONNECT                  = { IO = { { 109, 0 }, { 110, 0 }, { 111, 0 }, { 112, 0 }, }, }, },
USBC_CHARGE_CTRL2 = {
                                USBC_CC_TO_MODULE1          = { IO = { { 47, 0 }, { 66, 0 }, }, },
                                USBC_CC_TO_MODULE2          = { IO = { { 47, 0 }, { 66, 1 }, }, },
                                USBC_PN_TO_MODULE1          = { IO = { { 82, 0 }, { 67, 0 }, }, },
                                USBC_PN_TO_MODULE2          = { IO = { { 82, 0 }, { 67, 1 }, }, },
                                DISCONNECT                  = { IO = { { 46, 0 }, { 47, 0 }, { 67, 0 }, { 82, 0 }, }, }, },
}

return HWIO



















