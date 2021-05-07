//Yasin Ertune - Yaş hesaplama Uygulaması
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TimeSpan difference_day = DateTime.Today - dateTimePicker1.Value;
            int difference = difference_day.Days;
            int year = difference / 365;
            int day = difference % 365;
            textBox1.Text = year.ToString() + " Yaşındasınız. " + (365-day).ToString() + " Gün sonra "+(year+1).ToString()+" Yaşında olacaksınız.";
            

        }
    }
}
