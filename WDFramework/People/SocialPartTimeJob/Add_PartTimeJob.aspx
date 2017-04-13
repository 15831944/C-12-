<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add_PartTimeJob.aspx.cs" Inherits="WDFramework.People.Add_PartTimeJob" %>


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
                    Layout="Column" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5" Height="325px"
                    BoxConfigChildMargin="0 5 0 0" ShowHeader="false">
                    <Items>
                        <x:Panel ID="Panel3" Title="社会兼职信息" BoxFlex="1" runat="server" ColumnWidth="100%"
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
                                        <x:TextBox ID="UserInfoName" MaxLength="20" MaxLengthMessage="最多输入20个字符" ShowLabel="true" Label="姓名" AutoPostBack="true" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="1">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>
                                  <x:Label ID="Label7" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel7" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label8" Width="60px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="兼">
                                        </x:Label>
                                        <x:Label ID="Label15" Width="80px" CssStyle="text-align:left" runat="server" CssClass="marginr" ShowLabel="false" Text="职单位名称：">
                                        </x:Label>
                                        <x:TextBox ID="PartTimeUnit" MaxLength="50" MaxLengthMessage="最多输入50个字符" ShowLabel="true" Label="兼职单位名称" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="2">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>
                               
                                 <x:Label ID="Label2" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel8" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label23" Width="60px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="兼">
                                        </x:Label>
                                        <x:Label ID="Label24" Width="80px" CssStyle="text-align:left" runat="server" CssClass="marginr" ShowLabel="false" Text="职人员原单位：">
                                        </x:Label>
                                        <x:TextBox ID="tprimaryUnit" MaxLength="50" MaxLengthMessage="最多输入50个字符" ShowLabel="true" Label="兼职人员原单位" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="3">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>
                               
                                 <x:Label ID="Label25" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel4" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="PublicJournalName" Width="60px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="兼">
                                        </x:Label>
                                        <x:Label ID="Label13" Width="80px" runat="server" CssStyle="text-align:left" CssClass="marginr" ShowLabel="false" Text="职职位名称：">
                                        </x:Label>
                                        <x:TextBox ID="PartTimeName" MaxLength="20" MaxLengthMessage="最多输入20个字符" ShowLabel="true" Label="兼职职位名称" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="4">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>
                              
                                
                                <x:Label ID="Label14" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel17" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="ImpactFactor" Width="60px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="授">
                                        </x:Label>
                                        <x:Label ID="Label16" Width="80px" CssStyle="text-align:left" runat="server" CssClass="marginr" ShowLabel="false" Text="予部门：">
                                        </x:Label>
                                        <x:TextBox ID="AwardDepartments" MaxLength="40" MaxLengthMessage="最多输入40个字符" ShowLabel="true" Label="授予部门" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="5">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>

                                 <x:Label ID="Label17" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel21" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="EndPageNum" Width="60px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="批">
                                        </x:Label>
                                        <x:Label ID="Label19" Width="80px" CssStyle="text-align:left" runat="server" CssClass="marginr" ShowLabel="false" Text="准时间：">
                                        </x:Label>
                                        <x:DatePicker runat="server" Label="批准时间" EmptyText="请选择日期" EnableEdit="false" Width="195px" CssClass="marginr" Required="true" TabIndex="6"
                                            ID="DatePickerApproveTime">
                                        </x:DatePicker>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label11" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel14" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="StartPageNum" Width="60px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="任">
                                        </x:Label>
                                        <x:Label ID="Label18" Width="80px" CssStyle="text-align:left" runat="server" CssClass="marginr" ShowLabel="false" Text="期：">
                                        </x:Label>
                                        <x:TextBox ID="Terms" ShowLabel="true" Label="任期" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="7">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>

                                 <x:Label ID="Label1" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel5" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label3" Width="60px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="级">
                                        </x:Label>
                                        <x:Label ID="Label10" Width="80px" CssStyle="text-align:left" runat="server" CssClass="marginr" ShowLabel="false" Text="别：">
                                        </x:Label>
                                        <x:DropDownList Label="级别" AutoPostBack="false" Required="true" EnableSimulateTree="true" TabIndex="8"
                                            ShowRedStar="true" runat="server" ID="DropDownListLevelName" Width="195px">                                           
                                        </x:DropDownList>
                                    </Items>
                                </x:Panel>

                                        <x:Label ID="Label26" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel9" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label27" Width="60px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="兼">
                                        </x:Label>
                                        <x:Label ID="Label28" Width="80px" CssStyle="text-align:left" runat="server" CssClass="marginr" ShowLabel="false" Text="职分类：">
                                        </x:Label>
                                        <x:DropDownList Label="兼职分类" AutoPostBack="false" Required="true" EnableSimulateTree="true" TabIndex="9"
                                            ShowRedStar="true" runat="server" ID="ddl_sort" Width="195px">
                                            <x:ListItem Text="社会兼职" Value="社会兼职" />
                                            <x:ListItem Text="学术兼职" Value="学术兼职" />
                                        </x:DropDownList>
                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label4" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel27" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label32" Width="60px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="保">
                                        </x:Label>
                                        <x:Label ID="Label20" Width="80px" CssStyle="text-align:left" runat="server" CssClass="marginr" ShowLabel="false" Text="密级别：">
                                        </x:Label>
                                        <x:DropDownList Label="保密等级" AutoPostBack="false" Required="true" EnableSimulateTree="true" TabIndex="9"
                                            ShowRedStar="true" runat="server" ID="DropDownListSecrecyLevel" Width="195px">
                                            <x:ListItem Text="公开" Value="公开" />
                                        </x:DropDownList>
                                    </Items>
                                </x:Panel>
                                
                                <x:Label ID="Label6" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                         
                                <%--这是空行--%>

                                <x:Panel ID="Panel6" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                     Layout ="Column"  runat="server">
                                    <Items>
                                        <x:Label ID="Label5" Width="60px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="备">
                                        </x:Label>
                                        <x:Label ID="Label21" Width="80px" CssStyle="text-align:left" runat="server" CssClass="marginr" ShowLabel="false" Text="注：">
                                        </x:Label>
                                        <x:TextArea runat="server" MaxLength="200" MaxLengthMessage="最多输入200个字符" ShowLabel="true" Label ="备注"  ID="Remark" Width="195px" Height="80px" TabIndex="10" CssStyle ="overflow-y:scroll">
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
                                <x:Button ID="Save" Text="保存" runat="server" Size="Medium" Icon="Add" OnClick ="Save_Click"  ConfirmText="确定保存？" ConfirmTarget="Top" ValidateForms="Panel2" Type="Submit">
                                </x:Button>
                                <x:Button ID="Reset" Text="重置" runat="server" Size="Medium" Icon="Delete" OnClick ="Reset_Click"  ConfirmText="确定重置？" ConfirmTarget="Top">
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
