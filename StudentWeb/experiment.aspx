<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="experiment.aspx.cs" Inherits="StudentWeb.experiment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            height: 40px;
        }
        .auto-style2 {
            height: 41px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="border: thin dashed #00FFFF; width:auto; font-family: 宋体, Arial, Helvetica, sans-serif; font-size: large; position: relative; list-style-type: circle; table-layout: auto; border-collapse: collapse; border-spacing: inherit; empty-cells: show; caption-side: bottom;" border="1">
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="点击下载实验要求"></asp:Label>
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="下载实验要求" OnClick="Button1_Click" />
                </td>               
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:Button ID="Button2" runat="server" Text="下载补充资料" OnClick="Button2_Click" />
                </td>                
            </tr>
            <tr>
                <td class="auto-style2">
                    <input id="File1" type="file" runat="server" accept=".doc,.docx"/></td>
                <td class="auto-style2">
                    <asp:Button ID="Button3" runat="server" Text="上传实验报告" OnClick="Button3_Click" />
                </td>               
            </tr>
            <tr>
                <td>
                    <input id="File2" type="file" accept=".RAR" runat="server" /></td>
                <td class="auto-style1">
                    <asp:Button ID="Button4" runat="server" Text="上传实验附件" OnClick="Button4_Click" />
                </td>               
            </tr>
            <tr>
                <td class="auto-style1">
                    &nbsp;</td>
                <td class="auto-style1">
                    <asp:Button ID="Button5" runat="server" Text="下载实验报告" OnClick="Button5_Click" />
                </td>                
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td class="auto-style1" style="border: thin dashed #00FFFF">
                    <asp:Button ID="Button6" runat="server" Text="下载实验附件" OnClick="Button6_Click" />
                </td>               
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
