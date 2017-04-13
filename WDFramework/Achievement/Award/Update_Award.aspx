<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Update_Award.aspx.cs" Inherits="WDFramework.Achievement.Award.Update_Award" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script type="text/javascript">
        function clearFile() {
            var obj = document.getElementById('fileupload');
            obj.outerHTML = obj.outerHTML;
        }
    </script>
    <title></title>
    <link href="../res/css/main.css" rel="stylesheet" type="text/css" />
</head>
<body oncontextmenu='return false'>
    <%--取消鼠标右键的点击--%> 
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <%--  <x:Panel ID="Panel1" runat="server" BodyPadding="5px" EnableBackgroundColor="true" 
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" AutoScroll="true" 
            ShowHeader="false"    >
        <Items>--%>

        <x:Panel ID="Panel2" runat="server" ShowBorder="false" EnableCollapse="true"
            Layout="HBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5" Height="410px"
            BoxConfigChildMargin="0 5 0 0" ShowHeader="false">
            <Items>

                <x:Panel ID="Panel3" Title="项目概要" BoxFlex="1" runat="server"
                    BodyPadding="5px" BoxMargin="5 0 0 40" ShowBorder="false" ShowHeader="false">
                    <Items>
                        <x:Panel ID="Panel4" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="AwardName" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="获奖名称：">
                                </x:Label>
                                <x:TextBox ID="tAwardName" MaxLength="100" MaxLengthMessage="最多可输入100个字符" ShowLabel="true" Label="获奖名称" Width="200px" CssClass="marginr" runat="server" TabIndex="1" OnTextChanged="tAwardName_TextChanged">
                                </x:TextBox>
                            </Items>
                        </x:Panel>
                     

                        <x:Label ID="Label3" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>

                        <x:Panel ID="Panel12" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="AwardwSpecies" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="获奖类别：">
                                </x:Label>
                                  <x:DropDownList ID="dAwardwSpecies" ShowLabel="true" EnableEdit="false" Label="获奖类别" Width="195px" AutoPostBack="true" runat="server" TabIndex="2">
                                </x:DropDownList>
                              <%--  <x:TextBox ID="tAwardwSpecies" MaxLength="20" MaxLengthMessage="最多可输入20个字符" ShowLabel="true" Label="获奖类别" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="2">
                                </x:TextBox>--%>
                            </Items>
                        </x:Panel>
                        <x:Label ID="Label2" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>

                        <x:Panel ID="Panel5" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="Grade" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="获奖等级：">
                                </x:Label>
                                 <x:DropDownList ID="dGrade" ShowLabel="true" EnableEdit="false" Label="获奖等级" Width="195px" AutoPostBack="true" runat="server" TabIndex="3">
                                </x:DropDownList>
                              <%--  <x:TextBox ID="tGrade" MaxLength="5" ShowLabel="true" MaxLengthMessage="最多可输入5个字符" Label="获奖等级" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="3">
                                </x:TextBox>--%>

                            </Items>
                        </x:Panel>
                        <x:Label ID="Label1" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>
                        <x:Panel ID="Panel7" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="ApplicationTime" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="获奖时间：">
                                </x:Label>
                                <x:DatePicker ID="dAwardTime" runat="server" ShowLabel="true" EnableEdit="false" Label="获奖时间" TabIndex="4" Width="195px">
                                </x:DatePicker>
                            </Items>
                        </x:Panel>
                        <x:Label ID="Label4" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>
                        <x:Panel ID="Panel21" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="GivenAgency" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="授予机构：">
                                </x:Label>
                                <%--<x:TextArea runat="server" ID="TextArea1" Width="150px" Height="60px" AutoGrowHeight="true" AutoGrowHeightMin="100" AutoGrowHeightMax="200">
                                        </x:TextArea>--%>
                                <x:TextBox ID="tGivenAgency" MaxLength="20" MaxLengthMessage="最多可输入20个字符" ShowLabel="true" Label="授予机构" Width="200px" CssClass="marginr" runat="server" TabIndex="5">
                                </x:TextBox>
                            </Items>
                        </x:Panel>
                        <x:Label ID="Label7" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>
                        <x:Panel ID="Panel8" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="SecrecyLevel" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="保密级别：">
                                </x:Label>
                                <x:DropDownList ID="dSecrecyLevel" ShowLabel="true" EnableEdit="false" Label="保密级别" Width="195px" AutoPostBack="true" runat="server" TabIndex="6">
                                </x:DropDownList>
                                <%--<x:TextBox ID="SecrecyLevel1" ShowLabel="false" Required="true" Width="150px" CssClass="marginr" runat="server" TabIndex="13">
                                         </x:TextBox>--%>
                            </Items>
                        </x:Panel>
                        <x:Label ID="Label13" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>
                        <x:Panel ID="Panel13" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="Unit" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="单位1：">
                                </x:Label>
                                <x:TextBox ID="Unit1" MaxLength="40" MaxLengthMessage="最多可输入40个字符" ShowLabel="true" Label="单位1" Width="200px" CssClass="marginr" runat="server" TabIndex="7">
                                </x:TextBox>
                            </Items>
                        </x:Panel>
                            <x:Label ID="Label15" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>
                        <x:Panel ID="Panel9" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="Label11" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="单位2：">
                                </x:Label>
                                <x:TextBox ID="Unit2" MaxLength="40" MaxLengthMessage="最多可输入40个字符" ShowLabel="true" Label="单位2" Width="200px" CssClass="marginr" runat="server" TabIndex="8">
                                </x:TextBox>
                            </Items>
                        </x:Panel>
                        <x:Label ID="Label5" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>
                        <x:Panel ID="Panel10" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="Label14" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="单位3：">
                                </x:Label>
                                <x:TextBox ID="Unit3" MaxLength="40" MaxLengthMessage="最多可输入40个字符" ShowLabel="true" Label="单位3" Width="200px" CssClass="marginr" runat="server" TabIndex="9">
                                </x:TextBox>
                            </Items>
                        </x:Panel>
                          <x:Label ID="Label18" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>
                          <x:Panel ID="Panel15" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="Label17" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="单位4：">
                                </x:Label>
                                <x:TextBox ID="Unit4" MaxLength="40" MaxLengthMessage="最多可输入40个字符" ShowLabel="true" Label="单位4" Width="200px" CssClass="marginr" runat="server" TabIndex="10">
                                </x:TextBox>
                            </Items>
                        </x:Panel>
                    </Items>
                </x:Panel>
                <x:Panel ID="Panel6" Title="项目概要" BoxFlex="1" runat="server"
                    BodyPadding="5px" BoxMargin="5 0 0 40" ShowBorder="false" ShowHeader="false">

                    <Items>

                    
                      
                      
                        <x:Panel ID="Panel16" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="Label19" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="单位5：">
                                </x:Label>
                                <x:TextBox ID="Unit5" MaxLength="40" MaxLengthMessage="最多可输入40个字符" ShowLabel="true" Label="单位5" Width="200px" CssClass="marginr" runat="server" TabIndex="11">
                                </x:TextBox>
                            </Items>
                        </x:Panel>
                        <x:Label ID="Label22" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>
                        <x:Panel ID="Panel134" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="Label24" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="获奖类型：">
                                </x:Label>
                                  <x:DropDownList ID="dAwardForm" ShowLabel="true" EnableEdit="false" Label="获奖类型" Width="195px" AutoPostBack="true" runat="server" TabIndex="12">
                                </x:DropDownList>
                              <%--  <x:TextBox ID="tGrade" MaxLength="5" ShowLabel="true" MaxLengthMessage="最多可输入5个字符" Label="获奖等级" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="3">
                                </x:TextBox>--%>
                            </Items>
                        </x:Panel>
                          <x:Label ID="Label23" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>
                        <x:Panel ID="Panel154" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="Label25" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="获奖证书号：">
                                </x:Label>
                                <x:TextBox ID="AwardNum" MaxLength="50" MaxLengthMessage="最多可输入50个字符" ShowLabel="true" Label="获奖证书号" Width="200px" CssClass="marginr" runat="server" TabIndex="13">
                                </x:TextBox>
                            </Items>
                        </x:Panel>
                          <x:Label ID="Label26" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>
                        <x:Panel ID="Panel11" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="Label27" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="第一获奖人：">
                                </x:Label>
                                <x:TextBox ID="FirstAward" MaxLength="10" MaxLengthMessage="最多可输入10个字符" ShowLabel="true" Label="第一获奖人" Width="200px" CssClass="marginr" runat="server" TabIndex="14">
                                </x:TextBox>
                            </Items>
                        </x:Panel>
                           <x:Label ID="Label28" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>
                        
                        <x:Panel ID="Panel213" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="Label29" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="分类：">
                                </x:Label>
                                <x:DropDownList ID="DropDownList_Sort" ShowLabel="true" Label="分类：" EnableEdit="false" Width="195px" AutoPostBack="true" runat="server" TabIndex="19">
                                    <%--<x:ListItem Text="成果获奖" Value="成果获奖" Selected="true" />--%>
                                    <x:ListItem Text="科研获奖" Value="科研获奖" Selected="true" />
                                    <x:ListItem Text="教学获奖" Value="教学获奖" />
                                </x:DropDownList>
                            </Items>
                        </x:Panel>
                         <x:Label ID="Label16" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>
                        <x:Panel ID="Panelk" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="Label21" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="成果名称：">
                                </x:Label>
                                <x:TextArea runat="server" MaxLength="500" EmptyText="两个或两个以上请用逗号隔开" MaxLengthMessage="最多可输入500个字符"  ID="tachievement" Label="成果名称" CssStyle ="overflow-y:scroll" AutoPostBack="true" ShowLabel="true" Width="195px" Height="40px" AutoGrowHeight="false"  TabIndex="12" OnTextChanged="tachievement_TextChanged">
                                </x:TextArea>
                            </Items>
                        </x:Panel>
                        <x:Label ID="Label8" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>
                       <%-- <x:Panel ID="Panel" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="Label9" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="全部获奖人：">
                                </x:Label>
                                <x:TextArea runat="server" MaxLength="250" EmptyText="两个或两个以上请用逗号隔开" MaxLengthMessage="最多可输入250个字符" ID="AwardPeople" Label="全部获奖人" CssStyle ="overflow-y:scroll" ShowLabel="true" Width="195px" Height="40px" AutoGrowHeight="false"   TabIndex="13">
                                </x:TextArea>
                            </Items>
                        </x:Panel>

                        <x:Label ID="Label6" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>--%>
                        <%--这是空行--%>
                        <x:Panel ID="Panel_123" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                             <Items>
                                <x:Label ID="Member" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="成员及排序：">
                                 </x:Label>
                                <x:TextArea runat="server" MaxLength="200"  MaxLengthMessage="最多可输入200个字符" ID="Members" Label="成员及排序" ShowLabel="true" Width="195px" Height="40px" CssStyle ="overflow-y:scroll" AutoGrowHeight="false"   TabIndex="17">
                                </x:TextArea>
                            </Items>
                        </x:Panel>
                        <%--这是空行--%>
                        <x:Panel ID="Panel19" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="Remark" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="备注：">
                                </x:Label>
                                <x:TextArea runat="server" MaxLength="200"  MaxLengthMessage="最多可输入200个字符" ID="tRemark" Label="备注" ShowLabel="true" Width="195px" Height="40px" CssStyle ="overflow-y:scroll" AutoGrowHeight="false"   TabIndex="14">
                                </x:TextArea>
                            </Items>
                        </x:Panel>

                    </Items>

                </x:Panel>

            </Items>
        </x:Panel>
        <asp:Panel ID="Panelasp" ShowHeader="false" ShowBorder="false"
            Layout="Column" runat="server" Height="35px" BackColor="White">
            <asp:Label ID="Label10" runat="server" Label="Label" CssClass="marginr" Text=" " Width="430px">
            </asp:Label>
            <asp:Label ID="Label20" runat="server" Label="Label" CssClass="marginr"  Text="相关文档： " Width="95px">
            </asp:Label>
            <input type="file" id="fileupload" style="width: 200px" runat="server" />
        </asp:Panel>
        <x:Panel ID="Panel87" ShowHeader="false" CssClass="formitem" ShowBorder="false" Layout="Column" runat="server">
            <Items>
                <x:Toolbar ID="Toolbar1" runat="server" ColumnWidth="100%">
                    <Items>
                        <x:Label ID="Label12" runat="server" Label="Label" Text=" " Width="310"></x:Label>
                        <x:Button ID="Save" Text="保存" runat="server" Type="Submit" ValidateForms="Panel2" Size="Medium" Icon="Add" ConfirmText="确定保存吗？" OnClick="Save_Click">
                        </x:Button>
                        <x:Button ID="DeleteAll" Text="重置" runat="server" ConfirmText="确定重置吗？" Size="Medium" Icon="Delete" OnClick="DeleteAll_Click">
                        </x:Button>
                    </Items>
                </x:Toolbar>
            </Items>
        </x:Panel>
        <%-- </Items>
            </x:Panel>--%>
    </form>
</body>
</html>
