<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search_WorkPlanSummary.aspx.cs" Inherits="WebApplication1.Search_WorkPlanSummary" %>

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
                <x:Grid ID="Grid_WorkPlanSummary" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                     EnableRowNumberPaging="true" EnableTextSelection="true" EnableCheckBoxSelect="false"
                    DataKeyNames="WorkPlanSummaryID" AllowSorting="true" SortColumnIndex="0"
                    AllowPaging="true" IsDatabasePaging="true" OnPageIndexChange="Grid_WorkPlanSummary_PageIndexChange" OnRowCommand="Grid_WorkPlanSummary_RowCommand">
                    <Toolbars>
                        <x:Toolbar ID="Toolbar_top" runat="server">
                            <Items>
                                <x:Label ID="DocumentSortID" Width="30px" runat="server" CssClass="marginr" ShowLabel="true" Text="分类"></x:Label>
                                <x:DropDownList ID="DropDownListSort" ShowLabel="false" AutoPostBack="true" runat="server" EnableEdit="false" OnSelectedIndexChanged="DropDownListSort_SelectedIndexChanged">
                                    <x:ListItem Text="全部" Value="0" Selected="true"/>
                                    <%--<x:ListItem Text="个人/部门" Value="1" />--%>
                                    <x:ListItem Text="机构" Value="2" />
                                    <x:ListItem Text="人员" Value="3" />
                                    <x:ListItem Text="年份" Value="4" />
                                </x:DropDownList>
                                <x:DropDownList ID="DropDownList" ShowLabel="false" AutoPostBack="true" runat="server" EnableEdit="true" ForceSelection="true">
                                     <x:ListItem Text="全部" Value="0" Selected="true"/>
                                </x:DropDownList>
                              <%--  <x:TextBox ID="txtSort" ShowLabel="true" Required="true" Width="200px" EmptyText="请选择分类"
                                    CssClass="marginr" runat="server">
                                </x:TextBox>--%>
                                <%--<x:DropDownList ID="DropDownList" ShowLabel="false" AutoPostBack="true" runat="server" EnableEdit="false">
                                    <x:ListItem Text="请选择" Value="0" />
                                    
                                </x:DropDownList>--%>
                                <%--  <x:Label ID="AgencyID" Width="60px" runat="server" CssClass="marginr" ShowLabel="true" Text=""></x:Label>--%>
                                <%--  <x:DropDownList ID="DropDownListAgency" ShowLabel="false" AutoPostBack="true" runat="server" EnableEdit="false">
                                   <x:ListItem Text ="请选择" Value="0" />
                                    </x:DropDownList>--%>
                                <%--<x:TextBox ID="tUser" ShowLabel="true" Required="true" EmptyText="请输入部门名称或人名" Width="150px" CssClass="marginr" runat="server" AutoPostBack="true">
                                </x:TextBox>--%>
                                <x:Button ID="btnCheck" Text="搜索" Icon="SystemSearch" runat="server" OnClick="btnCheck_Click" Type="Submit">
                                </x:Button>
                                <x:Button ID="btnRefresh" runat="server" Icon="ArrowRotateClockwise" Text="刷新" OnClick="btnRefresh_Click">
                                </x:Button>
                                <x:Button ID="btnAddPlan" runat="server" EnablePostBack="false" Icon="Add" Text="新增信息">
                                </x:Button>
                                <x:Button ID="btnUpdate" Text="编辑" Icon="Pencil" runat="server" OnClick="btnUpdate_Click"></x:Button>
                                <x:Button ID="btnSelect_All" runat="server" Text="全选" OnClick="btnSelect_All_Click"></x:Button>
                                <x:Button ID="btnDelete" Text="删除选中信息" Icon="Delete" runat="server" OnClick="btnDelete_Click" ConfirmText="确定删除？"
                                    ConfirmTarget="Top" Enabled="false">
                                </x:Button>
                                <%--<x:Button ID="ButtonUpdate" Text="编辑选中行"  Icon="Pencil"   runat="server"   >
                                  </x:Button>--%>
                            </Items>
                        </x:Toolbar>
                    </Toolbars>

                    <%--AllowPaging这是分页功能--%>
                    <PageItems>
                        <x:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                        </x:ToolbarSeparator>
                        <x:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
                        </x:ToolbarText>
                        <x:DropDownList ID="ddlGridPageSize" Width="80px" AutoPostBack="true" OnSelectedIndexChanged="ddlGridPageSize_SelectedIndexChanged"
                            runat="server">
                            <x:ListItem Text="10" Value="10" />
                            <x:ListItem Text="20" Value="20" Selected="true" />
                            <x:ListItem Text="30" Value="30" />
                            <x:ListItem Text="50" Value="50" />
                        </x:DropDownList>
                    </PageItems>
                    <%--DataKeyNames这是数据库唯一标识--%>
                    <Columns>
                         <x:TemplateField Width="30px">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%#RowNumber( Container.DataItemIndex + 1) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>
                        <x:CheckBoxField ID="BoxSelect" CommandName="CBSelect" DataField="fileid" runat="server" AutoPostBack="true" Enabled="true" RenderAsStaticField="false" Width="30" />
                        
                        <x:BoundField Enabled="true" DataField="EntryPerson" SortField="EntryPerson" Hidden="true" />
                        <x:BoundField DataField="PlanWork" SortField="PlanWork" Width="200px" HeaderText="工作计划与总结名称" />
                        <x:TemplateField Width="200px" HeaderText="个人名/部门名">
                            <ItemTemplate>
                                <asp:Label ID="labSort" runat="server" Text='<%# Sort(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "WorkPlanSummaryID"))) %>'></asp:Label>
                                
                            </ItemTemplate>
                        </x:TemplateField>
                        <%--<x:BoundField DataField="Sort" SortField="Sort" Width="200px" HeaderText="个人/部门" />--%>
                        <x:BoundField DataField="Time" SortField="Time" Width="200px" HeaderText="时间" DataFormatString="{0:yyyy-MM-dd}" />
                       <%-- <x:TemplateField Width="150px" HeaderText="时间">
                            <ItemTemplate>
                                <asp:Label ID="labTime" runat="server" Text='<%# Time(Convert.ToDateTime( DataBinder.Eval

