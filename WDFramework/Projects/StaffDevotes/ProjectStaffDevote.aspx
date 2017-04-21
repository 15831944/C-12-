<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectStaffDevote.aspx.cs" Inherits="WDFramework.Projects.ProjectStaffDevote" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta name="sourcefiles" content="~/Projects/ADD_ProjectStaffDevote.aspx;~/Projects/Update_ProjectStaffDevote.aspx" />
</head>
<body oncontextmenu='return false'><%--取消鼠标右键的点击--%>
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <%--  --%>
       <x:Panel ID="Panel1" runat="server" BodyPadding="5px" EnableBackgroundColor="true"
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start"
            ShowHeader="false" Title="用户管理">
        <Items>
  <x:Grid ID="GridProjectAndPeople" runat ="server"  BoxFlex="1" ShowBorder="true" ShowHeader="false"
                        EnableCheckBoxSelect="false" EnableRowNumber="false" DataKeyNames="StaffDevoteID" 
                        AllowSorting="true" SortColumnIndex="0" SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" OnRowCommand ="GridProjectAndPeople_RowCommand"   OnPageIndexChange="GridProjectAndPeople_PageIndexChange" >
        <Toolbars>
        <x:Toolbar ID="Toolbar2" runat="server" Width ="800px">
            <Items>
                <x:Label ID="Label2" runat="server" Label="Label" Width ="5" Text=" "></x:Label>
                <x:Label ID="Label20" runat="server" Label="Label" Text="查询条件"></x:Label>
                
                <x:DropDownList ID="ddlsearch" ShowLabel="false" AutoPostBack="true" Width ="100px" runat="server" TabIndex ="1" OnSelectedIndexChanged="ddlsearch_SelectedIndexChanged" >
                     <x:ListItem Text="全部" Value="0" Selected ="true" />
                    <x:ListItem Text="项目名称" Value="项目名称" />
                    <x:ListItem Text="人员姓名" Value="人员姓名" />
                    </x:DropDownList>
               <x:Label ID="Label4" runat="server" Label="Label" Width ="5" Text=" "></x:Label>
                <x:TextBox ID="TriProjectNames" runat="server" EmptyText="输入搜索内容" MaxLength ="20" ShowLabel="true" TabIndex ="1"  >
                    ShowTrigger2 ="false" 
                                         </x:TextBox>      
               
                <x:Button ID="FindDevoteTime" Text="搜索" Icon="SystemSearch" Type ="Submit"  runat="server" OnClick ="FindDevoteTime_Click" >
                </x:Button>
                 <x:Button ID="btnRefresh" runat="server" Icon="ArrowRotateClockwise"  Text="刷新" OnClick ="btnRefresh_Click"  >  
                                    </x:Button> 
              <%--  <x:Label ID="Label4" runat="server" Label="Label" Width ="20" Text=" "></x:Label>  --%>          
                <x:Button ID="btnAddProject" Text="新增项目人员投入信息" Icon="Add" runat="server"  >
                </x:Button>

                 <x:Button ID="btnSelect_All" runat="server" Text="全选" OnClick="btnSelect_All_Click"></x:Button>
                <x:Button ID="btnDelete" Text="删除选中行" Icon="Delete" runat="server" OnClick ="btnDelete_Click" ConfirmText ="确定删除？" ConfirmTarget ="Top"  >
                </x:Button>
                <x:Button ID="btnUpdate" Text="编辑选择行" Icon="Pencil"  runat="server" OnClick ="btnUpdate_Click" >
                </x:Button>
                <x:Button ID="btn_Get" Text="导出Excel文件" ConfirmText="确定导出？" Icon="Disk" EnablePostBack="true" runat="server"  OnClick="btn_Get_Click"   EnableAjax="false" DisableControlBeforePostBack="false">
                                    </x:Button>
                 <x:Button ID="reprot" Text="报表"  Icon="Report" EnablePostBack="true" runat="server" >
                                         <Menu ID="Menu1" runat="server">
                                    <x:MenuButton ID="reprot1" runat="server" Text="按项目统计科研人员信息">
                                    </x:MenuButton>
                                
                                </Menu>
                                    </x:Button>
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
            <x:TemplateField Width="30px" >
                    <ItemTemplate>
                        <asp:Label ID="Label8" runat="server" Text='<%# RowNumber(Container.DataItemIndex + 1) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>
           <x:CheckBoxField ID="CBoxSelect" CommandName="CBSelect" DataField="Project" runat="server" AutoPostBack="true" Enabled="true" RenderAsStaticField="false" Width="30" /> 
           <x:BoundField Enabled="true" DataField="EntryPerson" SortField="EntryPerson" Hidden="true" /> 
            <x:TemplateField Width="30px" Hidden ="true" >
                    <ItemTemplate>
                        <asp:Label ID="Label7" runat="server" Text='<%# RowNumber(Container.DataItemIndex + 1) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>
            <x:TemplateField Width="80px" HeaderText="姓名">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# UserName(Convert.ToInt32 (DataBinder.Eval(Container.DataItem, "UserInfoID"))) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>         
           <%--<x:BoundField  DataField="UserInfoID"  HeaderText="人员名称" /> --%>       
           <x:BoundField  DataField="DevoteTime" DataFormatString="{0:yyyy-MM-dd}" HeaderText="投入时间" />
            <x:BoundField  DataField="ExitTime" DataFormatString="{0:yyyy-MM-dd}" HeaderText="退出时间" />
           <x:TemplateField Width="80px" HeaderText="项目名称">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# ProjectName(Convert.ToInt32 (DataBinder.Eval(Container.DataItem, "ProjectID"))) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>  
           <%-- <x:BoundField  DataField="ProjectID" HeaderText="项目名称" />--%>
             <x:TemplateField Width="80px" HeaderText="涉密等级">
                    <ItemTemplate>
                        <asp:Label ID="Label6" runat="server" Text='<%# SecrecyLevelName(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "SecrecyLevel"))) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>  
            <x:BoundField  DataField="ProjectCompletion" HeaderText="项目承担任务及完成情况" />
            <x:BoundField  DataField="Sort" HeaderText="排序" />
            
       </Columns>
       </x:Grid>
              </Items>
       </x:Panel>
        <x:Window ID="WindowProjectImportant" Popup="false" EnableIFrame="true"  runat="server"
            EnableMaximize="false" EnableResize="false" Height="400px" Width="450px" Title="添加项目相关人员投入信息">
        </x:Window>
        <x:Window ID="WindowUpdate" Popup="false" EnableIFrame="true"  runat="server"
            EnableMaximize="false" EnableResize="false" Height="400px" Width="450px" Title="编辑项目相关人员投入信息">
        </x:Window>

        <x:Window ID="Window2" Title="查询" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableResize="false" Target="Parent" 
            IsModal="True" Width="750px" Height="450px">
        </x:Window>
        <x:Window ID="WindowReport" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="true" EnableResize="false" Height="450px" Width="800px">
        </x:Window>
        <x:Label ID="labResult" Visible="false" runat="server">
        </x:Label>
    </form>
</body>
</html>