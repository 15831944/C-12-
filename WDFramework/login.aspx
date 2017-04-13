<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="WDFramework.login" %>

<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <title>������Ϣ����ϵͳ</title>
    <link href="../res/css/main.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .backImage {
            background-image: url(images/bg7.jpg);
        }

        .x-panel-body {
            background-color: transparent;
        }

        .white span {
            color: white;
        }

     
    </style>

</head>

<body style="background: url(images/login.jpg);">
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager1" runat="server" />
        <br />
        <x:Panel ID="Panel9"  Layout="VBox" runat="server" Width="1200px" Height="730px" CssClass="backImage" EnableBackgroundColor="false" CssStyle="position: absolute; left: 480px;top:160px"
            ShowBorder="false" ShowHeader="false">
            <Items>
            </Items> 
        </x:Panel>
        <x:Label ID="Label1" runat="server" Label="Label" Text="�û���:" CssClass="white" CssStyle="font-size:18px;position: absolute; left: 1050px; top: 585px"></x:Label>
        <x:TextBox ID="tbxUserName" runat="server" Width="200px" Required="true" Label="Label" Text=""  CssStyle="position: absolute; left: 1200px; top: 585px"></x:TextBox>
        <x:Label ID="Label2" runat="server" Label="Label" Text="����:" CssClass="white"  CssStyle="font-size:18px;position: absolute; left: 1050px; top: 650px"></x:Label>
        <x:TextBox ID="tbxPassword" runat="server" Width="200px" Required="true" Label="Label" Text="" TextMode ="Password"  CssStyle="position: absolute; left: 1200px; top: 650px"></x:TextBox>
        <x:Button ID="btnLogin" runat="server" Text="��   ¼"  Size="Medium"  OnClick ="btnLogin_Click" Type ="Submit"  CssStyle="  position: absolute; left: 1360px; top: 730px" ></x:Button>
        <x:Label ID="Lv" runat="server" Label="Label" Text="�汾��:4.1" CssClass="white" CssStyle="font-size:12px;position: absolute; left: 950px; top: 750px"></x:Label>
        <x:Label ID="Maker" runat="server" Label="Label" Text="���������Ϣ�����о���" CssClass="white" CssStyle="font-size:12px;position: absolute; left: 1700px; top: 940px"></x:Label>
    </form>
</body>
</html>
