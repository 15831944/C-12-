<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DownLoad_Photo.aspx.cs" Inherits="WDFramework.AcademicMeeting.DownLoad_Photo" %>

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
        <x:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
        <x:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false"
            AutoScroll="true" BodyPadding="10px" runat="server">
           
            <Rows>
                <x:FormRow>
                    <Items>
                        <x:Label ID="MeetingName" Width="150px" Label="会议名称"  runat="server" />
                    </Items>
                </x:FormRow>
                <x:FormRow>
                    <Items>
                       <x:Label ID="Organizers" Width="150px" Label="主办方"  runat="server" />
                    </Items>
                </x:FormRow>
                <x:FormRow>
                    <Items>
                       <x:Label ID="Coorganizers" Width="150px" Label="协办方"  runat="server" />
                    </Items>
                </x:FormRow>
                 <x:FormRow>
                    <Items>
                       <x:Label ID="MeetingPlace" Width="150px" Label="会议地点"  runat="server" />
                    </Items>
                </x:FormRow>
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