<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search_AchievementCA.aspx.cs" Inherits="WebApplication1.Achievement.AchievementCA.Search_AchievementCA" %>

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

                <x:Grid ID="Grid_AchievementCA" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                    DataKeyNames="AchievementCAID"
                    AllowSorting="true" SortColumnIndex="0" SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" AutoPostBack="true" OnRowCommand="Grid_AchievementCA_RowCommand" OnPageIndexChange="Grid_AchievementCA_PageIndexChange">

                    <Toolbars>
                        <x:Toolbar ID="Toolbar_top" runat="server">
                            <Items>
                                <x:Label ID="AgencyID" Width="60px" runat="server" CssClass="marginr" ShowLabel="true" Text="查询条件"></x:Label>
                                <x:DropDownList ID="dChoose" Width="100px" ShowLabel="false" AutoPostBack="true" runat="server" OnSelectedIndexChanged="dChoose_SelectedIndexChanged">
                                    <x:ListItem Text="全部" Value="全部" />
                                    <x:ListItem Text="项目名称" Value="项目名称" />
                                    <x:ListItem Text="验收年份" Value="验收年份" />
                                    <x:ListItem Text="验收组织部门" Value="验收组织部门" />
                                    <x:ListItem Text="完成人" Value="完成人" />
                                    <x:ListItem Text="验收评语级别" Value="验收评语级别" />
                                    <x:ListItem Text="保密级别" Value="保密级别" />
                                    <x:ListItem Text="成员" Value="成员" />
                                </x:DropDownList>
                                <x:Label ID="Label1" runat="server" Label="Label" Text=" " Width="5px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:TextBox ID="tCondition" ShowLabel="true" MaxLength="40" MaxLengthMessage="最多可输入40个字符" Enabled="false" EmptyText="请输入搜索条件" Width="100px" CssClass="marginr" runat="server" AutoPostBack="true">
                                </x:TextBox>
                                <x:Label ID="Label3" runat="server" Label="Label" Text=" " Width="5px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:DropDownList ID="dCondition" Width="100px" Enabled="false" EnableEdit="true" ShowLabel="false" AutoPostBack="true" runat="server">
                                </x:DropDownList>
                                <x:Button ID="Select" Text="搜索" Icon="SystemSearch" Type="Submit" runat="server" OnClick="Select_Click">
                                </x:Button>
                                <x:Button ID="btnRefresh" runat="server" Icon="ArrowRotateClockwise" Text="刷新" OnClick="btnRefresh_Click">
                                </x:Button>
                                <x:Button ID="btn_AddAchievementCA" Text="新增项目验收信息" Icon="Add" EnablePostBack="true" runat="server">
                                </x:Button>
                                <%--    <x:Button ID="btnDelete" Text="删除选中信息" Icon="Delete" EnablePostBack="true" runat="server" OnClick="btnDelete_Click">
                                    </x:Button>--%>
                                <%--  <x:Button ID="Get2" Text="导出所选信息" Icon="Disk" EnablePostBack="true" runat="server">
                                    </x:Button>--%>
                                <x:Button ID="btn_UpdateAchievementCA" Text="编辑选中行" Icon="BulletEdit" EnablePostBack="true" runat="server" OnClick="btn_UpdateAchievementCA_Click">
                                </x:Button>
                                <x:Button ID="btn_Get" Text="导出Excel文件" ConfirmText="确定导出？" Icon="Disk" EnablePostBack="true" runat="server" OnClick="btn_Get_Click" EnableAjax="false" DisableControlBeforePostBack="false">
                                </x:Button>
                                <%--  <x:Button ID="reprot" Text="报表"  Icon="Report" EnablePostBack="true" runat="server" >
                                         <Menu ID="Menu1" runat="server">
                                    <x:MenuButton ID="reprot1" runat="server" Text="分部门按验收部门、验收时间、验收评语级别统计项目验收情况">
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
                                <asp:Label ID="Label4" runat="server" Text='<%#RowNumber( Container.DataItemIndex + 1) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:CheckBoxField ID="CBoxSelect" CommandName="CBSelect" DataField="insepctid" runat="server" AutoPostBack="true" Enabled="true" RenderAsStaticField="false" Width="30" />
                        <x:BoundField Enabled="true" DataField="EntryPerson" SortField="EntryPerson" Hidden="true" />
                        <x:TemplateField Width="30px" Hidden="true">
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%#RowNumber( Container.DataItemIndex + 1) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:TemplateField Width="80px" HeaderText="项目名称">
                            <ItemTemplate>
                                <asp:Label ID="LabeAgency" runat="server" Text='<%# FindName(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "AchievementID"))) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>

                        <x:BoundField DataField="CAUnit" SortField="CAUnit" Width="150px" HeaderText="验收部门" />

                        <x:BoundField DataField="CATime" SortField="CATime" Width="150px" HeaderText="验收时间" DataFormatString="{0:yyyy-MM-dd}" />
                        <x:BoundField DataField="CACommnetLevel" SortField="CACommnetLevel" Width="150px" HeaderText="验收评语级别" />
                        <x:TemplateField Width="80px" HeaderText="保密级别">

                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# ChangeSecrecyLevel(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "SecrecyLevel"))) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                       <x:TemplateField HeaderText="成员" Width="80px">
                            <ItemTemplate>
                                <a href="javascript:<%# GetEditUrlm(Eval("AchievementCAID")) %>">详情</a>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:TemplateField HeaderText="相关文档" Width="60px">
                            <ItemTemplate>
                                <a href="javascript:<%# GetEditUrl(Eval("AchievementCAID")) %>">操作</a>
                            </ItemTemplate>
                        </x:TemplateField>
                    </Columns>
                </x:Grid>
            </Items>
        </x:Panel>
        <x:Window ID="Window_addAchievementCA" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableMinimize="false" EnableResize="false" Height="400px" Width="450px" Title="添加">
        </x:Window>

        <x:Window ID="Window_UpdateAchievementCA" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableMinimize="false" EnableResize="false" Height="400px" Width="450px" Title="修改">
        </x:Window>
        <x:Window ID="DownLoad" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableMinimize="false" EnableResize="false" Height="250px" Width="350px" Title="相关文件操作">
        </x:Window>
        <x:Window ID="WindowReport" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="true" EnableResize="false" Height="450px" Width="800px">
        </x:Window>
        <x:Window ID="Member" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="true" EnableResize="false" Height="250px" Width="350px">
        </x:Window>
        <x:Label ID="labResult" Visible="false" runat="server">
        </x:Label>
    </form>
</body>
</html>
