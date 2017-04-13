<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Update_HonorTitle.aspx.cs" Inherits="WDFramework.People.Honor.Update_HonorTitle" %>



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
                <x:Panel ID="Panel2" runat="server" ShowBorder="True" EnableCollapse="true" AutoScroll="true"
                    Layout="Column" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5" Height="320px"
                    BoxConfigChildMargin="0 5 0 0" ShowHeader="false">
                    <Items>

                        <x:Panel ID="Panel3" Title="荣誉称号信息" BoxFlex="1" runat="server" ColumnWidth="100%"
                            BodyPadding="45px" ShowBorder="false" ShowHeader="false">
                            <Items>
                                <x:Panel ID="Panel12" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Subject" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="姓名：">
                                        </x:Label>
                                        <x:TextBox ID="UserInfoName" MaxLength="20" MaxLengthMessage="最多输入20个字符" ShowLabel="true" Label="姓名" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="1">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label1" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel5" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label3" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="称号名称：">
                                        </x:Label>
                                        <x:TextBox ID="TitleName" MaxLength="20" MaxLengthMessage="最多输入20个字符" ShowLabel="true" Label="称号名称" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="2">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>

                                 <x:Label ID="Label14" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel17" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="ImpactFactor" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="授予部门：">
                                        </x:Label>
                                        <x:TextBox ID="GivDivision" MaxLength="40" MaxLengthMessage="最多输入40个字符" ShowLabel="true" Label="授予部门" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="3">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>
                               
                                <x:Label ID="Label7" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel7" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label8" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="授予时间：">
                                        </x:Label>
                                        <%--<x:TextArea runat="server" ID="TextArea1" Width="150px" Height="60px" AutoGrowHeight="true" AutoGrowHeightMin="100" AutoGrowHeightMax="200">
                                        </x:TextArea>--%>
                                        <x:DatePicker runat="server" Label="授予时间" EmptyText="请选择授予时间" EnableEdit="false" Required="true" Width="195px" CssClass="marginr" TabIndex="4"
                                            ID="DatePickerGiveTime">
                                        </x:DatePicker>
                                    </Items>
                                </x:Panel>
                               

                                 <x:Label ID="Label2" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel4" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="PublicJournalName" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="级别：">
                                        </x:Label>
                                        <x:DropDownList Label="" AutoPostBack="false" Required="true" EnableSimulateTree="true" TabIndex="5"
                                            ShowRedStar="true" runat="server" ID="DropDownListSort" Width="195px">
                                        </x:DropDownList>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label11" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel27" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label32" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="保密级别：">
                                        </x:Label>
                                        <x:DropDownList Label="保密等级" AutoPostBack="false" Required="true" EnableSimulateTree="true" TabIndex="6"
                                            ShowRedStar="true" runat="server" ID="DropDownListSecrecyLevel" Width="195px">
                                            <x:ListItem Text="公开" Value="公开" />
                                        </x:DropDownList>
                                    </Items>
                                </x:Panel>


                                <x:Label ID="Label6" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel14" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                     Layout ="Column"  runat="server">
                                    <Items>
                                        <x:Label ID="StartPageNum" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="备注：">
                                        </x:Label>
                                        <x:TextArea runat="server" MaxLength="200" MaxLengthMessage="最多输入200个字符" ShowLabel="true" ID="Remark" Width="195px" Height="80px" TabIndex="7" CssStyle ="overflow-y:scroll">
                                        </x:TextArea>
                                    </Items>
                                </x:Panel>
                            </Items>
                        </x:Panel>
                    </Items>
                </x:Panel>
                <x:Panel ID="Panel87" runat="server" Height="40px" ShowBorder="True" EnableCollapse="true"
                    BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5"
                    BoxConfigChildMargin="0 5 0 0" ShowHeader="false" Width="750px">
                    <Items>
                        <x:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <x:Label ID="Label12" runat="server" Label="Label" Text=" " Width="150px"></x:Label>
                                <x:Button ID="Save" Text="保存" runat="server" Size="Medium" Icon="Add" OnClick ="Save_Click"  ConfirmText="确定保存？" ConfirmTarget="Top"  ValidateForms="Panel2" Type="Submit">
                                </x:Button>
                                <x:Button ID="Reset" Text="重置" runat="server" Size="Medium" Icon="Delete" OnClick ="Reset_Click"  ConfirmText="确定重置？" ConfirmTarget="Top" >
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
