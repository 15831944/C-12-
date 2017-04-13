<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Remark_Window.aspx.cs" Inherits="WDFramework.People.SocialPartTimeJob.Remark_Window" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
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
