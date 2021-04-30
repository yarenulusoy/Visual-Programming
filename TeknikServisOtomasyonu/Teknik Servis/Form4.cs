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
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Net.Mail;
using System.Net;
namespace Teknik_Servis
{
    public partial class Form4 : Form
    {
       public void email()
        {
            MailMessage mail = new MailMessage(); //mail nesnemizi oluşturduk
            SmtpClient sc = new SmtpClient("smtp.gmail.com");// smtp nesnesi oluşturuyoruz.mail sunucusu adresi
            mail.From = new MailAddress("gorselp@gmail.com", "TEKNİK SERVİS"); //kendi mailimizi ve hangi isimle gönderceğimizi belirledik
            mail.To.Add(textBox3.Text);
            mail.Subject = "BİLGİ"; //konuyu belirledik
            mail.Body = "Sayın "+textBox1.Text+ "," + label13.Text+ " günü cihazınız teslim alınmıştır." ; //mesajımızın ne oldugunu belirledik
            sc.Port = 587;
            sc.Credentials = new NetworkCredential("gorselp@gmail.com", "Gorselprogramlama2"); //mailimiz ve şifremizi yazdık

            sc.EnableSsl = true; //ssl kullanılıyorsa

            sc.Send(mail); //gönderme işlemi

        }
        public Form4()
        {
            InitializeComponent();
        }
        private FilterInfoCollection CaptureDevices;// bilgisayara kaç kamera bağlıysa onları tutan bir dizi
        private VideoCaptureDevice videoSource; //bizim kullanacagımız aygıt

        public DataSet doldur(string sorgu)
        {
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            baglanti.Close();
            return ds;

        }
        public DataSet doldur1(string sorgu1)
        {
            baglanti.Open();
            SqlDataAdapter da1 = new SqlDataAdapter(sorgu1, baglanti);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            baglanti.Close();
            return ds1;

        }
        public static string isim,telefon, mail,chzadi, chzmarka, chzmodel, chzariza, atarih, vtarih; 
        public static Image img;

       
        static string baglanticumle = "Data Source=HP-YAREN\\SQLEXPRESS; Initial Catalog = TeknikServis; Integrated Security = True;";
        SqlConnection baglanti = new SqlConnection(baglanticumle);

        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone(); // kameradan alınan görüntüyü picturebox a atıyoruz.
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            videoSource = new VideoCaptureDevice(CaptureDevices[comboBox5.SelectedIndex].MonikerString); //capturedevices değişkenimize comboboxda seçili olan kamerayı atıyoruz ve kameramız bu değişkende oluştu
            videoSource.NewFrame += new NewFrameEventHandler(VideoSource_NewFrame);
            videoSource.Start(); //kamerayı açıyoruz
            panel1.Visible = true;
            button6.Visible = true;
            

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

            label13.Text = dateTimePicker1.Text; //date time pickerdan seçilen tarihi labela yazdırma
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form5 cihaz = new Form5();
            cihaz.Show();
            this.Hide();
        }

       

        private void button6_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            pictureBox3.Image = pictureBox1.Image; //kamera görüntüsünü picturebox3 e kaydetme
            pictureBox1.Visible = false;

            pictureBox1.Visible = false;
            button6.Visible = false;

