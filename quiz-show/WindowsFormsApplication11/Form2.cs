using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication11
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            timer1.Start();
        }

        int time = 30;
        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = time.ToString();
            time--;
            if(time==0)
            {
                timer1.Stop();
                Form5 frm5 = new Form5();
                frm5.Show();
                this.Hide();
            }
        }

        public static int total_time = 0;
        public static int score = 0;
        public static int true1 = 0,false1=0;
        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            if (radioButton1.Checked)
            {
                score += 5;
                total_time = time;
                true1++; 
            }
            else
            {
                score -= 2;
                total_time = time;
                false1++;
            }

            Form3 frm3 = new Form3();
            frm3.Show();
            this.Hide();
        }
    }
}
