<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search_AchievementInfoes.aspx.cs" Inherits="WDFramework.Achievement.AchievementInfo.Search_AchievementInfoes" %>

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

                <x:Grid ID="Grid_Achievementt" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                    DataKeyNames="AchievementID"
                    AllowSorting="true" SortColumnIndex="0" SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" OnRowCommand="Grid_Achievement_RowCommand" OnPageIndexChange="Grid_Achievement_PageIndexChange">
                    <%--OnPageIndexChange="Grid_Info_PageIndexChange"--%>
                    <Toolbars>
                        <x:Toolbar ID="Toolbar_top" runat="server">
                            <Items>
                                <x:Label ID="condition" Width="60px" runat="server" CssClass="marginr" ShowLabel="true" Text="查询条件"></x:Label>
                                <x:DropDownList ID="dChoose" Width="100px" ShowLabel="false" AutoPostBack="true" runat="server" OnSelectedIndexChanged="dChoose_SelectedIndexChanged">
                                    <x:ListItem Text="全部" Value="全部" />
                                    <x:ListItem Text="成果名称" Value="成果名称" />
                                    <x:ListItem Text="项目名称" Value="项目名称" />
                                    <x:ListItem Text="完成人" Value="完成人" />
                                    <x:ListItem Text="成员" Value="成员" />
                                    <x:ListItem Text="鉴定年份" Value="鉴定年份" />
                                    <x:ListItem Text="鉴定组织部门" Value="鉴定组织部门" />
                                    <x:ListItem Text="成果登记号" Value="成果登记号" />
                                    <x:ListItem Text="所属机构" Value="所属机构" />
                                    <%--<x:ListItem Text="鉴定级别" Value="鉴定级别"/>--%>
                                    <x:ListItem Text="保密级别" Value="保密级别" />
                                </x:DropDownList>
                                <x:Label ID="Label3" runat="server" Label="Label" Text=" " Width="5px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:TextBox ID="tCondition" ShowLabel="true" Enabled="false" MaxLength="60" MaxLengthMessage="最多可输入60个字符" EmptyText="请输入搜索条件" Width="100px" CssClass="marginr" runat="server" AutoPostBack="true">
                                </x:TextBox>
                                <x:Label ID="Label4" runat="server" Label="Label" Text=" " Width="5px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:DropDownList ID="dCondition" Enabled="false" Width="100px" EnableEdit="true" ShowLabel="false" AutoPostBack="true" runat="server">
                                </x:DropDownList>
                                <x:Button ID="Select" Text="搜索" Icon="SystemSearch" Type="Submit" runat="server" OnClick="Select_Click">
                                </x:Button>
                                <x:Button ID="btnRefresh" runat="server" Icon="ArrowRotateClockwise" Text="刷新" OnClick="btnRefresh_Click">
                                </x:Button>
                                <x:Button ID="btn_AddAchievement" Text="新增成果鉴定信息" Icon="Add" EnablePostBack="true" runat="server">
                                </x:Button>
                                <%-- <x:Button ID="btnDelete" Text="删除选中信息" Icon="Delete" EnablePostBack="true" runat="server" OnClick="btnDelete_Click">
                                    </x:Button>--%>
                                <%-- <x:Button ID="Get2" Text="导出所选信息" Icon="Disk" EnablePostBack="true" runat="server">
                                    </x:Button>--%>
                                <x:Button ID="btn_UpdateAchievement" Text="编辑选中行" Icon="BulletEdit" EnablePostBack="true" runat="server" OnClick="btn_UpdateAchievement_Click">
                                </x:Button>
                                <x:Button ID="btn_Get" Text="导出Excel文件" ConfirmText="确定导出？" Icon="Disk" EnablePostBack="true" runat="server" OnClick="btn_Get_Click" EnableAjax="false" DisableControlBeforePostBack="false">
                                </x:Button>
                                <%-- <x:Button ID="reprot" Text="报表"  Icon="Report" EnablePostBack="true" runat="server" >
                                         <Menu ID="Menu1" runat="server">
                                    <x:MenuButton ID="reprot1" runat="server" Text="分部门按项目统计项目成果情况">
                                    </x:MenuButton>
                                              <x:MenuButton ID="reprot2" runat="server" Text="分部门按鉴定组织部门、鉴定评语级别、鉴定时间统计鉴定情况">
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
                                <asp:Label ID="Label6" runat="server" Text='<%#RowNumber( Container.DataItemIndex + 1) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:CheckBoxField ID="CBoxSelect" CommandName="CBSelect" DataField="insepctid" runat="server" AutoPostBack="true" Enabled="true" RenderAsStaticField="false" Width="30" />
                        <x:BoundField Enabled="true" DataField="EntryPerson" SortField="EntryPerson" Hidden="true" />
                        <x:TemplateField Width="30px" Hidden="true">
                            <ItemTemplate>
                                <asp:Label ID="Label7" runat="server" Text='<%#RowNumber( Container.DataItemIndex + 1) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:BoundField DataField="AchievementName" SortField="AchievementName" Width="150px" HeaderText="成果名称" />
                        <%--  <x:BoundField DataField="AchievementRank" SortField="AchievementRank" Width="150px" HeaderText="级别" />
                            <x:BoundField DataField="AchievementTime" SortField="AchievementTime" Width="150px" HeaderText="取得时间" DataFormatString="{0:yyyy-MM-dd}" />--%>
                        <x:TemplateField Width="80px" HeaderText="所属机构">
                            <ItemTemplate>
                                <asp:Label ID="LabeAgency" runat="server" Text='<%# FindAgencyName(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "AgencyID"))) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:BoundField DataField="ProjectName" SortField="ProjectName" Width="150px" HeaderText="所属项目" />
                        <x:BoundField DataField="ProjectInNum" SortField="ProjectInNum" Width="150px" HeaderText="项目内部编号" />
                        <%--<x:BoundField DataField="ProjectRank" SortField="ProjectRank" Width="150px" HeaderText="鉴定级别" />--%>
                        <x:BoundField DataField="ProjectForm" SortField="ProjectForm" Width="150px" HeaderText="鉴定形式" />
                        <x:BoundField DataField="ProjectLevel" SortField="ProjectLevel" Width="150px" HeaderText="鉴定水平" />
                        <%--<x:BoundField DataField="ProjectSource" SortField="ProjectSource" Width="150px" HeaderText="所属项目来源" />--%>
                        <x:BoundField DataField="AppraisalTime" SortField="AppraisalTime" Width="150px" HeaderText="鉴定时间" DataFormatString="{0:yyyy-MM-dd}" />
                        <x:BoundField DataField="AppraisalUnit" SortField="AppraisalUnit" Width="150px" HeaderText="鉴定组织部门" />
                        <x:BoundField DataField="ApRemarkRank" SortField="ApRemarkRank" Width="150px" HeaderText="成果登记号" />
                        <x:BoundField DataField="ApprovalNum" SortField="ApprovalNum" Width="150px" HeaderText="证书文号" />
                        <x:BoundField DataField="FirstFinishedPeople" SortField="FirstFinishedPeople" Width="150px" HeaderText="成果第一完成人" />

                        <x:TemplateField Width="80px" HeaderText="保密级别">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# ChangeSecrecyLevel(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "SecrecyLevel"))) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:TemplateField Width="80px" HeaderText="完成人" Hidden="true">
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# getpeople(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "AchievementID"))) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:TemplateField HeaderText="完成人" Width="60px">
                            <ItemTemplate>
                                <a href="javascript:<%# GetEditUrl(Eval("AchievementID")) %>">详情</a>
                            </ItemTemplate>
                        </x:TemplateField>                    
                        <x:TemplateField HeaderText="成员" Width="60px">
                            <ItemTemplate>
                                <a href="javascript:<%# GetEditUrlm(Eval("AchievementID")) %>">详情</a>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:TemplateField HeaderText="相关文档" Width="60px">
                            <ItemTemplate>
                                <a href="javascript:<%# GetEditUrlu(Eval("AchievementID")) %>">操作</a>
                            </ItemTemplate>
                        </x:TemplateField>
                    </Columns>
                </x:Grid>
            </Items>
        </x:Panel>
        <x:Window ID="Window_addAchievement" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableMinimize="false" EnableResize="false" Height="430px" Width="800px" Title="添加">
        </x:Window>

        <x:Window ID="Window_UpdateAchievement" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableMinimize="false" EnableResize="false" Height="430px" Width="800px" Title="修改">
        </x:Window>
        <x:Window ID="Peoplef" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableMinimize="false" EnableResize="false" Height="250px" Width="350px" Title="完成人">
        </x:Window>
        <x:Window ID="DownLoad" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableMinimize="false" EnableResize="false" Height="350px" Width="320px" Title="相关文件操作">
        </x:Window>
        <x:Window ID="WindowReport" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="true" EnableResize="false" Height="450px" Width="800px">
        </x:Window>
        <x:Label ID="labResult" Visible="false" runat="server">
        </x:Label>
    </form>
</body>
</html>
