<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DownLoad_Report.aspx.cs" Inherits="WDFramework.AcademicMeeting.DownLoad_Report" %>

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
            AutoScroll="true" BodyPadding="10px" runat="server" >
           
            <Rows>
                <x:FormRow>
                    <Items>
                        <x:Label ID="SReportName" Width="150px" Label="报告名称"  runat="server" />
                    </Items>
                </x:FormRow>
                <x:FormRow>
                    <Items>
                       <x:Label ID="SReportPeople" Width="150px" Label="报告人"  runat="server" />
                    </Items>
                </x:FormRow>
                <x:FormRow>
                    <Items>
                       <x:Label ID="SReportTime" Width="150px" Label="报告时间"  runat="server" />
                    </Items>
                </x:FormRow>
                  <x:FormRow>
                    <Items>
                       <x:Label ID="SReportPlace" Width="150px" Label="报告地点"  runat="server" />
                    </Items>
                </x:FormRow>
                <x:FormRow>
                    <Items>
                       <x:LinkButton ID="DownFile" Label="相关文件"  runat="server" EnableAjax="false"  Text="下载"  OnClick="DownLoad_Click"></x:LinkButton>
                    </Items>
                </x:FormRow>
                <x:FormRow>
                    <Items>             
                           <x:LinkButton ID="Delete"  Label="相关文件" ConfirmText="确定删除？"  runat="server"  Text="删除" OnClick="Delete_Click"></x:LinkButton>
                    </Items>
                </x:FormRow>
            </Rows>
        </x:Form>
    </form>
</body>
</html>