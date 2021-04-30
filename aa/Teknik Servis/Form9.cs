using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge; //Kamera kütüphanesi
using AForge.Video;
using AForge.Video.DirectShow;
using System.IO;
using System.Data.SqlClient;
using System.Drawing.Printing;


namespace Teknik_Servis
{
    public partial class Form9 : Form
    {

        //Kümeleme algoritması denetlenmeyen bir algoritmadır ve ilgi alanını arka plandan ayırmak için kullanılır.
        //Verilen verileri K kümeleri veya K-centroid'leri temel alan bölümler halinde kümeler veya bölümler.
        //benzer RGB vektör noktalarını sınıflandırmak ve benzer pikseller birlikte gruplandırılacaktır.
        public DataSet doldur(string sorgu)
        {
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            baglanti.Close();
            return ds;

        }
      
        SqlConnection baglanti = new SqlConnection("Data Source=HP-YAREN\\SQLEXPRESS; Initial Catalog=TeknikServis; Integrated Security=True;"); //baglantı cumleciği
        private Bitmap bmpSearchImage; 
        private Bitmap bmpSearchImageProcessed;
        private List<Color> _centroidColor;
        private string[] _fileArray;

        private ResimleAramaAlgoritma _algorithm; //classları tanımladık
        private ResimleAra _searchImage;

        private List<KeyValuePair<string, double>> similarityList; //similarylist adında bir liste oluşturuldu//keyvalue bir değeri tutmak için kullanılır. //<listedeki nesnelerin türü

        public Form9()
        {
            InitializeComponent();
            
            _centroidColor = new List<Color>(); //renkleri _centroidcolora atadık
            _algorithm = new ResimleAramaAlgoritma(); //ResimleAramaAlgoritma sınıfını _algorithm e atadık
        }
        private FilterInfoCollection CaptureDevices; // bilgisayara kaç kamera bağlıysa onları tutan bir dizi
        private VideoCaptureDevice videoSource; //bizim kullanacagımız aygıt


        private void Form9_Load(object sender, EventArgs e)
        {
            
            CaptureDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice); //Capturedevices dizisine tüm kameralarımızı dolduruyoruz (webcam,telefon kamerası,ip kamera)
            foreach (FilterInfo Device in CaptureDevices)
            {
                comboBox1.Items.Add(Device.Name); //kameraları comboboxda listeliyoruz
            }
            comboBox1.SelectedIndex = 0; //telefonumun kamerası ilk seçenek oldugundan 0.indeksi seçili hale getiriyorum
            videoSource = new VideoCaptureDevice();
            videoSource = new VideoCaptureDevice(CaptureDevices[comboBox1.SelectedIndex].MonikerString); //capturedevices değişkenimize comboboxda seçili olan kamerayı atıyoruz ve kameramız bu değişkende oluştu
            videoSource.NewFrame += new NewFrameEventHandler(VideoSource_NewFrame);
            videoSource.Start(); //kamerayı başlatıyoruz

