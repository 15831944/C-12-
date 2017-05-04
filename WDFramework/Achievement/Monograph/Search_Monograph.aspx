<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search_Monograph.aspx.cs" Inherits="WebApplication1.Search_Monograph" %>

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

                <x:Grid ID="Grid_Monograph" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                    DataKeyNames="MonographID"
                    AllowSorting="true" SortColumnIndex="0" SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" AutoPostBack="true" OnPageIndexChange="Grid_Monograph_PageIndexChange" OnRowCommand="Grid_Monograph_RowCommand">
                    <%--OnPageIndexChange="Grid_Info_PageIndexChange"--%>
                    <Toolbars>
                        <x:Toolbar ID="Toolbar_top" runat="server">
                            <Items>
                                <%--<x:Label ID="year" Width="30px" runat="server" CssClass="marginr" ShowLabel="true" Text="年份"></x:Label>
                                    <x:DropDownList ID="ddlsearch" Width="200px" ShowLabel="false" AutoPostBack="true" runat="server" TabIndex="1">                                  
                                    </x:DropDownList>

                                    <x:Label ID="Label15"  Width="50px" runat="server"  ShowLabel="true" Text=" "></x:Label> --%>

                                <x:Label ID="AgencyID" Width="60px" runat="server" CssClass="marginr" ShowLabel="true" Text="查询条件"></x:Label>
                                <x:DropDownList ID="dChoose" Width="100px" ShowLabel="false" AutoPostBack="true" runat="server" OnSelectedIndexChanged="dChoose_SelectedIndexChanged">
                                    <x:ListItem Text="全部" Value="全部" />
                                    <x:ListItem Text="出版年份" Value="出版年份" />
                                    <x:ListItem Text="作者" Value="作者" />
                                    <%--x:ListItem Text="类别" Value="类别" /--%>
                                    <x:ListItem Text="著作名称" Value="著作名称" />
                                    <x:ListItem Text="第一作者" Value="第一作者" />
                                    <x:ListItem Text="第一作者身份" Value="第一作者身份" />
                                    <x:ListItem Text="部门" Value="部门" />
                                </x:DropDownList>
                                <x:Label ID="Label2" runat="server" Label="Label" Text=" " Width="5px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:TextBox ID="tCondition" ShowLabel="true" MaxLength="10" Enabled="false" MaxLengthMessage="最多可输入10个字符" EmptyText="请输入搜索条件" Width="100px" CssClass="marginr" runat="server" AutoPostBack="true">
                                </x:TextBox>
                                <x:Label ID="Label1" runat="server" Label="Label" Text=" " Width="5px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:DropDownList ID="dCondition" Width="100px" EnableEdit="true" Enabled="false" ShowLabel="false" AutoPostBack="true" runat="server">
                                </x:DropDownList>
                                <x:Button ID="Select" Text="搜索" Type="Submit" Icon="SystemSearch" runat="server" OnClick="Select_Click">
                                </x:Button>
                                <x:Button ID="btnRefresh" runat="server" Icon="ArrowRotateClockwise" Text="刷新" OnClick="btnRefresh_Click">
                                </x:Button>
                                <%--<x:Button ID="btnCheck" runat="server" EnablePostBack="true"  Icon="SystemSearch" Text="搜索">
                                    </x:Button> --%>
                                <x:Button ID="btn_AddMonograph" Text="新增专著信息" Icon="Add" EnablePostBack="true" runat="server">
                                </x:Button>

                                 <x:Button ID="btnSelect_All" runat="server" Text="全选" OnClick="btnSelect_All_Click"></x:Button>
                                <x:Button ID="Delete" Text="删除选中行" Icon="Delete" ConfirmText="确定删除？" Enabled="false" ConfirmTarget="Top" runat="server" OnClick="Delete_Click">
                                </x:Button>
                                 
                                <%--  <x:Button ID="btn_Get" Text="导出所选信息" Icon="Disk" EnablePostBack="true" runat="server">
                                    </x:Button>--%>
                                <x:Button ID="btn_UpdateInspect" Text="编辑选中行" Icon="BulletEdit" EnablePostBack="true" runat="server" OnClick="btn_UpdateInspect_Click">
                                </x:Button>
                                <x:Button ID="btnTool" EnablePostBack="false" Text="工具"  runat="server">
                                <Menu ID="Menu1" runat="server">
                                    <x:MenuButton ID="btnExcel" runat="server" Text="Excel导入" Icon="Add" EnablePostBack="true">
                                    </x:MenuButton>
                                    <x:MenuButton ID="btn_Get" Text="导出Excel文件" ConfirmText="确定导出？" Icon="Disk" EnablePostBack="true" runat="server" OnClick="btn_Get_Click" EnableAjax="false">
                                    </x:MenuButton>
                                </Menu>
                            </x:Button>
                                <%--<x:Button ID="btn_Get" Text="导出Excel文件" ConfirmText="确定导出？" Icon="Disk" EnablePostBack="true" runat="server" OnClick="btn_Get_Click" EnableAjax="false" DisableControlBeforePostBack="false">
                                </x:Button>--%>
                                <%--  <x:Button ID="reprot" Text="报表"  Icon="Report" EnablePostBack="true" runat="server" >
                                         <Menu ID="Menu1" runat="server">
                                    <x:MenuButton ID="reprot1" runat="server" Text="分部门按著作名称统计专著情况">
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
                                <asp:Label ID="Label5" runat="server" Text='<%#RowNumber( Container.DataItemIndex + 1) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:CheckBoxField ID="CBoxSelect" CommandName="CBSelect" DataField="insepctid" runat="server" AutoPostBack="true" Enabled="true" RenderAsStaticField="false" Width="30" />
                        <x:BoundField Enabled="true" DataField="EntryPerson" SortField="EntryPerson" Hidden="true" />
                        <x:TemplateField Width="30px" Hidden="true">
                            <ItemTemplate>
                                <asp:Label ID="Label6" runat="server" Text='<%#RowNumber( Container.DataItemIndex + 1) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:BoundField DataField="MonographName" SortField="MonographName" Width="100px" HeaderText="著作名称" />
                        <x:BoundField DataField="Sort" SortField="Sort" Width="150px" HeaderText="类别" Hidden="true" />
                        <x:BoundField DataField="FirstWriter" SortField="FirstWriter" Width="150px" HeaderText="第一作者" />
                         <x:BoundField DataField="WriterIdentity" SortField="WriterIdentity" Width="150px" HeaderText="第一作者身份" />
                        <x:BoundField DataField="Publisher" SortField="Publisher" Width="150px" HeaderText="出版单位" />
                        <x:BoundField DataField="IssueRegin" SortField="IssueRegin" Width="150px" HeaderText="出版地" />
                        <x:BoundField DataField="PUblicationTime" SortField="PUblicationTime" Width="150px" HeaderText="出版时间" DataFormatString="{0:yyyy-MM-dd}" />
                        <x:BoundField DataField="BookNuber" SortField="BookNuber" Width="150px" HeaderText="图书编号" />
                        <x:BoundField DataField="Revision" SortField="Revision" Width="150px" HeaderText="版次" />
                        <x:BoundField DataField="MonographType" SortField="MonographType" Width="150px" HeaderText="专著类型" />
                        <x:BoundField DataField="CIPNum" SortField="CIPNum" Width="150px" HeaderText="CIP号" />
                        <x:BoundField DataField="ISBNNum" SortField="ISBNNum" Width="150px" HeaderText="ISBN号" />
                        <x:BoundField DataField="PaperUnit" SortField="PaperUnit" Width="150px" HeaderText="所属机构" />
                        <x:TemplateField Width="80px" HeaderText="所属成果名称" Hidden="true">
                            <ItemTemplate>
                                <asp:Label ID="LabeAgency" runat="server" Text='<%# FindName(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "AchievementID"))) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:TemplateField Width="80px" HeaderText="保密级别">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# ChangeSecrecyLevel(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "SecrecyLevel"))) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:TemplateField Width="80px" HeaderText="作者" Hidden="true">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# getpeople(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "MonographID"))) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:BoundField DataField="Remark" SortField="Remark" Width="150px" Hidden="true" HeaderText="备注" />
                        <x:TemplateField HeaderText="作者" Width="60px">
                            <ItemTemplate>
                                <a href="javascript:<%# GetEditUrlw(Eval("MonographID")) %>">详情</a>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:TemplateField HeaderText="备注" Width="60px">
                            <ItemTemplate>
                                <a href="javascript:<%# GetEditUrl(Eval("MonographID")) %>">详情</a>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:TemplateField HeaderText="封面" Width="60px">
                            <ItemTemplate>
                                <a href="javascript:<%# GetEditUrlf(Eval("MonographID")) %>">操作</a>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:TemplateField HeaderText="版权页" Width="60px">
                            <ItemTemplate>
                                <a href="javascript:<%# GetEditUrlb(Eval("MonographID")) %>">操作</a>
                            </ItemTemplate>
                        </x:TemplateField>
                    </Columns>
                </x:Grid>
            </Items>
        </x:Panel>
        <x:Window ID="Window_addMonograph" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableMinimize="false" EnableResize="false" Height="495px" Width="800px" Title="添加">
        </x:Window>

        <x:Window ID="Window_UpdateMonograph" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableMinimize="false" EnableResize="false" Height="495px" Width="800px" Title="更新">
        </x:Window>
        <x:Window ID="Remark" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableMinimize="false" EnableResize="false" Height="250px" Width="350px" Title="备注">
        </x:Window>
        <x:Window ID="Writer" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableMinimize="false" EnableResize="false" Height="250px" Width="350px">
        </x:Window>
        <x:Window ID="DownLoadf" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableMinimize="false" EnableResize="false" Height="250px" Width="350px" Title="相关文件操作">
        </x:Window>
        <x:Window ID="DownLoadb" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableMinimize="false" EnableResize="false" Height="250px" Width="350px" Title="相关文件操作">
        </x:Window>
        <x:Window ID="WindowReport" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="true" EnableResize="false" Height="450px" Width="800px">
        </x:Window>
        <x:Window ID="Window_Import" Popup="false" EnableIFrame="true" runat="server" 
            EnableMaximize="false" EnableResize="false" Height="250px" Width="350px" OnClose="Window_Import_Close" CloseAction="HidePostBack">
        </x:Window>
        <x:Label ID="labResult" Visible="false" runat="server">
        </x:Label>
    </form>
</body>
</html>
