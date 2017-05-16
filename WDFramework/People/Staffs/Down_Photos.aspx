<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Down_Photos.aspx.cs" Inherits="WDFramework.People.Staffs.Down_Photos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head1" runat="server">
    <title></title>
    <link href="../res/css/main.css" rel="stylesheet" type="text/css" />
    <style>
        body.f-body {
            padding: 0;
            margin:auto;
            left:400px;
            text-align :center;
            width :auto;
            
        }
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <x:Image ID="Image_show" runat="server" ImageUrl="../../images/blank.png" ImageHeight="250px" ImageWidth="195px" ShowEmptyLabel="true"></x:Image>
        <x:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
        <x:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false"
            AutoScroll="true" BodyPadding="10px" runat="server" >
            <Rows>
                <x:FormRow>
                    <Items>
                       <x:LinkButton ID="DownFile" Label="相关照片"  runat="server" EnableAjax="false"  Text="下载"  OnClick="DownLoad_Click"></x:LinkButton>
                    </Items>
                </x:FormRow>
                <x:FormRow>
                    <Items>             
                           <x:LinkButton ID="Delete"  Label="相关照片" ConfirmText="确定删除？"  runat="server"  Text="删除" OnClick="Delete_Click"></x:LinkButton>
                    </Items>
                </x:FormRow>
            </Rows>
        </x:Form>
    </form>
</body>
</html>
