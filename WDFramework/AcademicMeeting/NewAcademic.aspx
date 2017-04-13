<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewAcademic.aspx.cs" Inherits="WDFramework.AcademicMeeting.NewAcademic" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body oncontextmenu='return false'>
    <%--取消鼠标右键的点击--%>
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <x:Panel ID="Panel1" runat="server" BodyPadding="5px" EnableBackgroundColor="true"
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start"
            ShowHeader="false">

            <Items>
                <x:Grid ID="Grid_MeetingName" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                    EnableRowNumberPaging="true" EnableTextSelection="true" EnableCheckBoxSelect="false"
                    DataKeyNames="AcademicMeetingID" AllowSorting="true" SortColumnIndex="0"
                    AllowPaging="true" IsDatabasePaging="true" OnPageIndexChange="Grid_MeetingName_PageIndexChange" OnRowCommand="Grid_MeetingName_RowCommand">
                    <Toolbars>
                        <x:Toolbar ID="Toolbar" runat="server">
                            <Items>
                                <x:Label ID="labYear" Width="40px" runat="server" CssClass="marginr" ShowLabel="true" Text="年份">
                                </x:Label>
                                <x:DropDownList ID="DropDownList_Year" ShowLabel="false" AutoPostBack="true" runat="server" ForceSelection="true" EnableEdit="false">
                                    <x:ListItem Text="全部" Value="0" Selected="true" />
                                </x:DropDownList>
                                <x:Label ID="Label9" runat="server" Label="Label " Width="10" Text=" "></x:Label>
                                <x:Label ID="labUser" Width="60px" runat="server" CssClass="marginr" ShowLabel="true" Text="科研人员">
                                </x:Label>
                                <x:DropDownList ID="DropDownList_User" ShowLabel="false" AutoPostBack="true" runat="server" EnableEdit="true" ForceSelection="true">
                                    <x:ListItem Text="全部" Value="0" Selected="true" />
                                </x:DropDownList>
                                <x:Button ID="btnCheck" Text="搜索" Icon="SystemSearch" runat="server" Type="Submit" OnClick="btnCheck_Click">
                                </x:Button>
                                <x:Button ID="btnRefresh" runat="server" Icon="ArrowRotateClockwise" Text="刷新" OnClick="btnRefresh_Click">
                                </x:Button>
                                <x:Button ID="btnAddMeeting" runat="server" EnablePostBack="true" Icon="Add" Text="新增学术会议">
                                </x:Button>
                                <x:Button ID="btnAddReport" runat="server" EnablePostBack="true" Icon="Add" Text="新增学术报告">
                                </x:Button>
                                <x:Button ID="btnUpdateMeeting" Text="编辑选中行" Icon="BulletEdit" EnablePostBack="true" runat="server" OnClick="btnUpdateMeeting_Click">
                                </x:Button>
                                <x:Button ID="btnDelete" Text="删除选中会议" Icon="Delete" EnablePostBack="true" runat="server" OnClick="btnDelete_Click"
                                    ConfirmText="确定删除？" ConfirmTarget="Top" Enabled="false">
                                </x:Button>
                            </Items>
                        </x:Toolbar>
                    </Toolbars>
                    <%--OnPageIndexChange="Grid_Info_PageIndexChange"--%>
                    <%--AllowPaging这是分页功能--%>
                    <PageItems>
                        <x:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                        </x:ToolbarSeparator>
                        <x:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
                        </x:ToolbarText>
                        <x:DropDownList ID="ddlGridPageSize" ShowLabel="false" Width="80px" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlGridPageSize_SelectedIndexChanged">
                            <x:ListItem Text="10" Value="10" />
                            <x:ListItem Text="20" Value="20" Selected="true" />
                            <x:ListItem Text="30" Value="30" />
                            <x:ListItem Text="50" Value="50" />
                        </x:DropDownList>
                    </PageItems>

                    <Columns>
                        <x:TemplateField Width="30px">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%#RowNumber( Container.DataItemIndex + 1) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:CheckBoxField ID="CBoxSelect_MeetingName" CommandName="CBSelect" DataField="fileid" runat="server" AutoPostBack="true" Enabled="true" RenderAsStaticField="false" Width="30" />
                        <x:BoundField DataField="EntryPerson" SortField="EntryPerson" Width="150px" HeaderText="录入人" Hidden="true" />
                        <x:BoundField DataField="MeetingName" SortField="MeetingName" Width="150px" HeaderText="会议名称" />
                        <x:BoundField DataField="MeetingSortName" SortField="MeetingSortName" Width="150px" HeaderText="会议分类" />
                        <x:BoundField DataField="Organizers" SortField="Organizers" Width="150px" HeaderText="主办方" />
                        <x:BoundField DataField="Coorganizers" SortField="Coorganizers" Width="150px" HeaderText="协办方" />
                        <x:BoundField DataField="MeetingMajorPerson" SortField="MeetingMajorPerson" Width="150px" HeaderText="会议主席" />
                        <x:BoundField DataField="MeetingMajorTheme" SortField="MeetingMajorTheme" Width="150px" HeaderText="会议主题" />
                        <x:BoundField DataField="StratTime" SortField="StratTime" Width="150px" HeaderText="开始时间" DataFormatString="{0:yyyy-MM-dd}" />
                        <x:BoundField DataField="EndTime" SortField="EndTime" Width="150px" HeaderText="结束时间" DataFormatString="{0:yyyy-MM-dd}" />
                        <x:BoundField DataField="MeetingPlace" SortField="MeetingPlace" Width="150px" HeaderText="会议地点" />
                        <x:BoundField DataField="MeetingCount" SortField="MeetingCount" Width="150px" HeaderText="会议参加人数" />
                        <x:BoundField DataField="MeetingHost" SortField="MeetingMajorTheme" Width="150px" HeaderText="会议主持人" />
                        <%--<x:BoundField DataField="ProceedingsofTitle" SortField="ProceedingsofTitle" Width="150px" HeaderText="论文集名称"/>--%>
                        <x:BoundField DataField="ProceedingsofTitle" SortField="ProceedingsofTitle" Width="150px" HeaderText="论文集名称" />
                        <x:TemplateField Width="80px" HeaderText="保密级别">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# ChangeSecrecyLevel(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "SecrecyLevel"))) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:TemplateField HeaderText="相关报告" Width="100px" ID="TemplateField2">
                            <ItemTemplate>
                                <a id="A2" href="javascript:<%# GetReportUrl(Eval("AcademicMeetingID")) %>">查看</a>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:TemplateField HeaderText="会议参加人员" Width="100px" ID="TemplateField3">
                            <ItemTemplate>
                                <a id="A3" href="javascript:<%# GetPeopletUrl(Eval("AcademicMeetingID")) %>">查看</a>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:TemplateField HeaderText="会议内容" Width="100px" ID="TemplateField4">
                            <ItemTemplate>
                                <a id="A4" href="javascript:<%# GetMeetingContentUrl(Eval("AcademicMeetingID")) %>">查看</a>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:TemplateField HeaderText="相关文档" Width="100px" ID="TemplateField1">
                            <ItemTemplate>
                                <a id="A1" href="javascript:<%# GetMeetingUrlDown(Eval("AcademicMeetingID")) %>">操作</a>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:TemplateField HeaderText="会议照片" Width="100px" ID="TemplateField5">
                            <ItemTemplate>
                                <a id="A1" href="javascript:<%# GetPhotoUrl(Eval("AcademicMeetingID")) %>">操作</a>
                            </ItemTemplate>
                        </x:TemplateField>
                        <%--<x:LinkButtonField HeaderText="&nbsp;" EnableAjax="false" Width="80px" CommandName="ActionDown" Text="下载" />--%>
                    </Columns>

                </x:Grid>
            </Items>
        </x:Panel>
        <x:Window ID="Window_AddMeeting" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableResize="false" Height="500px" Width="850px" Title="添加学术会议">
        </x:Window>
        <x:Window ID="Window_AddReport" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableResize="false" Height="420px" Width="450px" Title="添加学术报告">
        </x:Window>
        <x:Window ID="Window_Report" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableResize="false" Height="450px" Width="800px" Title="查询学术报告">
        </x:Window>
        <x:Window ID="Window_DownLoad" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableResize="false" Height="250px" Width="350px">
        </x:Window>
        <x:Window ID="Window_NoLibraryMessage" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableResize="false" Height="250px" Width="350px">
        </x:Window>
        <x:Window ID="Window_Import" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableResize="false" Height="250px" Width="350px">
        </x:Window>
        <x:Window ID="Window_AttendPeople" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableResize="false" Height="250px" Width="350px">
        </x:Window>
        <x:Window ID="Window_UppdateAcademic" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableResize="false" Height="500px" Width="850px" Title="更新学术会议信息">
        </x:Window>
        <x:Label ID="labResult" Visible="false" runat="server">
        </x:Label>
    </form>
</body>
</html>
