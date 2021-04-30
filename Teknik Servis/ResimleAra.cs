using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace Teknik_Servis
{
    public class ResimleAra
    {
        private ResimleAramaAlgoritma _imageSearchAlgorithm; //Resimle arama algoritma sınıfını kullanmak için imagesearchalgorithm tanımladık

        public ResimleAra(ResimleAramaAlgoritma imageSearchAlgorithm)
        {
            _imageSearchAlgorithm = imageSearchAlgorithm;
        }
        public Tuple<int, double> GetMostSimilarImage(Bitmap searchImage, string[] imageFileArray, List<Color> centroidColorList)
        {
            int mostSimilarImageIndex = 0;
            double maxSimilarity = 0;

            for (int i = 0; i < imageFileArray.Length; i++)
            {
                string image = imageFileArray[i];
                using (Bitmap bmpImage = new Bitmap(Image.FromFile(image), searchImage.Width, searchImage.Height))
                {
                    using (Bitmap bmpPorcessedImage = _imageSearchAlgorithm.ProcessImage(bmpImage, centroidColorList))
                    {
                        double similarityPercent = CalculateSimilarity(searchImage, bmpPorcessedImage);
                        if (similarityPercent > maxSimilarity)
                        {
                            mostSimilarImageIndex = i;
                            maxSimilarity = similarityPercent;
                        }
                    }
                }

            }
            return new Tuple<int, double>(mostSimilarImageIndex, maxSimilarity);
        }

        public List<KeyValuePair<string, double>> SortBySimilarity(Bitmap searchImage, string[] imageFileArray, List<Color> centroidColorList)
        {
            Dictionary<string, double> similarityDic = new Dictionary<string, double>(); //Dictionary koleksiyonunda Anahtar(Key) ve Değer(Value) 

            for (int i = 0; i < imageFileArray.Length; i++)
            {
                string image = imageFileArray[i];
                using (Bitmap bmpImage = new Bitmap(Image.FromFile(image), searchImage.Width, searchImage.Height))
                {
                    using (Bitmap bmpPorcessedImage = _imageSearchAlgorithm.ProcessImage(bmpImage, centroidColorList))
                    {
                        double similarityPercent = CalculateSimilarity(searchImage, bmpPorcessedImage);
                        similarityDic.Add(image, similarityPercent);
                    }
                }

            }
            return similarityDic.OrderByDescending(x => x.Value).ToList();
        }
        public double CalculateSimilarity(Bitmap bmpImage1, Bitmap bmpImage2)
        {
            //her iki resim de işlenir. Ve işlenen her görüntü başlangıçta belirlediğimiz bölge renkleriyle renklendirilir.
            int correct = 0; 
            for (int i = 0; i < bmpImage1.Width; i++)
            {
                for (int j = 0; j < bmpImage1.Height; j++)
                {
                    Color c1 = bmpImage1.GetPixel(i, j);
                    Color c2 = bmpImage2.GetPixel(i, j);
                    if (c1.ToArgb() == c2.ToArgb())
                        correct++;
                }
            }
            int maxPixels = bmpImage1.Width * bmpImage1.Height;
            double SimilarityPercent = (100.0 * correct) / maxPixels;
            return SimilarityPercent;
        }
    }
}


