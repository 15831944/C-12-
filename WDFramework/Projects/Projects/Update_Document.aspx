<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Update_Document.aspx.cs" Inherits="WDFramework.Projects.Projects.Update_Document" %>

<%@ Register Assembly="FineUI" Namespace="FineUI" TagPrefix="x" %>
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

<body oncontextmenu='return false'>
    <%--取消鼠标右键的点击--%>
    <form id="form1" runat="server">
        <div>
            <x:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
            <x:Panel ID="Panel1" runat="server" Height="180px" ShowBorder="false" EnableCollapse="true"
                Layout="HBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5"
                BoxConfigChildMargin="0 0 0 0" ShowHeader="false" AutoScroll="true">
                <Items>
                    <x:Panel ID="Panel2" Title="文档概要" BoxFlex="1" runat="server"
                        BodyPadding="5px" BoxMargin="15 0 0 35" ShowBorder="false" ShowHeader="false" ColumnWidth="100%" AutoScroll="true">
                        <Items>
                            <x:Panel ID="Panel3" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="labFileCode" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="文档编号：">
                                    </x:Label>
                                    <x:TextBox ID="txtFileCode" Label="文档编号" ShowLabel="true" MaxLength="20" MaxLengthMessage="最多输入20个数字" Width="200px" CssClass="marginr" runat="server" TabIndex="1">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>

                            <x:Label ID="Label1" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>

                            <x:Panel ID="Panel4" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="labFileName" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="文档名称：">
                                    </x:Label>
                                    <x:TextBox ID="txtFileName" Label="文档名称" ShowLabel="true" MaxLength="20" MaxLengthMessage="最多输入20个字符" Width="200px" CssClass="marginr" runat="server" TabIndex="2">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>

                            <x:Label ID="Label3" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>

                            <x:Panel ID="Panel5" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="labFileType" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="文档类型：">
                                    </x:Label>
                                    <x:DropDownList ID="listFileType" Label="文档类型" ShowLabel="true" Width="195px" CssClass="marginr" runat="server" TabIndex="3">
                                    </x:DropDownList>
                                </Items>
                            </x:Panel>

                            <x:Label ID="Label2" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>

                            <x:Panel ID="Panel7" ShowHeader="false" CssClass="formitem" ShowBorder="false"
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
            <asp:Panel ID="Panelasp" ShowHeader="false" ShowBorder="false"
                Layout="Column" runat="server" Height="35px" BackColor="White">
                <asp:Label ID="Label10" runat="server" Label="Label" CssClass="marginr" Text=" " Width="40px">
                </asp:Label>
                <asp:Label ID="Label20" runat="server" Label="Label" CssClass="marginr" Text="相关文档： " Width="90px">
                </asp:Label>
                <input type="file" id="fileupload" style="width: 200px" runat="server" />
            </asp:Panel>
            <x:Panel ID="Panel87" ShowHeader="false" CssClass="formitem" ShowBorder="false" Layout="Column" runat="server">
                <Items>
                    <x:Toolbar ID="Toolbar1" runat="server" ColumnWidth="100%">
                        <Items>
                            <x:Label ID="Label12" runat="server" Label="Label" Text=" " Width="140"></x:Label>
                            <x:Button ID="Save" Text="保存" runat="server" Type="Submit" ValidateForms="Panel2" Size="Medium" Icon="Add" ConfirmText="确定保存吗？" OnClick="Update">
                            </x:Button>
                            <x:Button ID="DeleteAll" Text="重置" runat="server" ConfirmText="确定重置吗？" Size="Medium" Icon="Delete" OnClick="Reset">
                            </x:Button>
                        </Items>
                    </x:Toolbar>
                </Items>
            </x:Panel>
        </div>
    </form>
</body>
</html>
