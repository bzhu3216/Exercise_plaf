﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="result2.aspx.cs" Inherits="StudentWeb.result2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="已保存"></asp:Label>
    
    </div>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" style="height: 21px" Text="Close" />
    </form>
</body>
</html>