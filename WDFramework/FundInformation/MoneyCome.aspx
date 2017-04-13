<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MoneyCome.aspx.cs" Inherits="WDFramework.MoneyCome" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body oncontextmenu='return false'>
    <%--取消鼠标右键的点击--%>
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <%--  --%>
        <x:Panel ID="Panel1" runat="server" BodyPadding="5px" EnableBackgroundColor="true"
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start"
            ShowHeader="false" Title="资金信息">
            <Items>

                <%--  --%>


                <x:Grid ID="gd_UnitAPeople" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                    EnableRowNumber="false" EnableRowNumberPaging="true" EnableTextSelection="true" AutoPostBack="true"
                    DataKeyNames="FundInformationID" AllowSorting="true" SortColumnIndex="0" OnRowCommand="gd_UnitAPeople_RowCommand"
                    SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" OnPageIndexChange="gd_UnitAPeople_PageIndexChange">
                    <Toolbars>
                        <x:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <x:Label ID="Label2" runat="server" Label="Label" Width="15" Text=" "></x:Label>
                                <x:Button ID="btn_ClickIn" Text="登记" Icon="BookEdit" EnablePostBack="true" runat="server">
                                </x:Button>

                                <x:Label ID="Label8" runat="server" Label="Label" Width="15" Text=" "></x:Label>
                                <x:Label ID="Label9" runat="server" Label="Label" Text="查询条件: "></x:Label>
                                <x:Label ID="Label6" runat="server" Label="Label" Width="10" Text=" "></x:Label>

                                <x:DropDownList ID="ddl_UnitALl" ShowLabel="false" AutoPostBack="true" runat="server" Width="100px" OnSelectedIndexChanged="ddl_UnitALl_SelectedIndexChanged">
                                    <x:ListItem Text="来款单位" Value="0" />
                                    <x:ListItem Text="承担部门" Value="1" />
                                    <x:ListItem Text="负责人" Value="2" />
                                    <x:ListItem Text="项目类型" Value="3" />
                                    <x:ListItem Text="项目来源" Value="4" />
                                    <x:ListItem Text="项目" Value="5" />
                                    <x:ListItem Text="全部" Value="6" />
                                </x:DropDownList>

                                <x:Label ID="Label3" runat="server" Label="Label" Width="15" Text=" "></x:Label>
                                <x:Label ID="lb_Change3" runat="server" Label="Label" Text="来款单位名称: "></x:Label>
                                <x:Label ID="Label5" runat="server" Label="Label" Width="10" Text=" "></x:Label>

                                <x:TextBox ID="tb_people" ShowLabel="false" Required="true" AutoPostBack="true" runat="server" Width="100px" OnTextChanged="tb_people_TextChanged"></x:TextBox>
                                

                                <x:Label ID="Label12" runat="server" Label="Label" Width="15" Text=" "></x:Label>
                                <x:Label ID="Lb_Change4" runat="server" Label="Label" Text="提取人： "></x:Label>
                                <x:Label ID="Label4" runat="server" Label="Label" Width="10" Text=" "></x:Label>

                                <x:DropDownList ID="ddl_ProName" runat="server" AutoPostBack="true" ShowLabel="false" Width="100px">

                                </x:DropDownList>
                                

                                <x:Label ID="Label13" runat="server" Label="Label" Width="15" Text=" "></x:Label>
                                <x:Label ID="Lb_Content" runat="server" Label="Label" Text=""></x:Label>
                                <x:Label ID="Label1" runat="server" Label="Label" Width="10" Text=" "></x:Label>


                                <x:Button ID="btn_Find" Text="搜索" Type="Submit" Icon="SystemSearch" EnablePostBack="true" runat="server" OnClick="btn_Find_Click">
                                </x:Button>
                                
                                <x:Button ID="Btn_Refresh" Text="刷新" Icon="ArrowRotateClockwise" EnablePostBack="true" runat="server" OnClick="Btn_Refresh_Click">
                                </x:Button>

                                <x:Button ID="btn_Delete" ConfirmText="确认删除？" Text="删除" Icon="Delete" EnablePostBack="true" runat="server" OnClick="btn_Delete_Click">
                                </x:Button>
                
                                <%--<x:Button ID="btn_Print" Text="打印" Icon="Printer" EnablePostBack="true" runat="server">
                                </x:Button>

                                <x:Button ID="btn_Show" Text="统计" Icon="CupEdit" EnablePostBack="true" runat="server">
                                </x:Button>--%>
                                   <x:Button ID="reprot" Text="报表"  Icon="Report" EnablePostBack="true" runat="server" >
                                         <Menu ID="Menu1" runat="server">
                                    <x:MenuButton ID="reprot1" runat="server" Text="分承担部门按项目统计进账经费">
                                    </x:MenuButton> 
                                              <x:MenuButton ID="reprot2" runat="server" Text="分承担部门按项目负责人统计进账经费">
                                    </x:MenuButton> 
                                              <x:MenuButton ID="reprot3" runat="server" Text="分项目来源按承担部门统计进账经费">
                                    </x:MenuButton>       
                                              <x:MenuButton ID="reprot4" runat="server" Text="分项目类型按承担部门统计进账经费">
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
                            <x:DropDownList ID="ddlGridPageSize" Width="80px" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlGridPageSize_SelectedIndexChanged">
                                <x:ListItem Text="10" Value="10" />
                                <x:ListItem Text="20" Value="20" />
                                <x:ListItem Text="30" Value="30" />
                                <x:ListItem Text="50" Value="50" />
                            </x:DropDownList>
                        </PageItems>
                    <%--DataKeyNames这是数据库唯一标识--%>
                    <Columns>
                        
       <x:BoundField Enabled="true" DataField="EntryPerson" SortField="EntryPerson" Hidden="true" />
                        <x:TemplateField Width="30px">
                    <ItemTemplate>
                        <asp:Label ID="Label7" runat="server" Text='<%#RowNumber( Container.DataItemIndex + 1) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>
                        <x:CheckBoxField ID="CBoxSelect" CommandName="CBSelect" DataField="fileid"  runat="server" AutoPostBack="true" Enabled="true" RenderAsStaticField="false" Width="30" />

                        <x:TemplateField Width="80px" HeaderText="项目名称">   
           <ItemTemplate>
                        <asp:Label ID="LabProName" runat="server" Text='<%# getProjectName(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ProjectID"))) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>

                        <x:TemplateField Width="80px" HeaderText="项目负责人">   
           <ItemTemplate>
                        <asp:Label ID="LabProjectHeads" runat="server" Text='<%# getProjectHeads(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ProjectID"))) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>

                        <x:TemplateField Width="80px" HeaderText="项目来源">   
           <ItemTemplate>
                        <asp:Label ID="LabSourceUnit" runat="server" Text='<%# getSourceUnit(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ProjectID"))) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>

                        <x:TemplateField Width="80px" HeaderText="项目性质">   
           <ItemTemplate>
                        <asp:Label ID="LabProjectNature" runat="server" Text='<%# getProjectNature(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ProjectID"))) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>

            <x:TemplateField Width="80px" HeaderText="承担部门名称">   
           <ItemTemplate>
                        <asp:Label ID="LabAgenName" runat="server" Text='<%# getAcceptUnit(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ProjectID"))) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField> 

                        <x:TemplateField Width="80px" HeaderText="来款单位">   
           <ItemTemplate>
                        <asp:Label ID="LabGivenMoneyUnits" runat="server" Text='<%# getGivenMoneyUnits(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ProjectID"))) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>
           
                        <%--<x:BoundField DataField="ProjectName" DataFormatString="{0}" HeaderText="项目名称" />
                        <x:BoundField DataField="ProjectName" DataFormatString="{0}" HeaderText="项目负责人" />
                        <x:BoundField DataField="SourceUnit" HeaderText="项目来源" />
                        <x:BoundField DataField="ProjectNature" HeaderText="项目性质" />
                        <x:BoundField DataField="AcceptUnit" HeaderText="承担部门名称" />
                        <x:BoundField DataField="GivenMoneyUnits" HeaderText="来款单位" />--%>
                        <x:BoundField ID="Time" DataField="Time"  DataFormatString="{0:yyyy-MM-dd}" HeaderText="进账时间" />
                        <x:BoundField ID="Budget" DataField="BudgetDirector" HeaderText="经费负责人" />
                        <x:BoundField ID="MoneyNum" DataField="EveItemUseMoney" HeaderText="金额" />

                        <x:TemplateField Width="80px" HeaderText="项目管理费">   
           <ItemTemplate>
                        <asp:Label ID="ManageMoney" runat="server" Text='<%# getManageMoney(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ProjectID"))) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>

                    </Columns>
                </x:Grid>
                <%--  --%>


                <%--  --%>
            </Items>
        </x:Panel>

        <x:Window ID="Window1" Popup="false" EnableIFrame="true" IFrameUrl="MoneyComeIn.aspx" runat="server"
            EnableMaximize="false" EnableResize="false" Height="400px" Width="450px" Title="添加">
        </x:Window>

        <x:Window ID="Window2" Title="查询" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="true" EnableResize="true" Target="Parent"
            IsModal="True" Width="950px" Height="450px">
        </x:Window>
        <x:Window ID="WindowReport" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="true" EnableResize="false" Height="450px" Width="800px">
        </x:Window>
        <x:Label ID="labResult" Visible="false" runat="server">
        </x:Label>
    </form>
</body>
</html>
