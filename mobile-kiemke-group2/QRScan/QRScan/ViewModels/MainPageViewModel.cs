using Acr.UserDialogs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QRScan.Models;
using QRScan.Views;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace QRScan.ViewModels
{
    public class MainPageViewModel:BaseViewModel
    {

        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; OnPropertyChanged(nameof(Date)); }
        }

        private string _maBKK;
        public string MaBKK
        {
            get { return _maBKK; }
            set { _maBKK = value; OnPropertyChanged(nameof(MaBKK)); }
        }

        private string _maDV;
        public string MaDV
        {
            get { return _maDV; }
            set { _maDV = value; OnPropertyChanged(nameof(MaDV)); }
        }

        public ICommand StartScanCommand { get; set; }
        public ICommand ShowScanCommand { get; set; }
        public ICommand GoBackCommand { get; set; }

        public MainPageViewModel()
        {
            ShowScanCommand = new Command(ShowScan);
            GoBackCommand = new Command(GoBack);
            Date = DateTime.Today;

         

            //var json = JsonConvert.SerializeObject(kiemKe);
            //KiemKe result = JsonConvert.DeserializeObject<KiemKe>(json);
            
        }

        public async void GoBack()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        } 

        public async void ShowScan()
        {
            var page = MainPage.GetInstance();
            var httpClient = new HttpClient();
            string validStr = "";
            try
            {
                var result = await httpClient.PostAsJsonAsync(Definitions.Localhost + "api/KiemKe/BKK_CHECK?dv_QL=" + MaDV + "&bkk_ID=" + MaBKK + "&ngay_TAO=" + Date.ToString("yyyy-MM-dd"), new { });
                var content = await result.Content.ReadAsStringAsync();
                JObject rss = JObject.Parse(content);
                var resultAPI = rss["result"];
                validStr = (resultAPI as JArray).First["result"].ToString();
            }
            catch
            {
                await page.DisplayAlert("Lỗi", "Kết nối server bị lỗi, kiểm tra lại đường dẫn!", "Ok");
                return;
            }
            
             
             
            if (validStr != "Hop Le")
            {
                
                await page.DisplayAlert("Lỗi", "Thông tin kiểm kê không đúng hoặc bản kiểm kê đã bị đóng!", "Ok");
                return;
            }
            using (UserDialogs.Instance.Loading("Waiting.."))
            {
                ScannerView scanPage = ScannerView.GetInstance();
                //scanPage.InitData(Date, MaBKK, MaDV);
                await App.Current.MainPage.Navigation.PushAsync(scanPage, true);
            }
            
        }


    }
}