            string yeniad = Guid.NewGuid() + ".jpg"; //Benzersiz isim verme
            SaveFileDialog swf = new SaveFileDialog();  //resmi kaydetme
            swf.Filter = "(*.jpg)|*.jpg|Bitma*p(*.bmp)|*.bmp";  //resmin cinsini belirleme
            pictureBox3.Image.Save("C:\\Users\\yaren\\Desktop\\Photo\\" + yeniad);  //bu adrese kaydet
            pictureBox3.ImageLocation = "C:\\Users\\yaren\\Desktop\\Photo\\" + yeniad;  //picturebox3 ün konumunu atama
            textBox2.Text = pictureBox3.ImageLocation;  //veri tabanından çekmek için konumunu yazdırma
        }
   
        private void button1_Click(object sender, EventArgs e)
        {
            email();
            Ekle();

        }

        private void Form4_Load(object sender, EventArgs e)
        {
           
            CaptureDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo Device in CaptureDevices) //Capturedevices dizisine tüm kameralarımızı dolduruyoruz (webcam,telefon kamerası,ip kamera)
            {
                comboBox5.Items.Add(Device.Name); //bütün kameralar comboboxa eklenir
            }
            comboBox5.SelectedIndex = 0; //ilk indeksteki kamera seçilir
            videoSource = new VideoCaptureDevice();



            {
                //comboboxa CihazAd tablosundan cihaz adlarını getir

                string sorgu = "select CihazAdi from CihazAd";
                DataSet ds = doldur(sorgu);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    comboBox1.Items.Add(ds.Tables[0].Rows[i][0].ToString());
                }


            }
            baglanti.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {    //form 3 yani fiş oluşan forma form4 den public olarak tanımladıgımız  verileri göndermek

            panel3.Visible = false;
            pictureBox7.Visible = false;
            Form4.isim = textBox1.Text.ToString();
            Form4.telefon = maskedTextBox1.Text.ToString();
            Form4.mail = textBox3.Text.ToString();
            Form4.chzadi = comboBox1.SelectedItem.ToString();
            Form4.chzmarka = comboBox2.SelectedItem.ToString();
            Form4.chzmodel = comboBox3.SelectedItem.ToString();
            Form4.chzariza = comboBox4.SelectedItem.ToString();
            Form4.atarih =label13.Text.ToString();
            Form4.img = pictureBox3.Image;
           
           panel4.Controls.Clear(); 
             Form3 frm3 = new Form3();

            frm3.TopLevel = false;
            panel4.Controls.Add(frm3); // panel1 içerisinde formu ekledik

            frm3.Show(); // formu gösterdik. 
            frm3.Dock = DockStyle.Fill; // Açılan formun paneli doldurmasını sağladık.
            frm3.BringToFront(); // formu panel içinde en öne getirdik

           








        }

       
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            comboBox3.Items.Clear();
            string sorgu = "select  IdCihazMarkasi from CihazMarka where CihazMarkasi ='" + comboBox2.Text + "'"; // combobox2 den seçilen verinin CihazMArka tablosundan IdCihazMarka değerini getir
            DataSet ds = doldur(sorgu);
            sorgu = "select CihazModeli from CihazModel where IdCihazMarkasi='" + ds.Tables[0].Rows[0][0] + "'"; // CihazModel tablosundan IdCihazMarkasi diğer sorgudan çekilen Id ile eşit olan cihazModelini getir
            ds.Clear();
            ds = doldur(sorgu);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                comboBox3.Items.Add(ds.Tables[0].Rows[i][0].ToString()); //combobox3e yazdır
              
            }


        }
       
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            string sorgu = "select  IdCihazAdi from CihazAd where CihazAdi ='" + comboBox1.Text + "'"; //Combobox1den seçilen verinin cihazad tablosundaki ıd sini getir
            DataSet ds = doldur(sorgu);
            sorgu = "select CihazMarkasi from CihazMarka where IdCihazAdi='" + ds.Tables[0].Rows[0][0] + "'"; // CihazMarka tablosundaki IdCihazAdi Diğer sorgudaki ıd ile eşit olan cihaz markalarını getir
            ds.Clear();
            ds = doldur(sorgu);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                comboBox2.Items.Add(ds.Tables[0].Rows[i][0].ToString()); //combobox2 ye yazdır
            }
            comboBox4.Items.Clear();

            string sorgu1 = "select  IdCihazAdi from CihazAd where CihazAdi ='" + comboBox1.Text + "'"; // combobox1 de yazan cihazadın ID sini getir
            DataSet ds1 = doldur1(sorgu1);
            sorgu1 = "select CihazArizasi from CihazAriza where IdCihazAdi='" + ds1.Tables[0].Rows[0][0] + "'"; // diğer sorgudaki ID ile CihazAriza tablosundaki IdCihazAdi eşit olanların CihazAriza sutununu getir
            ds1.Clear();
            ds1 = doldur1(sorgu1);
            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {
                comboBox4.Items.Add(ds1.Tables[0].Rows[i][0].ToString()); //combobox4 e yazdır
            }


        }
        
        
        void Ekle()
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                    baglanti.Open();


             
                //kayıt ekleme işlemini gerçekleştirecek sorgumuz
                SqlCommand komut = new SqlCommand("insert into MusteriKayit(AdSoyad,Telefon,Email,CihazAdi,CihazMarkasi,CihazModeli,CihazArizasi,TeslimAlinanTarih,CihazResmi) values (@AdSoyad,@Telefon,@Email,@CihazAdi,@CihazMarkasi,@CihazModeli,@CihazArizasi,@AlinanTarih,@CihazResmi)", baglanti); 

                komut.Parameters.AddWithValue("@AdSoyad", textBox1.Text);
                komut.Parameters.AddWithValue("@Telefon", maskedTextBox1.Text);
                komut.Parameters.AddWithValue("@Email", textBox3.Text);
                komut.Parameters.AddWithValue("@CihazAdi", comboBox1.Text);
                komut.Parameters.AddWithValue("@CihazMarkasi", comboBox2.Text);
                komut.Parameters.AddWithValue("@CihazModeli", comboBox3.Text);
                komut.Parameters.AddWithValue("@CihazArizasi", comboBox4.Text);
                komut.Parameters.AddWithValue("@AlinanTarih", label13.Text);
                komut.Parameters.AddWithValue("@CihazResmi",  textBox2.Text);

                //belirtilen yerlere girilen verileri MusteriKayit tablosuna ekle

               SqlDataReader dr = komut.ExecuteReader();


                baglanti.Close();

            MessageBox.Show("Kayıt İşlemi Gerçekleşti..."); //bir sorun yoksa
            }
            catch (Exception hata) //hata yakala ve mesajı ver
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }

        }
      
        private void button4_Click(object sender, EventArgs e)
        { //menuye geri dönme butonu
            Form2 menu = new Form2(); 
            this.Close();
            menu.Show();
        }
       
    }
}