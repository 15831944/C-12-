<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddNAReporting.aspx.cs" Inherits="WDFramework.NewAcademicReporting.AddNAReporting" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body oncontextmenu='return false'>
    <%--取消鼠标右键的点击--%>
    <form id="form1" runat="server">

            <x:PageManager ID="PageManager1" AutoSizePanelID="RegionPanel1" runat="server" />

            <x:Panel ID="Panel16" runat="server" BodyPadding="5px" Height="500px"
                ShowBorder="false" Layout="Column" BoxConfigAlign="Stretch" BoxConfigPosition="Start" ShowHeader="false">
                <Items>
                    <x:Panel ID="Panel2" Title="项目概要" BoxFlex="1" runat="server" ColumnWidth ="50%"
                        BodyPadding="30px" ShowBorder="false" ShowHeader="false">

                        <Items>

                            <x:Panel ID="Panel3" BoxFlex="1" runat="server" ColumnWidth="47%" ShowBorder="false" ShowHeader="false">
                                <Items>
                                    <x:Panel ID="Panel10" ShowHeader="false" CssClass="formitem" ShowBorder="false" Layout="Column" runat="server">
                                        <Items>
                                            <x:Label ID="Label21" Width="50px" CssStyle="text-align:right" runat="server"
                                                CssClass="marginr" ShowLabel="false" Text="学术报告">
                                            </x:Label>
                                            <x:Label ID="Label22" Width="60px" runat="server" CssClass="marginr" ShowLabel="false" Text="名称:">
                                            </x:Label>
                                            <x:TextBox ID="txtReportName" MaxLength="100" MaxLengthMessage="最多可输入100个字符" ShowLabel="true" Label="学术报告名称" Required="true"
                                                Width="200px" CssClass="marginr" runat="server" TabIndex="1">
                                            </x:TextBox>
                                        </Items>
                                    </x:Panel>
                                    <x:Label ID="Label23" runat="server" Label="Label" Text=" " Height="20px">
                                    </x:Label>
                                    <%--这是空行--%>

                                    <x:Panel ID="Panel11" ShowHeader="false" CssClass="formitem" ShowBorder="false" Layout="Column" runat="server">
                                        <Items>
                                            <x:Label ID="Label3" Width="50px" CssStyle="text-align:right" runat="server"
                                                CssClass="marginr" ShowLabel="false" Text="报告人">
                                            </x:Label>
                                            <x:Label ID="labNAReportingName" Width="60px" runat="server" CssClass="marginr" ShowLabel="false" Text=":">
                                            </x:Label>
                                            <x:TextBox ID="txtReportPeople" MaxLength="10" ShowLabel="true" Label="报告人" Required="true"
                                                Width="200px" CssClass="marginr" runat="server" TabIndex="2">
                                            </x:TextBox>
                                        </Items>
                                    </x:Panel>

                                    <x:Label ID="Label1" runat="server" Label="Label" Text=" " Height="25px">
                                    </x:Label>
                                    <%--这是空行--%>

                                    <x:Panel ID="Panel5" ShowHeader="false" CssClass="formitem" ShowBorder="false" Layout="Column" runat="server">
                                        <Items>
                                            <x:Label ID="Label18" Width="50px" CssStyle="text-align:right" runat="server"
                                                CssClass="marginr" ShowLabel="false" Text="职称">
                                            </x:Label>
                                            <x:Label ID="NAReportingName" Width="60px" runat="server" CssClass="marginr" ShowLabel="false" Text=":">
                                            </x:Label>
                                            <x:TextBox ID="txtJobName" MaxLength="20" ShowLabel="true" Label="职称"
                                                Width="200px" CssClass="marginr" runat="server" TabIndex="3">
                                            </x:TextBox>
                                        </Items>
                                    </x:Panel>

                                    <x:Label ID="Label2" runat="server" Label="Label" Text=" " Height="25px">
                                    </x:Label>
                                    <%--这是空行--%>

                                    <x:Panel ID="Panel1" ShowHeader="false" CssClass="formitem" ShowBorder="false" Layout="Column" runat="server">
                                        <Items>
                                            <x:Label ID="Label4" Width="50px" CssStyle="text-align:right" runat="server"
                                                CssClass="marginr" ShowLabel="false" Text="职务">
                                            </x:Label>
                                            <x:Label ID="Label5" Width="60px" runat="server" CssClass="marginr" ShowLabel="false" Text=":">
                                            </x:Label>
                                            <x:TextBox ID="txtJobMission" MaxLength="20" ShowLabel="true" Label="职务"
                                                Width="200px" CssClass="marginr" runat="server" TabIndex="4">
                                            </x:TextBox>
                                        </Items>
                                    </x:Panel>

                                    <x:Label ID="Label6" runat="server" Label="Label" Text=" " Height="25px">
                                    </x:Label>
                                    <%--这是空行--%>

                                    <x:Panel ID="Panel12" ShowHeader="false" CssClass="formitem" ShowBorder="false" Layout="Column" runat="server">
                                        <Items>
                                            <x:Label ID="Label7" Width="50px" CssStyle="text-align:right" runat="server"
                                                CssClass="marginr" ShowLabel="false" Text="报告人单">
                                            </x:Label>
                                            <x:Label ID="Label8" Width="60px" runat="server" CssClass="marginr" ShowLabel="false" Text="位:">
                                            </x:Label>
                                            <x:TextBox ID="txtReportUnit" MaxLength="50" ShowLabel="true" Label="报告人单位" Required="true"
                                                Width="200px" CssClass="marginr" runat="server" TabIndex="4">
                                            </x:TextBox>
                                        </Items>
                                    </x:Panel>

                                    <x:Label ID="Label9" runat="server" Label="Label" Text=" " Height="25px">
                                    </x:Label>
                                    <%--这是空行--%>

                                    <x:Panel ID="Panel4" ShowHeader="false" CssClass="formitem" ShowBorder="false" Layout="Column" runat="server">
                                        <Items>
                                            <x:Label ID="Label10" Width="50px" CssStyle="text-align:right" runat="server"
                                                CssClass="marginr" ShowLabel="false" Text="报告人身">
                                            </x:Label>
                                            <x:Label ID="Label11" Width="60px" runat="server" CssClass="marginr" ShowLabel="false" Text="份证:">
                                            </x:Label>
                                            <x:TextBox ID="txtReport" MaxLength="20" ShowLabel="true" Label="报告人身份证" Regex="^\d{15}|\d{18}$"
                                                Width="200px" CssClass="marginr" runat="server" TabIndex="5">
                                            </x:TextBox>
                                        </Items>
                                    </x:Panel>

                                    <x:Label ID="Label12" runat="server" Label="Label" Text=" " Height="25px">
                                    </x:Label>
                                    <%--这是空行--%>

                                    <x:Panel ID="Panel6" ShowHeader="false" CssClass="formitem" ShowBorder="false" Layout="Column" runat="server">
                                        <Items>
                                            <x:Label ID="Label13" Width="50px" CssStyle="text-align:right" runat="server"
                                                CssClass="marginr" ShowLabel="false" Text="报告人手">
                                            </x:Label>
                                            <x:Label ID="Label14" Width="60px" runat="server" CssClass="marginr" ShowLabel="false" Text="机号:">
                                            </x:Label>
                                            <x:TextBox ID="txtReportTele" MaxLength="20" ShowLabel="true" Label="报告人手机号" Regex="^(13[0-9]|14[5|7]|15[0|1|2|3|5|6|7|8|9]|18[0|1|2|3|5|6|7|8|9])\d{8}$"
                                                Width="200px" CssClass="marginr" runat="server" TabIndex="6">
                                            </x:TextBox>
                                        </Items>
                                    </x:Panel>

                                    <x:Label ID="Label15" runat="server" Label="Label" Text=" " Height="25px">
                                    </x:Label>
                                    <%--这是空行--%>

                                    <x:Panel ID="Panel7" ShowHeader="false" CssClass="formitem" ShowBorder="false" Layout="Column" runat="server">
                                        <Items>
                                            <x:Label ID="Label16" Width="50px" CssStyle="text-align:right" runat="server"
                                                CssClass="marginr" ShowLabel="false" Text="学术兼职荣誉称号">
                                            </x:Label>
                                            <x:Label ID="Label17" Width="60px" runat="server" CssClass="marginr" ShowLabel="false" Text="及:">
                                            </x:Label>
                                            <x:TextBox ID="txtAcademicTitle" MaxLength="50" ShowLabel="true" Label="学术兼职及荣誉称号"
                                                Width="200px" CssClass="marginr" runat="server" TabIndex="7">
                                            </x:TextBox>
                                        </Items>
                                    </x:Panel>

                                    <x:Label ID="Label19" runat="server" Label="Label" Text=" " Height="25px">
                                    </x:Label>
                                    <%--这是空行--%>

                                    <x:Panel ID="Panel19" ShowHeader="false" CssClass="formitem" ShowBorder="false" Layout="Column" runat="server">
                                        <Items>
                                            <x:Label ID="Label34" Width="60px" runat="server" CssClass="marginr" CssStyle="text-align:right"
                                                ShowRedStar="true" ShowLabel="false" Text="备注">
                                            </x:Label>
                                            <x:Label ID="Remark" Width="50px" runat="server" CssClass="marginr" ShowLabel="false" Text="：">
                                            </x:Label>
                                            <x:TextArea runat="server" MaxLength="200" MaxLengthMessage="最多输入200个字符" ShowLabel="true"  Label="备注" 
                                                ID="txtRemark" Width="195px" Height="60px" TabIndex="8" CssStyle ="overflow-y:scroll">
                                            </x:TextArea>
                                        </Items>
                                    </x:Panel>

                                    <x:Label ID="Label20" runat="server" Label="Label" Text=" " Height="20px">
                                    </x:Label>
                                    <%--这是空行--%>

                                    
                                </Items>

                            </x:Panel>
                        </Items>

                    </x:Panel>
                    <x:Panel ID="Panel8" Title="项目概要" BoxFlex="1" runat="server" ColumnWidth ="50%"
                        BodyPadding="30px" ShowBorder="false" ShowHeader="false">
                        <Items>
                            <x:Panel ID="Panel9" ShowHeader="false" CssClass="formitem" ShowBorder="false" Layout="Column" runat="server">
                                        <Items>
                                            <x:Label ID="Label24" Width="50px" CssStyle="text-align:right" runat="server"
                                                CssClass="marginr" ShowLabel="false" Text="报告时间">
                                            </x:Label>
                                            <x:Label ID="Label25" Width="60px" runat="server" CssClass="marginr" ShowLabel="false" Text=":">
                                            </x:Label>
                                            <x:DatePicker ID="DatePikerReportTime" runat="server" Required="true" Width="195px" Label="时间" EnableEdit="false" TabIndex="9">
                                        </x:DatePicker>
                                        </Items>
                                    </x:Panel>
                                    <x:Label ID="Label26" runat="server" Label="Label" Text=" " Height="20px">
                                    </x:Label>
                                    <%--这是空行--%>

                                    <x:Panel ID="Panel13" ShowHeader="false" CssClass="formitem" ShowBorder="false" Layout="Column" runat="server">
                                        <Items>
                                            <x:Label ID="Label27" Width="50px" CssStyle="text-align:right" runat="server"
                                                CssClass="marginr" ShowLabel="false" Text="报告地点">
                                            </x:Label>
                                            <x:Label ID="Label28" Width="60px" runat="server" CssClass="marginr" ShowLabel="false" Text=":">
                                            </x:Label>
                                            <x:TextBox ID="txtReportPlace" MaxLength="50" ShowLabel="true" Label="报告地点" Required="true"
                                                Width="200px" CssClass="marginr" runat="server" TabIndex="10">
                                            </x:TextBox>
                                        </Items>
                                    </x:Panel>
                                    <x:Label ID="Label29" runat="server" Label="Label" Text=" " Height="20px">
                                    </x:Label>
                                    <%--这是空行--%>

                                    <x:Panel ID="Panel14" ShowHeader="false" CssClass="formitem" ShowBorder="false" Layout="Column" runat="server">
                                        <Items>
                                            <x:Label ID="Label30" Width="50px" CssStyle="text-align:right" runat="server"
                                                CssClass="marginr" ShowLabel="false" Text="报告类别">
                                            </x:Label>
                                            <x:Label ID="Label31" Width="60px" runat="server" CssClass="marginr" ShowLabel="false" Text=":">
                                            </x:Label>
                                            <x:TextBox ID="txtReportType" MaxLength="20" ShowLabel="true" Label="报告类别"
                                                Width="200px" CssClass="marginr" runat="server" TabIndex="11">
                                            </x:TextBox>
                                        </Items>
                                    </x:Panel>
                                    <x:Label ID="Label32" runat="server" Label="Label" Text=" " Height="20px">
                                    </x:Label>
                                    <%--这是空行--%>

                                    <x:Panel ID="Panel15" ShowHeader="false" CssClass="formitem" ShowBorder="false" Layout="Column" runat="server">
                                        <Items>
                                            <x:Label ID="Label33" Width="50px" CssStyle="text-align:right" runat="server"
                                                CssClass="marginr" ShowLabel="false" Text="申请经费">
                                            </x:Label>
                                            <x:Label ID="Label35" Width="60px" runat="server" CssClass="marginr" ShowLabel="false" Text=":">
                                            </x:Label>
                                            <x:TextBox ID="txtApplyFund" MaxLength="20" ShowLabel="true" Label="申请经费"
                                                Width="170px" CssClass="marginr" runat="server" TabIndex="12">
                                            </x:TextBox>
                                            <x:Label ID="Label50" Width="30px" CssStyle="text-align:right" runat="server"
                                                CssClass="marginr" ShowLabel="false" Text="万元">
                                            </x:Label>
                                        </Items>
                                    </x:Panel>
                                    <x:Label ID="Label36" runat="server" Label="Label" Text=" " Height="20px">
                                    </x:Label>
                                    <%--这是空行--%>

                                    <x:Panel ID="Panel17" ShowHeader="false" CssClass="formitem" ShowBorder="false" Layout="Column" runat="server">
                                        <Items>
                                            <x:Label ID="Label37" Width="50px" CssStyle="text-align:right" runat="server"
                                                CssClass="marginr" ShowLabel="false" Text="参与人数">
                                            </x:Label>
                                            <x:Label ID="Label38" Width="60px" runat="server" CssClass="marginr" ShowLabel="false" Text=":">
                                            </x:Label>
                                            <x:TextBox ID="txtPeopleCount" MaxLength="4" ShowLabel="true" Label="参与人数" Regex="^[1-9]\d*$"
                                                Width="200px" CssClass="marginr" runat="server" TabIndex="13" Required="true">
                                            </x:TextBox>
                                        </Items>
                                    </x:Panel>
                                    <x:Label ID="Label39" runat="server" Label="Label" Text=" " Height="20px">
                                    </x:Label>
                                    <%--这是空行--%>

                                    <x:Panel ID="Panel18" ShowHeader="false" CssClass="formitem" ShowBorder="false" Layout="Column" runat="server">
                                        <Items>
                                            <x:Label ID="Label40" Width="50px" CssStyle="text-align:right" runat="server"
                                                CssClass="marginr" ShowLabel="false" Text="主要参与">
                                            </x:Label>
                                            <x:Label ID="Label41" Width="60px" runat="server" CssClass="marginr" ShowLabel="false" Text="人：">
                                            </x:Label>
                                            <x:TextArea runat="server" MaxLength="100" MaxLengthMessage="最多输入200个字符" ShowLabel="true"  Label="主要参与人" 
                                                 EmptyText="20个，用逗号隔开" ID="txtMajorPeople" Width="195px" Height="60px" TabIndex="14" CssStyle ="overflow-y:scroll">
                                            </x:TextArea>
                                        </Items>
                                    </x:Panel>
                                    <x:Label ID="Label42" runat="server" Label="Label" Text=" " Height="20px">
                                    </x:Label>
                                    <%--这是空行--%>

                                    <x:Panel ID="Panel20" ShowHeader="false" CssClass="formitem" ShowBorder="false" Layout="Column" runat="server">
                                        <Items>
                                            <x:Label ID="Label43" Width="50px" CssStyle="text-align:right" runat="server"
                                                CssClass="marginr" ShowLabel="false" Text="主办单位">
                                            </x:Label>
                                            <x:Label ID="Label44" Width="60px" runat="server" CssClass="marginr" ShowLabel="false" Text=":">
                                            </x:Label>
                                            <x:TextBox ID="txtOrganizers" MaxLength="10" ShowLabel="true" Label="主办单位"
                                                Width="200px" CssClass="marginr" runat="server" TabIndex="15">
                                            </x:TextBox>
                                        </Items>
                                    </x:Panel>
                                    <x:Label ID="Label45" runat="server" Label="Label" Text=" " Height="20px">
                                    </x:Label>
                                    <%--这是空行--%>

                                    <x:Panel ID="Panel21" ShowHeader="false" CssClass="formitem" ShowBorder="false" Layout="Column" runat="server">
                                        <Items>
                                            <x:Label ID="Label46" Width="50px" CssStyle="text-align:right" runat="server"
                                                CssClass="marginr" ShowLabel="false" Text="协办单位">
                                            </x:Label>
                                            <x:Label ID="Label47" Width="60px" runat="server" CssClass="marginr" ShowLabel="false" Text="：">
                                            </x:Label>
                                            <x:TextArea runat="server" MaxLength="100" MaxLengthMessage="最多输入200个字符" ShowLabel="true"  Label="协办单位" 
                                                 EmptyText="3个，用逗号隔开" ID="txtCoorganizer" Width="195px" Height="60px" TabIndex="16" CssStyle ="overflow-y:scroll">
                                            </x:TextArea>
                                        </Items>
                                    </x:Panel>
                                    <x:Label ID="Label48" runat="server" Label="Label" Text=" " Height="20px">
                                    </x:Label>
                                    <%--这是空行--%>

                                    <x:Panel ID="Panel22" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="labSecrecyLevel" Width="110px" runat="server" CssClass="marginr"
                                            ShowLabel="false" Text="保密级别：">
                                        </x:Label>
                                        <x:DropDownList ID="DropDownList_SecrecyLevel" Label="保密级别" Width="195px" ShowLabel="true"
                                            AutoPostBack="false" runat="server" Required="true" RequiredMessage="保密级别不能为空" TabIndex="17">
                                        </x:DropDownList>
                                    </Items>
                                </x:Panel>
                        </Items>

                      </x:Panel>
                     </Items>

                </x:Panel>

        <asp:Panel ID="Panelasp" ShowHeader="false" CssClass="formitem" ShowBorder="false"
            Layout="Column" runat="server" BackColor="White" Height="45px">
            <asp:Label ID="Label" Width="58px" runat="server" CssClass="marginr" ShowLabel="false" Text="">
            </asp:Label>
            <asp:Label ID="Label49" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="相关文件：">
            </asp:Label>
            <input type="file" id="fileupload" style="width: 190px" runat="server" />
        </asp:Panel>
        <x:Panel ID="Panel23" ShowHeader="false" CssClass="formitem" ShowBorder="false" Layout="Column" runat="server" Height="40">
            <Items>
                <x:Toolbar ID="Toolbar1" runat="server" ColumnWidth="100%" Height="45">
                    <Items>
                        <x:Label ID="Label51" Width="302px" runat="server" ShowLabel="true" Text=" ">
                        </x:Label>
                        <x:Button ID="Save" runat="server" CssClass="marginr" Type="Submit" Text="保存" Icon="Add" ConfirmText="确定保存？" Size="Medium" ValidateForms="Panel2" OnClick="Save_Click">
                        </x:Button>
                        <x:Button ID="DeleteAll" runat="server" CssClass="marginr" Text="重置" Icon="Delete" ConfirmText="确定重置？"
                            Size="Medium" OnClick="DeleteAll_Click">
                        </x:Button>
                    </Items>
                </x:Toolbar>
            </Items>
        </x:Panel>
    </form>
</body>
</html>
