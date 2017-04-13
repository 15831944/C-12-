<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WDFramework.Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>科研信息管理系统</title>
</head>
<body oncontextmenu='return false' onkeydown="return (event.keyCode!=8)">
    <form id="form1" runat="server">
    <x:PageManager ID="PageManager1"  AutoSizePanelID="RegionPanel1" runat="server" ></x:PageManager>
    <x:RegionPanel ID="RegionPanel1" ShowBorder="false" runat="server">
        <Regions>
            <x:Region ID="Region1"  Margins="0 0 0 0" ShowBorder="false" Height="60px" ShowHeader="false"
                Position="Top" Layout="Fit" runat="server">
                <Toolbars>
                    <x:Toolbar ID="Toolbar3" Position="Bottom" runat="server"> 
                        <Items>
                             <x:Label ID="People" Width="100px" runat="server"   CssClass="marginr" ShowLabel="false" >
                                        </x:Label> 
                            <x:ToolbarText ID="txtUser" runat="server">
                            </x:ToolbarText>
                         <%--   <x:ToolbarSeparator ID="ToolbarSeparator1" runat="server" />--%>
                            <x:ToolbarText ID="txtOnlineUserCount" runat="server">
                            </x:ToolbarText>
                         <%--   <x:ToolbarSeparator ID="ToolbarSeparator2" runat="server" />--%>
                            <x:ToolbarText ID="txtCurrentTime" runat="server">
                            </x:ToolbarText>
                            <x:ToolbarFill ID="ToolbarFill3" runat="server" />
                            <%--  <x:Button ID="btnShowHideHeader" runat="server" Icon="SectionExpanded" ToolTip="隐藏标题栏"
                                    EnablePostBack="false">--%>
                            <%--  </x:Button>--%>
                          <%--  <x:Button ID="BtnHomepage" EnablePostBack="false" Icon="house" Text="首页" runat="server">
                            </x:Button>
                             <x:ToolbarSeparator ID="ToolbarSeparator4" runat="server" />--%>
                             
                            <x:Button ID="BtnUserInfo"  Icon="user" Text="个人信息"  runat="server" >
                            </x:Button>
                            <x:ToolbarSeparator ID="ToolbarSeparator4" runat="server" />
                            <x:Button ID="ButtonOption" EnablePostBack="false" Icon="wrench" Text="修改密码" runat="server">
                            </x:Button>
                            <x:ToolbarSeparator ID="ToolbarSeparator3" runat="server" />
                            <x:Button ID="btnRefresh" runat="server" Text="刷新"  Icon="ArrowRotateClockwise" ToolTip="刷新主区域内容" OnClick="btnRefresh_Click">
                            </x:Button>
                            <x:Button ID="btnTool" EnablePostBack="false" Text="工具"  runat="server">
                                <Menu ID="Menu1" runat="server">
                                    <x:MenuButton ID="btnExcel" runat="server" Text="Excel导入">
                                    </x:MenuButton>
                                    <%-- <x:MenuButton ID="createShoutCut" runat="server" Text="创建桌面快捷方式" OnClick="createShoutCut_Click1"></x:MenuButton>--%>
                                </Menu>
                            </x:Button>
                          <%--  <x:ToolbarSeparator ID="ToolbarSeparator4" runat="server" />
                            <x:Button ID="btnHelp" EnablePostBack="true" Icon="Help" Text="帮助" runat="server" OnClick="Btnhelp_Click">
                            </x:Button>--%>
                            <x:DatePicker runat="server" Required="true" Label="日期" DateFormatString="yyyy-MM-dd"
                                ID="DatePicker1" ShowRedStar="True">
                            </x:DatePicker>
                            <x:ToolbarSeparator ID="ToolbarSeparator5" runat="server" />
                            <x:Button ID="btnExit" runat="server" Text="退出" ConfirmText="确定退出系统？" OnClick="btnExit_Click">
                            </x:Button>
                        </Items>
                    </x:Toolbar>
                </Toolbars>
                <Items>
                    <x:ContentPanel ShowBorder="false" ShowHeader="false" BodyStyle="background-color:#1C3E7E;"
                        ID="ContentPanel1" runat="server" Visible="true">
                        <div style="font-size: 20px; color: White; font-weight: bold; padding: 5px 10px;">
                            <a href="default.aspx" style="color: White; text-decoration: none;">科研信息管理系统</a>
                            <asp:Literal ID="litProductVersion" runat="server"></asp:Literal>
                        </div>
                    </x:ContentPanel>
                </Items>
            </x:Region>
            <x:Region ID="regionLeft" Split="true" Icon="Outline" EnableCollapse="true" Width="200px"
                ShowHeader="true" Title="系统菜单" Layout="Fit" Position="Left" runat="server">
            </x:Region>
            <x:Region ID="mainRegion" ShowHeader="false" Layout="Fit" Margins="0 0 0 0" Position="Center"
                runat="server">
                <Items>
                    <x:TabStrip ID="mainTabStrip" EnableTabCloseMenu="true" ShowBorder="false" runat="server">
                        <Tabs>
                            <x:Tab ID="Tab1" Title="首页" EnableIFrame="true" IFrameUrl="~/WebForms/HomePage.aspx"
                                Icon="House" runat="server">
                               
                            </x:Tab>
                        </Tabs>
                    </x:TabStrip>
                </Items>
            </x:Region>
        </Regions>
    </x:RegionPanel>
    <x:Window ID="Window1"  runat="server" Popup="false"
        WindowPosition="Center" IsModal="true" Title="Popup Window 1" EnableMaximize="false"
        EnableResize="false" Target="Self" EnableIFrame="true" IFrameUrl="about:blank"
        Height="300px" Width="350px" OnClose="Window1_Close">
    </x:Window>
           <x:Window ID="WindowADD" Popup="false" EnableIFrame="true"  runat="server"
            EnableMaximize="false" EnableResize="false" Height="450px" Width="800px" Title="个人消息" OnClose="WindowADD_Close" CloseAction="HidePostBack">
        </x:Window>
        <x:Window ID="Window_Import" Popup="false" EnableIFrame="true" runat="server" 
            EnableMaximize="false" EnableResize="false" Height="250px" Width="350px">
        </x:Window>
    </form>
</body>
</html>
