<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search_Pact.aspx.cs" Inherits="WebApplication1.ContractAndPact.Pact.Search_Pact" %>

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

                <x:Grid ID="Grid_Pact" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                     EnableRowNumberPaging="true" EnableTextSelection="true" EnableCheckBoxSelect="false"
                    DataKeyNames="PactID" AllowSorting="true" SortColumnIndex="0"
                    AllowPaging="true" IsDatabasePaging="true" OnPageIndexChange="Grid_Pact_PageIndexChange" OnRowCommand="Grid_Pact_RowCommand">
                    <%--OnPageIndexChange="Grid_Info_PageIndexChange"--%>
                    <Toolbars>
                        <x:Toolbar ID="Toolbar_top" runat="server">
                            <Items>
                                <x:Label ID="labPactNum" runat="server" CssClass="marginr" ShowLabel="false" Text="查询条件">
                                </x:Label>
                                <x:DropDownList ID="ddl_Pact" ShowLabel="false" AutoPostBack="true" runat="server" EnableEdit="false">
                                    <x:ListItem Text="全部" Value="0" />
                                    <x:ListItem Text="合同编号" Value="1" />
                                    <x:ListItem Text="所属项目" Value="2" />
                                    <x:ListItem Text="合同名称" Value="3" />
                                    <x:ListItem Text="合同完成情况" Value="4" />
                                    <x:ListItem Text="合同负责人" Value="5" />
                                </x:DropDownList>
                                   <x:TextBox ID="tCondition" Enabled="true" ShowLabel="true" MaxLength="60" MaxLengthMessage="最多可输入40个字符"   EmptyText="请输入搜索条件"   Width="100px" CssClass="marginr" runat="server"  AutoPostBack="false"  >
                                        </x:TextBox>
                                <x:Button ID="btnCheck" Text="搜索" Icon="SystemSearch" runat="server" OnClick="btnCheck_Click" Type="Submit">
                                </x:Button>
                                <%--<x:Label ID="year" Width="30px" runat="server" CssClass="marginr" ShowLabel="true" Text="项目"></x:Label>
                                    <x:DropDownList ID="ddlsearch" Width="200px" ShowLabel="false" AutoPostBack="true" runat="server" TabIndex="1">
                                    
                                    </x:DropDownList>

                                    <x:Label ID="Label15"  Width="50px" runat="server"  ShowLabel="true" Text=" "></x:Label>

                                    <x:Label ID="AgencyID" Width="50px" runat="server" CssClass="marginr" ShowLabel="true" Text="所属机构"></x:Label>
                                    <x:DropDownList ID="DropDownList1" Width="200px" ShowLabel="false" AutoPostBack="true" runat="server" TabIndex="2">
                                    
                                    </x:DropDownList>--%>
                                <%--<x:TwinTriggerBox runat="server" EmptyText="输入要搜索的关键词" ShowLabel="false" ID="ttbSearch" ShowTrigger1="false"
                                        Trigger2Icon="Search">
                                    </x:TwinTriggerBox>--%>
                                <%--<x:Button ID="btnCheck" runat="server" EnablePostBack="true"  Icon="SystemSearch" Text="搜索">
                                    </x:Button>--%>
                                <x:Button ID="btnRefresh" runat="server" Icon="ArrowRotateClockwise" Text="刷新" OnClick="btnRefresh_Click">
                                </x:Button>
                                <x:Button ID="btn_AddPact" Text="新增合同信息" Icon="Add" EnablePostBack="true" runat="server">
                                </x:Button>

                                 <x:Button ID="btnSelect_All" runat="server" Text="全选" OnClick="btnSelect_All_Click"></x:Button>
                                <x:Button ID="btnDelete" Text="删除选中信息" Icon="Delete" EnablePostBack="true" runat="server" OnClick="btnDelete_Click" ConfirmText="确定删除？" ConfirmTarget="Top" Enabled="false">
                                </x:Button>
                                <x:Button ID="ButtonUpdate" Text="编辑选中行"  Icon="Pencil"   runat="server" OnClick="ButtonUpdate_Click"   >
                                </x:Button>
                                <x:Button ID="btnLibraryRecord" Text="查看借阅记录" Icon="Disk" EnablePostBack="true" runat="server">
                                </x:Button>
                                <x:Button ID="btnAddLibraryRecord" Text="增加借阅记录" Icon="BookmarkAdd" EnablePostBack="true" runat="server" Hidden="true">
                                </x:Button>
                                <x:Button ID="rep" Text="借阅信息统计" Icon="Report" EnablePostBack="true" runat="server">
                                    <Menu ID="Menu1" runat="server">
                                        <x:MenuButton ID="reprotLibraryRecord" Text="借阅统计"  Icon="Report" EnablePostBack="true" runat="server">
                                        </x:MenuButton>
                                        <x:MenuButton ID="report" Text="增减统计"  Icon="Report" EnablePostBack="true" runat="server">
                                        </x:MenuButton>
                                    </Menu>
                                </x:Button>
                                <%--<x:Button ID="Get2" Text="导出所选信息" Icon="Disk" EnablePostBack="true" runat="server">
                                    </x:Button>--%>
                                <%--<x:Button ID="btn_UpdatePact" Text="修改" Icon="BulletEdit" EnablePostBack="true" runat="server">
                                    </x:Button>--%>
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
                        <x:CheckBoxField ID="BoxSelect_Pact" CommandName="CBSelect" DataField="fileid" runat="server" AutoPostBack="true" Enabled="true" RenderAsStaticField="false" Width="30" />
                        <x:BoundField DataField="EntryPerson" SortField="EntryPerson" Width="150px" HeaderText="录入人" Hidden="true" />
                        <x:BoundField DataField="PactNum" SortField="PactNum" Width="150px" HeaderText="合同编号" />
                        <x:BoundField DataField="PactName" SortField="PactNum" Width="150px" HeaderText="合同名称" />
                        <%--<x:BoundField DataField="ProjectID" SortField="ProjectID" Width="150px" HeaderText="所属项目" />--%>
                        <x:TemplateField Width="230px" HeaderText="所属项目">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# ProjectName (Convert.ToInt32(DataBinder.Eval

