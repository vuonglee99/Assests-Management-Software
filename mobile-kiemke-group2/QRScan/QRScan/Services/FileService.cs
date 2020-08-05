using Acr.UserDialogs;
using Newtonsoft.Json;
using QRScan.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QRScan.Services
{
    public interface IFileSaver
    {
        Task<bool> CreateFile(string path);
        Task<string> GetRootPath();
    }

    public class FileService
    {
        public static async void CreateFileData()
        {
            bool alreadyExist = true;
            string rootPath = await DependencyService.Get<IFileSaver>().GetRootPath();
            string appFolder = rootPath + "/QRScanApp";
            string fileName = "KiemkeData.txt";
            string filePath = appFolder + "/" + fileName;

            //TẠO FOLDER NẾU MÁY CHƯA CÓ FOLDER
            if (!Directory.Exists(appFolder))
            {
                alreadyExist = false;
                Directory.CreateDirectory(appFolder);
            }

            //TẠO FILE NẾU MÁY CHƯA CÓ FILE
            FileStream f;
            if (!File.Exists(filePath))
            {
                alreadyExist = false;
                LocalData.KiemKes = new List<KiemKe>();
                f = System.IO.File.Create(filePath);
                f.Close();
            }

            if (!alreadyExist) return;

            //ĐỌC FILE
            System.IO.StreamReader stream = new StreamReader(filePath);
            string datas = stream.ReadToEnd();
            if (!string.IsNullOrEmpty(datas))
            {
                LocalData.KiemKes = JsonConvert.DeserializeObject<List<KiemKe>>(datas);
            }
            //try
            //{
            //    LocalData.KiemKes = JsonConvert.DeserializeObject<List<KiemKe>>(datas);
            //}
            //catch
            //{
                
            //}

            //CHƯA CÓ DATA
            if(string.IsNullOrEmpty(datas))
                LocalData.KiemKes = new List<KiemKe>();

            stream.Close();

        }

        public static async void WriteFileData(List<KiemKe> KiemKes)
        {
            string rootPath = await DependencyService.Get<IFileSaver>().GetRootPath();
            string appFolder = rootPath + "/QRScanApp";
            string fileName = "KiemkeData.txt";
            string filePath = appFolder + "/" + fileName;

            var jsonStr = JsonConvert.SerializeObject(KiemKes);

            //TẠO FILE MỚI
            File.Delete(filePath);
            FileStream f=System.IO.File.Create(filePath);
            f.Close();

            FileStream fileData= File.OpenWrite(filePath);
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fileData))
            {
                sw.Write(jsonStr);
                sw.Close();
            }
            
            fileData.Close();
        }
    }
}
