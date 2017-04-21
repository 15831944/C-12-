<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search_DFurtherStudy.aspx.cs" Inherits="WebApplication1.Search_DFurtherStudy" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body oncontextmenu='return false'><%--取消鼠标右键的点击--%>
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server"/>
        <x:Panel ID="Panel1" runat="server" BodyPadding="5px" EnableBackgroundColor="true"
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start"
            ShowHeader="false" Title="用户管理">
        <Items>        
         <x:Grid ID="Grid_FurtherStudy" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                        EnableCheckBoxSelect="false"  DataKeyNames="DFurtherStudyID"
                        AllowSorting="true" SortColumnIndex="0" SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" 
            EnableRowClick="true"  OnPageIndexChange="Grid_FurtherStudy_PageIndexChange" OnRowCommand="Grid_FurtherStudy_RowCommand1">
                        <%--OnPageIndexChange="Grid_Info_PageIndexChange"--%>
                        <Toolbars>
                            <x:Toolbar ID="Toolbar_top" runat="server" >
                                <Items>
                                   <x:Label ID="lYear" Width="60px" runat="server" CssClass="marginr" ShowLabel="false" Text="查询条件：">
                                        </x:Label>   
                                    <x:DropDownList ID="ddl_search" ShowLabel="false" AutoPostBack="true" runat="server" EnableEdit="false" OnSelectedIndexChanged="ddl_search_SelectedIndexChanged">
                                   <x:ListItem Text ="全部" Value="0" />
                                   <x:ListItem Text ="年份" Value="1" />
                                   <x:ListItem Text ="姓名" Value="2" />
                                    </x:DropDownList>                        
                                       <x:DropDownList ID="dCondition" Width="100px" ShowLabel="false" AutoPostBack="true" EnableEdit="true" runat="server" >                                                        
                                    </x:DropDownList>
                                    <x:TextBox ID="tb_content" Width="50px" runat="server" Enabled="false"></x:TextBox>
                                      <x:Button ID="Select" Text="搜索" Type="Submit" Icon="SystemSearch"  runat="server" OnClick="Select_Click">
                                     </x:Button>    
                                    <x:Button ID="btnRefresh" runat="server" Icon="ArrowRotateClockwise"  Text="刷新"  OnClick="btnRefresh_Click">  
                                    </x:Button>
                                    <x:Button ID="btnAddDFutherStudy" Text="新增进修学习信息(派遣)" Icon="Add" EnablePostBack="true" runat="server">
                                    </x:Button>

                                     <x:Button ID="btnSelect_All" runat="server" Text="全    选" OnClick="btnSelect_All_Click"></x:Button>
                                    <x:Button ID="btnDelete" Text="删除选中行" Icon="Delete" ConfirmText="确定删除？" Enabled="false" EnablePostBack="true" runat="server" OnClick="btnDelete_Click">
                                    </x:Button>
                                  <x:Button ID="btnUpdate" Text="编辑选中行" Icon="Pencil"  EnablePostBack="true"  runat="server" OnClick="btnUpdate_Click"   >
                                  </x:Button>  
                                     <x:Button ID="btn_Get" Text="导出Excel文件" ConfirmText="确定导出？" Icon="Disk" EnablePostBack="true" runat="server" EnableAjax="false" OnClick="btn_Get_Click"  DisableControlBeforePostBack="false">
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
                                <x:ListItem Text="20" Value="20" Selected="true"/>
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
                        <asp:Label ID="Label4" runat="server" Text='<%#RowNumber( Container.DataItemIndex + 1) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>              
                             <x:TemplateField Width="80px" HeaderText="人员姓名">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# UserName(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "UserInfoID"))) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>                        
                            <x:BoundField DataField="StudyPlace" SortField="StudyPlace" Width="150px" HeaderText="进修地点" />
                            <x:BoundField DataField="StudySchool" SortField="StudySchool" Width="150px" HeaderText="进修学校" />
                            <x:BoundField DataField="StudyContent" SortField="StudyContent" Width="150px" HeaderText="进修内容" />
                            <x:BoundField DataField="DBegainTime" SortField="DBegainTime" Width="150px" DataFormatString="{0:yyyy-MM-dd}" HeaderText="进修开始时间" />
                            <x:BoundField DataField="DEndTime" SortField="DEndTime" Width="150px" DataFormatString="{0:yyyy-MM-dd}" HeaderText="进修结束时间" />
                             <x:TemplateField Width="80px" HeaderText="保密级别">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# ChangeSecrecyLevel(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "SecrecyLevel"))) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>          
                          
                        </Columns>
                    </x:Grid>
       </Items>
       </x:Panel>
        <x:Window ID="Window_addDFutherStudy" Popup="false" EnableIFrame="true" runat="server"
             EnableMaximize="false" EnableMinimize="false"  EnableResize="false" Height="450px" Width="450px" Title="添加"  EnableConfirmOnClose="true">
        </x:Window>
         <x:Window ID="Window_UpdateDFutherStudy" Popup="false" EnableIFrame="true" runat="server"
             EnableMaximize="false" EnableMinimize="false"  EnableResize="false" Height="450px" Width="450px" Title="编辑"   EnableConfirmOnClose="true">
        </x:Window>

      
        <x:Label ID="labResult" Visible="false" runat="server">
        </x:Label>
    </form>
</body>
</html>
