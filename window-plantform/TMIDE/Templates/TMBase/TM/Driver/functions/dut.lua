--- DUT Function
-- @module functions.dut
-- @alias dut_module
local dut_module = {}

local sequence_utils = require("utils.sequence_utils")
local string_utils = require("utils.string_utils")
local time_utils = require("utils.time_utils")
local global_data = require("utils.global_data")
-- local console = require("console.dut.potassium")
-- local nanocom = require("console.dut.nanocom")
local nanocom = {}
local console = require("console.dut.tcpAndZmq")
local bit32 = require("bit32")
--===========================--
-- Module local variables
--===========================--
local _PDCA_KEY_MATCH_ = "{{(.-)}}"
local _last_diags_command = ""
local _last_diags_response = ""
local TemplatePatternTable = {}
local _SOC_P_RESPONSE = ""
-- local TemplateVariantTable = {}

--===========================--
--- Private Functions
-- @section private
--===========================--
local function addTimeStampMask(str)
  local t = {}
  local x = 0;
  local mask = "[0-9A-F]+@[RT]%d%s+"
  for v in string.gmatch(str..'\n', "(.-)[\r\n]+") do
    -- print(#v,v)
    if(x ==0 and #v==0) then 
      t[#t+1] = v--the first line, if value is empty, do not add time stamp
    else
      t[#t+1] = mask .. v
    end
    x=x+1
  end
  if string.match(string.sub(str,-1),"[\r\n]") then
      t[#t+1]=''
    end
  return table.concat(t,"[\r\n]+")
end

--- Parse diags template file and populate the match table with patterns
-- @param diags_command Diags command parsing template that should be processed
-- @return TemplatePatternTable reference
local function parse_template_file(diags_command)--par = file Name
  local fileName = diags_command..".txt"
  local file = io.open(CONFIG.TEMPLATE_PATH..fileName)
  if (file == nil) then
    file = io.open(CONFIG.TEMPLATE_ROOT_PATH..fileName)
  end

  if(file) then
    local keyTable = {}
    local patternTable = {}
    local str = file:read("*a")
    file:close()

    for key in string.gmatch(str, _PDCA_KEY_MATCH_) do--{{Phosphorus_ASIC_odr}}, delete {{}}
      table.insert(keyTable, key)
    end

    -- TemplateVariantTable[fileName] = keyTable--{"123.txt"={key1,key2,...}},key has no {{}}

    local strMatch = ""
    local sig = 1
    for i=1, #keyTable do
      st, ed = string.find(str, "{{"..keyTable[i].."}}", sig)
      txt = string.sub(str, sig, st-1)
      txt = string_utils.replace_special_char(txt)

      local endStr = string.sub(str, ed+1, ed+1)
      endStr = string_utils.replace_special_char(endStr)
      local endStr2 = string.sub(str, ed+2, ed+2)

      if(endStr2 ~= "{") then
        endStr2 = string_utils.replace_special_char(endStr2)
      else
        endStr2 = ""
      end

      matchTmp = strMatch .. txt .. "(.-)" .. endStr .. endStr2--string.sub(str, ed+1, ed+1)
      strMatch = strMatch .. txt .. ".-" .. endStr--string.sub(str, ed+1, ed+1)
      -- string.gsub(matchTmp,'[\r\n]+','[\r\n]+')
      -- matchTmp = addTimeStampMask(matchTmp)
      patternTable[keyTable[i]] = matchTmp--patternTable={"ABC"="xxxx(.-)",...}, "ABC", there is no "{{}}""
      sig = ed + 2
      -- print(keyTable[i], matchTmp)
    end

    TemplatePatternTable[diags_command] = patternTable--{"file.txt" = {key="..."}, ...} key has no {{}}

  else
    error("ERRCODE[-5]Failed to open template file: "..fileName)
  end

  return TemplatePatternTable
end

--===========================--
--- Public API
-- @section public
--===========================--

--- Run the given smokey script on the device
-- @tparam sequence_object sequence FCT test sequence table
-- @param global_var_table table full of "global" test variables
-- @return true, or whether the returned data matches the expected value
-- @return passing status (whether the returned data matches the expected value)
-- @raise error
-- @see diags, parse
function dut_module.smokey(sequence, global_var_table)
    local old_command = sequence.param1
    sequence.param1 = "smokeyshell -f nandfs:\\AppleInternal\\Diags\\Scripts\\X527\\"..tostring(old_command)
    
    local retsult, passing = dut_module.diags(sequence, global_var_table)
    _last_diags_command = string.match(tostring(old_command),"(.-) ")

    sequence.param1 = old_command
    
    return result, passing
end

--- Run the given diags command on the device
-- @tparam sequence_object sequence FCT test sequence table
-- @param global_var_table table full of "global" test variables
-- @return true, or whether the returned data matches the expected value
-- @return passing status (whether the returned data matches the expected value)
-- @raise error
-- @see parse
function dut_module.diags(sequence, global_var_table)
	_SOC_P_RESPONSE = ""
  local timeout = tonumber(sequence.timeout)  --3sec
  if(timeout == nil) then 
      timeout = 10000
  end

  local command = global_data.sub(global_var_table, sequence.param1)
  _last_diags_command = command
  console._Dut_Read_String_()
  if(command) then
    if string.match(command, "sleep") ~= nil or string.match(command, "playaudio") ~= nil or string.match(command, "loopaudio") ~= nil or string.match(command, "fsboot") ~= nil or string.match(command, "recordaudio") ~= nil then
      console._Dut_Send_String_(command)
    else
      local ret, errmsg = console._Dut_Send_Cmd_(command, timeout) --this function will set detect to ] :-)
      _last_diags_response = console._Dut_Read_String_()
      if(ret ~= 0) then return "--FAIL--diags timeout" end
      -- here need to replace xxxxxxx\r return format data
      -- _last_diags_response = string.gsub(tostring(_last_diags_response), "[0-9A-F]+@[RT]%d%s+\r", "")
      -- _last_diags_response = string.gsub(tostring(_last_diags_response), "[0-9A-F]+@[RT]%d%s+\n", "")
      -- print(" < DUT ReadResp: >\n",_last_diags_response)
    end
  else
      return tostring("ERROCODE[-6]Problem with diags command string or variable substitution: "..command)
  end
  local result = sequence_utils.check_string_match_limit(_last_diags_response, sequence)
  return result, result
end

--- Spin until timeout or we detect a string (given in the sequence)
-- @tparam sequence_object sequence FCT test sequence table
-- @return true
-- @return passing status (true)
-- @raise error
function dut_module.detect(sequence) --par = "xxx"
  -- local det = string_utils.replace_special_char(sequence.param1)
  local det = sequence.param1
  local timeout = tonumber(sequence.timeout)
  if timeout==nil then timeout=30000 end
  print("< Note > : wait to detect...'"..det.."'")
  -- console._Dut_Read_String_()
  console._Dut_Set_Detect_String_(det)

  local st, msg = console._Dut_Wait_For_String_(timeout)
  if st ~= 0 then
    if string.match(det, "%:%-%)") then
      return "--FAIL--diags timeout" 
    else
      return tostring("--FAIL--Timeout when waiting for ".. tostring(det) ..". Time out due to: "..tostring(msg))
    end
  end

  local ret = console._Dut_Read_String_()
  if string.match(det, "%:%-%)") then
    local times = 0;
    for v in string.gmatch(ret, "(%:%-%))") do  --search how many :-) got
      times = times + 1
    end
    local firstLine = string.match(ret, "(.-)[\r\n]+")
    if(string.match(firstLine, "%:%-%)") and times==1) then --only one :-) and in the first line,detect again
      st, msg = console._Dut_Wait_For_String_(timeout)
      if st ~= 0 then
        return "--FAIL--diags timeout" 
        -- error("ERRCODE[-7]Timeout when waiting for ".. tostring(det) ..". Time out due to: "..tostring(msg))
      end
    end
  end

  console._Dut_Read_String_()
  if(sequence.param1 == "starting command prompt") then
  else  
      console._Dut_Set_Detect_String_("%:%-%)")
  end    
  --time_utils.delay(1000)

  return true, true
end


--- Parse serial output from the previous diags command and extract values
--  using a parsing template.
-- @tparam sequence_object sequence FCT test sequence table
-- @param global_var_table table full of "global" test variables
-- @return result of parsing the serial 
-- @return passing status (whether the returned data matches the expected value or is within the expected range)
-- @see diags, smokey
-- @raise error
local retry_flag = 0
function dut_module.parse(sequence, global_var_table) --params = {fileName, keyName} --In keyName, including {{}}
  local diags_command = _last_diags_command --_last_diags_command global variant, changed by diags function
  local keyName = string.match(sequence.param1, _PDCA_KEY_MATCH_) --{{Phosphorus_ASIC_odr}},delete {{}}
  
  if keyName == nil then
      return tostring("ERRCODE[-8]Invalid match key name or format: "..sequence.param1)
  end

  if TemplatePatternTable[diags_command] == nil then
    --scan file, and stor key. if the file is scaned, it will not scan again.
    --Also, cant do this when Engine start due to dont know which file need to scan
    parse_template_file(diags_command)
  end

  if TemplatePatternTable[diags_command] == nil then
    global_data.set_from_param(global_var_table, sequence.param2, "NULL")
      return tostring("ERRCODE[-9]Template doesn’t exist for: ".._last_diags_command)
  end

  if TemplatePatternTable[diags_command][keyName] == nil then
    global_data.set_from_param(global_var_table, sequence.param2, "NULL")
      return tostring("ERRCODE[-10]Match key '"..keyName.."' does not exist for command: ".._last_diags_command)
  end
    -- print("*************************")
    -- print("<  DUT returen > : \n"..tostring(_last_diags_response))
    -- print("<  KeyName  > : \n"..tostring(keyName))
    -- print("<  match parttern  > : \n"..tostring(TemplatePatternTable[diags_command][keyName]))
    -- print("<  ASCII  > : ")
    -- for i=1, #_last_diags_response do
    --   io.write(string.format("%02X",string.byte(string.sub(_last_diags_response,i,i))))
    --   io.write(" ")
    -- end
    -- print("*********************\n")
    
    -- for i=1, #_last_diags_response do
    --   io.write(string.format("%02X",string.byte(string.sub(_last_diags_response,i,i))))
    --   io.write(" ")
    -- end
    -- print("*********************\n")
  local value = string.match(_last_diags_response, TemplatePatternTable[diags_command][keyName])
  if value == nil then 
    if(retry_flag < 1) then
        retry_flag = retry_flag + 1
        local retry_cmd = _last_diags_command
        dut_module.diags({param1=retry_cmd,timeout=(tonumber(sequence.timeout)-500)}) 
        dut_module.parse({param1=sequence.param1})
    end    
    retry_flag = 0
    global_data.set_from_param(global_var_table, sequence.param2, "NULL")
    --error("ERRCODE[-11]Could not match pattern for '"..keyName.."' for command: ".._last_diags_command)
    value = "--FAIL--ERRCODE[-11]Could not capture variable for '"..keyName.."' for command: ".._last_diags_command .." DUT Got:"..string.gsub(tostring(_last_diags_response),"[\r\n]+"," ")
    print("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~DUT parse failed~~~~~~~~~~~~~~~~~~~~~~~~~~")
    print("DUT response:");
    print(_last_diags_response);
    print("Match String:")
    print(TemplatePatternTable[diags_command][keyName])
    print("Diags Command:")
    print(_last_diags_command)
    print("KeyName:")
    print(keyName)
    print("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~DUT parse~~~~~~~~~~~~~~~~~~~~~~~~~~")
  end

  value = string.gsub(tostring(value), "[\r\n]", "") 
  global_data.set_from_param(global_var_table, sequence.param2, value)

  if(keyName == "mlbsn") then
      if(string.len(value) ~= 17) then
          value = "--FAIL--ERRCODE[-11]Error mlbsn string lenght response. Value is: "..tostring(value)
      end  
      -- print("Before delete messy data: "..tostring(value))
      -- local result_tmp_str = ""
      -- local result_str = ""
      -- for i=1,#value do 
      --     result_tmp_str = string.sub(value,i,i) -- Get data one by one
      --     if(result_tmp_str) then
      --         if(string.byte(result_tmp_str)>47 and string.byte(result_tmp_str)<58) then  --Number
      --             result_str = result_str..result_tmp_str
      --         elseif(string.byte(result_tmp_str)>64 and string.byte(result_tmp_str)<91) then  --A->Z
                   
      --             result_str = result_str..result_tmp_str
      --         else 
      --            print("Delete messy data1: "..tostring(result_tmp_str))
      --         end 
      --     else    
      --         print("Delete messy data2: "..tostring(result_tmp_str))
      --     end  
      -- end  
      -- if(string.len(result_str) == 17) then
      --       value = result_str
      --       print("After delete messy data: "..tostring(value))
      -- else 
      --     local ret = string.find(result_str,"C") --find SN start line.
      --     result_str = string.sub(result_str,ret,19) --Get 17 bit SN.
      --     if(string.len(result_str) == 17) then
      --         value = result_str
      --         print("After delete messy data: "..tostring(value))
      --     else 
      --         return false, false
      --     end   
      -- end  
  end
  -- If we have numerical limits
  local passing = true
  -- if tonumber(sequence.low) or tonumber(sequence.high) then
  --   passing = sequence_utils.check_numerical_limits(value, sequence)
  -- else
  --   passing = sequence_utils.check_string_low_high_limit(value, sequence)
  -- end
  return value, passing
end

local function string_match(string,match_string,option)
  local temp = nil;
  local table={};
  local table_1={};
  if (string == nil) then
    string = "nil";
  end
  if (match_string == nil) then
    match_string = "nil";
  end   
  for chr in string.gmatch(match_string,"(%()") do    --check how many matches we need
    table[#table+1] = chr;
  end
  for chr in string.gmatch(match_string,"(%%%()") do    --check how many matches mixed
    table_1[#table_1+1] = chr;
  end
  if ((#table - #table_1) >= 2) then    --if matches more than 2
    return string.match(string,match_string);
  end
  temp = string.match(string,match_string);
  if (option == 1) then
    temp = tostring(temp);
    if (string.find((string.lower(temp)),"0x")) then    --if the string is hex then return
      return temp;
    end
    temp = string.match(temp,"[+-]?%d*%.?%d*");
    if (temp == nil) or (temp == "") then
      temp = -9999999;
    end
    return tonumber(temp);
  else
    return tostring(temp);
  end
end


function dut_module.ioscmd(sequence, global_var_table)
  local timeout = tonumber(sequence.timeout)  --3sec
  if(timeout == nil) then 
    timeout = 5000
  end
  local value = ""
  if string.find(sequence.param1,"%%") == nil then
    _SOC_P_RESPONSE = ""
    _last_diags_command = sequence.param1
    console._Dut_Read_String_()
    for i = 1,2 do  --retry three times to reduce diags timeout 
      console._Dut_Send_String_(sequence.param1)
      time_utils.delay(1000)
      _SOC_P_RESPONSE = console._Dut_Read_String_()
      if(_SOC_P_RESPONSE) then return true,true end
    end
    return "--FAIL--diags timeout"
  else
    value = string_match(_SOC_P_RESPONSE,sequence.param1)
  end
  value = string.gsub(tostring(value), "[\r\n]", "") 
  if(value == nil) then
      value = "--FAIL--ERRCODE[-11]Could not capture variable for '"..sequence.param1
  end
  local passing = true
  return value, passing
end



function dut_module.iefidiags(sequence, global_var_table)
  local timeout = tonumber(sequence.timeout)  --3sec
  if(timeout == nil) then 
    timeout = 5000
  end
  local value = ""
  if string.find(sequence.param1,"%%") == nil then
    _SOC_P_RESPONSE = ""
    _last_diags_command = sequence.param1
    console._Dut_Read_String_()
    for i = 1,2 do  --retry three times to reduce diags timeout 
      local ret, errmsg = console._Dut_Send_Cmd_(sequence.param1, ((timeout)/2-300))
      _SOC_P_RESPONSE = console._Dut_Read_String_()
      if(ret == 0) then return true,true end
    end
    return "--FAIL--diags timeout"
  else
    print("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~DUT parse~~~~~~~~~~~~~~~~~~~~~~~~~~")
    print("_SOC_P_RESPONSE:".._SOC_P_RESPONSE)
    print("sequence.param1:"..tostring(sequence.param1))
    value = string_match(_SOC_P_RESPONSE,sequence.param1)
    if tostring(value) == "nil" then
      console._Dut_Read_String_()
      local ret, errmsg = console._Dut_Send_Cmd_(_last_diags_command, ((timeout/2)-300))   --maybe also need to retry three times
      _SOC_P_RESPONSE = console._Dut_Read_String_()
      value = string_match(_SOC_P_RESPONSE,sequence.param1)
    end
  end
  value = string.gsub(tostring(value), "[\r\n]", "") 
  if(value == nil) then
      value = "--FAIL--ERRCODE[-11]Could not capture variable for '"..sequence.param1.."' for command: ".._last_diags_command .." DUT Got:"..string.gsub(tostring(_last_diags_response),"[\r\n]+"," ")
  end
  local passing = true
  return value, passing
end



function dut_module.iefisend(sequence, global_var_table)
  local command = sequence.param1
  local timeout = tonumber(sequence.timeout)  --3sec
  if(timeout == nil) then 
      timeout = 5000
  end

  console._Dut_Read_String_()
  if(command) then
      console._Dut_Send_String_(command)
  else
      return tostring("ERROCODE[-6]Problem with diags command string or variable substitution: "..command)
  end
  return true, true
end

--- Evaluate the given lua expression, substituting any variables in {{}} braces.
-- @tparam sequence_object sequence FCT test sequence table
-- @param global_var_table table full of "global" test variables
-- @return result of executing the lua expression
-- @return true
-- @see diags, smokey
-- @raise error
function dut_module.calculate(sequence, global_var_table) 
  -- local command = global_data.sub(global_var_table, sequence.param1)
  local command = global_data.sub(global_var_table, string.match(sequence.param1, "(%(.+%))"))
  if string.match(string.upper(tostring(command)),"NONE") then
    return("--FAIL--ERRCODE[-997]None is invalide : "..tostring(command))
  elseif string.match(string.upper(tostring(command)),"ERRCODE[") then
    return("--FAIL--ERRCODE[-996]Invalide parameter : " .. tostring(command))
  end
  -- local result = loadstring("return ("..command..")")()
  local f,err = loadstring("return ("..command..")")
  assert(f,err)
  local result = f()
  
  global_data.set_from_param(global_var_table, sequence.param2, result)
  
  return result, sequence_utils.check_numerical_limits(result, sequence)
end

function dut_module.store(sequence, global_var_table)
  return dut_module.calculate(sequence, global_var_table)
end


-------------------------
-- SOC
-------------------------
function dut_module.socdiags(sequence)
  local timeout = tonumber(sequence.timeout)  --3sec
  if(timeout == nil) then 
      timeout = 5000
  end

  local command = global_data.sub(global_var_table, sequence.param1)
  console._Dut_Read_String_()
  if(command) then
    console._Dut_Send_String_(command)
    local ret, errmsg = console._Dut_Wait_For_String_(timeout)
    local response = console._Dut_Read_String_()
    if(ret ~= 0) then return "--FAIL--diags timeout" end
    response = string.gsub(tostring(response), "[0-9A-F]+@[RT]%d%s", "")
    return response
  else
    error("ERROCODE[-6]Problem with diags command string or variable substitution: "..command)
  end
end


local _det = ":%-%)"
function dut_module.setdetectstring(sequence)
  local det = sequence.param1
  _det = string_utils.replace_special_char(det)
  console._Dut_Set_Detect_String_(det)
  return true
end

function dut_module.soclongdiags(sequence)
  local timeout = tonumber(sequence.timeout)  --3sec
  if(timeout == nil) then 
      timeout = 5000
  end
  local timer = 1000
  if tonumber(sequence.param2) then
    timer = tonumber(sequence.param2)
  end

  local command = global_data.sub(global_var_table, sequence.param1)
  console._Dut_Read_String_()
  if(command) then
    local resp = {}
    console._Dut_Send_String_(command)
    local starttime = time_utils.get_unix_time_ms()
    while true do
      local currenttime = time_utils.get_unix_time_ms()
      if(currenttime - starttime)>timeout then
        return "--FAIL--Timeout"
      end

      time_utils.delay(timer)
      local tmp = console._Dut_Read_String_()
      resp[#resp+1] = tmp
      if(string.find(tmp, _det)) then
        break
      end
      if tmp == nil or tmp == '' then
        return "--FAIL--PANIC FAIL"
      end
    end

    local response = table.concat( resp, "")
    response = string.gsub(tostring(response), "[0-9A-F]+@[RT]%d%s", "")
    return response
  else
    error("ERROCODE[-6]Problem with diags command string or variable substitution: "..command)
  end
end

function dut_module.potassium(sequence, global_var_table)
  if tonumber(sequence.param1) == 1 then
    local result = console._Connect_()
    -- local potassium_status = console._Loop_Try_Connection_()
    -- if(potassium_status) then
    time_utils.delay(1000)
    console._Dut_Send_String_("\r\n",1000)
      return true, true
    -- else
    --   return false,false 
    -- end 
  else
    console._Close_()
    time_utils.delay(1000)
    return true, true
  end
end

function dut_module.potassium_reset(sequence, global_var_table)
  if tonumber(sequence.param1) == 1 then    --use ktool reset
    nanocom.close()    
    time_utils.delay(1000)
    nanocom.init()
    return true, true
  else       -- relay reset
    time_utils.delay(1000)
    return true, true
  end
end

function dut_module.enteriboot(sequence, global_var_table)
    local dut_send_cmd = "ENTER"
    local enter_boot_timeout = 60000
    if(tonumber(sequence.timeout)> enter_boot_timeout) then enter_boot_timeout = tonumber(sequence.timeout)end
   
    local loop_delay_time1 = 1000
    local loop_delay_time2 = 4000
    local test_count_total = 33
    local test_count = 0
    local loop_string1_time = 500
    local loop_string2_time = 1500
    local loop_string3_time = 9500
    local loop_string1_time_tmp = 0
    local loop_string2_time_tmp = 0
    local loop_string3_time_tmp = 0
    local loop_string1_flag = 0
    local loop_string2_flag = 0
    local loop_string3_flag = 0

    while(1) do

        loop_string1_time_tmp = 0
        loop_string2_time_tmp = 0
        loop_string3_time_tmp = 0
        if(loop_string1_flag) then loop_string1_time_tmp = loop_string1_time end
        if(loop_string2_flag) then loop_string2_time_tmp = loop_string2_time end
        if(loop_string3_flag) then loop_string3_time_tmp = loop_string3_time end

        test_count_total = ((enter_boot_timeout - 2000 - loop_string1_time_tmp - loop_string2_time_tmp - loop_string3_time_tmp)/(loop_delay_time1+loop_delay_time2))

        if(test_count > test_count_total) then
            return "--FAIL--Can't catch [\]] and [login] flag from DUT response."
        end 
        console._Dut_Send_String_(dut_send_cmd)
        time_utils.delay(loop_delay_time1)
        local dut_status_respond = console._Dut_Read_String_()
        if(string.match(dut_status_respond,"login")) then 
            console._Dut_Send_String_(dut_send_cmd)  
            time_utils.delay(500)
            if(string.match(console._Dut_Read_String_(),"login")) then 
                loop_string3_flag = 1
                console._Dut_Send_String_("root")  
                time_utils.delay(500)
                console._Dut_Send_String_("alpine")  
                time_utils.delay(500)
                console._Dut_Send_String_("reboot")  
                time_utils.delay(1500)
                console._Dut_Send_String_("ENTER")  
                time_utils.delay(1500)
                console._Dut_Send_String_("ENTER") 
                time_utils.delay(3500)
                console._Dut_Send_String_("ENTER") 
                time_utils.delay(1500)
                console._Dut_Send_String_("ENTER") 
                time_utils.delay(1500)
                console._Dut_Send_String_("ENTER") 
            else
                loop_string1_flag = 1 
            end    
        elseif(string.match(dut_status_respond,"]")) then
            console._Dut_Send_String_(dut_send_cmd)  
            time_utils.delay(500)
            if(string.match(console._Dut_Read_String_(),"]")) then
                console._Dut_Send_String_("checkbootstatus")  
                time_utils.delay(1000)
                if(string.match(console._Dut_Read_String_(),"ERROR")) then
                    return true,true
                else 
                    loop_string2_flag = 1    
                end  
            else 
                loop_string1_flag = 1 
            end   
        else  
            time_utils.delay(loop_delay_time2)
            test_count = test_count + 1
        end     
    end    
end



return dut_module
