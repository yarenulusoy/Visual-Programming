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

namespace Teknik_Servis
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=HP-YAREN\\SQLEXPRESS; Initial Catalog=TeknikServis; Integrated Security=True;");    //sql baglantı cümleciği

        #region temizle
        private void temizle()
        {
            foreach (Control nesne in this.Controls)  //tümt exboxların sayısı kadar temizle
            {
                if (nesne is TextBox)  
                {
                    TextBox textbox = (TextBox)nesne;
                    textbox.Clear();
                }
            }
        }
        #endregion


        private void button1_Click(object sender, EventArgs e)
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open(); // sql bağlantısı yapmamız için bağlantımızı açtık

            SqlCommand komut = new SqlCommand("select *from Login where KullaniciAdi= '" + textBox1.Text + "' and Sifre='" + textBox2.Text + "'", baglanti); //login tablosundaki kayıtları getirecek olan sql sorgularımızı içine yazdığımız komut  // login tablosundaki kullanıcı adı textbox1 de,şifre de textbox2 deki veriye eşitse 
            SqlDataReader dr = komut.ExecuteReader(); // sorgumuzu satır satır okuma işlemi yapar
            if (dr.Read()) //verilerimiz sorgudaki şartları sağlıyorsa form2 ye geçiş yapar
            {
                Form2 menu = new Form2();
                menu.Show();
                this.Hide();
                
            }
            else //şartları sağlamıyorsa programımız uyarı verir
            {
                MessageBox.Show("HATALI GİRİŞ");
            }
            temizle();// textbox temizleme fonksiyonu
            baglanti.Close(); //baglantı kapatılır

        }
        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = true; //kayıt ol butonuna bastıgımızda kayıt olmak için panel1 açılır
        }
        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Visible = false; //kayıt ol sayfasından geriye dönmek için panel görünmez yapılır
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open(); //baglantımızı açtık
            SqlCommand komut = new SqlCommand("select *from Login where KullaniciAdi= '"+textBox3.Text +"'",baglanti); //textbox3 de yazan login tablosundaki kullanıcı adına eşitse getir
            SqlDataReader dr = komut.ExecuteReader(); //sorgumuzu okuma

            if (dr.Read()) //sorgumuzdaki sartları sağlıyorsa
            {
                MessageBox.Show("AYNI İSİMDE KULLANICI VAR");
                temizle();
            }
            else if (textBox4.Text == textBox5.Text) 
            {
                dr.Close(); //diğer sorgumuzu kapattık
                SqlCommand komut1 = new SqlCommand("insert into Login(KullaniciAdi,Sifre) values('" +textBox3.Text+"','" + textBox4.Text+"')",baglanti); //eğer textbox 4 ve textbox 5 birbirine eşitse veri tabanımıza veri yani bir kullanıcı ekleyeceğiz
                komut1.ExecuteNonQuery(); //ekledik
                MessageBox.Show("YENİ KULLANICI EKLENDİ...");
                baglanti.Close();
                panel1.Visible = false; //ekledikten sonra kullanıcı kayıt panelimiz kapanır
            }
            else
            {
                MessageBox.Show("ŞİFRELER AYNI DEĞİL"); // textbox4 ve textbox5 birbirine eşit olmadıgı durumda hata verir
            }
            baglanti.Close(); //ilk baglantımız da kaaptılır

        }
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close(); //sayfayı kapatma
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
    }



