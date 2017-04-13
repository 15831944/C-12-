<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Operate.aspx.cs" Inherits="WDFramework.File.Operate" %>

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
        <x:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
        <x:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false"
            AutoScroll="true" BodyPadding="10px" runat="server">
           
            <Rows>
                <x:FormRow>
                    <Items>
                        <x:Label ID="filename" Width="100px" Label="文件名"  runat="server" />
                    </Items>
                </x:FormRow>
                <x:FormRow>
                    <Items>
                       <x:Label ID="filesort" Width="100px" Label="文件分类"  runat="server" />
                    </Items>
                </x:FormRow>
                <x:FormRow>
                    <Items>
                       <x:Label ID="agency" Width="100px" Label="所属部门"  runat="server" />
                    </Items>
                </x:FormRow>
                <x:FormRow>
                    <Items>
                       <x:LinkButton ID="DownFile" Label="相关文件"  runat="server" EnableAjax="false"  Text="下载" OnClick="DownLoad_Click"></x:LinkButton>
                    </Items>
                </x:FormRow>
            </Rows>
        </x:Form>
    </form>
</body>
</html>