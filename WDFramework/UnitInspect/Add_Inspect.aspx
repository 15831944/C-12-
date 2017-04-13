<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add_Inspect.aspx.cs" Inherits="WebApplication1.新增考察信息页面" %>

<!DOCTYPE html>



<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script  type="text/javascript">
        function clearFile() {
            var obj = document.getElementById('fileupload');
            obj.outerHTML = obj.outerHTML;
        }
    </script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <x:PageManager ID="PageManager1" AutoSizePanelID="Panel2" runat="server" />
               
               <x:Panel ID="Panel2" runat="server" Height="290px" Width="734px" ShowBorder="false"  EnableCollapse="true"
                Layout="HBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5"
                BoxConfigChildMargin="0 5 0 0" ShowHeader="false">
                    
                <Items>

   
                <x:Panel ID="Panel3" Title="项目概要" BoxFlex="1" runat="server"
                 BodyPadding="5px" ShowBorder="false" BoxMargin="30 0 0 40" ShowHeader="false " ColumnWidth="50%">
                       <Items>
                             <x:Panel ID="Panel12" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                              Layout="Column" runat="server">
                                        <Items>
                                        <x:Label ID="lInspectName" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="姓名：">
                                        </x:Label>                        
                                        <x:TextBox ID="tInspectName" MaxLength="10" MaxLengthMessage="最多可输入10个字符" ShowLabel="true" Label="姓名" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="1" AutoPostBack="true" >
                                        </x:TextBox>
                                        </Items>                
                            </x:Panel>
                           <x:Label ID="Label11" runat="server" Label="Label" Text=" " Height ="20px" >
                            </x:Label><%--这是空行--%>
                              <x:Panel ID="Panel14" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                              Layout="Column" runat="server">
                                        <Items>
                                        <x:Label ID="lWorkPlace" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="工作单位：">
                                        </x:Label>                        
                                        <x:TextBox ID="tWorkPlace" MaxLength="40" MaxLengthMessage="最多可输入40个字符" ShowLabel="true" Label="工作单位" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="2" AutoPostBack="true" >
                                         </x:TextBox>
                                        </Items>                
                            </x:Panel>
                            <x:Label ID="Label2" runat="server" Label="Label" Text=" " Height ="20px" >
                            </x:Label><%--这是空行--%>

                           <x:Panel ID="Panel1" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                              Layout="Column" runat="server">
                                        <Items>
                                        <x:Label ID="lDuty" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="职称/职务">
                                        </x:Label>                        
                                        <x:TextBox ID="tDuty" MaxLength="20" MaxLengthMessage="最多可输入20个字符" ShowLabel="true" Label="职称/职务"  Width="200px" CssClass="marginr" runat="server" TabIndex="3" AutoPostBack="true" >
                                        </x:TextBox>
                                        </Items>                
                            </x:Panel>
                           <x:Label ID="Label18" runat="server" Label="Label" Text=" " Height ="20px" >
                           </x:Label><%--这是空行--%>
                           <x:Panel ID="Panel10" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                              Layout="Column" runat="server">
                                        <Items>
                                        <x:Label ID="InspectTime" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="时间：">
                                        </x:Label>                        
                                        <x:DatePicker ID="dInspectTime" runat="server" Width="196px" EnableEdit="false" ShowLabel="true" Label="时间" Required="true" TabIndex="4" AutoPostBack="true" >
                                        </x:DatePicker>
                                        </Items>                
                            </x:Panel>
                            <x:Label ID="Label10" runat="server" Label="Label" Text=" " Height ="20px" >
                           </x:Label><%--这是空行--%>
                          <x:Panel ID="Panel22" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                              Layout="Column" runat="server">
                                        <Items>
                                               <x:Label ID="lAgency" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="参观部门：">
                                        </x:Label>  
                                           <x:DropDownList Label="参观部门"  AutoPostBack="false" Required="true" TabIndex="5" EnableSimulateTree="true"
                                         runat="server" ID="DropDownListAgency" Width="196px">
                                          </x:DropDownList>
                                        </Items>                
                            </x:Panel>
                            <x:Label ID="Label3" runat="server" Label="Label" Text=" " Height ="20px" >
                            </x:Label><%--这是空行--%>


                            <x:Panel ID="Panel5" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                              Layout="Column" runat="server">
                                        <Items>
                                        <x:Label ID="Label17" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="保密级别：">
                                        </x:Label>                                      
                                        <x:DropDownList ID="dSecrecyLevel"  ShowLabel="true" Label="保密级别" TabIndex="6" Required="true" Width="196px" AutoPostBack="true" runat="server" >
                                                        
                                        </x:DropDownList>
                           </Items>                
                            </x:Panel>
                         
                                                                                                                                                  
                          </Items>                
                            </x:Panel> 
                 <x:Panel ID="Panel6" Title="项目概要" BoxFlex="1" runat="server"
                 BodyPadding="5px" ShowBorder="false" BoxMargin="30 0 0 40" ShowHeader="false" ColumnWidth="50%">     

                        <Items>   
                           
                            <x:Panel ID="Panel21" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                              Layout="Column" runat="server">
                                        <Items>                                                                               
                                        <x:Label ID="VisitContent" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="参观内容：">
                                        </x:Label>
                                        <x:TextArea runat="server" MaxLength="200" MaxLengthMessage="最多可输入200个字符" ShowLabel="true" Label="参观内容" Required="true" ID="tVisitContent" Width="200px" CssStyle ="overflow-y:scroll" Height="230px" AutoGrowHeight="false" AutoGrowHeightMax="230px"  TabIndex="7">
                                        </x:TextArea>                        
                                      
                                        </Items>                
                            </x:Panel>                                                                                                                         
                                  </Items>               
                            </x:Panel>                
                         </Items>        
                    </x:Panel>     
                   <asp:Panel ID="Panelasp" ShowHeader="false"  ShowBorder="false"
                Layout="Column" runat="server" Height="85px" BackColor="White">
                  <asp:Label ID="Label1" runat="server" Label="Label" CssClass="marginr" Text=" " Width="45px">
            </asp:Label>  
            <asp:Label ID="Label20" runat="server" Label="Label" CssClass="marginr" Text="相关文档： " Width="95px">
            </asp:Label>                       
            <input type="file"  id="fileupload" style="width:200px" runat="server"/>        
         </asp:Panel>       
                    <x:Panel ID="Panel4" ShowHeader="false" CssClass="formitem" ShowBorder="false" Layout="Column" runat="server">
                         <Items>
                        <x:Toolbar ID="Toolbar1" runat="server" ColumnWidth="100%" >
                            <Items>
                            <x:Label ID="Label15"  Width="310px" runat="server"  ShowLabel="true" Text=" ">
                            </x:Label>   
                            <x:Button ID="Save"  runat="server" CssClass="marginr" Type="Submit" Size="Medium" Text="保存" Icon="Add" OnClick="Save_Click" ValidateForms="Panel2" ConfirmText="确定保存？">
                            </x:Button>                        
                           <x:Button ID="DeleteAll"  runat="server" CssClass="marginr"  Text="重置"  ConfirmText="确定重置？" Size="Medium" Icon="Delete" OnClick="DeleteAll_Click" >
                            </x:Button>    
                        </Items>
                        </x:Toolbar>
                    </Items>                            
                            </x:Panel>
                                
    </div>
    </form>
</body>
</html>
