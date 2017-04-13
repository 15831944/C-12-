<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgencyNavigate.aspx.cs" Inherits="WDFramework.Agencies.AgencyNavigate" %>


<%@ Register assembly="FineUI" namespace="FineUI" tagprefix="x" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body oncontextmenu='return false'>
    <form id="form1" runat="server">
    <x:PageManager ID="PageManager1" AutoSizePanelID="RegionPanel1" runat="server" />
    <x:RegionPanel ID="RegionPanel1" ShowBorder="false" runat="server">
        <Regions>
            <x:Region ID="Region1" ShowBorder="false" ShowHeader="false" Split="true" Title ="机构名称" 
                 Margins="0 0 0 0" Width="200px" Position="Left" Layout="Fit"
                BodyPadding="5px 0 5px 5px" EnableBackgroundColor="true" runat="server">
                <Items>
                    <x:Accordion ID="Accordion1" runat="server" ShowBorder="false" ShowHeader="false" ShowCollapseTool="true">
                        <Panes>
                    <x:AccordionPane ID="Population" runat="server" Title="总体" BodyPadding="2px 5px"
                                    Layout="Fit" ShowBorder="false">
                                   <Items>
                                       <x:Tree runat="server" ShowBorder="false" ShowHeader="false" ID="TrePopulation" OnNodeCommand="Tree1_NodeCommand">
                                       </x:Tree>
                                   </Items>
                                </x:AccordionPane>
                    <x:AccordionPane ID="AccordionPaneinside" runat="server" Title="内部" BodyPadding="2px 5px"
                                    Layout="Fit" ShowBorder="false">
                                   <Items>
                                       <x:Tree runat="server" ShowBorder="false" ShowHeader="false" ID="TreInside" OnNodeCommand="Tree1_NodeCommand">
                                       </x:Tree>
                                   </Items>
                                </x:AccordionPane>
                        </Panes>
                    </x:Accordion>

                   <%--<x:Grid ID="GridAgency" runat="server" ShowBorder="true" ShowHeader="false" EnableCheckBoxSelect="false" 
                        EnableRowNumber="true" DataKeyNames="AgencyID" AllowSorting="true" AutoPostBack="true" 
                        SortColumnIndex="0" SortDirection="DESC" AllowPaging="false" EnableMultiSelect="false"
                        OnRowClick="GridAgency_RowClick" EnableRowClick="true" >
                    <Columns>
                            <x:BoundField DataField="AgencyName" SortField="AgencyName" ExpandUnusedSpace="true" 
                                HeaderText="机构名称" />  
                      </Columns>   
                    </x:Grid>--%>
                </Items>
            </x:Region>
            <x:Region ID="Region2" ShowBorder="false" ShowHeader="false" Position="Center" Layout="VBox"
                BoxConfigAlign="Stretch" BoxConfigPosition="Left" BodyPadding="5px 5px 5px 0"
                EnableBackgroundColor="true" runat="server">
                <Items>
