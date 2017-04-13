<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LibraryRecord_Pact.aspx.cs" Inherits="WebApplication1.ContractAndPact.Pact.Update_Pact" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <%--取消鼠标右键的点击--%>
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <x:Panel ID="Panel1" runat="server" BodyPadding="5px" EnableBackgroundColor="true"
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start"
            ShowHeader="false">
            <%-- <x:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server"  />
   <x:Panel ID="Panel1" runat="server"  ShowBorder="True"  EnableCollapse="true"
                Layout="Column"    BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5"
                BoxConfigChildMargin="0 5 0 0" ShowHeader="false">--%>
            <Items>
                <x:Grid ID="Grid_LibraryRecord_Pact" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                     EnableRowNumberPaging="true" EnableTextSelection="true" EnableCheckBoxSelect="false"
                    DataKeyNames="LibraryRecordID" AllowSorting="true" SortColumnIndex="0"
                    AllowPaging="true" IsDatabasePaging="true" OnPageIndexChange="Grid_LibraryRecord_Pact_PageIndexChange" OnRowCommand="Grid_LibraryRecord_Pact_RowCommand">
                    <Toolbars>
                        <x:Toolbar ID="Toolbar_top" runat="server">
                            <Items>
                                <%--<x:Label ID="Label1" runat="server" Label="Label" Text=" " Width="20px">
                                </x:Label>--%>
                                <%--这是空行--%>
                                <x:Label ID="labPact" Width="60px" runat="server" CssClass="marginr" ShowLabel="true" Text="合同编号"></x:Label>
                                <x:DropDownList ID="DropDownListPact" ShowLabel="false" AutoPostBack="true"  EnableEdit="true" runat="server" >
                                    <x:ListItem Text="全部" Value="0" />
                                </x:DropDownList>
                                <x:Button ID="btnCheck" Text="搜索" Icon="SystemSearch" runat="server" OnClick="btnCheck_Click" Type="Submit">
                                </x:Button>
                                <x:Button ID="btnRefresh" runat="server" Icon="ArrowRotateClockwise" Text="刷新" OnClick="btnRefresh_Click">
                                </x:Button>
                                  <x:Button ID="btnUpdate" runat="server" Icon="Pencil" Text="编辑选中行" OnClick="btnUpdate_Click" Hidden="true">
                                </x:Button>
                                <x:Button ID="btnDelete" runat="server" Icon="Delete" Text="删除" Hidden="true" OnClick="btnDelete_Click" ConfirmText="确定删除？" ConfirmTarget="Top" Enabled="false">
                                </x:Button>
                                 <%--<x:Button ID="reprotLibraryRecord" Text="借阅统计"  Icon="Report" EnablePostBack="true" runat="server">
                                    </x:Button>
                                <x:Button ID="report" Text="增减统计"  Icon="Report" EnablePostBack="true" runat="server">
                                    </x:Button>--%>
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
                        <x:CheckBoxField ID="BoxSelect_PactRecord" CommandName="CBSelect" DataField="fileid" runat="server" AutoPostBack="true" Enabled="true" RenderAsStaticField="false" Width="30" />
                          <x:BoundField DataField="EntryPerson" SortField="EntryPerson" Width="150px" HeaderText="录入人" Hidden="true" />
                        <%--<x:CheckBoxField ID="CBoxSelect" CommandName="CBSelect" DataField="fileid" runat="server" AutoPostBack="true" Enabled="true" RenderAsStaticField="false" Width="30" />--%>
                        <%--<x:BoundField Enabled="true" DataField="EntryPerson" SortField="EntryPerson" Hidden="true" />--%>
                        <%--  <x:BoundField DataField="FilesID" SortField="FilesID" Width="100px" HeaderText="文件编号" />--%>
                        <x:TemplateField Width="200px" HeaderText="合同编号">
                            <ItemTemplate>
                                <asp:Label ID="labContractName" runat="server" Text='<%# FindByPactIDAndSort(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "LibraryRecordID")),"合同") %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                          <x:TemplateField Width="150px" HeaderText="借阅人">
                            <ItemTemplate>
                                <asp:Label ID="labBorrowPeople" runat="server" Text='<%# FindBorrowPeople(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "LibraryRecordID"))) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:BoundField DataField="BorrowTime" SortField="BorrowTime" Width="100px" DataFormatString="{0:yyyy-MM-dd}" HeaderText="借阅时间" />
                        <x:BoundField DataField="ReturnTime" SortField="ReturnTime" Width="100px" DataFormatString="{0:yyyy-MM-dd}" HeaderText="归还时间" />
                         <x:TemplateField Width="80px" HeaderText="保密级别">
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# ChangeSecrecyLevel(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "SecrecyLevel"))) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>
                        
                        <%--<x:BoundField DataField="SecrecyLevel" SortField="SecrecyLevel" Width="150px" HeaderText="保密等级" />--%>
                    </Columns>
                </x:Grid>
            </Items>
        </x:Panel>
        <x:Window ID="Window_addFile" Popup="false" EnableIFrame="true"  runat="server"  
            EnableMaximize="false" EnableResize="false" Width="400px" Title="添加" Height="350px" AutoScroll="true">
        </x:Window>
        <x:Window ID="Window_Update" Popup="false" EnableIFrame="true"  runat="server" 
            EnableMaximize="false" EnableResize="false" Width="400px" Title="添加" Height="300px" >
        </x:Window>
        <x:Window ID="WindowReport" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="true" EnableResize="false" Height="450px" Width="800px">
        </x:Window>
        <x:Label ID="labResult" Visible="false" runat="server">
        </x:Label>
    </form>
</body>
</html>
