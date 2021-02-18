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
using System.IO;

namespace Exercise_form
{
    public partial class shiyanpgui : Form
    {
        param pp;
        classinfo cl;
        View_class_exp vce;
        int sel1 = -1;
        studreport cstrep = null;
 List< View_class_student> vlst = null;
        
        string[] strobjectives=null;
        string[] cjs=null;
        public shiyanpgui(param  pp1,classinfo cl1,View_class_exp vce1)
        {
            InitializeComponent();
            pp = pp1;
            cl = cl1;
            vce = vce1;
        }

        private void shiyanpgui_Load(object sender, EventArgs e)
        {
            vlst = null;            
            var questionQuery1 = from o in pp.context.View_class_student
                                 where o.classid== cl.classid
                                 orderby o.classno
                                 select o;
            if (questionQuery1.Count<View_class_student>() > 0)
            {
                vlst = questionQuery1.ToList<View_class_student>();
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.DataSource = vlst;
            }
            String strobjective = null;
            strobjective = vce.objective;
            strobjectives = strobjective.Split('|');
            int numobjective = strobjectives.Length - 1;           
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.RowCount = numobjective +3;
           // RowStyle rs = new RowStyle(System.Windows.Forms.SizeType.Percent, 100/numobjective + 2);
           // tableLayoutPanel1.RowStyles.Add(rs);
            for (int i = 0; i < numobjective; i++)
            {
                Label l1 = new Label();
                l1.Text = "目标"+strobjectives[i+1];
                TextBox t1 = new TextBox();
                t1.Text = "";
                tableLayoutPanel1.Controls.Add(l1, 0, i);
                tableLayoutPanel1.Controls.Add(t1, 1, i);
            }
            Label l11 = new Label();
            l11.Text = "各目标分数满分";
            l11.ForeColor = Color.Red;
            Label l12 = new Label();
            l12.Text = "为100";
            l12.ForeColor = Color.Red;
            tableLayoutPanel1.Controls.Add(button1, 1, numobjective);
            tableLayoutPanel1.Controls.Add(button2, 0, numobjective);
            tableLayoutPanel1.Controls.Add(button3, 1, numobjective+1);
            tableLayoutPanel1.Controls.Add(l11, 0, numobjective + 2);
            tableLayoutPanel1.Controls.Add(l12, 1, numobjective + 2);
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.OutsetPartial;
            getrep();


        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

            sel1 = e.RowIndex;
            getrep();


        }

        ///////////getrep

        private void getrep()
        {
            cstrep = null;
            docViewer1.CloseDocument();
            button3.Enabled = true;
            if (sel1 >= 0)
            {
                // docViewer1.LoadFromStream(null, Spire.Doc.FileFormat.Docx);
                ;
                 var questionQuery1 = from o in pp.context.studreport
                                     where o.classid == cl.classid && o.expid == vce.expid && o.stid == vlst[sel1].studentid 
                                     select o;
                if (questionQuery1.Count<studreport>() > 0)
                {
                    cstrep = questionQuery1.First<studreport>();

                    if (cstrep.rep != null)
                    {

                        try
                        {
                            docViewer1.LoadFromStream(new MemoryStream(cstrep.rep), Spire.Doc.FileFormat.Docx);
                        }
                        catch
                        {
                            try { docViewer1.LoadFromStream(new MemoryStream(cstrep.rep), Spire.Doc.FileFormat.Doc); }
                            catch { MessageBox.Show("文件格式不对"); }

                        }

                       String cj = cstrep.score;
                        cjs = null;
                       if(cj!=null)  cjs = cj.Split('|');
                        if (cjs == null)
                        {
                            if (strobjectives != null)
                            {
                                int numobjective = strobjectives.Length - 1;
                                for (int i = 0; i < numobjective; i++)
                                {

                                    TextBox tt = (TextBox)tableLayoutPanel1.Controls[2 * i + 1];
                                    tt.Text = "";
                                }

                            }

                        }
                        if (cjs != null && strobjectives!=null&&cjs.Length==strobjectives.Length)
                        { 
                        int numobjective = strobjectives.Length - 1;
                         for (int i = 0; i < numobjective; i++)
                        {

                            TextBox tt = (TextBox)tableLayoutPanel1.Controls[2 * i + 1];
                            tt.Text = cjs[i+1];
                        }
                        }


                    }
                }
                else
                { button3.Enabled = false;

                    if (strobjectives != null) { 
                    int numobjective = strobjectives.Length - 1;
                    for (int i = 0; i < numobjective; i++)
                    {
                        
                        TextBox tt = (TextBox)tableLayoutPanel1.Controls[2 * i + 1];
                        tt.Text = "";
                         }

                    }

                }
                //endif (questionQuery1.Count<studreport>() > 0)


            }

            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (sel1 < vlst.LongCount()-1)
            {
                sel1 = sel1 + 1;
                getrep();
                dataGridView1.Rows[sel1].Selected=true;
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (sel1>0)
            {
                sel1 = sel1 - 1;
                getrep();
                dataGridView1.Rows[sel1].Selected = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (cstrep != null)
            {
                int numobjective = strobjectives.Length - 1;
                String scores = "";
                for (int i = 0; i < numobjective; i++)
                {
                    //TextBox tt = tableLayoutPanel1.Controls[1,i];
                    TextBox tt = (TextBox)tableLayoutPanel1.Controls[2 * i + 1];
                    if (isnum(tt))
                        scores = scores +"|"+tt.Text;
                    else
                    { MessageBox.Show("输入的值不对"); return; }
                    cstrep.score = scores;
                    pp.context.UpdateObject(cstrep);
                     pp.context.SaveChanges();


                }
                //MessageBox.Show(scores);

            }
        }
        /////////////

        private bool isnum(TextBox tb)
        {
            bool iFlag= true;

            try
            {
                int ss = int.Parse(tb.Text);
                if (ss < 0 || ss > 100) iFlag = false;
            }

            catch
            {
                iFlag = false;
            }

            

            return iFlag;
        }


        //////////





    }
}
