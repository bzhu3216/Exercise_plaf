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
namespace Exercise_form
{
    public partial class testlayout : Form
    {
        param pp;
        List<V_tea_course> lcs = null;
        List<exerL> tlvedp = null;
        public testlayout(param p)
        {
            InitializeComponent();
            pp = p;

            /*var questionQuery = from o in context.Course
                                select o;
            lcs = questionQuery.ToList<Course>();
            
            */
            lcs = pp.ltea_c;
            comboBox7.DataSource = lcs;
            comboBox7.ValueMember = "CourseName";


        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void testlayout_Load(object sender, EventArgs e)
        {






        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox7.SelectedIndex >= 0)
            {
                pp.updataccid = lcs[comboBox7.SelectedIndex].couseid;
                dataGridView2.Rows.Clear();
                dataGridView3.Rows.Clear();
                int numobjective = lcs[comboBox7.SelectedIndex].numobjective;
                int numcon = lcs[comboBox7.SelectedIndex].numcontent;

                // dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                for (int i = 0; i < numobjective; i++)
                {
                    DataGridViewRow dgvr = new DataGridViewRow();
                    foreach (DataGridViewColumn c in this.dataGridView2.Columns)
                    {

                        dgvr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                    }
                    dgvr.Cells[0].Value = i + 1;
                    this.dataGridView2.Rows.Add(dgvr);
                }
                for (int i = 0; i < numcon; i++)
                {
                    DataGridViewRow dgvr = new DataGridViewRow();
                    foreach (DataGridViewColumn c in this.dataGridView3.Columns)
                    {

                        dgvr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                    }
                    dgvr.Cells[0].Value = i + 1;
                    this.dataGridView3.Rows.Add(dgvr);
                }
                updatalist();
            }
            else
            {

                MessageBox.Show("怎么没选择课程？");
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (comboBox7.SelectedIndex >= 0 && textBox1.Text != "")
            {
                var q1 = from o in pp.context.exerL
                         where (o.name == textBox1.Text) && o.teacherid == pp.teacher.teacherid && o.pub == 3 && o.courseid == lcs[comboBox7.SelectedIndex].couseid
                         select 0;
                if (q1.Count() <= 0)
                {

                    exerL mcq = new exerL();
                    // mcq.answ = comboBox4.SelectedIndex + 1;

                    mcq.courseid = lcs[comboBox7.SelectedIndex].couseid;
                    mcq.teacherid = pp.teacher.teacherid;
                    mcq.name = textBox1.Text;
                    mcq.pub = 3;
                    ////////////write richtext
                    pp.context.AddToexerL(mcq);
                    pp.context.SaveChanges();
                    textBox1.Text = "";
                    updatalist();

                }
                else
                    MessageBox.Show("同名试卷已经存在");
            }
            else

                MessageBox.Show("怎么没选择课程？或没写名称");

        }

        private void updatalist()
        {

            tlvedp = null;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = null;
            var q1 = from o in pp.context.exerL
                     where o.courseid == lcs[comboBox7.SelectedIndex].couseid && o.pub == 3 && o.teacherid == pp.teacher.teacherid
                     select o;
            if (q1.Count<exerL>() > 0) tlvedp = q1.ToList<exerL>();
            dataGridView1.DataSource = tlvedp;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int irow = -1;
            if (dataGridView1.CurrentRow != null)
                irow = dataGridView1.CurrentRow.Index;
            if (irow >= 0 && comboBox7.SelectedIndex >= 0)
            {
                //pp.vdlword=
                EditTestPaper mq = null;
                if (mq == null || mq.IsDisposed)
                {
                    pp.elword = tlvedp[irow];
                    pp.vdlword = lcs[comboBox7.SelectedIndex];
                    mq = new EditTestPaper(pp, tlvedp[irow], lcs[comboBox7.SelectedIndex]);
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
                MessageBox.Show("请选择练习！or 课程");
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int irow = -1;
            if (dataGridView1.CurrentRow != null)
                irow = dataGridView1.CurrentRow.Index;
            if (irow >= 0)
            {
                if (MessageBox.Show("确认删除？", "此删除不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    exerL yex = tlvedp[irow];
                    pp.context.DeleteObject(yex);
                    pp.context.SaveChanges();
                    //deteldell
                    deldetail(yex);
                    updatalist();



                }
                else
                {

                    MessageBox.Show("请选择删除的试卷");
                }
            }



        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null) {
                int irow = dataGridView1.CurrentRow.Index;
                if (irow >= 0) {

                    exerL yex = tlvedp[irow];
                    if (dataGridView1.CurrentRow.Cells[2].Value.ToString() != "")
                    { if (!EXtools.isexitel(dataGridView1.CurrentRow.Cells[2].Value.ToString(), yex.courseid, pp))
                        {
                            yex.name = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                            pp.context.UpdateObject(yex);
                            pp.context.SaveChanges();
                            updatalist();
                        }
                        else
                        {
                            MessageBox.Show("名称已经存在");
                        }

                    }
                    else
                    {
                        MessageBox.Show("请datagrid中输入新名称");
                    }


                }
                else
                {

                    MessageBox.Show("请选择书卷");
                }

            }
        }
        ////
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

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Exercise_Summary mq = null;
            int irow = -1;
            if (dataGridView1.CurrentRow != null)
                irow = dataGridView1.CurrentRow.Index;
            if (irow >= 0 && comboBox7.SelectedIndex >= 0)
            {

                if (mq == null || mq.IsDisposed)
                {
                    //   pp.vdlword = null;
                    //  pp.elword = null;

                    mq = new Exercise_Summary(pp);
                    mq.textBox1.Text = EXtools.toSummary(tlvedp[irow], lcs[comboBox7.SelectedIndex], pp);
                    mq.ShowDialog();
                    // mq.Show();
                }
                else
                {
                    mq.Activate();
                    mq.WindowState = FormWindowState.Normal;
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // pp.updataccid = lcs[comboBox7.SelectedIndex].couseid;
            List<exerL> comel = new List<ServiceReference1.exerL>();

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (((bool?)(dataGridView1.Rows[i].Cells[0].Value)) == true)
                {
                    comel.Add(tlvedp[i]);
                }

            }
            if (comel.Count() > 8 || comel.Count() < 2)
                MessageBox.Show("只支持选择2-8份");
            else
            {
                compare mq = null;
                if (mq == null || mq.IsDisposed)
                {
                    //   pp.vdlword = null;
                    //  pp.elword = null;

                    mq = new compare(comel, lcs[comboBox7.SelectedIndex], pp);

                    mq.ShowDialog();
                    // mq.Show();
                }
                else
                {
                    mq.Activate();
                    mq.WindowState = FormWindowState.Normal;
                }

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {   if (!checke()) { MessageBox.Show("有参数为空");return; }
            autogen(true, int.Parse(comboBox1.Text ),int.Parse(textBox7.Text ));
        }



        public void autogen(bool flag,int nums,int totalscore)
            {

            if (flag)
            { List<int> lobjetive = new List<int>();
                List<int> objectives = new List<int>();
                int p = -1;int sump = 0;
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {   if (dataGridView2.Rows[i].Cells[1].Value != null)
                        p = int.Parse(dataGridView2.Rows[i].Cells[1].Value.ToString());
                    else
                        p = 0;
                    objectives.Add(p);
                    sump = sump + p;
                    lobjetive.Add(i + 1);


                }
                List<int> cons = new List<int>();
                int q = -1; int sumq = 0;
                for (int i = 0; i < dataGridView3.Rows.Count; i++)
                {
                    if (dataGridView3.Rows[i].Cells[1].Value != null)
                        q = int.Parse(dataGridView3.Rows[i].Cells[1].Value.ToString());
                    else
                        q = 0;
                    cons.Add(q);
                    sumq = sumq + q;
                    


                }
                if (sump != 100 || sumq!=100) { MessageBox.Show("Sum must be 100");return; }
                List<exerL> ell = new List<ServiceReference1.exerL>();
                for (int i = 0; i < nums; i++)
                { exerL el= addexerL(textBox1.Text + i);
                    if (el != null) ell.Add(el);
                    else
                        return;

                }
                updatalist();
                textBox1.Text = "";
                List<int> numt = new List<int>();
                numt.Add(int.Parse(textBox2.Text));
                numt.Add(int.Parse(textBox3.Text));
                numt.Add(int.Parse(textBox4.Text));
                numt.Add(int.Parse(textBox5.Text));
                numt.Add(int.Parse(textBox6.Text));
                List<int> snum = new List<int>();
                snum.Add(int.Parse(comboBox2.Text));
                snum.Add(int.Parse(comboBox3.Text));
                snum.Add(int.Parse(comboBox4.Text));
                snum.Add(int.Parse(comboBox5.Text));
                snum.Add(int.Parse(comboBox6.Text));
                addexerdetail(lcs[comboBox7.SelectedIndex], ell, objectives,cons, numt, snum, totalscore);



            }
            else
            { }






            
            
            }

        private bool checke()
        {
          bool  result = true;
            if (comboBox1.SelectedIndex < 0 || comboBox1.Text == "") result = false;
            if (comboBox2.SelectedIndex < 0 || comboBox2.Text == "") result = false;
            if (comboBox3.SelectedIndex < 0 || comboBox3.Text == "") result = false;
            if (comboBox4.SelectedIndex < 0 || comboBox4.Text == "") result = false;
            if (comboBox5.SelectedIndex < 0 || comboBox5.Text == "") result = false;
            if (comboBox6.SelectedIndex < 0 || comboBox6.Text == "") result = false;
            if (comboBox7.SelectedIndex < 0 || comboBox7.Text == "") result = false;
            if(textBox1.Text=="") result = false;
            if (textBox2.Text == "") result = false;
            if (textBox3.Text == "") result = false;
            if (textBox4.Text == "") result = false;
            if (textBox5.Text == "") result = false;
            if (textBox6.Text == "") result = false;
            return result;
        }

        //
        private exerL addexerL(string name)
        {
            exerL rel = null;
            var q1 = from o in pp.context.exerL
                     where (o.name == textBox1.Text) && o.teacherid == pp.teacher.teacherid && o.pub == 3 && o.courseid == lcs[comboBox7.SelectedIndex].couseid
                     select 0;
            if (q1.Count() <= 0)
            {

                exerL mcq = new exerL();
                // mcq.answ = comboBox4.SelectedIndex + 1;
                mcq.courseid = lcs[comboBox7.SelectedIndex].couseid;
                mcq.teacherid = pp.teacher.teacherid;
                mcq.name = name;
                mcq.pub = 3;
                ////////////write richtext
                pp.context.AddToexerL(mcq);
                pp.context.SaveChanges();
                rel = mcq;
            }
            else
                MessageBox.Show("同名试卷已经存在");
            return rel;
        }
        /////

        private void addexerdetail(V_tea_course vcc,List<exerL> ell1,List<int> obctivep, List<int> comp, List<int> typenum,List<int>  typescore,int totalscore)
        {
            List<List<featurehelp>> testpaper = new List<List<featurehelp>> ();
            int countell = ell1.Count();
            foreach (exerL eel in ell1)
            {
                List<featurehelp> paperf = new List<ServiceReference1.featurehelp>();
                testpaper.Add(paperf); 

            }

            List<List<featurehelp>> qlist = new List<List<featurehelp>>();
            /*
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < obctivep.Count(); j++)
                {
                    var q1 = from o in pp.context.featurehelp
                             where o.type1 == i && o.objective == j + 1 && o.courseid == vcc.couseid
                             select o;
                    List<featurehelp> fp = null;
                    fp = q1.ToList();
                    qlist.Add(fp); 

                }*/
           
                for (int j = 0; j < obctivep.Count(); j++)
                {
                    var q1 = from o in pp.context.featurehelp
                             where   o.objective == j + 1 && o.courseid == vcc.couseid
                             select o;
                    List<featurehelp> fp = null;
                    fp = q1.ToList();
                    qlist.Add(fp);

                }

          //  int totalscore = 0;
           // for (int i = 0; i < typenum.Count(); i++)
              //  totalscore = totalscore + typenum[i] * typescore[i];
            List<int> scoreofobj = new List<int>();
    
            for (int i = 0; i < obctivep.Count(); i++)
            {
                scoreofobj.Add((int)obctivep[i]* totalscore/100);
            }

            //  totalscore = totalscore + typenum[i] * typescore[i];
            List<int> scoreofcon = new List<int>();

            for (int i = 0; i < comp.Count(); i++)
            {
                scoreofcon.Add((int)comp[i] * totalscore / 100);
            }


            /////////////////////////////////////随机化各类目标
            List<List<featurehelp> > qlist2 = new List<List<featurehelp>>();
            for (int i = 0; i < qlist.Count(); i++)
            {
                List<featurehelp> tfp;
                tfp = RandomSortList<featurehelp>(qlist[i]);
                qlist2.Add(tfp); 

            }

            /////////////////////////////////
            //  List<featurehelp> templ;
            //  templ= GetRandomQue((List<featurehelp>)qlist[0],2);
            //////////////////////////////
            //List < List<featurehelp>> allqustion=new List<List<featurehelp>>() ;
            ///////产生问题
            for (int i = qlist2.Count()-1; i >=0 ; i--)
            {
               // int sumss = 0;
               // int jj = 0;
                List<bool> lflag =new List<bool> (countell);
                for (int k = 0; k < lflag.Capacity ; k++) lflag.Add(false);

                for (int j = 0; j < qlist2[i].Count(); j++)
                {     
                     int numel = j % countell;
                    /* if (numel == 0 && sumss / countell >= scoreofobj[i]) break;
                     testpaper[numel].Add(qlist2[i][j]);
                     sumss = sumss + getscore(qlist2[i][j], typescore);*/
                   // if (isobjectiveok(testpaper[numel], scoreofobj, i, typescore) )
                    if (isobjectiveok(testpaper[numel], scoreofobj, i, typescore)   && isconok(testpaper[numel], scoreofcon, (int)qlist2[i][j].con ,i, typescore))
                    {
                        testpaper[numel].Add(qlist2[i][j]);
                        
                    }
                    else
                    {
                        lflag[numel] = true;
                        
                    }
                    // ??????if (!lflag.Exists(o=>o==false)) break;
                 /*   bool flag2 = true;
                    foreach (bool bl in lflag)
                        if (!bl) flag2 = bl;
                    if(flag2) break;*/

                }
                //MessageBox.Show(i.ToString());

            }


            //////////////////////////////
            int kk = 0;
            foreach (exerL el in ell1)
            {
                toexerl(el, testpaper[kk], typescore);
                kk++;

            }

        }
        /// <summary>
        /// ///
      
        public void toexerl(exerL  el, List<featurehelp> lfh,List<int> ss)
        {
            foreach (featurehelp fp in lfh)
            { 
            exerDetail edl = new exerDetail();
            edl.lid = el.id;
            edl.qid = fp.qid ;
            edl.score = ss[fp.type1];
            edl.typeq = fp.type1 ;
             pp.context.AddToexerDetail(edl);               
            }
            pp.context.SaveChanges();
        }

        /// 
        /// 
        /// 
        /// ///
        public bool  isconok(List<featurehelp> lfp,List<int> cons,int index,int iobjective, List<int> ss)
        {
            bool result = true;
            int isum = 0; int isum2 = 0;
            foreach (featurehelp fp in lfp)
            {
                if (fp.con == index && fp.objective == (iobjective+1)) isum = isum + getscore(fp, ss);
                if (fp.con == index ) isum2 = isum2 + getscore(fp, ss);

            }
            if ( isum2>= cons[index - 1] ) result = false;

            return result;


        }

        public bool isobjectiveok(List<featurehelp> lfp, List<int> objs, int index, List<int> ss)
        {
            bool result = true;
            int isum = 0;
            foreach (featurehelp fp in lfp)
            {
                if (fp.objective == index + 1) isum = isum + getscore(fp, ss);

            }
            if (isum >= objs[index]) result = false;

            return result;


        }


        public int getscore(featurehelp fp ,List<int> ss)
        {
            int result = 0;
            result = ss[fp.type1];
            if (fp.type1 == 2) result = ss[fp.type1] * (int)fp.emnum;
            return result;
        }

        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ListT"></param>
        /// <returns></returns>
        /////////////////////////
        public List<T> RandomSortList<T>(List<T> ListT)
        {
            Random random = new Random();
            List<T> newList = new List<T>();
            foreach (T item in ListT)
            {
                newList.Insert(random.Next(newList.Count + 1), item);

            }
            return newList;
        }

        /// <summary>
        /// //////////////////////////////
        public List<T> removeList<T>(List<T> ListT1, List<T> ListT2)
        {
            List<T> newList = new List<T>();
            newList = ListT1.Except(ListT2).ToList<T>() ; 
            return newList;
        }
        /// <returns></returns>



        ///////////////////////////////////
        public List<featurehelp> GetRandomQue(List<featurehelp> quelibraryList,int inum)
        {  /*
            List<string> names = new List<string>();
            List<string> values = new List<string>();
            string itemId = Config.QueType;
            string hql = "from V_Quelibrary where state=1 and Item_Id=:Item_Id";
            names.Add("Item_Id");
            values.Add(itemId);
            List<Entities.V_Quelibrary> quelibraryList = queService.GetListQuelibraryNoPage(hql, names, values);
            */
            List <featurehelp> queList = new List<featurehelp>();

            //筛选分值为2的随机两道题
            queList.AddRange(this.RandomSortList<featurehelp>(quelibraryList).Take(inum).ToList());

            //筛选分值为5的随机两道题
           // queList.AddRange(this.RandomSortList<featurehelp>(quelibraryList.Where(t => t.Score == 5).ToList()).Take(2).ToList());

            //筛选分值为8的随机两道题
            //queList.AddRange(this.RandomSortList<featurehelp>(quelibraryList.Where(t => t.Score == 8).ToList()).Take(2).ToList());

            //筛选分值为12的随机两道题
           // queList.AddRange(this.RandomSortList<Entities.V_Quelibrary>(quelibraryList.Where(t => t.Score == 12).ToList()).Take(2).ToList());

            //筛选分值为20的随机两道题
           // queList.AddRange(this.RandomSortList<Entities.V_Quelibrary>(quelibraryList.Where(t => t.Score == 20).ToList()).Take(2).ToList());

            //获得的list再次随机排序
            queList = this.RandomSortList<featurehelp>(queList);
            return queList;
        }


        ///////////////////////////////////




        //////endcalsss
    }
}
