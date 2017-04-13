<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Update_Project.aspx.cs" Inherits="WDFramework.Projects.UpdateProject" %>

<%@ Register Assembly="FineUI" Namespace="FineUI" TagPrefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">
        function clearFile() {
            var obj = document.getElementById('FileUploadFile');
            obj.outerHTML = obj.outerHTML;
        }
        function clearFiles() {
            var obj = document.getElementById('FileUploadFileM');
            obj.outerHTML = obj.outerHTML;
        }
    </script>
    <link href="../res/css/main.css" rel="stylesheet" type="text/css" />
</head>
<body oncontextmenu='return false'>
    <%--取消鼠标右键的点击--%>
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />

        <x:Panel ID="Panel2" runat="server" ShowBorder="false" EnableCollapse="false" AutoScroll="false"
            Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5" Height="380px"
            BoxConfigChildMargin="0" ShowHeader="false">
            <Items>
                <x:Panel ID="Panel188" runat="server" ShowBorder="false" EnableCollapse="false" AutoScroll="true"
                    Layout="Column" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5" Height="380px"
                    BoxConfigChildMargin="0" ShowHeader="false">
                    <Items>
                        <x:Panel ID="Panel3" Title="项目概要" runat="server" Width="400px" ShowBorder="false" BodyPadding="30px"
                            ShowHeader="false">
                            <Items>
                                <x:Panel ID="Panel12" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="ProjectName" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="项目名称：">
                                        </x:Label>
                                        <x:TextBox ID="ProjectName2" Label="项目名称" ShowLabel="true" MaxLength="100" MaxLengthMessage="最多输入100个字符" Width="200px" CssClass="marginr" runat="server" TabIndex="1">
                                        </x:TextBox>

                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label17" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel16556" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label10" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="项目内部编号（科技处）：">
                                        </x:Label>
                                        <x:TextBox ID="ProjectInNum" Label="项目内部编号（科技处）" ShowLabel="true" MaxLength="50" MaxLengthMessage="最多输入50个字符" Width="200px" CssClass="marginr" runat="server" TabIndex="2">
                                        </x:TextBox>

                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label15" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel34" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="PactNum" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="合同编号：">
                                        </x:Label>
                                        <x:TextBox ID="PactNum2" ShowLabel="true" Label="合同编号" MaxLength="20" MaxLengthMessage="最多输入20个字符" Width="200px" CssClass="marginr" runat="server" TabIndex="3">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>


                                <x:Label ID="Label133" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel11" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="TaskNum" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="课题编号：">
                                        </x:Label>
                                        <x:TextBox ID="TaskNum2" ShowLabel="true" Label="课题编号" MaxLength="20" MaxLengthMessage="最多输入20个字符" Width="200px" CssClass="marginr" runat="server" TabIndex="4">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>


                                <x:Label ID="Label7" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel8" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="SourceUnit" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="项目来源：">
                                        </x:Label>
                                        <x:TextBox ID="SourceUnit2" ShowLabel="true" Label="项目来源" MaxLength="20" MaxLengthMessage="最多输入20个字符" Width="200px" CssClass="marginr" runat="server" TabIndex="5">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>


                                <x:Label ID="Label2" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel20" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="ProjectNature" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="项目性质：">
                                        </x:Label>
                                        <x:DropDownList AutoPostBack="false" EnableSimulateTree="true" ShowLabel="false" TabIndex="7"
                                            Width="195px" CssClass="marginr" runat="server" ID="DropDownListNature">
                                        </x:DropDownList>

                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label9" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel7" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="ProjectLevel" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="项目级别：">
                                        </x:Label>
                                        <x:DropDownList AutoPostBack="false" EnableSimulateTree="true" ShowLabel="false" TabIndex="8"
                                            Width="195px" CssClass="marginr" runat="server" ID="DropDownListProjectLevel">
                                        </x:DropDownList>
                                    </Items>
                                </x:Panel>



                                <x:Label ID="Label127" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel10" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="CooperationForms" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="合作形式：">
                                        </x:Label>
                                        <%--<x:TextBox ID="CooperationForms2" ShowLabel="true" Label="合作形式" MaxLength="10" MaxLengthMessage="最多输入10个字符" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="9">
                                </x:TextBox>--%>
                                        <x:DropDownList AutoPostBack="false" EnableSimulateTree="true" ShowLabel="false" TabIndex="9"
                                            Width="195px" CssClass="marginr" runat="server" ID="DropDownListCooperationForms" Label="合作形式">
                                        </x:DropDownList>
                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label1" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel13" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="AgencyID" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="项目所属机构：">
                                        </x:Label>
                                        <x:DropDownList AutoPostBack="false" EnableSimulateTree="true" Label="项目所属机构" ShowLabel="false" TabIndex="10"
                                            Width="195px" CssClass="marginr" runat="server" ID="DropDownListAgencyP">
                                        </x:DropDownList>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label20" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel4" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label19" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="项目负责人(前三人)：">
                                        </x:Label>
                                        <x:TextBox ID="ProjectManager" ShowLabel="true" Label="项目负责人(前三人)" Width="200px" MaxLength="20" MaxLengthMessage="最多输入20个字符" CssClass="marginr" runat="server" TabIndex="11">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label4" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel30" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="ProjectHeads" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="实际负责人：">
                                        </x:Label>
                                        <x:TextBox ID="ProjectHeads2" ShowLabel="true" Label="实际负责人" Width="200px" MaxLength="20" MaxLengthMessage="最多输入20个字符" CssClass="marginr" runat="server" TabIndex="12">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label5" runat="server" Label="Label" Text=" " Height="30px"></x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel0824" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label25" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="项目成员:">
                                        </x:Label>
                                        <x:TextArea runat="server" MaxLength="200" MaxLengthMessage="最多输入200个字符" ShowLabel="true" ID="ProjectMember" Width="195px" Height="100px" TabIndex="24" CssStyle="overflow-y:scroll">
                                        </x:TextArea>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label8" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel24" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="AcceptUnit" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="承担部门：">
                                        </x:Label>
                                        <x:TextBox ID="AcceptUnit2" ShowLabel="true" Label="承担部门" MaxLength="20" MaxLengthMessage="最多输入20个字符" Width="200px" CssClass="marginr" runat="server" TabIndex="13">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>

                            </Items>
                        </x:Panel>

                        <x:Panel ID="Panel85" Title="项目信息" Width="400px" ShowBorder="false" BodyPadding="30px"
                            runat="server" ShowHeader="false">
                            <Items>
                                <x:Panel ID="Panel6" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="ProjectState" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="项目状态：">
                                        </x:Label>
                                        <x:DropDownList AutoPostBack="false" EnableSimulateTree="true" ShowLabel="false" TabIndex="14"
                                            Width="195px" CssClass="marginr" runat="server" ID="DropDownListState">
                                        </x:DropDownList>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label14" runat="server" Label="Label" Text=" " Height="28px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel14" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>

                                        <x:Label ID="StartTime" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="开始时间：">
                                        </x:Label>
                                        <x:DatePicker runat="server" Label="开始日期" EnableEdit="false" EmptyText="请选择开始日期" Width="195px" CssClass="marginr" TabIndex="15"
                                            ID="DatePickerStartTime">
                                        </x:DatePicker>


                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label131" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel31" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="EndTime" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="结束时间：">
                                        </x:Label>
                                        <x:DatePicker ID="DatePickerEndTime" EmptyText="请选择结束日期" CompareControl="DatePicker1" DateFormatString="yyyy-MM-dd"
                                            CompareOperator="GreaterThan" CompareMessage="结束日期应该大于开始日期" EnableEdit="false" Label="结束日期" TabIndex="16"
                                            runat="server" Width="195px" CssClass="marginr">
                                        </x:DatePicker>
                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label134" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel32" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="ExpectEndTime" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="预期完成时间：">
                                        </x:Label>
                                        <x:DatePicker ID="DatePickerExpectEndTime" EmptyText="请选择结束日期" CompareControl="DatePicker1" DateFormatString="yyyy-MM-dd"
                                            CompareOperator="GreaterThan" CompareMessage="结束日期应该大于开始日期" EnableEdit="false" Label="结束日期" TabIndex="17"
                                            runat="server" Width="195px" CssClass="marginr">
                                        </x:DatePicker>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label137" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel33" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="ExpecteResults" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="预期成果：">
                                        </x:Label>
                                        <%-- <x:TextBox ID="ExpecteResults2" ShowLabel="true" Label="预期成果" MaxLength="10" MaxLengthMessage="最多输入10个字符" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex=18">
                                </x:TextBox>--%>
                                        <x:DropDownList Label="预期成果" AutoPostBack="false" EnableSimulateTree="true" TabIndex="18"
                                            runat="server" ID="DropDownListExpecteResults" Width="195px">
                                        </x:DropDownList>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label13" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel23" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="GivenMoneyUnits" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="来款单位：">
                                        </x:Label>
                                        <x:TextBox ID="GivenMoneyUnits2" ShowLabel="true" Label="来款单位" MaxLength="50" MaxLengthMessage="最多输入50个字符" Width="200px" CssClass="marginr" runat="server" TabIndex="19">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label121" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel21" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="ApprovedMoney" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="项目经费(万元)：">
                                        </x:Label>
                                        <x:TextBox ID="ApprovedMoney2" ShowLabel="true" Label="项目经费" MaxLength="15" Regex="^\d+\.?\d+$|^\d+$" RegexMessage="只能输入数字" MaxLengthMessage="最多输入15个字符" Width="200px" CssClass="marginr" runat="server" TabIndex="20">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label6" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel22" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="GetMoney" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="到账金额(万元)：">
                                        </x:Label>
                                        <x:TextBox ID="GetMoney2" ShowLabel="true" Label="到账金额" MaxLength="15" Regex="^\d+\.?\d+$|^\d+$" RegexMessage="只能输入数字" MaxLengthMessage="最多输入15个字符" Width="200px" CssClass="marginr" runat="server" TabIndex="21">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>



                                <x:Label ID="Label11" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel17" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="ProjectType" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="管理费比例：">
                                        </x:Label>
                                        <x:TextBox ID="ManageMoney" ShowLabel="true" Label="管理费比例" Regex="^\d+\.?\d+$|^\d+$" MaxLength="10" MaxLengthMessage="最多输入10个字符" Width="200px" CssClass="marginr" runat="server" TabIndex="22">
                                        </x:TextBox>
                                        <x:Label ID="Label16" Width="20px" runat="server" CssClass="marginr" ShowLabel="false" Text="%">
                                        </x:Label>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label126" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel9" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="SecrecyLevel" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="保密级别：">
                                        </x:Label>
                                        <x:DropDownList Label="保密等级" AutoPostBack="false" EnableSimulateTree="true" TabIndex="23"
                                            runat="server" ID="DropDownListSecrecyLevel" Width="195px">
                                        </x:DropDownList>

                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label3" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel19" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Remark" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="备注:">
                                        </x:Label>
                                        <x:TextArea runat="server" MaxLength="200" MaxLengthMessage="最多输入200个字符" ShowLabel="true" ID="Remark2" Width="195px" Height="100px" TabIndex="24" CssStyle="overflow-y:scroll">
                                        </x:TextArea>
                                    </Items>
                                </x:Panel>


                            </Items>
                        </x:Panel>
                        <x:Panel ID="Panel15" ShowHeader="false" BodyPadding="30px" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="ProjectSort" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="项目分类：">
                                </x:Label>
                                <x:DropDownList AutoPostBack="true" ShowLabel="false" TabIndex="6"
                                    Width="80px" CssClass="marginr" runat="server" ID="DropDownListProjectSort" OnSelectedIndexChanged="DropDownListProjectSort_SelectedIndexChanged">
                                    <x:ListItem Text="一类" Value="一类" Selected="true" />
                                    <x:ListItem Text="二类" Value="二类" />
                                    <x:ListItem Text="三类" Value="三类" />

                                </x:DropDownList>
                                <x:Label ID="Label24" Width="20px" runat="server" CssClass="marginr" ShowLabel="false">
                                </x:Label>
                                <x:DropDownList AutoPostBack="false" EnableSimulateTree="true" ShowLabel="false"
                                    Width="497px" CssClass="marginr" runat="server" ID="DropDownListProjectSortName">
                                </x:DropDownList>

                            </Items>
                        </x:Panel>
                    </Items>
                </x:Panel>

            </Items>
        </x:Panel>
        <%--</Items>
            </x:Panel>--%>
        <asp:Panel ID="Panel5" ShowHeader="false" ShowBorder="false" ScrollBars="None"
            Layout="Column" runat="server" BackColor="White" Visible="false">
            <asp:Label ID="Label18" runat="server" Label="Label" CssClass="marginr" Text=" " Width="30px">
            </asp:Label>
            <asp:Label ID="Label21" runat="server" Label="Label" CssClass="marginr" Text="经济效益相关文件： " Width="110px">
            </asp:Label>
            <input type="file" id="FileUploadFile" style="width: 200px" runat="server" />
            <asp:Label ID="Label22" runat="server" Label="Label" CssClass="marginr" Text=" " Width="80px">
            </asp:Label>
            <asp:Label ID="Label23" runat="server" Label="Label" CssClass="marginr" Text="经费预算相关文件： " Width="110px">
            </asp:Label>
            <input type="file" id="FileUploadFileM" style="width: 200px" runat="server" />
        </asp:Panel>
        <x:Panel ID="Panel87" runat="server" ShowBorder="false" ShowHeader="false" AutoScroll="false">
            <Items>
                <x:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <%--<x:Button ID="Addstaff"  Text="添加项目相关人员" runat="server" Size="Large"  >
                          </x:Button>--%>
                        <x:Label ID="Label12" runat="server" Label="Label" Text=" " Width="350px"></x:Label>
                        <x:Button ID="Save" Text="保存" runat="server" Icon="Add" Size="Medium" ConfirmText="确定保存？" ConfirmTarget="Top" OnClick="Save_Click" ValidateForms="Panel2" Type="Submit">
                        </x:Button>
                        <x:Button ID="Reset" Text="重置" runat="server" Icon="Delete" Size="Medium" ConfirmText="确定重置？" ConfirmTarget="Top" OnClick="Reset_Click">
                        </x:Button>
                    </Items>
                </x:Toolbar>
            </Items>
        </x:Panel>
        <x:Window ID="WindowStaff" Popup="false" EnableIFrame="true" IFrameUrl="#" runat="server"
            EnableMaximize="true" EnableResize="true" Height="450px" Width="750px" Title="添加人员">
        </x:Window>
    </form>
</body>
</html>

