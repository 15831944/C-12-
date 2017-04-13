<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlatformMember.aspx.cs" Inherits="WDFramework.Platform.PlatformMember" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head" runat="server">
    <title></title>
    <link href="../res/css/main.css" rel="stylesheet" type="text/css" />
    <style>
        body.f-body {
            padding: 0;
        }
    </style>
</head>
<body>
    <form id="form" runat="server">
        <x:PageManager ID="PageManager" runat="server" AutoSizePanelID="Panel" />
        <x:Panel ID="Panel" runat="server" Width="320" Height="200" BodyPadding="5px" ShowBorder="false" ShowHeader="false" Title="Panel">
            <Items>
                <x:TextArea ID="Contents" runat="server" Width="320" Height="200" Readonly="true" />
            </Items>
        </x:Panel>
    </form>
</body>
</html>
