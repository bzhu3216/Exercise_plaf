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
                    if (fexist)
                        {
                        featurehelp fhelp = new featurehelp();
                        fhelp.qid = mq.id;
                        fhelp.type1 = 0;                    
                        
                        string fstr = null;
                        if (strq.Length < 21)
                            fstr = strq;
                        else
                            fstr = strq.Substring(0, 20).Trim();
                        StringBuilder sb = new StringBuilder(fstr);
                        fhelp.featurestr = sb.ToString();
                        pp.context.AddTofeaturehelp(fhelp);                        
                        
                    }                   
                }
                pp.context.SaveChanges();


            }
        }
//////////////////////////////////////////

    }
}
