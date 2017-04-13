<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StaffInfo.aspx.cs" Inherits="WDFramework.People.Staffs.StaffInfo" %>


<%@ Register Assembly="FineUI" Namespace="FineUI" TagPrefix="x" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <meta name="sourcefiles" content="~/People/Add_StaffInfos.aspx;~/People/Update_StaffInfos.aspx" />
</head>
<body oncontextmenu='return false'>
    <%--取消鼠标右键的点击--%>
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <x:Panel ID="Panel1" runat="server" BodyPadding="5px" EnableBackgroundColor="true"
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start"
            ShowHeader="false" Title="用户管理">
            <Items>
                <x:Grid ID="People_Info" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                    EnableCheckBoxSelect="false" EnableRowNumber="false" EnableRowNumberPaging="true" EnableTextSelection="true"
                    DataKeyNames="UserInfoID" AllowSorting="true" SortColumnIndex="0" SortDirection="DESC"
                    AllowPaging="true" IsDatabasePaging="true" AllowCellEditing="true" OnRowCommand="People_Info_RowCommand" OnPageIndexChange="People_Info_PageIndexChange">
                    <Toolbars>
                        <x:Toolbar ID="Toolbar_top" runat="server" Width="800px">
                            <Items>
                                <x:Label ID="Label4" runat="server" Label="Label" Width="15" Text=" "></x:Label>
                                <x:Label ID="Label9" runat="server" Label="Label" Text="查询条件："></x:Label>
                                <x:Label ID="Label10" runat="server" Label="Label" Width="15" Text=" "></x:Label>
                                <x:DropDownList ID="ddlsearch" ShowLabel="false" AutoPostBack="true" Width="100px" runat="server" TabIndex="1" OnSelectedIndexChanged="ddlsearch_SelectedIndexChanged">
                                    <x:ListItem Text="全部" Value="0" Selected="true" />
                                    <x:ListItem Text="用户姓名" Value="用户姓名" />
                                    <%--<x:ListItem Text="用户登录名" Value="用户登录名" />--%>
                                    <x:ListItem Text="学历" Value="学历" />

                                    <x:ListItem Text="入校时间" Value="入校时间" />
                                    <x:ListItem Text="政治面貌" Value="政治面貌" />
                                    <x:ListItem Text="行政级别" Value="行政级别" />
                                    <x:ListItem Text="所属机构" Value="所属机构" />

                                    <x:ListItem Text="学位" Value="学位" />
                                    <x:ListItem Text="研究方向" Value="研究方向" />
                                    <x:ListItem Text="最后毕业学校" Value="最后毕业学校" />
                                    <x:ListItem Text="职称" Value="职称" />
                                    <x:ListItem Text="员工类型" Value="员工类型" />
                                    <%--<x:ListItem Text="手机" Value="3" />--%>
                                </x:DropDownList>

                                <x:TwinTriggerBox runat="server" EmptyText="请输入搜索条件" ID="TriggerBox" ShowTrigger1="false" TabIndex="1"
                                    MaxLength="20" MaxLengthMessage="最多输入20个字符" ShowLabel="true" Label="用户姓名或用户登录名"
                                    ShowTrigger2="false">
                                </x:TwinTriggerBox>
                                <x:Label ID="Label11" runat="server" Label="Label" Text=" " Width="5px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:DropDownList ID="dCondition" Enabled="false" Width="100px" EnableEdit="true" ShowLabel="false" AutoPostBack="true" runat="server">
                                </x:DropDownList>
                                <x:Button ID="FindObjectAll" Text="搜索" Icon="SystemSearch" runat="server" OnClick="FindObjectAll_Click" Type="Submit" ValidateForms="Panel1">
                                </x:Button>
                                <x:Button ID="btnRefresh" runat="server" Icon="ArrowRotateClockwise" Text="刷新" OnClick="btnRefresh_Click">
                                </x:Button>
                                <x:Button ID="btnNew" Text="新增人员基本信息" Icon="Add" runat="server">
                                </x:Button>
                                <x:Button ID="btnDelete" Text="删除选中行" Icon="Delete" runat="server" OnClick="btnDelete_Click" ConfirmText="确定删除？" ConfirmTarget="Top" EnablePostBack="true">
                                </x:Button>
                                <x:Button ID="ButtonUpdate" Text="编辑选中行" Icon="Pencil" runat="server" OnClick="ButtonUpdate_Click">
                                </x:Button>
                                <x:Button ID="btn_Get" Text="导出Excel文件" ConfirmText="确定导出？" Icon="Disk" EnablePostBack="true" runat="server" OnClick="btn_Get_Click" EnableAjax="false" DisableControlBeforePostBack="false">
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
                                <asp:Label ID="Label1" runat="server" Text='<%#RowNumber( Container.DataItemIndex + 1) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:CheckBoxField ID="CBoxSelect" CommandName="CBSelect" DataField="Project" runat="server" AutoPostBack="true" Enabled="true" RenderAsStaticField="false" Width="30" />
                        <x:BoundField Enabled="true" DataField="EntryPerson" SortField="EntryPerson" Hidden="true" />
                        <x:TemplateField Width="30px" Hidden="true">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:BoundField Width="100px" DataField="UserName" SortField="UserName" HeaderText="用户姓名" />
                        <x:BoundField Width="80px" DataField="LoginName" SortField="LoginName" HeaderText="用户登录名" />
                        <%--    <x:BoundField Width="80px" DataField="UserInfoBH" SortField="UserInfoBH" HeaderText="校园一卡通号" />      --%>
                        <x:TemplateField Width="80px" HeaderText="性别">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# getgender(Convert.ToString(DataBinder.Eval(Container.DataItem, "Sex"))) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:BoundField Width="80px" DataField="Nation" SortField="Nation" HeaderText="民族" />
                        <x:BoundField Width="80px" DataField="Hometown" SortField="Hometown" HeaderText="籍贯" />
                        <x:BoundField Width="80px" DataField="Domicile" SortField="Domicile" HeaderText="户籍地" />
                        <x:BoundField Width="80px" DataField="HomeAddress" SortField="HomeAddress" HeaderText="家庭住址" />
                        <x:BoundField Width="80px" DataField="Birth" SortField="Birth" DataFormatString="{0:yyyy-MM-dd}" HeaderText="出生年月日" />
                        <x:TemplateField Width="80px" HeaderText="婚姻状况">
                            <ItemTemplate>
                                <asp:Label ID="Label7" runat="server" Text='<%# getMarried(Convert.ToString(DataBinder.Eval(Container.DataItem, "Marriage"))) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:BoundField Width="80px" DataField="TeleNum" SortField="TeleNum" HeaderText="手机号码" />
                        <x:BoundField Width="80px" DataField="HomeNum" SortField="HomeNum" HeaderText="家庭号码" />
                        <x:BoundField Width="80px" DataField="OfficeNum" SortField="OfficeNumBirth" HeaderText="办公电话" />
                        <x:BoundField Width="80px" DataField="Fax" SortField="Fax" HeaderText="传真" />
                        <x:BoundField Width="80px" DataField="qqNum" SortField="qqNum" HeaderText="QQ号码" />
                        <x:BoundField Width="80px" DataField="Email" SortField="Email" HeaderText="电子信箱" />
                        <x:BoundField Width="80px" DataField="PostalCode" SortField="PostalCode" HeaderText="邮件编码" />
                        <x:BoundField Width="80px" DataField="DocumentsType" SortField="DocumentsType" HeaderText="证件类型" />
                        <x:BoundField Width="80px" DataField="DocumentsNum" SortField="DocumentsNum" HeaderText="证件号码" />
                        <x:BoundField Width="80px" DataField="EnterSchoolTime" SortField="EnterSchoolTime" DataFormatString="{0:yyyy-MM-dd}" HeaderText="入校时间" />
                        <x:BoundField Width="80px" DataField="LastSchool" SortField="LastSchool" HeaderText="最后毕业学校" />
                        <x:BoundField Width="80px" DataField="Education" SortField="Education" HeaderText="学历" />
                        <x:BoundField Width="80px" DataField="StudySource" SortField="StudySource" HeaderText="学缘" />
                        <x:BoundField Width="80px" DataField="Degree" SortField="Degree" HeaderText="学位" />
                        <x:BoundField Width="80px" DataField="ResearchDirection" SortField="ResearchDirection" HeaderText="研究方向" />
                        <x:BoundField Width="80px" DataField="Specialty" SortField="Specialty" HeaderText="专长" />
                        <x:BoundField Width="80px" DataField="AdministrativeLevelName" SortField="AdministrativeLevelName" HeaderText="行政级别" />
                        <x:BoundField Width="80px" DataField="SubjectSortName" SortField="SubjectSortName" HeaderText="学科分类名称" />
                        <x:BoundField Width="80px" DataField="PoliticalStatus" SortField="PoliticalStatus" HeaderText="政治面貌" />
                        <x:BoundField Width="80px" DataField="PoliticalStatusTime" SortField="PoliticalStatusTime" DataFormatString="{0:yyyy-MM-dd}" HeaderText="政治面貌获得时间" />
                        <x:BoundField Width="80px" DataField="UnitName" SortField="UnitName" HeaderText="单位名称" />

                        <x:TemplateField Width="80px" HeaderText="所属机构名称">
                            <ItemTemplate>
                                <asp:Label ID="Label16" runat="server" Text='<%# AgencyName (Convert .ToInt32 ( DataBinder.Eval(Container.DataItem, "AgencyID"))) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:BoundField Width="80px" DataField="StaffType" SortField="StaffType" HeaderText="员工类型" />
                        <x:BoundField Width="80px" DataField="JobTitle" SortField="JobTitle" HeaderText="职称" />
                        <x:BoundField Width="80px" DataField="JobTitleTime" SortField="JobTitleTime" DataFormatString="{0:yyyy-MM-dd}" HeaderText="职称获得时间" />
                        <x:TemplateField Width="80px" HeaderText="是否博士生导师">
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# getDoctorTeacher(Convert.ToString(DataBinder.Eval(Container.DataItem, "IsDocdorTeacher"))) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:BoundField Width="80px" DataField="MasterTeacherTime" SortField="MasterTeacherTime" DataFormatString="{0:yyyy-MM-dd}" HeaderText="硕士生导师取得时间" />
                        <x:TemplateField Width="80px" HeaderText="是否为硕士生导师">
                            <ItemTemplate>
                                <asp:Label ID="Label6" runat="server" Text='<%# getMasterTeacher(Convert.ToString(DataBinder.Eval(Container.DataItem, "IsMasteTeacher"))) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>

                        <x:BoundField Width="80px" DataField="DoctorTeacherTime" SortField="DoctorTeacherTime" DataFormatString="{0:yyyy-MM-dd}" HeaderText="博士生导师取得时间" />
                        <x:TemplateField Width="80px" HeaderText="保密等级">
                            <ItemTemplate>
                                <asp:Label ID="Label8" runat="server" Text='<%# SecrecyLevelName(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "SecrecyLevel"))) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:TemplateField HeaderText="人员照片" Width="80px" ID="TemplateField5">
                            <ItemTemplate>
                                <a id="A1" href="javascript:<%# GetPhotoUrl(Eval("UserInfoID")) %>">操作</a>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:BoundField Width="80px" Enabled="true" DataField="Remark" Hidden="true" HeaderText="备注" />
                        <x:BoundField Width="80px" Enabled="true" DataField="Profile" Hidden="true" HeaderText="个人简介" />

                        <x:TemplateField HeaderText="备注" Width="60px">
                            <ItemTemplate>
                                <a href="javascript:<%# GetEditUrl(Eval("UserInfoID")) %>">详情</a>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:TemplateField HeaderText="个人简介" Width="60px">
                            <ItemTemplate>
                                <a href="javascript:<%# GetEditUrlP(Eval("UserInfoID")) %>">详情</a>
                            </ItemTemplate>
                        </x:TemplateField>
                    </Columns>
                </x:Grid>
            </Items>
        </x:Panel>
        <x:Window ID="WindowADD" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableResize="false" Height="450px" Width="800px" Title="添加人员基本信息">
        </x:Window>
        <x:Window ID="WindowUpdate" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableResize="false" Height="450px" Width="800px" Title="编辑人员基本信息">
        </x:Window>

        <x:Window ID="Window2" Title="查询" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="true" EnableResize="true" Target="Parent"
            IsModal="True" Width="750px" Height="450px">
        </x:Window>
        <x:Window ID="Remark" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableResize="false" Height="250px" Width="350px">
        </x:Window>
        <x:Label ID="labResult" Visible="false" runat="server">
        </x:Label>
    </form>
</body>
</html>

