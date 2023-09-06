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
        // 

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
