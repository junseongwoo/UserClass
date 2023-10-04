using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserClass
{
    class FileHelper
    {
        public string OpenImageDialog()
        {
            // 파일 다이얼로그 열기
            OpenFileDialog imageDialog = new OpenFileDialog();
            imageDialog.Filter = "이미지 파일 (*.jpg, *.png, *.bmp)|*.jpg;*.png;*.bmp";
            string imagePath = "";

            if (imageDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // 선택 파일 경로 가져오기
                    imagePath = imageDialog.FileName;

                    return imagePath;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Image Open Err " + ex.ToString());
                    return imagePath;
                }
            }
            else
            {
                MessageBox.Show("이미지를 선택하지 않았습니다");
                return imagePath;
            }
        }
    }
}
