<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search_FutherStudy.aspx.cs" Inherits="WebApplication1.查询进修学习_接受_" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    
<head runat="server">
    <title></title>
    <script>
        function doclick() {
            var btn = document.getElementById("ButtonUpdate");
            btn.click();
        }
    </script>

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

         <x:Grid ID="Grid_FurtherStudy" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                        EnableCheckBoxSelect="false" EnableRowNumber="false" DataKeyNames="FutherStudyID"
                        AllowSorting="true" SortColumnIndex="0" AutoPostBack="true" SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" OnRowCommand="Grid_FurtherStudy_RowCommand" OnPageIndexChange="Grid_FurtherStudy_PageIndexChange">
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
                                        <x:DropDownList ID="ddl_PeopleName" Enabled="false" EnableEdit="true" ForceSelection="true" ShowLabel="false" AutoPostBack="true" runat="server" Width ="100px" >
                    
                </x:DropDownList>
                                    <x:TextBox ID="tb_content" Width="50px" runat="server" Enabled="false"></x:TextBox>
                                    <x:Label ID="lb_nu" runat="server" Width="15px"></x:Label>
                                      <x:Button ID="Select" Text="搜索" Type="Submit" Icon="SystemSearch"  runat="server" OnClick="Select_Click" >
                                     </x:Button>    
                                    <x:Button ID="btnRefresh" runat="server" Icon="ArrowRotateClockwise"  Text="刷新" OnClick="btnRefresh_Click"  >  
                                    </x:Button>
                                    <x:Button ID="btnAddFutherStudy" Text="新增进修学习信息(接受)" Icon="Add" EnablePostBack="true" runat="server">
                                    </x:Button>
                                    <x:Button ID="btnDelete" Text="删除选中行" Icon="Delete" ConfirmText="确定删除？" EnablePostBack="true" runat="server" OnClick="btnDelete_Click">
                                    </x:Button>  
                                     <x:Button ID="ButtonUpdate" OnClick="ButtonUpdate_Click" Text="编辑选中行" Icon="Pencil" runat="server">
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
                                <x:ListItem Text="20" Value="20" />
                                <x:ListItem Text="30" Value="30" />
                                <x:ListItem Text="50" Value="50" />
                            </x:DropDownList>
                        </PageItems>

                        <Columns>
                            <x:BoundField Enabled="true" DataField="EntryPerson" SortField="EntryPerson" Hidden="true" />
                        <x:TemplateField Width="30px">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%#RowNumber( Container.DataItemIndex + 1) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField> 
                            <%--<x:BoundField DataField="UnitInspectID" SortField="UnitInspectID" Width="100px" HeaderText="ID" />--%>
                            <x:CheckBoxField  ID="CBoxSelect" CommandName="CBSelect" DataField="FutherID" runat="server" AutoPostBack="true" Enabled="true" RenderAsStaticField="false" Width="30" />
                            <x:BoundField DataField="Name" SortField="Name" Width="100px" HeaderText="姓名" />
                             <x:TemplateField Width="80px" HeaderText="性别">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# getgender(Convert.ToString(DataBinder.Eval(Container.DataItem, "Sex"))) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField> 
                            <x:BoundField DataField="Birthday" SortField="Birthday" Width="150px" DataFormatString="{0:yyyy-MM-dd}" HeaderText="出生年月" />
                            <x:BoundField DataField="Hometown" SortField="Hometown" Width="150px" HeaderText="籍贯" />
                            <x:BoundField DataField="PhoneNum" SortField="PhoneNum" Width="150px" HeaderText="联系电话" />
                            <x:BoundField DataField="Email" SortField="Email" Width="150px" HeaderText="电子邮箱" />
                            <x:BoundField DataField="DocuType" SortField="DocuType" Width="100px" HeaderText="证件类型" />
                            <x:BoundField DataField="IDNum" SortField="IDNum" Width="150px" HeaderText="证件号码" />   
                            <x:BoundField DataField="LearnPlace" SortField="LearnPlace" Width="150px" HeaderText="进修地点" />
                            <x:BoundField DataField="LearnSchool" SortField="LearnSchool" Width="150px" HeaderText="进修学校" />                       
                            <x:BoundField DataField="LearnBeginTime" SortField="LearnBeginTime" Width="150px" DataFormatString="{0:yyyy-MM-dd}" HeaderText="进修开始时间" />
                            <x:BoundField DataField="LearnEndTime" SortField="LearnEndTime" Width="150px" DataFormatString="{0:yyyy-MM-dd}" HeaderText="进修结束时间"  />
                            <x:BoundField DataField="LearnContent" SortField="LearnContent" Width="150px" HeaderText="进修内容" />      
                          
                          
                           <x:TemplateField Width="80px" HeaderText="进修部门">
                    <ItemTemplate>                        
                        <asp:Label ID="Label16" runat="server" Text='<%#AgencyName (Convert.ToInt32( DataBinder.Eval(Container.DataItem, "AgencyID"))) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>   
                            <x:TemplateField Width="80px" HeaderText="保密级别">
                            <ItemTemplate>                        
                        <asp:Label ID="Label1" runat="server" Text='<%#SecrecyLevel (Convert.ToInt32( DataBinder.Eval(Container.DataItem, "SecrecyLevel"))) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>  
                             <x:TemplateField HeaderText="个人简介"  Width="60px" >
                           <ItemTemplate>
                        <a href="javascript:<%# GetEditUrlp(Eval("FutherStudyID")) %>">详情</a>
                          </ItemTemplate>
                          </x:TemplateField> 
                              <x:TemplateField HeaderText="备注"  Width="60px" >
                           <ItemTemplate>
                        <a href="javascript:<%# GetEditUrlc(Eval("FutherStudyID")) %>">详情</a>
                          </ItemTemplate>
                          </x:TemplateField>  
                        </Columns>
                    </x:Grid>
       </Items>
       </x:Panel>
         <x:Window ID="Window_addFutherStudy" Popup="false" EnableIFrame="true" IFrameUrl="Add_FutherStudy.aspx" runat="server"
            EnableMaximize="false" EnableResize="false" Height="450px" Width="800px" Title="添加">
        </x:Window>

        <x:Window ID="Window_UpdateFutherStudy" Popup="false" EnableIFrame="true" IFrameUrl="Update_FutherStudy.aspx" runat="server"
            EnableMaximize="false" EnableResize="false" Height="450px" Width="800px" Title="更新">
        </x:Window>
        <x:Window ID="Personfile" Popup="false" EnableIFrame="true"  runat="server"
            EnableMaximize="false" EnableMinimize="false"  EnableResize="false" Height="250px" Width="350px" Title="个人简介">
        </x:Window>
           <x:Window ID="Contents" Popup="false" EnableIFrame="true"  runat="server"
            EnableMaximize="false" EnableMinimize="false"  EnableResize="false" Height="250px" Width="350px" Title="备注">
        </x:Window>
        <x:Label ID="labResult" Visible="false" runat="server">
        </x:Label>
    </form>
</body>
</html>
