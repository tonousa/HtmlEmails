<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm.aspx.cs" Inherits="HtmlEmails.WebForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="UserNameLbl" runat="server" Text="User Name"></asp:Label><br />
        <asp:TextBox ID="UserName" runat="server"></asp:TextBox><br />
        <asp:Button ID="Submit" runat="server" Text="Submit" />
    </div>
    </form>
</body>
</html>
