using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Exercise_student.ServiceExer;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using System.IO;

namespace Exercise_student
{
    public partial class shiyan : Form
    {
        paramst pp;
        classinfo clin;
        StudInfo stin;
        List<View_class_exp> lvce;
        int selexp = -1;
        public shiyan(paramst p, classinfo clin1, StudInfo stin1)
        {
            InitializeComponent();
            pp = p;
            clin = clin1;
            stin = stin1;
        }

        private void shiyan_Load(object sender, EventArgs e)
        {

            var questionQuery = from o in pp.context.View_class_exp
                                where (o.classid==clin.classid)
                                orderby o.con
                                select o;
            if (questionQuery.Count() > 0) lvce = questionQuery.ToList<View_class_exp>();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = lvce;




        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                                 e.RowBounds.Location.Y,
                                 dataGridView1.RowHeadersWidth,
                                 e.RowBounds.Height);

            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                dataGridView1.RowHeadersDefaultCellStyle.Font,
                rectangle,
                dataGridView1.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (selexp >= 0) {
                View_class_exp vp = lvce[selexp];
                var questionQuery2 = from o in pp.context.exp_q
                                     where (o.idexp == vp.expid)

                                     select o;
                exp_q qew = null;
                if (questionQuery2.Count<exp_q>() > 0)
                {
                     qew = questionQuery2.First<exp_q>();
                }
                
                saveFileDialog1.FileName = qew.docfilename;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                    string attaDirectory = saveFileDialog1.FileName;
                         Byte[] Files = qew.expdoc;
                        BinaryWriter bw = new BinaryWriter(File.Open(attaDirectory, FileMode.OpenOrCreate));
                        bw.Write(Files);
                        bw.Close();
                }

            }


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            // MessageBox.Show(e.RowIndex.ToString());
            selexp = e.RowIndex;
            getstate();
        }

        ////////////////////////////////////

        private void getstate()
        {
            if (selexp >= 0)
            {
                View_class_exp vp = lvce[selexp];                
                DateTime dt = DateTime.Now;
                if (dt > vp.starttime && vp.endtime > dt) {
                    button3.Enabled = true;
                    button4.Enabled = (bool)vp.attach;
                }


                var questionQuery = from o in pp.context.studreport
                                    where (o.classid == clin.classid && o.stid== stin.studentid && o.expid==vp.expid )
                                   
                                    select o;

                if (questionQuery.Count<studreport>() > 0) {
                    button5.Enabled = true;
                    if (questionQuery.First<studreport>().atta == null) button6.Enabled = false; else button6.Enabled = true;
                }
                else { button5.Enabled = false; button6.Enabled = false; }
                var questionQuery2= from o in pp.context.exp_q
                                    where (o.idexp== vp.expid)

                                    select o;

                if (questionQuery2.Count<exp_q>() > 0)
                {
                    exp_q qew = questionQuery2.First<exp_q>();
                   if(qew.attachment!=null) button2.Enabled = true;
                   else button2.Enabled = false;

                }


            }
            else
                MessageBox.Show("请选择表格中的行");



        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (selexp >= 0)
            {
                View_class_exp vp = lvce[selexp];
                var questionQuery2 = from o in pp.context.exp_q
                                     where (o.idexp == vp.expid)

                                     select o;
                exp_q qew = null;
                if (questionQuery2.Count<exp_q>() > 0)
                {
                    qew = questionQuery2.First<exp_q>();
                }

                saveFileDialog1.FileName = qew.attachmentname;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string attaDirectory = saveFileDialog1.FileName;
                    Byte[] Files = qew.attachment;
                    BinaryWriter bw = new BinaryWriter(File.Open(attaDirectory, FileMode.OpenOrCreate));
                    bw.Write(Files);
                    bw.Close();
                }

            }







        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (selexp >= 0)
            {
                View_class_exp vp = lvce[selexp];

                var questionQuery = from o in pp.context.studreport
                                    where (o.classid == clin.classid && o.stid == stin.studentid && o.expid == vp.expid)

                                    select o;

                if (questionQuery.Count<studreport>() > 0)
                {
                    //updata
                    studreport stp = questionQuery.First<studreport>();
                    string fileDirectory = "";
                    OpenFileDialog dialog = new OpenFileDialog();
                    dialog.Filter = "Word97-2003 files(*.doc)|*.doc|Word2007-2010 files (*.docx)|*.docx|All files (*.*)|*.*";
                    dialog.Title = "Select a DOC file";
                    dialog.Multiselect = false;
                    dialog.InitialDirectory = System.IO.Path.GetFullPath(@"..\..\..\..\..\..\Data");
                    DialogResult result = dialog.ShowDialog();
                    // if (result == DialogResult.OK)
                    fileDirectory = dialog.FileName;

                    if (!fileDirectory.Equals("") && result == DialogResult.OK)
                    {
                        FileStream fs = new FileStream(fileDirectory, System.IO.FileMode.Open);
                        BinaryReader br = new BinaryReader((Stream)fs);
                        int intLength = (int)fs.Length;
                        // MessageBox.Show(intLength.ToString());
                        if (intLength > vp.maxfile * 1024 * 1024) { MessageBox.Show("文件太大"); return; };

                        byte[] bytContent = new byte[intLength];
                        bytContent = br.ReadBytes((int)intLength);
                        stp.rep = bytContent;
                        String[] fnames = fileDirectory.Split('\\');
                        String filenamestr = fnames[fnames.Length - 1];
                        stp.fname = filenamestr;
                        //        MessageBox.Show(fileDirectory+"   "+ filenamestr);
                        pp.context.UpdateObject(stp);
                        pp.context.SaveChanges();

                    }


                }
                else
                {//add
                    studreport stp = new studreport();
                    stp.classid = clin.classid;
                    stp.expid = vp.expid;
                    stp.stid = stin.studentid;
                    string   fileDirectory = "";
                    OpenFileDialog dialog = new OpenFileDialog();
                    dialog.Filter = "Word97-2003 files(*.doc)|*.doc|Word2007-2010 files (*.docx)|*.docx|All files (*.*)|*.*";
                    dialog.Title = "Select a DOC file";
                    dialog.Multiselect = false;
                    dialog.InitialDirectory = System.IO.Path.GetFullPath(@"..\..\..\..\..\..\Data");
                    DialogResult result = dialog.ShowDialog();
                    // if (result == DialogResult.OK)
                    fileDirectory = dialog.FileName;

                        if (!fileDirectory.Equals("") && result == DialogResult.OK)
                    {
                        FileStream fs = new FileStream(fileDirectory, System.IO.FileMode.Open);
                        BinaryReader br = new BinaryReader((Stream)fs);
                        int intLength =(int) fs.Length;
                        // MessageBox.Show(intLength.ToString());
                        if (intLength > vp.maxfile * 1024 * 1024) { MessageBox.Show("文件太大");return; };

                        byte[] bytContent = new byte[intLength];
                        bytContent = br.ReadBytes((int)intLength);
                        stp.rep = bytContent;
                        String[] fnames = fileDirectory.Split('\\');
                        String filenamestr = fnames[fnames.Length - 1];
                        stp.fname  = filenamestr;
                        //        MessageBox.Show(fileDirectory+"   "+ filenamestr);
                         pp.context.AddTostudreport(stp);
                            pp.context.SaveChanges();                            

                        }                  
                    

                    }//endelse


                getstate();

            }




        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (selexp >= 0)
            {
                View_class_exp vp = lvce[selexp];

                var questionQuery = from o in pp.context.studreport
                                    where (o.classid == clin.classid && o.stid == stin.studentid && o.expid == vp.expid)

                                    select o;

                if (questionQuery.Count<studreport>() > 0)
                {
                    //updata
                    studreport stp = questionQuery.First<studreport>();
                    string fileDirectory = "";
                    OpenFileDialog dialog = new OpenFileDialog();
                    dialog.Filter = "RAR files (*.RAR)|*.RAR";
                    dialog.Title = "Select a RAR file";
                    dialog.Multiselect = false;
                    dialog.InitialDirectory = System.IO.Path.GetFullPath(@"..\..\..\..\..\..\Data");
                    DialogResult result = dialog.ShowDialog();
                    // if (result == DialogResult.OK)
                    fileDirectory = dialog.FileName;

                    if (!fileDirectory.Equals("") && result == DialogResult.OK)
                    {
                        FileStream fs = new FileStream(fileDirectory, System.IO.FileMode.Open);
                        BinaryReader br = new BinaryReader((Stream)fs);
                        int intLength = (int)fs.Length;
                        // MessageBox.Show(intLength.ToString());
                        if (intLength > vp.maxfile * 1024 * 1024) { MessageBox.Show("文件太大"); return; };

                        byte[] bytContent = new byte[intLength];
                        bytContent = br.ReadBytes((int)intLength);
                        stp.atta = bytContent;
                        String[] fnames = fileDirectory.Split('\\');
                        String filenamestr = fnames[fnames.Length - 1];
                        stp.aname = filenamestr;
                        //        MessageBox.Show(fileDirectory+"   "+ filenamestr);
                        pp.context.UpdateObject(stp);
                        pp.context.SaveChanges();

                    }


                }
                else
                {//add
                    studreport stp = new studreport();
                    stp.classid = clin.classid;
                    stp.expid = vp.expid;
                    stp.stid = stin.studentid;
                    string fileDirectory = "";
                    OpenFileDialog dialog = new OpenFileDialog();
                    dialog.Filter = "RAR files (*.RAR)|*.RAR";
                    dialog.Title = "Select a RAR file";
                    dialog.Multiselect = false;
                    dialog.InitialDirectory = System.IO.Path.GetFullPath(@"..\..\..\..\..\..\Data");
                    DialogResult result = dialog.ShowDialog();
                    // if (result == DialogResult.OK)
                    fileDirectory = dialog.FileName;

                    if (!fileDirectory.Equals("") && result == DialogResult.OK)
                    {
                        FileStream fs = new FileStream(fileDirectory, System.IO.FileMode.Open);
                        BinaryReader br = new BinaryReader((Stream)fs);
                        int intLength = (int)fs.Length;
                        // MessageBox.Show(intLength.ToString());
                        if (intLength > vp.maxfile * 1024 * 1024) { MessageBox.Show("文件太大"); return; };

                        byte[] bytContent = new byte[intLength];
                        bytContent = br.ReadBytes((int)intLength);
                        stp.atta = bytContent;
                        String[] fnames = fileDirectory.Split('\\');
                        String filenamestr = fnames[fnames.Length - 1];
                        stp.aname = filenamestr;
                        //        MessageBox.Show(fileDirectory+"   "+ filenamestr);
                        pp.context.AddTostudreport(stp);
                        pp.context.SaveChanges();

                    }


                }//endelse


                getstate();

            }








        }

        private void button5_Click(object sender, EventArgs e)
        {
            View_class_exp vp = lvce[selexp];

            var questionQuery2 = from o in pp.context.studreport
                                where (o.classid == clin.classid && o.stid == stin.studentid && o.expid == vp.expid)

                                select o;
            studreport qew = null;
                if (questionQuery2.Count<studreport>() > 0)
                {
                    qew = questionQuery2.First<studreport>();
                }

                saveFileDialog1.FileName = qew.fname;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string attaDirectory = saveFileDialog1.FileName;
                    Byte[] Files = qew.rep;
                    BinaryWriter bw = new BinaryWriter(File.Open(attaDirectory, FileMode.OpenOrCreate));
                    bw.Write(Files);
                    bw.Close();
                }

         }

        private void button6_Click(object sender, EventArgs e)
        {
            View_class_exp vp = lvce[selexp];

            var questionQuery2 = from o in pp.context.studreport
                                 where (o.classid == clin.classid && o.stid == stin.studentid && o.expid == vp.expid)

                                 select o;
            studreport qew = null;
            if (questionQuery2.Count<studreport>() > 0)
            {
                qew = questionQuery2.First<studreport>();
            }
            if (qew.atta == null) { MessageBox.Show("附件没空");return; }
            saveFileDialog1.FileName = qew.aname;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string attaDirectory = saveFileDialog1.FileName;
                Byte[] Files = qew.atta;
                BinaryWriter bw = new BinaryWriter(File.Open(attaDirectory, FileMode.OpenOrCreate));
                bw.Write(Files);
                bw.Close();
            }
        }









        ///////////////////////////////////////


    }
}
