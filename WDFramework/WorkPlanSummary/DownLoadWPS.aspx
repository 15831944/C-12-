<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DownLoadWPS.aspx.cs" Inherits="WDFramework.WorkPlanSummary.DownLoadWPS" %>

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
                        <x:Label ID="PlanWork" Width="50px" Label="名称"  runat="server" />
                    </Items>
                </x:FormRow>
                <x:FormRow>
                    <Items>
                       <x:Label ID="Time" Width="50px" Label="时间"  runat="server" />
                    </Items>
                </x:FormRow>
               <x:FormRow>
                    <Items>
                       <x:LinkButton ID="DownFile" Label="相关文件"  runat="server" EnableAjax="false"  Text="下载" OnClick="btn_DownLoad_Click"></x:LinkButton>
                    </Items>
                </x:FormRow>
            </Rows>
        </x:Form>
    </form>
</body>
</html>