<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MoneyGive.aspx.cs" Inherits="WDFramework.MoneyGive" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body oncontextmenu='return false'><%--取消鼠标右键的点击--%>
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <%--  --%>
       <x:Panel ID="Panel1" runat="server" BodyPadding="5px" EnableBackgroundColor="true"
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start"
            ShowHeader="false" Title="资金信息">
        <Items>
            
<%--  --%>


         <x:Grid ID="gd_MoneyGive" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
         EnableRowNumber="false" EnableRowNumberPaging="true" EnableTextSelection="true" 
         DataKeyNames="FundInformationID" AllowSorting="true" SortColumnIndex="0" AutoPostBack="true"
         SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" OnPageIndexChange="gd_MoneyGive_PageIndexChange"
              OnRowClick="gd_MoneyGive_RowClick">
        <Toolbars>
        <x:Toolbar ID="Toolbar1" runat="server" >
            <Items>
                <x:Label ID="Label2" runat="server" Label="Label" Width ="15" Text=" "></x:Label>
                <x:Button ID="btn_ClickIn" Text="登记" Icon="BookEdit"  EnablePostBack="true" runat="server">
                </x:Button>

                <x:Label ID="Label8" runat="server" Label="Label" Width ="15" Text=" "></x:Label>
                <x:Label ID="Label9" runat="server" Label="Label" Text="查询条件"></x:Label>
                <x:Label ID="Label10" runat="server" Label="Label" Width ="15" Text=" "></x:Label>

                <x:DropDownList ID="ddl_UnitALl" ShowLabel="false" AutoPostBack="true" runat="server" Width ="100px"  OnSelectedIndexChanged ="ddl_UnitALl_SelectedIndexChanged">
              
                <x:ListItem Text="承担部门" Value="0" />
                <x:ListItem Text="项目" Value="1" />
                <x:ListItem Text="全部" Value="2" />
                </x:DropDownList>

                <x:Label ID="Label1" runat="server" Label="Label" Width ="15" Text=" "></x:Label>
                <x:Label ID="lb_Change" runat="server" Label="Label" Text="选择部门名"></x:Label>
                <x:Label ID="Label7" runat="server" Label="Label" Width ="15" Text=" "></x:Label>

                <x:DropDownList ID="ddl_Unit" ShowLabel="false" AutoPostBack="true" runat="server" Width ="100px" >

                </x:DropDownList>



                <x:Label ID="Label12" runat="server" Label="Label" Width ="30px" Text=" "></x:Label>

                <x:TwinTriggerBox runat="server" EmptyText="输入项目名称" ShowLabel="false" ID="ttb_Work" ShowTrigger1="false"
                     ShowTrigger2="false" Width="100px">
                </x:TwinTriggerBox>

                <x:Label ID="Label13" runat="server" Label="Label" Width ="15" Text=" "></x:Label>

                <x:Button ID="btn_FindUnitPeople" Text="搜索" Type="Submit" Icon="SystemSearch"  EnablePostBack="true" runat="server" OnClick="btn_FindUnitPeople_Click">
                </x:Button>

                <x:Button ID="btnRefresh" runat="server" Icon="ArrowRotateClockwise"  Text="刷新" OnClick="btnRefresh_Click">  
                 </x:Button> 

                <x:Button ID="btn_Delete" Text="删除选中行" ConfirmText="确认删除？" Icon="Delete" EnablePostBack="true" runat="server" OnClick="btn_Delete_Click">
                </x:Button>

                <x:Button ID="reprot" Text="报表"  Icon="Report" EnablePostBack="true" runat="server" >
                                         <Menu ID="Menu1" runat="server">
                                    <x:MenuButton ID="reprot1" runat="server" Text="分承担部门按项目统计经费支出">
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
                        <x:TemplateField Width="30px">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%#RowNumber( Container.DataItemIndex + 1) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>
           <x:CheckBoxField ID="CBoxSelect" CommandName="CBSelect" DataField="fileid"  runat="server" AutoPostBack="true" Enabled="true" RenderAsStaticField="false" Width="30" />
            <x:TemplateField Width="100px" HeaderText="承担部门名称">   
           <ItemTemplate>
                        <asp:Label ID="LabAgenName" runat="server" Text='<%# getAcceptUnit(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ProjectID"))) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField> 
           <x:TemplateField Width="80px" HeaderText="所属项目">   
           <ItemTemplate>
                        <asp:Label ID="LabProName" runat="server" Text='<%# getProjectName(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ProjectID"))) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>
       <%--<x:BoundField  DataField="ProjectID" HeaderText="承担部门名称" />
       <x:BoundField  DataField="ProjectID" HeaderText="所属项目" />--%>
       <x:BoundField  DataField="FundingPurposeSortName" HeaderText="用途" />
       <x:BoundField  DataField="BudgetDirector" HeaderText="负责人" />
       <x:BoundField  DataField="Time" DataFormatString="{0:yyyy-MM-dd}" HeaderText="支出时间" />
       <x:BoundField  DataField="EveItemUseMoney" HeaderText="金额" />
       <x:BoundField Enabled="true" DataField="EntryPerson" SortField="EntryPerson" Hidden="true" />
         

       <%--<x:LinkButtonField HeaderText="详细"  CommandName="Action1" Text="查看详细"/>
       <x:LinkButtonField HeaderText="操作"  CommandName="Action1" Text="修改"/>--%>

       </Columns>
       </x:Grid>
            
            <%--  --%>
           

            <%--  --%>
       </Items>
          
       </x:Panel>
         
        <x:Window ID="Window1" Popup="false" EnableIFrame="true" IFrameUrl="MoneyGiveIn.aspx" runat="server"
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