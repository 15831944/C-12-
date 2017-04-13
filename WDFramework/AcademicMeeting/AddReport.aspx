<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddReport.aspx.cs" Inherits="WDFramework.AcademicMeeting.AddReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script  type="text/javascript">
        function clearFile() {
            var obj = document.getElementById('fileupload');
            obj.outerHTML = obj.outerHTML;
        }
    </script>
</head>
<body oncontextmenu='return false'>
    <%--取消鼠标右键的点击--%>
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager2" runat="server" />

        <x:Panel ID="Panel2" runat="server" Height="315px" ShowBorder="false" EnableCollapse="true"
            Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" 
            BoxConfigChildMargin="10 40 0 0" ShowHeader="false">
            <Items>
                <x:Panel ID="Panel10" runat="server" ShowBorder="false" EnableCollapse="true" 
                    Layout="Column" BoxConfigAlign="Stretch" BoxConfigPosition="Start" Height="290px"
                    BoxConfigChildMargin="0 5 0 0" ShowHeader="false">

                    <Items>
                        <x:Panel ID="Panel3" Title="项目概要" runat="server" ColumnWidth="100%" ShowBorder="false" BodyPadding="20px"
                            ShowHeader="false">
                            <Items>

                                <x:Panel ID="Panel13" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="labReportName" Width="110px" runat="server" CssClass="marginr" ShowLabel="false"
                                            Text="报告名称：">
                                        </x:Label>
                                        <x:TextBox ID="txtReportName" Label="报告名称" ShowLabel="true" Required="true"
                                            RequiredMessage="报告名称不能为空" Width="200px" CssClass="marginr" runat="server" MaxLength="100" MaxLengthMessage="最多可输入100个字符"
                                            TabIndex="1">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label4" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel14" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="labReportPeople" Width="110px" runat="server" CssClass="marginr"
                                            ShowLabel="false" Text="报告人：">
                                        </x:Label>
                                        <x:TextBox ID="txtReportPeople" Label="报告人" ShowLabel="true" Required="true" Width="200px"
                                            CssClass="marginr" runat="server" MaxLength="10" TabIndex="2">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label5" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel15" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="labReportTime" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="报告时间：">
                                        </x:Label>
                                        <x:DatePicker runat="server" Width="195px" ShowLabel="true" Label="报告时间" DateFormatString="yyyy-MM-dd"
                                            EnableEdit="false"
                                            ID="DatePicker_ReportTime" RequiredMessage="报告时间不能为空" TabIndex="3">
                                        </x:DatePicker>
                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label2" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel21" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="labReportPlace" Width="110px" runat="server" CssClass="marginr" ShowLabel="false"
                                            Text="报告地点：">
                                        </x:Label>
                                        <x:TextBox ID="txtReportPlace" Label="报告地点" ShowLabel="true" Width="200px"
                                            Required="true" RequiredMessage="报告地点不能为空" CssClass="marginr" runat="server" MaxLength="40" CompareMessage="输入最大长度为40个字符"
                                            TabIndex="4">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label9" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel22" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="labAgency" Width="110px" runat="server" CssClass="marginr" ShowLabel="false"
                                            Text="所属部门：">
                                        </x:Label>
                                        <x:DropDownList ID="DropDownList_AgencyName" Label="所属部门" Width="195px" ShowLabel="true"
                                            AutoPostBack="false" runat="server" TabIndex="5">
                                            <x:ListItem Text="请选择" Value="0" />
                                        </x:DropDownList>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label6" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel5" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label1" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="所属会议：">
                                        </x:Label>
                                        <%-- <x:TextBox ID="Location2" ShowLabel="false" Required="true" Width="200px" CssClass="marginr" 

runat="server" >
                            </x:TextBox>--%>
                                        <x:DropDownList ID="DropDownListMeetingName" Label="所属会议" Width="195px" ShowLabel="true"
                                            AutoPostBack="false" runat="server" Required="true" RequiredMessage="所属会议不能为空" TabIndex="6">
                                            <%--<x:ListItem Text="请选择" Value="0" />--%>
                                        </x:DropDownList>

                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label7" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel11" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="SecrecyLevel" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="保密级别：">
                                        </x:Label>
                                        <%-- <x:TextBox ID="Location2" ShowLabel="false" Required="true" Width="200px" CssClass="marginr" 

runat="server" >
                            </x:TextBox>--%>
                                        <x:DropDownList ID="DropDownList_SecrecyLevel" Label="保密级别" Width="195px" ShowLabel="true"
                                            AutoPostBack="false" runat="server" Required="true" RequiredMessage="保密级别不能为空" TabIndex="7">
                                        </x:DropDownList>

                                    </Items>
                                </x:Panel>

                            </Items>

                        </x:Panel>
                    </Items>
                </x:Panel>
            </Items>
        </x:Panel>
        <asp:Panel ID="Panelasp" ShowHeader="false" CssClass="formitem" ShowBorder="false"
            Layout="Column" runat="server" BackColor="White" Height="40px">
            <asp:Label ID="Label" Width="17px" runat="server" CssClass="marginr" ShowLabel="false" Text="">
            </asp:Label>
            <asp:Label ID="Label16" Width="105px" runat="server" CssClass="marginr" ShowLabel="false" Text="相关文档：">
            </asp:Label>
            <input type="file" id="fileupload" style="width: 190px" runat="server" />
            <%-- <asp:FileUpload ID="FileUpload1" Width="190px" runat="server" BackColor="White" />--%>
        </asp:Panel>
        <x:Panel ID="Panel4" ShowHeader="false" CssClass="formitem" ShowBorder="false" Layout="Column" runat="server">
            <Items>
                <x:Toolbar ID="Toolbar1" runat="server" ColumnWidth="100%">
                    <Items>
                        <x:Label ID="Label15" Width="150px" runat="server" ShowLabel="true" Text=" ">
                        </x:Label>
                        <x:Button ID="Save" runat="server" CssClass="marginr" Type="Submit" Text="保存" Icon="Add" Size="Medium"
                            ValidateForms="Panel2" ConfirmText="确定保存？" OnClick ="btnSave_Click">
                        </x:Button>
                        <x:Button ID="DeleteAll" runat="server" CssClass="marginr" Icon="Delete" Text="重置" Size="Medium" ConfirmText="确定重置？" OnClick ="btnSet_Click">
                        </x:Button>
                    </Items>
                </x:Toolbar>
            </Items>
        </x:Panel>
    </form>
</body>
</html>
