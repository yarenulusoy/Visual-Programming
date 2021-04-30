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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        public static string isim, telefon,resim, mail, chzadi, chzmarka, chzmodel, chzariza,chzdurum, atarih, vtarih,ucret;

        private void button4_Click_1(object sender, EventArgs e)
        {
            Form2 menu = new Form2();
            menu.Show();
            this.Hide();
        }

        public static string baglanticumle = "Data Source=HP-YAREN\\SQLEXPRESS; Initial Catalog = TeknikServis; Integrated Security = True;";
        SqlConnection baglanti = new SqlConnection(baglanticumle);

        public DataSet doldur(string sorgu)
        {
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            baglanti.Close();
            return ds;
           
        }
       

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            label16.Text = dateTimePicker1.Text; //seçilen tarihi labela yazdır
        }

       

        void eklee()
        {

            try
            {
                if (baglanti.State == ConnectionState.Closed)

                    baglanti.Open();

                //Girilen verileri CihazTamir tablosuna ekle
              
                SqlCommand komut = new SqlCommand("insert into CihazTamir(AdSoyad,CihazDurum,VerilenTarih,Ucret) values(@AdSoyad,@CihazDurum,@VerilenTarih,@Ucret) ", baglanti);
                komut.Parameters.AddWithValue("@AdSoyad", comboBox1.Text);
                komut.Parameters.AddWithValue("@CihazDurum", comboBox6.Text);
                komut.Parameters.AddWithValue("@VerilenTarih",label16.Text);
                komut.Parameters.AddWithValue("@Ucret", comboBox7.Text);

                SqlDataReader dr = komut.ExecuteReader();

                baglanti.Close();
                MessageBox.Show("Kayıt İşlemi Gerçekleşti...");
            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }

        }
       


        private void button1_Click(object sender, EventArgs e)
        {
            eklee();

        }
     
        private void button4_Click(object sender, EventArgs e)
        {
            Form2 menu = new Form2();
            this.Close();
            menu.Show();
        }


        private void Form5_Load_1(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select AdSoyad from MusteriKayit", baglanti); //MusteriKayıt tablosundaki adsoyadları getir
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {

                comboBox1.Items.Add(dr["AdSoyad"]); //combobox1e yazdır


            }
            baglanti.Close();


            baglanti.Open();
            SqlCommand komut1 = new SqlCommand("Select Fiyat from CihazFiyat", baglanti); //CihazFiyat tablosundaki Fiyatları getir
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {

                comboBox7.Items.Add(dr1["Fiyat"]); //combobox7 ye yazdır


            }
            baglanti.Close();
            
            baglanti.Close();
            
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string sorgu = "select  AdSoyad from MusteriKayit where AdSoyad ='" + comboBox1.Text + "'"; //combobox1den seçilen ismi MusteriKayit tablosundan getir
            DataSet ds = doldur(sorgu);
            sorgu = "select CihazAdi,CihazMarkasi,CihazModeli,TeslimAlinanTarih,CihazArizasi,Telefon,Email,CihazResmi from MusteriKayit where AdSoyad='" + ds.Tables[0].Rows[0][0] + "'";  //comboboxdan seçilen ad soyadın bilgilerini getir
            ds.Clear();
            ds = doldur(sorgu);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                //bu bilgileri labellara sırayla yazdır
                label11.Text = (ds.Tables[0].Rows[i][0].ToString());
                label12.Text = (ds.Tables[0].Rows[i][1].ToString());
                label13.Text = (ds.Tables[0].Rows[i][2].ToString());
                label14.Text = (ds.Tables[0].Rows[i][3].ToString());
                label15.Text = (ds.Tables[0].Rows[i][4].ToString());
                labeltelefon.Text = (ds.Tables[0].Rows[i][5].ToString());
                labelemail.Text = (ds.Tables[0].Rows[i][6].ToString());
                textBox1.Text = (ds.Tables[0].Rows[i][7].ToString());
            }



            baglanti.Open();

            if (label11.Text == "Bilgisayar") //Labelda yazan Bilgisayar ise
            {
                SqlCommand komut2 = new SqlCommand("Select CihazDurumu from CihazDurum where IdCihazAdi=1", baglanti); //Id si 1 olanların cihazdurumu nu getir
                SqlDataReader dr2 = komut2.ExecuteReader();
                while (dr2.Read())
                {

                    comboBox6.Items.Add(dr2["CihazDurumu"]); //comboboxa CihazDurumu sutununu yazdır


                }
            }
            else if (label11.Text == "Telefon") //Labelda yazan Telefon ise
            {
                SqlCommand komut = new SqlCommand("Select CihazDurumu from CihazDurum where IdCihazAdi=2", baglanti); //Id si 2 olanların cihazdurumu nu getir
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {

                    comboBox6.Items.Add(dr["CihazDurumu"]); //comboboxa CihazDurumu sutununu yazdır


                }
            }
            else if(label11.Text=="Yazıcı") // Labelda yazan Yazıcı ise
            {
                SqlCommand komut3 = new SqlCommand("Select CihazDurumu from CihazDurum where IdCihazAdi=3", baglanti);//Id si 3 olanların cihazdurumu nu getir
                SqlDataReader dr3 = komut3.ExecuteReader();
                while (dr3.Read())
                {

                    comboBox6.Items.Add(dr3["CihazDurumu"]); //comboboxa CihazDurumu sutununu yazdır


                }
            }
            baglanti.Close();
        }

        
    }
}

      
 


        

        

     
            
   



    
        


       


    


   
