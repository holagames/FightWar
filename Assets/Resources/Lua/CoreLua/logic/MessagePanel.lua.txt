require "common/functions"

MessagePanel = {};
local this = MessagePanel;

local transform;
local gameObject;

--启动事件--发送登录请求--
function MessagePanel.Start()
	warn("Start lua--->>"..this.gameObject.name);

	local panel = this.gameObject:GetComponent('UIPanel');
	panel.depth = 10;	--设置纵深--

	local Button = find('Button');
	UIEventListener.Get(Button).onClick = LuaHelper.VoidDelegate(this.OnClick);
end

--单击事件--
function MessagePanel.OnClick()
	destroy(gameObject);
end

