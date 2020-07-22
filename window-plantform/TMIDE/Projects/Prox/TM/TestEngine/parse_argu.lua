
local lapp = require("pl.lapp")
local path = require("pl.path")

local VERSION = "0.0.1"

--===========================--
-- deal with argument
--===========================--
local args = lapp [[
Test Engine
    -d,--dir        (string)                               The path of Driver directory
    -f,--file       (default TesterConfig/zmqports.json)   The path of zmqports
    -u,--uut        (number default 0)                     UUT slot number (used for IP/Port selection)
    -e,--echo       (default 1)                            Echo receive and result in terminal
    -m,--module     (number default 0)                     The number of fixture
    -s,--slots      (number default 1)                     Slots of the fixture
    <updates...>    (default "X=Y")                        Series of X=Y pairs to update the CONFIG table with
]]


if not args.dir or not args.file 
   or type(args.dir) ~= "string" or type(args.file) ~= "string" 
   or args.dir == "" or args.file == "" 
   or not path.isdir(args.dir) or not path.isfile(args.file) then
    error("The argument is error; please check!")
end

if type(args.uut) ~= "number" or type(args.module) ~= "number" or type(args.slots) ~= "number"
  or not (args.uut < args.slots) then
    error("The argument 'uut' or 'module' or 'slots' maybe error; please check!")
end

if not path.isabs(args.file) then
    args.file = JoinPath(CurrentDir(), args.file)
end

return args