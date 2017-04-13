<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="WDFramework.WebForms.HomePage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
                <x:Grid ID="Grid_Announce" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                     EnableRowNumberPaging="true" EnableTextSelection="true" EnableCheckBoxSelect="false"
                    DataKeyNames="AnnouncementID" AllowSorting="true" SortColumnIndex="0"
                    AllowPaging="true" IsDatabasePaging="true" OnPageIndexChange="Grid_Inform_PageIndexChange" OnRowCommand="Grid_Files_RowCommand">
                    <Toolbars>
                        <x:Toolbar ID="Toolbar" runat="server">
                            <Items>
                                <x:Label ID="labSort" runat="server" CssClass="marginr" ShowLabel="false" Text="分类：">
                                </x:Label>
                                <x:DropDownList ID="DropDownList_AnnouceSort" ShowLabel="false" AutoPostBack="true" runat="server" EnableEdit="false">
                                    <x:ListItem Text="全部" Value="0" />
                                    <%-- <x:ListItem Text="通知" Value="1" />
                                    <x:ListItem Text="学校公告" Value="2" />
                                    <x:ListItem Text="外来公告" Value="3" />--%>
                                </x:DropDownList>
                                <%--<x:TwinTriggerBox runat="server" EmptyText="输入要搜索的关键词" ShowLabel="false" ID="ttbSearch" ShowTrigger1="false"
                                        Trigger2Icon="Search">
                                    </x:TwinTriggerBox>--%>
                                <x:Button ID="btnCheck" Text="搜索" Icon="SystemSearch" runat="server" OnClick="btnCheck_Click" Type="Submit">
                                </x:Button>
                                <x:Button ID="btnRefresh" runat="server" Icon="ArrowRotateClockwise" Text="刷新" OnClick="btnRefresh_Click">
                                </x:Button>
                                <x:Button ID="btn_AddAnnouncement" runat="server" EnablePostBack="true" Icon="Add" Text="新增通知公告">
                                </x:Button>
                                <x:Button ID="btnDelete" Text="删除选中行" Icon="Delete" EnablePostBack="true" runat="server" OnClick="btnDelete_Click"
                                    ConfirmText="确定删除？" ConfirmTarget="Top" Enabled="false">
                                </x:Button>

                                <%--<x:Button ID="Get2" Text="导出所选文件" Icon="Disk" EnablePostBack="true" runat="server">
                                </x:Button>--%>
                                <%--<x:Button ID="btn_UpdateAnnouncement" runat="server" EnablePostBack="true" Icon="BulletEdit" Text="修改">
                                </x:Button>--%>
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
                        <x:CheckBoxField ID="BoxSelect_Announce" CommandName="CBSelect" DataField="fileid" runat="server" AutoPostBack="true" Enabled="true" RenderAsStaticField="false" Width="30" />
                        
                        <%--<x:TemplateField HeaderText="主题" Width="250px">
                            <ItemTemplate>
                                <asp:Label ID="LabAnno" runat="server" Text='<%# AnnounceHeadLine(Convert.ToInt32( DataBinder.Eval
(Container.DataItem, "AnnouncementID"))) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>--%>

                        <%--<asp:Label ID="LabelAgency" runat="server"></asp:Label>--%>
                        <x:BoundField DataField="HeadLine" SortField="HeadLine" Width="200px" HeaderText="标题" />
                        <x:BoundField DataField="SourceAgency" SortField="SourceAgency" Width="300px" HeaderText="来源单位" />
                        <x:BoundField DataField="AnnouncementSortName" SortField="AnnouncementSortName" Width="100px" HeaderText="分类" />
                        <x:BoundField DataField="Time" SortField="Time" Width="100px" HeaderText="时间" DataFormatString="{0:yyyy-MM-dd}"/>
                        <%--<x:TemplateField HeaderText="分类" TextAlign="Center" Width="250px">
                            <ItemTemplate>
                               
                                <asp:Label ID="labAnnouceSort" runat="server" Text='<%# AnnounceSort(Convert.ToInt32( DataBinder.Eval
(Container.DataItem, "AnnouncementID"))) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>--%>
                        <x:TemplateField HeaderText="相关文档" Width="100px" ID="TemplateField1">
                            <ItemTemplate>
                                <a id="A1" href="javascript:<%# GetRecordUrlDown(Eval("AnnouncementID")) %>">下载</a>
                            </ItemTemplate>
                        </x:TemplateField>
                        <%--<x:LinkButtonField HeaderText="&nbsp;" EnableAjax="false" Width="80px" CommandName="ActionDown" Text="下载" />--%>
                    </Columns>

                </x:Grid>
            </Items>
        </x:Panel>
        <x:Window ID="Window_addAnnouncement" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableResize="false" Height="400px" Width="450px" Title="添加">
        </x:Window>

        <x:Window ID="Window_UpdateAnnouncement" Popup="false" EnableIFrame="true" runat="server" 
            EnableMaximize="false" EnableResize="false" Height="450px" Width="800px" Title="修改">
        </x:Window>
        <x:Window ID="Window_DownLoad" Popup="false" EnableIFrame="true" runat="server" 
            EnableMaximize="false" EnableResize="false" Height="250px" Width="350px">
        </x:Window>
        <x:Window ID="Window_NoLibraryMessage" Popup="false" EnableIFrame="true" runat="server" 
            EnableMaximize="false" EnableResize="false" Height="250px" Width="350px">
        </x:Window>
        <x:Label ID="labResult" Visible="false" runat="server">
        </x:Label>
    </form>
</body>
</html>
