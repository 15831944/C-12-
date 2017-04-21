<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HonorTitle.aspx.cs" Inherits="WDFramework.People.honorarytitle" %>

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
            Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" EnableCollapse="true"
            ShowHeader="false" >
        <Items>
        
         <x:Grid ID="GridHonor" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
         EnableCheckBoxSelect="false" EnableRowNumber="false" EnableRowNumberPaging="true" EnableTextSelection="true"
         DataKeyNames="HonorID" AllowSorting="true" SortColumnIndex="0"
         SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" OnRowCommand ="GridHonor_RowCommand" OnPageIndexChange ="GridHonor_PageIndexChange"   >
        <Toolbars>
        <x:Toolbar ID="Toolbar1" runat="server"  Width="1300px" >
            <Items>
                <x:Label ID="Label1" runat="server" Label="Label" Width ="5" Text=" "></x:Label>
                <x:Label ID="Label2" runat="server" Label="Label" Text="查询条件："></x:Label>
                <x:Label ID="Label3" runat="server" Label="Label" Width ="5" Text=" "></x:Label>

                <x:DropDownList ID="DropDownListFind" ShowLabel="false" AutoPostBack="true" Width ="100px" runat="server" TabIndex ="1" OnSelectedIndexChanged ="DropDownListFind_SelectedIndexChanged" >
                     <x:ListItem Text="全部" Value="全部" Selected ="true" />
                    <x:ListItem Text="人员姓名" Value="人员姓名" />
                    <x:ListItem Text="授予时间" Value="授予时间" />                    
                    <x:ListItem Text="授予部门" Value="授予部门" />
                     <x:ListItem Text="级别名称" Value="级别名称" />
                </x:DropDownList>

                <x:Label ID="Label4" runat="server" Label="Label" Width ="5" Text=" "></x:Label>

               <x:TwinTriggerBox runat="server" EmptyText="请输入搜索条件" MaxLength ="20" Width ="150px" ShowLabel="false" ID="TBNameandAgency" ShowTrigger1="false" TabIndex ="2"
                    ShowTrigger2 ="false" >
                </x:TwinTriggerBox>
                 <x:Label ID="Label9" runat="server" Label="Label" Width ="5" Text=" "></x:Label>
               <x:DropDownList ID="DropDownListYearandLevel" ShowLabel="false" AutoPostBack="true" runat="server" TabIndex ="3" Width ="150px">
                </x:DropDownList>
               <x:Button ID="FindDevoteTime" Text="搜索" Icon="SystemSearch" Type ="Submit"   runat="server" OnClick ="FindDevoteTime_Click"   >
                </x:Button>
                <x:Button ID="btnRefresh" runat="server" Icon="ArrowRotateClockwise"  Text="刷新" OnClick ="btnRefresh_Click" >  
                                    </x:Button> 
               <%-- <x:Label ID="Label4" runat="server" Label="Label" Width ="20" Text=" "></x:Label>    --%>        
                <x:Button ID="btnAddProject" Text="新增人员荣誉称号信息" Icon="Add" runat="server"    >
                </x:Button>

                 <x:Button ID="btnSelect_All" runat="server" Text="全选" OnClick="btnSelect_All_Click"></x:Button>
                <x:Button ID="btnDelete" Text="删除选中行" Icon="Delete" runat="server" OnClick ="btnDelete_Click" >
                </x:Button>
                <x:Button ID="btnUpdate" Text="编辑选中行" Icon="Pencil"  runat="server" OnClick ="btnUpdate_Click" >
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
                        <asp:Label ID="Label7" runat="server" Text='<%# RowNumber(Container.DataItemIndex + 1) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>
                       <x:TemplateField Width="80px" HeaderText="姓名">
                    <ItemTemplate>                        
                        <asp:Label ID="Label5" runat="server" Text='<%# UserName(Convert .ToInt32 (DataBinder.Eval(Container.DataItem, "UserInfoID"))) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>   
       <x:BoundField DataField="TitleName" HeaderText="称号名称" />
                  <x:BoundField DataField="GivDivision" HeaderText="授予部门" /> 
       <x:BoundField DataField="GiveTime" DataFormatString="{0:yyyy-MM-dd}" HeaderText="授予时间" />
                  <x:BoundField DataField="Sort" HeaderText="级别" />
            <x:TemplateField Width="80px" HeaderText="保密等级">
                    <ItemTemplate>
                        <asp:Label ID="Label6" runat="server" Text='<%# SecrecyLevelName(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "SecrecyLevel"))) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField> 
            <x:BoundField DataField="Remark" HeaderText="备注" Hidden ="true"  />              
      
              <x:TemplateField HeaderText="备注" Width="60px" >
                           <ItemTemplate>
                        <a href="javascript:<%# GetEditUrl(Eval("HonorID ")) %>">详情</a>
                          </ItemTemplate>
                          </x:TemplateField>
       </Columns>
       </x:Grid>
         </Items>        
       </x:Panel>
         <x:Window ID="WindowHonor"  Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableResize="false" Height="400px" Width="450px" Title="添加人员荣誉称号">
        </x:Window>
         <x:Window ID="WindowUpdate"  Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableResize="false" Height="400px" Width="450px" Title="编辑人员荣誉称号">
        </x:Window>
         <x:Window ID="Remark" Popup="false" EnableIFrame="true"  runat="server"
            EnableMaximize="false" EnableResize="false" Height="250px" Width="350px" >
        </x:Window>
    </form>
</body>

</html>
