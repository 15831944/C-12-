<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search_Paper.aspx.cs" Inherits="WebApplication1.Search_Paper" %>

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
                <%--  --%>
                <%--  --%>

                <x:Grid ID="Grid_Paper" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                    DataKeyNames="PaperID"
                    AllowSorting="true" SortColumnIndex="0" SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" OnRowCommand="Grid_Paper_RowCommand" OnPageIndexChange="Grid_Paper_PageIndexChange">
                    <%--OnPageIndexChange="Grid_Info_PageIndexChange"--%>
                    <Toolbars>
                        <x:Toolbar ID="Toolbar_top" runat="server">
                            <Items>
                                <%--<x:Label ID="year" Width="30px" runat="server" CssClass="marginr" ShowLabel="true" Text="年份"></x:Label>
                                    <x:DropDownList ID="ddlsearch" Width="200px" ShowLabel="false" AutoPostBack="true" runat="server" TabIndex="1">                                  
                                    </x:DropDownList>

                                    <x:Label ID="Label15"  Width="50px" runat="server"  ShowLabel="true" Text=" "></x:Label> --%>

                                <x:Label ID="AgencyID" Width="60px" runat="server" CssClass="marginr" ShowLabel="true" Text="查询条件"></x:Label>
                                <x:DropDownList ID="dChoose" Width="100px" ShowLabel="false" AutoPostBack="true" runat="server" TabIndex="2" OnSelectedIndexChanged="dChoose_SelectedIndexChanged">
                                    <x:ListItem Text="全部" Value="全部" />
                                    <x:ListItem Text="部门" Value="部门" />
                                    <x:ListItem Text="作者" Value="作者" />
                                    <x:ListItem Text="刊物级别" Value="刊物级别" />

                                    <x:ListItem Text="发表年份" Value="发表年份" />
                                    <x:ListItem Text="收录情况" Value="收录情况" />

                                    
                                    <x:ListItem Text="第一作者" Value="第一作者" />
                                    <x:ListItem Text="通讯作者" Value="通讯作者" />
                                </x:DropDownList>
                                <x:Label ID="Label4" runat="server" Label="Label" Text=" " Width="5px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:TextBox ID="tCondition" ShowLabel="true" Enabled="false" MaxLength="10" MaxLengthMessage="最多可输入10个字符" EmptyText="请输入搜索条件" Width="100px" CssClass="marginr" runat="server" AutoPostBack="true">
                                </x:TextBox>
                                <x:Label ID="Label5" runat="server" Label="Label" Text=" " Width="5px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:DropDownList ID="dCondition" Width="150px" Enabled="false" ShowLabel="false" EnableEdit="true" AutoPostBack="true" runat="server">
                                </x:DropDownList>
                                <x:Button ID="Select" Text="搜索" Type="Submit" Icon="SystemSearch" runat="server" OnClick="Select_Click">
                                </x:Button>
                                <x:Button ID="btnRefresh" runat="server" Icon="ArrowRotateClockwise" Text="刷新" OnClick="btnRefresh_Click">
                                </x:Button>
                                <x:Button ID="btn_AddPaper" Text="新增论文信息" Icon="Add" EnablePostBack="true" runat="server">
                                </x:Button>
                               <x:Button ID="btn_Delete" Text="删除选中行" Icon="Delete" EnablePostBack="true" ConfirmText="确定删除？" runat="server" OnClick="btn_Delete_Click">
                                    </x:Button>
                                <%--  <x:Button ID="btn_Get" Text="导出所选信息" Icon="Disk" EnablePostBack="true" runat="server">
                                    </x:Button>--%>
                                <x:Button ID="btn_UpdatePaper" Text="编辑选中行" Icon="BulletEdit" EnablePostBack="true" runat="server" OnClick="btn_UpdatePaper_Click">
                                </x:Button>
                                <x:Button ID="btn_Get" Text="导出Excel文件" ConfirmText="确定导出？" Icon="Disk" EnablePostBack="true" runat="server" OnClick="btn_Get_Click" EnableAjax="false" DisableControlBeforePostBack="false">
                                </x:Button>
                                <%--    <x:Button ID="reprot" Text="报表"  Icon="Report" EnablePostBack="true" runat="server" >
                                         <Menu ID="Menu1" runat="server">
                                    <x:MenuButton ID="reprot1" runat="server" Text="分部门按题目统计论文情况">
                                    </x:MenuButton>                                
                                </Menu>
                                    </x:Button>--%>
                            </Items>
                        </x:Toolbar>
                    </Toolbars>
                    <PageItems>
                        <x:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                        </x:ToolbarSeparator>
                        <x:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
                        </x:ToolbarText>
                        <x:DropDownList ID="ddlGridPageSize" Width="80px" EnableEdit="false" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlGridPageSize_SelectedIndexChanged">
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
                        <x:CheckBoxField ID="CBoxSelect" CommandName="CBSelect" DataField="insepctid" runat="server" AutoPostBack="true" Enabled="true" RenderAsStaticField="false" Width="30" />
                        <x:BoundField Enabled="true" DataField="EntryPerson" SortField="EntryPerson" Hidden="true" />
                        <x:TemplateField Width="30px" Hidden="true">
                            <ItemTemplate>
                                <asp:Label ID="Label7" runat="server" Text='<%#RowNumber( Container.DataItemIndex + 1) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:BoundField DataField="Subject" SortField="Subject" Width="100px" HeaderText="题目" />
                        <x:BoundField DataField="FirstWriter" SortField="FirstWriter" Width="150px" HeaderText="第一作者" />
                        <x:BoundField DataField="WriterIdentity" SortField="WriterIdentity" Width="150px" HeaderText="论文作者身份" />
                        <x:BoundField DataField="MessageWriter" SortField="MessageWriter" Width="150px" HeaderText="通讯作者" />
                        <x:BoundField DataField="MWAgency" SortField="MWAgency" Width="150px" HeaderText="通讯作者部门" />
                        <x:BoundField DataField="PublicJournalName" SortField="PublicJournalName" Width="150px" HeaderText="发布刊物" />
                        <x:BoundField DataField="PublicDate" SortField="PublicDate" Width="100px" DataFormatString="{0:yyyy-MM-dd}" HeaderText="发表时间" />
                        <x:BoundField DataField="SerialNum" SortField="SerialNum" Width="150px" HeaderText="刊号" />
                        <x:BoundField DataField="VolumesNum" SortField="VolumesNum" Width="150px" HeaderText="卷号" />
                        <x:BoundField DataField="JournalNum" SortField="JournalNum" Width="150px" HeaderText="期号" />
                        <x:BoundField DataField="StartPageNum" SortField="StartPageNum" Width="150px" HeaderText="起始页码" />
                        <x:BoundField DataField="EndPageNum" SortField="EndPageNum" Width="150px" HeaderText="结束页码" />
                        <x:BoundField DataField="PaperRank" SortField="PaperRank" Width="150px" HeaderText="刊物级别" />
                        <%-- <x:BoundField DataField="PaperForm" SortField="PaperForm" Width="150px" HeaderText="论文形式" />--%>
                        <x:BoundField DataField="RetrieveSituation" SortField="RetrieveSituation" Width="150px" HeaderText="检索情况" />
                        <%-- <x:BoundField DataField="IncludeNum" SortField="IncludeNum" Width="150px" HeaderText="收录号" />  --%>
                        <x:BoundField DataField="ImpactFactor" SortField="ImpactFactor" Width="150px" HeaderText="影响因子" />
                        <%-- <x:TemplateField Width="80px" HeaderText="是否收录">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%#FindSL(Convert.ToBoolean(Eval( "Record"))) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>  --%>
                        <x:BoundField DataField="QuoteNum" SortField="QuoteNum" Width="150px" HeaderText="引用次数" />
                        <x:BoundField DataField="HQuoteNum" SortField="HQuoteNum" Width="150px" HeaderText="他引次数" />


                        <x:BoundField DataField="PaperUnit" SortField="PaperUnit" Width="150px" HeaderText="论文所属单位" />
                        <x:TemplateField Width="80px" HeaderText="成果名称">
                            <ItemTemplate>
                                <asp:Label ID="LabeAgency" runat="server" Text='<%# FindName(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "AchievementID"))) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:TemplateField Width="80px" HeaderText="保密级别">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# ChangeSecrecyLevel(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "SecrecyLevel"))) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>

                        <x:TemplateField HeaderText="收录号" Hidden="true">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# getincludenum(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "PaperID"))) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:TemplateField HeaderText="全部作者" Hidden="true">
                            <ItemTemplate>
                                <asp:Label ID="Label6" runat="server" Text='<%# getwriter(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "PaperID"))) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:BoundField DataField="Remark" SortField="Remark" Width="150px" Hidden="true" HeaderText="备注" />
                        <x:TemplateField HeaderText="收录号" Width="60px">
                            <ItemTemplate>
                                <a href="javascript:<%# GetEditUrlr(Eval("PaperID")) %>">详情</a>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:TemplateField HeaderText="作者" Width="60px">
                            <ItemTemplate>
                                <a href="javascript:<%# GetEditUrlw(Eval("PaperID")) %>">详情</a>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:BoundField DataField="Sort" SortField="Sort" Width="150px" HeaderText="分类" />
                        <x:TemplateField HeaderText="备注" Width="60px">
                            <ItemTemplate>
                                <a href="javascript:<%# GetEditUrl(Eval("PaperID")) %>">详情</a>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:TemplateField HeaderText="相关文档" Width="60px">
                            <ItemTemplate>
                                <a href="javascript:<%# GetEditUrlf(Eval("PaperID")) %>">详情</a>
                            </ItemTemplate>
                        </x:TemplateField>
                    </Columns>
                </x:Grid>
            </Items>
        </x:Panel>
        <x:Window ID="Window_addPaper" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableMinimize="false" EnableResize="false" Height="500px" Width="800px" Title="添加">
        </x:Window>

        <x:Window ID="Window_UpdatePaper" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableMinimize="false" EnableResize="false" Height="500px" Width="800px" Title="更新">
        </x:Window>
        <x:Window ID="Details" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableMinimize="false" EnableResize="false" Height="250px" Width="350px">
        </x:Window>
        <x:Window ID="Writer" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableMinimize="false" EnableResize="false" Height="250px" Width="350px">
        </x:Window>
        <x:Window ID="WindowReport" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="true" EnableResize="false" Height="450px" Width="800px">
        </x:Window>
        <x:Label ID="labResult" Visible="false" runat="server">
        </x:Label>
    </form>
</body>
</html>
