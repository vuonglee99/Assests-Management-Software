using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using QRScan.Droid;
using QRScan.Services;

[assembly: Xamarin.Forms.Dependency(typeof(FileSaverService))]

namespace QRScan.Droid
{
    class FileSaverService : IFileSaver
    {

        public async Task<bool> CreateFile(string path)
        {
            //TRIED: .MyDocuments, 
            string f = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string folder = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
            string allpath = System.IO.Path.Combine(f, path);

            try
            {
                System.IO.File.Create(allpath);
                return await Task.FromResult(true);
            }
            catch (System.Exception exception)
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<string> GetRootPath()
        {
            string path= Android.OS.Environment.ExternalStorageDirectory.ToString();
            return await Task.FromResult(path);
        }
    }
}