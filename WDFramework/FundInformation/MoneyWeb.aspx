<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MoneyWeb.aspx.cs" Inherits="WDFramework.MoneyWeb" %>
<%@ Register Assembly="FineUI" Namespace="FineUI" TagPrefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body oncontextmenu='return false'>
    <%--取消鼠标右键的点击--%>
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <%--  --%>

        <x:Panel ID="Panel1" runat="server" BodyPadding="5px" EnableBackgroundColor="true"
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start"
            ShowHeader="false" Title="用户管理">
            <Items>

                <x:Panel ID="Panel3" runat="server" Height="320px"  ShowBorder="True" EnableCollapse="true" AutoScroll="true"
                    Layout="Column" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5"
                    BoxConfigChildMargin="0 5 0 0" ShowHeader="false">
                    <Items>

                        <x:Panel ID="Panel4" Title="经费基本信息" BoxFlex="1" runat="server" ColumnWidth="100%"
                            BodyPadding="40px" ShowBorder="false" ShowHeader="false">
                            <Items>

                                <x:Label ID="Label12" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel12" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>

                                        <x:Label ID="tb_HorGProportion" Width="130px" runat="server" ShowLabel="false" Text="横向项目管理费比例：">
                                        </x:Label>
                                        <x:TextBox ID="tb_HorGProportion2" CssStyle="text-align:center" ShowLabel="true" MaxLength="2" Label="横向项目管理费比例" Width="200px" runat="server" Required="true" RegexPattern="NUMBER" TabIndex="1">
                                        </x:TextBox>
                                        <x:Label ID="Label1" Width="10px" runat="server" ShowLabel="false" Text=" %">
                                        </x:Label>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label4" runat="server" Label="Label" Text=" " Height="40px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel14" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>

                                        <x:Label ID="tb_VerGProportion" Width="130px" runat="server" ShowLabel="false" Text="纵向项目管理费比例：">
                                        </x:Label>
                                        <x:TextBox ID="tb_VerGProportion2" CssStyle="text-align:center" ShowLabel="true" MaxLength="2" Label="纵向项目管理费比例" Width="200px" RegexPattern="NUMBER" runat="server" Required="true" TabIndex="2">
                                        </x:TextBox>
                                        <x:Label ID="Label3" Width="10px" runat="server" ShowLabel="false" Text=" %"></x:Label>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label8" runat="server" Label="Label" Text=" " Height="40px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel7" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>

                                        <x:Label ID="lb_school" Width="130px" runat="server" ShowLabel="false" Text="校内项目管理费比例：">
                                        </x:Label>
                                        <x:TextBox ID="tb_school" CssStyle="text-align:center" ShowLabel="true" MaxLength="2" Label="校内项目管理费比例" Width="200px" runat="server" RegexPattern="NUMBER" Required="true" TabIndex="2">
                                        </x:TextBox>
                                        <x:Label ID="Label5" Width="10px" runat="server" ShowLabel="false" Text=" %"></x:Label>
                                    </Items>
                                </x:Panel>
                            </Items>
                        </x:Panel>

                    </Items>
                </x:Panel>

                <x:Toolbar ID="Toolbar2" runat="server" Position="Footer">
                    <Items>
                        <x:Label ID="Label17" runat="server" Text=" " Width="150px"></x:Label>
                        <x:Button ID="btn_Save" Text="保存" Icon="Accept" Size="Medium" Type="Submit" ValidateForms="Panel3" ValidateTarget="Top"
                            runat="server" ConfirmText="确认保存？" OnClick="btn_Save_Click" >
                        </x:Button>
                        <x:Button ID="btn_Delete" ConfirmText="确认重置？" Text="重置" Size="Medium" Icon="Delete" Type="Submit" ValidateTarget="Top"
                            runat="server" OnClick="Delete_Onclick" >
                        </x:Button>
                    </Items>
                </x:Toolbar>

            </Items>
        </x:Panel>

        <x:Window ID="Window1" Popup="false" EnableIFrame="true" IFrameUrl="#" runat="server"
            EnableMaximize="true" EnableResize="true" Height="450px" Width="750px" Title="添加">
        </x:Window>

        <x:Window ID="Window2" Title="查询" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="true" EnableResize="true" Target="Parent"
            IsModal="True" Width="750px" Height="450px">
        </x:Window>
        <x:Label ID="labResult" Visible="false" runat="server">
        </x:Label>
    </form>
</body>
</html>
