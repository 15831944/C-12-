<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search_Lectures.aspx.cs" Inherits="WebApplication1.查询讲学页面" %>

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

                <x:Grid ID="Grid_Lectures" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                     EnableRowNumberPaging="true" EnableTextSelection="true" EnableCheckBoxSelect="false"
                    DataKeyNames="UnitLecturesID" AllowSorting="true" SortColumnIndex="0"
                    AllowPaging="true" IsDatabasePaging="true" OnPageIndexChange="Grid_Lectures_PageIndexChange" OnRowCommand="Grid_Lectures_RowCommand">

                    <%--OnPageIndexChange="Grid_Info_PageIndexChange"--%>
                    <Toolbars>
                        <x:Toolbar ID="Toolbar_top" runat="server">
                            <Items>
                                <x:Label ID="AgencyID" Width="70px" runat="server" CssClass="marginr" ShowLabel="true" Text="所属机构"></x:Label>
                                <x:DropDownList ID="DropDownList_Agency" ShowLabel="false" AutoPostBack="true" runat="server" Width="300px">
                                    <x:ListItem Text="全部" Value="0" />
                                    <%--<x:ListItem Text="研究所机构" Value="1" />
                                    <x:ListItem Text="挂靠机构" Value="2" />--%>
                                </x:DropDownList>
                                <%--<x:TwinTriggerBox runat="server" EmptyText="输入要搜索的关键词" ShowLabel="false" ID="ttbSearch" ShowTrigger1="false"
                                        Trigger2Icon="Search">
                                    </x:TwinTriggerBox>--%>
                                <x:Button ID="btnCheck" runat="server" EnablePostBack="true" Icon="SystemSearch" Text="搜索" OnClick="btnCheck_Click" Type="Submit">
                                </x:Button>
                                <x:Button ID="btnRefresh" runat="server" Icon="ArrowRotateClockwise" Text="刷新" OnClick="btnRefresh_Click">
                                </x:Button>
                                <x:Button ID="btnAddLecture" Text="新增讲学信息" Icon="Add" EnablePostBack="true" runat="server">
                                </x:Button>
                                <x:Button ID="btnUpdate" Text="编辑选中行" Icon="Pencil" runat="server" OnClick="ButtonUpdate_Click">
                                </x:Button>

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
                         
                        <x:BoundField DataField="LecturesName" SortField="LecturesName" Width="100px" HeaderText="姓名" />
                        <x:BoundField Enabled="true" DataField="EntryPerson" SortField="EntryPerson" Hidden="true" />
                        <x:TemplateField Width="150px" HeaderText="所属机构">
                            <ItemTemplate>
                                <asp:Label ID="LabeAgency" runat="server" Text='<%# AgencyName(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "AgencyID"))) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:BoundField DataField="UReportName" SortField="UReportName" Width="150px" HeaderText="报告名称" />
                        <x:BoundField DataField="LecturesTime" SortField="LecturesTime" Width="150px" DataFormatString="{0:yyyy-MM-dd}" HeaderText="时间" />
                        <x:BoundField DataField="LecturesPlace" SortField="LecturesPlace" Width="150px" HeaderText="地点" />
                        <x:BoundField DataField="listenerNumber" SortField="listenerNumber" Width="150px" HeaderText="听众人数" />
                        <%--<x:BoundField DataField="SecrecyLevel" SortField="SecrecyLevel" Width="150px" HeaderText="保密级别" />--%>
                        <x:BoundField DataField="WorkUnit" SortField="WorkUnit" Width="150px" HeaderText="工作单位" />
                        <x:BoundField DataField="WorkTitle" SortField="WorkTitle" Width="150px" HeaderText="职称/职务" />
                        <x:BoundField DataField="Identity" SortField="Identity" Width="150px" HeaderText="身份证号" />
                        <x:BoundField DataField="Telephone" SortField="Telephone" Width="150px" HeaderText="手机号" />
                        <%--<x:BoundField DataField="Remark" SortField="Remark" Width="150px" HeaderText="备注" />--%>
                         <x:TemplateField Width="80px" HeaderText="保密级别">
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# ChangeSecrecyLevel(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "SecrecyLevel"))) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>
                         <x:TemplateField HeaderText="备注" Width="60px" >
                           <ItemTemplate>
                        <a href="javascript:<%# GetEditUrl(Eval("UnitLecturesID ")) %>">详情</a>
                          </ItemTemplate>
                          </x:TemplateField>
                         
                        <%--<x:LinkButtonField HeaderText="操作" CommandName="Action1"  Text="修改" Width="80px"/>--%>
                        <%--<x:LinkButtonField Width="80px" Text="下载" HeaderText="相关文档" EnableAjax="false" CommandName="ActiveDown" />--%>
                          <x:TemplateField HeaderText="相关文档" Width="100px"  ID="TemplateField1">
                           <ItemTemplate>
                        <a id="A1" href="javascript:<%# GetRecordUrlDown(Eval("UnitLecturesID")) %>" >下载</a>
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
            EnableMaximize="false" EnableResize="false" Height="450px" Width="850px" Title="更新">
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
