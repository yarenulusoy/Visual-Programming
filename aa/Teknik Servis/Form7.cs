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
using System.Data.OleDb;

namespace Teknik_Servis
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=HP-YAREN\\SQLEXPRESS; Initial Catalog=TeknikServis; Integrated Security=True;");
        
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
                baglanti.Open();
                SqlDataAdapter da = new SqlDataAdapter("Select MusteriKayit.AdSoyad,Telefon,Email,CihazAdi,CihazMarkasi,CihazModeli,CihazArizasi,CihazDurum,TeslimAlinanTarih,VerilenTarih,Ucret,CihazResmi from CihazTamir,MusteriKayit where MusteriKayit.AdSoyad=CihazTamir.AdSoyad", baglanti); // Musteri kayıt ve cihaz tamir tablosundan AdSoyad aynı olan verileri getir
                DataTable tablo = new DataTable();
                da.Fill(tablo);  //tabloya doldur
                dataGridView1.DataSource = tablo; //data grid vieve getir
                baglanti.Close();

            
        }

        private void button2_Click(object sender, EventArgs e) //seçilen kaydı sil
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("DELETE from MusteriKayit where AdSoyad='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'",baglanti);  // datagrid den seçilen satırı MusteriKayit tablosundan Ad soyada göre sil
            SqlCommand komut1 = new SqlCommand("DELETE from CihazTamir where AdSoyad='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'", baglanti);// datagrid den seçilen satırı CihazTamir tablosundan Ad soyada göre sil
            komut.ExecuteNonQuery();
            komut1.ExecuteNonQuery();
            baglanti.Close();

            if (dataGridView1.SelectedRows.Count > 0)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index); //data grid viewden seçilen satırı sil
          
            }
       

            else
            {
                MessageBox.Show("Lüffen silinecek satırı seçin."); //seçilen satır yoksa
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            Form2 menu = new Form2();
            this.Close();
            menu.Show();
        }

        private void button3_Click(object sender, EventArgs e) //kayıt ara
        {
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select MusteriKayit.AdSoyad,Telefon,Email,CihazAdi,CihazMarkasi,CihazModeli,CihazArizasi,CihazDurum,TeslimAlinanTarih,VerilenTarih,Ucret from CihazTamir,MusteriKayit where MusteriKayit.AdSoyad=CihazTamir.AdSoyad and  CihazAdi like '%" + textBox1.Text + "%'", baglanti); //yazılanı CihazAdi benzerliğinden verileri getir
            SqlDataAdapter da1 = new SqlDataAdapter(" Select MusteriKayit.AdSoyad,Telefon,Email,CihazAdi,CihazMarkasi,CihazModeli,CihazArizasi,CihazDurum,TeslimAlinanTarih,VerilenTarih,Ucret from CihazTamir,MusteriKayit where MusteriKayit.AdSoyad=CihazTamir.AdSoyad and CihazMarkasi like '%" + textBox1.Text + "%'", baglanti); //yazılanı CihazMarkasi benzerliğinden verileri getir
            SqlDataAdapter da2 = new SqlDataAdapter("Select MusteriKayit.AdSoyad,Telefon,Email,CihazAdi,CihazMarkasi,CihazModeli,CihazArizasi,CihazDurum,TeslimAlinanTarih,VerilenTarih,Ucret from CihazTamir,MusteriKayit where MusteriKayit.AdSoyad=CihazTamir.AdSoyad and MusteriKayit.AdSoyad like '%" + textBox1.Text + "%'", baglanti);//yazılanı AdSoyad benzerliğinden verileri getir
            SqlDataAdapter da3 = new SqlDataAdapter(" Select MusteriKayit.AdSoyad,Telefon,Email,CihazAdi,CihazMarkasi,CihazModeli,CihazArizasi,CihazDurum,TeslimAlinanTarih,VerilenTarih,Ucret from CihazTamir,MusteriKayit where MusteriKayit.AdSoyad=CihazTamir.AdSoyad and MusteriKayit.Telefon like '%" + textBox1.Text + "%'", baglanti);//yazılanı Telefon benzerliğinden verileri getir

            SqlDataAdapter da5 = new SqlDataAdapter(" Select MusteriKayit.AdSoyad,Telefon,Email,CihazAdi,CihazMarkasi,CihazModeli,CihazArizasi,CihazDurum,TeslimAlinanTarih,VerilenTarih,Ucret from CihazTamir,MusteriKayit where MusteriKayit.AdSoyad=CihazTamir.AdSoyad and CihazDurum like '%" + textBox1.Text + "%'", baglanti);//yazılanı CihazDurum benzerliğinden verileri getir
            SqlDataAdapter da6 = new SqlDataAdapter("Select MusteriKayit.AdSoyad,Telefon,Email,CihazAdi,CihazMarkasi,CihazModeli,CihazArizasi,CihazDurum,TeslimAlinanTarih,VerilenTarih,Ucret from CihazTamir,MusteriKayit where MusteriKayit.AdSoyad=CihazTamir.AdSoyad and CihazArizasi like '%" + textBox1.Text + "%'", baglanti);//yazılanı CihazArizasi benzerliğinden verileri getir
            SqlDataAdapter da7 = new SqlDataAdapter(" Select MusteriKayit.AdSoyad,Telefon,Email,CihazAdi,CihazMarkasi,CihazModeli,CihazArizasi,CihazDurum,TeslimAlinanTarih,VerilenTarih,Ucret from CihazTamir,MusteriKayit where MusteriKayit.AdSoyad=CihazTamir.AdSoyad and Ucret like '%" + textBox1.Text + "%'", baglanti);//yazılanı Ucret benzerliğinden verileri getir

            DataTable tablo = new DataTable();
            da.Fill(tablo);
            da1.Fill(tablo);
            da2.Fill(tablo);
            da3.Fill(tablo);
            da5.Fill(tablo);
            da6.Fill(tablo);
            da7.Fill(tablo);
           

            dataGridView1.DataSource = tablo;
            baglanti.Close();

        }


        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel2.Controls.Clear();
            Form4 frm4 = new Form4();

            frm4.TopLevel = false;
            panel2.Controls.Add(frm4); // panel1 içerisinde formu ekledik

            frm4.Show(); // formu gösterdik. Ancak buraya dikakt. ShowDialog(); olarak değil Show(); olarak açıyoruz.
            frm4.Dock = DockStyle.Fill; // Açılan formun paneli doldurmasını sağladık.
            frm4.BringToFront(); // formu panel içinde en öne getirdik

        }

       
    }
}







