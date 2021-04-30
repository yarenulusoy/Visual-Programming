using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Teknik_Servis
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form8 frm8 = new Form8();
            frm8.Show();
            this.Close();
            frm8.panel1.Controls.Clear(); // Panel'in içini temizliyoruz
            Form4 frm4 = new Form4();

            frm4.TopLevel = false;
            frm8.panel1.Controls.Add(frm4); // panel1 içerisinde formu ekledik

            frm4.Show(); // formu gösterdik.
            frm4.Dock = DockStyle.Fill; // Açılan formun paneli doldurmasını sağladık.
            frm4.BringToFront(); // formu panel içinde en öne getirdik
        }
        private void button2_Click(object sender, EventArgs e)
        {

            Form8 frm8 = new Form8();
            frm8.Show();
            this.Close();
            frm8.panel1.Controls.Clear(); // Panel'in içini temizliyoruz
            Form9 frm9 = new Form9();

            frm9.TopLevel = false;
            frm8.panel1.Controls.Add(frm9); // panel1 içerisinde formu ekledik

            frm9.Show(); // formu gösterdik.
            frm9.Dock = DockStyle.Fill; // Açılan formun paneli doldurmasını sağladık.
            frm9.BringToFront(); // formu panel içinde en öne getirdik
        }
        private void button3_Click(object sender, EventArgs e)
        {

            Form8 frm8 = new Form8();
            frm8.Show();
            this.Close();
            frm8.panel1.Controls.Clear(); // Panel'in içini temizliyoruz
            Form5 frm5 = new Form5();

            frm5.TopLevel = false;
            frm8.panel1.Controls.Add(frm5); // panel1 içerisinde formu ekledik

            frm5.Show(); // formu gösterdik. 
            frm5.Dock = DockStyle.Fill; // Açılan formun paneli doldurmasını sağladık.
            frm5.BringToFront(); // formu panel içinde en öne getirdik
        }
        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit(); //program durdurma komutu
        }
        private void button5_Click(object sender, EventArgs e)
        {

            Form8 frm8 = new Form8();
            frm8.Show();
            this.Close();
            frm8.panel1.Controls.Clear(); // Panel'in içini temizliyoruz
            Form6 frm6 = new Form6();

            frm6.TopLevel = false;
            frm8.panel1.Controls.Add(frm6); // panel1 içerisinde formu ekledik

            frm6.Show(); // formu gösterdik.
            frm6.Dock = DockStyle.Fill; // Açılan formun paneli doldurmasını sağladık.
            frm6.BringToFront(); // formu panel içinde en öne getirdik
        }
        private void button4_Click(object sender, EventArgs e)
        {

            Form8 frm8 = new Form8();
            frm8.Show();
            this.Close();
            frm8.panel1.Controls.Clear(); // Panel'in içini temizliyoruz
            Form7 frm7 = new Form7();

            frm7.TopLevel = false;
            frm8.panel1.Controls.Add(frm7); // panel1 içerisinde formu ekledik

            frm7.Show(); // formu gösterdik. 
            frm7.Dock = DockStyle.Fill; // Açılan formun paneli doldurmasını sağladık.
            frm7.BringToFront(); // formu panel içinde en öne getirdik
        }

       
    }
}