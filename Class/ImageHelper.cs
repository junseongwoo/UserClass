using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserClass
{
    class ImageHelper
    {
        private Stack<Bitmap> bitmapStack = new Stack<Bitmap>();
        private Stack<Image> imageStack = new Stack<Image>();

        /// <summary>
        /// 이미지 로드
        /// </summary>
        /// <param name="imagePath">이미지 경로</param>
        /// <returns>Load Image</returns>
        public Image LoadImage(string imagePath)
        {
            Image newImage = Image.FromFile(imagePath);
            imageStack.Clear();
            imageStack.Push(newImage);

            return newImage;
        }

        /// <summary>
        /// 이전 이미지를 가져온다.
        /// </summary>
        /// <returns>Previous Image</returns>
        public Image PrevImage()
        {
            if (imageStack.Count > 1)
            {
                imageStack.Pop();
                Image prevImage = imageStack.Peek();

                return prevImage;
            }
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





    }
}