//private void kayitGetir()
//{

//    baglanti.Open();

//    string kayit =/* "/*SELECT * from MusteriBİlgi*/ "Select  Adi,Soyadi,Telefon,Email,CihazAdi,CihazMarkasi,CihazModeli,TeslimTarihi,VerilenTarih,CihazArizasi,CihazSonDurumu,Ucret From MusteriBİlgi Inner Join CİhazEkle on ( MusteriBİlgi.MusteriId=CİhazEkle.MusteriId ) ";
//    musteriler tablosundaki tüm kayıtları çekecek olan sql sorgusu.
//    SqlCommand komut = new SqlCommand(kayit, baglanti);
//    Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
//    SqlDataAdapter da = new SqlDataAdapter(komut);
//    SqlDataAdapter sınıfı verilerin databaseden aktarılması işlemini gerçekleştirir.
//    DataTable dt = new DataTable();
//    da.Fill(dt);
//    Bir DataTable oluşturarak DataAdapter ile getirilen verileri tablo içerisine dolduruyoruz.
//    dataGridView1.DataSource = dt;
//    Formumuzdaki DataGridViewin veri kaynağını oluşturduğumuz tablo olarak gösteriyoruz.
//    baglanti.Close();
//}


//private void button3_Click(object sender, EventArgs e)
//{
//     string aranan = textBox1.Text.Trim().ToUpper();
//    for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
//    {
//        foreach (DataGridViewRow row in dataGridView1.Rows)
//        {
//            foreach (DataGridViewCell cell in dataGridView1.Rows[i].Cells)
//            {
//                if (cell.Value != null)
//                {
//                    if (cell.Value.ToString().ToUpper() == aranan)
//                    {
//                        cell.Style.BackColor = Color.DarkTurquoise;
//                        break;
//                    }
//                }

//            }
//        }
//    }
//}