<%--  --%>     <x:Panel ID="Panel1" runat="server"   ShowBorder="True"  EnableCollapse="true"
                Layout="Column"   BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5"
                BoxConfigChildMargin="0 5 0 0" ShowHeader="false">
                <Items>
                    <x:Toolbar ID="Toolbar1" runat="server" ColumnWidth="100%" >
                        <Items>
                            <x:Button ID="Add" Text="新增机构" Icon="Add" EnablePostBack="false" runat="server">
                            </x:Button>
                            <x:Button ID="Change" Text="修改机构信息" Icon="BulletEdit" runat="server" OnClick="Change_Click">
                            </x:Button>
                            <x:Button ID="Delete" Text="删除所选机构" Icon="Delete" ConfirmText="确定删除？" ConfirmTarget="Top" runat="server" OnClick="Delete_Click">
                            </x:Button>
                            <x:Button ID="btnRefresh" runat="server" Icon="ArrowRotateClockwise"  Text="刷新" OnClick="btnRefresh_Click">  
                                    </x:Button> 
                        </Items>
                    </x:Toolbar>
                </Items>
                </x:Panel>
               <x:Panel ID="Panel2" runat="server" Height="250px"  ShowBorder="True"  EnableCollapse="true" AutoScroll="true" 
                Layout="Column"    BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5"
                BoxConfigChildMargin="0 5 0 0" ShowHeader="false">
                    
                <Items>

   
                <x:Panel ID="Panel3" Title="项目概要" BoxFlex="1" runat="server"  ColumnWidth ="50%"
                 BodyPadding="20px" ShowBorder="false"  ShowHeader="false">
                       <Items>

                           <x:Panel ID="Panel13" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                              Layout="Column" runat="server">
                                        <Items>

                                        <x:Label ID="AgencyName"  Width="120px" runat="server" ShowLabel="false" Text="机构名称：">
                                        </x:Label>    
                                        <x:TextBox ID="AgencyName2" ShowLabel="false"  Width="300px" CssClass="marginr" runat="server" Readonly ="true">
                                        </x:TextBox>                      

                                        </Items>                
                            </x:Panel>

                           <x:Label ID="Label1" runat="server" Label="Label" Text=" " Height ="20px" >
                           </x:Label><%--这是空行--%>

                           <x:Panel ID="Panel12" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                              Layout="Column" runat="server">
                                        <Items>

                                        <x:Label ID="ParentID" Width="120px" runat="server"  ShowLabel="false" Text="上级机构：">
                                        </x:Label>
                                        <x:TextBox ID="ParentID2" ShowLabel="false"  Width="300px" CssClass="marginr" runat="server" Readonly ="true">
                                        </x:TextBox>
                                                                    
                                        </Items>                
                            </x:Panel>
                            

                            <x:Label ID="Label4" runat="server" Label="Label" Text=" " Height ="20px" >
                            </x:Label><%--这是空行--%>

                            <x:Panel ID="Panel14"  ShowHeader="false" CssClass="formitem" ShowBorder="false"
                              Layout="Column" runat="server">
                                        <Items>

                                        <x:Label ID="SecrecyLevel"   Width ="120px" runat="server"  ShowLabel="false" Text="保密级别：">
                                        </x:Label>                        
                                        <x:TextBox ID="SecrecyLevel2" ShowLabel="false"  Width="300px" CssClass="marginr" runat="server" Readonly ="true">
                                        </x:TextBox>

                                        </Items>                
                            </x:Panel>

                            <x:Label ID="Label15" runat="server" Label="Label" Text=" " Height ="20px" >
                            </x:Label><%--这是空行--%>

                            <x:Panel ID="Panel7"  ShowHeader="false" CssClass="formitem" ShowBorder="false"
                              Layout="Column" runat="server">
                                        <Items>

                                        <x:Label ID="AgencyHeads"  Width="120px" runat="server"  ShowLabel="false" Text="机构负责人：">
                                        </x:Label>
                                        <x:TextBox ID="AgencyHeads2" ShowLabel="false"  Width="300px" CssClass="marginr" runat="server" Readonly ="true">
                                        </x:TextBox>

                                        </Items>                
                            </x:Panel>

                           <x:Label ID="Label12" runat="server" Label="Label" Text=" " Height ="20px" >
                           </x:Label><%--这是空行--%>

                            <x:Panel ID="Panel8"  ShowHeader="false" CssClass="formitem" ShowBorder="false"
                              Layout="Column" runat="server">
                                        <Items>

                                        <x:Label ID="Research" Width="120px" runat="server"  ShowLabel="false" Text="研究方向：">
                                        </x:Label>      
                                        <x:TextBox ID="Research2" ShowLabel="false" Width="300px" CssClass="marginr" runat="server" Readonly ="true">
                                        </x:TextBox>

                                        </Items>                
                            </x:Panel>

                       </Items>               
                 </x:Panel>           
                 


                <x:Panel ID="Panel4" Title="项目概要" BoxFlex="1" runat="server" ColumnWidth ="50%"
                 BodyPadding="20px" ShowBorder="false" ShowHeader="false">     

                        <Items>

                            <x:Panel ID="Panel22" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                              Layout="Column" runat="server">
                                        <Items>
                                            
                                        <x:Label ID="AgencyNumber"  Width="120px" runat="server"  ShowLabel="false" Text="机构分类：">
                                        </x:Label>  
                                        <x:TextBox ID="AgencyNumber2" ShowLabel="false"  Width="300px" CssClass="marginr" runat="server" Readonly ="true">
                                        </x:TextBox>                       
                                       
                                        </Items>                
                            </x:Panel>

                            <x:Label ID="Label8" runat="server" Label="Label" Text=" " Height ="20px" >
                            </x:Label><%--这是空行--%>

                            <x:Panel ID="Panel23" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                              Layout="Column" runat="server">
                                        <Items>

                                        <x:Label ID="FullTimeNumbers"  Width="120px" runat="server" ShowLabel="false" Text="专职人数：">
                                        </x:Label>   
                                        <x:TextBox ID="FullTimeNumber2" ShowLabel="false"  Width="300px" CssClass="marginr" runat="server" Readonly ="true">
                                        </x:TextBox>

                                        </Items>                
                            </x:Panel>

                            <x:Label ID="Label10" runat="server" Label="Label" Text=" " Height ="20px" >
                            </x:Label><%--这是空行--%>

                            <x:Panel ID="Panel24" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                              Layout="Column" runat="server">
                                        <Items>

                                        <x:Label ID="PartTimeNumber"  Width="120px" runat="server"  ShowLabel="false" Text="兼职人数：">
                                        </x:Label>
                                        <x:TextBox ID="PartTimeNumber2" ShowLabel="false"  Width="300px" CssClass="marginr" runat="server" Readonly ="true">
                                        </x:TextBox> 

                                        </Items>                
                            </x:Panel>

                            <x:Label ID="Label9" runat="server" Label="Label" Text=" " Height ="20px" >
                            </x:Label><%--这是空行--%>
                           
                            <x:Panel ID="Panel17" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                              Layout="Column" runat="server">
                                        <Items>

                                        <x:Label ID="Area"  Width="120px" runat="server"  ShowLabel="false" Text="面积：">
                                        </x:Label>
                                        <x:TextBox ID="Area2" ShowLabel="false"  Width="300px" CssClass="marginr" runat="server" Readonly ="true">
                                        </x:TextBox>   

                                        </Items>                
                            </x:Panel>

                            <x:Label ID="Label11" runat="server" Label="Label" Text=" " Height ="20px" >
                            </x:Label><%--这是空行--%>

                             <x:Panel ID="Panel18" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                              Layout="Column" runat="server">
                                        <Items>

                                        <x:Label ID="Location"  Width="120px" runat="server"  ShowLabel="false" Text="地点：">
                                        </x:Label> 
                                        <x:TextBox ID="Location2" ShowLabel="false"  Width="300px" CssClass="marginr" runat="server" Readonly ="true">
                                        </x:TextBox>    

                                        </Items>                
                            </x:Panel>
                            
                            </Items>               
                            </x:Panel>   
                                     
                                 
                    </Items>        
                    </x:Panel>
                    <%--  --%>
                    
                    <x:Grid ID="Grid_Info" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="true"  Title ="人员信息"
                         EnableRowNumber="true" DataKeyNames="UserInfoID" 
                        AllowSorting="true" SortColumnIndex="0" SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" OnPageIndexChange="People_Info_PageIndexChange" >
                    

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
                            <x:BoundField DataField="UserName" SortField="UserName"  HeaderText="人员姓名" />
                            
                            <x:TemplateField Width="80px" HeaderText="性别">
                    <ItemTemplate>
                        <%-- Container.DataItem 的类型是 System.Data.DataRowView 或者用户自定义类型 --%>
                        <%--<asp:Label ID="Label2" runat="server" Text='<%# GetGender(DataBinder.Eval(Container.DataItem, "Gender")) %>'></asp:Label>--%>
                        <asp:Label ID="Label3" runat="server" Text='<%# getgender(Convert.ToString(DataBinder.Eval(Container.DataItem, "Sex"))) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>
                            <x:BoundField DataField="Education" SortField="Education" HeaderText="学历" />

                            <x:BoundField DataField="Degree" SortField="Degree" HeaderText="学位" />
                            <x:BoundField DataField="Email" SortField="Email"  HeaderText="邮箱" />
                            <x:BoundField DataField="TeleNum" SortField="TeleNum"  HeaderText="电话" />
                        </Columns>
                    </x:Grid>
                </Items>
            </x:Region>
        </Regions>
    </x:RegionPanel>

         <x:Window ID="Window3" Popup="false" EnableIFrame="true" IFrameUrl="WorkAdd.aspx" runat="server"
            EnableMaximize="false" EnableResize="false" Height="450px" Width="800px" Title="添加" OnClose="Window3_Close">
    </x:Window>


    <x:Window ID="Window4" Title="查询" Popup="false" EnableIFrame="true" runat="server" IFrameUrl="ChangeAgency.aspx"
            EnableMaximize="false" EnableResize="false" Target="Parent" 
            IsModal="True" Width="800px" Height="450px" OnClose="Window4_Close">
    </x:Window>
    </form>
</body>
</html>


