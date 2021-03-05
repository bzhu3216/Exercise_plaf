<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="detail.aspx.cs" Inherits="StudentWeb.detail" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>练习</title>
</head>
<body id="document">
    <form id="form1" runat="server">
          <div>
              <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    </div>
    <div id="viewDiv" name="viewDiv" runat="server" >    
    


        &nbsp;---------------------------------------------------------------------<br />
    </div>
   &nbsp;<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit" />
    </form>


   
    </body>
</html>
