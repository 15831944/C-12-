<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add_Pact.aspx.cs" Inherits="WebApplication1.ContractAndPact.Pact.Add_Pact" %>

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
</head>
<body oncontextmenu='return false'>
    <%--取消鼠标右键的点击--%>
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager1" runat="server" />

        <x:Panel ID="Panel2" runat="server" Height="335px" ShowBorder="false" EnableCollapse="true" AutoScroll="false"
            Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="0"
            BoxConfigChildMargin="0 0 0 0" ShowHeader="false">
            <Items>
                <x:Panel ID="Panel20" runat="server" Height="300px" ShowBorder="false" EnableCollapse="true" AutoScroll="false"
                    Layout="Column" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="0" BodyPadding="20px"
                    BoxConfigChildMargin="0 0 0 0" ShowHeader="false">

                    <Items>
                        <x:Panel ID="Panel3" BoxFlex="1" runat="server" ColumnWidth="46%" BodyPadding="0px" ShowBorder="false"
                            ShowHeader="false">
                            <Items>

                                <x:Label ID="Label10" runat="server" Label="Label" Text=" " Height="10px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel13" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label116" Width="80px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="合同">
                                        </x:Label>
                                        <x:Label ID="labPactNum" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="编号：">
                                        </x:Label>
                                        <x:TextBox ID="txtPactNum" MaxLength="20" ShowLabel="true" Label="合同编号" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="1">
                                            <%--RequiredMessage="合同编号不能为空"  CompareMessage="输入最大长度为20个字符"--%>
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label4" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel5" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label86" Width="80px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="合同">
                                        </x:Label>
                                        <x:Label ID="Label22" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="名称：">
                                        </x:Label>
                                        <x:TextBox ID="txtPactName" MaxLength="100" ShowLabel="true" Label="合同名称" Width="200px" CssClass="marginr" runat="server" TabIndex="2">
                                            <%--RequiredMessage="合同类别不能为空"  CompareMessage="输入最大长度为5个字符"--%>
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label2" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel14" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label13" Width="80px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="任务">
                                        </x:Label>
                                        <x:Label ID="labPactType" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="来源：">
                                        </x:Label>
                                        <x:TextBox ID="txtPactType" MaxLength="5" ShowLabel="true" Label="任务来源" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="3">
                                            <%--RequiredMessage="合同类别不能为空"  CompareMessage="输入最大长度为5个字符"--%>
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label5" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel15" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label126" Width="80px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="合同">
                                        </x:Label>
                                        <x:Label ID="labStartTime" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="开始时间：">
                                        </x:Label>
                                        <x:DatePicker ID="DatePicker_StartTime" runat="server" Width="195px" ShowLabel="true" Label="合同开始时间" Required="true" EnableEdit="false" TabIndex="4">
                                            <%--RequiredMessage="合同开始时间不能为空"--%>
                                        </x:DatePicker>
                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label6" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <%--   <x:Panel ID="Panel16" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label30" Width="80px" runat="server" CssClass="marginr" CssStyle="text-align:right"
                                            ShowLabel="false" Text="会议">
                                        </x:Label>
                                        <x:Label ID="Label31" Width="80px" runat="server" CssClass="marginr" CssStyle="text-align:left"
                                            ShowLabel="false" Text="地点：">
                                        </x:Label>
                                        <x:TextBox ID="txtMeetingPlace" Label="会议地点" Required="true" ShowLabel="true"
                                            Width="200px" CssClass="marginr" runat="server" MaxLength="50" CompareMessage="输入最大长度为50个字符" TabIndex="5">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label14" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>--%>
                                <%--这是空行--%>

                                <x:Panel ID="Panel8" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label26" Width="80px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="合同">
                                        </x:Label>
                                        <x:Label ID="Label23" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="结束时间：">
                                        </x:Label>
                                        <x:DatePicker ID="DatePicker_EndTime" runat="server" Width="195px" ShowLabel="true" Label="合同结束时间" EnableEdit="false" TabIndex="5">
                                            <%--RequiredMessage="合同开始时间不能为空"--%>
                                        </x:DatePicker>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Labe20" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel11" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label15" Width="80px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="所属">
                                        </x:Label>
                                        <x:Label ID="labProject" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="项目:">
                                        </x:Label>
                                        <%--  <x:TextBox ID="txtProject"  ShowLabel="true" Label="所属项目" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="2">
                                        </x:TextBox>--%>
                                        <x:DropDownList ID="DropDownList_Project" ShowLabel="true" Label="所属项目" Required="true" Width="195px" AutoPostBack="true" runat="server" TabIndex="6">
                                            <%--RequiredMessage="所属项目不能为空"--%>
                                            <x:ListItem Text="请选择" Value="0" />
                                        </x:DropDownList>
                                    </Items>
                                </x:Panel>
                                
                                <x:Label ID="Label7" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel25" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                         <x:Label ID="SecrecyLevel" Width="80px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="保密">
                                        </x:Label>
                                        <x:Label ID="SecrecyLevel2" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="级别：">
                                        </x:Label>

                                        <x:DropDownList ID="DropDownList_SecrecyLevel" Label="保密级别" Width="195px"
                                            ShowLabel="true" AutoPostBack="false" runat="server" Required="true" TabIndex="12">
                                        </x:DropDownList>

                                    </Items>
                                </x:Panel>
                            </Items>
                        </x:Panel>

                        <x:Panel ID="Panel23" Title="空Panel" runat="server" BodyPadding="5px" ShowBorder="false"
                            ShowHeader="false" ColumnWidth="8%">
                        </x:Panel>

                        <x:Panel ID="Panel6" BoxFlex="1" runat="server" ColumnWidth="46%" BodyPadding="0px" ShowBorder="false"
                            ShowHeader="false">

                            <Items>

                                <x:Label ID="Label19" runat="server" Label="Label" Text=" " Height="10px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel9" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label18" Width="100px" runat="server" CssClass="marginr"
                                            ShowLabel="false" Text="合同负责人：">
                                        </x:Label>
                                        <x:TextBox ID="txtChargePerson" Label="合同负责人" ShowLabel="true"
                                            Width="200px" CssClass="marginr" runat="server" MaxLength="20" TabIndex="7">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label11" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel22" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="labStratTime" Width="100px" runat="server" CssClass="marginr"
                                            ShowLabel="false" Text="合同经费：">
                                        </x:Label>
                                        <%-- <x:TextBox ID="txtStratTime" ShowLabel="false" Required="true" Width="150px" 

