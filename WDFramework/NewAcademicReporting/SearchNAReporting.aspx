<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchNAReporting.aspx.cs" Inherits="WDFramework.NewAcademicReporting.SearchNAReporting" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body oncontextmenu='return false'>
    <%--取消鼠标右键的点击--%>
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <x:Panel ID="Panel1" runat="server" BodyPadding="5px" EnableBackgroundColor="true"
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start"
            ShowHeader="false" Title="用户管理">
            <Items>

                <x:Grid ID="Grid_NAReporting" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                    EnableRowNumberPaging="true" EnableTextSelection="true" EnableCheckBoxSelect="false"
                    DataKeyNames="NewAcademicReportingID" AllowSorting="true" SortColumnIndex="0"
                    AllowPaging="true" IsDatabasePaging="true" OnPageIndexChange="Grid_NAReporting_PageIndexChange" OnRowCommand="Grid_NAReporting_RowCommand">
                    
                    <Toolbars>
                        <x:Toolbar ID="Toolbar_top" runat="server">
                            <Items>
                                <x:Label ID="labSort" runat="server" CssClass="marginr" ShowLabel="false" Text="查询条件   ">
                                </x:Label>
                                
                                <x:DropDownList ID="ddl_search" runat="server" ShowLabel="false" AutoPostBack="true" Width ="100px">
                                     <%--OnSelectedIndexChanged="ddl_search_SelectedIndexChanged" --%>
                                    <x:ListItem Text="全部" Value="0" Selected="true" />
                                    <x:ListItem Text="报告名称" Value="1" />
                                    <x:ListItem Text="报告时间" Value="2" />
                                    <x:ListItem Text="报告人" Value="3" />
                                </x:DropDownList> 
                                <x:TextBox ID="txtReportName" Enabled="true" ShowLabel="true" MaxLength="60" MaxLengthMessage="最多可输入40个字符"   EmptyText="请输入搜索条件" Width="100px" CssClass="marginr" runat="server"  AutoPostBack="false"  >
                                        </x:TextBox>
                                <x:Button ID="btnCheck" runat="server" EnablePostBack="true" Icon="SystemSearch" Text="搜索" OnClick="btnCheck_Click1" Type="Submit">
                                </x:Button>
                                <x:Button ID="btnRefresh" runat="server" Icon="ArrowRotateClockwise" Text="刷新" OnClick="btnRefresh_Click">
                                </x:Button>
                                <x:Button ID="btnAddLecture" Text="新增学术报告" Icon="Add" EnablePostBack="true" runat="server">
                                </x:Button>
                                <x:Button ID="btnUpdate" Text="编辑选中行" Icon="Pencil" runat="server" OnClick="btnUpdate_Click">
                                </x:Button>


                                <x:Button ID="btnSelect_All" runat="server" Text="全选" OnClick="btnSelect_All_Click"></x:Button>
                                <x:Button ID="btnDelete" Text="删除选中信息" Icon="Delete" EnablePostBack="true" runat="server" ConfirmText="确定删除？" ConfirmTarget="Top" OnClick="btnDelete_Click" Enabled="false">
                                </x:Button>
                                <%--<x:Button ID="Get2" Text="导出所选信息" Icon="Disk" EnablePostBack="true" runat="server">
                                    </x:Button>--%>
                                <%--<x:Button ID="btnDown" Text="下载选中行文件" runat="server" >
                                    </x:Button>--%>
                            </Items>
                        </x:Toolbar>
                    </Toolbars>

                    <PageItems>
                        <x:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                        </x:ToolbarSeparator>
                        <x:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
                        </x:ToolbarText>
                        <x:DropDownList ID="ddlGridPageSize" Width="80px" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlGridPageSize_SelectedIndexChanged">
                            <x:ListItem Text="10" Value="10" />
                            <x:ListItem Text="20" Value="20" Selected="true" />
                            <x:ListItem Text="30" Value="30" />
                            <x:ListItem Text="50" Value="50" />
                        </x:DropDownList>
                    </PageItems>

                    <Columns>
                        <x:TemplateField Width="30px">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%#RowNumber( Container.DataItemIndex + 1) %>'></asp:Label> 
                    </ItemTemplate>
                </x:TemplateField>
                        <x:CheckBoxField ID="CBoxSelect" CommandName="CBSelect" DataField="fileid" runat="server" AutoPostBack="true" Enabled="true" RenderAsStaticField="false" Width="30" />
                         
                        <x:BoundField Enabled="true" DataField="EntryPerson" SortField="EntryPerson" Hidden="true"/>

                        <x:BoundField DataField="ReportName" SortField="ReportName" Width="150px" HeaderText="学术报告名称" />
                        <x:BoundField DataField="ReportTime" SortField="ReportTime" Width="150px" DataFormatString="{0:yyyy-MM-dd}" HeaderText="报告时间" />
                        <x:BoundField DataField="ReportPlace" SortField="ReportPlace" Width="150px" HeaderText="报告地点" />
                        <x:BoundField DataField="ReportType" SortField="ReportType" Width="150px" HeaderText="报告类别" />

                        <x:BoundField DataField="ReportPeople" SortField="ReportPeople" Width="100px" HeaderText="报告人" />
                        <x:BoundField DataField="JobName" SortField="JobName" Width="150px" HeaderText="职称" />
                        <x:BoundField DataField="JobMission" SortField="JobMission" Width="150px" HeaderText="职务" />
                        <x:BoundField DataField="ReportUnit" SortField="ReportUnit" Width="150px" HeaderText="报告人单位" />
                        <x:BoundField DataField="Report" SortField="Report" Width="150px" HeaderText="报告人身份证" />
                        <x:BoundField DataField="ReportTele" SortField="ReportTele" Width="150px" HeaderText="报告人手机号" />
                        <x:BoundField DataField="AcademicTitle" SortField="AcademicTitle" Width="150px" HeaderText="学术兼职及荣誉称号" />

                        <x:BoundField DataField="ApplyFund" SortField="ApplyFund" Width="150px" HeaderText="申请经费" />
                        <x:BoundField DataField="PeopleCount" SortField="PeopleCount" Width="150px" HeaderText="参与人数" />
                        <%--<x:BoundField DataField="SecrecyLevel" SortField="SecrecyLevel" Width="150px" HeaderText="保密级别" />--%>
                        <x:BoundField DataField="MajorPeople" SortField="MajorPeople" Width="150px" HeaderText="主要参与人" />
                        <x:BoundField DataField="Organizers" SortField="Organizers" Width="150px" HeaderText="主办单位" />
                        <x:BoundField DataField="Coorganizer" SortField="Coorganizer" Width="150px" HeaderText="协办单位" />
                        <%--<x:BoundField DataField="Remark" SortField="Remark" Width="150px" HeaderText="备注" />--%>
                        <%--<x:LinkButtonField HeaderText="操作" CommandName="Action1"  Text="修改" Width="80px"/>--%>
                        <%--<x:LinkButtonField Width="80px" Text="下载" HeaderText="相关文档" EnableAjax="false" CommandName="ActiveDown" />--%>
                        <x:TemplateField Width="80px" HeaderText="保密级别">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# ChangeSecrecyLevel(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "SecrecyLevel"))) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:TemplateField HeaderText="备注" Width="60px" >
                           <ItemTemplate>
                        <a href="javascript:<%# GetEditUrl(Eval("NewAcademicReportingID ")) %>">详情</a>
                          </ItemTemplate>
                          </x:TemplateField>
                          <x:TemplateField HeaderText="相关文档" Width="100px"  ID="TemplateField1">
                           <ItemTemplate>
                        <a id="A1" href="javascript:<%# GetRecordUrlDown(Eval("NewAcademicReportingID")) %>" >下载</a>
                          </ItemTemplate>
                          </x:TemplateField>
                    </Columns>
                </x:Grid>
            </Items>
        </x:Panel>
        <x:Window ID="Window_addLecture" Popup="false" EnableIFrame="true" runat="server" 
            EnableMaximize="false" EnableResize="false" Height="450px" Width="800px" Title="添加">
        </x:Window>

        <x:Window ID="Window_Update" Popup="false" EnableIFrame="true" runat="server" 
            EnableMaximize="false" EnableResize="false" Height="450px" Width="850px" Title="添加">
        </x:Window>
          <x:Window ID="Window_NoLibraryMessage" Popup="false" EnableIFrame="true"  runat="server"
            EnableMaximize="false" EnableResize="false" Height="250px" Width="350px" >
        </x:Window>
         <x:Window ID="Window_DownLoad" Popup="false" EnableIFrame="true"  runat="server" 
            EnableMaximize="false" EnableResize="false" Height="250px" Width="350px" >
        </x:Window>
         <x:Window ID="Window_Remark" Popup="false" EnableIFrame="true"  runat="server" 
            EnableMaximize="false" EnableResize="false" Height="250px" Width="350px" >
        </x:Window>
        <x:Label ID="labResult" Visible="false" runat="server">
        </x:Label>
    </form>
</body>
</html>
