﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Year_type.aspx.cs" Inherits="WDFramework.Report.Year_type" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div>
    <rsweb:ReportViewer  style="margin:auto auto;" ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" ProcessingMode="Remote" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"  Width="1130px"  Height ="825px">
        <serverreport reportpath="/报表项目2/Year_type" reportserverurl="http://win-ja7hpm931cq/ReportServer_SA" />
        </rsweb:ReportViewer>
    </div>
    </form>
</body>


</html>
