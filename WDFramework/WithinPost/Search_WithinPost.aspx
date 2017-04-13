<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search_WithinPost.aspx.cs" Inherits="WDFramework.WithinPost.Search_WithinPost" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body oncontextmenu='return false'><%--取消鼠标右键的点击--%>
    <form id="form2" runat="server">
        <x:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <x:Panel ID="Panel1" runat="server" BodyPadding="5px" EnableBackgroundColor="true"
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start"
            ShowHeader="false">
       <%-- <x:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server"  />
   <x:Panel ID="Panel1" runat="server"  ShowBorder="True"  EnableCollapse="true"
                Layout="Column"    BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5"
                BoxConfigChildMargin="0 5 0 0" ShowHeader="false">--%>
        <Items>
            <x:Grid ID="Grid_Files" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
      EnableRowNumberPaging="true" EnableTextSelection="true" EnableCheckBoxSelect="false"
         DataKeyNames="WithinPostID" AllowSorting="true" SortColumnIndex="0"
          AllowPaging="true" IsDatabasePaging="true"  OnPageIndexChange="Grid_Files_PageIndexChange" OnRowCommand="Grid_Files_RowCommand" >
        <Toolbars>
                            <x:Toolbar ID="Toolbar_top" runat="server" >
                                <Items>
                                    <x:Label ID="DocumentCategoryID" Width="70px" runat="server" CssClass="marginr" ShowLabel="true" Text="文件分类"></x:Label>
                                    <x:DropDownList ID="DropDownListFile" ShowLabel="false" AutoPostBack="true" runat="server" EnableEdit="false">
                                    <x:ListItem Text ="全部" Value="0" />
                                    </x:DropDownList>
                                      <x:Label ID="Label1" runat="server" Label="Label" Text=" " Width="20px">
                                      </x:Label><%--这是空行--%>
                                    <x:Label ID="AgencyID" Width="60px" runat="server" CssClass="marginr" ShowLabel="true" Text="所属机构"></x:Label>
                                    <x:DropDownList ID="DropDownListAgency" ShowLabel="false" AutoPostBack="true" runat="server" EnableEdit="false">
                                   <x:ListItem Text ="全部" Value="0" />
                                    </x:DropDownList>
                                 
                                     <x:Button ID="Select" Text="搜索" Type="Submit" Icon="SystemSearch"  runat="server" OnClick="Select_Click">
                                     </x:Button>     
                                     <x:Button ID="btnRefresh" runat="server" Icon="ArrowRotateClockwise"  Text="刷新" OnClick="btnRefresh_Click">  
                                    </x:Button>                           
                                     <x:Button ID="btnAddFile" runat="server" EnablePostBack="false"  Icon="Add" Text="新增文件">
                                     </x:Button>
                                     <x:Button ID="btnDelete" Text="删除选中行" Icon="Delete" ConfirmText="确定删除？" Enabled="false"  runat="server" OnClick="btnDelete_Click">
                                     </x:Button>
                                 <x:Button ID="ButtonUpdate" Text="编辑选中行"  Icon="Pencil"   runat="server" OnClick="ButtonUpdate_Click" >
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
              <x:TemplateField Width="30px">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%#RowNumber( Container.DataItemIndex + 1) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>
                            <x:CheckBoxField ID="CBoxSelect" CommandName="CBSelect" DataField="fileid"  runat="server" AutoPostBack="true" Enabled="true" RenderAsStaticField="false" Width="30" />
                            <x:BoundField Enabled="true" DataField="EntryPerson" SortField="EntryPerson" Hidden="true" /> 
                            <x:BoundField DataField="FileName" SortField="FileName" Width="100px" HeaderText="文件名" />
         
                          <x:BoundField DataField="AndUnit" SortField="AndUnit" Width="100px" HeaderText="文件发放部门" />
             <x:BoundField DataField="FileType" SortField="FileType" Width="100px" HeaderText="文件分类" />
            
               <x:BoundField DataField="Time" SortField="Time" Width="100px" HeaderText="文件发放时间" DataFormatString="{0:yyyy-MM-dd}" />
               <x:BoundField DataField="recipient" SortField="recipient" Width="100px" HeaderText="文件接收人" />
                       
                                 <x:TemplateField Width="80px" HeaderText="保密级别">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# ChangeSecrecyLevel(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "SecrecyLevel"))) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>          
                           <x:TemplateField HeaderText="相关文档" Width="60px" >
                           <ItemTemplate>
                        <a href="javascript:<%# GetEditUrl(Eval("WithinPostID")) %>">操作</a>
                          </ItemTemplate>
                          </x:TemplateField> 
                        </Columns>
       </x:Grid>
       </Items> 
       </x:Panel>
          <x:Window ID="Window_add" Popup="false" EnableIFrame="true" runat="server" 
                          EnableMaximize="false" EnableMinimize="false"  EnableResize="false" Height="400px" Width="450px" Title="添加">
        </x:Window>
         <x:Window ID="Window_Update" Popup="false" EnableIFrame="true" runat="server"
             EnableMaximize="false" EnableMinimize="false"  EnableResize="false" Height="400px" Width="450px" Title="添加">
        </x:Window>
          <x:Window ID="DownLoad" Popup="false" EnableIFrame="true"  runat="server"
            EnableMaximize="false" EnableMinimize="false"  EnableResize="false" Height="250px" Width="350px" Title="相关文件操作">
        </x:Window>
        <x:Label ID="labResult" Visible="false" runat="server">
        </x:Label>
    </form>
</body>
</html>

