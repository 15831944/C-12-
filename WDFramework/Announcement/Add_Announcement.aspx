<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add_Announcement.aspx.cs" Inherits="WebApplication1.Add_Announcement" %>

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

        <x:Panel ID="Panel2" runat="server" Height="200px" ShowBorder="false" EnableCollapse="true"
                Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" 
                BoxConfigChildMargin="0 40 0 0" ShowHeader="false">
            <Items>          
               <x:Panel ID="Panel10" runat="server" ShowBorder="false" EnableCollapse="true" 
                    Layout="Column" BoxConfigAlign="Stretch" BoxConfigPosition="Start" Height="290px"
                    BoxConfigChildMargin="0 5 0 0" ShowHeader="false">
                    
                <Items>   
                <x:Panel ID="Panel3" Title="项目概要" runat="server" ColumnWidth="100%" ShowBorder="false" BodyPadding="30px"
                            ShowHeader="false">

                            <Items>
                                <x:Panel ID="Panel12" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="labHeadLine" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" 
Text="标题：">
                                        </x:Label>
                                        <x:TextBox ID="txtHeadLine" MaxLength="100" ShowLabel="true" Label="标题" Required="true" 
Width="200px" CssClass="marginr" runat="server" EnableAjax="True" TabIndex="1">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label1" runat="server" Label="Label" Text=" " Height="22px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel13" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="labAnnouncementSortName" Width="100px" runat="server" CssClass="marginr" 
ShowLabel="false" Text="分类：">
                                        </x:Label>
                                        <%-- <x:TextBox ID="txtAnnouncementSortName" RegexPattern="NUMBER"  ShowLabel="true" Label="分
类：" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="3">
                                        </x:TextBox>--%>
                                        <x:DropDownList ID="DropDownList_Sort" ShowLabel="true" Label="分类" Required="true" 
Width="195px" AutoPostBack="true" runat="server" TabIndex="2">
                                           <%-- <x:ListItem Text="通知" Value="1" />
                                            <x:ListItem Text="学校公告" Value="2" />
                                            <x:ListItem Text="外来公告" Value="3" />--%>

                                        </x:DropDownList>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label3" runat="server" Label="Label" Text=" " Height="22px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel5" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="labTime" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="时间：">
                                        </x:Label>
                                        <x:DatePicker ID="DatePicker_Time" runat="server" Width="195px" EnableEdit="false" 
ShowLabel="true" Label="时间" Required="true" TabIndex="3">
                                        </x:DatePicker>
                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label6" runat="server" Label="Label" Text=" " Height="22px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel22" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="labAgency" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" 
Text="来源单位：">
                                        </x:Label>
                                        <x:TextBox ID="txtSourceAgency" ShowLabel="true" Label="来源单位" 
Width="200px" MaxLength="50" CssClass="marginr" runat="server" TabIndex="4">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>                                
                            </Items>
                        </x:Panel> 
                </Items>  
            </x:Panel>                          
        </Items>  
    </x:Panel>
    <asp:Panel ID="Panelasp" ShowHeader="false" CssClass="formitem" ShowBorder="false"  
                              Layout="Column" runat="server" BackColor="White" Height ="135px">
             <asp:Label ID="Label" Width="27px" runat="server" CssClass="marginr" ShowLabel="false" Text="">
             </asp:Label>  
            <asp:Label ID="Label16" Width="95px" runat="server" CssClass="marginr" ShowLabel="false" Text="相关文档：">
            </asp:Label> 
            <input type="file"  id="fileupload" style="width:200px" runat="server" tabindex="5"/> 
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