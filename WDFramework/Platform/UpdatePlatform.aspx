<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdatePlatform.aspx.cs" Inherits="WDFramework.Platform.UpdatePlatform" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
     <%--取消鼠标右键的点击--%>
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager2" runat="server" />

        <x:Panel ID="Panel2" runat="server" ShowBorder="false" EnableCollapse="true" Height="560" AutoScroll="false"
            Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" EnableBackgroundColor="true" BoxConfigChildMargin="0"
            ShowHeader="false">
            <Items>
                <x:Panel ID="Panel10" runat="server" ShowBorder="false" EnableCollapse="true" AutoScroll="false"
                    Layout="Column" BoxConfigAlign="Stretch" BoxConfigPosition="Start" Height="560px"
                    BoxConfigChildMargin="0" ShowHeader="false">
                    <Items>
                        <x:Panel ID="Panel3" Title="项目概要" runat="server" ColumnWidth="100%" ShowBorder="false" BodyPadding="30px" BoxConfigPadding="5" AutoScroll="false"
                            ShowHeader="false">
                            <Items>
                                <x:Panel ID="Panel12" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label5" Width="60px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="平台">
                                        </x:Label>
                                        <x:Label ID="labPlatformName" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="名称：">
                                        </x:Label>
                                        <x:TextBox ID="txtPlatformName" Label="平台名称" MaxLength="100"  MaxLengthMessage="最多输入100个字符" ShowLabel="true"
                                            Required="true"  Width="200px" CssClass="marginr" runat="server" TabIndex ="1">
                                        </x:TextBox>  
                                    </Items>
                                </x:Panel>
                                  <x:Label ID="Label12" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel6" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label13" Width="60px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="平台">
                                        </x:Label>
                                        <x:Label ID="Label14" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="级别：">
                                        </x:Label>
                                        <x:DropDownList AutoPostBack="false" Label="平台级别" Required ="true" EnableSimulateTree="true" ShowLabel="true" Width="195px" CssClass="marginr" TabIndex="2"
                                            runat="server" ID="DropDownListPlatformRank">
                                        </x:DropDownList>
                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label2" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel1" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label6" Width="60px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="批复">
                                        </x:Label>
                                        <x:Label ID="labAgreeUnit" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="部门：">
                                        </x:Label>
                                        <x:DropDownList AutoPostBack="false" Label="批复部门" Required ="true" EnableSimulateTree="true" ShowLabel="true" Width="195px" CssClass="marginr" TabIndex="3"
                                            runat="server" ID="DropDownListAgreeUnit">
                                        </x:DropDownList>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label3" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel5" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label8" Width="60px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="批复">
                                        </x:Label>
                                        <x:Label ID="labAgreeTime" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="日期：">
                                        </x:Label>
                                        <x:DatePicker ID="DatePicker_AgreeTime" runat="server" Width="195px" ShowLabel="true" Label="批复日期" Required="true" EnableEdit="false" TabIndex="4" DateFormatString="yyyy-MM-dd">
                                        </x:DatePicker>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label1" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel8" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label19" Width="60px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="批复">
                                        </x:Label>
                                        <x:Label ID="labAgreeNumber" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="文号：">
                                        </x:Label>
                                        <x:TextBox ID="txtAgreeNumber" Label="批复文号" MaxLength="100"  MaxLengthMessage="最多输入20个字符" ShowLabel="true"
                                            Required="true"  Width="200px" CssClass="marginr" runat="server" TabIndex ="5">
                                        </x:TextBox>  
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label16" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel9" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label7" Width="60px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="批复">
                                        </x:Label>
                                        <x:Label ID="labAgreeExpenditure" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="经费：">
                                        </x:Label>
                                        <x:TextBox ID="txtAgreeExpenditure" Label="批复经费" MaxLength="100"  MaxLengthMessage="最多输入20位数字" ShowLabel="true"
                                            Required="true"  Width="200px" CssClass="marginr" runat="server" TabIndex ="6">
                                        </x:TextBox>  
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label17" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel11" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label9" Width="60px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="平台">
                                        </x:Label>
                                        <x:Label ID="labPlatformPrincipal" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="负责人：">
                                        </x:Label>
                                        <x:TextBox ID="txtPlatformPrincipal" Label="平台负责人" MaxLength="100"  MaxLengthMessage="最多输入20个字符" ShowLabel="true"
                                            Required="true"  Width="200px" CssClass="marginr" runat="server" TabIndex ="7">
                                        </x:TextBox>  
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label20" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel13" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label18" Width="60px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="平台">
                                        </x:Label>
                                        <x:Label ID="labPlatformMember" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="成员：">
                                        </x:Label>
                                        <x:TextArea ID="txtPlatformMember" Label="平台成员" MaxLength="100"  MaxLengthMessage="最多输入200个字符" ShowLabel="true"
                                            Required="true"  Width="200px" CssClass="marginr" runat="server" TabIndex ="8">
                                        </x:TextArea> 
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label22" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel14" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label21" Width="60px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="平台">
                                        </x:Label>
                                        <x:Label ID="labPlatformManagement" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="管理：">
                                        </x:Label>
                                        <x:TextArea ID="txtPlatformManagement" Label="平台管理" MaxLength="100"  MaxLengthMessage="最多输入200个字符" ShowLabel="true"
                                            Required="true"  Width="200px" CssClass="marginr" runat="server" TabIndex ="9">
                                        </x:TextArea>  
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label24" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel22" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label10" Width="60px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="平台">
                                        </x:Label>
                                        <x:Label ID="labPlatformType" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="类别:">
                                        </x:Label>
                                       <x:DropDownList AutoPostBack="false" Label="平台类别" Required ="true" EnableSimulateTree="true" ShowLabel="true" Width="195px" CssClass="marginr" TabIndex="10"
                                            runat="server" ID="DropDownListPlatformType">
                                        </x:DropDownList>
                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label4" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel7" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label11" Width="60px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="保密">
                                        </x:Label>
                                        <x:Label ID="SecrecyLevel" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="级别：">
                                        </x:Label>
                                        <x:DropDownList ID="DropDownList_SecrecyLevel" ShowLabel="true" Label="保密级别" Required="true" Width="195px" AutoPostBack="true" runat="server" TabIndex="11">
                                        </x:DropDownList>
                                    </Items>
                                </x:Panel>
                            </Items>
                        </x:Panel>
                    </Items>
                </x:Panel>
            </Items>
        </x:Panel>
        <div runat ="server" id = "div_fileUpload" style="background-color:white; padding-left:66px; height:40px; width:351px;">
            <asp:Label ID="Label25" runat="server" Label="Label" CssClass="marginr"  Text="相关文档： " Width="95px">
            </asp:Label>
            <input type="file" id="fileupload" style="width: 200px" runat="server" />
        </div>
        <x:Panel ID="Panel4" ShowHeader="false" CssClass="formitem" ShowBorder="false" Layout="Column" runat="server" Height="30">
            <Items>
                <x:Toolbar ID="Toolbar1" runat="server" ColumnWidth="100%" Height="50">
                    <Items>
                        <x:Label ID="Label15" Width="130px" runat="server" ShowLabel="true" Text=" ">
                        </x:Label>
                        <x:Button ID="btnSave" runat="server" CssClass="marginr" Type="Submit" Text="保存" Icon="Add" Size="Medium" ValidateForms="Panel2" ConfirmText="确定保存？" OnClick="btnSave_Click">
                        </x:Button>
                        <x:Button ID="btnReset" runat="server" CssClass="marginr" Icon="Delete" Text="重置" Size="Medium" ConfirmText="确定重置？" OnClick="btnReset_Click">
                        </x:Button>
                    </Items>
                </x:Toolbar>
            </Items>
        </x:Panel>
    </form>
</body>
</html>
