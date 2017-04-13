<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManagerInformation.aspx.cs" Inherits="WDFramework.Manager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body oncontextmenu='return false'>
    <%--取消鼠标右键的点击--%>
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <x:Panel ID="Panel1" runat="server" BodyPadding="5px" EnableBackgroundColor="true"
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start"
            ShowHeader="false" Title="用户管理">
            <Items>
                <x:Grid ID="GridOpetate" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                    EnableCheckBoxSelect="true" EnableMultiSelect="true" EnableRowNumber="true" EnableRowNumberPaging="true" EnableTextSelection="true"
                    DataKeyNames="OperationLogID" AllowSorting="true" SortColumnIndex="0" SortDirection="DESC"
                    AllowPaging="true" IsDatabasePaging="true" OnPageIndexChange="GridOpetate_PageIndexChange">
                    <Toolbars>
                        <x:Toolbar ID="Toolbar_top" runat="server">
                            <Items>

                                <x:Button ID="BtnAgree" OnClick="BtnAgree_Click" Text="同意" runat="server">
                                </x:Button>
                                <x:Button ID="BtnDisAgree" OnClick="BtnDisAgree_Click" Text="不同意" runat="server">
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
                        <x:DropDownList ID="ddlGridPageSize" Width="80px" AutoPostBack="true"
                            runat="server" OnSelectedIndexChanged="ddlGridPageSize_SelectedIndexChanged">
                            <x:ListItem Text="10" Value="10" />
                            <x:ListItem Text="20" Value="20" Selected="true" />
                            <x:ListItem Text="30" Value="30" />
                            <x:ListItem Text="50" Value="50" />
                        </x:DropDownList>
                    </PageItems>
                    <%--DataKeyNames这是数据库唯一标识--%>
                    <Columns>
                        <%--<x:CheckBoxField  ID="CBoxSelect" CommandName="CBSelect" DataField="OpId" runat="server" AutoPostBack="true" Enabled="true" RenderAsStaticField="false" Width="30" />--%>
                        <x:BoundField Width="100px" DataField="LoginName" SortField="LoginName" HeaderText="用户名" />
                        <x:BoundField Width="100px" DataField="OperationType" SortField="OperationType" HeaderText="操作类型" />
                        <x:BoundField Width="100px" DataField="OperationContent" SortField="OperationContent" HeaderText="操作表" />
                        <x:BoundField Width="100px" DataField="OperationDataID" SortField="OperationDataID" HeaderText="操作数据" />
                        <x:BoundField Width="100px" DataField="OperationTime" SortField="OperationTime" HeaderText="操作时间" />
                        <x:TemplateField HeaderText="详情" Width="60px">
                            <ItemTemplate>
                                <a href="javascript:<%# GetEditUrlp(Eval("OperationLogID ")) %>">详情</a>
                            </ItemTemplate>
                        </x:TemplateField>
                    </Columns>
                </x:Grid>
            </Items>
        </x:Panel>
        <x:Window ID="Detail" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableMinimize="false" EnableResize="false" Height="250px" Width="350px">
        </x:Window>
    </form>
</body>
</html>
