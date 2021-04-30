using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMeansProject; 
using System.Drawing;

namespace Teknik_Servis
{
    public class ResimleAramaAlgoritma
    {
        //K - Kümeleme algoritması denetlenmeyen bir algoritmadır ve ilgi alanını arka plandan ayırmak için kullanılır.
        //Verilen verileri K kümeleri veya K-centroid'leri temel alan bölümler halinde kümeler veya bölümler.
        //benzer RGB vektör noktalarını sınıflandırmak ve benzer pikseller birlikte gruplandırılacaktır.
        private KMeans _kmeans; //kmeans kütüphanesinde _kmeans oluşturduk
        private List<double[]> _dataset; 

        public ResimleAramaAlgoritma()
        {
            _dataset = new List<double[]>(); //dataset adında liste oluşturduk
        }

        public void RunAlgorithm(Bitmap searchImage, int k) //RunAlgorithm fonksiyonunun içinde searchimage adında bitmap ve k adında bir değişken tanımladık
        {
            for (int i = 0; i < searchImage.Height; i++) //resmin uzunluguna kadar aldık
            {
                for (int j = 0; j < searchImage.Width; j++) //resmin genişliğine kadar aldık
                {
                   // K - Means için kaç tane centroid kullanmak istediğimizi seçtik
                     Color c = searchImage.GetPixel(i, j); //uzunluk ve genişliklerden aldıgımız pikselleri c coloruna atadık
                    double[] pixelArray = new double[] { c.R, c.G, c.B }; // RGB renklerin pixellerini pixelarray dizisine atadık
                    _dataset.Add(pixelArray); //oluşturdugumuz listeye  bu pixelleri ekledik
                }
            }
            //K-Means her bir vektörü seçilen iki merkezden birine sınıflandırmaya çalışacaktır.Böylece benzer pixeller aynı gruba ait olur
           _kmeans = new KMeans(k, new EuclideanDistance());  //iki nokta rasındaki doğrusal uzaklık hesaplaması
            _kmeans.Run(_dataset.ToArray()); //pixelleri kmeanse ekle
        }

        public Bitmap ProcessImage(Bitmap image, List<Color> cenotridColorList)
        {
            Bitmap resultImage = new Bitmap(image.Width, image.Height); //resultimage bitmapinin uzunluk ve genişliğini tanımladık

            for (int i = 0; i < resultImage.Height; i++) //yüksekliğine kadar aldık
            {
                for (int j = 0; j < resultImage.Width; j++) //genişliğine kadar aldık
                {
                    Color c = image.GetPixel(i, j); ////image in pixellerini c ye atadık
                    double[] pixelArray = new double[] { c.R, c.G, c.B }; // bu pixelleri rneklerine ayırdık
                    int resultCentroid = _kmeans.Classify(pixelArray); 
                    Color centroidColor = cenotridColorList[resultCentroid]; 
                    resultImage.SetPixel(i, j, centroidColor);
                }
            }

            return resultImage;
        }
    }
}



