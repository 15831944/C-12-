<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Update_Patent.aspx.cs" Inherits="WebApplication1.Achievement.Patent.Update_Patent" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script type="text/javascript">
        function clearFile() {
            var pat = document.getElementById('PatentFile');
            pat.outerHTML = pat.outerHTML;
            var app = document.getElementById('ApplicationFile');
            app.outerHTML = app.outerHTML;
        }
    </script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <x:PageManager ID="PageManager1" AutoSizePanelID="Panel2" runat="server" />

            <x:Panel ID="Panel2" runat="server" Height="480px" Width="750px" ShowBorder="false" EnableCollapse="true"
                Layout="HBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5"
                BoxConfigChildMargin="0 5 0 0" ShowHeader="false" AutoScroll="true">

                <Items>
                    <x:Panel ID="Panel3" Title="项目概要" BoxFlex="1" runat="server"
                        BodyPadding="5px" ShowBorder="false" BoxMargin="0 0 0 30" ShowHeader="false">
                        <Items>
                            <x:Panel ID="Panel12" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="PatentName" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="专利名称：">
                                    </x:Label>
                                    <x:TextBox ID="tPatentName" MaxLength="100" MaxLengthMessage="最多可输入100个字符" ShowLabel="true" Label="专利名称：" Width="200px" CssClass="marginr" runat="server" TabIndex="1">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>

                            <x:Label ID="Label11" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>

                            <x:Panel ID="Panel14" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="AchievementID" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="所属成果名称：">
                                    </x:Label>
                                    <x:TextBox ID="tAchievement" MaxLength="100" MaxLengthMessage="最多可输入100个字符" ShowLabel="true" Label="所属成果名称" Width="200px" CssClass="marginr" runat="server" TabIndex="2" AutoPostBack="true" OnTextChanged="tAchievement_TextChanged">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>
                            <x:Label ID="Label2" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>

                            <x:Panel ID="Panel1" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="PatentNumber" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="专利号：">
                                    </x:Label>
                                    <x:TextBox ID="tPatentNumber" MaxLength="20" MaxLengthMessage="最多可输入20个字符" ShowLabel="true" Label="专利号：" Width="200px" CssClass="marginr" runat="server" TabIndex="3">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>


                            <x:Label ID="Label10" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>
                            <x:Panel ID="Panel17" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="PatentForm" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="专利类型：">
                                    </x:Label>
                                    <x:DropDownList ID="dPatentForm" ShowLabel="true" Label="专利类型" Width="195px" AutoPostBack="true" runat="server" TabIndex="4">
                                    </x:DropDownList>
                                </Items>
                            </x:Panel>
                            <x:Label ID="Label3" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>


                            <x:Panel ID="Panel5" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="ApplicationTime" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="申请时间：">
                                    </x:Label>
                                    <x:DatePicker ID="tApplicationTime" runat="server" ShowLabel="true" EnableEdit="false" Label="申请时间" Width="200px" TabIndex="5">
                                    </x:DatePicker>
                                </Items>
                            </x:Panel>
                            <x:Label ID="Label7" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>

                            <x:Panel ID="Panel16" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="AccreditTime" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="授权时间：">
                                    </x:Label>
                                    <x:DatePicker ID="tAccreditTime" runat="server" ShowLabel="true" EnableEdit="false" Label="授权时间" Width="200px" TabIndex="6">
                                    </x:DatePicker>
                                </Items>
                            </x:Panel>




                            <x:Label ID="Label9" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>

                            <x:Panel ID="Panel11" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="CertificateNumber" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="证书号：">
                                    </x:Label>
                                    <x:TextBox ID="tCertificateNumber" MaxLength="20" MaxLengthMessage="最多可输入20个字符" ShowLabel="true" Label="证书号" Width="200px" CssClass="marginr" runat="server" TabIndex="7">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>


                            <x:Label ID="Label1" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>
                            <x:Panel ID="Panel21" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="GivenUnit" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="授予机构：">
                                    </x:Label>
                                    <%--<x:TextArea runat="server" ID="TextArea1" Width="150px" Height="60px" AutoGrowHeight="true" AutoGrowHeightMin="100" AutoGrowHeightMax="200">
                                        </x:TextArea>--%>
                                    <x:TextBox ID="tGivenUnit" MaxLength="20" ShowLabel="true" MaxLengthMessage="最多可输入20个字符" Label="授予机构" Width="200px" CssClass="marginr" runat="server" TabIndex="8">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>
                            <x:Label ID="Label13" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>
                            <x:Panel ID="Panel18" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label6" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="状态：">
                                    </x:Label>
                                    <x:TextBox ID="tState" MaxLength="10" MaxLengthMessage="最多可输入10个字符" ShowLabel="true" Label="状态" Width="200px" CssClass="marginr" runat="server" TabIndex="9">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>
                            <x:Label ID="Label18" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <x:Panel ID="Panel9" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label14" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="所属部门：">
                                    </x:Label>
                                    <x:DropDownList Label="所属部门" AutoPostBack="false" EnableSimulateTree="true" TabIndex="10"
                                        runat="server" ID="DropDownListAgency" Width="195px">
                                    </x:DropDownList>
                                </Items>
                            </x:Panel>
                            <x:Label ID="Label16" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>
                            <x:Panel ID="Panel22" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label23" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="专利授权号：">
                                    </x:Label>
                                    <x:TextBox Label="专利授权号" AutoPostBack="false" TabIndex="21"
                                        runat="server" ID="tPatentAuthorization" Width="195px">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>
                        </Items>
                    </x:Panel>

                    <x:Panel ID="Panel6" Title="项目概要" BoxFlex="1" runat="server"
                        BodyPadding="5px" ShowBorder="false" BoxMargin="0 0 0 30" ShowHeader="false" AutoScroll="true">

                        <Items>

                            <x:Panel ID="Panel10" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label22" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="专利情况：">
                                    </x:Label>
                                    <x:DropDownList Label="专利情况" AutoPostBack="false" EnableSimulateTree="true"
                                        runat="server" ID="dPatentCondition" Width="195px" TabIndex="11">
                                    </x:DropDownList>
                                </Items>
                            </x:Panel>
                            <x:Label ID="Label21" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%-- 
                            <x:Panel ID="Panel22" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label24" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="申请号：">
                                    </x:Label>
                                    <x:TextBox ID="ApplyNum" MaxLength="20" MaxLengthMessage="最多可输入20个字符" ShowLabel="true" Label="申请号" Width="200px" CssClass="marginr" runat="server" TabIndex="12">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>
                            <x:Label ID="Label23" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            --%>
                            <%-- 
                            <x:Panel ID="Panel23" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label25" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="经费：">
                                    </x:Label>
                                    <x:TextBox ID="Fund" MaxLength="40" MaxLengthMessage="最多可输入40个字符" ShowLabel="true" Label="经费" Regex="^\d+\.?\d+$|^\d+$" RegexMessage="只能输入数字" Width="200px" CssClass="marginr" runat="server" TabIndex="13">
                                    </x:TextBox>
                                    <x:Label ID="Label29" Width="40" runat="server" Label="Label" Text="万元">
                                    </x:Label>
                                </Items>
                            </x:Panel>
                            <x:Label ID="Label26" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            --%>
                            <x:Panel ID="Panel13" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="PatentDepartment1" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="单位1：">
                                    </x:Label>
                                    <x:TextBox ID="tPatentDepartment1" MaxLength="40" MaxLengthMessage="最多可输入40个字符" ShowLabel="true" Label="单位1" Width="200px" CssClass="marginr" runat="server" TabIndex="14">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>
                            <x:Label ID="Label8" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>

                            <x:Panel ID="Panel8" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="lPatentDepartment2" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="单位2：">
                                    </x:Label>
                                    <x:TextBox ID="tPatentDepartment2" MaxLength="40" MaxLengthMessage="最多可输入40个字符" ShowLabel="true" Label="单位2" Width="200px" CssClass="marginr" runat="server" TabIndex="15">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>
                            <x:Label ID="Label12" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>

                            <x:Panel ID="Panel15" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="PatentDepartment3" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="单位3：">
                                    </x:Label>
                                    <x:TextBox ID="tPatentDepartment3" MaxLength="40" MaxLengthMessage="最多可输入40个字符" ShowLabel="true" Label="单位3" Width="200px" CssClass="marginr" runat="server" TabIndex="16">
                                    </x:TextBox>
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
                                    <%--  <x:TextBox ID="tSecrecyLevel"  Readonly="true" ShowLabel="true" Required="true" AutoPostBack="true" Label="保密级别" Width="200px" CssClass="marginr" runat="server" TabIndex="17">
                                    </x:TextBox>    --%>
                                    <x:DropDownList ID="drSecrecyLevel" ShowLabel="true" Label="保密级别：" EnableEdit="false" Width="195px" AutoPostBack="true" runat="server" TabIndex="17">
                                    </x:DropDownList>
                                </Items>
                            </x:Panel>
                            <x:Label ID="Label27" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <x:Panel ID="Panel24" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label28" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="第一发明人：">
                                    </x:Label>
                                    <x:TextBox ID="FirstPeople" MaxLength="20" MaxLengthMessage="最多可输入20个字符" ShowLabel="true" Label="第一发明人" Width="200px" CssClass="marginr" runat="server" TabIndex="18">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>
                            <x:Label ID="Label17" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>
                            <x:Panel ID="Panel20" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label19" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="全部发明人：">
                                    </x:Label>
                                    <x:TextArea runat="server" MaxLength="200" MaxLengthMessage="最多可输入200个字符" ShowLabel="true" EmptyText="两个或两个以上请用逗号隔开" CssStyle="overflow-y:scroll" Label="全部发明人" ID="PatentPeople" Width="195px" Height="70px" AutoGrowHeight="false" AutoGrowHeightMax="70" TabIndex="19">
                                    </x:TextArea>
                                </Items>
                            </x:Panel>
                            <x:Label ID="Label30" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>
                            <%--
                            <x:Panel ID="Panel25" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label5" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="成员：">
                                    </x:Label>
                                    <x:TextArea runat="server" MaxLength="200" MaxLengthMessage="最多可输入200个字符" ShowLabel="true" EmptyText="两个或两个以上请用逗号隔开" CssStyle="overflow-y:scroll" Label="成员" ID="PatentMember" Width="195px" Height="70px" AutoGrowHeight="false" AutoGrowHeightMax="70" TabIndex="19">
                                    </x:TextArea>
                                </Items>
                            </x:Panel>
                            <x:Label ID="Label31" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>

                            <x:Panel ID="Panel19" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Remark" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="备注：">
                                    </x:Label>
                                    <x:TextArea runat="server" MaxLength="200" MaxLengthMessage="最多可输入200个字符" ID="tRemark" Label="备注" ShowLabel="true" Width="195px" CssStyle="overflow-y:scroll" Height="70px" AutoGrowHeight="false" AutoGrowHeightMax="70" TabIndex="20">
                                    </x:TextArea>
                                </Items>
                            </x:Panel>
                            <x:Label ID="Label24" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <x:Panel ID="Panel25" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label26" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="专利证书号：">
                                    </x:Label>
                                    <x:TextBox runat="server" ID="tPatentCertificate" Label="专利证书号" ShowLabel="true" Width="195px" TabIndex="22">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>
                        </Items>
                    </x:Panel>

                </Items>
            </x:Panel>
            <asp:Panel ID="Panelasp" ShowHeader="false" ShowBorder="false"
                Layout="Column" runat="server" Height="25px" BackColor="White">
                <asp:Label ID="Labels" runat="server" Label="Label" CssClass="marginr" Text=" " Width="35px">
                </asp:Label>
                <asp:Label ID="Label20" runat="server" Label="Label" CssClass="marginr" Text="专利证书： " Width="95px">
                </asp:Label>
                <input type="file" id="PatentFile" style="width: 200px" runat="server" />
            </asp:Panel>
            <asp:Panel ID="Panel23" ShowHeader="false" ShowBorder="false"
                Layout="Column" runat="server" Height="25px" BackColor="White">
                <asp:Label ID="Label5" runat="server" Label="Label" CssClass="marginr" Text=" " Width="35px">
                </asp:Label>
                <asp:Label ID="Label25" runat="server" Label="Label" CssClass="marginr" Text="申请书： " Width="95px">
                </asp:Label>
                <input type="file" id="ApplicationFile" style="width: 200px" runat="server" />
            </asp:Panel>
            <x:Panel ID="Panel4" ShowHeader="false" CssClass="formitem" ShowBorder="false" Layout="Column" runat="server">
                <Items>
                    <x:Toolbar ID="Toolbar1" runat="server" ColumnWidth="100%">
                        <Items>
                            <x:Label ID="Label15" Width="310px" runat="server" ShowLabel="true" Text=" ">
                            </x:Label>
                            <x:Button ID="Save" runat="server" CssClass="marginr" Type="Submit" Text="保存" Size="Medium" Icon="Add" OnClick="Save_Click" ValidateForms="Panel2" ConfirmText="确定保存？">
                            </x:Button>
                            <x:Button ID="DeleteAll" runat="server" CssClass="marginr" Text="重置" ConfirmText="确定重置？" Size="Medium" Icon="Delete" OnClick="DeleteAll_Click">
                            </x:Button>
                        </Items>
                    </x:Toolbar>
                </Items>
            </x:Panel>
        </div>
    </form>
</body>
</html>
