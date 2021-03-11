<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="StudentWeb.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>登录</title>
    <style type="text/css">
        .auto-style1 {
            height: 23px;
        }
    </style>
</head>
<body>
    
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%; background-color: #00FFFF; vertical-align: middle; text-align: center;">
           <tr>
                <td></td>
                <td style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: 60px; font-weight: normal; font-style: normal; color: #FF0000">学生作业实验提交小工具
                </td>
                <td></td>
            </tr>
             <tr>
                <td class="auto-style1">用户名</td>
                <td class="auto-style1">
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style1"></td>
            </tr>
            <tr>
                <td>密码</td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server" TextMode="Password">12345678</asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="登录" />
                </td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td>
                   初始密码11111111 
                </td>
                <td></td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
