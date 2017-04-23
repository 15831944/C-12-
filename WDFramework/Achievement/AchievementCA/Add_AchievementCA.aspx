<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add_AchievementCA.aspx.cs" Inherits="WebApplication1.Achievement.AchievementCA.Add_AchievementCA" %>

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


        <%--Height="350px" Width="850px"  --%>
        <x:Panel ID="Panel2" runat="server" ShowBorder="false" EnableCollapse="true" 
            Layout="HBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5" Height="250px"
            BoxConfigChildMargin="0 5 0 0" ShowHeader="false">
            <Items>

                <x:Panel ID="Panel3" Title="项目概要" BoxFlex="1" runat="server"
                    BodyPadding="5px" ShowBorder="false" BoxMargin="35 0 0 40" ShowHeader="false" ColumnWidth="100%" AutoScroll="true">
                    <Items>

                        <x:Panel ID="Panel14" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="Achievement" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="成果名称：">
                                </x:Label>
                                <x:TextBox ID="tAchievement" MaxLength="100" MaxLengthMessage="最多可输入100个字符" ShowLabel="true" Label="项目名称" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="1" AutoPostBack="true" OnTextChanged="tAchievement_TextChanged">
                                </x:TextBox>
                            </Items>
                        </x:Panel>

                        <x:Label ID="Label3" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>

                        <x:Panel ID="Panel5" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="CAUnit" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="验收部门：">
                                </x:Label>
                                <x:TextBox ID="tCAUnit" MaxLength="40" MaxLengthMessage="最多可输入40个字符" ShowLabel="true" Label="验收部门" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="2">
                                </x:TextBox>
                            </Items>
                        </x:Panel>
                        <x:Label ID="Label16" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>
                        <x:Panel ID="Panel_2015" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="Member" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="成员：">
                                </x:Label>
                                <x:TextBox ID="tMember" MaxLength="40" MaxLengthMessage="最多可输入40个字符" ShowLabel="true" Label="成员" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="2">
                                </x:TextBox>
                                
                            </Items>
                        </x:Panel>
                        <x:Label ID="Label4" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>

                        <x:Panel ID="Panel4" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="CATime" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="验收时间：">
                                </x:Label>
                                <x:DatePicker ID="dCATime" runat="server" Width="195px" ShowLabel="true" EnableEdit="false" Label="验收时间" Required="true" TabIndex="3">
                                </x:DatePicker>
                            </Items>
                        </x:Panel>
                        <x:Label ID="Label1" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>

                        <%--      </Items>               
                 </x:Panel>           
                 <x:Panel ID="Panel6" Title="项目概要" BoxFlex="1" runat="server"
                 BodyPadding="5px" ShowBorder="false" ShowHeader="false"  ColumnWidth="50%" >     

                        <Items>--%>
                        <x:Panel ID="Panel8" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="CACommnetLevel" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="验收评语级别：">
                                </x:Label>
                                <x:TextBox ID="tCACommnetLevel" MaxLength="10" MaxLengthMessage="最多可输入10个字符" ShowLabel="true" Label="验收评语级别" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="4">
                                </x:TextBox>
                            </Items>
                        </x:Panel>

                        <x:Label ID="Label18" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>
                        <x:Panel ID="Panel10" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="SecrecyLevel" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="保密级别：">
                                </x:Label>
                                <x:DropDownList ID="dSecrecyLevel" ShowLabel="true" Label="保密级别" EnableEdit="false" Required="true" Width="195px" AutoPostBack="true" runat="server" TabIndex="5">
                                </x:DropDownList>
                                <%--<x:TextBox ID="SecrecyLevel1" ShowLabel="false" Required="true" Width="150px" CssClass="marginr" runat="server" TabIndex="13">
                                         </x:TextBox>--%>
                            </Items>
                        </x:Panel>
                    </Items>
                </x:Panel>

            </Items>
        </x:Panel>

        <asp:Panel ID="Panelasp" ShowHeader="false" ShowBorder="false"
            Layout="Column" runat="server" Height="80px" BackColor="White">
            <asp:Label ID="Label10" runat="server" Label="Label" CssClass="marginr" Text=" " Width="45px">
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
                        <x:Button ID="Save" Text="保存" runat="server" Type="Submit" ValidateForms="Panel2" OnClick="Save_Click" Size="Medium" Icon="Add" ConfirmText="确定保存？">
                        </x:Button>
                        <x:Button ID="DeleteAll" Text="重置" runat="server" Size="Medium" Icon="Delete" ConfirmText="确定重置？" OnClick="DeleteAll_Click">
                        </x:Button>
                        <%--  <x:Button ID="Cancel" Text="取消" runat="server"   >
                          </x:Button>--%>
                    </Items>
                </x:Toolbar>
            </Items>
        </x:Panel>
        <%--    </Items>
            </x:Panel>--%>
        <x:Window ID="WindowStaff" Popup="false" EnableIFrame="true" IFrameUrl="#" runat="server"
            EnableMaximize="true" EnableResize="true" Height="450px" Width="750px" Title="添加人员">
        </x:Window>
    </form>
</body>
</html>
