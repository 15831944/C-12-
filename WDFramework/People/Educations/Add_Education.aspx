<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add_Education.aspx.cs" Inherits="WDFramework.People.Educations.Add_Education" %>

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

                        <x:Panel ID="Panel3" Title="学历信息" BoxFlex="1" runat="server" ColumnWidth="100%"
                            BodyPadding="5px" ShowBorder="false" ShowHeader="false">
                            <Items>
                                <x:Panel ID="Panel7" ShowHeader="false" CssClass="formitem" ShowBorder="false" Height="40"
                                    Layout="Column" runat="server">
                                    <Items>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel12" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label7" Width="70px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="姓">
                                        </x:Label>
                                        <x:Label ID="Subject" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="名：">
                                        </x:Label>
                                        <x:TextBox ID="UserInfoName" MaxLength="20" MaxLengthMessage="最多输入20个字符" ShowLabel="true" Label="姓名" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="1">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label2" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel4" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label9" Width="70px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="学">
                                        </x:Label>
                                        <x:Label ID="PublicJournalName" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="位：">
                                        </x:Label>
                                        <x:DropDownList AutoPostBack="false" Label="学位" EnableSimulateTree="true" Required ="true"  ShowLabel="true" Width="195px" CssClass="marginr" TabIndex="2"
                                            runat="server" ID="DropDownListDegree">
                                        </x:DropDownList>
                                    </Items>
                                </x:Panel>

                                

                                <x:Label ID="Label19" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel9" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label20" Width="70px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="学">
                                        </x:Label>
                                        <x:Label ID="Label21" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="位证书号：">
                                        </x:Label>
                                        <x:TextBox ID="DegreeNumber" MaxLength="20" MaxLengthMessage="最多输入20个字符" ShowLabel="true" Label="学位证书号" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="3">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>

                               
                                 <x:Label ID="Label24" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel10" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label22" Width="70px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="毕">
                                        </x:Label>
                                        <x:Label ID="Label23" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="业证书号：">
                                        </x:Label>
                                        <x:TextBox ID="GraduateNumber" MaxLength="20" MaxLengthMessage="最多输入20个字符" ShowLabel="true" Label="毕业证书号" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="4">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>
                                 <x:Label ID="Label17" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel21" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label10" Width="70px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="取">
                                        </x:Label>
                                        <x:Label ID="EndPageNum" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="得时间：">
                                        </x:Label>
                                        <x:DatePicker runat="server" Label="取得时间" EmptyText="请选择取得时间" EnableEdit="false" Width="195px" CssClass="marginr" Required="true" TabIndex="5"
                                            ID="DatePickerEduTime">
                                        </x:DatePicker>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label1" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel5" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label8" Width="70px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="学">
                                        </x:Label>
                                        <x:Label ID="Label3" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="校名称：">
                                        </x:Label>
                                        <x:TextBox ID="SchoolName" MaxLength="20" MaxLengthMessage="最多输入20个字符" ShowLabel="true" Label="学校名称" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="6">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>

                                

                                
                                <x:Label ID="Label14" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel17" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label13" Width="70px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="学">
                                        </x:Label>
                                        <x:Label ID="ImpactFactor" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="院：">
                                        </x:Label>
                                        <x:TextBox ID="College" MaxLength="20" MaxLengthMessage="最多输入20个字符" ShowLabel="true" Label="学院" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="7">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>



                                <x:Label ID="Label11" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel14" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label15" Width="70px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="系">
                                        </x:Label>
                                        <x:Label ID="StartPageNum" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="：">
                                        </x:Label>
                                        <x:TextBox ID="Series" ShowLabel="true" Label="系" MaxLength="20" MaxLengthMessage="最多输入20个字符" Width="200px" Required="true" CssClass="marginr" runat="server" TabIndex="8">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>



                                <x:Label ID="Label4" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel6" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label16" Width="70px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="专">
                                        </x:Label>
                                        <x:Label ID="Label5" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="业：">
                                        </x:Label>
                                        <x:TextBox ID="Major" ShowLabel="true" Label="专业" Width="200px" MaxLength="20" MaxLengthMessage="最多输入20个字符" Required="true" CssClass="marginr" runat="server" TabIndex="9">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label6" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel27" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label18" Width="70px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="保">
                                        </x:Label>
                                        <x:Label ID="Label32" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="密级别：">
                                        </x:Label>
                                        <x:DropDownList Label="保密等级" AutoPostBack="false" Required="true" EnableSimulateTree="true" TabIndex="8"
                                            ShowRedStar="true" runat="server" ID="DropDownListSecrecyLevel" Width="195px">
                                            <x:ListItem Text="公开" Value="公开" />

                                        </x:DropDownList>
                                    </Items>
                                </x:Panel>
                                <x:Panel ID="Panel8" ShowHeader="false" CssClass="formitem" ShowBorder="false" Height="40"
                                    Layout="Column" runat="server">
                                    <Items>
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
                                <x:Button ID="Save" Text="保存" runat="server" Size="Medium" Icon="Add" OnClick ="Save_Click"  ConfirmText="确定保存？" ConfirmTarget="Top" Type="Submit" ValidateForms="Panel2">
                                </x:Button>
                                <x:Button ID="Reset" Text="重置" runat="server" Size="Medium" OnClick ="Reset_Click"  Icon="Delete" ConfirmText="确定重置？" ConfirmTarget="Top">
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
