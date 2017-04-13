<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add_BasicCode.aspx.cs" Inherits="WDFramework.People.ManageBasicCode.Add_BasicCode" %>

<%@ Register assembly="FineUI" namespace="FineUI" tagprefix="x" %>
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
            

<%--Height="350px" Width="850px"  --%>
         <x:Panel ID="Panel2" runat="server"  ShowBorder="True"  EnableCollapse="true" AutoScroll="true" 
         Layout="Column" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5"   Height ="320px"
         BoxConfigChildMargin="0 5 0 0" ShowHeader="false" >
         <Items> 
              
                <x:Panel ID="Panel3" Title="项目概要" BoxFlex="1" runat="server"
                        BodyPadding="10px" ShowBorder="false" ShowHeader="false" ColumnWidth="100%">
                        <Items>
                          <x:Label ID="Label2" runat="server" Label="Label" Text=" " Height="30px">
                            </x:Label>
                            <%--这是空行--%>
                            <x:Panel ID="Panel45" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label62" Width="70px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="分">
                                    </x:Label>
                                    <x:Label ID="Label6" Width="80px" CssStyle="text-align:legt" runat="server" CssClass="marginr" ShowLabel="false" Text="类名称：">
                                    </x:Label>
                                    <x:DropDownList Label=""   AutoPostBack="false" Required="true" EnableSimulateTree="true" TabIndex ="1"
                    ShowRedStar="true" runat="server" ID="DropDownListCategoryName" Width="195px">                                           
                </x:DropDownList>              
                                </Items>
                            </x:Panel>
                            <x:Label ID="Label1" runat="server" Label="Label" Text=" " Height="30px">
                            </x:Label>
                            <%--这是空行--%>
                            <x:Panel ID="Panel6" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label4" Width="70px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="分">
                                    </x:Label>
                                    <x:Label ID="Label7" Width="80px" CssStyle="text-align:legt" runat="server" CssClass="marginr" ShowLabel="false" Text="类内容：">
                                    </x:Label>
                                     <x:TextBox ID="CategoryContent" MaxLength="100"  MaxLengthMessage ="最多输入100个字符"  ShowLabel="true" Label="分类内容" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex ="2">
                                        </x:TextBox> 
                                </Items>
                            </x:Panel>          
                       </Items>               
                 </x:Panel> 
              
          </Items>        
          </x:Panel>
             <x:Panel ID="Panel87" runat="server" Height="40px"  ShowBorder="True"  EnableCollapse="true"
                  BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5"   
               BoxConfigChildMargin="0 5 0 0" ShowHeader="false" Width ="750px">
              <Items>
                  <x:Toolbar ID="Toolbar1" runat="server"  >
                      <Items>
                          <x:Label ID="Label12" runat="server" Label="Label" Text=" "  Width ="150px"  ></x:Label>
                          <x:Button ID="Save"  Text="保存" runat="server" Size="Medium" Icon ="Add" ConfirmText="确定保存？" ConfirmTarget="Top" OnClick ="Save_Click"  ValidateForms ="Panel2" Type ="Submit" >
                          </x:Button>
                          <x:Button ID="Reset" Text="重置" runat="server" Size="Medium" Icon ="Delete" ConfirmText="确定重置？" ConfirmTarget="Top" OnClick ="Reset_Click" >
                          </x:Button>
                      </Items>
                  </x:Toolbar>
              </Items>
          </x:Panel> 
            </Items>
            </x:Panel>
          <x:Window ID="WindowStaff" Popup="false" EnableIFrame="true" IFrameUrl="#" runat="server"
            EnableMaximize="true" EnableResize="true" Height ="450px"  Width="750px" Title="添加人员">
        </x:Window>              
    </form>
</body>
</html>
