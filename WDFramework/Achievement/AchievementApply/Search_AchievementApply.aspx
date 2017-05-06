<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search_AchievementApply.aspx.cs" Inherits="WDFramework.Acheievement.AchievementApply.Search_AchievementApply" %>

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
            ShowHeader="false" Title="用户管理">
            <Items>
                <%--  --%>
                <%--  --%>

                <x:Grid ID="Grid_AchieveApply" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                    DataKeyNames="AchivementApplyID"
                    AllowSorting="true" SortColumnIndex="0" SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" AutoPostBack="true" OnRowCommand="Grid_AchieveApply_RowCommand" OnPageIndexChange="Grid_AchieveApply_PageIndexChange">
                    <%--OnPageIndexChange="Grid_Info_PageIndexChange"--%>
                    <Toolbars>
                        <x:Toolbar ID="Toolbar_top" runat="server">
                            <Items>
                                <x:Label ID="condition" Width="60px" runat="server" CssClass="marginr" ShowLabel="true" Text="查询条件"></x:Label>
                                <x:DropDownList ID="dChoose" Width="100px" ShowLabel="false" AutoPostBack="true" runat="server" OnSelectedIndexChanged="dChoose_SelectedIndexChanged">
                                    <x:ListItem Text="全部" Value="全部" />
                                    <x:ListItem Text="成果名称" Value="成果名称" />
                                    <x:ListItem Text="应用单位" Value="应用单位" />
                                    <x:ListItem Text="完成人" Value="完成人" />
                                    <x:ListItem Text="开始年份" Value="开始年份" />
                                    <x:ListItem Text="保密级别" Value="保密级别" />
                                    <x:ListItem Text="成员" Value="成员" />
                                </x:DropDownList>
                                <x:Label ID="Label1" runat="server" Label="Label" Text=" " Width="5px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:TextBox ID="tCondition" ShowLabel="true" MaxLength="60" MaxLengthMessage="最多可输入60个字符" EmptyText="请输入搜索条件" Width="100px" CssClass="marginr" runat="server" AutoPostBack="true" Enabled="false">
                                </x:TextBox>
                                <x:Label ID="Label3" runat="server" Label="Label" Text=" " Width="5px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:DropDownList ID="dCondition" Enabled="false" Width="100px" EnableEdit="true" ShowLabel="false" AutoPostBack="true" runat="server">
                                </x:DropDownList>
                                <x:Button ID="Select" Text="搜索" Icon="SystemSearch" Type="Submit" runat="server" OnClick="Select_Click">
                                </x:Button>
                                <x:Button ID="btnRefresh" runat="server" Icon="ArrowRotateClockwise" Text="刷新" OnClick="btnRefresh_Click">
                                </x:Button>
                                <x:Button ID="btnAddAchieveAward" Text="新增成果应用信息" Icon="Add" EnablePostBack="true" runat="server">
                                </x:Button>
                                  <x:Button ID="btnSelect_All" runat="server" Text="全选" OnClick="btnSelect_All_Click"></x:Button>
                                 <x:Button ID="btnDelete" Text="删除获奖信息" Icon="Delete" EnablePostBack="true" runat="server" OnClick="btnDelete_Click"
                                    ConfirmText="确定删除？" ConfirmTarget="Top" Enabled="false">
                                </x:Button>
                                <%--<x:Button ID="btnDelete" Text="删除选中信息" Icon="Delete" EnablePostBack="true" runat="server" OnClick="btnDelete_Click" >
                                    </x:Button>--%>
                                <%--  <x:Button ID="Get2" Text="导出所选信息" Icon="Disk" EnablePostBack="true" runat="server">
                                    </x:Button>--%>
                                <x:Button ID="btnUpdateAchieveApply" Text="编辑选中行" Icon="BulletEdit" EnablePostBack="true" runat="server" OnClick="btnUpdateAchieveApply_Click">
                                </x:Button>
                                <x:Button ID="btn_Get" Text="导出Excel文件" ConfirmText="确定导出？" Icon="Disk" EnablePostBack="true" runat="server" OnClick="btn_Get_Click" EnableAjax="false" DisableControlBeforePostBack="false">
                                </x:Button>
                                <%-- <x:Button ID="reprot" Text="报表"  Icon="Report" EnablePostBack="true" runat="server" >
                                         <Menu ID="Menu1" runat="server">
                                    <x:MenuButton ID="reprot1" runat="server" Text="分部门按人员统计成果报奖情况" >
                                    </x:MenuButton>                           
                                </Menu>
                                    </x:Button>--%>
                            </Items>
                        </x:Toolbar>
                    </Toolbars>
                    <PageItems>
                        <x:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                        </x:ToolbarSeparator>
                        <x:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
                        </x:ToolbarText>
                        <x:DropDownList ID="ddlGridPageSize" Width="80px" AutoPostBack="true" runat="server" TabIndex="3" OnSelectedIndexChanged="ddlGridPageSize_SelectedIndexChanged">
                            <x:ListItem Text="10" Value="10" />
                            <x:ListItem Text="20" Value="20" Selected="true" />
                            <x:ListItem Text="30" Value="30" />
                            <x:ListItem Text="50" Value="50" />
                        </x:DropDownList>
                    </PageItems>

                    <Columns>
                        <x:TemplateField Width="30px">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:CheckBoxField ID="CBoxSelect" CommandName="CBSelect" DataField="insepctid" runat="server" AutoPostBack="true" Enabled="true" RenderAsStaticField="false" Width="30" />
                        <x:BoundField Enabled="true" DataField="EntryPerson" SortField="EntryPerson" Hidden="true" />
                        <x:TemplateField Width="30px" Hidden="true">
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:TemplateField Width="80px" HeaderText="成果名称">
                            <ItemTemplate>
                                <asp:Label ID="LabeAgency" runat="server" Text='<%# FindName(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "AchievementID"))) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:BoundField DataField="ApplyUnit" SortField="ApplyUnit" Width="100px" HeaderText="应用单位" />
                        <x:BoundField DataField="StartTime" SortField="StartTime" Width="150px" DataFormatString="{0:yyyy-MM-dd}" HeaderText="开始时间" />
                        <x:BoundField DataField="EndTime" SortField="EndTime" Width="150px" DataFormatString="{0:yyyy-MM-dd}" HeaderText="结束时间" />
                        <x:BoundField DataField="Use" SortField="Use" Width="150px" HeaderText="用途" />
                        <x:BoundField DataField="EconomicBenefit" SortField="EconomicBenefit" Width="150px" HeaderText="经济效益" />
                        <x:TemplateField HeaderText="成员" Width="60px">
                            <ItemTemplate>
                                <a href="javascript:<%# GetEditUrlm(Eval("AchivementApplyID")) %>">详情</a>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:TemplateField HeaderText="保密级别" Width="60px">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# ChangeSecrecyLevel(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "SecrecyLevel"))) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:TemplateField HeaderText="相关文档" Width="60px">
                            <ItemTemplate>
                                <a href="javascript:<%# GetEditUrl(Eval("AchivementApplyID")) %>">操作</a>
                            </ItemTemplate>
                        </x:TemplateField>
                    </Columns>
                </x:Grid>
            </Items>
        </x:Panel>
        <x:Window ID="Peoplef" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableMinimize="false" EnableResize="false" Height="250px" Width="350px" Title="查询详情">
        </x:Window>
        <x:Window ID="Window_addAchieveApply" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableMinimize="false" EnableResize="false" Height="400px" Width="450px" Title="添加成果应用信息">
        </x:Window>

        <x:Window ID="Window_ReviseAchieveApply" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableMinimize="false" EnableResize="false" Height="400px" Width="450px" Title="更新成果应用信息">
        </x:Window>
        <x:Window ID="DownLoad" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableMinimize="false" EnableResize="false" Height="250px" Width="350px" Title="相关文件操作">
        </x:Window>
        <x:Label ID="labResult" Visible="false" runat="server">
        </x:Label>
    </form>
</body>
</html>
