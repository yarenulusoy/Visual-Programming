//Yasin Ertune - Bilgi testi uygulamasi
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static string name_surname="";
        private void button1_Click(object sender, EventArgs e)
        {
            //isim soyisim bilgisi aldik
            name_surname = textBox1.Text + "  " + textBox2.Text;
            Form2 frm2 = new Form2();
            frm2.Show();
            this.Hide();
        }
    }
}
