<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Award.aspx.cs" Inherits="WebApplication1.Award" %>

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
            ShowHeader="false" >
        <Items>        
          

         <x:Grid ID="Grid_Award" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                        DataKeyNames="AwardID "
                        AllowSorting="true" SortColumnIndex="0" SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" AutoPostBack="true" OnRowCommand="Grid_Award_RowCommand" OnPageIndexChange="Grid_Award_PageIndexChange" >
                        <%--OnPageIndexChange="Grid_Info_PageIndexChange"--%>
                        <Toolbars>
                            <x:Toolbar ID="Toolbar_top" runat="server" >
                                <Items>
                                     <x:Label ID="AgencyID" Width="60px" runat="server" CssClass="marginr" ShowLabel="true" Text="查询条件"></x:Label>
                                    <x:DropDownList ID="dChoose" Width="100px" ShowLabel="false" AutoPostBack="true" runat="server" OnSelectedIndexChanged="dChoose_SelectedIndexChanged">
                                   <x:ListItem Text="全部" Value="全部"/>
                                   
                                    <%--<x:ListItem Text="全部获奖人" Value="全部获奖人" /> --%>
                                   <x:ListItem Text="获奖等级" Value="获奖等级" /> 
                                    <x:ListItem Text="获奖部门" Value="获奖部门" /> 
                                    <x:ListItem Text="成果名称" Value="成果名称" />
                                    <x:ListItem Text="获奖名称" Value="获奖名称" />
                                    <x:ListItem Text="颁奖部门" Value="颁奖部门" />                                     
                                    <x:ListItem Text="获奖年份" Value="获奖年份" />
                                    <x:ListItem Text="保密级别" Value="保密级别" /> 
                                    <x:ListItem Text="成员及排序" Value="成员及排序" />                                                                                                                                                   
                                    </x:DropDownList>
                                        <x:Label ID="Label1" runat="server" Label="Label" Text=" " Width="5px" >
                            </x:Label><%--这是空行--%>
                                   <x:TextBox ID="tCondition" Enabled="false" ShowLabel="true" MaxLength="40" MaxLengthMessage="最多可输入40个字符"   EmptyText="请输入搜索条件"   Width="100px" CssClass="marginr" runat="server"  AutoPostBack="true"  >
                                        </x:TextBox>
                                        <x:Label ID="Label3" runat="server" Label="Label" Text=" " Width="5px" >
                            </x:Label><%--这是空行--%>
                                      <x:DropDownList ID="dCondition" Enabled="false" Width="100px" EnableEdit="true" ShowLabel="false" AutoPostBack="true" runat="server" >                                                        
                                    </x:DropDownList>
                                     <x:Button ID="Select" Text="搜索" Type="Submit" Icon="SystemSearch"  runat="server" OnClick="Select_Click">
                                     </x:Button>     
                                     <x:Button ID="btnRefresh" runat="server" Icon="ArrowRotateClockwise"  Text="刷新"  OnClick="btnRefresh_Click">  
                                    </x:Button>                             
                                    <x:Button ID="btnAddAward" Text="新增成果获奖信息" Icon="Add" EnablePostBack="true" runat="server">
                                    </x:Button>
                                <%--    <x:Button ID="btnDelete" Text="删除选中信息" Icon="Delete" EnablePostBack="true" runat="server" OnClick="btnDelete_Click">
                                    </x:Button>--%>
                                  <%--  <x:Button ID="Get2" Text="导出所选信息" Icon="Disk" EnablePostBack="true" runat="server">
                                    </x:Button>--%>
                                    <x:Button ID="btnReviseAward" Text="编辑选中行" Icon="BulletEdit" EnablePostBack="true" runat="server" OnClick="btnReviseAward_Click">
                                    </x:Button>
                                     <%--<x:Button ID="btnDelete" Text="删除获奖信息" Icon="Add" EnablePostBack="true" runat="server">
                                    </x:Button>--%>

                                      <x:Button ID="btnSelect_All" runat="server" Text="全选" OnClick="btnSelect_All_Click"></x:Button>
                                    <x:Button ID="btnDelete" Text="删除获奖信息" Icon="Delete" EnablePostBack="true" runat="server" OnClick="btnDelete_Click"
                                    ConfirmText="确定删除？" ConfirmTarget="Top" Enabled="false">
                                </x:Button>
                                      <x:Button ID="btn_Get" Text="导出Excel文件" ConfirmText="确定导出？" Icon="Disk" EnablePostBack="true" runat="server" OnClick="btn_Get_Click" EnableAjax="false" DisableControlBeforePostBack="false">
                                    </x:Button>
                                      <x:Button ID="reprot" Text="报表"  Icon="Report" EnablePostBack="true" runat="server" >
                                         <Menu ID="Menu1" runat="server">
                                    <x:MenuButton ID="reprot1" runat="server" Text="分部门按获奖名称、获奖时间统计获奖情况">
                                    </x:MenuButton>       
                                               <x:MenuButton ID="reprot2" runat="server" Text="分部门按项目、项目级别统计成果获奖情况">
                                    </x:MenuButton>                                
                                </Menu>
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
                        <asp:Label ID="Label6" runat="server" Text='<%#RowNumber( Container.DataItemIndex + 1) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>
                            <x:CheckBoxField ID="CBoxSelect" CommandName="CBSelect" DataField="insepctid"  runat="server" AutoPostBack="true" Enabled="true" RenderAsStaticField="false" Width="30" />
                            <x:BoundField Enabled="true" DataField="EntryPerson" SortField="EntryPerson" Hidden="true" /> 
                               <x:TemplateField Width="30px" Hidden="true">
                    <ItemTemplate>
                        <asp:Label ID="Label7" runat="server" Text='<%#RowNumber( Container.DataItemIndex + 1) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>
                             <x:BoundField DataField="Acheivement" SortField="Acheivement" Width="150px" HeaderText="成果名称" />                                                         
                            <x:BoundField DataField="AwardwSpecies" SortField="AwardwSpecies" Width="100px" HeaderText="奖励种类" />         
                            <x:BoundField DataField="Grade" SortField="Grade" Width="150px" HeaderText="等级" />
                            <x:BoundField DataField="GivAgency" SortField="GivAgency" Width="150px" HeaderText="授予机构" />
                            <x:BoundField DataField="AwardTime" SortField="AwardTime" Width="150px" HeaderText="获奖时间" DataFormatString="{0:yyyy-MM-dd}"/>
                             <x:BoundField DataField="AwardForm" SortField="AwardForm" Width="150px" HeaderText="获奖类型" />
                             <x:BoundField DataField="AwardNum" SortField="AwardNum" Width="150px" HeaderText="获奖证书号" />
                             <x:BoundField DataField="FirstAward" SortField="FirstAward" Width="150px" HeaderText="第一获奖人" />
                             <x:BoundField DataField="AwardName" SortField="AwardName" Width="150px" HeaderText="获奖名称" />
                             <x:BoundField DataField="AwardPeople" SortField="AwardPeople" Width="150px" HeaderText="成员及排序" />
                            <%--  <x:TemplateField Width="80px" HeaderText="成果名称">
                    <ItemTemplate>
                        <asp:Label ID="LabeAgency" runat="server" Text='<%# FindName(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "Acheivement"))) %>'></asp:Label>
                    </ItemTemplate> 
                </x:TemplateField> --%>    
                              <x:TemplateField Width="80px" HeaderText="保密级别">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# ChangeSecrecyLevel(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "SecrecyLevel"))) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>  
                               <x:TemplateField Width="80px" HeaderText="成果名称" Hidden="true">
                    <ItemTemplate>
                        <asp:Label ID="Label8" runat="server" Text='<%# getachievement(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "AwardID"))) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>       
                             <x:TemplateField Width="80px" HeaderText="全部获奖人" Hidden="true">
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# getpeople(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "AwardID"))) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>      
                             <x:TemplateField Width="80px" HeaderText="单位" Hidden="true">
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# getunit(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "AwardID"))) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField> 
                               <x:BoundField DataField="Remark" SortField="Remark" Hidden="true" Width="150px" HeaderText="备注"/> 
                            <%-- <x:TemplateField HeaderText="获奖名称" Width="60px" >
                           <ItemTemplate>
                        <a href="javascript:<%# GetEditUrla(Eval("AwardID ")) %>">详情</a>
                          </ItemTemplate>
                          </x:TemplateField>  --%>
                            <%-- <x:TemplateField HeaderText="全部获奖人" Width="60px" >
                           <ItemTemplate>
                        <a href="javascript:<%# GetEditUrlp(Eval("AwardID ")) %>">详情</a>
                          </ItemTemplate>
                          </x:TemplateField> --%>
                           <%-- <x:TemplateField HeaderText="成员及排序" Width="80px" >
                           <ItemTemplate>
                        <a href="javascript:<%# GetEditUrlf(Eval("AwardID ")) %>">详情</a>
                          </ItemTemplate>
                          </x:TemplateField> --%>    
                             <x:TemplateField HeaderText="单位" Width="60px" >
                           <ItemTemplate>
                        <a href="javascript:<%# GetEditUrlw(Eval("AwardID ")) %>">详情</a>
                          </ItemTemplate>
                          </x:TemplateField>  
                             <x:TemplateField HeaderText="备注" Width="60px" >
                           <ItemTemplate>
                        <a href="javascript:<%# GetEditUrl(Eval("AwardID ")) %>">详情</a>
                          </ItemTemplate>
                          </x:TemplateField>             
                              <x:TemplateField HeaderText="相关文档"  Width="60px" >
                           <ItemTemplate>
                        <a href="javascript:<%# GetEditUrlu(Eval("AwardID")) %>">操作</a>
                          </ItemTemplate>
                          </x:TemplateField> 
                        </Columns>
                    </x:Grid>
       </Items>
       </x:Panel> 
        <x:Window ID="Window_addAward" Popup="false" EnableIFrame="true"  runat="server"
             EnableMaximize="false" EnableMinimize="false"  EnableResize="false" Height="510px" Width="800px" Title="添加成果获奖信息">
        </x:Window>

        <x:Window ID="Window_ReviseAward" Popup="false" EnableIFrame="true"  runat="server"
             EnableMaximize="false" EnableMinimize="false"  EnableResize="false" Height="510px" Width="800px" Title="修改成果获奖信息">
        </x:Window>
        <x:Window ID="Remark" Popup="false" EnableIFrame="true"  runat="server"
             EnableMaximize="false" EnableResize="false" Height="250px" Width="350px" >
        </x:Window>
        <x:Window ID="Unit" Popup="false" EnableIFrame="true"  runat="server"
             EnableMaximize="false" EnableMinimize="false"  EnableResize="false" Height="250px" Width="350px" >
        </x:Window>
          <x:Window ID="DownLoad" Popup="false" EnableIFrame="true"  runat="server"
             EnableMaximize="false" EnableMinimize="false"  EnableResize="false" Height="250px" Width="350px" Title="相关文件操作">
        </x:Window>
        <x:Window ID="WindowReport" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="true" EnableResize="false" Height="450px" Width="800px">
        </x:Window>
        <x:Label ID="labResult" Visible="false" runat="server">
        </x:Label>
    </form>
</body>
</html>
