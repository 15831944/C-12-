<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewMeeting.aspx.cs" Inherits="WDFramework.AcademicMeeting.NewMeeting" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager1" AutoSizePanelID="RegionPanel1" runat="server" />
        <x:RegionPanel ID="RegionPanel1" ShowBorder="false" runat="server">
            <Regions>
                <x:Region ID="Region2" ShowBorder="false" ShowHeader="false" Position="Center" Layout="HBox"
                    BoxConfigAlign="Stretch" BoxConfigPosition="Left" BodyPadding="5px 5px 5px 0"
                    EnableBackgroundColor="true" runat="server">
                    <Items>
                        <%--  --%>
                        <x:Grid ID="Grid_ReportInfo" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                             EnableRowNumberPaging="true" EnableTextSelection="true" EnableCheckBoxSelect="false"
                            DataKeyNames="ScienceReportID" AllowSorting="true" SortColumnIndex="0"
                            AllowPaging="true" IsDatabasePaging="true" OnPageIndexChange="Grid_ReportInfo_PageIndexChange" OnRowCommand="Grid_ReportInfo_RowCommand">
                            <Toolbars>
                                <x:Toolbar ID="Toolbar1" runat="server" Width="800px">
                                    <Items>
                                        <x:Label ID="Label2" Width="60px" runat="server" CssClass="marginr" ShowLabel="true" Text="所属部门">
                                        </x:Label>
                                        <x:DropDownList ID="DropDownList_Agency" ShowLabel="false" AutoPostBack="false" runat="server" Width="300px">
                                            <x:ListItem Text="全部" Value="0" />
                                        </x:DropDownList>
                                        <x:Button ID="btnCheckReport" Text="搜索" Icon="SystemSearch" EnablePostBack="true" runat="server" Type="Submit"
                                            OnClick="btnCheckReport_Click">
                                        </x:Button>
                                        <x:Button ID="btnRefreshReport" runat="server" Icon="ArrowRotateClockwise" Text="刷新" OnClick="btnRefreshReport_Click">
                                        </x:Button>
                                        <%--<x:Button ID="btnAddReport" Text="新增会议报告" Icon="Add" EnablePostBack="false" runat="server">
                                        </x:Button>--%>
                                        <x:Button ID="btnDeleteReport" Text="删除选中学术报告" Icon="Delete" ConfirmText="确定删除？" ConfirmTarget="Top" EnablePostBack="true" runat="server" OnClick="btnDeleteReport_Click" Enabled="false">
                                        </x:Button>
                                        <%--<x:Button ID="Get1" Text="导出所选报告信息" Icon="Disk" EnablePostBack="false" runat="server">
                                        </x:Button>--%>
                                    </Items>
                                </x:Toolbar>
                            </Toolbars>
                            <PageItems>
                                <x:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                                </x:ToolbarSeparator>
                                <x:ToolbarText ID="ToolbarText2" runat="server" Text="每页记录数：">
                                </x:ToolbarText>
                                <x:DropDownList ID="ddlGridPageSize" Width="80px" AutoPostBack="true" runat="server"
                                    OnSelectedIndexChanged="ddlGridPageSize_SelectedIndexChanged">
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
                                <x:CheckBoxField ID="CBoxSelect_Report" CommandName="CBSelect" DataField="fileid" runat="server" AutoPostBack="true" Enabled="true" RenderAsStaticField="false" Width="30" />
                                 
                                <x:BoundField DataField="EntryPerson" SortField="EntryPerson" Width="100px" HeaderText="录入人" Hidden="true" />
                                <x:BoundField DataField="SReportName" SortField="UserInfoBH" Width="100px" HeaderText="报告名称" />
                                <x:BoundField DataField="SReportPeople" SortField="LoginName" Width="100px" HeaderText="报告人" />


                                <x:BoundField DataField="SReportTime" DataFormatString="{0:yyyy-MM-dd}" SortField="SReportTime" Width="100px" HeaderText="报告时间" />
                                <x:BoundField DataField="SReportPlace" SortField="UserName" Width="150px" HeaderText="报告地点" />
                                <%--<x:BoundField  DataField="AgencyID" SortField="UserName" Width="150px" HeaderText="所属部门" />--%>
                                <x:TemplateField HeaderText="所属部门" Width="200px">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# AgencyName (Convert.ToInt32(DataBinder.Eval

(Container.DataItem, "AgencyID"))) %>'></asp:Label>
                                        <%--<asp:Label ID="LabelAgency" runat="server"></asp:Label>--%>
                                    </ItemTemplate>
                                </x:TemplateField>
                                <%--<x:LinkButtonField HeaderText="&nbsp;"  EnableAjax="false" Width="80px" CommandName="ActionDown" Text="下载" />--%>
                                  <x:TemplateField Width="80px" HeaderText="保密级别">
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# ChangeSecrecyLevel(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "SecrecyLevel"))) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField> 
                                <x:TemplateField HeaderText="相关文档" Width="100px" ID="TemplateField1">
                                    <ItemTemplate>
                                        <a id="A1" href="javascript:<%# GetRecordUrlDown(Eval("ScienceReportID")) %>">下载</a>
                                    </ItemTemplate>
                                </x:TemplateField>
                            </Columns>
                        </x:Grid>

                    </Items>
                </x:Region>
            </Regions>
        </x:RegionPanel>
        <x:Window ID="Window_AddReport" Popup="false" EnableIFrame="true" runat="server" 
            EnableMaximize="false" EnableResize="false" Height="450px" Width="800px" Title="添加">
        </x:Window>
        <x:Window ID="Window_DownLoad" Popup="false" EnableIFrame="true" runat="server" 
            EnableMaximize="false" EnableResize="false" Height="250px" Width="350px">
        </x:Window>
    </form>
</body>
</html>
