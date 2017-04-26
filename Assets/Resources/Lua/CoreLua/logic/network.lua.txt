require "common/protocal"

Network = {};
local this = Network;

local transform;
local gameObject;

local islogging = false;

function Network.Start() 
    warn("Network.Start!!");
end

--Socket消息--
function Network.OnSocket(key, buffer)
	if key == Connect then this.OnConnect(); end
	if key == Exception then this.OnException(); end
	if key == Disconnect then this.OnDisconnect(); end
	---------------------------------------------
	if key == Login then this.OnLogin(buffer); end
	--ModuleName.function--
end

--当连接建立时--
function Network.OnConnect() 
    warn("Game Server connected!!");
end

--异常断线--
function Network.OnException() 
    islogging = false; 
    io.networkManager:SendConnect();
   	error("OnException------->>>>");
end

--连接中断，或者被踢掉--
function Network.OnDisconnect() 
    islogging = false; 
    error("OnDisconnect------->>>>");
end

--当登录时--
function Network.OnLogin(buffer) 
    local result = buffer:ReadByte();
    if result == 0 then return; end
    islogging = true;
    local str = buffer:ReadString();
    warn('OnLogin---->>>'..str);
    createPanel("Message"); --Lua里创建面板
end

