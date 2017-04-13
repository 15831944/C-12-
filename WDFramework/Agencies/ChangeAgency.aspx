<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangeAgency.aspx.cs" Inherits="WDFramework.Agency.ChangeAgency" %>

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
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" ShowHeader="false">
            <Items>

                <x:Panel ID="Panel2" runat="server" Height="375px" ShowBorder="True" EnableCollapse="true" AutoScroll="true"
                    Layout="Column" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5"
                    BoxConfigChildMargin="0 5 0 0" ShowHeader="false">

                    <Items>

                        <x:Panel ID="Panel3" BoxFlex="1" runat="server" ColumnWidth="50%"
                            BodyPadding="30px" ShowBorder="false" ShowHeader="false">
                            <Items>

                                <x:Panel ID="Panel13" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>

                                        <x:Label ID="AgencyName" Width="120px" runat="server" ShowLabel="false" Text="机构名称：">
                                        </x:Label>
                                        <x:TextBox ID="AgencyName2" ShowLabel="true" Label="机构名称" Required="true" Width="200px" runat="server" TabIndex="1" MaxLength="40" AutoPostBack="true" OnTextChanged="AgencyName2_TextChanged" Regex="^[\u4E00-\u9FA5A-Za-z]+$">
                                        </x:TextBox>

                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label1" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel12" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>

                                        <x:Label ID="ParentID" Width="120px" runat="server" ShowLabel="false" Text="上级机构：">
                                        </x:Label>
                                        <x:TextBox ID="ParentID2" ShowLabel="true" Label="上级机构" Required="true" Width="200px" runat="server" TabIndex="2" OnTextChanged="ParentID2_TextChanged" MaxLength="30" AutoPostBack="true"  Regex="^[\u4E00-\u9FA5A-Za-z]+$">
                                        </x:TextBox>

                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label4" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel14" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>

                                        <x:Label ID="SecrecyLevel" Width="120px" runat="server" ShowLabel="false" Text="保密级别：">
                                        </x:Label>

                                        <x:DropDownList ID="DroSecrecyLevel" ShowLabel="true" Label="保密级别" Required="true" Width="195px" runat="server" TabIndex="3">
                                        </x:DropDownList>

                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label15" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel7" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>

                                        <x:Label ID="AgencyHeads" Width="120px" runat="server" ShowLabel="false" Text="机构负责人：">
                                        </x:Label>
                                        <x:TextBox ID="AgencyHeads2" ShowLabel="true" Label="机构负责人" Required="true" Width="200px" runat="server" TabIndex="4" MaxLength="20" Regex="^[\u4E00-\u9FA5A-Za-z]+$">
                                        </x:TextBox>

                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label12" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel8" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>

                                        <x:Label ID="Research" Width="120px" runat="server" ShowLabel="false" Text="研究方向：">
                                        </x:Label>
                                        <x:TextBox ID="Research2" ShowLabel="true" Label="研究方向" Required="true" Width="200px" runat="server" TabIndex="5" MaxLength="30" Regex="^[\u4E00-\u9FA5A-Za-z]+$">
                                        </x:TextBox>

                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label5" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel6" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>

                                        <x:Label ID="Label3" Width="120px" runat="server" ShowLabel="false" Text="总体.内部机构：">
                                        </x:Label>
                                        <x:DropDownList ID="ddl_glo" ShowLabel="true" Label="总体.内部机构" Width="195px" runat="server" TabIndex="6">
                                            <x:ListItem Text="总体" Value="总体" Selected="true" />
                                             <x:ListItem Text="内部" Value="内部" />
                                        </x:DropDownList>

                                    </Items>
                                </x:Panel>

                            </Items>
                        </x:Panel>

                        <x:Panel ID="Panel4" BoxFlex="1" runat="server" ColumnWidth="50%"
                            BodyPadding="30px" ShowBorder="false" ShowHeader="false">

                            <Items>

                                <x:Panel ID="Panel22" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>

                                        <x:Label ID="AgencyNumber" Width="120px" runat="server" ShowLabel="false" Text="机构分类：">
                                        </x:Label>
                                        <x:DropDownList ID="DroAgencyNumber" ShowLabel="true" Label="机构分类" Required="true" Width="195px" runat="server" TabIndex="7">
                                        </x:DropDownList>

                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label8" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel23" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>

                                        <x:Label ID="FullTimeNumbers" Width="120px" runat="server" ShowLabel="false" Text="专职人数：">
                                        </x:Label>
                                        <x:TextBox ID="FullTimeNumber2" ShowLabel="true" Label="专职人数" Required="true" Width="200px" runat="server" TabIndex="8" MaxLength="20" RegexPattern="NUMBER">
                                        </x:TextBox>

                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label10" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel24" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>

                                        <x:Label ID="PartTimeNumber" Width="120px" runat="server" ShowLabel="false" Text="兼职人数：">
                                        </x:Label>
                                        <x:TextBox ID="PartTimeNumber2" ShowLabel="true" Label="兼职人数" Required="true" Width="200px" runat="server" TabIndex="9" MaxLength="20" RegexPattern="NUMBER">
                                        </x:TextBox>

                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label9" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel17" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>

                                        <x:Label ID="Area" Width="120px" runat="server" ShowLabel="false" Text="面积：">
                                        </x:Label>
                                        <x:TextBox ID="Area2" ShowLabel="true" Label="面积" Required="true" Width="200px" runat="server" TabIndex="10" MaxLength="20" Regex="^\d{1,4}(\.\d{1,4})?$">
                                        </x:TextBox>
                                        <x:Label ID="area3" runat="server" Width="60" Text="平方米" ShowLabel="false"></x:Label>

                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label11" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel18" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>

                                        <x:Label ID="Location" Width="120px" runat="server" ShowLabel="false" Text="地点：">
                                        </x:Label>
                                        <x:TextBox ID="Location2" ShowLabel="true" Label="地点" Required="true" Width="200px" runat="server" TabIndex="11" MaxLength="50">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>
                            </Items>
                        </x:Panel>
                    </Items>
                </x:Panel>
                <x:Panel ID="Panel5" runat="server" ShowBorder="True" EnableCollapse="true"
                    Layout="Column" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5"
                    BoxConfigChildMargin="0 0 0 0" ShowHeader="false">
                    <Items>
                        <x:Toolbar ID="Toolbar1" runat="server" ColumnWidth="100%">
                            <Items>
                                <x:Label ID="Label2" Width="310px" runat="server" ShowLabel="true" Text=" ">
                                </x:Label>
                                <x:Button ID="Add" Text="保存" Type="Submit" Icon="Add" ConfirmText="确定保存？" Size="Medium" runat="server" OnClick="Save_Click" ValidateForms="Panel2" ValidateTarget="Top">
                                </x:Button>
                                <x:Button ID="Delete" Text="重置" ConfirmText="确定重置？" Icon="Delete" Size="Medium" runat="server" OnClick="Delete_Click">
                                </x:Button>
                            </Items>
                        </x:Toolbar>
                    </Items>
                </x:Panel>

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