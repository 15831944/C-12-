<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="R_AcceptUnit_RemainMoney.aspx.cs" Inherits="WDFramework.Report.R_AcceptUnit_RemainMoney" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div>
            <rsweb:ReportViewer Style="margin: auto auto;" ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" ProcessingMode="Remote" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="923px"  Height ="825px">
                <ServerReport ReportPath="/报表项目2/R_AcceptUnit_RemainMoney" ReportServerUrl="http://win-ja7hpm931cq/ReportServer_SA" />
            </rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
