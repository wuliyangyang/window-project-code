

local path = require("pl.path")


require("Global")

-- return nil or {totalSize = "8190M"， usedSize = "7849M", unusedSize = "341M"}
function getMemUsageInfo()
	if not path.is_windows then
		local cmd = "top -n 0 -l 1 | tail -n 10 | grep PhysMem"
		local f = io.popen(cmd)

		if not f then
			print(string.format("io.popen cmd:%s failure", cmd))
			return nil
		end

		local result = f:read("*a")
		print(result)
		f:close()
		f = nil


		local memUsage = result:sub(#"PhysMem:")
		local used = memUsage:sub(2, memUsage:find("used") - 3)
		local unused = memUsage:sub(memUsage:find(",") + 2, memUsage:find("unused") - 3)
		local total = tonumber(used) + tonumber(unused)

		used = used.."M"
		unused = unused.."M"
		total = tostring(total).."M"

		return {usedSize = used, unusedSize = unused, totalSize = total}

	else

		local memUsageInfo = {}
		local totalSize = nil
		local unusedSize = nil
		local cmd = "wmic os get TotalVisibleMemorySize/value"
		local f = io.popen(cmd)

		if not f then
			print(string.format("io.popen cmd:%s failure", cmd))
			return nil
		else	
			local content = f:lines()

			for line in content do
				if line:find("TotalVisibleMemorySize=") then
					totalSize = line:sub(#"TotalVisibleMemorySize==")
				end
			end

			f:close()
			f = nil

			local cmd = "wmic os get FreePhysicalMemory/value"
			local f = io.popen(cmd)
			local content = f:lines()

			for line in content do
				if line:find("FreePhysicalMemory=") then
					unusedSize = line:sub(#"FreePhysicalMemory==")
				end
			end

			f:close()
			f = nil
			local usedMemorySize = tonumber(totalSize)-tonumber(unusedSize)
			memUsageInfo = {usedSize=usedMemorySize,unusedSize=unusedSize,totalSize=totalSize}
			return memUsageInfo
		end
	end
end


-- return nil or {totalSize = "279G", usedSize = "180G", unusedSize = "99G"}
function getDiskUsageInfo()
	if not path.is_windows then
		local cmd = "df -H / | tail -n 1 | awk '{print $2, $3, $4}'"
		local f = io.popen(cmd)
		if not f then
			print(string.format("io.popen cmd:%s failure", cmd))
			return nil
		end

		local result = f:read("*a")
		-- print(result)
		f:close()
		f = nil

		local diskUsageInfo = {}
		local i = 0

		for v in string.gmatch(result, "(%w*)%s") do
			if i == 0 then
				diskUsageInfo.totalSize = v
			elseif i == 1 then
				diskUsageInfo.usedSize = v
			else
				diskUsageInfo.unusedSize = v
			end

			i = i + 1
		end

		return diskUsageInfo

	else
		local diskInfo = {}
		local totalSize = nil	
		local usedSize = nil
		local unusedSize = 0
		local UnSize = nil
		local cmd = "wmic DiskDrive get Size /value"
		local f = io.popen(cmd)

		if not f then
			print(string.format("io.popen cmd:%s failure", cmd))
			return nil
		else
			local content = f:lines()
			for line in content do
				if line:find("Size=") then
					totalSize = tonumber(line:sub(#"Size=="))
				end
			end

			f:close()
			f = nil

			local cmd = "wmic LogicalDisk get FreeSpace/value"
			local f = io.popen(cmd)
			if not f then
				print(string.format("io.popen cmd:%s failure", cmd))
				return nil
			end
			local content = f:lines()
			for line in content do
				if line:find("FreeSpace=") then
					UnSize = tonumber(line:sub(#"FreeSpace%=")) 
					UnSize = UnSize or 0

					if UnSize<totalSize then
						unusedSize = unusedSize+UnSize
					else 
						break 
					end
				end
			end
			f:close()
			f = nil	

			usedSize = totalSize-unusedSize
			diskInfo = {unusedSize=unusedSize,usedSize=usedSize,totalSize=totalSize}
			return diskInfo
		end
	end
end

-- {loadAvg = {1.67, 1.52, 1.54}, cpuUsage = {sys="2.18%", idle="71.84%", user="25.97%"}}
-- 在window上，loadAvg为nil， cpuUsage中sys为nil，全部算到user上， idle=100 - user得到的值；所以window上
-- 的结果eg:{cpuUsage = {idle="71", user="29"}}
function getCpuUsageInfo()
	if not path.is_windows then
		
		local cmd = "top -n 0 -l 3 | tail -n 10"
		local f = io.popen(cmd)
		if not f then
			print(string.format("io.popen cmd:%s failure", cmd))
			return nil
		end

		local content = f:lines()
		local cpuUsageInfo = {}

		for line in content do
			if line:find("Load Avg:") then
				cpuUsageInfo.loadAvg = {}
				local loadAvg = line:sub(#"Load Avg:")
				for str in string.gmatch(loadAvg, "%s*([0-9.]*),?") do
					if #str then
						table.insert(cpuUsageInfo.loadAvg, tonumber(str))
					end
				end
			elseif line:find("CPU usage:") then
				cpuUsageInfo.cpuUsage = {}

				local cpuUsage = line:sub(#"CPU usage:")
				for k, v in string.gmatch(cpuUsage, "%s*([0-9.]*%%) (%a*),?") do
					cpuUsageInfo.cpuUsage[v] = k
				end
			end
		end

		f:close()
		f = nil

		return cpuUsageInfo
	else

		local cpuUsage = {}
		local CPUusage 
		local user = 0
		local idle 
		local cmd = "wmic cpu get loadpercentage/value"
		local f = io.popen(cmd)

		if not f then
			print(string.format("io.popen cmd:%s failure", cmd))
			return nil
		end

		local content = f:lines()
		for line in content do
			if line:find("LoadPercentage=") then
				CPUusage = line:sub(#"LoadPercentage==")
				user = tonumber(CPUusage)
				user = user or 0
				idle = 100-user
				cpuUsage = {user=user,idle=idle}
				return cpuUsage
			end	
		end	
		f:close()
		f = nil
	end
end



function test()
	local result = getMemUsageInfo()
	for k,v in pairs(result) do
		print(k,v)
	end

	result = getDiskUsageInfo()
	for k,v in pairs(result) do
		print(k,v)
	end

	result = getCpuUsageInfo()
	for k,v in pairs(result) do
		print(k)
		if type(v) == "table" then
			for k,v in pairs(v) do
				print("\t", k,v)

			end
		else
			print(k,v)
		end
	end
end


