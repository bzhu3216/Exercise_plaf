using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StudentWeb.ServiceReference1;

namespace StudentWeb
{
    public partial class TaskList : System.Web.UI.Page
    {
        public StudInfo st;
        public String stid = null;
        Global gb = new Global();
        paramst pp;
        public List<class_student> lcsl = null;
        public List<classinfo> lclinfo = null;
        public List<classExer> lce = null;
        public List<exerL> erl = null;
        int sel1 = -1;
        List<Object> ltemp = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            st = Session["user"] as StudInfo;
            stid = st.studentid;
            Label1.Text = "欢迎：" + stid;
            pp = gb.pp;

            lcsl = getcsl(stid);
            lclinfo = getclassinfo(lcsl);
           
            /*
            DropDownList1.DataSource= lclinfo;
            //DropDownList1.DataSourceID   = "classinfo1";
            DropDownList1.DataTextField = "classinfo1";
            DropDownList1.DataBind();
            if (Session["cindex"]as string  != null) DropDownList1.SelectedIndex =int.Parse( Session["cindex"]as string) ;
           // DropDownList1.SelectedIndex = 1;
            GridView1.DataSource = ltemp;
            GridView1.DataBind();
            */

        }
       
        protected void Page_LoadComplete(object sender, EventArgs e)
        {

            DropDownList1.DataSource = lclinfo;
            //DropDownList1.DataSourceID   = "classinfo1";
            DropDownList1.DataTextField = "classinfo1";
            DropDownList1.DataBind();
            string setindex = null;
            if (Session["cindex"] != null) setindex = Session["cindex"].ToString();
            if (setindex != null) DropDownList1.SelectedIndex = int.Parse(setindex);
            // DropDownList1.SelectedIndex = 1;
            GridView1.DataSource = ltemp;
            GridView1.DataBind();
            DropDownList1_SelectedIndexChanged(sender, e);

        }

        private List<class_student> getcsl(String sid)
        {
            List<class_student> csl = null;
            var q1 = from c in pp.context.class_student
                     where c.studentid == sid
                     select c;
            csl = q1.ToList<class_student>();
            return csl;
        }
        private List<classinfo> getclassinfo(List<class_student> tlcs)
        {
            List<classinfo> tcsl = null;
            List<classinfo> tcsl2 = null;
            try
            {
                var q1 = from o in pp.context.classinfo
                         where o.finish == 0
                         select o;
                tcsl2 = q1.ToList<classinfo>();
                var qtcsl = tcsl2.Where(p => tlcs.Any(c => c.classid == p.classid));
                tcsl = qtcsl.ToList<classinfo>();

            }
            catch (Exception e1)
            {
               // MessageBox.Show(e.Message);
            }


            return tcsl;
        }
        ///////

        private List<classExer> getclexerl(classinfo tcin)
        {
            List<classExer> tel = null;
            var q1 = from o in pp.context.classExer
                     where o.cid == tcin.classid
                     select o;

            tel = q1.ToList();

            return tel;
        }


        /// //////////
        private List<exerL> getexerl2(classinfo tcin)
        {
            List<exerL> tel = null;
            var q1 = from o in pp.context.exerL
                     where o.courseid == tcin.courseid
                     select o;
            tel = q1.ToList();
            return tel;
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Response.Write("<script>window.alert('your message')</script>");
            Session.Add("cindex", DropDownList1.SelectedIndex);
           if (DropDownList1.SelectedIndex >= 0)
           {
                sel1 = DropDownList1.SelectedIndex;
           // Response.Write(@"<script>window.alert('"+sel1+@"')</script>");
            classinfo cin = lclinfo[sel1];
                lce = getclexerl(cin);
                erl = getexerl2(cin);
               GridView1.DataSource = null;
              GridView1.DataBind();
            //  persons.Join(cities, p => p.CityID, c => c.ID, (p, c) => new { PersonName = p.Name, CityName = c.Name });

            var q1 = lce.Join(erl, p => p.eid, c => c.id, (p, c) => new { eid = p.eid, ename = c.name, stime = p.starttime, etime = p.endtime });

                ltemp = q1.OrderBy(s => s.stime).ToList<Object>();
                GridView1.DataSource = ltemp;
                GridView1.DataBind();
                Session.Add("ltemp", ltemp);

           }


         }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void lbtnPhoneHide_Click(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;
            DataControlFieldCell dcf = (DataControlFieldCell)lb.Parent;
            GridViewRow gvr = (GridViewRow)dcf.Parent;
            GridView1.SelectedIndex = gvr.RowIndex;
            int tsel= gvr.RowIndex;
            Session.Add("Lexserise", lb.Text);
            // Response.Write(@"<script>window.alert('" + sender.ToString() + @"')</script>");

            //  Response.Redirect("detail.aspx",false ) ;
            // Server.Transfer("detail.aspx");
            if(RadioButtonList1.SelectedValue=="0")
            Response.Write("<script>window.open('detail.aspx','_blank')</script>");
            if (RadioButtonList1.SelectedValue == "1")
                Response.Write("<script>window.open('detail2.aspx','_blank')</script>");

        }

    }


}