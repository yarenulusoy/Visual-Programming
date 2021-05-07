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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            label1.Text = Form1.name_surname;
            label3.Text = Form2.score.ToString();
            label5.Text = (30-Form2.total_time).ToString();
            label8.Text = Form2.true1.ToString();
            label9.Text = Form2.false1.ToString();
        }
    }
}