            _fileArray = System.IO.Directory.GetFiles("C:\\Users\\yaren\\Desktop\\Photo", "*.jpg"); //resimleri seçmek için photos klosörümü dosya yolu olarak belirledim.bu klasördeki resimleri listeleyecektir.
            for (int j = 0; j < _fileArray.Length; j++)
            {
                fileList.Items.Add(_fileArray[j]); //klasörümdeki kayıtlı olan resimleri teker teker listeliyor
            }
            
            
        }
        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone(); //kameradan alınan görüntüyü picturebox a atıyoruz.

        }



        private void resimyukle_Click(object sender, EventArgs e)
        {
            //bugünün tarihi
            DateTime dt = DateTime.Today;
            int yil = dt.Year;
            int ay = dt.Month;
            int gun = dt.Day;
            labeltarih.Text = gun.ToString() + "/" + ay.ToString() + "/" + yil.ToString(); //

            pictureBox2.Image = pictureBox1.Image; //picturebox2 den görüntü al yani resmini çek
            SaveFileDialog swf = new SaveFileDialog(); //kaydet
            Bitmap bmpKucuk = new Bitmap(pictureBox2.Image, 300, 300); // Yeniden boyutlandırmak için //Bitmap sınıfı kullanılır Picturebox da yüklü olan resim 100 e 50 boyutunda yeniden  boyutlandırılıyor. Kümeleme yönteminini kullanılması için 
            pictureBox2.Image = bmpKucuk; //picture2 nin boyutlarını değiştir
            pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox3.Image = pictureBox2.Image; //pictruebox3 e picturebox2 yi ata
            bmpSearchImage = new Bitmap(pictureBox3.Image); // picturebox3 bitmap oluştur
            swf.Filter = "(*.jpg)|*.jpg|Bitma*p(*.bmp)|*.bmp"; //fotograf cinsini ayarla
 
            _centroidColor.Add(Color.Green); //renk değişkenime yeşil rengini ekledim
            _centroidColor.Add(Color.Red); // kırmızı rengi ekledim
            _centroidColor.Add(Color.Blue); // mavi rengi ekledim
            txtCentroids.Text = _centroidColor.Count.ToString(); // 3 rengimiz oluştu yani RGB renkleri kümeleme yöntemini kullanmak için

            //process
            _algorithm.RunAlgorithm(bmpSearchImage, _centroidColor.Count); 

            bmpSearchImageProcessed = _algorithm.ProcessImage(bmpSearchImage, _centroidColor);
            /*RGB renklerini  kümeleme işlemi yaparak görüntümüzü bölümlere ayırır ve benzer piksellerin aynı renk olması durumunu sağlar.*/


            //seçilen klasördeki tüm görüntüler için ayırdıgımız pixellere göre görüntü benzerliğini hesaplar. Sonuçta listeyi  benzerliğe göre sıralar.Bizde en çok yüzdeye sahip olan fotografı getiririz.
              _searchImage = new ResimleAra(_algorithm);
            similarityList = _searchImage.SortBySimilarity(bmpSearchImageProcessed, _fileArray, _centroidColor); //
            fileList.Items.Clear();
            List<string> tempList = new List<string>();  //templist listesi oluşturur
            foreach (var imagePath in similarityList)
                tempList.Add(imagePath.Key);
            _fileArray = tempList.ToArray();
            foreach (var imagePath in _fileArray)
                fileList.Items.Add(System.IO.Path.GetFileNameWithoutExtension(imagePath));
            fileList.SelectedIndex = 0; //en çok benzerlik olan fotografı seçer


            textBox1.Text = Convert.ToString("C:\\Users\\yaren\\Desktop\\Photo\\" + fileList.SelectedItem + ".jpg"); // texboxa çekilen resimden buldugu resmi yani urlsini yazdır
            pictureBox9.Image = Image.FromFile(textBox1.Text); //picturebox9un resim yoluna textbox1 i ata



            string sorgu = "select AdSoyad from MusteriKayit where  CihazResmi = '" + textBox1.Text + "'"; //textboxda yazan resmin verilerini getir
            DataSet ds = doldur(sorgu);
            sorgu = "select AdSoyad,Telefon,Email,CihazAdi,CihazMarkasi,CihazModeli,CihazArizasi,TeslimAlinanTarih from MusteriKayit where AdSoyad='" + ds.Tables[0].Rows[0][0] + "'"; 
            ds.Clear();
            ds = doldur(sorgu);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            { 
                // çektiğimiz verileri labela yazdırma

                labelad.Text = (ds.Tables[0].Rows[i][0].ToString());
                labeltelefon.Text = (ds.Tables[0].Rows[i][1].ToString());
                comboBox2.Items.Add(labelad.Text); 
                comboBox2.SelectedIndex = 0;
                labelemail.Text = (ds.Tables[0].Rows[i][2].ToString());
                labelcihazadi.Text = (ds.Tables[0].Rows[i][3].ToString());
                labelcihazmarkasi.Text = (ds.Tables[0].Rows[i][4].ToString());
                labelcihazmodeli.Text = (ds.Tables[0].Rows[i][5].ToString());
                labelalinantarih.Text = (ds.Tables[0].Rows[i][7].ToString());
                labelcihazarizasi.Text = (ds.Tables[0].Rows[i][6].ToString());

            }
            panel1.Visible = true;
            pictureBox1.Visible = false;
            button3.Visible = true;

        }


        private void fileList_SelectedIndexChanged(object sender, EventArgs e)
        {//fotografları listele

            picResultImage.Image = Image.FromFile(_fileArray[fileList.SelectedIndex]);  
            //listede en çok benzerlik bulunan fotografı picresultimage getir
            if (similarityList == null) return;

            var similarityItem = similarityList.FirstOrDefault(x => x.Key == _fileArray[fileList.SelectedIndex]); 
           
        }

       
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sorgu = "select AdSoyad from CihazTamir where  AdSoyad = '" + labelad.Text + "'";  //labeldaki isme eşit olan ismi CihazTamir tablosundan getir
            DataSet ds = doldur(sorgu);
            sorgu = "select CihazDurum,VerilenTarih,Ucret from CihazTamir where AdSoyad='" + ds.Tables[0].Rows[0][0] + "'"; // labeldaki isme eşit olan ismin verilerini CihazTamir tablosudnan getir
            ds.Clear();
            ds = doldur(sorgu);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            { 
                //gelen verileri labela yazdır
                labelcihazdurumu.Text = (ds.Tables[0].Rows[i][0].ToString());
                labelverilentarih.Text = (ds.Tables[0].Rows[i][1].ToString());
                labelucret.Text = (ds.Tables[0].Rows[i][2].ToString());
            }
        }

     
      

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 menu = new Form2();
            menu.Show();
            this.Hide();
        }

       

        private void button3_Click_1(object sender, EventArgs e)
        {

            PrintDocument doc = new PrintDocument();// PrintDocument nesnemizin tanimlamasi
            doc.PrintPage += this.Doc_PrintPage;// Print event'i yaratiliyor.
            PrintDialog dlgSettings = new PrintDialog(); //yazdırma penceresi açılır
            dlgSettings.Document = doc;
            if (dlgSettings.ShowDialog() == DialogResult.OK)
            {
                doc.Print();
            }
        }


        private void Doc_PrintPage(object sender, PrintPageEventArgs e)
        { //yazdırma alanını çizer
            float x = e.MarginBounds.Left;
            float y = e.MarginBounds.Top;
            Bitmap bmp = new Bitmap(this.panel1.Width, this.panel1.Height);
            //Bu kısımda panel1 yazısını ve çizgileri yazdırıyorum
            this.panel1.DrawToBitmap(bmp, new Rectangle(0, 0, this.panel1.Width, this.panel1.Height));
            e.Graphics.DrawImage((Image)bmp, x, y); //imagedeki grafigi çiz
        }
    }

    }
    

