<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="WDFramework.Information.Detail" %>

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
<body>
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager2" runat="server" AutoSizePanelID="Panel1" />
        <x:Panel ID="Panel1" runat="server" Width="320" Height="200" BodyPadding="5px" ShowBorder="false" ShowHeader="false" Title="Panel" AutoScroll="true">
            <Items>
                <x:TextArea ID="Contents" runat="server" Width="320" Height="200"  Readonly="true" />
                <x:Label ID="old" runat="server" Text="原照片"></x:Label>  
                <x:Image ID="imgPhotoold" runat="server" ImageUrl="~/images/blank.png" Label="Label" ImageHeight ="200px" ImageWidth ="350px"></x:Image>
                 <x:Label ID="inew" runat="server" Text="新照片"></x:Label>  
                 <x:Image ID="imgPhotonew" runat="server" ImageUrl="~/images/blank.png" Label="Label" ImageHeight ="200px" ImageWidth ="350px"></x:Image>
            </Items>
        </x:Panel>

    </form>
</body>
</html>
