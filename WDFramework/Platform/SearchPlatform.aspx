<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchPlatform.aspx.cs" Inherits="WDFramework.Platform.SearchPlatform" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <%--取消鼠标右键的点击--%>
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <x:Panel ID="Panel1" runat="server" BodyPadding="5px" EnableBackgroundColor="true"
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start"
            ShowHeader="false" Title="用户管理">
            <Items>
                <%--  --%>
                <%--  --%>
                <%--  --%>
                <x:Grid ID="Grid_Platform" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                    EnableRowNumberPaging="true" EnableTextSelection="true" EnableCheckBoxSelect="false"
                    DataKeyNames="PlatformID" AllowSorting="true" SortColumnIndex="0" EnableRowNumber="false"
                    AllowPaging="true" IsDatabasePaging="true" OnPageIndexChange="Grid_Platform_PageIndexChange" OnRowCommand="Grid_Platform_RowCommand">
                    <%--OnPageIndexChange="Grid_Info_PageIndexChange"--%>
                    <Toolbars>
                        <x:Toolbar ID="Toolbar_top" runat="server">
                            <Items>
                                <x:Label ID="Label4" runat="server" Label="Label" Width="15" Text=" "></x:Label>
                                <x:Label ID="Label9" runat="server" Label="Label" Text="查询条件："></x:Label>
                                <x:Label ID="Label10" runat="server" Label="Label" Width="15" Text=" "></x:Label>
                                <x:DropDownList ID="ddlsearch" ShowLabel="false" AutoPostBack="true" Width="100px" runat="server" TabIndex="1" OnSelectedIndexChanged="ddlsearch_SelectedIndexChanged">
                                    <x:ListItem Text="全部" Value="0" Selected="true" />
                                    <x:ListItem Text="平台名称" Value="平台名称" />
                                    <x:ListItem Text="平台级别" Value="平台级别" />
                                </x:DropDownList>
                                <x:TwinTriggerBox runat="server" EmptyText="请输入搜索条件" ID="TriggerBox" ShowTrigger1="false" TabIndex="1"
                                    MaxLength="20" MaxLengthMessage="最多输入20个字符" ShowLabel="true" Label="平台名称或平台级别"
                                    ShowTrigger2="false">
                                </x:TwinTriggerBox>
                                <x:DropDownList ID="DropDownListPlatformType" ShowLabel="false" AutoPostBack="true" runat="server" TabIndex="3" Width="150px">
                                </x:DropDownList>
                                <x:Button ID="btnCheck" Text="搜索" Icon="SystemSearch" runat="server" OnClick="btnCheck_Click" Type="Submit">
                                </x:Button>
                                <x:Button ID="btnRefresh" runat="server" Icon="ArrowRotateClockwise" Text="刷新" OnClick="btnRefresh_Click">
                                </x:Button>
                                <x:Button ID="btn_AddPlatform" Text="新增平台信息" Icon="Add" EnablePostBack="true" runat="server">
                                </x:Button>

                                 <x:Button ID="btnSelect_All" runat="server" Text="全选" OnClick="btnSelect_All_Click"></x:Button>
                                <x:Button ID="btnDelete" Text="删除选中信息" Icon="Delete" EnablePostBack="true" runat="server" OnClick="btnDelete_Click" ConfirmText="确定删除？" ConfirmTarget="Top" Enabled="false">
                                </x:Button>
                                <x:Button ID="ButtonUpdate" Text="编辑选中行" Icon="Pencil" OnClick="ButtonUpdate_Click" runat="server">
                                </x:Button>
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
                                <asp:Label ID="Label3" runat="server" Text='<%#RowNumber( Container.DataItemIndex + 1) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:CheckBoxField ID="CBoxSelect" CommandName="CBSelect" DataField="Project" runat="server" AutoPostBack="true" Enabled="true" RenderAsStaticField="false" Width="30" />
                        <x:BoundField Enabled="true" DataField="EntryPerson" SortField="EntryPerson" Hidden="true" />
                        <x:BoundField DataField="PlatformName" SortField="PactNum" Width="150px" HeaderText="平台名称" />
                        <x:BoundField DataField="PlatformRank" SortField="PactNum" Width="150px" HeaderText="平台级别" />
                        <x:BoundField DataField="AgreeUnit" SortField="PactNum" Width="150px" HeaderText="批复部门" />
                        <x:BoundField DataField="AgreeTime" SortField="AgreeTime" Width="150px" HeaderText="批复日期" DataFormatString="{0:yyyy-MM-dd}" />
                        <x:BoundField DataField="AgreeNumber" SortField="AgreeNumber" Width="150px" HeaderText="批复文号" />
                        <x:BoundField DataField="PlatformPrincipal" SortField="PlatformPrincipal" Width="100px" HeaderText="平台负责人" />
                        <x:TemplateField HeaderText="平台成员" Width="60px">
                            <ItemTemplate>
                                <a href="javascript:<%# GetEditUrlmem(Eval("PlatformID")) %>">详情</a>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:BoundField DataField="AgreeExpenditure" SortField="AgreeExpenditure" Width="100px" HeaderText="批复经费" />
                        <x:TemplateField HeaderText="平台管理" Width="60px">
                            <ItemTemplate>
                                <a href="javascript:<%# GetEditUrlmana(Eval("PlatformID")) %>">详情</a>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:BoundField DataField="PlatformType" SortField="PactNum" Width="100px" HeaderText="平台类别" />
                        <x:TemplateField Width="60px" HeaderText="保密级别">
                            <ItemTemplate>
                                <asp:Label ID="Label8" runat="server" Text='<%# SecrecyLevelName(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "SecrecyLevel"))) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:TemplateField Width="60px" HeaderText="相关文档">
                            <ItemTemplate>
                                <a href="javascript:<%# GetEditUrl(Eval("PlatformID")) %>">操作</a>
                            </ItemTemplate>
                        </x:TemplateField>
                    </Columns>
                </x:Grid>
            </Items>
        </x:Panel>
        <x:Window ID="Window_AddPlatform" Popup="false" EnableIFrame="true" runat="server" AutoScroll="false"
            EnableMaximize="false" EnableResize="false" Height="410px" Width="450px" Title="新增平台信息">
        </x:Window>
        <x:Window ID="Window_Update" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableResize="false" Height="410px" Width="450px" Title="更新平台信息">
        </x:Window>
        <x:Window ID="DownLoad" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableMinimize="false" EnableResize="false" Height="250px" Width="350px" Title="相关文件操作">
        </x:Window>
        <%--lby ↓--%>
        <x:Window ID="PlatformMemberWindow" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableMinimize="false" EnableResize="false" Height="250px" Width="350px" Title="成员详情">
        </x:Window>
        <x:Window ID="PlatformManagementWindow" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableMinimize="false" EnableResize="false" Height="250px" Width="350px" Title="管理详情">
        </x:Window>
    </form>
</body>
</html>
