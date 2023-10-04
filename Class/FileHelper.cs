using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserClass
{
    class FileHelper
    {
        #region [Method : 파일 존재 여부 확인]
        public bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }
        #endregion

        #region [Method : 파일 삭제]
        public bool DeleteFile(string filePath)
        {
            try
            {
                File.Delete(filePath);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"파일 삭제 중 오류 발생 : {ex.ToString()}");
                return false;
            }
        }
        #endregion

        #region [Method : 다이얼로그를 통해 이미지 파일 경로 가져오기]
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
        #endregion

        #region [Method Group : JSON]

        #region [Method : Json 파일에서 데이터 읽기]
        public static T ReadJsonFile<T>(string filePath)
        {
            try
            {
                string jsonText = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<T>(jsonText);
            }
            catch (Exception ex)
            {
                MessageBox.Show("JSON 파일 읽는 중 오류 발생 : " + ex.ToString());
                return default(T);
            }
        }
        #endregion

        #region [Method : 데이터를 Json 파일에 쓰기]
        public bool WriteJsonFile<T>(string filePath, T data)
        {
            try
            {
                string jsonText = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(filePath, jsonText);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"JSON 파일을 쓰는 동안 오류 발생 : {ex.Message}");
                return false;
            }
        }
        #endregion

        #region [Method : 객체를 JSON 문자열로 변환]
        public string SerializeToJson<T>(T data)
        {
            return JsonConvert.SerializeObject(data, Formatting.Indented);
        }
        #endregion

        #region [Method : JSON 문자열을 객체로 변환]
        public T DeserializeFromJson<T>(string jsonText)
        {
            return JsonConvert.DeserializeObject<T>(jsonText);
        }
        #endregion

        #endregion

    }
}
