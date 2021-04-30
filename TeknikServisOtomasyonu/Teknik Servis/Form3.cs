using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing.Printing;
namespace Teknik_Servis
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=HP-YAREN\\SQLEXPRESS; Initial Catalog=TeknikServis; Integrated Security=True;");
        //public DataSet doldur(string sorgu)
        //{
        //    baglanti.Open();
        //    SqlDataAdapter da = new SqlDataAdapter(sorgu, baglanti);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds);
        //    baglanti.Close();
        //    return ds;

        //}
   
      

        private void Form3_Load(object sender, EventArgs e)
        {
          
            //labellara form4 den veri çekme işlemi
            labelad.Text = Form4.isim;
            label18.Text = Form4.telefon;
            labelemail.Text = Form4.mail;
            label21.Text = Form4.chzadi;
            label20.Text = Form4.chzmodel;
            label22.Text = Form4.chzmarka;
            label23.Text = Form4.chzariza;
            label25.Text = Form4.atarih;
            pictureBox1.Image = Form4.img;
          
               

            DateTime dt = DateTime.Today; //bugunun  tarihi fonskiyonu
            int yil = dt.Year; //yıl
            int ay = dt.Month;//ay
            int gun = dt.Day;//gün
            labeltarih.Text = gun.ToString() + "/" + ay.ToString() + "/" + yil.ToString(); //gün ay yıl şeklinde yazdırma




        }


        private void button1_Click(object sender, EventArgs e)
        { //geri butonu
            Form2 menu = new Form2();
            this.Close();
            menu.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        { //kapatma butonu
           
            this.Close(); 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PrintDocument doc = new PrintDocument();
            doc.PrintPage += this.Doc_PrintPage;
            PrintDialog dlgSettings = new PrintDialog();
            dlgSettings.Document = doc;
            if (dlgSettings.ShowDialog() == DialogResult.OK)
            {
                doc.Print();
            }
        }


        private void Doc_PrintPage(object sender, PrintPageEventArgs e)
        {
            float x = e.MarginBounds.Left;
            float y = e.MarginBounds.Top;
            Bitmap bmp = new Bitmap(this.panel3.Width, this.panel3.Height);
            this.panel3.DrawToBitmap(bmp, new Rectangle(0, 0, this.panel3.Width, this.panel3.Height));
            e.Graphics.DrawImage((Image)bmp, x, y);
        }

    }
}



