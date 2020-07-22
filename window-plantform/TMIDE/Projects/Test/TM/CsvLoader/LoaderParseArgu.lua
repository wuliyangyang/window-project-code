
local lapp = require("pl.lapp")
local path = require("pl.path")

local VERSION = "0.0.1"

--===========================--
-- deal with argument
--===========================--
local args = lapp [[
Csv Loader
    -f,--file       (default TesterConfig/zmqports.json)   The path of zmqports
    -p,--profile    (default profile/PROX__TEST.csv)          The path of profile
    -e,--echo       (default 1)                            Echo receive and result in terminal
    -m,--modules    (number default 1)                     The number of fixture
    -s,--slots      (number default 1)                     Slots of the fixture
    <updates...>    (default "X=Y")                        Series of X=Y pairs to update the CONFIG table with
]]


if not args.profile or not args.file 
   or type(args.profile) ~= "string" or type(args.file) ~= "string" 
   or args.profile == "" or args.file == "" 
   or not path.isfile(args.profile) or not path.isfile(args.file) then
    error("The argument is error; please check!")
end

if type(args.modules) ~= "number" or type(args.slots) ~= "number" then
    error("The argument 'modules' or 'slots' maybe error; please check!")
end

if not path.isabs(args.profile) then
    args.profile = JoinPath(CurrentDir(), args.profile)
end

if not path.isabs(args.file) then
    args.file = JoinPath(CurrentDir(), args.file)
end

return args