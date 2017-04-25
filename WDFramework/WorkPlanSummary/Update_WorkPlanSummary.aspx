<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Update_WorkPlanSummary.aspx.cs" Inherits="WDFramework.WorkPlanSummary.Update_WorkPlanSummary" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script  type="text/javascript">
        function clearFile() {
            var obj = document.getElementById('fileupload');
            obj.outerHTML = obj.outerHTML;
        }
    </script>
</head>
<body oncontextmenu='return false'>
    <%--取消鼠标右键的点击--%>
    <form id="form1" runat="server">
     <x:PageManager ID="PageManager2" runat="server" />

        <x:Panel ID="Panel2" runat="server" Height="255px" ShowBorder="false" EnableCollapse="true"
                Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="10"
                BoxConfigChildMargin="10 40 0 0" ShowHeader="false">
            <Items>          
               <x:Panel ID="Panel10" runat="server" ShowBorder="false" EnableCollapse="true" 
                    Layout="Column" BoxConfigAlign="Stretch" BoxConfigPosition="Start" Height="290px"
                    BoxConfigChildMargin="0 5 0 0" ShowHeader="false">
                    
                <Items>   
                <x:Panel ID="Panel3" Title="项目概要" runat="server" ColumnWidth="100%" ShowBorder="false" BodyPadding="20px"
                            ShowHeader="false">

                            <Items>
                                <x:Panel ID="Panel1" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="labPlanWork" Width="120px" runat="server" CssClass="marginr" ShowLabel="false" Text="工作计划与总结名称：">
                                    </x:Label>
                                    <x:TextBox ID="txtPlanWork" MaxLength="100" ShowLabel="true" Required="true" Label="工作计划与总结名称" Width="200px" CssClass="marginr" runat="server" 

TabIndex="1" Readonly="true">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>
                            <x:Label ID="Label3" runat="server" Label="Label" Text=" " Height="22px">
                            </x:Label>
                            <%--这是空行--%>
                            <x:Panel ID="Panel7" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="DocumentSortID" Width="120px" runat="server" CssClass="marginr"  Label="分类" 

ShowLabel="true" Text="分类："></x:Label>
                                    <x:DropDownList ID="DropDownListSort" ShowLabel="false" Required="true" AutoPostBack="true" 

runat="server" EnableEdit="false" Width="195px" OnSelectedIndexChanged="DropDownListSort_SelectedIndexChanged">
                                        <x:ListItem Text="请选择" Value="0" />
                                        <%--<x:ListItem Text="个人" Value="1" />
                                        <x:ListItem Text="部门" Value="2" />--%>

                                    </x:DropDownList>
                                    <%-- <x:DropDownList ID="DropDownList" ShowLabel="false" AutoPostBack="false" runat="server" 

EnableEdit="false">
                                        <x:ListItem Text="请选择" Value="0" />
                                        
                                    </x:DropDownList>--%>
                                </Items>

                            </x:Panel>
                            <x:Label ID="Label4" runat="server" Label="Label" Text=" " Height="22px">
                            </x:Label>
                            <%--这是空行--%>
                            <x:Panel ID="Panel8" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="labSort" Width="120px" runat="server" CssClass="marginr" ShowLabel="false"
                                        Text="输入">
                                    </x:Label>
                                    <%--<x:TextBox ID="txtSort" ShowLabel="true" Required="true" Width="200px" CssClass="marginr"
                                        runat="server"  AutoPostBack="true"  MaxLength="30" CompareMessage="输入最大长度为30个字符">
                                    </x:TextBox>--%>
                                     <x:DropDownList ID="DropDownList" ShowLabel="false" Required="true" AutoPostBack="true" 

runat="server" EnableEdit="false" Width="195px">
                                        <x:ListItem Text="请选择" Value="0" />
                                       <%-- <x:ListItem Text="个人" Value="1" />
                                        <x:ListItem Text="部门" Value="2" />--%>

                                    </x:DropDownList>
                                </Items>
                                <%--<Items>
                                    <x:Label ID="Label1" Width="120px" runat="server" CssClass="marginr" ShowLabel="true" Text="具体分

类"></x:Label>
                                    <x:DropDownList ID="DropDownList" ShowLabel="false" AutoPostBack="false" runat="server" 

EnableEdit="false" Width="195px">
                                        <x:ListItem Text="请选择" Value="0" />

                                    </x:DropDownList>
                                </Items>--%>
                            </x:Panel>
                            <x:Label ID="Label2" runat="server" Label="Label" Text=" " Height="22px">
                            </x:Label>
                            <x:Panel ID="Panel5" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="labTime" Width="120px" runat="server" CssClass="marginr" ShowLabel="false" Text="时间：">
                                    </x:Label>
                                    <x:DatePicker ID="DatePikerTime" runat="server" Required="true" ShowLabel="true" Width="195px" 

Label="时间" EnableEdit="false" >
                                    </x:DatePicker>
                                </Items>

                            </x:Panel>
                            <x:Label ID="Label6" runat="server" Label="Label" Text=" " Height="22px">
                            </x:Label>
                            <x:Panel ID="Panel11" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="labSecrecyLevel" Width="120px" runat="server" CssClass="marginr" ShowLabel="false" 

Text="保密级别：">
                                    </x:Label>
                                    <%-- <x:TextBox ID="Location2" ShowLabel="false" Required="true" Width="200px" CssClass="marginr" 

runat="server" >
                                         </x:TextBox>--%>
                                    <x:DropDownList ID="DropDownList_SecrecyLevel" Label="保密级别" Width="195px" ShowLabel="true" 

AutoPostBack="false" runat="server" Required="true" >
                                    </x:DropDownList>
                                </Items>

                            </x:Panel>                                                
                            </Items>
                        </x:Panel> 
                </Items>  
            </x:Panel>                          
        </Items>  
    </x:Panel>
    <asp:Panel ID="Panelasp" ShowHeader="false" CssClass="formitem" ShowBorder="false"  
                              Layout="Column" runat="server" BackColor="White" Height="130px">
             <asp:Label ID="Label" Width="27px" runat="server" CssClass="marginr" ShowLabel="false" Text="">
             </asp:Label>  
            <asp:Label ID="Label16" Width="113px" runat="server" CssClass="marginr" ShowLabel="false" Text="相关文档：">
            </asp:Label> 
            <input type="file"  id="fileupload" style="width:200px" runat="server"/> 
            <%-- <asp:FileUpload ID="FileUpload1" Width="190px" runat="server" BackColor="White" />--%>
    </asp:Panel>
    <x:Panel ID="Panel4" ShowHeader="false" CssClass="formitem" ShowBorder="false" Layout="Column" runat="server">
            <Items>
            <x:Toolbar ID="Toolbar1" runat="server" ColumnWidth="100%" >
            <Items>
            <x:Label ID="Label15"  Width="150px" runat="server"  ShowLabel="true" Text=" ">
            </x:Label>   
            <x:Button ID="Save"  runat="server" CssClass="marginr" Type="Submit" Text="保存" Icon="Add" Size="Medium" 

ValidateForms="Panel2" ConfirmText="确定保存？" OnClick="btnSave_Click">
            </x:Button> 
            <x:Button ID="DeleteAll"  runat="server" CssClass="marginr" Icon="Delete"  Text="重置" Size="Medium" ConfirmText="确定重置？" OnClick="btnReset_Click">
            </x:Button>                        
        </Items>
        </x:Toolbar>
    </Items>                            
    </x:Panel>
    </form>
</body>
</html>
