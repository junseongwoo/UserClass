using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserClass
{
    class ImageHelper
    {
        private Stack<Bitmap> bitmapStack = new Stack<Bitmap>();

        /// <summary>
        /// 이미지 로드
        /// </summary>
        /// <param name="imagePath">이미지 경로</param>
        /// <returns>Load Image</returns>
        public Bitmap LoadImage(string imagePath)
        {
            Bitmap newImage = new Bitmap(Image.FromFile(imagePath));
            bitmapStack.Clear();
            bitmapStack.Push(newImage);

            return newImage;
        }

        /// <summary>
        /// 이전 이미지를 가져온다.
        /// </summary>
        /// <returns>Previous Image</returns>
        public Bitmap PrevImage()
        {
            if (bitmapStack.Count > 1)
            {
                bitmapStack.Pop();
                Bitmap prevImage = bitmapStack.Peek();

                return prevImage;
            }
            return bitmapStack.Peek();
        }

        /// <summary>
        /// LockBit 하지 않고 Bitmap Load 
        /// </summary>
        /// <param name="filePath">파일 경로</param>
        /// <returns></returns>
        public Bitmap LoadBitmapUnlocked(string filePath)
        {
            using (Bitmap bitmap = new Bitmap(filePath))
            {
                return new Bitmap(bitmap);
            }
        }

        /// <summary>
        /// 이미지 저장하기 
        /// </summary>
        /// <param name="image">이미지</param>
        /// <param name="filePath">파일 경로</param>
        public void SaveImage(Image image, string filePath)
        {
            string fileExtension = Path.GetExtension(filePath);

            switch (fileExtension.ToLower())
            {
                case ".bmp":
                    image.Save(filePath, ImageFormat.Bmp);
                    break;
                case ".jpeg":
                case ".jpg":
                    image.Save(filePath, ImageFormat.Jpeg);
                    break;
                case ".png":
                    image.Save(filePath, ImageFormat.Png);
                    break;
                default:
                    throw new NotSupportedException("Unkown fie Extension" + fileExtension);
            }
        }

        /// <summary>
        /// 이미지 Histogram 계산
        /// </summary>
        /// <param name="image">계산할 이미지</param>
        /// <returns>계산된 histogram</returns>
        public int[] CalculateHistogram(Bitmap image)
        {
            int[] histogram = new int[256];

            // 대용량 이미지에 적합하지 않음 
            //for (int x = 0; x < image.Width; x++)
            //{
            //    for (int y = 0; y < image.Height; y++)
            //    {
            //        Color pixel = image.GetPixel(x, y);
            //        int grayValue = (int)(pixel.R * 0.3 + pixel.G * 0.59 + pixel.B * 0.11);
            //        histogram[grayValue]++; // 히스토그램 카운터 증가
            //    }
            //}

            // 이미지 잠금
            BitmapData imageData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            IntPtr ptr = imageData.Scan0;

            // 이미지 데이터의 바이트 배열 크기 계산
            int bytes = Math.Abs(imageData.Stride) * image.Height;
            byte[] rgbValues = new byte[bytes];

            // 이미지 데이터 복사
            Marshal.Copy(ptr, rgbValues, 0, bytes);

            // 이미지 잠금 해제
            image.UnlockBits(imageData);

            // 병렬 처리를 사용하여 픽셀 처리
            Parallel.ForEach(rgbValues, pixel =>
            {
                int grayValue = (int)(0.3 * (pixel & 0xFF) + 0.59 * ((pixel >> 8) & 0xFF) + 0.11 * ((pixel >> 16) & 0xFF));
                lock (histogram)
                {
                    histogram[grayValue]++;
                }
            });

            return histogram;
        }

        /// <summary>
        /// Histogram 그리기 
        /// </summary>
        /// <param name="histogram">계산한 histogram</param>
        /// <returns>Histogram Image</returns>
        public Bitmap DrawHistogram(int[] histogram)
        {
            // 히스토그램 넓이, 높이
            int histWidth = 256;
            int histHeight = 256;

            Bitmap histogramBitmap = new Bitmap(histWidth, histHeight);

            using (Graphics g = Graphics.FromImage(histogramBitmap))
            {
                g.Clear(Color.White); // 흰 배경으로 초기화 

                int maxCount = histogram.Max();

                for (int i = 0; i < histWidth; i++)
                {
                    int barHeight = (int)(double)histogram[i] / maxCount * histWidth;
                    g.DrawLine(Pens.Black, i, histWidth, 1, histHeight - barHeight);
                }
                    
            }
            return histogramBitmap;
        }
    }
}
