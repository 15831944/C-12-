<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectImportant.aspx.cs" Inherits="WDFramework.Projects.ProjectImportant" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta name="sourcefiles" content="~/Projects/ADD_ProjectImportant.aspx;~/Projects/Update_ProjectImportant.aspx" />
</head>
<body oncontextmenu='return false'><%--取消鼠标右键的点击--%>
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <%--  --%>
       <x:Panel ID="Panel1" runat="server" BodyPadding="5px" EnableBackgroundColor="true"
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start"
            ShowHeader="false" Title="用户管理">
        <Items>
              <x:Grid ID="GridProjectAndTime"  runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                        EnableCheckBoxSelect="false" EnableRowNumber="false" DataKeyNames="ProjectImportantNodeID" 
                        AllowSorting="true" SortColumnIndex="0" SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" OnRowCommand ="GridProjectAndTime_RowCommand"   OnPageIndexChange="GridProjectAndTime_PageIndexChange" >
        <Toolbars>
        <x:Toolbar ID="Toolbar_top" runat="server" Width ="800px">
            <Items>
                <x:Label ID="Label2" runat="server" Label="Label" Width ="5" Text=" "></x:Label>
                <x:Label ID="Label18" runat="server" Label="Label" Text="查询条件"></x:Label>
               <x:Label ID="Label3" runat="server" Label="Label" Width ="5" Text=" "></x:Label>  
                <x:DropDownList ID="ddl_search" runat="server" ShowLabel="false" AutoPostBack="true" Width ="100px" OnSelectedIndexChanged="ddl_search_SelectedIndexChanged">
                    <x:ListItem Text="全部" Value="0" Selected="true" />
                    <x:ListItem Text="项目" Value="1" />
                    <x:ListItem Text="负责人" Value="2" />
                    <x:ListItem Text="机构" Value="3" />
                      <x:ListItem Text="项目完成情况" Value="4" />
                      <x:ListItem Text="实际完成" Value="5" />
                </x:DropDownList>            
                 <x:TextBox ID="TriProjectNames" runat="server" MaxLength ="20" ShowLabel="true" TabIndex ="1" Enabled="false"  >
                    ShowTrigger2 ="false" 
                                         </x:TextBox>
                
                
                <x:Button ID="FindObjectAndTime" Text="搜索" Icon="SystemSearch" runat="server" Type="Submit" OnClick ="FindObjectAndTime_Click"  >
                </x:Button> 

                <x:Button ID="btnRefresh" runat="server" Icon="ArrowRotateClockwise"  Text="刷新" OnClick ="btnRefresh_Click" >  
                                    </x:Button> 

                         
                <x:Button ID="btnAddProject" Text="新增项目重大节点信息" Icon="Add" runat="server"  >
                </x:Button>

                 <x:Button ID="btnSelect_All" runat="server" Text="全选" OnClick="btnSelect_All_Click"></x:Button>
                <x:Button ID="btnDelete" Text="删除选中行" Icon="Delete" runat="server" OnClick ="btnDelete_Click"   >
                </x:Button>
                <x:Button ID="btnUpdate" Text="编辑选中行" Icon="Pencil"  runat="server" OnClick ="btnUpdate_Click"   >
                </x:Button>
                 <x:Button ID="btn_Get" Text="导出Excel文件" ConfirmText="确定导出？" Icon="Disk" EnablePostBack="true" runat="server"  OnClick ="btn_Get_Click"  EnableAjax="false" DisableControlBeforePostBack="false">
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
                        <asp:Label ID="Label8" runat="server" Text='<%# RowNumber( Container.DataItemIndex + 1) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>
           <x:CheckBoxField ID="CBoxSelect" CommandName="CBSelect" DataField="Project" runat="server" AutoPostBack="true" Enabled="true" RenderAsStaticField="false"   Width="30" /> 
          
            <x:BoundField Enabled="true" DataField="EntryPerson" SortField="EntryPerson" Hidden="true" /> 
            <x:TemplateField Width="30px" Hidden ="true" >
                    <ItemTemplate>
                        <asp:Label ID="Label7" runat="server" Text='<%# RowNumber( Container.DataItemIndex + 1) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>
           <x:BoundField  DataField="MissionName" SortField="MissionName" Width="100px"  HeaderText="节点名称" Hidden ="true"  />
           <x:TemplateField Width="80px" HeaderText="项目名称">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# ProjectName(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ProjectID"))) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>           
           <%--<x:BoundField DataField="ProjectID"  HeaderText="项目名称" /> --%>
            <x:BoundField DataField="StartTime" DataFormatString="{0:yyyy-MM-dd}" HeaderText="开始时间" /> 
            <x:BoundField DataField="EndTime" DataFormatString="{0:yyyy-MM-dd}" HeaderText="计划完成时间" /> 
            <x:BoundField DataField="PersonCharge" HeaderText="负责人" /> 
            <x:BoundField DataField="ResearchCharge" HeaderText="机构" /> 
            <x:BoundField DataField="CompleteSpecificPerson" HeaderText="具体完成人" /> 
   <x:TemplateField Width="80px" HeaderText="保密等级">
                    <ItemTemplate>
                        <asp:Label ID="Label6" runat="server" Text='<%# SecrecyLevelName(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "SecrecyLevel"))) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>                   
          
            <x:BoundField  DataField="Remark" SortField="Remark" Width="100px"  HeaderText="备注" Hidden ="true" /> 
          
                         <x:TemplateField HeaderText="节点名称" Width="60px" >
                           <ItemTemplate>
                        <a href="javascript:<%# GetEditUrlW(Eval("ProjectImportantNodeID ")) %>">详情</a>
                          </ItemTemplate>
                          </x:TemplateField> 
           <x:BoundField DataField="ProjectCompletion" HeaderText="项目完成情况" />       
          <x:TemplateField HeaderText="备注" Width="60px" >
                           <ItemTemplate>
                        <a href="javascript:<%# GetEditUrl(Eval("ProjectImportantNodeID ")) %>">详情</a>
                          </ItemTemplate>
                          </x:TemplateField>    
          
                 <x:BoundField DataField="ActualComleption" HeaderText="实际完成" /> 
       </Columns>
       </x:Grid>

             </Items>
       </x:Panel>
        <x:Window ID="WindowProjectImportant" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableResize="false" Height="400px" Width="450px" Title="添加项目重大节点信息">
        </x:Window>
           <x:Window ID="WindowUpdate" Popup="false" EnableIFrame="true"  runat="server"
            EnableMaximize="false" EnableResize="false" Height="400px" Width="450px" Title="编辑项目重大节点信息">
        </x:Window>

        <x:Window ID="Window2" Title="查询" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableResize="false" Target="Parent" 
            IsModal="True" Width="750px" Height="450px">
        </x:Window>
          <x:Window ID="Remark" Popup="false" EnableIFrame="true"  runat="server"
            EnableMaximize="false" EnableResize="false" Height="250px" Width="350px" >
        </x:Window>
        <x:Window ID="MissionName" Popup="false" EnableIFrame="true"  runat="server"
            EnableMaximize="false" EnableResize="false" Height="250px" Width="350px" >
        </x:Window>
        <x:Label ID="labResult" Visible="false" runat="server">
        </x:Label>
    </form>
</body>
</html>