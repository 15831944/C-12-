<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FindLoginPWD.aspx.cs" Inherits="WDFramework.People.LoginPWD.FindLoginPWD" %>



<%@ Register assembly="FineUI" namespace="FineUI" tagprefix="x" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta name="sourcefiles" content="~/People/Add_StaffInfos.aspx;~/People/Update_StaffInfos.aspx" />
</head>
<body oncontextmenu='return false' ><%--取消鼠标右键的点击--%>
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <x:Panel ID="Panel1" runat="server" BodyPadding="5px" EnableBackgroundColor="true"
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start"
            ShowHeader="false" Title="用户管理">
        <Items>
            <x:Grid ID="People_Info" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
         EnableCheckBoxSelect="false" EnableRowNumber="false" EnableRowNumberPaging="true" EnableTextSelection="true"
         DataKeyNames="UserInfoID" AllowSorting="true" SortColumnIndex="0" SortDirection="DESC" 
         AllowPaging="true" IsDatabasePaging="true" AllowCellEditing="true" OnRowCommand ="People_Info_RowCommand" OnPageIndexChange ="People_Info_PageIndexChange" >
        <Toolbars>
        <x:Toolbar ID="Toolbar_top" runat="server" Width ="800px">
            <Items>    
                 <x:Label ID="Label9" runat="server" Label="Label" Text="人员姓名："></x:Label>  
                 <x:TextBox runat="server" EmptyText="输入要搜索的关键词" ID="TriggerBox"  TabIndex ="1"
                        MaxLength ="20" MaxLengthMessage ="字数过长" ShowLabel="true" Label ="人员姓名">
                                         </x:TextBox>     
                 <x:Button ID="FindObjectAll" Text="搜索" Icon="SystemSearch" Type ="Submit"   runat="server"  OnClick ="FindObjectAll_Click"  ValidateForms ="Panel1"   >
                </x:Button>
                 <x:Button ID="btnRefresh" runat="server" Icon="ArrowRotateClockwise"  Text="刷新" OnClick ="btnRefresh_Click" >  
                                    </x:Button> 
                 <x:Button ID="btnResetPWD" runat="server" Text="重置人员密码" ConfirmText ="确定重置？" OnClick ="btnResetPWD_Click"   >  
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
            <x:CheckBoxField ID="CBoxSelect" CommandName="CBSelect" DataField="User" runat="server" AutoPostBack="true" Enabled="true" RenderAsStaticField="false" Width="30" /> 
            <x:BoundField Enabled="true" DataField="EntryPerson" SortField="EntryPerson" Hidden="true" /> 
                      <x:BoundField Width="80px" DataField="LoginName" SortField ="LoginName" HeaderText="用户登录名"  />             
           <x:BoundField Width="100px" DataField="UserName" SortField ="UserName"  HeaderText="姓名" />                    
                <x:TemplateField Width="80px" HeaderText="性别">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# getgender(Convert.ToString(DataBinder.Eval(Container.DataItem, "Sex"))) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>
           <x:BoundField Width="80px" DataField="Nation" SortField="Nation" HeaderText="民族" />
           <x:BoundField Width="80px" DataField="Hometown" SortField="Hometown" HeaderText="籍贯" />
           <x:BoundField Width="80px" DataField="Birth" SortField="Birth" DataFormatString="{0:yyyy-MM-dd}" HeaderText="出生年月日" /> 
           <x:BoundField Width="80px" DataField="AgencyName" SortField="AgencyName"  HeaderText="所属机构名称" />                             
           <x:BoundField Width="80px" DataField="JobTitle" SortField="JobTitle" HeaderText="职称" />  
           <x:BoundField Width="80px" DataField="TeleNum" SortField="TeleNum" HeaderText="手机号码" />               
       </Columns>
       </x:Grid>
       </Items>
       </x:Panel>
        <x:Window ID="WindowADD" Popup="false" EnableIFrame="true"  runat="server"
            EnableMaximize="true" EnableResize="true" Height="450px" Width="800px" Title ="添加人员基本信息">
        </x:Window>
         <x:Window ID="WindowUpdate" Popup="false" EnableIFrame="true"  runat="server"
            EnableMaximize="true" EnableResize="true" Height="450px" Width="800px" Title ="编辑人员基本信息">
        </x:Window>

        <x:Window ID="WindowPWD" Title="重置人员密码" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="true" EnableResize="true" Target="Parent" 
            IsModal="True" Width="450px" Height="400px">
        </x:Window>
         <x:Window ID="Remark" Popup="false" EnableIFrame="true"  runat="server"
            EnableMaximize="true" EnableResize="true" Height="250px" Width="350px" >
        </x:Window>
        <x:Label ID="labResult" Visible="false" runat="server">
        </x:Label>
    </form>
</body>
</html>


