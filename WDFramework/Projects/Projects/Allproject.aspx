<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Allproject.aspx.cs" Inherits="WDFramework.Project.Allproject" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body oncontextmenu='return false'>
    <%--取消鼠标右键的点击--%>
    <form id="form1" runat="server">
       <x:PageManager ID="PageManager1" AutoSizePanelID="Panel1"  runat="server" />
        <%--  --%>
          <x:Panel ID="Panel1" runat="server" BodyPadding="5px" EnableBackgroundColor="true"
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start"
            ShowHeader="false" Title="用户管理">
            <Items>
                <%--  --%>
                <x:Grid ID="GridProjectAll" runat="server" BoxFlex="3" ShowBorder="true" ShowHeader="false"
                    EnableCheckBoxSelect="false" EnableRowNumber="false" DataKeyNames="ProjectID"
                    AllowSorting="true" SortColumnIndex="0" SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" OnRowCommand="GridProjectAll_RowCommand" OnPageIndexChange="GridProjectAll_PageIndexChange">
                    <Toolbars>
                        <x:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <x:Label ID="Label2" runat="server" Label="Label" Width="0" Text=" "></x:Label>
                                <x:Label ID="Label9" runat="server" Label="Label" Text="查询条件："></x:Label>
                                <x:Label ID="Label3" runat="server" Label="Label" Width="0" Text=" "></x:Label>
                                <x:DropDownList ID="FEN" ShowLabel="false" AutoPostBack="true" TabIndex="1" OnSelectedIndexChanged="FEN_SelectedIndexChanged" runat="server" Width="100px">
                                    <x:ListItem Text="全部" Value="全部" />
                                    <x:ListItem Text="项目名称" Value="项目名称" />
                                    <x:ListItem Text="项目状态" Value="项目状态" />
                                    <x:ListItem Text="年份" Value="年份" />
                                    <x:ListItem Text="项目来源" Value="项目来源" />
                                    <x:ListItem Text="项目级别" Value="项目级别" />
                                    <x:ListItem Text="承担部门" Value="承担部门" />
                                    <x:ListItem Text="项目负责人" Value="项目负责人" />
                                    <x:ListItem Text="项目性质" Value="项目性质" />
                                    <x:ListItem Text="项目成员" Value="项目成员" />
                                    <x:ListItem Text="保密等级" Value="保密等级" />
                                </x:DropDownList>

                                <%--<x:Label ID="Label5" runat="server" Label="Label" Width ="10" Text=" "></x:Label>--%>

                                <x:TwinTriggerBox ID="SourceUnit" runat="server" MaxLength="20" TabIndex="2" EmptyText="请输入搜索条件" ShowLabel="false" ShowTrigger1="false" ShowTrigger2="false" Width="100px">
                                </x:TwinTriggerBox>
                                <x:DropDownList ID="DropDownListYearandLevel" ShowLabel="false" AutoPostBack="true" runat="server" TabIndex="3" Width="100px">
                                </x:DropDownList>
                                <x:Label ID="Label7" runat="server" Label="Label" Width="5" Text=" "></x:Label>
                                <x:Label ID="Label11" runat="server" Label="Label" Text="按"></x:Label>
                                <x:Label ID="Label12" runat="server" Label="Label" Width="5" Text=" "></x:Label>
                               

                                <x:DropDownList ID="AN" ShowLabel="false" AutoPostBack="true" runat="server" TabIndex="4" Width="100px">
                                    <%--                     <x:ListItem Text="" Value="1" /> --%>
                                </x:DropDownList>
                                <x:Label ID="Label4" runat="server" Label="Label" Width="5" Text=" "></x:Label>
                                <x:DropDownList ID="ProjectNature" ShowLabel="false" AutoPostBack="true" TabIndex="5" runat="server" Text="项目性质" Width="100px">
                                </x:DropDownList>
                                <x:Button ID="FindObjectAll" Text="搜索" Icon="SystemSearch" Type="Submit" runat="server" OnClick="FindObjectAll_Click" ValidateForms="GridProjectAll">
                                </x:Button>
                                <x:Button ID="btnRefresh" runat="server" Icon="ArrowRotateClockwise" Text="刷新" OnClick="btnRefresh_Click">
                                </x:Button>
                                <%--<x:Label ID="Label4" runat="server" Label="Label" Width ="10" Text=" "></x:Label> --%>
                                <x:Button ID="btnAddProject" Text="新增" Icon="Add" runat="server">
                                </x:Button>

                                <x:Button ID="btnSelect_All" runat="server" Text="全选" OnClick="btnSelect_All_Click"></x:Button>
                                <x:Button ID="btnDelete" Text="删除" Icon="Delete" runat="server" OnClick="btnDelete_Click" ConfirmText="确定删除？" ConfirmTarget="Top">
                                </x:Button>
                                <x:Button ID="btnUpdate" Text="编辑" Icon="Pencil" runat="server" OnClick="btnUpdate_Click">
                                </x:Button>
                                <x:Button ID="btn_Get" Text="导出Excel文件" ConfirmText="确定导出？" Icon="Disk" EnablePostBack="true" runat="server" OnClick="btn_Get_Click" EnableAjax="false" DisableControlBeforePostBack="false">
                                </x:Button>
                                <x:Button ID="reprot" Text="报表" Icon="Report" EnablePostBack="true" runat="server">
                                    <Menu ID="Menu1" runat="server">
                                        <x:MenuButton ID="reprot1" runat="server" Text="按状态统计项目信息">
                                        </x:MenuButton>
                                        <x:MenuButton ID="reprot2" runat="server" Text="分承担部门按负责人统计项目信息">
                                        </x:MenuButton>
                                        <x:MenuButton ID="reprot3" runat="server" Text="分项目类型按年份统计项目信息">
                                        </x:MenuButton>
                                        <x:MenuButton ID="reprot4" runat="server" Text="分项目来源按年份统计项目信息">
                                        </x:MenuButton>
                                        <x:MenuButton ID="reprot5" runat="server" Text="分年份按承担部门统计项目信息">
                                        </x:MenuButton>
                                        <x:MenuButton ID="reprot6" runat="server" Text="分横向/纵向按年份统计项目信息">
                                        </x:MenuButton>
                                        <x:MenuButton ID="reprot7" runat="server" Text="分部门统计项目情况">
                                        </x:MenuButton>
                                        <x:MenuButton ID="reprot8" runat="server" Text="分年份按项目来源管理费">
                                        </x:MenuButton>
                                        <x:MenuButton ID="reprot9" runat="server" Text="分年份按项目类型统计管理费">
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
                        <x:DropDownList ID="ddlGridPageSize" Width="80px" AutoPostBack="true" OnSelectedIndexChanged="ddlGridPageSize_SelectedIndexChanged"
                            runat="server">
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
                                <asp:Label ID="Label8" runat="server" Text='<%# RowNumber(Container.DataItemIndex + 1) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:CheckBoxField ID="CBoxSelect" CommandName="CBSelect" DataField="Project" runat="server" AutoPostBack="true" Enabled="true" RenderAsStaticField="false" Width="30"/>
                        <x:BoundField Enabled="true" DataField="EntryPerson" SortField="EntryPerson" Hidden="true" />
                        <x:TemplateField Width="30px" Hidden="true">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# RowNumber(Container.DataItemIndex + 1) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:BoundField DataField="ProjectName" SortField="ProjectName" Width="100px" HeaderText="项目名称" />
                        <x:BoundField DataField="ProjectInNum" SortField="ProjectInNum" Width="100px" HeaderText="项目内部编号(科技处)" />
                        <x:BoundField DataField="PactNum" SortField="PactNum" Width="100px" HeaderText="合同编号" />
                        <x:BoundField DataField="TaskNum" SortField="TaskNum" Width="100px" HeaderText="课题编号" />
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
                        <x:BoundField DataField="ProjectManager" SortField="ProjectManager" Width="100px" HeaderText="项目负责人(前三)" />
                        <x:BoundField DataField="ProjectHeads" SortField="ProjectHeads" Width="100px" HeaderText="实际负责人" />
                        <x:BoundField DataField="AcceptUnit" SortField="AcceptUnit" Width="100px" HeaderText="承担部门" />
                        <x:BoundField DataField="StartTime" SortField="StartTime" Width="100px" DataFormatString="{0:yyyy-MM-dd}" HeaderText="开始时间" />
                        <x:BoundField DataField="EndTime" SortField="EndTime" Width="100px" DataFormatString="{0:yyyy-MM-dd}" HeaderText="结束时间" />
                        <x:BoundField DataField="ExpectEndTime" SortField="ExpectEndTime" Width="100px" DataFormatString="{0:yyyy-MM-dd}" HeaderText="预期完成时间" />
                        <x:BoundField DataField="ExpecteResults" SortField="ExpecteResults" Width="100px" HeaderText="预期成果" />
                        <x:BoundField DataField="ProjectState" SortField="ProjectState" Width="100px" HeaderText="项目状态" />
                        <x:BoundField DataField="GivenMoneyUnits" SortField="GivenMoneyUnits" Width="100px" HeaderText="来款单位" />
                        <x:BoundField DataField="ApprovedMoney" SortField="ApprovedMoney" Width="100px" HeaderText="项目经费" />
                        <x:BoundField DataField="GetMoney" SortField="GetMoney" Width="100px" HeaderText="到账金额" />
                        <x:BoundField DataField="ManageMoney" SortField="ManageMoney" Width="100px" HeaderText="管理费比例" />
                        <x:TemplateField Width="80px" HeaderText="保密等级">
                            <ItemTemplate>
                                <asp:Label ID="Label6" runat="server" Text='<%# SecrecyLevelName(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "SecrecyLevel"))) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>

                        <x:BoundField DataField="Remark" SortField="Remark" Width="100px" HeaderText="项目成员" Hidden="true" />
                        <x:TemplateField HeaderText="项目成员" Width="80px">
                            <ItemTemplate>
                                <a href="javascript:<%# GetEditUrlProjectMember(Eval("ProjectID ")) %>">详情</a>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:BoundField DataField="Remark" SortField="Remark" Width="100px" HeaderText="备注" Hidden="true" />
                        <x:TemplateField HeaderText="备注" Width="60px">
                            <ItemTemplate>
                                <a href="javascript:<%# GetEditUrl(Eval("ProjectID ")) %>">详情</a>
                            </ItemTemplate>
                        </x:TemplateField>

                        <%--<x:TemplateField HeaderText="经济效益附件" Width="100px">
                            <ItemTemplate>
                                <a href="javascript:<%# GetEditUrlBenefit(Eval("ProjectID")) %>">操作</a>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:TemplateField HeaderText="经费预算附件" Width="100px">
                            <ItemTemplate>
                                <a href="javascript:<%# GetEditUrlBudget(Eval("ProjectID")) %>">操作</a>
                            </ItemTemplate>
                        </x:TemplateField>--%>

                    </Columns>
                </x:Grid>
                <%------------%>
        
                <x:Grid ID="GridProjectAllTwo" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                    EnableCheckBoxSelect="false" EnableRowNumber="false" DataKeyNames="ProjectFileID"
                    AllowSorting="true" SortColumnIndex="0" SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" OnRowCommand="GridProjectAll_Two_RowCommand" OnPageIndexChange="GridProjectAll_PageIndexChange">

                    <Toolbars>
                        <x:Toolbar ID="Toolbar2" runat="server">
                            <Items>
                                <x:Label ID="Label5" runat="server" Label="Label" Width="5" Text=" "></x:Label>
                                <x:Label ID="Label10" runat="server" Label="Label" Text="     "></x:Label>
                                <x:Label ID="Label13" runat="server" Label="Label" Width="5" Text=" "></x:Label>
                                <x:Button ID="btnAdd_two" Text="新增项目文档" Icon="Add" runat="server">
                                </x:Button>

                                <x:Button ID="btnSelect_All_Two" runat="server" Text="全选" OnClick="btnSelect_All_Click_Two"></x:Button>
                                <x:Button ID="btnDelete_two" Text="删除项目文档" Icon="Delete" runat="server" OnClick="btnDeleteTwo_Click" ConfirmText="确定删除？" ConfirmTarget="Top">
                                </x:Button>
                                <x:Button ID="btnUpdate_two" Text="编辑项目文档" Icon="Pencil" runat="server">
                                </x:Button>
                            </Items>
                        </x:Toolbar>
                    </Toolbars>
                    <%--。。。。。--%>
                    <Columns>
                        <x:TemplateField Width="30px">
                            <ItemTemplate>
                                <asp:Label ID="Label166" runat="server" Text='<%# RowNumber_two(Container.DataItemIndex + 1) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:CheckBoxField ID="CBSelect_Two" CommandName="CBSelect_Two" DataField="Project" runat="server" AutoPostBack="true" Enabled="true" RenderAsStaticField="false" Width="30" />
                        <x:BoundField Enabled="true" DataField="EntryPerson" SortField="EntryPerson" Hidden="true" />
                        <x:TemplateField Width="30px" Hidden="true">
                            <ItemTemplate>
                                <asp:Label ID="Label155" runat="server" Text='<%# RowNumber_two(Container.DataItemIndex + 1) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:BoundField DataField="FileCode" SortField="FileCode" Width="120px" HeaderText="文档编号" />
                        <x:BoundField DataField="FileName" SortField="FileName" Width="120px" HeaderText="文档名称" />
                        <x:BoundField DataField="FileType" SortField="FileType" Width="120px" HeaderText="文档类型" />
                        <%--<x:BoundField DataField="SecrecyLevel" SortField="SecrecyLevel" Width="80px" HeaderText="保密等级" />--%>
                        <x:TemplateField Width="80px" HeaderText="保密等级">
                            <ItemTemplate>
                                <asp:Label ID="SecrecyLevel" runat="server" Text='<%# SecrecyLevelName(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "SecrecyLevel"))) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:TemplateField HeaderText="文件下载" Width="80px">
                            <ItemTemplate>
                                <a href="javascript:<%# GetEditUrlDownload(Eval("ProjectFileID")) %>">操作</a>
                            </ItemTemplate>
                        </x:TemplateField>
                    </Columns>
                    <%--AllowPaging这是分页功能--%>
                    <PageItems>
                        <x:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                        </x:ToolbarSeparator>
                        <x:ToolbarText ID="ToolbarText2" runat="server" Text="每页记录数：">
                        </x:ToolbarText>
                        <x:DropDownList ID="DropDownList5" Width="80px" AutoPostBack="true" OnSelectedIndexChanged="ddlGridPageSize_SelectedIndexChanged" runat="server">
                            <x:ListItem Text="10" Value="10" />
                            <x:ListItem Text="20" Value="20" Selected="true" />
                            <x:ListItem Text="30" Value="30" />
                            <x:ListItem Text="50" Value="50" />
                        </x:DropDownList>
                    </PageItems>
                </x:Grid>

      </Items>
   </x:Panel>
                 <%------------------%>
        <x:Window ID="WindowProject" Popup="false" EnableIFrame="true" runat="server" AutoScroll="false"
            EnableMaximize="false" EnableResize="false" Height="450px" Width="850px" Title="添加项目基本信息">
        </x:Window>
        <x:Window ID="WindowUpdate" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableResize="false" Height="450px" Width="850px" Title="编辑项目基本信息">
        </x:Window>
        <x:Window ID="Window1" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableResize="false" Height="450px" Width="750px" Title="备注">
        </x:Window>
        <x:Window ID="Window2" Title="查询" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableResize="false" Target="Parent"
            IsModal="True" Width="750px" Height="450px">
        </x:Window>
        <x:Window ID="Remark" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableResize="false" Height="250px" Width="350px">
        </x:Window>
        <x:Window ID="ProjectMember" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableResize="false" Height="250px" Width="350px">
        </x:Window>
        <x:Window ID="Benefit" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableResize="false" Height="250px" Width="350px">
        </x:Window>
        <x:Window ID="Budget" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableResize="false" Height="250px" Width="350px">
        </x:Window>
        <x:Window ID="WindowReport" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="true" EnableResize="false" Height="450px" Width="800px">
        </x:Window>
        <x:Window ID="WindowAddDocument" Popup="false" EnableIFrame="true" runat="server" AutoScroll="false"
            EnableMaximize="false" EnableResize="false" Height="280px" Width="400px" Title="添加项目文档">
        </x:Window>
        <x:Window ID="WindowUpdateDocument" Popup="false" EnableIFrame="true" runat="server" AutoScroll="false"
            EnableMaximize="false" EnableResize="false" Height="280px" Width="400px" Title="编辑项目文档">
        </x:Window>
        <x:Window ID="WindowDownloadFile" Popup="false" EnableIFrame="true" runat="server" AutoScroll="false"
            EnableMaximize="false" EnableResize="false" Height="200px" Width="250px" Title="下载项目文档">
        </x:Window>
        <x:Label ID="labResult" Visible="false" runat="server">
        </x:Label>
    </form>
</body>
</html>
