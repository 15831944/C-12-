<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Educations.aspx.cs" Inherits="WDFramework.People.Educations.Educations" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../res/css/main.css" rel="stylesheet" type="text/css" />
</head>
<body oncontextmenu='return false' ><%--取消鼠标右键的点击--%>
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
       <x:Panel ID="Panel1" runat="server" BodyPadding="5px" EnableBackgroundColor="true" 
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" AutoScroll="true" 
            ShowHeader="false" Title="用户管理"   >
        <Items>
         <x:Grid ID="GridEducation" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
         EnableCheckBoxSelect="false" EnableRowNumber="false" EnableRowNumberPaging="true" EnableTextSelection="true"
         DataKeyNames="EducationID" AllowSorting="true" SortColumnIndex="0"
         SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" OnRowCommand="GridEducation_RowCommand" OnPageIndexChange ="GridEducation_PageIndexChange" >
        <Toolbars>
        <x:Toolbar ID="Toolbar1" runat="server"  Width="1300px" >
            <Items>
                 <x:Label ID="Label2" runat="server" Label="Label" Width ="5" Text=" "></x:Label>
                <x:Label ID="Label7" runat="server" Label="Label" Text="查询条件："></x:Label>
             
                <x:Label ID="Label4" runat="server" Label="Label" Width ="5" Text=" "></x:Label>

                 <x:TextBox ID="TriggerBox" runat="server" EmptyText="请输入人员姓名" MaxLength ="20" ShowLabel="true" TabIndex ="1"  >
                    ShowTrigger2 ="false" 
                                         </x:TextBox>            
                <x:Button ID="Find" Text="搜索" Icon="SystemSearch" Type ="Submit" OnClick ="Find_Click" runat="server"   >
                </x:Button>
                 <x:Button ID="btnRefresh" runat="server" Icon="ArrowRotateClockwise"  Text="刷新"  OnClick ="btnRefresh_Click"  >  
                                    </x:Button> 
                 <x:Button ID="btnAdd" Text="新增人员学历信息" Icon="Add"  runat="server">
                </x:Button>

                <x:Button ID="btnSelect_All" runat="server" Text="全选" OnClick="btnSelect_All_Click"></x:Button>
                <x:Button ID="btnDelete" Text="删除选中行" Icon="Delete" runat="server" ConfirmText ="确定删除？" ConfirmTarget ="Top"  OnClick ="btnDelete_Click">
                </x:Button>
               <x:Button ID="btnUpdate" Text="编辑选中行" Icon="Pencil"  runat="server" OnClick ="btnUpdate_Click">
                </x:Button>
                <x:Button ID="btn_Get" Text="导出Excel文件" ConfirmText="确定导出？" Icon="Disk" EnablePostBack="true" runat="server"  OnClick ="btn_Get_Click"  EnableAjax="false" DisableControlBeforePostBack="false">
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
           <x:TemplateField Width="30px" >
                    <ItemTemplate>
                        <asp:Label ID="Label8" runat="server" Text='<%# RowNumber(Container.DataItemIndex + 1) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>
           <x:CheckBoxField ID="CBoxSelect" CommandName="CBSelect" DataField="Project" runat="server" AutoPostBack="true" Enabled="true" RenderAsStaticField="false" Width="30" /> 
           <x:BoundField Enabled="true" DataField="EntryPerson" SortField="EntryPerson" Hidden="true" /> 
            <x:TemplateField Width="30px" Hidden ="true" >
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# RowNumber(Container.DataItemIndex + 1) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>
           <x:TemplateField Width="80px" HeaderText="姓名">
                    <ItemTemplate>                        
                        <asp:Label ID="Label5" runat="server" Text='<%# UserName(Convert .ToInt32 (DataBinder.Eval(Container.DataItem, "UserInfoID"))) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>
            <x:BoundField DataField="Degree" HeaderText="学位" />
            <x:BoundField DataField="DegreeNumber" HeaderText="学位证书号" />
            <x:BoundField DataField="GraduateNumber" HeaderText="毕业证书号" />
            <x:BoundField DataField="EduTime" DataFormatString="{0:yyyy-MM-dd}" HeaderText="取得时间" />
       <x:BoundField DataField="SchoolName" HeaderText="学校名称" />     
       <x:BoundField DataField="College" HeaderText="学院" />
       <x:BoundField DataField="Series"  HeaderText="系" />                      
       <x:BoundField DataField="Major" HeaderText="专业" />     
                <x:TemplateField Width="80px" HeaderText="保密等级">
                    <ItemTemplate>
                        <asp:Label ID="Label6" runat="server" Text='<%# SecrecyLevelName(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "SecrecyLevel"))) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField> 
       </Columns>
       </x:Grid>
       </Items>
       </x:Panel>
         <x:Window ID="WindowAdd" Popup="false" EnableIFrame="true"  runat="server"
            EnableMaximize="false" EnableResize="false" Height="400px" Width="450px" Title="新增人员学历信息">
        </x:Window>
        <x:Window ID="WindowUpdate" Popup="false" EnableIFrame="true"  runat="server"
            EnableMaximize="false" EnableResize="false" Height="400px" Width="450px" Title="编辑人员学历信息">
        </x:Window>

        <x:Window ID="Window4" Title="" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableResize="false" Target="Parent" 
            IsModal="True" Width="750px" Height="450px">
        </x:Window>
    </form>
</body>

</html>


