using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Exercise_form.ServiceReference1;
using System.Data.Services.Client;

namespace Exercise_form
{
    public partial class exerList : Form
    {
        private db_exerciseEntities context;
        param pp;
        List<V_tea_course> lcs = null;
        List<exerL> el = null;
        List<exerL> l1 = null;
        List<exerL> l2 = null;
        // List<exerL> lved= null;
        int cid = -1;
        public exerList(param p)
        {
            InitializeComponent();
            pp = p;
            context = p.context;
        }

        private void exerList_Load(object sender, EventArgs e)
        {
            context = pp.context;

            /*var questionQuery = from o in context.Course
                                select o;
            lcs = questionQuery.ToList<Course>();
            
            */
            lcs = pp.ltea_c;

            comboBox1.DataSource = lcs;
            comboBox1.ValueMember = "CourseName";
            // listBox1.Items.Clear();
            // listBox2.Items.Clear();
            // comboBox1.Text = "";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*
                        EditererList  mq = null;
                        if (mq == null || mq.IsDisposed)
                        {
                            mq = new EditererList(pp);
                            mq.ShowDialog();
                           // mq.Show();
                        }
                        else
                        {
                            mq.Activate();
                            mq.WindowState = FormWindowState.Normal;
                        }
            */
            if (listBox1.SelectedIndex >= 0)
            {

                exerL yex = l1[listBox1.SelectedIndex];
                exerL pex = new exerL();
                pex.courseid = yex.courseid;
                pex.name = yex.name + pp.teacher.teacherid;
                pex.teacherid = pp.teacher.teacherid;
                pex.pub = true;
                pp.context.AddToexerL(pex);
                pp.context.SaveChanges();
                copyedetail(yex, pex);
                /////copydetail
                updatalist();



            }
            else
            {

                MessageBox.Show("请选择私有练习");
            }









        }

        private void button1_Click(object sender, EventArgs e)
        {

            // try
            // {
            // Instantiate the DataServiceContext.


            exerL mcq = new exerL();
            // mcq.answ = comboBox4.SelectedIndex + 1;

            mcq.courseid = cid;
            mcq.teacherid = pp.teacher.teacherid;
            mcq.name = textBox1.Text;
            mcq.pub = false;
            ////////////write richtext


            if (comboBox1.Text != "" && textBox1.Text != "")
            {
                context.AddToexerL(mcq);

                context.SaveChanges();
                textBox1.Text = "";
                updatalist();


            }


            // Make the DataServiceCollection<T> the binding source for the Grid.
            //  }
            //  catch (Exception ex)
            // {
            //     MessageBox.Show(ex.ToString());
            // }









        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int numobjective = 0;
            int con = 0;
            int diff = 0;

            foreach (V_tea_course cc in lcs)
                if (comboBox1.Text == cc.CourseName)
                {
                    numobjective = (int)cc.numobjective;
                    con = (int)cc.numcontent;
                    diff = (int)cc.diff;
                    cid = (int)cc.couseid;
                }
            updatalist();
            //  el = searchall(cid);
            // if (el != null)
            // updatalist();

        }

        /////////////////////////////////my function
        private void updatalist()
        {
            el = searchall(cid);
            if (el != null)
            {
                listBox1.ValueMember = null; ;
                listBox2.ValueMember = null; ;
                listBox1.DataSource = null;
                listBox2.DataSource = null;

                l1 = null;
                l2 = null;

                var q1 = el.Where(o => o.pub == true);
                if (q1.Count<exerL>() > 0)
                    l2 = q1.ToList<exerL>();

                var q2 = el.Where(o => o.pub == false);
                if (q2.Count<exerL>() > 0)
                    l1 = q2.ToList<exerL>();

                listBox1.DataSource = l1;
                listBox2.DataSource = l2;
                listBox1.ValueMember = "name";
                listBox2.ValueMember = "name";
                /*
                context = pp.context;
                var questionQuery = from o in context.exerL 
                                    where o.courseid ==cid 
                                    select o;
                el = questionQuery.ToList<exerL>();

                foreach (exerL obel in el) {
                    if (obel.pub == true)
                        listBox1.Items.Add(obel.name);
                    if (obel.teacherid  == pp.teacher.teacherid )
                      listBox2.Items.Add(obel.name);
                }*/


            }




        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                pp.exerl1 = l1[listBox1.SelectedIndex].id;
                EditererList mq = null;
                if (mq == null || mq.IsDisposed)
                {
                    mq = new EditererList(pp);
                    mq.ShowDialog();
                    // mq.Show();
                }
                else
                {
                    mq.Activate();
                    mq.WindowState = FormWindowState.Normal;
                }
            }
            else
            {
                MessageBox.Show("请选择练习！");
            }



        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (listBox1.SelectedIndex >= 0)
            {
                pp.exerl1 = l1[listBox1.SelectedIndex].id;
                Edit_cal_exerL mq = null;
                if (mq == null || mq.IsDisposed)
                {
                    mq = new Edit_cal_exerL(l1[listBox1.SelectedIndex].id, pp);
                    mq.ShowDialog();
                    // mq.Show();
                }
                else
                {
                    mq.Activate();
                    mq.WindowState = FormWindowState.Normal;
                }
            }
            else
            {
                MessageBox.Show("请选择练习");
            }




        }

