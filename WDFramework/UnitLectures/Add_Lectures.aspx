<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add_Lectures.aspx.cs" Inherits="WebApplication1.新增讲学信息页面" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body oncontextmenu='return false'>
    <%--取消鼠标右键的点击--%>
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager1" runat="server" />

        <x:Panel ID="Panel16" runat="server" BodyPadding="5px" Height="330px"
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" ShowHeader="false">
            <Items>
                <x:Panel ID="Panel2" runat="server" Height="330px" ShowBorder="false" EnableCollapse="true"
                    Layout="Column" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5" BodyPadding="50px"
                    BoxConfigChildMargin="0 5 0 0" ShowHeader="false">

                    <Items>

                        <x:Panel ID="Panel3" BoxFlex="1" runat="server" ColumnWidth="47%"
                            ShowBorder="false" ShowHeader="false">
                            <Items>
                                <x:Panel ID="Panel12" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                         <x:Label ID="Label3" Width="50px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="姓名">
                                        </x:Label>
                                        <x:Label ID="labLecturesName" Width="60px" runat="server" CssClass="marginr" ShowLabel="false" Text="：">
                                        </x:Label>
                                        <x:TextBox ID="txtLecturesName" MaxLength="10" ShowLabel="true" Label="姓名" Required="true"
                                            Width="200px" CssClass="marginr" runat="server" TabIndex="1">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label1" runat="server" Label="Label" Text=" " Height="25px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel1" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label6" Width="50px" CssStyle="text-align:right" runat="server"
                                            CssClass="marginr" ShowLabel="false" Text="所属">
                                        </x:Label>
                                        <x:Label ID="labAgencyID" Width="60px" runat="server" CssClass="marginr" ShowLabel="false"
                                            Text="部门：">
                                        </x:Label>
                                        <x:DropDownList ID="DropDownList_Agency" ShowLabel="true" Label="所属部门" Required="true"
                                            AutoPostBack="false" runat="server" Width="195px" TabIndex="2">
                                             <x:ListItem Text="请选择" Value="0" />
                                        </x:DropDownList>
                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label2" runat="server" Label="Label" Text=" " Height="25px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel5" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label18" Width="50px" CssStyle="text-align:right" runat="server"
                                            CssClass="marginr" ShowLabel="false" Text="报告">
                                        </x:Label>
                                        <x:Label ID="labUReportName" Width="60px" runat="server" CssClass="marginr" ShowLabel="false"
                                            Text="名称：">
                                        </x:Label>
                                        <x:TextBox ID="txtUReportName" MaxLength="100" ShowLabel="true" Label="报告名称"
                                            Width="200px" CssClass="marginr" runat="server" TabIndex="3">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label8" runat="server" Label="Label" Text=" " Height="25px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel21" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label19" Width="50px" CssStyle="text-align:right" runat="server"
                                            CssClass="marginr" ShowLabel="false" Text="地点">
                                        </x:Label>
                                        <x:Label ID="labLecturesPlace" Width="60px" runat="server" CssClass="marginr"
                                            ShowLabel="false" Text="：">
                                        </x:Label>
                                        <x:TextBox ID="txtLecturesPlace" MaxLength="40" Label="地点" ShowLabel="true" 
                                            Width="200px" CssClass="marginr" runat="server" TabIndex="4">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label11" runat="server" Label="Label" Text=" " Height="25px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel22" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label20" Width="50px" CssStyle="text-align:right" runat="server"
                                            CssClass="marginr" ShowLabel="false" Text="听众">
                                        </x:Label>
                                        <x:Label ID="lablistenerNumber" Width="60px" runat="server" CssClass="marginr"
                                            ShowLabel="false" Text="人数：" >
                                        </x:Label>
                                        <%--非零的正整数 ^[1-9]\d*$--%>
                                        <x:TextBox ID="txtlistenerNumber" ShowLabel="true" Label="听众人数" MaxLength="5"
                                            Regex="^[1-9]\d*$" Width="200px" CssClass="marginr" runat="server" TabIndex="4">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label7" runat="server" Label="Label" Text=" " Height="25px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel9" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label22" Width="50px" CssStyle="text-align:right" runat="server"
                                            CssClass="marginr" ShowLabel="false" Text="时间">
                                        </x:Label>
                                        <x:Label ID="labLecturesTime" Width="60px" runat="server" CssClass="marginr"
                                            ShowLabel="false" Text="：">
                                        </x:Label>
                                        <x:DatePicker ID="DatePikerLecturesTime" runat="server" Required="true" Width="195px" Label="时间" EnableEdit="false" TabIndex="6">
                                        </x:DatePicker>
                                    </Items>
                                </x:Panel>

                            </Items>
                        </x:Panel>
                        <x:Panel ID="Panel17" Title="空Panel" runat="server"
                            BodyPadding="5px" ShowBorder="false" ShowHeader="false" ColumnWidth="6%">
                        </x:Panel>
                        <x:Panel ID="Panel6" BoxFlex="1" runat="server" ColumnWidth="47%"
                            BodyPadding="0px" ShowBorder="false" ShowHeader="false">
                            <Items>

                                <x:Panel ID="Panel8" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="labWorkPlace" Width="100px" runat="server" CssClass="marginr" ShowLabel="false"
                                            Text="工作单位：">
                                        </x:Label>
                                        <x:TextBox ID="txtWorkPlace" MaxLength="40" ShowLabel="true" Label="工作单位" Required="true"
                                            Width="200px" CssClass="marginr" runat="server" TabIndex="7">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label9" runat="server" Label="Label" Text=" " Height="25px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel10" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label10" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="职称/职务：">
                                        </x:Label>
                                        <x:TextBox ID="txtjobtitle" MaxLength="40" ShowLabel="true" Label="职称/职务" 
                                            Width="200px" CssClass="marginr" runat="server" TabIndex="8">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label4" runat="server" Label="Label" Text=" " Height="25px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel11" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label12" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="身份证号：">
                                        </x:Label>
                                        <x:TextBox ID="txtIDCard" MaxLength="20" ShowLabel="true" Label="身份证号"  Regex="^\d{15}|\d{18}$"
                                            Width="200px" CssClass="marginr" runat="server" TabIndex="9">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label13" runat="server" Label="Label" Text=" " Height="25px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel13" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label14" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="手机号：">
                                        </x:Label>
                                        <x:TextBox ID="txtTel" MaxLength="20" ShowLabel="true" Label="手机号" Regex="^(13[0-9]|14[5|7]|15[0|1|2|3|5|6|7|8|9]|18[0|1|2|3|5|6|7|8|9])\d{8}$"
                                            Width="200px" CssClass="marginr" runat="server" TabIndex="10">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label5" runat="server" Label="Label" Text=" " Height="25px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel14" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label17" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="备注：">
                                        </x:Label>
                                           <x:TextArea runat="server" MaxLength="200" MaxLengthMessage="最多输入200个字符" ShowLabel="true"  ID="Remark" Width="195px" Height="40px" TabIndex="11" CssStyle ="overflow-y:scroll">
                                        </x:TextArea>
                                        <%--<x:TextBox ID="txtRemark" MaxLength="40" ShowLabel="true" Label="备注" 
                                            Width="200px" CssClass="marginr" runat="server" TabIndex="11">
                                        </x:TextBox>--%>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label16" runat="server" Label="Label" Text=" " Height="25px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel7" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="labSecrecyLevel" Width="100px" runat="server" CssClass="marginr"
                                            ShowLabel="false" Text="保密级别：">
                                        </x:Label>
                                        <x:DropDownList ID="DropDownList_SecrecyLevel" Label="保密级别" Width="195px" ShowLabel="true"
                                            AutoPostBack="false" runat="server" Required="true" RequiredMessage="保密级别不能为空" TabIndex="12">
                                        </x:DropDownList>
                                    </Items>
                                </x:Panel>

                            </Items>
                        </x:Panel>
                    </Items>
                </x:Panel>
            </Items>
        </x:Panel>
        <asp:Panel ID="Panelasp" ShowHeader="false" CssClass="formitem" ShowBorder="false"
            Layout="Column" runat="server" BackColor="White" Height="45px">
            <asp:Label ID="Label" Width="75px" runat="server" CssClass="marginr" ShowLabel="false" Text="">
            </asp:Label>
            <asp:Label ID="Label21" Width="85px" runat="server" CssClass="marginr" ShowLabel="false" Text="相关文档：">
            </asp:Label>
            <input type="file" id="fileupload" style="width: 190px" runat="server" />
        </asp:Panel>
        <x:Panel ID="Panel4" ShowHeader="false" CssClass="formitem" ShowBorder="false" Layout="Column" runat="server" Height="40">
            <Items>
                <x:Toolbar ID="Toolbar1" runat="server" ColumnWidth="100%" Height="45">
                    <Items>
                        <x:Label ID="Label15" Width="302px" runat="server" ShowLabel="true" Text=" ">
                        </x:Label>
                        <x:Button ID="Save" runat="server" CssClass="marginr" Type="Submit" Text="保存" Icon="Add" ConfirmText="确定保存？" Size="Medium" ValidateForms="Panel2" OnClick="btnSave_Click">
                        </x:Button>
                        <x:Button ID="DeleteAll" runat="server" CssClass="marginr" Text="重置" Icon="Delete" ConfirmText="确定重置？"
                            Size="Medium" OnClick="btnReset_Click">
                        </x:Button>
                    </Items>
                </x:Toolbar>
            </Items>
        </x:Panel>
    </form>
</body>
</html>
