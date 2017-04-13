<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Revise_AchieveAward.aspx.cs" Inherits="WebApplication1.Revise_AchieveAward" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <x:PageManager ID="PageManager1" AutoSizePanelID="Panel2" runat="server" />
               
               <x:Panel ID="Panel2" runat="server" Height="330px" ShowBorder="True"  EnableCollapse="true"
                Layout="HBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5"
                BoxConfigChildMargin="0 5 0 0" ShowHeader="false" AutoScroll="true">
                    
                <Items>

   
                <x:Panel ID="Panel3" Title="项目概要" BoxFlex="1" runat="server"
                 BodyPadding="5px" BoxMargin="5 0 0 45" ShowBorder="false" ShowHeader="false"  ColumnWidth="100%" AutoScroll="true">
                       <Items>
                          
                            <x:Panel ID="Panel14" ShowHeader="false" CssClass="formitem"  ShowBorder="false"
                              Layout="Column" runat="server">
                                        <Items>
                                        <x:Label ID="Achievement" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="成果名称：">
                                        </x:Label>                        
                                        <x:TextBox ID="tAchievement" MaxLength="100" MaxLengthMessage="最多可输入100个字符" ShowLabel="true" Label="成果名称" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="1" AutoPostBack="true" OnTextChanged="tAchievement_TextChanged">
                                        </x:TextBox>
                                        </Items>                
                            </x:Panel>                        
                                   
                      <x:Label ID="Label16" runat="server" Label="Label" Text=" " Height ="20px" >
                            </x:Label><%--这是空行--%>  
                     
                            <x:Panel ID="Panel1" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                              Layout="Column" runat="server">
                                        <Items>
                                        <x:Label ID="AwardName" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="报奖名称：">
                                        </x:Label>                        
                                        <x:TextBox ID="tAwardName" MaxLength="100" MaxLengthMessage="最多可输入100个字符" ShowLabel="true" Label="报奖名称" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="2">
                                        </x:TextBox>
                                        </Items>                
                            </x:Panel>   
                      <x:Label ID="Label2" runat="server" Label="Label" Text=" " Height ="20px" >
                            </x:Label><%--这是空行--%>
     
                              <x:Panel ID="Panel5" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="AwardUnit" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="报奖单位：">
                                    </x:Label>
                                    <x:DropDownList Label="报奖单位" AutoPostBack="false" EnableEdit="false" Required="true" EnableSimulateTree="true" TabIndex="3"
                                        runat="server" ID="DropDownListAgency" Width="200px">
                                    </x:DropDownList>
                                </Items>
                            </x:Panel>


                          <x:Label ID="Label1" runat="server" Label="Label" Text=" " Height ="20px" >
                            </x:Label><%--这是空行--%>    
                   
                            <x:Panel ID="Panel13" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                              Layout="Column" runat="server">
                                        <Items>
                                        <x:Label ID="AwardType" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="报奖类别：">
                                        </x:Label>                        
                                        <x:TextBox ID="tAwardType" MaxLength="20" MaxLengthMessage="最多可输入20个字符" ShowLabel="true" Label="报奖类别" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="4">
                                        </x:TextBox>
                                        </Items>                
                            </x:Panel>
                            
                           <x:Label ID="Label18" runat="server" Label="Label" Text=" " Height ="20px" >
                           </x:Label><%--这是空行--%>
                           <x:Panel ID="Panel10" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                                        <Items>
                                        <x:Label ID="AwardGrade" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="报奖等级：">
                                        </x:Label>                        
                                        <x:TextBox ID="tAwardGrade" MaxLength="10" MaxLengthMessage="最多可输入10个字符" ShowLabel="true" Label="报奖等级" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="5">
                                        </x:TextBox>
                                        </Items>                
                            </x:Panel>

                            <x:Label ID="Label11" runat="server" Label="Label" Text=" " Height ="20px" >
                            </x:Label><%--这是空行--%>

                            <x:Panel ID="Panel7" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                              Layout="Column" runat="server">
                                        <Items>
                                        <x:Label ID="SecrecyLevel" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="保密级别：">
                                        </x:Label>                                      
                                        <x:DropDownList ID="dSecrecyLevel"  ShowLabel="true" Label="保密级别" Required="true" Width="195px" AutoPostBack="true" runat="server" TabIndex="6">
                                                             
                                        </x:DropDownList>                            
                                        <%--<x:TextBox ID="SecrecyLevel1" ShowLabel="false" Required="true" Width="150px" CssClass="marginr" runat="server" TabIndex="13">
                                         </x:TextBox>--%>
                                        </Items>                
                            </x:Panel>                                                 
                                
                                 <x:Label ID="Label3" runat="server" Label="Label" Text=" " Height ="20px" >
                            </x:Label><%--这是空行--%>

                            <x:Panel ID="Panel19" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="AwardPeople" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="报奖人：">
                                    </x:Label>
                                    <x:TextArea runat="server" MaxLength="300" Required="true" MaxLengthMessage="最多可输入300个字符" ShowLabel="true" Label="报奖人" CssStyle ="overflow-y:scroll" EmptyText="两个或两个以上请用逗号隔开" ID="tAwardPeople" Width="200px" Height="100px" AutoGrowHeight="false"  AutoGrowHeightMax="100" TabIndex="7">
                                    </x:TextArea>
                                </Items>
                            </x:Panel>           
                           
                                <x:Label ID="Label4" runat="server" Label="Label" Text=" " Height ="20px" >
                            </x:Label><%--这是空行--%>

                            <x:Panel ID="Panel6" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="AwardMember" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="成员：">
                                    </x:Label>
                                    <x:TextArea runat="server" MaxLength="200" MaxLengthMessage="最多可输入200个字符" ShowLabel="true" Label="成员" CssStyle ="overflow-y:scroll" EmptyText="两个或两个以上请用逗号隔开" ID="tAwardMember" Width="200px" Height="100px" AutoGrowHeight="false"  AutoGrowHeightMax="100" TabIndex="7">
                                    </x:TextArea>
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
                            <x:Button ID="Save"  runat="server" CssClass="marginr" Icon="Add" Size="Medium" Type="Submit" Text="保存" ValidateForms="Panel2" ConfirmText="确定保存？" OnClick="Save_Click">                            
                            </x:Button>   
                                 <x:Button ID="DeleteAll"  runat="server" CssClass="marginr" Icon="Delete"  Text="重置" Size="Medium" ConfirmText="确定重置？" OnClick="DeleteAll_Click" >
                            </x:Button>                         
                        </Items>
                        </x:Toolbar>
                    </Items>                            
                    </x:Panel>
    </div>
    </form>
</body>
</html>
