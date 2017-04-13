<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Update_ProjectStaffDevote.aspx.cs" Inherits="WDFramework.Projects.Update_ProjectStaffDevote" %>


<%@ Register Assembly="FineUI" Namespace="FineUI" TagPrefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../res/css/main.css" rel="stylesheet" type="text/css" />
</head>
<body oncontextmenu='return false'>
    <%--取消鼠标右键的点击--%>
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <x:Panel ID="Panel1" runat="server" BodyPadding="5px" EnableBackgroundColor="true"
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" AutoScroll="true"
            ShowHeader="false" Title="用户管理">
            <Items>


                <%--Height="350px" Width="850px"  --%>
                <x:Panel ID="Panel2" runat="server" ShowBorder="false" EnableCollapse="true" AutoScroll="true"
                    Layout="Column" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5" Height="330px"
                    BoxConfigChildMargin="0 5 0 0" ShowHeader="false">
                    <Items>

                        <x:Panel ID="Panel3" Title="项目概要" runat="server" ColumnWidth="100%" ShowBorder="false" BodyPadding="30px"
                            ShowHeader="false">
                            <Items>
                                <x:Panel ID="Panel12" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="MissionName2" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="人员名称：">
                                        </x:Label>
                                        <x:TextBox ID="UserInfoName" Label="人员名称" ShowLabel="true" Width="200px" CssClass="marginr" runat="server">
                                        </x:TextBox>

                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label1" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel16" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>

                                        <x:Label ID="Label4" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="投入时间：">
                                        </x:Label>
                                        <x:DatePicker runat="server" Label="投入时间" EmptyText="请选择投入时间" EnableEdit="false" Width="195px" CssClass="marginr"
                                            ID="DatePickerDevoteTime">
                                        </x:DatePicker>


                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label2" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel100" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>

                                        <x:Label ID="Label3" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="退出时间：">
                                        </x:Label>
                                        <x:DatePicker runat="server" Label="退出时间" EmptyText="请选择退出时间" Width="195px" EnableEdit="false" CssClass="marginr"
                                            ID="DatePickerExitTime">
                                        </x:DatePicker>


                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label5" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel13" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="ProjectID" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="所属项目：">
                                        </x:Label>
                                        <x:DropDownList AutoPostBack="false" Label="所属项目" EnableSimulateTree="true" ShowLabel="true" Width="195px" CssClass="marginr"
                                            runat="server" ID="DropDownListProjectID">
                                        </x:DropDownList>
                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label126" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel9" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="SecrecyLevel" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="保密级别：">
                                        </x:Label>
                                        <x:DropDownList Label="保密等级" AutoPostBack="false" EnableSimulateTree="true"
                                            runat="server" ID="DropDownListSecrecyLevel" Width="195px">
                                        </x:DropDownList>
                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label18" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel4" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Sort2" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="排序：">
                                        </x:Label>
                                        <x:DropDownList Label="排序" AutoPostBack="false" EnableSimulateTree="true"
                                            runat="server" ID="DropDownListSort" Width="200px">
                                        </x:DropDownList>

                                    </Items>
                                </x:Panel>

                                 <x:Label ID="Label6" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel5" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label7" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="项目承担任务及完成情况：">
                                        </x:Label>
                                        <%--  <x:TextBox ID="Sort"  Label="排序" ShowLabel="true" Required ="true"  Width="150px" CssClass="marginr" runat="server" >
                                        </x:TextBox>--%>
                                        <x:TextBox ID="tb_ProjectCompletion" Label="项目承担任务及完成情况" ShowLabel="true" Width="200px" CssClass="marginr" runat="server">
                                        </x:TextBox>

                                    </Items>
                                </x:Panel>
                            </Items>
                        </x:Panel>

                    </Items>
                </x:Panel>
                <x:Panel ID="Panel87" runat="server" Height="40px" ShowBorder="false" EnableCollapse="true"
                    BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5"
                    BoxConfigChildMargin="0 5 0 0" ShowHeader="false" Width="750px">
                    <Items>
                        <x:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <x:Label ID="Label12" runat="server" Label="Label" Text=" " Width="150px"></x:Label>
                                <x:Button ID="Save" Text="保存" runat="server" Icon="Add" Size="Medium" ConfirmText="确定保存？" ConfirmTarget="Top" Type="Submit" OnClick="Save_Click" ValidateForms="Panel2">
                                </x:Button>
                                <x:Button ID="Reset" Text="重置" runat="server" Icon="Delete" Size="Medium" ConfirmText="确定重置？" ConfirmTarget="Top" OnClick="Reset_Click">
                                </x:Button>
                            </Items>
                        </x:Toolbar>
                    </Items>
                </x:Panel>
            </Items>
        </x:Panel>
        <x:Window ID="WindowStaff" Popup="false" EnableIFrame="true" IFrameUrl="#" runat="server"
            EnableMaximize="true" EnableResize="true" Height="450px" Width="750px" Title="添加人员">
        </x:Window>
    </form>
</body>
</html>
