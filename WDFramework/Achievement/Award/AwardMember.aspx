<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AwardMember.aspx.cs" Inherits="WDFramework.Achievement.Award.AwardMember" %>

<!DOCTYPE html>

<html>
<head id="head1" runat="server">
    <title></title>
    <link href="../res/css/main.css" rel="stylesheet" type="text/css" />
    <style>
        body.f-body {
            padding: 0;
        }
    </style>
</head>
<body onkeydown="return (event.keyCode!=8)">
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager2" runat="server" AutoSizePanelID="Panel1" />
        <x:Panel ID="Panel1" runat="server" Width="320" Height="200" BodyPadding="5px" ShowBorder="false" ShowHeader="false" Title="Panel">
            <Items>
                <x:TextArea ID="Content" runat="server" Width="320" Height="200"  Readonly="true" />
            </Items>
        </x:Panel>

    </form>
</body>
</html>