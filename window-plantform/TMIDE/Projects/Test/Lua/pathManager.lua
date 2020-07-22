require "lfs" 

local path = require("pl.path")

function UserHome()
	return path.expanduser '~'
end

function CurrentDir()
	return lfs.currentdir()
end

function JoinPath(p1, p2)
	return path.join(p1, p2)
end

function CurrentPathSeparator()
	local tempStr = path.join("1","2")
	return string.sub(tempStr,2,2)
end

function deleteLastPathComponent(p)
	local t = {}
	local lp = string.gsub(p,"\\","/")
	for v in string.gmatch(lp,"[^/]+")do
		t[#t+1] = v
	end
	if(#t>1) then
		table.remove(t,#t)
	end

	if path.is_windows then
		return table.concat(t,"\\");
	else
		return "/"..table.concat(t,"/")
	end
end

function appendPath(op, np)
	local t = {}
	for v in string.gmatch(op,"[^/]+")do
		t[#t+1] = v
	end

	for v in string.gmatch(np,"[^/]+")do
		t[#t+1] = v
	end
	return "/"..table.concat(t,"/")
end

function lastPathComponent(p)
	return string.match(p, "([^/]+)$")
end

function pathComponents(p)
	local t = {}
	for v in string.gmatch(p,"[^/]+")do
		t[#t+1] = v
	end
	return t
end

function AddModuleSearchPath(script_path)
  local scriptDir = path.dirname(script_path)
  local currPath = CurrentDir()

  if path.isabs(script_path) then
      package.path = package.path .. ";" .. JoinPath(scriptDir, "?.lua")

      if path.is_windows then
          package.cpath = package.cpath .. ";" .. JoinPath(scriptDir, "?.dll")
      else 
          package.cpath = package.cpath .. ";" .. JoinPath(scriptDir, "?.so")
          package.cpath = package.cpath .. ";" .. JoinPath(scriptDir, "?.dylib")
      end
  else
      package.path = package.path .. ";" .. JoinPath(JoinPath(currPath, scriptDir), "?.lua")

      if path.is_windows then
          package.cpath = package.cpath .. ";" .. JoinPath(JoinPath(currPath, scriptDir), "?.dll")
      else 
          package.cpath = package.cpath .. ";" .. JoinPath(JoinPath(currPath, scriptDir), "?.so")
          package.cpath = package.cpath .. ";" .. JoinPath(JoinPath(currPath, scriptDir), "?.dylib")
      end
      
  end 

  print("package.path is " .. package.path)
  print()
  print("package.cpath is " .. package.cpath)
  print()
end

function MakeNFolder(folderPath)
	-- body
	local pathSpliter = "/"
	if path.is_windows then
		pathSpliter = "\\"
	end

	local folderName = {};
	for v in string.gmatch(folderPath..pathSpliter,"(.-)"..pathSpliter) do
		folderName[#folderName+1] = v
	end
	if folderName[#folderName] == "" then table.remove(folderName,#folderName); end
	
	local newFolderPath = "";
	for i,v in ipairs(folderName) do
		if i > 1 then
			newFolderPath = newFolderPath..pathSpliter..v;
			if not path.exists(newFolderPath) then
				os.execute("mkdir ".."\""..newFolderPath.."\"");
			end
		else
			newFolderPath = v
		end
	end
end