        /////////////////////////////////////////
        private List<exerL> searchall(int courseid)
        {
            List<exerL> tlvedp = null;
            var q1 = from o in pp.context.exerL
                     where o.courseid == courseid && (o.pub || o.teacherid == pp.teacher.teacherid)
                     select o;
            if (q1.Count<exerL>() > 0) tlvedp = q1.ToList<exerL>();
            return tlvedp;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex >= 0)
            {

                exerL yex = l2[listBox2.SelectedIndex];
                exerL pex = new exerL();
                pex.courseid = yex.courseid;
                pex.name = yex.name + "请重命名";
                pex.teacherid = pp.teacher.teacherid;
                pex.pub = false;
                pp.context.AddToexerL(pex);
                pp.context.SaveChanges();
                /////copydetail
                copyedetail(yex, pex);
                updatalist();



            }
            else
            {

                MessageBox.Show("请选择公有练习");
            }

        }



        private void copyedetail(exerL s, exerL target)
        {
            var q1 = from o in pp.context.exerDetail
                     where o.lid == s.id
                     select o;
            if (q1.Count<exerDetail>() > 0)
            {
                List<exerDetail> tlerd = q1.ToList<exerDetail>();
                foreach (exerDetail iexd in tlerd)
                {
                    exerDetail new_ed = new exerDetail();
                    new_ed.lid = target.id;
                    new_ed.score = iexd.score;
                    new_ed.typeq = iexd.typeq;
                    new_ed.qid = iexd.qid;
                    pp.context.AddToexerDetail(new_ed);

                }

                pp.context.SaveChanges();

            }







        }

        private void button6_Click(object sender, EventArgs e)
        {

            if (listBox1.SelectedIndex >= 0)
            {

                exerL yex = l1[listBox1.SelectedIndex];
                if (textBox1.Text != "")
                {
                    yex.name = textBox1.Text;
                    pp.context.UpdateObject(yex);
                    pp.context.SaveChanges();
                    updatalist();
                    textBox1.Text = "";
                }
                else
                {
                    MessageBox.Show("请文本框中输入新名称");
                }


            }
            else
            {

                MessageBox.Show("请选择私有练习");
            }







        }

        private void button8_Click(object sender, EventArgs e)
        {


            if (listBox2.SelectedIndex >= 0)
            {
                if (MessageBox.Show("确认删除？", "此删除不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
                { }
                exerL yex = l2[listBox2.SelectedIndex];
                pp.context.DeleteObject(yex);
                pp.context.SaveChanges();
                //deteldell
                deldetail(yex);
                updatalist();



            }
            else
            {

                MessageBox.Show("请选择公有练习");
            }



        }


        ///////////////////////////////////////////////////


        private void deldetail(exerL s)
        {
            var q1 = from o in pp.context.exerDetail
                     where o.lid == s.id
                     select o;
            if (q1.Count<exerDetail>() > 0)
            {
                List<exerDetail> tlerd = q1.ToList<exerDetail>();
                foreach (exerDetail iexd in tlerd)
                {
                    pp.context.DeleteObject(iexd);

                }

                pp.context.SaveChanges();

            }









            ///////////////////////////////////end my function


        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                if (MessageBox.Show("确认删除？已有同学用不建议删除哦", "此删除不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    exerL yex = l1[listBox1.SelectedIndex];
                    pp.context.DeleteObject(yex);
                    pp.context.SaveChanges();
                    //deteldell
                    deldetail(yex);
                    updatalist();



                }
                else
                {

                    MessageBox.Show("请选择公有练习");
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Exercise_Summary mq = null;
            if (listBox1.SelectedIndex >= 0)
            {

               

            if (mq == null || mq.IsDisposed)
                {
                    mq = new Exercise_Summary();
                    mq.textBox1.Text = EXtools.toSummary(l1[listBox1.SelectedIndex], lcs[comboBox1.SelectedIndex], pp);
                    mq.ShowDialog();
                    // mq.Show();
                }
                else
                {
                    mq.Activate();
                    mq.WindowState = FormWindowState.Normal;
                }
            }
            else
            {

                MessageBox.Show("请选择私有练习");
            }
            


        }

        ///////endclass
    }
    }
