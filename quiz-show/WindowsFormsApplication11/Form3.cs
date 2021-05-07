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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            label2.Text = time.ToString();
            timer1.Interval = 1000;
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            if (radioButton2.Checked)
            {
                Form2.score += 5;
                Form2.total_time = time;
                Form2.true1++; 
            }
            else
            {
                Form2.score -= 2;
                Form2.total_time = time;
                Form2.false1++;
            }

            Form4 ac = new Form4();
            ac.Show();
            this.Hide();
        }

        int time = Form2.total_time;
        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = time.ToString();
            time--;
            if (time == 0)
            {
                timer1.Stop();
                Form5 frm5 = new Form5();
                frm5.Show();
                this.Hide();
            }
        }
    }
}