CssClass="marginr" runat="server" >
                                        </x:TextBox>--%>
                                        <x:TextBox ID="txtPactMoney" Label="合同经费" ShowLabel="true"
                                            Width="200px" CssClass="marginr" runat="server" MaxLength="20" TabIndex="7">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label8" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel7" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="labEndTime" Width="100px" runat="server" CssClass="marginr" ShowLabel="false"
                                            Text="实到经费：">
                                        </x:Label>
                                        <x:TextBox ID="txtRealMoney" Label="实到经费" ShowLabel="true"
                                            Width="200px" CssClass="marginr" runat="server" MaxLength="20" TabIndex="7">
                                        </x:TextBox>
                                        <%--RequiredMessage="会议结束时间不能为空" CompareControl="DatePicker_StratTime" 

CompareOperator="GreaterThanEqual" CompareMessage="结束日期应该大于开始日期！"--%>
                                        <%--  <x:TextBox ID="txtEndTime" ShowLabel="false" Required="true" Width="200px" 

CssClass="marginr" runat="server" >
                                    </x:TextBox>--%>
                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label1" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel1" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label3" Width="100px" runat="server" CssClass="marginr"
                                            ShowLabel="false" Text="合同完成情况：">
                                        </x:Label>
                                        <x:TextBox ID="txtPactCompletion" Label="合同完成情况" ShowLabel="true"
                                            Width="200px" CssClass="marginr" runat="server" MaxLength="20" TabIndex="10">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label12" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>


                                <x:Panel ID="Panel24" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="labProceedingsofTitle" Width="100px" runat="server" CssClass="marginr"
                                            ShowLabel="false" Text="文件保存人：">
                                        </x:Label>
                                        <x:TextBox ID="txtIsExistingFile" Label="文件保存人" ShowLabel="true" Width="200px"
                                            CssClass="marginr" runat="server" MaxLength="20"
                                            TabIndex="11">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label216" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel12" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label14" Width="100px" runat="server" CssClass="marginr"
                                            ShowLabel="false" Text="文件编号：">
                                        </x:Label>
                                        <x:TextBox ID="txtFileNum" Label="文件编号" ShowLabel="true" Width="200px"
                                            CssClass="marginr" runat="server" MaxLength="20"
                                            TabIndex="11">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>
                            </Items>

                        </x:Panel>
                    </Items>
                </x:Panel>
            </Items>
        </x:Panel>
        <asp:Panel ID="Panelasp" ShowHeader="false" CssClass="formitem" ShowBorder="false"
            Layout="Column" runat="server" BackColor="White" Height="25px">
            <asp:Label ID="Label" Width="72px" runat="server" CssClass="marginr" ShowLabel="false" Text=" ">
            </asp:Label>

            <asp:Label ID="Label24" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="相关文档：">
            </asp:Label>
            <input type="file" id="fileupload" style="width: 190px" runat="server" />
        </asp:Panel>
        <x:Panel ID="Panel4" ShowHeader="false" CssClass="formitem" ShowBorder="false" Layout="Column" runat="server">
            <Items>
                <x:Toolbar ID="Toolbar2" runat="server" ColumnWidth="100%">
                    <Items>
                        <x:Label ID="Label9" Width="300px" runat="server" ShowLabel="true" Text=" ">
                        </x:Label>
                        <x:Button ID="btnSave" runat="server" CssClass="marginr" Text="保存" Size="Medium" Icon="Add"
                            ConfirmText="确定保存？" ConfirmTarget="Top" ValidateForms="Panel2" Type="Submit" OnClick="btnSave_Click">
                        </x:Button>
                        <x:Button ID="btnSet" runat="server" CssClass="marginr" Text="重置" Size="Medium" Icon="Delete"
                            ConfirmText="确定重置？" EnablePostBack="true" ConfirmTarget="Top" OnClick="btnReSet_Click">
                        </x:Button>
                    </Items>
                </x:Toolbar>
            </Items>
        </x:Panel>
    </form>
</body>
</html>
