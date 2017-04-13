<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Update_WorkExperience.aspx.cs" Inherits="WDFramework.People.WorkExperiences.Update_WorkExperience" %>



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

                <x:Panel ID="Panel2" runat="server" ShowBorder="True" EnableCollapse="true" AutoScroll="true"
                    Layout="Column" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5" Height="320px"
                    BoxConfigChildMargin="0 5 0 0" ShowHeader="false">
                    <Items>

                        <x:Panel ID="Panel3" Title="工作经历信息" BoxFlex="1" runat="server" ColumnWidth="100%"
                            BodyPadding="5px" ShowBorder="false" ShowHeader="false">
                            <Items>
                                 <x:Label ID="Label22" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel12" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label4" Width="60px" runat="server" CssStyle="text-align:right" CssClass="marginr" ShowLabel="false" Text="姓名">
                                        </x:Label>
                                        <x:Label ID="Subject" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="：">
                                        </x:Label>
                                        <x:TextBox ID="UserInfoName" MaxLength="20" MaxLengthMessage="最多输入20个字符" ShowLabel="true" Label="姓名" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="1">
                                        </x:TextBox>

                                    </Items>
                                </x:Panel>

                                 <x:Label ID="Label11" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel14" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label15" Width="60px" runat="server" CssStyle="text-align:right" CssClass="marginr" ShowLabel="false" Text="工作">
                                        </x:Label>
                                        <x:Label ID="StartPageNum" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="单位：">
                                        </x:Label>
                                        <x:TextBox ID="WorkUnit" MaxLength="40" MaxLengthMessage="最多输入40个字符" ShowLabel="true" Label ="工作单位" Width="200px" Required="true" CssClass="marginr" runat="server" TabIndex="2">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label17" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel6" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label18" Width="60px" runat="server" CssStyle="text-align:right" CssClass="marginr" ShowLabel="false" Text="兼职">
                                        </x:Label>
                                        <x:Label ID="Label19" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="单位：">
                                        </x:Label>
                                        <x:TextBox ID="PartTimeUnit" MaxLength="40" MaxLengthMessage="最多输入40个字符" Label ="兼职单位" ShowLabel="true" Width="200px" Required="true" CssClass="marginr" runat="server" TabIndex="3">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label7" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel7" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label10" Width="60px" runat="server" CssStyle="text-align:right" CssClass="marginr" ShowLabel="false" Text="职务">
                                        </x:Label>
                                        <x:Label ID="Label8" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="：">
                                        </x:Label>
                                        <x:TextBox ID="Post" ShowLabel="true" MaxLength="20" MaxLengthMessage="最多输入20个字符" Label="职务" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="4">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label14" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel17" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label13" Width="60px" runat="server" CssStyle="text-align:right" CssClass="marginr" ShowLabel="false" Text="职称">
                                        </x:Label>
                                        <x:Label ID="ImpactFactor" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="：">
                                        </x:Label>
                                        <x:TextBox ID="JobTitle" MaxLength="20" MaxLengthMessage="最多输入20个字符" ShowLabel="true" Label="职称" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="5">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label1" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel5" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label5" Width="60px" runat="server" CssStyle="text-align:right" CssClass="marginr" ShowLabel="false" Text="开始">
                                        </x:Label>
                                        <x:Label ID="Label3" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="时间：">
                                        </x:Label>
                                        <x:DatePicker runat="server" Label="开始时间" EmptyText="请选择日期" Required="true" EnableEdit="false" Width="195px" CssClass="marginr" TabIndex="6"
                                            ID="DatePickerStartTime">
                                        </x:DatePicker>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label2" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel4" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label9" Width="60px" runat="server" CssStyle="text-align:right" CssClass="marginr" ShowLabel="false" Text="结束">
                                        </x:Label>
                                        <x:Label ID="PublicJournalName" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="时间：">
                                        </x:Label>
                                        <x:DatePicker runat="server" Label="结束时间" EmptyText="请选择日期" Width="195px" CssClass="marginr" EnableEdit="false" TabIndex="7"
                                            ID="DatePickerEndTime" Required="true">
                                        </x:DatePicker>
                                    </Items>
                                </x:Panel>
                                

                               

                                <x:Label ID="Label6" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel27" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label16" Width="60px" runat="server" CssStyle="text-align:right" CssClass="marginr" ShowLabel="false" Text="保密">
                                        </x:Label>
                                        <x:Label ID="Label32" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="级别：">
                                        </x:Label>
                                        <x:DropDownList Label="保密等级" AutoPostBack="false" Required="true" EnableSimulateTree="true" TabIndex="8"
                                            ShowRedStar="true" runat="server" ID="DropDownListSecrecyLevel" Width="195px">
                                            <x:ListItem Text="公开" Value="公开" />
                                        </x:DropDownList>
                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label20" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel9" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label23" Width="60px" runat="server" CssStyle="text-align:right" CssClass="marginr" ShowLabel="false" Text="备注">
                                        </x:Label>
                                        <x:Label ID="Label24" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="：">
                                        </x:Label>
                                        <x:TextArea runat="server" MaxLength="200" MaxLengthMessage="最多输入200个字符" Label ="备注" ShowLabel="true" CssStyle ="overflow-y:scroll"  ID="Remark" Width="195px" Height="80px" TabIndex="9">
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
                                <x:Button ID="Save" Text="保存" runat="server" Size="Medium" Icon="Add" ConfirmText="确定保存？" OnClick ="Save_Click"  ConfirmTarget="Top" Type="Submit" ValidateForms="Panel2">
                                </x:Button>
                                <x:Button ID="Reset" Text="重置" runat="server" Size="Medium" Icon="Delete" ConfirmText="确定重置？" OnClick ="Reset_Click"   ConfirmTarget="Top">
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
