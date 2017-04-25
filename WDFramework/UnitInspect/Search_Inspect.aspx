<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search_Inspect.aspx.cs" Inherits="WebApplication1.查询考察信息页面" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body oncontextmenu='return false'><%--取消鼠标右键的点击--%>
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <x:Panel ID="Panel1" runat="server" BodyPadding="5px" EnableBackgroundColor="true"
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start"
            ShowHeader="false" Title="用户管理">
        <Items>        
            <%--  --%>
            <%--  --%>

         <x:Grid ID="Grid_UnitInspect" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                          DataKeyNames="UnitInspectID"
                        AllowSorting="true" SortColumnIndex="0" SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" OnPageIndexChange="Grid_UnitInspect_PageIndexChange" OnRowCommand="Grid_UnitInspect_RowCommand" AutoPostBack="true" >
                        <%--OnPageIndexChange="Grid_Info_PageIndexChange"--%>
                        <Toolbars>
                            <x:Toolbar ID="Toolbar_top" runat="server" >
                                <Items>
                                   <x:Label ID="lYear" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="查询条件：">
                                        </x:Label>
                                                         
                                          <x:DropDownList ID="ddl_search" Width="100px" EnableEdit="false" ShowLabel="false" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddl_search_SelectedIndexChanged" >  
                                              <x:ListItem Text="全部" Selected="true" Value="0" />             
                                              <x:ListItem Text="开始年份" Value="1" />  
                                              <x:ListItem Text="工作单位" Value="2" />                                           
                                    </x:DropDownList>                        
                                          <x:DropDownList ID="dCondition" Width="100px" EnableEdit="true" ShowLabel="false" AutoPostBack="true" runat="server" >                                                        
                                    </x:DropDownList>
                                    <x:TextBox ID="tb_content" runat="server" Width="50px"></x:TextBox>
                                      <x:Button ID="Select" Text="搜索" Icon="SystemSearch" Type="Submit"  runat="server" OnClick="Select_Click" >
                                     </x:Button>    
                                    <x:Button ID="btnRefresh" runat="server" Icon="ArrowRotateClockwise"  Text="刷新" OnClick="btnRefresh_Click" >  
                                    </x:Button>
                                    <x:Button ID="btnAddInspect" Text="新增单位考察信息" Icon="Add" EnablePostBack="true" runat="server">
                                    </x:Button>

                                     <x:Button ID="btnSelect_All" runat="server" Text="全选" OnClick="btnSelect_All_Click"></x:Button>
                                    <x:Button ID="btnDelete" Text="删除选中行" Icon="Delete" ConfirmText="确定删除？" Enabled="false" EnablePostBack="true" runat="server" OnClick="btnDelete_Click" >
                                    </x:Button>
                                  <x:Button ID="btnUpdate" Text="编辑选中行" Icon="Pencil"  EnablePostBack="true"  runat="server" OnClick="btnUpdate_Click">
                                  </x:Button>  
                                      <x:Button ID="btn_Get" Text="导出Excel文件" ConfirmText="确定导出？" Icon="Disk" EnablePostBack="true" runat="server" OnClick="btn_Get_Click" EnableAjax="false">
                                    </x:Button>
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
                                <x:ListItem Text="20" Value="20" Selected ="true" />
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
                            <x:CheckBoxField ID="CBoxSelect" CommandName="CBSelect" DataField="insepctid"  runat="server" AutoPostBack="true" Enabled="true" RenderAsStaticField="false" Width="30" />
                            <x:BoundField Enabled="true" DataField="EntryPerson" SortField="EntryPerson" Hidden="true" /> 
                               <x:TemplateField Width="30px" Hidden="true">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%#RowNumber( Container.DataItemIndex + 1) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>
                            <%-- <x:BoundField Enabled="true" DataField="UnitInspectID" SortField="UnitInspectID" Hidden="true" />  --%>
                            <x:BoundField DataField="InspectName" SortField="InspectName" Width="100px" HeaderText="姓名" />         
                            <x:BoundField DataField="WorkPlace" SortField="WorkPlace" Width="150px" HeaderText="工作单位" />
                            <x:BoundField DataField="Duty" SortField="Duty" Width="150px" HeaderText="职称/职务" />
                            <x:BoundField DataField="InspectTime" SortField="DeptBH" Width="150px" HeaderText="时间" DataFormatString="{0:yyyy-MM-dd}" />
                            <x:TemplateField Width="80px" HeaderText="所属部门">
                    <ItemTemplate>
                        <asp:Label ID="LabeAgency" runat="server" Text='<%# FindAgencyName(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "AgencyID"))) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>
                                  <x:TemplateField Width="80px" HeaderText="保密级别">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# ChangeSecrecyLevel(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "SecrecyLevel"))) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>       
                              <x:BoundField DataField="DutVisitContenty" SortField="VisitContent" Hidden="true" Width="150px" HeaderText="参观内容" />
                            <%-- <x:LinkButtonField ID="linContent"  Width="80px" Text="详情"  HeaderText="参观内容" EnableAjax="false" CommandName="Details"/>--%>
                             <x:TemplateField HeaderText="参观内容" Width="60px" >
                           <ItemTemplate>
                        <a href="javascript:<%# GetEditUrl(Eval("UnitInspectID")) %>">详情</a>
                          </ItemTemplate>
                          </x:TemplateField>
                         <x:TemplateField HeaderText="相关文档"  Width="60px" >
                           <ItemTemplate>
                        <a href="javascript:<%# GetEditUrlx(Eval("UnitInspectID")) %>">操作</a>
                          </ItemTemplate>
                          </x:TemplateField>
                             
                        </Columns>
                    </x:Grid>
       </Items>
       </x:Panel>
        <x:Window ID="Window_addInspect" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableMinimize="false"  EnableResize="false" Height="450px" Width="800px"  Title="添加">
        </x:Window>

        <x:Window ID="Window_updateInspect" Popup="false" EnableIFrame="true" runat="server"
             EnableMaximize="false" EnableMinimize="false"  EnableResize="false" Height="450px" Width="800px" Title="更新">
        </x:Window>
         <x:Window ID="Details" Popup="false" EnableIFrame="true"  runat="server"
             EnableMaximize="false" EnableMinimize="false"  EnableResize="false" Height="250px" Width="350px" >
        </x:Window>
           <x:Window ID="DownLoad" Popup="false" EnableIFrame="true"  runat="server"
             EnableMaximize="false" EnableMinimize="false"  EnableResize="false" Height="250px" Width="350px" Title="相关文件操作">
        </x:Window>
        <x:Label ID="labResult" Visible="false" runat="server">
        </x:Label>
    </form>
</body>

</html>
