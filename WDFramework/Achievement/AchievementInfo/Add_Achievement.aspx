<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add_Achievement.aspx.cs" Inherits="WebApplication1.Achievement.Achievement.Add_Achievement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript">
        function clearFile() {
            var obj = document.getElementById('fileupload');
            obj.outerHTML = obj.outerHTML;
            //OpinionPage MemberPage SealPage          
            var obj = document.getElementById('MemberPage');
            obj.outerHTML = obj.outerHTML;
            var obj = document.getElementById('SealPage');
            obj.outerHTML = obj.outerHTML;

        }
    </script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <x:PageManager ID="PageManager1" AutoSizePanelID="Panel2" runat="server" />


            <x:Panel ID="Panel2" runat="server" Height="310" Width="734px" ShowBorder="false" EnableCollapse="true"
                Layout="HBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5"
                BoxConfigChildMargin="0 5 0 0" ShowHeader="false">

                <Items>


                    <x:Panel ID="Panel3" Title="项目概要" BoxFlex="1" runat="server"
                        BodyPadding="5px" BoxMargin="30 0 0 30" ShowBorder="false" ShowHeader="false">
                        <Items>
                            <x:Panel ID="Panel12" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="InspectName" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="成果名称：">
                                    </x:Label>
                                    <x:TextBox ID="tAchievementName" MaxLength="100" MaxLengthMessage="最多可输入100个字符" ShowLabel="true" Label="成果名称：" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="1" AutoPostBack="true" OnTextChanged="tAchievementName_TextChanged">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>
                            <x:Label ID="Label6" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>

                            <x:Panel ID="Panel15" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="AwardUnit" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="所属机构：">
                                    </x:Label>
                                    <x:DropDownList Label="所属机构" AutoPostBack="false" EnableEdit="false" Required="true" EnableSimulateTree="true" TabIndex ="2"
                                        runat="server" ID="DropDownListAgency" Width="195px">
                                    </x:DropDownList>
                                </Items>
                            </x:Panel>
                            <x:Label ID="Label7" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>

                            <x:Panel ID="Panel9" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label8" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="所属项目：">
                                    </x:Label>
                                    <x:TextBox ID="tProjectID" ShowLabel="true" Label="所属项目" MaxLength="60" MaxLengthMessage="最多可输入60个字符" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="3" AutoPostBack="true" OnTextChanged="tProjectID_TextChanged">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>
                            <x:Label ID="Label12" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>
                             <x:Panel ID="Panel1" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label1" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="项目内部编号：">
                                    </x:Label>
                                    <x:TextBox ID="ProjectInNum" ShowLabel="true" Label="项目内部编号" MaxLength="30" MaxLengthMessage="最多可输入30个字符" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="4" AutoPostBack="true">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>
                            <x:Panel ID="Panel5" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                               </x:Panel>
                            <x:Label ID="Label18" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>
                             <x:Panel ID="Panel18" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label14" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="成果第一完成人：">
                                    </x:Label>
                                    <x:TextBox ID="FirstFinishedPeople" ShowLabel="true" Label="成果第一完成人" MaxLength="155" MaxLengthMessage="最多可输入155个字符" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="13" AutoPostBack="true">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>
                            <x:Label ID="Label21" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>
                             <x:Panel ID="Panel17" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label16" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="成员：">
                                    </x:Label>
                                    <x:TextBox ID="ProjectPeople" ShowLabel="true" Label="成员" MaxLength="155" MaxLengthMessage="最多可输入155个字符" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="4" AutoPostBack="true">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>
                            <x:Label ID="Label2" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>
                            <%--<x:Panel ID="Panel56" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label14" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="鉴定级别：">
                                    </x:Label>
                                    <x:DropDownList Label="鉴定级别" AutoPostBack="false" EnableEdit="false" Required="true" EnableSimulateTree="true" TabIndex ="5"
                                        runat="server" ID="DropDownListProjectRank" Width="195px">
                                    </x:DropDownList>
                                </Items>
                            </x:Panel>
                            <x:Label ID="Label166" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>
                            <x:Panel ID="Panel13" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label17" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="鉴定形式：">
                                    </x:Label>
                                    <x:DropDownList Label="鉴定形式" AutoPostBack="false" EnableEdit="false" Required="true" EnableSimulateTree="true" TabIndex ="6"
                                        runat="server" ID="DropDownListProjectForm" Width="195px">
                                    </x:DropDownList>
                                </Items>
                            </x:Panel>
                           
                        </Items>
                    </x:Panel>
                    <x:Panel ID="Panel6" Title="项目概要" BoxFlex="1" runat="server"
                        BodyPadding="5px" BoxMargin="30 0 0 30" ShowBorder="false" ShowHeader="false">
                        <Items>
                             
                            <x:Panel ID="Panel16" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label19" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="鉴定水平：">
                                    </x:Label>
                                    <x:DropDownList Label="鉴定水平" AutoPostBack="false" EnableEdit="false" Required="true" EnableSimulateTree="true" TabIndex ="7"
                                        runat="server" ID="DropDownListProjectLevel" Width="195px">
                                    </x:DropDownList>
                                </Items>
                            </x:Panel>
                             <x:Label ID="Label186" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>
                             <x:Panel ID="Panel14" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label13" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="证书文号：">
                                    </x:Label>
                                    <x:TextBox ID="tApprovalNum" MaxLength="20" MaxLengthMessage="最多可输入20个字符" ShowLabel="true" Label="证书文号：" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="8">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>
                             <x:Label ID="Label3" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>
                            <x:Panel ID="Panel8" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="EntryPerson" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="鉴定组织部门：">
                                    </x:Label>
                                    <x:TextBox ID="tAppraisalUnit" MaxLength="40" MaxLengthMessage="最多可输入40个字符" ShowLabel="true" Label="鉴定组织部门：" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="9">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>

                            <x:Label ID="Label10" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>

                            <x:Panel ID="Panel11" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label11" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="成果登记号：">
                                    </x:Label>
                                    <x:TextBox ID="tApRemarkRank" MaxLength="10" MaxLengthMessage="最多可输入10个字符" ShowLabel="true" Label="成果登记号：" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="10">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>
                            <x:Label ID="Label5" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>
                            <x:Panel ID="Panel10" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label9" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="鉴定时间：">
                                    </x:Label>
                                    <x:DatePicker ID="dAppraisalTime" runat="server" EnableEdit="false" Width="195px" ShowLabel="true" Label="鉴定时间：" Required="true" TabIndex="11">
                                    </x:DatePicker>
                                </Items>
                            </x:Panel>

                            <x:Label ID="Label4" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>

                            <x:Panel ID="Panel7" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="SecrecyLevel" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="保密级别：">
                                    </x:Label>
                                    <x:DropDownList ID="dSecrecyLevel" ShowLabel="true" Label="保密级别" Required="true" Width="195px" AutoPostBack="true" runat="server" TabIndex="12">
                                    </x:DropDownList>
                                </Items>
                            </x:Panel>

                        </Items>
                    </x:Panel>

                </Items>
            </x:Panel>
             <asp:Panel ID="Panelasp" ShowHeader="false" ShowBorder="false"
                Layout="Column" runat="server" BackColor="White">
                <asp:Label ID="Labelp" runat="server" Label="Label" CssClass="marginr" Text=" " Width="35px">
                </asp:Label>
                <asp:Label ID="Label20" runat="server" Label="Label" CssClass="marginr" Text="相关文档： " Width="95px">
                </asp:Label>
                <input type="file" id="fileupload" style="width: 200px" runat="server" />
                 <asp:Label ID="Label22" runat="server" Label="Label" CssClass="marginr" Text=" " Width="35px">
                </asp:Label>
                <asp:Label ID="Label23" runat="server" Label="Label" CssClass="marginr" Text="鉴定意见页： " Width="95px">
                </asp:Label>
                <input type="file" id="OpinionPage" style="width: 200px" runat="server" />         
            </asp:Panel>
            <asp:Panel ID="Panel19" ShowHeader="false" ShowBorder="false"
                Layout="Column" runat="server" BackColor="White">
                <asp:Label ID="Label24" runat="server" Label="Label" CssClass="marginr" Text=" " Width="35px">
                </asp:Label>
                <asp:Label ID="Label25" runat="server" Label="Label" CssClass="marginr" Text="课题组成员页： " Width="95px">
                </asp:Label>
                <input type="file" id="MemberPage" style="width: 200px" runat="server" />
                 <asp:Label ID="Label26" runat="server" Label="Label" CssClass="marginr" Text=" " Width="35px">
                </asp:Label>
                <asp:Label ID="Label27" runat="server" Label="Label" CssClass="marginr" Text="组织单位盖章页： " Width="95px">
                </asp:Label>
                <input type="file" id="SealPage" style="width: 200px" runat="server" />          
            </asp:Panel>
            <x:Panel ID="Panel4" ShowHeader="false" CssClass="formitem" ShowBorder="false" Layout="Column" runat="server">
                <Items>
                    <x:Toolbar ID="Toolbar1" runat="server" ColumnWidth="100%">
                        <Items>
                            <x:Label ID="Label15" Width="310px" runat="server" ShowLabel="true" Text=" ">
                            </x:Label>
                            <x:Button ID="Save" runat="server" CssClass="marginr" Type="Submit" Text="保存" Icon="Add" ConfirmText="确定保存？" Size="Medium" ValidateForms="Panel2" OnClick="Save_Click">
                            </x:Button>
                            <x:Button ID="DeleteAll" runat="server" CssClass="marginr" Text="重置" Icon="Delete" ConfirmText="确定重置？" Size="Medium" OnClick="DeleteAll_Click">
                            </x:Button>
                        </Items>
                    </x:Toolbar>
                </Items>
            </x:Panel>


        </div>
    </form>
</body>
</html>
