<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pop_Option.aspx.cs" Inherits="WDFramework.WebForms.Pop_Option" %>


<%@ Register Assembly="FineUI" Namespace="FineUI" TagPrefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <x:PageManager ID="PageManager1" AutoSizePanelID="Panel2" runat="server" />

            <x:Panel ID="Panel2" runat="server" Height="230px" ShowBorder="True" EnableCollapse="true"
                Layout="HBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5"
                BoxConfigChildMargin="0 5 0 0" ShowHeader="false">

                <Items>


                    <x:Panel ID="Panel3" Title="项目概要" BoxFlex="1" runat="server"
                        BodyPadding="10px" ShowBorder="false" ShowHeader="false" ColumnWidth="100%">
                        <Items>
                            <x:Label ID="Label4" runat="server" Label="Label" Text=" " Height="40px">
                            </x:Label>
                            <%--这是空行--%>
                            <x:Panel ID="Panel35" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label5" Width="50px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="原">
                                    </x:Label>
                                    <x:Label ID="Label36" Width="80px" CssStyle="text-align:legt" runat="server" CssClass="marginr" ShowLabel="false" Text="密码：">
                                    </x:Label>
                                    <x:TextBox ID="OldLoginPWD" ShowLabel="true" Label="原密码" MaxLength="50" TextMode ="Password"  MaxLengthMessage="最多输入50个字符" MinLengthMessage="最少输入6个字符" CssClass="input" Required="true" Width="150px" runat="server" AutoPostBack="true"  TabIndex="1">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>
                            <x:Label ID="Label3" runat="server" Label="Label" Text=" " Height="30px">
                            </x:Label>
                            <%--这是空行--%>
                            <x:Panel ID="Panel45" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label62" Width="50px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="新">
                                    </x:Label>
                                    <x:Label ID="Label6" Width="80px" CssStyle="text-align:legt" runat="server" CssClass="marginr" ShowLabel="false" Text="密码：">
                                    </x:Label>
                                    <x:TextBox ID="NewPWD" ShowLabel="true" Label="新密码" MaxLength="50" TextMode ="Password" MaxLengthMessage="最多输入50个字符" MinLength="6" MinLengthMessage="最少输入6个字符" CssClass="input" Required="true" Width="150px" runat="server" AutoPostBack="true"  TabIndex="2">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>
                            <x:Label ID="Label1" runat="server" Label="Label" Text=" " Height="30px">
                            </x:Label>
                            <%--这是空行--%>
                            <x:Panel ID="Panel1" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label2" Width="50px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="确">
                                    </x:Label>
                                    <x:Label ID="Label7" Width="80px" CssStyle="text-align:legt" runat="server" CssClass="marginr" ShowLabel="false" Text="认密码：">
                                    </x:Label>
                                    <x:TextBox ID="IsNewPWD" ShowLabel="true" Label="确认新密码" MaxLength="50" TextMode ="Password" MaxLengthMessage="最多输入50个字符" MinLength="6" MinLengthMessage="最少输入6个字符" CssClass="input" Required="true" Width="150px" runat="server" AutoPostBack="true" TabIndex="3">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>

                        </Items>
                    </x:Panel>

                </Items>
            </x:Panel>
            <x:Panel ID="Panel4" ShowHeader="false" CssClass="formitem" ShowBorder="false" Layout="Column" runat="server" Height="40">
                <Items>
                    <x:Toolbar ID="Toolbar1" runat="server" ColumnWidth="100%" Height="40">
                        <Items>
                            <x:Label ID="Label15" Width="150px" runat="server" ShowLabel="true" Text=" ">
                            </x:Label>                          
                            <x:Button ID="Save" runat="server" CssClass="marginr" Text ="保存"  ConfirmText="确定保存？" OnClick ="Save_Click"  ValidateForms="Panel2" Size="Medium" Type ="Submit"  Icon="Add">
                            </x:Button>
                        </Items>
                    </x:Toolbar>
                </Items>
            </x:Panel>
        </div>
        <x:Window ID="Window_NoLibraryMessage" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="true" EnableResize="true" Height="400px" Width="450px">
        </x:Window>
    </form>
</body>
</html>
