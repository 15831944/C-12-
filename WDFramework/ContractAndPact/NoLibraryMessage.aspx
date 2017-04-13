<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoLibraryMessage.aspx.cs" Inherits="WDFramework.ContractAndPact.NoLibraryMessage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head1" runat="server">
    <title></title>
    <link href="../res/css/main.css" rel="stylesheet" type="text/css" />
    <style>
        body.f-body {
            padding: 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager2" runat="server" AutoSizePanelID="Panel1" />
        <x:Panel ID="Panel1" runat="server" Width="350px" Height="250px"  ShowBorder="false" ShowHeader="false" Title="Panel">
            <Items>
                <x:TextArea ID="Content" runat="server" Width="350px" Height="250px"  Readonly="true" />
            </Items>
        </x:Panel>

    </form>
</body>
</html>
