using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StudentWeb.ServiceReference1;

namespace StudentWeb
{
    public partial class change_password : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            Global gb = Session["gb"] as Global; ;
            paramst pp = gb.pp;

            StudInfo st = Session["user"] as StudInfo;


            if (TextBox1.Text == TextBox2.Text && TextBox2.Text != "")
            {
                // var questionQuery1 = from o in pp.context.StudInfo
                //                     where o.studentid == st.studentid
                //                  select o;

                st.pd = TextBox1.Text;
                pp.context.UpdateObject(st);
                pp.context.SaveChanges();
                Response.Write("<script>window.alert('修改成功')</script>");

            }
            else
            {
                Response.Write("<script>window.alert('密码不一致')</script>");
            }



        }
    }
}