<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageMoneys.aspx.cs" Inherits="WDFramework.Information.ManageMoney.ManageMoneys" %>


<%@ Register assembly="FineUI" namespace="FineUI" tagprefix="x" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta name="sourcefiles" content="~/People/Add_StaffInfos.aspx;~/People/Update_StaffInfos.aspx" />
</head>
<body oncontextmenu='return false' ><%--取消鼠标右键的点击--%>
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <x:Panel ID="Panel1" runat="server" BodyPadding="5px" EnableBackgroundColor="true"
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start"
            ShowHeader="false" Title="用户管理">
        <Items>
            <x:Grid ID="People_Info" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
         EnableCheckBoxSelect="false" EnableRowNumber="false" EnableRowNumberPaging="true" EnableTextSelection="true"
         DataKeyNames="ProjectID" AllowSorting="true" SortColumnIndex="0" SortDirection="DESC" 
         AllowPaging="true" IsDatabasePaging="true" AllowCellEditing="true"  >
        <Toolbars>
        <x:Toolbar ID="Toolbar_top" runat="server" Width ="800px">
            <Items>              
                 <x:Button ID="btnMoneyManege" runat="server" Icon="Wrench" Text="设置管理费比例"  OnClick="btnMoneyManege_Click">  
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
                        <x:DropDownList ID="ddlGridPageSize" Width="80px" AutoPostBack="true" OnSelectedIndexChanged ="ddlGridPageSize_SelectedIndexChanged" 
                            runat="server">
                            <x:ListItem Text="10" Value="10" />
                            <x:ListItem Text="20" Value="20" Selected="true" />
                            <x:ListItem Text="30" Value="30" />
                            <x:ListItem Text="50" Value="50" />
                        </x:DropDownList>
                    </PageItems>
       <Columns>
                        <x:TemplateField Width="30px">
                            <ItemTemplate>
                                <asp:Label ID="Label8" runat="server" Text='<%# RowNumber(Container.DataItemIndex + 1) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:BoundField DataField="ProjectName" SortField="ProjectName" Width="100px" HeaderText="项目名称" />
                        <x:BoundField DataField="SourceUnit" SortField="SourceUnit" Width="100px" HeaderText="项目来源" />
                        <x:BoundField DataField="ProjectSortName" SortField="ProjectSortName" Width="100px" HeaderText="项目分类名称" />
                        <x:BoundField DataField="ProjectNature" SortField="ProjectNature" Width="100px" HeaderText="项目性质" />
                        <x:BoundField DataField="ProjectLevel" SortField="ProjectLevel" Width="100px" HeaderText="项目级别" />
                        <x:BoundField DataField="CooperationForms" SortField="CooperationForms" Width="100px" HeaderText="合作形式" />
                        <x:TemplateField Width="80px" HeaderText="所属机构名称">
                            <ItemTemplate>
                                <asp:Label ID="Label16" runat="server" Text='<%# AgencyName (Convert .ToInt32 ( DataBinder.Eval(Container.DataItem, "AgencyID"))) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:BoundField DataField="ApprovedMoney" SortField="ApprovedMoney" Width="100px" HeaderText="项目经费" />
                        <x:BoundField DataField="GetMoney" SortField="GetMoney" Width="100px" HeaderText="到账金额" />
                        <x:BoundField DataField="ManageMoney" SortField="ManageMoney" Width="100px" HeaderText="管理费比例" />
                        <x:TemplateField Width="80px" HeaderText="保密等级">
                            <ItemTemplate>
                                <asp:Label ID="Label6" runat="server" Text='<%# SecrecyLevelName(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "SecrecyLevel"))) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                    </Columns>
       </x:Grid>
       </Items>
       </x:Panel>
        <x:Window ID="WindowADD" Popup="false" EnableIFrame="true"  runat="server"
            EnableMaximize="false" EnableResize="false" Height="400px" Width="450px" Title ="添加人员基本信息">
        </x:Window>
         <x:Window ID="WindowUpdate" Popup="false" EnableIFrame="true"  runat="server"
            EnableMaximize="false" EnableResize="false" Height="400px" Width="450px" Title ="设置管理费比例">
        </x:Window>
         <x:Window ID="Remark" Popup="false" EnableIFrame="true"  runat="server"
            EnableMaximize="false" EnableResize="false" Height="250px" Width="350px" >
        </x:Window>
        <x:Label ID="labResult" Visible="false" runat="server">
        </x:Label>
    </form>
</body>
</html>


