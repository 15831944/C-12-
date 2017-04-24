<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Updata_Contract.aspx.cs" Inherits="WDFramework.ContractAndPact.Contract.Updata_Contract" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body oncontextmenu='return false'>
    <%--取消鼠标右键的点击--%>
    <form id="form1" runat="server">
     <x:PageManager ID="PageManager2" runat="server" />

        <x:Panel ID="Panel2" runat="server" Height="250px" ShowBorder="false" EnableCollapse="true"
                Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" 
                BoxConfigChildMargin="0 0 0 0" ShowHeader="false">
            <Items>          
               <x:Panel ID="Panel10" runat="server" ShowBorder="false" EnableCollapse="true"
                    Layout="Column" BoxConfigAlign="Stretch" BoxConfigPosition="Start" Height="290px"
                    BoxConfigChildMargin="0 0 0 0" ShowHeader="false">
                    
                <Items>   
                <x:Panel ID="Panel3" Title="项目概要" runat="server" ColumnWidth="100%" ShowBorder="false" BodyPadding="20px"
                            ShowHeader="false">
                       <Items>
                            <x:Label ID="Label4" runat="server" Label="Label" Text=" " Height="25px">
                            </x:Label>
                            <%--这是空行--%>
                          
                             <x:Panel ID="Panel12" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="labContractHeadLine" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="资料题目：">
                                    </x:Label>
                                    <%--正则表达式:中文、英文、数字包括下划线--%>
                                    <x:TextBox ID="txtContractHeadLine" RequiredMessage="资料题目不能为空"  MaxLength="100"  CompareMessage="输入最大长度为100个字符" ShowLabel="true" Label="资料题目" Required="true"  Width="200px" CssClass="marginr" runat="server" TabIndex="1">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>

                            <x:Label ID="Label3" runat="server" Label="Label" Text=" " Height="30px">
                            </x:Label>
                            <%--这是空行--%>

                            <x:Panel ID="Panel1" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="labContractAuthors" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="资料保存人：">
                                    </x:Label>
                                    <x:TextBox ID="txtContractAuthors" MaxLength="10" ShowLabel="true" Label="资料保存人" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="2" ><%--RequiredMessage="资料著作人不能为空" CompareMessage="输入最大长度为10个字符"--%>
                                    </x:TextBox>
                                </Items>
                            </x:Panel>

                           <x:Label ID="Label23" runat="server" Label="Label" Text=" " Height="30px">
                           </x:Label>
                           <%--这是空行--%>
                           <x:Panel ID="Panel19" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                               Layout="Column" runat="server">
                               <Items>
                                   <x:Label ID="labContractOrigianl" Width="110px" runat="server" CssClass="marinr" ShowLabel="false" Text="原始文件保存人">
                                   </x:Label>
                                   <x:TextBox ID="txtContractOriginal" MaxLength="10" ShowLabel="true" Label="原始文件保存人" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="5">
                                   </x:TextBox>
                               </Items>
                           </x:Panel>

                            <x:Label ID="Label1" runat="server" Label="Label" Text=" " Height="30px">
                            </x:Label>
                            <%--这是空行--%>

                            <x:Panel ID="Panel5" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label2" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="保密级别：">
                                    </x:Label>
                                    <x:DropDownList ID="DropDownList_SecrecyLevel" ShowLabel="true" Label="保密级别" Required="true" Width="195px" AutoPostBack="true" runat="server" TabIndex="3">
                                    </x:DropDownList>
                                </Items>
                            </x:Panel>                           
                    </Items>  
                           
                    </x:Panel>
                </Items>  
            </x:Panel>            
            </Items>  
    </x:Panel>
    <asp:Panel ID="Panelasp" ShowHeader="false" CssClass="formitem" ShowBorder="true"  
                              Layout="Column" runat="server" BackColor="White" Height="135px">
             <asp:Label ID="Label" Width="17px" runat="server" CssClass="marginr" ShowLabel="false" Text="">
             </asp:Label>  
            <asp:Label ID="Label16" Width="105px" runat="server" CssClass="marginr" ShowLabel="false" Text="相关文档：">
            </asp:Label> 
            <input type="file"  id="fileupload" style="width:200px" runat="server" tabindex="4"/> 
            <%-- <asp:FileUpload ID="FileUpload1" Width="190px" runat="server" BackColor="White" />--%>
    </asp:Panel>
    <x:Panel ID="Panel4" ShowHeader="false" CssClass="formitem" ShowBorder="false" Layout="Column" runat="server">
            <Items>
            <x:Toolbar ID="Toolbar1" runat="server" ColumnWidth="100%" >
            <Items>
            <x:Label ID="Label15"  Width="150px" runat="server"  ShowLabel="true" Text=" ">
            </x:Label>   
            <x:Button ID="Save"  runat="server" CssClass="marginr" Type="Submit" Text="保存" Icon="Add" Size="Medium" ValidateForms="Panel2" ConfirmText="确定保存？" OnClick="btnSave_Click">
            </x:Button> 
                    <x:Button ID="DeleteAll"  runat="server" CssClass="marginr" Icon="Delete"  Text="重置" Size="Medium" ConfirmText="确定重置？" OnClick="btnReSet_Click">
            </x:Button>                        
        </Items>
        </x:Toolbar>
    </Items>                            
    </x:Panel>
    </form>
</body>
</html>
