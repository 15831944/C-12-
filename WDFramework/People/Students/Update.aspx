<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Update.aspx.cs" Inherits="WDFramework.People.Update" %>



<%@ Register Assembly="FineUI" Namespace="FineUI" TagPrefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2" runat="server">
    <title></title>
    <link href="../res/css/main.css" rel="stylesheet" type="text/css" />
</head>
<body oncontextmenu='return false'>
    <%--取消鼠标右键的点击--%>
    <form id="form2" runat="server">
        <x:PageManager ID="PageManager2" AutoSizePanelID="Panel16" runat="server" />

        <x:Panel ID="Panel16" runat="server" BodyPadding="5px" EnableBackgroundColor="true"
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" ShowHeader="false">
            <Items>
                <x:Panel ID="Panel11" runat="server" Height="370px" ShowBorder="True" EnableCollapse="true" AutoScroll="true"
                    Layout="Column" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5" BodyPadding="0px"
                    BoxConfigChildMargin="0 5 0 0" ShowHeader="false">
                    <Items>
                        <x:Panel ID="Panel13" Title="学生信息" runat="server" ColumnWidth="50%" ShowBorder="false" BodyPadding="30px"
                            ShowHeader="false">
                            <Items>
                                <x:Panel ID="Panel12" ShowHeader="false" CssClass="formitem" ShowBorder="false" 
                              Layout="Column" runat="server">
                                        <Items>                                         
                                        <x:Label ID="ProjectName" Width="100px" runat="server"   CssClass="marginr" ShowLabel="false" Text="姓名：">
                                        </x:Label>                        
                                        <x:TextBox ID="T_SName"  Label="姓名" ShowLabel="true" MaxLength ="10" MaxLengthMessage ="最多输入10个字符"  Width="200px" CssClass="marginr" runat="server" TabIndex ="1" >
                                  </x:TextBox>

                                        </Items>                
                             </x:Panel>
                           <x:Label ID="Label1" runat="server" Label="Label" Text=" " Height ="30px" >
                            </x:Label><%--这是空行--%>
                            <x:Panel ID="Panel30" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                              Layout="Column" runat="server">
                                        <Items>
                                        <x:Label ID="ProjectHeads" Width="100px" runat="server" CssClass="marginr"  ShowLabel="false" Text="学号：">
                                        </x:Label>                        
                                        <x:TextBox ID="T_Sno" ShowLabel="true" Label ="学号" Readonly ="true"  Width="200px" MaxLength ="20" MaxLengthMessage ="最多输入20个字符"  Regex="^[0-9]+$" RegexMessage ="只能输入数字" CssClass="marginr" runat="server" TabIndex ="2" >
                                        </x:TextBox>
                                        </Items>                
                              </x:Panel>

                            <x:Label ID="Label4" runat="server" Label="Label" Text=" " Height ="30px" >
                            </x:Label><%--这是空行--%>
                           <x:Panel ID="Panel4" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                              Layout="Column" runat="server">
                                        <Items>
                                        <x:Label ID="Sex" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="性别：">
                                        </x:Label>
                                         <x:RadioButton ID="rbtnBoy" Label="性别" Checked="true" GroupName="sex" Text="男" runat="server" TabIndex ="3">
                                        </x:RadioButton>
                                        <x:RadioButton ID="rbtnGril" GroupName="sex" ShowEmptyLabel="true" Text="女" runat="server">
                                        </x:RadioButton>                                        
                                        </Items>                
                            </x:Panel>
                            
                            

                            <x:Label ID="Label5" runat="server" Label="Label" Text=" " Height ="30px" >
                            </x:Label><%--这是空行--%>

                            <x:Panel ID="Panel15" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                              Layout="Column" runat="server">
                                        <Items>
                                        <x:Label ID="ProjectSort" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="证件类型：">
                                        </x:Label> 
                                            <x:DropDownList   AutoPostBack="false"  EnableSimulateTree="true" ShowLabel="false"  
                                                Width="195px" CssClass="marginr" runat="server" ID="DropDownListDocumentType" TabIndex ="4">   
                                                
                                            </x:DropDownList>                                                              
                                        </Items>                
                            </x:Panel>

                            <x:Label ID="Label2" runat="server" Label="Label" Text=" " Height ="30px" >
                            </x:Label><%--这是空行--%>

                           <x:Panel ID="Panel20" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                              Layout="Column" runat="server">
                                        <Items>
                                        <x:Label ID="ProjectNature" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="证件号码：">
                                        </x:Label>                        
                                        <x:TextBox ID="T_DocumentNumber" ShowLabel="true"  Label ="证件号码" MaxLength ="25" MaxLengthMessage ="最多输入25个字符"  Width="200px" CssClass="marginr" runat="server" TabIndex ="5" >
                                         </x:TextBox>
                                        </Items>                
                            </x:Panel>
                           <x:Label ID="Label14" runat="server" Label="Label" Text=" " Height ="30px" >
                           </x:Label><%--这是空行--%>
                            <x:Panel ID="Panel6" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                              Layout="Column" runat="server">
                                        <Items>
                                        <x:Label ID="ProjectState" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="联系方式：">
                                        </x:Label>                        
                                        <x:TextBox ID="T_Contact" ShowLabel="true" Label ="联系方式" Width="200px" CssClass="marginr" runat="server" MaxLength="20" MaxLengthMessage ="最多输入20个字符" TabIndex ="6">
                                         </x:TextBox>
                                        </Items>                
                            </x:Panel>

                                <x:Label ID="Label10" runat="server" Label="Label" Text=" " Height ="30px" >
                           </x:Label><%--这是空行--%>

                          <x:Panel ID="Panel9" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                              Layout="Column" runat="server">
                                        <Items>
                                        <x:Label ID="Label8" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="学生类型：">
                                        </x:Label>                                     
                                             <x:DropDownList   AutoPostBack="false" EnableSimulateTree="true" CssClass="marginr" TabIndex ="7"               
                                                   runat="server" ID="DropDownListType" Width="195px">  
                                                         
                                             </x:DropDownList>   
                                                                              
                                        </Items>                
                            </x:Panel>

                                <x:Label ID="Label3" runat="server" Label="Label" Text=" " Height ="30px" >
                            </x:Label><%--这是空行--%>

                             <x:Panel ID="Panel10" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                              Layout="Column" runat="server">
                                        <Items>
                                        <x:Label ID="Label11" Width="100px" runat="server" CssClass="marginr"  ShowLabel="false" Text="指导教师：">
                                        </x:Label>                        
                                        <x:TextBox ID="T_UserInfoID" ShowLabel="true" Label ="指导教师"  Width="200px" MaxLength ="20" MaxLengthMessage ="最多输入20个字符" CssClass="marginr" runat="server" TabIndex ="8" >
                                        </x:TextBox>
                                        </Items>                
                              </x:Panel>
                            
                            </Items>
                        </x:Panel>
                        <x:Panel ID="Panel28" Title="学生信息" runat="server" ColumnWidth="50%" ShowBorder="false" BodyPadding="30px"
                            ShowHeader="false">
                            <Items>
                            <x:Panel ID="Panel7" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                              Layout="Column" runat="server">
                                        <Items>
                                        <x:Label ID="ProjectLevel" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="专业：">
                                        </x:Label>                        
                                        <x:TextBox ID="T_Specialty" ShowLabel="true" Label ="专业" Width="200px" MaxLength ="20" MaxLengthMessage ="最多输入20个字符" CssClass ="marginr" runat="server" TabIndex ="9" >
                                         </x:TextBox>
                                        </Items>                
                            </x:Panel>

                                 <x:Label ID="Label9" runat="server" Label="Label" Text=" " Height ="30px" >
                            </x:Label><%--这是空行--%>

                              <x:Panel ID="Panel25" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                              Layout="Column" runat="server">
                                        <Items>
                                        <x:Label ID="ApproveUnit" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="研究方向：">
                                        </x:Label>                        
                                        <x:TextBox ID="T_SResearch" ShowLabel="true" Label ="研究方向" Width="200px" MaxLength ="20" MaxLengthMessage ="最多输入20个字符"  CssClass="marginr" runat="server"  TabIndex ="10">
                                         </x:TextBox>
                                        </Items>                
                            </x:Panel>
                                <x:Label ID="Label13" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel1" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label12" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="所属机构：">
                                        </x:Label>
                                        <x:DropDownList AutoPostBack="false" EnableSimulateTree="true" CssClass="marginr" TabIndex="11"
                                            runat="server" ID="DropDownList_Agency" Width="195px">
                                        </x:DropDownList>
                                    </Items>
                                </x:Panel>
                                                              <x:Label ID="Label19" runat="server" Label="Label" Text=" " Height ="30px" >
                            </x:Label><%--这是空行--%>

                            <x:Panel ID="Panel14" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                              Layout="Column" runat="server">
                                        <Items>

                                        <x:Label ID="StartTime" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="入学时间：">
                                        </x:Label>      
                                       <%--  <x:TextBox ID="StartTime2" ShowLabel="false" Required="true" Width="150px" CssClass="marginr" runat="server" >
                                         </x:TextBox> --%>  
                                            <x:DatePicker runat="server"  Label="入学时间" EmptyText="请选择入学时间"  Width="195px" CssClass="marginr" TabIndex="11"                    
                                                ID="DatePickerEnterTime" EnableEdit ="false"   >               
                                            </x:DatePicker>
                              

                                        </Items>                
                            </x:Panel>

                                 <x:Label ID="Label7" runat="server" Label="Label" Text=" " Height ="30px" >
                            </x:Label><%--这是空行--%>
                           
                            <x:Panel ID="Panel5" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                              Layout="Column" runat="server">
                                        <Items>
                                        <x:Label ID="Label6" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="是否毕业：">
                                        </x:Label>
                                        <x:RadioButton ID="IsGraduation"  ShowLabel="true" Checked="true" GroupName="IsGraduation" ShowEmptyLabel="true" Text="是" runat="server" TabIndex ="12">
                                        </x:RadioButton>
                                        <x:RadioButton ID="NotGraduation"  ShowLabel="true" GroupName="IsGraduation" ShowEmptyLabel="true" Text="否" runat="server">
                                        </x:RadioButton>                                       
                                        </Items>                
                            </x:Panel>

                           <x:Label ID="Label131" runat="server" Label="Label" Text=" " Height ="30px" >
                           </x:Label><%--这是空行--%>

                           <x:Panel ID="Panel31" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                              Layout="Column" runat="server">
                                        <Items>
                                             <x:Label ID="EndTime" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="毕业时间：">
                                        </x:Label> 
                                    
                                             <x:DatePicker runat="server"  Label="毕业时间" EmptyText="请选择毕业时间"  Width="195px" CssClass="marginr"  TabIndex ="13"                     
                                                ID="DatePickerGraduationTime" EnableEdit ="false" >               
                                            </x:DatePicker> 
                                                              
                                        <%--<x:TextBox ID="EndTime2" ShowLabel="false" Required="true" Width="150px" CssClass="marginr" runat="server" >
                                         </x:TextBox>--%>
                                        </Items>                
                            </x:Panel>

                            <x:Label ID="Label15" runat="server" Label="Label" Text=" " Height ="30px" >
                            </x:Label><%--这是空行--%>
                            
                         <x:Panel ID="Panel8" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                              Layout="Column" runat="server">
                                        <Items>
                                        <x:Label ID="SourceUnit" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="毕业去向单位：">
                                        </x:Label>                        
                                        <x:TextBox ID="T_SGraduationDirection" ShowLabel="true" Label ="毕业去向单位" Width="200px" MaxLength ="20" MaxLengthMessage ="最多输入20个字符"   CssClass="marginr" runat="server" TabIndex ="14" >
                                         </x:TextBox>
                                        </Items>                
                            </x:Panel>
                            
                            
                            

                           
                          
                           
                          <x:Label ID="Label43" runat="server" Label="Label" Text=" " Height ="30px" >
                            </x:Label><%--这是空行--%>
                              <x:Panel ID="Panel27" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                              Layout="Column" runat="server">
                                        <Items>
                                        <x:Label ID="Label32" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="保密级别：">
                                        </x:Label>                        
                                         <x:DropDownList Label="保密等级"  AutoPostBack="false" EnableSimulateTree="true" TabIndex ="15"
                    ShowRedStar="true" runat="server" ID="DropDownListSecrecyLevel" Width="195px">
                          <x:ListItem Text="四级" Value="四级" /> 
                </x:DropDownList>              
                                        </Items>                
                            </x:Panel>
                                </Items> 
                        </x:Panel>
                    
                    </Items>
                </x:Panel>
                <x:Panel ID="Panel38" runat="server" Height="40px" ShowBorder="True" EnableCollapse="true"
                    BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5"
                    BoxConfigChildMargin="0 5 0 0" ShowHeader="false" Width="750px">
                    <Items>
                        <x:Toolbar ID="Toolbar2" runat="server">
                            <Items>
                                <x:Label ID="Label62" runat="server" Label="Label" Text=" " Width="310px"></x:Label>
                                <x:Button ID="Button1" Text="保存" Icon="Add" runat="server" OnClick="Save_Click" Type ="Submit" ConfirmText ="确定保存？"    Size="Medium" ValidateForms="Panel11">
                                </x:Button>
                                <x:Button ID="Button2" Text="重置" Icon="Delete" runat="server"  OnClick ="Reset_Click" Type ="Submit" ConfirmText ="确定重置？"  Size="Medium">
                                </x:Button>
                            </Items>
                        </x:Toolbar>
                    </Items>
                </x:Panel>

            </Items>
        </x:Panel>
    </form>
</body>
</html>