using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StudentWeb.ServiceReference1;

namespace StudentWeb
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Global gb = new Global();
            paramst p = gb.pp;
            Session.Add("gb", gb);
            var questionQuery = from o in p.context.StudInfo
                                where (o.studentid == TextBox1.Text) && (o.pd == TextBox2.Text)
                                select o;
           // Response.Write("<script>window.alert('your message')</script>");

            try
            {
                StudInfo st = questionQuery.First<StudInfo>();
                Session.Add("user", st);
                Response.Redirect("TaskList");

                // Response.Write(@"<script>window.alert('ok')</script>");

            }
            catch(Exception e1)
            {
                String stre = e1.Message+"(无此账号或密码错误)";

                
                Response.Write(@"<script>window.alert('"+ stre + @"')</script>");

            }
        }
    }
}