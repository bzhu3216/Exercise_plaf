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
    public partial class Check : Form
    {
        param pp;
        List<helpsimilar> hsl = new List<helpsimilar>();
        public Check(param p)
        {
            InitializeComponent();
            pp = p;

        }

        private void Check_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var q1 = from o in pp.context.mchoiceQues
                     select o;
            List<mchoiceQues> lmq = null;
            EXtools etool = new Exercise_form.EXtools();
            List<featurehelp> lfea = null;

            if (q1.Count() > 0)
            {
                lfea = etool.checkfeature (0, pp);
                lmq = q1.ToList<mchoiceQues>();
                List<mchoiceQues> lmq2 = new List<mchoiceQues>(lmq.ToArray());
                  
              
                foreach (mchoiceQues mq in lmq2)
                {
                    bool fexist = true;
                    if (lfea != null)
                    {
                        var q2 = from k in lfea
                                 where k.qid == mq.id && k.type1 == 0
                                 select k;
                         if (q2.Count() > 0) fexist = false;
                    }
                    richTextBox1.Text = "";
                   System.IO.MemoryStream mstream = new System.IO.MemoryStream(mq.question, false);
                    this.richTextBox1.LoadFile(mstream, RichTextBoxStreamType.RichText);
                    string strq = richTextBox1.Text;
                    string fstr = null;
                    if (fexist)
                        {                                           
                         
                        if (strq.Length < 21)
                            fstr = strq;
                        else
                            fstr = strq.Substring(0, 20).Trim();
                        
                        featurehelp fhelp = new featurehelp();
                        ///
                        fhelp.con = mq.con;
                        fhelp.objective = mq.objective;
                        fhelp.diff = mq.diff;
                        fhelp.courseid = mq.courseid;
                        ////

                        fhelp.qid = mq.id;
                        fhelp.type1 = 0;
                        fhelp.featurestr = fstr.ToString();
                        pp.context.AddTofeaturehelp(fhelp);                        
                        
                    }                   
                }
                pp.context.SaveChanges();
            }
////////////////////////////////////////////////////////////////////////////////////////
          var   q11 = from o in pp.context.TFQues 
                     select o;
            List<TFQues> lTF = null;
            //EXtools etool = new Exercise_form.EXtools();
            
            if (q11.Count() > 0)
            {
                lfea = etool.checkfeature(1, pp);
                lTF = q11.ToList<TFQues>();
                List<TFQues> lmq2 = new List<TFQues>(lTF.ToArray());


                foreach (TFQues mq in lmq2)
                {
                    bool fexist = true;
                    if (lfea != null)
                    {
                        var q2 = from k in lfea
                                 where k.qid == mq.id && k.type1 == 1
                                 select k;
                        if (q2.Count() > 0) fexist = false;
                    }
                    richTextBox1.Text = "";
                    System.IO.MemoryStream mstream = new System.IO.MemoryStream(mq.question, false);
                    this.richTextBox1.LoadFile(mstream, RichTextBoxStreamType.RichText);
                    string strq = richTextBox1.Text;
                    string fstr = null;
                    if (fexist)
                    {

                        if (strq.Length < 21)
                            fstr = strq;
                        else
                            fstr = strq.Substring(0, 20).Trim();

                        featurehelp fhelp = new featurehelp();
                        ///
                        fhelp.con = mq.con;
                        fhelp.objective = mq.objective;
                        fhelp.diff = mq.diff;
                        fhelp.courseid = mq.courseid;
                        ////
                        fhelp.qid = mq.id;
                        fhelp.type1 = 1;
                        fhelp.featurestr = fstr.ToString();
                        pp.context.AddTofeaturehelp(fhelp);
                    }
                }
                pp.context.SaveChanges();
            }

            /////////////////////////////////////////////////////

            var q12 = from o in pp.context.eQues 
                      select o;
            List<eQues> le = null;
            //EXtools etool = new Exercise_form.EXtools();

            if (q12.Count() > 0)
            {
                lfea = etool.checkfeature(2, pp);
                le = q12.ToList<eQues>();
                List<eQues> lmq2 = new List<eQues>(le.ToArray());
                foreach (eQues mq in lmq2)
                {
                    bool fexist = true;
                    if (lfea != null)
                    {
                        var q2 = from k in lfea
                                 where k.qid == mq.id && k.type1 == 2
                                 select k;
                        if (q2.Count() > 0) fexist = false;
                    }
                    richTextBox1.Text = "";
                    System.IO.MemoryStream mstream = new System.IO.MemoryStream(mq.question, false);
                    this.richTextBox1.LoadFile(mstream, RichTextBoxStreamType.RichText);
                    string strq = richTextBox1.Text;
                    string fstr = null;
                    if (fexist)
                    {

                        if (strq.Length < 21)
                            fstr = strq;
                        else
                            fstr = strq.Substring(0, 20).Trim();

                        featurehelp fhelp = new featurehelp();
                        ///
                        fhelp.con = mq.con;
                        fhelp.objective = mq.objective;
                        fhelp.diff = mq.diff;
                        fhelp.courseid = mq.courseid;
                        ////
                        fhelp.qid = mq.id;
                        fhelp.type1 = 2;
                        fhelp.emnum = mq.emnum;
                        fhelp.featurestr = fstr.ToString();
                        pp.context.AddTofeaturehelp(fhelp);
                    }
                }
                pp.context.SaveChanges();
            }
            /////////////////////////////////////////////////////////////////////////////////////////////////////////

            var q13 = from o in pp.context.SQues 
                      select o;
            List<SQues> ls = null;
            //EXtools etool = new Exercise_form.EXtools();

            if (q13.Count() > 0)
            {
                lfea = etool.checkfeature(3, pp);
                ls = q13.ToList<SQues>();
                List<SQues> lmq2 = new List<SQues>(ls.ToArray());
                foreach (SQues mq in lmq2)
                {
                    bool fexist = true;
                    if (lfea != null)
                    {
                        var q2 = from k in lfea
                                 where k.qid == mq.id && k.type1 == 3
                                 select k;
                        if (q2.Count() > 0) fexist = false;
                    }
                    richTextBox1.Text = "";
                    System.IO.MemoryStream mstream = new System.IO.MemoryStream(mq.question, false);
                    this.richTextBox1.LoadFile(mstream, RichTextBoxStreamType.RichText);
                    string strq = richTextBox1.Text;
                    string fstr = null;
                    if (fexist)
                    {

                        if (strq.Length < 31)
                            fstr = strq;
                        else
                            fstr = strq.Substring(0, 30).Trim();

                        featurehelp fhelp = new featurehelp();
                        ///
                        fhelp.con = mq.con;
                        fhelp.objective = mq.objective;
                        fhelp.diff = mq.diff;
                        fhelp.courseid = mq.courseid;
                        ////
                        fhelp.qid = mq.id;
                        fhelp.type1 = 3;
                        fhelp.featurestr = fstr.ToString();
                        pp.context.AddTofeaturehelp(fhelp);
                    }
                }
                pp.context.SaveChanges();
            }


            ///////////////////////////////////////////////////////////////////////////////////////
            var q14 = from o in pp.context.AQues 
                      select o;
            List<AQues> lA = null;
            //EXtools etool = new Exercise_form.EXtools();

            if (q14.Count() > 0)
            {
                lfea = etool.checkfeature(4, pp);
                lA = q14.ToList<AQues>();
                List<AQues> lmq2 = new List<AQues>(lA.ToArray());
                foreach (AQues mq in lmq2)
                {
                    bool fexist = true;
                    if (lfea != null)
                    {
                        var q2 = from k in lfea
                                 where k.qid == mq.id && k.type1 == 4
                                 select k;
                        if (q2.Count() > 0) fexist = false;
                    }
                    richTextBox1.Text = "";
                    System.IO.MemoryStream mstream = new System.IO.MemoryStream(mq.question, false);
                    this.richTextBox1.LoadFile(mstream, RichTextBoxStreamType.RichText);
                    string strq = richTextBox1.Text;
                    string fstr = null;
                    if (fexist)
                    {
                        int ll = strq.Length;
                        if (ll < 21)
                            fstr = strq.Trim();
                        else
                        { 
                        if (ll < 41)
                            fstr = strq.Substring(ll-21, 20).Trim();
                        else
                            fstr = strq.Substring(10, 20).Trim();
                        }
                        featurehelp fhelp = new featurehelp();
                        ///
                        fhelp.con = mq.con;
                        fhelp.objective = mq.objective;
                        fhelp.diff = mq.diff;
                        fhelp.courseid = mq.courseid;
                        ////
                        fhelp.qid = mq.id;
                        fhelp.type1 =4;
                        fhelp.featurestr = fstr.ToString();
                        pp.context.AddTofeaturehelp(fhelp);
                    }
                }
                pp.context.SaveChanges();
            }

            MessageBox.Show("ok"); 




        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            EXtools etool = new Exercise_form.EXtools();
            List<featurehelp> lfea = null;
            // List<helpsimilar> hsl = new List<Exercise_form.helpsimilar>();
            hsl.Clear();
             lfea = etool.checkfeature(0, pp);
            foreach (featurehelp fp in lfea)
            {
                helpsimilar hs = new Exercise_form.helpsimilar();
                hs.id = fp.id;
                hs.qid = fp.qid;
                hs.sim = -1;
                hs.str = fp.featurestr;
                hs.type1 = fp.type1;
                hsl.Add(hs);

            }
            for (int i = 0; i < hsl.Count()-1; i++)
                for (int j = i+1; j < hsl.Count(); j++)
                {  if(hsl[j].sim==-1)
                    if (EXtools.degreeofsimilarity(hsl[i].str , hsl[j].str) > 0.9) { hsl[j].sim = hsl[i].qid;     }

                }

            dataGridView1.DataSource = hsl;
            //dataGridView1.DataSource = hsl;





        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            EXtools etool = new Exercise_form.EXtools();
            List<featurehelp> lfea = null;
            // List<helpsimilar> hsl = new List<Exercise_form.helpsimilar>();
            hsl.Clear();
            lfea = etool.checkfeature(1, pp);
            foreach (featurehelp fp in lfea)
            {
                helpsimilar hs = new Exercise_form.helpsimilar();
                hs.id = fp.id;
                hs.qid = fp.qid;
                hs.sim = -1;
                hs.str = fp.featurestr;
                hs.type1 = fp.type1;
                hsl.Add(hs);

            }
            for (int i = 0; i < hsl.Count() - 1; i++)
                for (int j = i + 1; j < hsl.Count(); j++)
                {
                    if (hsl[j].sim == -1)
                        if (EXtools.degreeofsimilarity(hsl[i].str, hsl[j].str) > 0.9) { hsl[j].sim = hsl[i].qid; }

                }

            dataGridView1.DataSource = hsl;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            EXtools etool = new Exercise_form.EXtools();
            List<featurehelp> lfea = null;
            // List<helpsimilar> hsl = new List<Exercise_form.helpsimilar>();
            hsl.Clear();
            lfea = etool.checkfeature(2, pp);
            foreach (featurehelp fp in lfea)
            {
                helpsimilar hs = new Exercise_form.helpsimilar();
                hs.id = fp.id;
                hs.qid = fp.qid;
                hs.sim = -1;
                hs.str = fp.featurestr;
                hs.type1 = fp.type1;
                hsl.Add(hs);

            }
            for (int i = 0; i < hsl.Count() - 1; i++)
                for (int j = i + 1; j < hsl.Count(); j++)
                {
                    if (hsl[j].sim == -1)
                        if (EXtools.degreeofsimilarity(hsl[i].str, hsl[j].str) > 0.9) { hsl[j].sim = hsl[i].qid; }

                }

            dataGridView1.DataSource = hsl;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            EXtools etool = new Exercise_form.EXtools();
            List<featurehelp> lfea = null;
            // List<helpsimilar> hsl = new List<Exercise_form.helpsimilar>();
            hsl.Clear();
            lfea = etool.checkfeature(3, pp);
            foreach (featurehelp fp in lfea)
            {
                helpsimilar hs = new Exercise_form.helpsimilar();
                hs.id = fp.id;
                hs.qid = fp.qid;
                hs.sim = -1;
                hs.str = fp.featurestr;
                hs.type1 = fp.type1;
                hsl.Add(hs);

            }
            for (int i = 0; i < hsl.Count() - 1; i++)
                for (int j = i + 1; j < hsl.Count(); j++)
                {
                    if (hsl[j].sim == -1)
                        if (EXtools.degreeofsimilarity(hsl[i].str, hsl[j].str) > 0.9) { hsl[j].sim = hsl[i].qid; }

                }

            dataGridView1.DataSource = hsl;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            EXtools etool = new Exercise_form.EXtools();
            List<featurehelp> lfea = null;
            // List<helpsimilar> hsl = new List<Exercise_form.helpsimilar>();
            hsl.Clear();
            lfea = etool.checkfeature(4, pp);
            foreach (featurehelp fp in lfea)
            {
                helpsimilar hs = new Exercise_form.helpsimilar();
                hs.id = fp.id;
                hs.qid = fp.qid;
                hs.sim = -1;
                hs.str = fp.featurestr;
                hs.type1 = fp.type1;
                hsl.Add(hs);

            }
            for (int i = 0; i < hsl.Count() - 1; i++)
                for (int j = i + 1; j < hsl.Count(); j++)
                {
                    if (hsl[j].sim == -1)
                        if (EXtools.degreeofsimilarity(hsl[i].str, hsl[j].str) > 0.9) { hsl[j].sim = hsl[i].qid; }

                }

            dataGridView1.DataSource = hsl;
        }

        private void button7_Click(object sender, EventArgs e)
        {
           
                int irow = -1;
                if (dataGridView1.CurrentCell != null)
                    irow = dataGridView1.CurrentCell.RowIndex;
                DialogResult result = MessageBox.Show("确定删除吗？", "删除", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes && irow >= 0)
                {
                    helpsimilar ths = hsl[irow];
                    if (ths.sim != -1)
                    {
                        if (ths.type1 == 0)
                        {
                            var q1 = from o in pp.context.mchoiceQues
                                     where o.id == ths.qid
                                     select o;
                            mchoiceQues m1 = q1.First();
                            pp.context.DeleteObject(m1);
                            var q2 = from o in pp.context.exerDetail
                                     where o.qid == ths.qid && o.typeq == 0
                                     select o;
                            List<exerDetail> tl = null;
                            if (q2.Count() > 0)
                            {
                                tl = q2.ToList();
                                foreach (exerDetail ed in tl)
                                {
                                    ed.qid = ths.sim;
                                    pp.context.UpdateObject(ed);
                                    MessageBox.Show("updata:" + ed.id);
                                    EXtools.checkd(pp, ed.lid);

                                }

                            }


                        }
                    try
                    {
                        if (ths.type1 == 1)
                        {
                            var q1 = from o in pp.context.TFQues
                                     where o.id == ths.qid
                                     select o;
                            TFQues m1 = q1.First();
                            pp.context.DeleteObject(m1);
                            var q2 = from o in pp.context.exerDetail
                                     where o.qid == ths.qid && o.typeq == 1
                                     select o;
                            List<exerDetail> tl = null;
                            if (q2.Count() > 0)
                            {
                                tl = q2.ToList();
                                foreach (exerDetail ed in tl)
                                {
                                    ed.qid = ths.sim;
                                    pp.context.UpdateObject(ed);
                                    MessageBox.Show("updata:" + ed.id);
                                    EXtools.checkd(pp, ed.lid);
                                }

                            }
                        }
                    }
            catch (InvalidOperationException e1)
            {
                MessageBox.Show(e1.Message.ToString());
            }
             catch (ArgumentNullException  e1)
             {
                        MessageBox.Show(e1.Message.ToString());
             }
                    catch (Exception e1)
                    {
                        MessageBox.Show(e1.Message.ToString());
                    }


                    if (ths.type1 == 2)
                        {
                            var q1 = from o in pp.context.eQues
                                     where o.id == ths.qid
                                     select o;
                            eQues m1 = q1.First();
                            pp.context.DeleteObject(m1);
                            var q2 = from o in pp.context.exerDetail
                                     where o.qid == ths.qid && o.typeq == 2
                                     select o;
                            List<exerDetail> tl = null;
                            if (q2.Count() > 0)
                            {
                                tl = q2.ToList();
                                foreach (exerDetail ed in tl)
                                {
                                    ed.qid = ths.sim;
                                    pp.context.UpdateObject(ed);
                                    MessageBox.Show("updata:" + ed.id);
                                    EXtools.checkd(pp, ed.lid);
                                }

                            }
                        }
                        if (ths.type1 == 3)
                        {
                            var q1 = from o in pp.context.SQues
                                     where o.id == ths.qid
                                     select o;
                            SQues m1 = q1.First();
                            pp.context.DeleteObject(m1);
                            var q2 = from o in pp.context.exerDetail
                                     where o.qid == ths.qid && o.typeq == 3
                                     select o;
                            List<exerDetail> tl = null;
                            if (q2.Count() > 0)
                            {
                                tl = q2.ToList();
                                foreach (exerDetail ed in tl)
                                {
                                    ed.qid = ths.sim;
                                    pp.context.UpdateObject(ed);
                                    MessageBox.Show("updata:" + ed.id);
                                    EXtools.checkd(pp, ed.lid);
                                }

                            }
                        }
                        if (ths.type1 == 4)
                        {
                            var q1 = from o in pp.context.AQues
                                     where o.id == ths.qid
                                     select o;
                            AQues m1 = q1.First();
                            pp.context.DeleteObject(m1);
                            var q2 = from o in pp.context.exerDetail
                                     where o.qid == ths.qid && o.typeq == 4
                                     select o;
                            List<exerDetail> tl = null;
                            if (q2.Count() > 0)
                            {
                                tl = q2.ToList();
                                foreach (exerDetail ed in tl)
                                {
                                    ed.qid = ths.sim;
                                    pp.context.UpdateObject(ed);
                                    MessageBox.Show("updata:" + ed.id);
                                    EXtools.checkd(pp, ed.lid);
                                }

                            }
                        }
                    //del question over updata over
                    //del theinformation
                    
                    try
                    {
                        var q3 = from o in pp.context.featurehelp
                                 where o.id == ths.id
                                 select o;
                        featurehelp fp2 = q3.First();
                        pp.context.DeleteObject(fp2);
                        pp.context.SaveChanges();
                        dataGridView1.DataSource = null;
                        hsl.Remove(ths);
                        dataGridView1.DataSource = hsl;
                    }
                    catch (Exception e1)
                    { MessageBox.Show(e1.ToString()); }


                    }
                    else
                    {

                        MessageBox.Show("Can not be deleted");
                    }

                }
            

        }

        //////////////////////////////////////////

    }
}