(Container.DataItem, "ProjectID"))) %>'></asp:Label>
                                <%--<asp:Label ID="LabelAgency" runat="server"></asp:Label>--%>
                            </ItemTemplate>
                        </x:TemplateField>
                         <x:BoundField DataField="StartTime" SortField="StartTime" Width="150px" HeaderText="合同开始时间" DataFormatString="{0:yyyy-MM-dd}"/>
                        <x:BoundField DataField="EndTime" SortField="EndTime" Width="150px" HeaderText="合同结束时间" DataFormatString="{0:yyyy-MM-dd}"/>
                       
                        <x:BoundField DataField="PactType" SortField="PactType" Width="150px" HeaderText="合同类别" />

                          <x:BoundField DataField="ChargePerson" SortField="ChargePerson" Width="150px" HeaderText="合同负责人" />
                          <x:BoundField DataField="PactMoney" SortField="PactMoney" Width="150px" HeaderText="合同经费" />
                          <x:BoundField DataField="RealMoney" SortField="RealMoney" Width="150px" HeaderText="实到经费" />
                        <x:BoundField DataField="PactCompletion" SortField="PactCompletion" Width="150px" HeaderText="合同完成情况" />
                          <x:BoundField DataField="IsExistingFile" SortField="IsExistingFile" Width="150px" HeaderText="文件保存人" />
                          <x:BoundField DataField="FileNum" SortField="FileNum" Width="150px" HeaderText="文件编号" />
                        <%--<x:TemplateField HeaderText=" " TextAlign="Center" Width="200px">
                            <ItemTemplate>
                                <asp:LinkButton ID="labDownLoad" runat="server" Text='<%# DownLoad(Convert.ToInt32( DataBinder.Eval
(Container.DataItem, "PactID"))) %>' ></asp:LinkButton>
                            </ItemTemplate>
                        </x:TemplateField>--%>
                         <x:TemplateField Width="80px" HeaderText="保密级别">
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# ChangeSecrecyLevel(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "SecrecyLevel"))) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>
                        <x:TemplateField HeaderText="相关文档" Width="100px" Hidden="true" ID="ContractRecordLibrary">
                            <ItemTemplate>
                                <a id="hrefLibraryRecord" href="javascript:<%# GetRecordUrlw(Eval("PactID"),"借阅") %>">借阅</a>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:TemplateField HeaderText="相关文档" Width="100px" Hidden="false" ID="TemplateField1">
                            <ItemTemplate>
                                <a id="A1" href="javascript:<%# GetRecordUrlDown(Eval("PactID"),"下载") %>">下载</a>
                            </ItemTemplate>
                        </x:TemplateField>
                        <%--<x:LinkButtonField  Width="80px" Text="下载"  HeaderText="相关文档" EnableAjax="false" CommandName="DownLoad"  />--%>
                        <%--<x:BoundField DataField="EntryPerson" SortField="EntryPerson" Width="150px" HeaderText="录入人" Hidden="true" />--%>
                        <%--<x:BoundField DataField="SecrecyLevel" SortField="SecrecyLevel" Width="150px" HeaderText="保密级别" />--%>
                    </Columns>
                </x:Grid>
            </Items>
        </x:Panel>
       <%-- <x:Window ID="Window_AddPact" Popup="false" EnableIFrame="true" runat="server" AutoScroll="false"
            EnableMaximize="false" EnableResize="false" Height="415px" Width="450px" Title="新增合同信息">
        </x:Window>--%>
         <x:Window ID="Window_AddPact" Popup="false" EnableIFrame="true" runat="server" AutoScroll="false"
            EnableMaximize="false" EnableResize="false" Height="425px" Width="850px" Title="新增合同信息">
        </x:Window>
        <x:Window ID="Window_Add_LibraryRecord" Popup="false" EnableIFrame="true" runat="server" 
            EnableMaximize="false" EnableResize="false" Height="400px" Width="450px" Title="新增借阅记录">
        </x:Window>
        <x:Window ID="Window_LibraryRecord_Pact" Popup="false" EnableIFrame="true" runat="server" 
            EnableMaximize="true" EnableResize="false" Height="450px" Width="800px" Title="查看借阅记录">
        </x:Window>
        <x:Window ID="Window2" Title="查询" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="true" EnableResize="false" Target="Parent"
            IsModal="True" Width="750px" Height="450px">
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
