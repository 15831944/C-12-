<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AchieveAward.aspx.cs" Inherits="WebApplication1.AchieveAward" %>

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
                <%--  --%>
                <%--  --%>

                <x:Grid ID="Grid_AchieveAward" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                    DataKeyNames="AchieveAwardID"
                    AllowSorting="true" SortColumnIndex="0" SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" AutoPostBack="true" OnRowCommand="Grid_AchieveAward_RowCommand" OnPageIndexChange="Grid_AchieveAward_PageIndexChange">
                    <%--OnPageIndexChange="Grid_Info_PageIndexChange"--%>
                    <Toolbars>
                        <x:Toolbar ID="Toolbar_top" runat="server">
                            <Items>
                                <x:Label ID="lYear" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="查询条件：">
                                </x:Label>
                                <x:DropDownList ID="ddl_search" ShowLabel="false" AutoPostBack="true" Width="100px" runat="server" TabIndex="1" OnSelectedIndexChanged="ddl_search_SelectedIndexChanged">
                                    <x:ListItem Text="全部" Value="0" Selected="true" />
                                    <x:ListItem Text="报奖名称" Value="1" />
                                    <x:ListItem Text="报奖单位" Value="2" />
                                    <x:ListItem Text="成员" Value="3" />
                                    <x:ListItem Text="保密级别" Value="4" />
                                </x:DropDownList>
                                <x:TextBox ID="tAchieveName" ShowLabel="false" Width="100px" CssClass="marginr" runat="server" MaxLength="20" MaxLengthMessage="最多可输入20个字符">
                                </x:TextBox>
                                <x:DropDownList ID="secrecyLevel" Width="100px" runat="server" >
                                    <x:ListItem Text="四级" Value="1" Selected="true" />
                                    <x:ListItem Text="三级" Value="2" />
                                    <x:ListItem Text="二级" Value="3" />
                                    <x:ListItem Text="一级" Value="4" />
                                    <x:ListItem Text="管理员" Value="5" />
                                </x:DropDownList>
                                <x:Button ID="Select" Text="搜索" Icon="SystemSearch" runat="server" Type="Submit" OnClick="Select_Click">
                                </x:Button>
                                <x:Button ID="btnRefresh" runat="server" Icon="ArrowRotateClockwise" Text="刷新" OnClick="btnRefresh_Click">
                                </x:Button>
                                <x:Button ID="btnAddAchieveAward" Text="新增成果报奖信息" Icon="Add" EnablePostBack="true" runat="server">
                                </x:Button>
                                <%--  <x:Button ID="btnDelete" Text="删除选中信息" Icon="Delete" EnablePostBack="true" runat="server" OnClick="btnDelete_Click">
                                    </x:Button>--%>
                                <%--  <x:Button ID="Get2" Text="导出所选信息" Icon="Disk" EnablePostBack="true" runat="server">
                                    </x:Button>--%>
                                <x:Button ID="btnReviseAchieveAward" Text="编辑选中行" Icon="BulletEdit" EnablePostBack="true" runat="server" OnClick="btnReviseAchieveAward_Click">
                                </x:Button>
                                <x:Button ID="btn_Get" Text="导出Excel文件" ConfirmText="确定导出？" Icon="Disk" EnablePostBack="true" runat="server" OnClick="btn_Get_Click" EnableAjax="false" DisableControlBeforePostBack="false">
                                </x:Button>
                                <%--  <x:Button ID="reprot" Text="报表"  Icon="Report" EnablePostBack="true" runat="server" >
                                         <Menu ID="Menu1" runat="server">
                                    <x:MenuButton ID="reprot1" runat="server" Text="分部门按人员统计成果报奖情况">
                                    </x:MenuButton>--%>

                                <%-- </Menu>
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
                                <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:CheckBoxField ID="CBoxSelect" CommandName="CBSelect" DataField="insepctid" runat="server" AutoPostBack="true" Enabled="true" RenderAsStaticField="false" Width="30" />
                        <x:BoundField Enabled="true" DataField="EntryPerson" SortField="EntryPerson" Hidden="true" />
                        <x:TemplateField Width="30px" Hidden="true">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:TemplateField Width="80px" HeaderText="成果名称">
                            <ItemTemplate>
                                <asp:Label ID="LabeAgency" runat="server" Text='<%# FindName(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "AchievementID"))) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:BoundField DataField="AwardName" SortField="AwardName" Width="150px" HeaderText="报奖名称" />
                        <x:BoundField DataField="AwardUnit" SortField="AwardUnit" Width="100px" HeaderText="报奖单位" />
                        <x:BoundField DataField="AwardType" SortField="AwardType" Width="150px" HeaderText="报奖类别" />
                        <x:BoundField DataField="AwardGrade" SortField="AwardGrade" Width="150px" HeaderText="报奖等级" />

                        <x:TemplateField Width="80px" HeaderText="保密级别">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# ChangeSecrecyLevel(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "SecrecyLevel"))) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>

                        <x:TemplateField HeaderText="报奖人" Hidden="true">
                            <ItemTemplate>
                                <asp:Label ID="Label6" runat="server" Text='<%# getAchieveAwardPeople(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "AchieveAwardID"))) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:TemplateField HeaderText="报奖人" Width="60px">
                            <ItemTemplate>
                                <a href="javascript:<%# GetEditUrl(Eval("AchieveAwardID")) %>">详情</a>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:TemplateField HeaderText="成员" Hidden="true">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# getMember(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "AchieveAwardID"))) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:TemplateField HeaderText="成员" Width="60px">
                            <ItemTemplate>
                                <a href="javascript:<%# GetEditUrlMember(Eval("AchieveAwardID")) %>">详情</a>
                            </ItemTemplate>
                        </x:TemplateField>
                    </Columns>
                </x:Grid>
            </Items>
        </x:Panel>
        <x:Window ID="Window_addAchieveAward" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableMinimize="false" EnableConfirmOnClose="true" CloseAction="HidePostBack" EnableResize="false" Height="400px" Width="450px" Title="添加">
        </x:Window>

        <x:Window ID="Window_ReviseAchieveAward" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableMinimize="false" EnableConfirmOnClose="true" EnableResize="false" Height="400px" Width="450px" Title="更新">
        </x:Window>
        <x:Window ID="AchieveAwardPeople" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableMinimize="false" EnableResize="false" Height="250px" Width="350px" Title="报奖人">
        </x:Window>
        <x:Window ID="AchieveAwardMember" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableMinimize="false" EnableResize="false" Height="250px" Width="350px" Title="成员">
        </x:Window>
        <x:Window ID="WindowReport" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="true" EnableResize="false" Height="450px" Width="800px">
        </x:Window>
        <x:Label ID="labResult" Visible="false" runat="server">
        </x:Label>
    </form>
</body>
</html>
