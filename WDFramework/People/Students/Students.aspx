<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Students.aspx.cs" Inherits="WDFramework.People.Students" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta name="sourcefiles" content="~/Projects/Add_Students.aspx;~/Projects/Update_Students.aspx" />
</head>
<body oncontextmenu='return false' ><%--取消鼠标右键的点击--%>
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <%--  --%>
       <x:Panel ID="Panel1" runat="server" BodyPadding="5px" EnableBackgroundColor="true"
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start"
            ShowHeader="false" Title="用户管理">
        <Items>
  <x:Grid ID="GridProjectStudent" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="true"
         EnableCheckBoxSelect="true" EnableRowNumber="false" EnableRowNumberPaging="false" EnableTextSelection="false"
         DataKeyNames="StudentID"  AllowSorting="true" SortColumnIndex="0"  AllowPaging="true" OnPageIndexChange ="GridProjectStudent_PageIndexChange"  OnRowCommand ="GridProjectStudent_RowCommand" 
         SortDirection="DESC"  IsDatabasePaging="true"  >
        <Toolbars>
        <x:Toolbar ID="Toolbar2" runat="server" Width ="800px">
            <Items>
                <x:Label ID="Label1" runat="server" Label="Label" Width ="15" Text=" "></x:Label>
                <x:Label ID="Label2" runat="server" Label="Label" Text="查询条件："></x:Label>
                <x:Label ID="Label3" runat="server" Label="Label" Width ="15" Text=" "></x:Label>

                <x:DropDownList ID="DropDownStudentType" ShowLabel="false" AutoPostBack="true" Width ="100px" runat="server" TabIndex ="1" OnSelectedIndexChanged ="DropDownStudentType_SelectedIndexChanged" >
                     <x:ListItem Text="全部" Value="全部" Selected ="true" />
                     <x:ListItem Text="在读" Value="在读" /> 
                    <x:ListItem Text="毕业" Value="毕业" />     
                    <x:ListItem Text="指导教师" Value="指导教师" />     
                    <x:ListItem Text="姓名" Value="姓名" />        
                    <x:ListItem Text="专业" Value="专业" />
                    <x:ListItem Text="入学年份" Value="入学年份" />
                    <x:ListItem Text="毕业年份" Value="毕业年份" />
                    <x:ListItem Text="所属部门" Value="所属部门" />
                    <x:ListItem Text="学生类型" Value="学生类型" />                            
                </x:DropDownList>
                 <x:DropDownList ID="DropDownList" ShowLabel="false" AutoPostBack="true" runat="server" TabIndex ="3" Width ="150px">
                </x:DropDownList>
              <%--  <x:Label ID="Label5" runat="server" Label="Label" Width ="30" Text=" "></x:Label>--%>

                       <x:TwinTriggerBox runat="server" EmptyText="请输入搜索内容" MaxLength ="20" ID="TriggerBox" ShowTrigger1="false" TabIndex ="3" ShowLabel="true" Label ="学生姓名"
                    ShowTrigger2 ="false" >
                </x:TwinTriggerBox>                
                <x:Button ID="FindDevoteTime" Text="搜索" Icon="SystemSearch" Type ="Submit"  runat="server" OnClick ="FindDevoteTime_Click" >
                </x:Button>
                <x:Button ID="btnRefresh" runat="server" Icon="ArrowRotateClockwise"  Text="刷新" OnClick ="btnRefresh_Click" >  
                                    </x:Button>      
                <x:Button ID="btnAddProject" Text="新增学生信息" Icon="Add" runat="server">
                </x:Button>
                <x:Button ID="btnDelete" Text="删除选中行" Icon="Delete" runat="server" ConfirmText ="确定删除？" ConfirmTarget ="Top"  OnClick ="btnDelete_Click"  >
                </x:Button>
                <x:Button ID="btnUpdate" Text="编辑选中行" Icon="Pencil"  runat="server" OnClick ="btnUpdate_Click"  >
                </x:Button>
                 <x:Button ID="btn_Get" Text="导出Excel文件" ConfirmText="确定导出？" Icon="Disk" EnablePostBack="true" runat="server" OnClick ="btn_Get_Click"   EnableAjax="false" DisableControlBeforePostBack="false">
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
                        <x:DropDownList ID="ddlGridPageSize" Width="80px" AutoPostBack="true"  OnSelectedIndexChanged ="ddlGridPageSize_SelectedIndexChanged" 
                            runat="server">
                            <x:ListItem Text="10" Value="10" />
                            <x:ListItem Text="20" Value="20" Selected="true" />
                            <x:ListItem Text="30" Value="30" />
                            <x:ListItem Text="50" Value="50" />
                        </x:DropDownList>
                    </PageItems>
       <%--DataKeyNames这是数据库唯一标识--%>     
       <%--DataKeyNames这是数据库唯一标识--%>
       <Columns>  
           
           <x:TemplateField Width="30px" >
                    <ItemTemplate>
                        <asp:Label ID="Label8" runat="server" Text='<%# RowNumber(Container.DataItemIndex + 1) %>'></asp:Label>
                    </ItemTemplate>
                
                </x:TemplateField>
           <x:CheckBoxField ID="CBoxSelect" CommandName="CBSelect" DataField="Project" runat="server" AutoPostBack="true" Enabled="true" RenderAsStaticField="false"  ShowHeaderCheckBox="true" Width="30"  /> 
          
           <x:BoundField Enabled="true" DataField="EntryPerson" SortField="EntryPerson" Hidden="true" /> 
            <x:TemplateField Width="30px" Hidden ="true" >
                    <ItemTemplate>
                        <asp:Label ID="Label10" runat="server" Text='<%# RowNumber(Container.DataItemIndex + 1) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>     
             <x:BoundField DataField="Sno" HeaderText="学号" />
           <x:BoundField DataField="Sname" HeaderText="姓名" />      
           <x:TemplateField Width="80px" HeaderText="性别">
                    <ItemTemplate>
                        <asp:Label ID="Label7" runat="server" Text='<%# getgender(Convert.ToString(DataBinder.Eval(Container.DataItem, "Sex"))) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>
           <x:BoundField DataField="DocumentType" HeaderText="证件类型" />     
           <x:BoundField DataField="DocumentNumber" HeaderText="证件号码" /> 
            <x:BoundField DataField="Contact" HeaderText="联系方式" /> 
                     <x:BoundField DataField="Type" HeaderText="学生类型" /> 
            <x:TemplateField Width="80px" HeaderText="指导教师">
                    <ItemTemplate>
                        <asp:Label ID="Label6" runat="server" Text='<%# UserName(Convert.ToInt32 (DataBinder.Eval(Container.DataItem, "UserInfoID"))) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>       
           <x:BoundField DataField="Specialty" HeaderText="专业" />     
            <x:BoundField DataField="SResearch" HeaderText="研究方向" />      
                   <x:TemplateField Width="80px" HeaderText="所属部门">
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# FindAgency(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "StudentID"))) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>                                           
           <x:BoundField DataField="EnterTime" DataFormatString="{0:yyyy-MM-dd}" HeaderText="入学时间" /> 
           <x:TemplateField Width="80px" HeaderText="是否毕业">
                    <ItemTemplate>
                        <asp:Label ID="Label9" runat="server" Text='<%# IsNullGraduation(Convert.ToString(DataBinder.Eval(Container.DataItem, "IsGraduation"))) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>                         
           <x:BoundField DataField="GraduationTime" DataFormatString="{0:yyyy-MM-dd}" HeaderText="毕业时间" />  
                <x:BoundField DataField="SGraduationDirection" HeaderText="毕业去向单位" />                                          
            <x:TemplateField Width="80px" HeaderText="保密等级">
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# SecrecyLevelName(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "SecrecyLevel"))) %>'></asp:Label>
                    </ItemTemplate> 
                </x:TemplateField>  
       </Columns>
       </x:Grid>
              </Items>
       </x:Panel>
        <x:Window ID="WindowStudent"  Popup="false" EnableIFrame="true"  runat="server"
            EnableMaximize="false" EnableResize="false" Height="450px" Width="800px" Title ="添加学生基本信息">
        </x:Window>
        <x:Window ID="WindowUpdate"  Popup="false" EnableIFrame="true"  runat="server"
            EnableMaximize="false" EnableResize="false" Height="450px" Width="800px" Title ="编辑学生基本信息">
        </x:Window>
        <x:Window ID="Window2" Title="查询" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableResize="false" Target="Parent" 
            IsModal="True" Width="750px" Height="450px">
        </x:Window>
        <x:Label ID="labResult" Visible="false" runat="server">
        </x:Label>
    </form>
</body>
</html>