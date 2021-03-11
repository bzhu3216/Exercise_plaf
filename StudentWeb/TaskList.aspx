<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TaskList.aspx.cs" Inherits="StudentWeb.TaskList" EnableSessionState="True" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
  
<body>
    
    <form id="form1" runat="server">
    <div>    
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label> 
        <asp:Button ID="Button1" runat="server" Text="修改密码" OnClick="Button1_Click" />
        
    </div>
    <div> 
        <asp:DropDownList ID="DropDownList1" runat="server" Height="50px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" Width="225px" AutoPostBack="True">
        </asp:DropDownList>
        
    </div>
        <div>
    
    </div>
         <div>
    
    </div>
     <div>
         <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
             <asp:ListItem Value="0">选择题</asp:ListItem>
             <asp:ListItem Value="1">判断题</asp:ListItem>
             <asp:ListItem Value="2">简答题</asp:ListItem>
             <asp:ListItem Value="3">分析题</asp:ListItem>
         </asp:RadioButtonList>
    </div>
    <div>
    
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
            <Columns>
                <asp:TemplateField HeaderText="编号" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="" OnClick="lbtnPhoneHide_Click" Text='<%# Eval("eid") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ename" HeaderText="名称" />
                <asp:BoundField DataField="stime" HeaderText="开始日期" />
                <asp:BoundField DataField="etime" HeaderText="结束日期（不包含）" />
            </Columns>
        </asp:GridView>
    
        <br />
    
    </div>
<div>
    实验部分
     
    <br />
    <br />
    </div>
       <div>
    
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
            <Columns>
                <asp:TemplateField HeaderText="编号" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="false" CommandName="" OnClick="lbtnPhoneHide2_Click" Text='<%# Eval("expid") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="exname" HeaderText="名称" />
                <asp:BoundField DataField="starttime" HeaderText="开始日期" />
                <asp:BoundField DataField="endtime" HeaderText="结束日期（不包含）" />
            </Columns>
        </asp:GridView>
    
    </div> 
    </form>
</body>
</html>