(Container.DataItem, "Time"))) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>--%>
                        <%--<x:BoundField DataField="SecrecyLevel" SortField="SecrecyLevel" Width="150px" HeaderText="人员" />--%>
                        <%--<x:BoundField DataField="SecrecyLevel" SortField="SecrecyLevel" Width="150px" HeaderText="保密等级" />--%>
                        <%--<x:LinkButtonField Width="80px" Text="下载" HeaderText="相关文档" EnableAjax="false" />--%>
                         <x:TemplateField Width="80px" HeaderText="保密级别">
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# ChangeSecrecyLevel(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "SecrecyLevel"))) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>
                          <x:TemplateField HeaderText="相关文档" Width="100px"  ID="TemplateField1">
                           <ItemTemplate>
                        <a id="A1" href="javascript:<%# GetRecordUrlDown(Eval("WorkPlanSummaryID")) %>" >下载</a>
                          </ItemTemplate>
                          </x:TemplateField>
                    </Columns>
                </x:Grid>
            </Items>
        </x:Panel>
        <x:Window ID="Window_AddPlan" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableResize="false" Width="450px" Height="450px" Title="添加" >
        </x:Window>
        <x:Window ID="Window_Update" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableResize="false" Width="400px" Title="添加" Height="300px">
        </x:Window>
        <x:Window ID="Window_DownLoad" Popup="false" EnableIFrame="true"  runat="server"
            EnableMaximize="false" EnableResize="false" Height="250px" Width="350px" >
        </x:Window>
          <x:Window ID="Window_NoLibraryMessage" Popup="false" EnableIFrame="true"  runat="server"
            EnableMaximize="false" EnableResize="false" Height="250px" Width="350px" >
        </x:Window>
        <x:Window ID="WindowUpdate" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableResize="false" Height="450px" Width="450px" Title="编辑项目基本信息">
        </x:Window>
        <x:Label ID="labResult" Visible="false" runat="server">
        </x:Label>
    </form>
</body>
</html>
