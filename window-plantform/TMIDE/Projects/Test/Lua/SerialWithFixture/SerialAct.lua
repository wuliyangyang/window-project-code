require "SerialWithFixture"

_device = SerialWithFixture:new()
print("_device:",_device:Open("COM3","115200,0,8,2"))
print("SetDetect:",_device:SetDetectString("\r\n"))
print("CreateZmqServer",_device:CreateZmqServer("tcp://127.0.0.1:6470","tcp://127.0.0.1:6471"))
print("CreateZmqClient",_device:CreateZmqClient("tcp://127.0.0.1:6900",3000))
print("SetInfo",_device:SetFixtureInfo("Start","{\"function\":\"START\"}"))

io.read()
print("Write:",_device:WriteString("Hello\r\n"))
print("Wait:",_device:WaitDetect(3000))
print("Read:",_device:ReadString())
io.read()
print("Write Start",_device:WriteString("Start"))
io.read()
print("close:",_device:Close())