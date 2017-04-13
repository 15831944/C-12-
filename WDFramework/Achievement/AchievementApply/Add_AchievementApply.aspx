<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add_AchievementApply.aspx.cs" Inherits="WDFramework.Acheievement.AchievementApply.Add_AchievementApply" %>



<%@ Register Assembly="FineUI" Namespace="FineUI" TagPrefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">
        function clearFile() {
            var obj = document.getElementById('fileupload');
            obj.outerHTML = obj.outerHTML;
        }
    </script>
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


        <%--Height="350px" Width="850px"  --%>
        <x:Panel ID="Panel2" runat="server" ShowBorder="false" EnableCollapse="true" AutoScroll="true"
            Layout="HBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5" Height="620px"
            BoxConfigChildMargin="0 5 0 0" ShowHeader="false">
            <Items>


                <x:Panel ID="Panel3" Title="项目概要" BoxFlex="1" runat="server"
                    BodyPadding="5px" BoxMargin="0 0 0 35" ShowBorder="false" ShowHeader="false" ColumnWidth="100%">
                    <Items>

                        <x:Panel ID="Panel14" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="Achievement" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="成果名称：">
                                </x:Label>
                                <x:TextBox ID="tAchievement" MaxLength="100" MaxLengthMessage="最多可输入100个字符" ShowLabel="true" Label="成果名称" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="1" AutoPostBack="true" OnTextChanged="tAchievement_TextChanged1">
                                </x:TextBox>
                            </Items>
                        </x:Panel>

                        <x:Label ID="Label16" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>

                        <x:Panel ID="Panel4" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="ApplyUnit" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="应用单位：">
                                </x:Label>
                                <x:TextBox ID="tApplyUnit" MaxLength="20" MaxLengthMessage="最多可输入20个字符" ShowLabel="true" Label="应用单位" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="2">
                                </x:TextBox>
                            </Items>
                        </x:Panel>
                        <x:Label ID="Label3" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>

                        <x:Panel ID="Panel5" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="StartTime" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="开始时间：">
                                </x:Label>
                                <x:DatePicker ID="dStartTime" runat="server" EnableEdit="false" Required="true" Width="200px" ShowLabel="true" Label="开始时间：" TabIndex="3">
                                </x:DatePicker>
                            </Items>
                        </x:Panel>
                        <x:Label ID="Label1" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>

                        <x:Panel ID="Panel8" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="EndTime" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="结束时间：">
                                </x:Label>
                                <x:DatePicker ID="dEndTime" runat="server" EnableEdit="false" Width="200px" ShowLabel="true" Label="结束时间（大于开始时间）" TabIndex="4">
                                </x:DatePicker>
                            </Items>
                        </x:Panel>

                        <%-- </Items>               
                 </x:Panel>           
                 <x:Panel ID="Panel6" Title="项目概要" BoxFlex="1" runat="server"
                 BodyPadding="5px" ShowBorder="false" ShowHeader="false"  ColumnWidth="50%" >     

                        <Items>--%>
                        <x:Label ID="Label2" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>
                        <x:Panel ID="Panel13" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="Use" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="用途：">
                                </x:Label>
                                <x:TextBox ID="tUse" MaxLength="50" MaxLengthMessage="最多可输入50个字符" ShowLabel="true" Label="用途" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="5">
                                </x:TextBox>
                            </Items>
                        </x:Panel>

                        <x:Label ID="Label18" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>
                        <x:Panel ID="Panel10" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="EconomicBenefit" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="经济效益：">
                                </x:Label>
                                <x:TextBox ID="tEconomicBenefit" MaxLength="10" MaxLengthMessage="最多可输入10个字符" ShowLabel="true" Label="经济效益" Width="200px" CssClass="marginr" runat="server" TabIndex="6">
                                </x:TextBox>
                            </Items>
                        </x:Panel>

                        <x:Label ID="Label11" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>

                        <x:Panel ID="Panel7" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="SecrecyLevel" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="保密级别：">
                                </x:Label>
                                <x:DropDownList ID="dSecrecyLevel" ShowLabel="true" Label="保密级别" Required="true" Width="195px" AutoPostBack="true" runat="server" TabIndex="7">
                                </x:DropDownList>

                            </Items>
                        </x:Panel>

                        <x:Label ID="Label15" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>


                       <%-- <x:Panel ID="Panel_12" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="Member" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="成员：">
                                </x:Label>
                                <x:TextArea runat="server" MaxLength="2000" MaxLengthMessage="最多可输入2000个字符" ShowLabel="true" Label="成员" CssClass="overflow-y:scroll" ID="tMember" Width="195px" Height="300px" TabIndex="8">
                                </x:TextArea>
                            </Items>
                        </x:Panel>--%> 

                        <x:Panel ID="Panel44" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="Label67" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="成员：">
                                </x:Label>
                                <x:TextArea runat="server" MaxLength="2000" MaxLengthMessage="最多输入2000个字符" ShowLabel="true" Label="成员" CssStyle="overflow-y:scroll" ID="TextAreaMember" Width="195px" Height="300px" TabIndex="42">
                                </x:TextArea>
                            </Items>
                        </x:Panel>

                    </Items>
                </x:Panel>
            </Items>
        </x:Panel>
        <asp:Panel ID="Panelasp" ShowHeader="false" ShowBorder="false"
            Layout="Column" runat="server" Height="30px" BackColor="White">
            <asp:Label ID="Label10" runat="server" Label="Label" CssClass="marginr" Text=" " Width="40px">
            </asp:Label>
            <asp:Label ID="Label20" runat="server" Label="Label" CssClass="marginr" Text="相关文档： " Width="95px">
            </asp:Label>
            <input type="file" id="fileupload" style="width: 200px" runat="server" />

        </asp:Panel>
        <x:Panel ID="Panel87" ShowHeader="false" CssClass="formitem" ShowBorder="false" Layout="Column" runat="server">
            <Items>
                <x:Toolbar ID="Toolbar1" runat="server" ColumnWidth="100%">
                    <Items>
                        <x:Label ID="Label12" runat="server" Label="Label" Text=" " Width="150"></x:Label>
                        <x:Button ID="Save" Text="保存" runat="server" Type="Submit" ConfirmText="确定保存？" Icon="Add" Size="Medium" ValidateForms="Panel2" OnClick="Save_Click">
                        </x:Button>
                        <x:Button ID="DeleteAll" Text="重置" runat="server" ConfirmText="确定重置？" Icon="Delete" Size="Medium" OnClick="DeleteAll_Click">
                        </x:Button>
                    </Items>
                </x:Toolbar>
            </Items>
        </x:Panel>
        <%--     </Items>
            </x:Panel>--%>
    </form>
</body>
</html>
