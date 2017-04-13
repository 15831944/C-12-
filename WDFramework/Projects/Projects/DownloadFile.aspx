<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DownloadFile.aspx.cs" Inherits="WDFramework.Projects.Projects.DownloadFile" %>

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
<body onkeydown="return (event.keyCode!=8)">
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
        <x:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false"
            AutoScroll="true" BodyPadding="20px" runat="server">
            <Rows>
                <x:FormRow>
                    <Items>
                        <x:Label ID="FileCode" Width="100px" Label="文件编号" runat="server" />
                    </Items>
                </x:FormRow>
                <x:FormRow>
                    <Items>
                        <x:Label ID="FileName" Width="100px" Label="文件名称" runat="server" />
                    </Items>
                </x:FormRow>
                <x:FormRow>
                    <Items>
                        <x:Label ID="FileType" Width="100px" Label="文件类型" runat="server" />
                    </Items>
                </x:FormRow>
                <x:FormRow>
                    <Items>
                        <x:LinkButton ID="Download" Label="相关文件" runat="server" EnableAjax="false" Text="下载" OnClick="DownFile_Click"></x:LinkButton>
                    </Items>
                </x:FormRow>
                <x:FormRow>
                    <Items>
                        <x:LinkButton ID="Delete" Label="相关文件" runat="server" Text="删除" OnClick="Delete_Click"></x:LinkButton>
                    </Items>
                </x:FormRow>
            </Rows>
        </x:Form>
    </form>
</body>
</html>
