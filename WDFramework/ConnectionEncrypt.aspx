<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConnectionEncrypt.aspx.cs" Inherits="WDFramework.ConnectionEncrypt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
      <form id="form1"  runat="server">
        <div>
          <asp:Button id="btnEncrypt" runat="server" Text="Encrypt" onclick="btnEncrypt_Click" />
          <asp:Button ID="btnDecrypt" runat="server" Text="Decrypt" onclick="btnDecrypt_Click" />
        </div>
      </form>
    </body>
</html>
