<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="change password.aspx.cs" Inherits="StudentWeb.change_password" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        新密码&nbsp;&nbsp; <asp:TextBox ID="TextBox1" runat="server" TextMode="Password"></asp:TextBox>

        <br />
        确认密码&nbsp; 
        <asp:TextBox ID="TextBox2" runat="server" TextMode="Password"></asp:TextBox>
        <br />
        <br />
        <br />
     &nbsp;   &nbsp;  &nbsp; &nbsp; &nbsp; <asp:Button ID="Button1" runat="server" Text="修改密码" OnClick="Button1_Click" />

    </form>
</body>
</html>
