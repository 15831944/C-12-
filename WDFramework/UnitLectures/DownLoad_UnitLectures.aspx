<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DownLoad_UnitLectures.aspx.cs" Inherits="WDFramework.UnitLectures.DownLoad_UnitLectures" %>

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
                        <x:Label ID="LecturesName" Width="50px" Label="姓名"  runat="server" />
                    </Items>
                </x:FormRow>
                <x:FormRow>
                    <Items>
                       <x:Label ID="UReportName" Width="50px" Label="报告名称"  runat="server" />
                    </Items>
                </x:FormRow>
                 <x:FormRow>
                    <Items>
                       <x:Label ID="LecturesPlace" Width="50px" Label="地点"  runat="server" />
                    </Items>
                </x:FormRow>
                <x:FormRow>
                    <Items>
                       <x:Label ID="LecturesTime" Label="时间"  Width="50px"  runat="server"></x:Label>
                    </Items>
                </x:FormRow>
                 <x:FormRow>
                    <Items>
                       <x:Label ID="Agency" Label="所属部门"  Width="50px"  runat="server" ></x:Label>
                    </Items>
                </x:FormRow>
               <x:FormRow>
                    <Items>
                       <x:LinkButton ID="DownFile" Label="相关文件"  runat="server" EnableAjax="false"  Text="下载" OnClick="btn_DownLoadContract_Click"></x:LinkButton>
                    </Items>
                </x:FormRow>
            </Rows>
        </x:Form>
    </form>
</body>
</html>