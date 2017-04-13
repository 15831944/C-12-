<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add_Document.aspx.cs" Inherits="WDFramework.Projects.Projects.Add_Document" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript">
        function clearFile() {
            var obj = document.getElementById('fileupload');
            obj.outerHTML = obj.outerHTML;
        }
    </script>
    <title></title>
</head>
<body>
    <%--取消鼠标右键的点击--%>
    <form id="form1" runat="server">
        <div>
            <x:PageManager ID="PageManager1" AutoSizePanelID="Panel2" runat="server" />
            <x:Panel ID="Panel2" runat="server" Height="180px" ShowBorder="false" EnableCollapse="true"
                Layout="HBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5"
                BoxConfigChildMargin="0 0 0 0" ShowHeader="false" AutoScroll="true">
                <Items>
                    <x:Panel ID="Panel3" Title="文档概要" BoxFlex="1" runat="server"
                        BodyPadding="5px" BoxMargin="15 0 0 35" ShowBorder="false" ShowHeader="false" ColumnWidth="100%" AutoScroll="true">
                        <Items>
                            <x:Panel ID="Panel4" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Document" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="文档编号：">
                                    </x:Label>
                                    <x:TextBox ID="tDocument" MaxLength="20" MaxLengthMessage="最多可输入20个字符" ShowLabel="true" Label="文档编号" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="1" AutoPostBack="false">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>

                            <x:Label ID="Label16" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>

                            <x:Panel ID="Panel5" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="NDocument" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="文档名称：">
                                    </x:Label>
                                    <x:TextBox ID="tNDocument" ShowLabel="true" Label="文档名称" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="2">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>

                            <x:Label ID="Label17" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>

                            <x:Panel ID="Panel6" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="DocumentType" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="文档类型：">
                                    </x:Label>
                                    <x:DropDownList Label="文档类型" AutoPostBack="false" EnableEdit="false" Required="true" EnableSimulateTree="true" TabIndex="3"
                                        runat="server" ID="DropDownListDocument" Width="195px">
                                    </x:DropDownList>
                                </Items>
                            </x:Panel>

                            <x:Label ID="Label18" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>

                            <x:Panel ID="Panel1" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="labSecrecyLevel" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="保密级别：">
                                    </x:Label>
                                    <x:DropDownList ID="listSecrecyLevel" ShowLabel="true" Label="保密级别" Required="true" Width="195px" AutoPostBack="true" runat="server" TabIndex="4">
                                    </x:DropDownList>
                                </Items>
                            </x:Panel>
                        </Items>
                    </x:Panel>
                </Items>
            </x:Panel>
            <asp:Panel ID="Panel11" ShowHeader="false" ShowBorder="false"
                Layout="Column" runat="server" BackColor="White" Height="35px">
                <asp:Label ID="Labelp" runat="server" Label="Label" CssClass="marginr" Text=" " Width="40px">
                </asp:Label>
                <asp:Label ID="Label20" runat="server" Label="Label" CssClass="marginr" Text="相关文档： " Width="90px">
                </asp:Label>
                <input type="file" id="fileupload" style="width: 200px" runat="server" />
            </asp:Panel>
            <x:Panel ID="Panel7" ShowHeader="false" CssClass="formitem" ShowBorder="false" Layout="Column" runat="server">
                <Items>
                    <x:Toolbar ID="Toolbar1" runat="server" ColumnWidth="100%">
                        <Items>
                            <x:Label ID="Label19" Width="140px" runat="server" ShowLabel="true" Text=" ">
                            </x:Label>
                            <x:Button ID="Save" runat="server" CssClass="marginr" Type="Submit" Text="保存" Icon="Add" Size="Medium" ValidateForms="Panel2" ConfirmText="确定保存？" OnClick="Save_Click">
                            </x:Button>
                            <x:Button ID="DeleteAll" runat="server" CssClass="marginr" Icon="Delete" Text="重置" Size="Medium" ConfirmText="确定重置？" OnClick="DeleteAll_Click">
                            </x:Button>
                        </Items>
                    </x:Toolbar>
                </Items>
            </x:Panel>
        </div>
    </form>
</body>
</html>
