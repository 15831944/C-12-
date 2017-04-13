<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search_Contract.aspx.cs" Inherits="WebApplication1.ContractAndPact.Contract.Search_Contract" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
                <%--  --%>
                <%--  --%>

                <x:Grid ID="Grid_Contract" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                    EnableRowNumberPaging="true" EnableTextSelection="true" EnableCheckBoxSelect="false"
                    DataKeyNames="ContractID" AllowSorting="true" SortColumnIndex="0"
                    AllowPaging="true" IsDatabasePaging="true" OnPageIndexChange="Grid_Contract_PageIndexChange" OnRowCommand="Grid_Contract_RowCommand">
                    <%--OnPageIndexChange="Grid_Info_PageIndexChange"--%>
                    <Toolbars>
                        <x:Toolbar ID="Toolbar" runat="server">
                            <Items>
                                <x:Label ID="labSort" runat="server" CssClass="marginr" ShowLabel="false" Text="查询条件">
                                </x:Label>
                                <x:TextBox ID="tCondition" Enabled="true" ShowLabel="true" MaxLength="60" MaxLengthMessage="最多可输入40个字符" EmptyText="请输入资料题目" Width="100px" CssClass="marginr" runat="server" AutoPostBack="false">
                                </x:TextBox>
                                <%--<x:DropDownList ID="DropDownList_Contract" ShowLabel="false" AutoPostBack="true" runat="server" EnableEdit="false">
                                   
                                </x:DropDownList>--%><%-- <x:ListItem Text="全部" Value="0" />--%>
                                <x:Button ID="btnCheck" Text="搜索" Icon="SystemSearch" runat="server" OnClick="btnCheck_Click" Type="Submit">
                                </x:Button>
                                <x:Button ID="btnRefresh" runat="server" Icon="ArrowRotateClockwise" Text="刷新" OnClick="btnRefresh_Click">
                                </x:Button>
                                <x:Button ID="btn_AddContract" Text="新增资料信息" Icon="Add" EnablePostBack="true" runat="server">
                                </x:Button>
                                <x:Button ID="btnDelete" Text="删除选中信息" Icon="Delete" EnablePostBack="true" runat="server" OnClick="btnDelete_Click" ConfirmText="确定删除？" ConfirmTarget="Top" Enabled="false">
                                </x:Button>
                                <x:Button ID="btnLibraryRecord" Text="查看借阅记录" Icon="Disk" EnablePostBack="true" runat="server">
                                </x:Button>

                                <x:Button ID="rep" Text="借阅信息统计" Icon="Report" EnablePostBack="true" runat="server">
                                    <Menu ID="Menu1" runat="server">
                                        <x:MenuButton ID="reprotLibrery" Text="借阅统计" Icon="Disk" EnablePostBack="true" runat="server">
                                        </x:MenuButton>
                                        <x:MenuButton ID="reprot" Text="增减统计" Icon="Disk" EnablePostBack="true" runat="server">
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
                                <asp:Label ID="Label3" runat="server" Text='<%#RowNumber( Container.DataItemIndex + 1) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:CheckBoxField ID="BoxSelect_Contract" CommandName="CBSelect" DataField="fileid" runat="server" AutoPostBack="true" Enabled="true" RenderAsStaticField="false" Width="30" />
                        <x:BoundField DataField="EntryPerson" SortField="EntryPerson" Width="150px" HeaderText="录入人" Hidden="true" />
                        <%--<x:BoundField DataField="UnitInspectID" SortField="UnitInspectID" Width="100px" HeaderText="ID" />--%>
                        <x:BoundField DataField="ContractHeadLine" SortField="ContractHeadLine" Width="200px" HeaderText="资料题目" />
                        <x:BoundField DataField="ContractAuthors" SortField="ContractAuthors" Width="200px" HeaderText="资料保存人" />
                        <x:BoundField DataField="ContractOriginal" SortField="ContractOriginal" Width="200px" HeaderText="原始文件保存人" />
                        <%--<x:BoundField DataField="SecrecyLevel" SortField="SecrecyLevel" Width="150px" HeaderText="保密级别" />--%>
                        <%--<x:LinkButtonField HeaderText="&nbsp;" ID="LinkbtnDownLoad" EnableAjax="false" Width="150px" CommandName="Action2" Text=" " />--%>
                        <%--<x:LinkButtonField  ID="btnAddLibraryRecord" runat="server" Text="借阅" Hidden="true"></x:LinkButtonField>--%>
                        <x:TemplateField Width="80px" HeaderText="保密级别">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# ChangeSecrecyLevel(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "SecrecyLevel"))) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:TemplateField HeaderText="相关文档" Width="100px" Hidden="true" ID="ContractRecordLibrary">
                            <ItemTemplate>
                                <a id="hrefLibraryRecord" href="javascript:<%# GetRecordUrlw(Eval("ContractID"),"借阅") %>">借阅</a>
                            </ItemTemplate>
                        </x:TemplateField>
                        <%-- <x:TemplateField HeaderText="借阅" Width="60px" Hidden="true" ID="TemplateField1">
                           <ItemTemplate>
                        <a href="javascript:<%# GetRecordUrlw(Eval("ContractID")) %>">借阅</a>
                          </ItemTemplate>
                          </x:TemplateField> --%>
                        <%--<x:LinkButtonField  ID="btnContractDownLoad" runat="server" Text="下载" Width="100px" CommandName="ActionDown" EnableAjax="false"></x:LinkButtonField>--%>
                        <x:TemplateField HeaderText="相关文档" Width="100px" ID="TemplateField1">
                            <ItemTemplate>
                                <a id="A1" href="javascript:<%# GetRecordUrlDown(Eval("ContractID"),"下载") %>">下载</a>
                            </ItemTemplate>
                        </x:TemplateField>
                        <%--<x:Button ID="btnDownload" runat="server" Text="下载" Visible="false"></x:Button>--%>
                        <%--<x:LinkButtonField ID="LinkbtnLibraryRecord" HeaderText="&nbsp;" EnableAjax="false" Width="80px" CommandName="Action2" Text="借阅" Enabled='<%# ReportTime(Convert.ToDateTime( DataBinder.Eval

(Container.DataItem, "SReportTime"))) %>'/>--%>
                        <%--<x:LinkButtonField ID="LinkbtnDownLoad" HeaderText="&nbsp;" EnableAjax="false" Width="80px" CommandName="Action2" Text="下载" />--%>
                    </Columns>
                </x:Grid>
            </Items>
        </x:Panel>
        <x:Window ID="Window_addContract" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableResize="false" Height="400px" Width="450px" Title="添加资料信息">
        </x:Window>

        <x:Window ID="Window_LibraryRecord" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="true" EnableResize="false" Height="450px" Width="800px" Title="查看借阅记录">
        </x:Window>
        <x:Window ID="Window_Add_LibraryRecord" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableResize="false" Height="400px" Width="450px" Title="增加借阅记录">
        </x:Window>
        <x:Window ID="Window_NoLibraryMessage" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableResize="false" Height="250px" Width="350px">
        </x:Window>
        <x:Window ID="Window_DownLoad" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableResize="false" Height="250px" Width="350px">
        </x:Window>
        <x:Label ID="labResult" Visible="false" runat="server">
        </x:Label>
    </form>
</body>
</html>
