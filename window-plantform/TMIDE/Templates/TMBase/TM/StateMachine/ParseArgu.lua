
local lapp = require("pl.lapp")
local path = require("pl.path")


--===========================--
-- deal with argument
--===========================--
local args = lapp [[
State Machine
    -f,--file     (string)                          Path of zmqports file
    -e,--enable     (default all)                   Enable slot to test(start from 0)
    -s,--slots      (number default 1)              Slots of the fixture
    -m,--module     (number default 0)              The number of fixture
    <updates...>    (default "X=Y")                 Series of X=Y pairs to update the CONFIG table with
]]



if not path.isfile(args.file) then
    error("The zmqport file is not exist; please check!")
elseif type(args.slots) ~= "number" then
    error("argument of slots must be type of number")
elseif args.slots < 1 then
    error("argument of slots must be not less than one")
end

if type(args.module) ~= "number" or type(args.slots) ~= "number" then
    error("The argument 'module' or 'slots' maybe error; please check!")
end

return args