﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Exercise_student.ServiceExer;

namespace Exercise_student
{
    public partial class password : Form
    {
        paramst pp;
        public password(paramst p)
        {
            InitializeComponent();
            pp = p;
        }

        private void password_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (maskedTextBox1.Text == maskedTextBox2.Text  && maskedTextBox2.Text.Length<9 && maskedTextBox2.Text.Length >3)
            {
                var questionQuery = from o in pp.context.StudInfo 
                                    where o.studentid == pp.st.studentid 
                                    select o;


                StudInfo st = questionQuery.First<StudInfo>();
                st.pd = maskedTextBox1.Text;
                pp.context.UpdateObject(st);
                pp.context.SaveChanges();
                MessageBox.Show("密码已修改！");
                this.Close();
            }
            else
            {
                MessageBox.Show("两次的密码输入不一样,密码大于八位或小于四位！");
            }


        }
    }
}
