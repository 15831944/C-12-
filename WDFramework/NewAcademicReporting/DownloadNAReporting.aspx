<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DownloadNAReporting.aspx.cs" Inherits="WDFramework.NewAcademicReporting.DownloadNAReporting" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
                        <x:Label ID="ReportPeople" Width="50px" Label="报告人"  runat="server" />
                    </Items>
                </x:FormRow>

                 <x:FormRow>
                    <Items>
                        <x:Label ID="ReportUnit" Width="50px" Label="报告人单位"  runat="server" />
                    </Items>
                </x:FormRow>

                 <x:FormRow>
                    <Items>
                        <x:Label ID="ReportName" Width="50px" Label="学术报告名称"  runat="server" />
                    </Items>
                </x:FormRow>

                <x:FormRow>
                    <Items>
                        <x:Label ID="ReportTime" Width="50px" Label="报告时间"  runat="server" />
                    </Items>
                </x:FormRow>

                 <x:FormRow>
                    <Items>
                        <x:Label ID="ReportPlace" Width="50px" Label="报告地点"  runat="server" />
                    </Items>
                </x:FormRow>

                 <x:FormRow>
                    <Items>
                        <x:Label ID="PeopleCount" Width="50px" Label="参与人数"  runat="server" />
                    </Items>
                </x:FormRow>

                 <x:FormRow>
                    <Items>
                       <x:LinkButton ID="DownFile" Label="相关文件"  runat="server" EnableAjax="false"  Text="下载" OnClick="DownFile_Click"></x:LinkButton>
                    </Items>
                </x:FormRow>
             </Rows>
         </x:Form>
 
    </form>
</body>
</html>
