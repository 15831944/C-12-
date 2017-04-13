<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportExcel.aspx.cs" Inherits="WDFramework.AcademicMeeting.ImportExcel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

</head>
<body>
    <form id="form1" runat="server">


        <x:PageManager ID="PageManager1" AutoSizePanelID="RegionPanel1" runat="server" />
        <x:RegionPanel ID="RegionPanel1" ShowBorder="false" runat="server">
            <Regions>

                <x:Region ID="Region2" ShowBorder="false" ShowHeader="false" Position="Center" Layout="VBox"
                    BoxConfigAlign="Stretch" BoxConfigPosition="Left" BodyPadding="5px 5px 5px 0"
                    EnableBackgroundColor="true" runat="server">
                    <Items>

                        <x:Panel ID="Panel2" runat="server" Height="250px" Width="850px" ShowBorder="True" EnableCollapse="true"
                            Layout="HBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5"
                            BoxConfigChildMargin="0 5 0 0" ShowHeader="false">
                            <Items>
                                <x:Panel ID="Panel6" Title="项目概要" BoxFlex="1" runat="server"
                                    BodyPadding="5px" ShowBorder="false" ShowHeader="false">

                                    <Items>
                                        <x:Label ID="Label3" runat="server" Label="Label" Text=" " Height="20px">
                                        </x:Label>
                                        <%--这是空行--%>
                                        <x:Panel ID="Panel1" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                            Layout="Column" runat="server">
                                            <Items>
                                                <%-- <x:Label ID="Label9" Width="20px" runat="server"  CssStyle ="text-align:right" CssClass="marginr" ShowLabel="false" Text="上">
                                                </x:Label>--%>
                                                <x:Label ID="Label1" Width="100px" runat="server" CssStyle="text-align:left" CssClass="marginr" ShowLabel="false" Text="导入Excel文件：">
                                                </x:Label>
                                                <x:FileUpload runat="server" Width="200px" ID="filePath" EmptyText="请选择上传Excel" Label="上传文件" AutoPostBack="true" TabIndex="10">
                                                </x:FileUpload>
                                                <%-- <x:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" ValidateForms="SimpleForm1"
                                                   Text="提交">
                                                </x:Button>--%>
                                            </Items>
                                        </x:Panel>
                                          <x:Label ID="Label2" runat="server" Label="Label" Text=" " Height="20px">
                                        </x:Label>
                                        <%--这是空行--%>
                                        <x:Panel ID="Panel3" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                            Layout="Column" runat="server">
                                            <Items> 
                                                 <x:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" ValidateForms="SimpleForm1"
                                                   Text="导入">
                                                </x:Button>
                                            </Items>
                                        </x:Panel>
                                    </Items>

                                </x:Panel>
                            </Items>
                        </x:Panel>
                        <%--<x:Panel ID="Panel5" ShowHeader="false" CssClass="formitem" ShowBorder="false" Layout="Column" runat="server">
                            <Items>
                                <x:Toolbar ID="Toolbar1" runat="server" ColumnWidth="100%">
                                    <Items>
                                        <x:Label ID="Label15" Width="250px" runat="server" ShowLabel="true" Text=" ">
                                        </x:Label>
                                        <x:Button ID="btnSave" runat="server" CssClass="marginr" Text="保存" Size="Medium" Icon="Add" ConfirmText="确定保存？" ConfirmTarget="Top" ValidateForms="Panel2"  Type="Submit">
                                        </x:Button>
                                        <x:Button ID="btnSet" runat="server" CssClass="marginr" Text="重置" Size="Medium" Icon="Delete" ConfirmText="确定重置？" EnablePostBack="true" ConfirmTarget="Top" >
                                        </x:Button>
                                    </Items>
                                </x:Toolbar>
                            </Items>
                        </x:Panel>--%>
                        <%--<x:Panel ID="Panel8" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Button ID="btnSave" runat="server" CssClass="marginr" Text="保存" ConfirmText="确定保存？" ConfirmTarget="Top" OnClick="btnSave_Click" >
                                </x:Button>
                                <x:Button ID="btnSet" runat="server" CssClass="marginr" Text="重置" ConfirmText="确定重置？" EnablePostBack="true" ConfirmTarget="Top" OnClick="btnSet_Click">
                                </x:Button>
                                <x:Button ID="btnCancel" runat="server" CssClass="marginr" Text="取消" ConfirmText="确定取消？" ConfirmTarget="Top" OnClick="btnCancel_Click">
                                </x:Button>

                            </Items>
                        </x:Panel>--%>
                        <%--  --%>
                    </Items>
                </x:Region>
            </Regions>
        </x:RegionPanel>

        <%--<div>
            <asp:Button ID="Button1" runat="server" Text="Button" />
        </div>--%>
    </form>

</body>
</html>

