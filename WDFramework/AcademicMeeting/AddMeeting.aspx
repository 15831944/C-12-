<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddMeeting.aspx.cs" Inherits="WDFramework.AcademicMeeting.AddMeeting" %>

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
        <x:pagemanager id="PageManager1" runat="server" />

        <x:panel id="Panel2" runat="server" height="395px" showborder="false" enablecollapse="true" autoscroll="false"
            layout="VBox" boxconfigalign="Stretch" boxconfigposition="Start" boxconfigpadding="0"
            boxconfigchildmargin="0 0 0 0" showheader="false">
            <Items>
                <x:Panel ID="Panel20" runat="server" Height="395px" ShowBorder="false" EnableCollapse="true" AutoScroll="false"
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
                                        <x:Label ID="labMeetingName" Width="80px" runat="server" CssClass="marginr" CssStyle="text-align:right"
                                            ShowLabel="false" Text="会议">
                                        </x:Label>
                                        <x:Label ID="Label22" Width="80px" runat="server" CssClass="marginr" CssStyle="text-align:left"
                                            ShowLabel="false" Text="名称：">
                                        </x:Label>
                                        <%--正则表达式:中文、英文、数字包括下划线"^[\u4E00-\u9FA5A-Za-z0-9_]+$"--%>
                                        <x:TextBox ID="txtMeetingName" ShowLabel="true" Label="会议名称" Required="true" Regex="^\s*|\s*$"
                                            Width="200px" CssClass="marginr" runat="server" RequiredMessage="会议名称不能为空" MaxLength="100" MaxLengthMessage="最多可输入100个字符" TabIndex="1">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label4" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel5" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label25" Width="80px" runat="server" CssClass="marginr" CssStyle="text-align:right"
                                            ShowLabel="false" Text="会议">
                                        </x:Label>
                                        <x:Label ID="Label26" Width="80px" runat="server" CssClass="marginr" CssStyle="text-align:left"
                                            ShowLabel="false" Text="分类：">
                                        </x:Label>
                                        <x:DropDownList ID="DropDownList_MeetingSort" Label="会议分类" Width="195px"
                                            ShowLabel="true" runat="server" TabIndex="2">
                                            <x:ListItem Text="请选择" Value="0" />
                                        </x:DropDownList>

                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label2" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel14" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label13" Width="80px" runat="server" CssClass="marginr" CssStyle="text-align:right"
                                            ShowLabel="false" Text="主办">
                                        </x:Label>
                                        <x:Label ID="Label27" Width="80px" runat="server" CssClass="marginr" CssStyle="text-align:left"
                                            ShowLabel="false" Text="方：">
                                        </x:Label>
                                        <x:TextBox ID="txtOrganizer" Label="主办方" ShowLabel="true" Required="true" Width="200px"
                                            CssClass="marginr" runat="server" RequiredMessage="主办方不能为空" MaxLength="40" CompareMessage="输入最大长度为40个字符" TabIndex="3">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label5" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel15" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label28" Width="80px" runat="server" CssClass="marginr" CssStyle="text-align:right"
                                            ShowLabel="false" Text="协办">
                                        </x:Label>
                                        <x:Label ID="Label29" Width="80px" runat="server" CssClass="marginr" CssStyle="text-align:left"
                                            ShowLabel="false" Text="方：">
                                        </x:Label>
                                        <x:TextBox ID="txtCoorganizer" Label="协办方" ShowLabel="true" Required="true"
                                            Width="200px" MaxLength="40" CssClass="marginr" runat="server"
                                            TabIndex="4">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label6" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel16" ShowHeader="false" CssClass="formitem" ShowBorder="false"
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
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel8" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label32" Width="80px" runat="server" CssClass="marginr" CssStyle="text-align:right"
                                            ShowLabel="false" Text="会议">
                                        </x:Label>
                                        <x:Label ID="Label33" Width="80px" runat="server" CssClass="marginr" CssStyle="text-align:left"
                                            ShowLabel="false" Text="主席：">
                                        </x:Label>
                                        <x:TextBox ID="txtMajorPerson" Label="会议主席" Required="true" ShowLabel="true"
                                            Width="200px" CssClass="marginr" runat="server" MaxLength="20" TabIndex="6">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Labe20" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel11" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label15" Width="80px" runat="server" CssClass="marginr" CssStyle="text-align:right"
                                            ShowLabel="false" Text="会议">
                                        </x:Label>
                                        <x:Label ID="Label34" Width="80px" runat="server" CssClass="marginr" CssStyle="text-align:left"
                                            ShowLabel="false" Text="参加人员：">
                                        </x:Label>
                                        <x:TextArea runat="server" EmptyText="人名之间有逗号隔开" MaxLength="200" MaxLengthMessage="最多输入100个字符" ShowLabel="true" ID="AttendMeetingPeople" Width="195px" Height="70px" TabIndex="24" CssStyle="overflow-y:scroll">
                                        </x:TextArea>
                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label37" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel17" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label35" Width="80px" runat="server" CssClass="marginr" CssStyle="text-align:right"
                                            ShowLabel="false" Text="会议">
                                        </x:Label>
                                        <x:Label ID="Label36" Width="80px" runat="server" CssClass="marginr" CssStyle="text-align:left"
                                            ShowLabel="false" Text="照片：">
                                        </x:Label>
                                        <x:FileUpload ID="photoupload" width="195" runat="server" Label="会议照片" ShowLabel="true" TabIndex="27">
                                        </x:FileUpload>
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
                                <x:Label ID="Label17" runat="server" Label="Label" Text=" " Height="10px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel10" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label20" Width="100px" runat="server" CssClass="marginr"
                                            ShowLabel="false" Text="会议主持人：">
                                        </x:Label>
                                        <x:TextBox ID="txtMeetingHost" Label="会议主持人" Required="true" ShowLabel="true"
                                            Width="200px" CssClass="marginr" runat="server" MaxLength="20" TabIndex="6">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label19" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel9" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label18" Width="100px" runat="server" CssClass="marginr"
                                            ShowLabel="false" Text="会议主题：">
                                        </x:Label>
                                        <x:TextBox ID="txtMajorTheme" Label="会议主题" Required="true" ShowLabel="true"
                                            Width="200px" CssClass="marginr" runat="server" MaxLength="100" TabIndex="7">
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
                                            ShowLabel="false" Text="会议开始时间：">
                                        </x:Label>
                                        <%-- <x:TextBox ID="txtStratTime" ShowLabel="false" Required="true" Width="150px" CssClass="marginr" runat="server" >
                                        </x:TextBox>--%>
                                        <x:DatePicker runat="server" Width="195px" ShowLabel="true" Label="会议开始时间"
                                            DateFormatString="yyyy-MM-dd" EnableEdit="false"
                                            ID="DatePicker_StratTime" TabIndex="8">
                                        </x:DatePicker>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label8" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel7" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="labEndTime" Width="100px" runat="server" CssClass="marginr" ShowLabel="false"
                                            Text="会议结束时间：">
                                        </x:Label>
                                        <x:DatePicker runat="server" Width="195px" ShowLabel="true" Label="会议结束时间"
                                            DateFormatString="yyyy-MM-dd" EnableEdit="false"
                                            ID="DatePicker_EndTime" TabIndex="9">
                                        </x:DatePicker>
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
                                            ShowLabel="false" Text="会议参加人数：">
                                        </x:Label>
                                        <x:TextBox ID="txtMeetingCount" Label="会议参加人数" Required="true" ShowLabel="true" Regex="^[1-9]\d*$"
                                            Width="200px" CssClass="marginr" runat="server" MaxLength="50" TabIndex="10">
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
                                            ShowLabel="false" Text="论文集名称：">
                                        </x:Label>
                                        <x:TextBox ID="txtProceedingsofTitle" Label="论文集名称" ShowLabel="true" Width="200px"
                                            CssClass="marginr" runat="server" Required="true" MaxLength="20" CompareMessage="输入最大长度为20个字符"
                                            TabIndex="11">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label7" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel25" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="SecrecyLevel" Width="100px" runat="server" CssClass="marginr"
                                            ShowLabel="false" Text="保密级别：">
                                        </x:Label>

                                        <x:DropDownList ID="DropDownList_SecrecyLevel" Label="保密级别" Width="195px"
                                            ShowLabel="true" AutoPostBack="false" runat="server" Required="true" TabIndex="12">
                                        </x:DropDownList>

                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label21" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel19" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Remark" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="会议内容：">
                                        </x:Label>
                                        <x:TextArea runat="server" MaxLength="200" MaxLengthMessage="最多输入200个字符" ShowLabel="true" ID="MeetingContent" Width="195px" Height="60px" TabIndex="24" CssStyle="overflow-y:scroll">
                                        </x:TextArea>
                                    </Items>
                                </x:Panel>
                            </Items>
                        </x:Panel>
                    </Items>
                </x:Panel>
            </Items>
        </x:panel>
        <asp:Panel ID="Panelasp" ShowHeader="false" CssClass="formitem" ShowBorder="false"
            Layout="Column" runat="server" BackColor="White" Height="50px">
            <asp:Label ID="Label" Width="67px" runat="server" CssClass="marginr" ShowLabel="false" Text="">
            </asp:Label>
            <asp:Label ID="Label24" Width="95px" runat="server" CssClass="marginr" ShowLabel="false" Text="相关文档：">
            </asp:Label>
            <input type="file" id="fileupload" style="width: 190px" runat="server" />
        </asp:Panel>
        <x:panel id="Panel4" showheader="false" cssclass="formitem" showborder="false" layout="Column" runat="server">
            <Items>
                <x:Toolbar ID="Toolbar2" runat="server" ColumnWidth="100%">
                    <Items>
                        <x:Label ID="Label9" Width="300px" runat="server" ShowLabel="true" Text=" ">
                        </x:Label>
                        <x:Button ID="btnSave" runat="server" CssClass="marginr" Text="保存" Size="Medium" Icon="Add"
                            ConfirmText="确定保存？" ConfirmTarget="Top" ValidateForms="Panel2" Type="Submit" OnClick="btnSave_Click">
                        </x:Button>
                        <x:Button ID="btnSet" runat="server" CssClass="marginr" Text="重置" Size="Medium" Icon="Delete"
                            ConfirmText="确定重置？" EnablePostBack="true" ConfirmTarget="Top" OnClick="btnSet_Click">
                        </x:Button>
                    </Items>
                </x:Toolbar>
            </Items>
        </x:panel>
    </form>
</body>
</html>
