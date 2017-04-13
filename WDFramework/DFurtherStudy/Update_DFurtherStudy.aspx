<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Update_DFurtherStudy.aspx.cs" Inherits="WebApplication1.Update_DFurtherStudy" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <x:PageManager ID="PageManager1" AutoSizePanelID="Panel2" runat="server" />
               
               <x:Panel ID="Panel2" runat="server" Height="380px" ShowBorder="True"  EnableCollapse="true"
                Layout="HBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5"
                BoxConfigChildMargin="0 5 0 0" ShowHeader="false">
                    
                <Items>

   
                <x:Panel ID="Panel3" Title="项目概要" BoxFlex="1" runat="server"
                 BodyPadding="5px" ShowBorder="false" BoxMargin="0 0 0 40" ShowHeader="false"  ColumnWidth="100%">
                       <Items>
                          
                             <x:Panel ID="Panel12" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                              Layout="Column" runat="server">
                                        <Items>
                                        <x:Label ID="lUser" Width="100px" runat="server"   ShowLabel="true" Text="人员姓名：">
                                        </x:Label>                        
                                        <x:TextBox ID="tUser" ShowLabel="true" Label="人员姓名"  Required="true"   Width="200px" TabIndex="1" CssClass="marginr" runat="server" MaxLength="10" MaxLengthMessage="最多可输入10个字符" AutoPostBack="true" OnTextChanged="tUser_TextChanged">
                                        </x:TextBox>
                                           
                                        </Items>                
                            </x:Panel>          
                                   
                      <x:Label ID="Label16" runat="server" Label="Label" Text=" " Height ="20px" >
                            </x:Label><%--这是空行--%>  
                     
                          <x:Panel ID="Panel1" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                              Layout="Column" runat="server">
                                        <Items>
                                        <x:Label ID="lStudyPlace" Width="100px" runat="server" CssClass="marginr" ShowLabel="false"     Text="进修地点：">
                                        </x:Label>                        
                                        <x:TextBox ID="tStudyPlace" MaxLength="50" ShowLabel="true" Label="进修地点" Required="true" Width="200px" CssClass="marginr" TabIndex="2" runat="server" MaxLengthMessage="最多可输入50个字符" AutoPostBack="true" >
                                         </x:TextBox>
                                        </Items>                
                            </x:Panel>
                      <x:Label ID="Label2" runat="server" Label="Label" Text=" " Height ="20px" >
                            </x:Label><%--这是空行--%>
     
                             <x:Panel ID="Panel5" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                              Layout="Column" runat="server">
                                        <Items>
                                        <x:Label ID="lStudySchool" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" ShowRedStar="true" Text="进修学校：">
                                        </x:Label>                        
                                        <x:TextBox ID="tStudySchool"  MaxLength="30" ShowLabel="true" Label="进修学校" Required="true" Width="200px"   CssClass="marginr" TabIndex="3" runat="server" MaxLengthMessage="最多可输入30个字符" AutoPostBack="true" >
                                        </x:TextBox>
                                        </Items>                
                            </x:Panel>

                                 <x:Label ID="Label1" runat="server" Label="Label" Text=" " Height ="20px" >
                            </x:Label><%--这是空行--%>
                      
          
                
                             <x:Panel ID="Panel6" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                              Layout="Column" runat="server">
                                        <Items>
                                        <x:Label ID="lDBegainTime" Width="100px" runat="server" CssClass="marginr" ShowLabel="false"  ShowRedStar="true" Text="进修开始时间：">
                                        </x:Label>                        
                                        <x:DatePicker ID="tDBegainTime" runat="server" Width="200px" Label="进修开始时间" Required="true" TabIndex="4" EnableEdit="false" >
                                        </x:DatePicker>

                                        </Items>                
                            </x:Panel>
                            
                           <x:Label ID="Label18" runat="server" Label="Label" Text=" " Height ="20px" >
                           </x:Label><%--这是空行--%>
                            <x:Panel ID="Panel8" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                              Layout="Column" runat="server">
                                        <Items>
                                        <x:Label ID="lDEndTime" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" ShowRedStar="true" Text="进修结束时间：">
                                        </x:Label>                        
                                        <x:DatePicker ID="tDEndTime" runat="server" EnableEdit="false"  Width="200px" Label="结束时间（大于开始时间）" TabIndex="5" AutoPostBack="true" >
                                        </x:DatePicker>
                                        </Items>                
                            </x:Panel>

                            <x:Label ID="Label11" runat="server" Label="Label" Text=" " Height ="20px" >
                            </x:Label><%--这是空行--%>

                            <x:Panel ID="Panel7" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                              Layout="Column" runat="server">
                                        <Items>
                                        <x:Label ID="SecrecyLevel" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="保密级别：">
                                        </x:Label>                                      
                                        <x:DropDownList ID="dSecrecyLevel"  ShowLabel="true" Label="保密级别" Required="true" Width="195px" TabIndex="6"  runat="server">
                                                             
                                        </x:DropDownList>                            
                                        <%--<x:TextBox ID="SecrecyLevel1" ShowLabel="false" Required="true" Width="150px" CssClass="marginr" runat="server" TabIndex="13">
                                         </x:TextBox>--%>
                                        </Items>                
                            </x:Panel>                                                 
                           
                            <x:Label ID="Label3" runat="server" Label="Label" Text=" " Height ="20px" >
                            </x:Label><%--这是空行--%>

                            <x:Panel ID="Panel9" ShowHeader="false" CssClass="formitem" ShowBorder="false" 
                              Layout="Column" runat="server">
                                        <Items>
                                        <x:Label ID="lContent" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="进修内容：">
                                        </x:Label>
                                        <x:TextArea runat="server" MaxLength="20" Label="进修内容" ID="tContent" Required="true" Width="200px" Height="100px" TabIndex="7" AutoGrowHeight="false" CssStyle ="overflow-y:scroll"  AutoGrowHeightMax="100" 
                                            MaxLengthMessage="最多可输入20个字符" AutoPostBack="true" >
                                        </x:TextArea>                        
                                        <%--<x:TextBox ID="VisitContent2" ShowLabel="false" Required="true" Width="150px" CssClass="marginr" runat="server">
                                        </x:TextBox>--%>
                                        </Items>                
                            </x:Panel>
                            
                    </Items>  
                           
                    </x:Panel>
                     </Items>  
                    </x:Panel>
                    <x:Panel ID="Panel4" ShowHeader="false" CssClass="formitem" ShowBorder="false" Layout="Column" runat="server">
                         <Items>
                         <x:Toolbar ID="Toolbar1" runat="server" ColumnWidth="100%" >
                            <Items>
                            <x:Label ID="Label15"  Width="150px" runat="server"  ShowLabel="true" Text=" ">
                            </x:Label>   
                            <x:Button ID="Save"  runat="server" CssClass="marginr" Type="Submit"  Text="保存" Icon="Add" Size="Medium" ValidateForms="Panel2" ConfirmText="确定保存？" OnClick="Save_Click">
                            </x:Button> 
                                 <x:Button ID="DeleteAll"  runat="server" CssClass="marginr" Icon="Delete"  Text="重置" Size="Medium" ConfirmText="确定重置？" OnClick="DeleteAll_Click"  >
                            </x:Button>                        
                        </Items>
                        </x:Toolbar>
                    </Items>                            
                    </x:Panel>
    </div>
    </form>
</body>
</html>
