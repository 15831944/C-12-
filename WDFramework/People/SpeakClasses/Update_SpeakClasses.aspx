<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Update_SpeakClasses.aspx.cs" Inherits="WDFramework.People.SpeakClasses.Update_SpeakClasses" %>




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

                        <x:Panel ID="Panel3" Title="主讲课程信息" BoxFlex="1" runat="server" ColumnWidth="100%"
                            BodyPadding="5px" ShowBorder="false" ShowHeader="false">
                            <Items>
                                <x:Label ID="Label22" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel12" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Subject" Width="60px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="姓">
                                        </x:Label>
                                        <x:Label ID="Label9" Width="80px" CssStyle="text-align:left" runat="server" CssClass="marginr" ShowLabel="false" Text="名：">
                                        </x:Label>
                                        <x:TextBox ID="UserInfoName" ShowLabel="true" Label="姓名" Required="true" Width="200px" MaxLength="20" MaxLengthMessage="最多输入20个字符" CssClass="marginr" runat="server" TabIndex="1">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label1" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel5" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label3" Width="60px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="课">
                                        </x:Label>
                                        <x:Label ID="Label4" Width="80px" CssStyle="text-align:left" runat="server" CssClass="marginr" ShowLabel="false" Text="程名称：">
                                        </x:Label>
                                        <x:TextBox ID="ClassName" MaxLength="100" MaxLengthMessage="最多输入100个字符" ShowLabel="true" Label="课程名称" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="2">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label14" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel17" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="ImpactFactor" Width="60px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="教">
                                        </x:Label>
                                        <x:Label ID="Label13" Width="80px" CssStyle="text-align:left" runat="server" CssClass="marginr" ShowLabel="false" Text="学时间：">
                                        </x:Label>
                                        <x:DatePicker runat="server" EmptyText="请选择日期" Width="195px" EnableEdit="false" CssClass="marginr" Label ="教学时间" Required="true" TabIndex="3"
                                            ID="DatePickerTeachingTime">
                                        </x:DatePicker>
                                    </Items>
                                </x:Panel>
       
                                <x:Label ID="Label7" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel7" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>

                                        <x:Label ID="Label10" Width="60px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="教">
                                        </x:Label>
                                        <x:Label ID="Label8" Width="80px" CssStyle="text-align:left" runat="server" CssClass="marginr" ShowLabel="false" Text="学对象学历：">
                                        </x:Label>
                                        <x:DropDownList Label="教学对象" AutoPostBack="false" Required="true" EnableSimulateTree="true" TabIndex="4"
                                            ShowRedStar="true" runat="server" ID="DropDownListTeachingDegree" Width="195px">
                                          
                                        </x:DropDownList>

                                    </Items>
                                </x:Panel>
                                
                                <x:Label ID="Label2" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel4" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="PublicJournalName" Width="60px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="专">
                                        </x:Label>
                                        <x:Label ID="Label5" Width="80px" runat="server" CssStyle="text-align:left" CssClass="marginr" ShowLabel="false" Text="业：">
                                        </x:Label>
                                        <x:TextBox ID="Specialty" MaxLength="20" MaxLengthMessage="最多输入20个字符" ShowLabel="true" Label="专业" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="5">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label11" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel14" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="StartPageNum" Width="60px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="班">
                                        </x:Label>
                                        <x:Label ID="Label15" Width="80px" CssStyle="text-align:left" runat="server" CssClass="marginr" ShowLabel="false" Text="号：">
                                        </x:Label>
                                        <x:TextBox ID="Grade" ShowLabel="true" MaxLength="10" MaxLengthMessage="最多输入10个字符" Label="班号" Width="200px" Required="true" CssClass="marginr" runat="server" TabIndex="6">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label6" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel27" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label32" Width="60px"  CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="保">
                                        </x:Label>
                                        <x:Label ID="Label16" Width="80px" CssStyle="text-align:left" runat="server" CssClass="marginr" ShowLabel="false" Text="密级别：">
                                        </x:Label>
                                        <x:DropDownList Label="保密等级" AutoPostBack="false" Required="true" EnableSimulateTree="true" TabIndex="7"
                                            ShowRedStar="true" runat="server" ID="DropDownListSecrecyLevel" Width="195px">
                                            <x:ListItem Text="公开" Value="公开" />
                                        </x:DropDownList>
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
                                <x:Button ID="Save" Text="保存" runat="server" Icon="Add" Size="Medium" ConfirmText="确定保存？" OnClick ="Save_Click"  ConfirmTarget="Top" ValidateForms="Panel2" Type="Submit">
                                </x:Button>
                                <x:Button ID="Reset" Text="重置" runat="server" Icon="Delete" Size="Medium" ConfirmText="确定重置？" OnClick ="Reset_Click"  ConfirmTarget="Top">
                                </x:Button>
                            </Items>
                        </x:Toolbar>
                    </Items>
                </x:Panel>
            </Items>
        </x:Panel>

    </form>
</body>
</html>
