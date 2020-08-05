using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QRScan.Models;
using QRScan.Services;
using QRScan.Views;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace QRScan.ViewModels
{
    public class ScannerViewModel:BaseViewModel
    {
        private bool _choose1;
        public bool Choose1
        {
            get { return _choose1; }
            set { _choose1 = value; OnPropertyChanged(nameof(Choose1)); }
        }

        private bool _choose2;
        public bool Choose2
        {
            get { return _choose2; }
            set { _choose2 = value; OnPropertyChanged(nameof(Choose2)); }
        }

        private string _maTB;
        public string MaTB
        {
            get { return _maTB; }
            set { _maTB = value; OnPropertyChanged(nameof(MaTB)); }
        }

        private string _tenTB;
        public string TenTB
        {
            get { return _tenTB; }
            set { _tenTB = value; OnPropertyChanged(nameof(TenTB)); }
        }

        public ICommand Choose1Command { get; set; } //nhập mã
        public ICommand Choose2Command { get; set; } //nhập tên
        public ICommand GobackCommand { get; set; }
        public ICommand KiemKeCommand { get; set; }
        public ScannerViewModel()
        {
            Choose1 = true;
            Choose2 = false;
            Choose1Command = new Command(Choose1Action);
            Choose2Command = new Command(Choose2Action);
            GobackCommand = new Command(Goback);
            KiemKeCommand = new Command(KiemKe);
        }

        public async void KiemKe()
        {
            string error = CheckValidInfor();
            if (error != "")
            {
                var page = NavigationService.GetTopPage();
                await page.DisplayAlert("Lỗi", error, "OK");
                return;
            }

            ThietBi thietbi = new ThietBi();
            if (AppValue.KKType == 1)//ONLINE
            {
                if (Choose1)//nhập mã
                {
                    var httpClient = new HttpClient();
                    JsonSerializer serializer = new JsonSerializer();
                    var response = await httpClient.GetStringAsync(Definitions.Localhost + "api/ThietBi/ThietBiById?thietbiId=" + MaTB);

                    JObject rss2 = JObject.Parse(response);
                    var resultAPI2 = rss2["result"];
                    thietbi = (ThietBi)serializer.Deserialize(new JTokenReader(resultAPI2), typeof(ThietBi));

                    if (thietbi == null)
                    {
                        var scanPage = ScannerView.GetInstance();
                        await scanPage.DisplayAlert("Lỗi", "Thiết bị không tồn tại hoặc đã bị xóa!", "OK", "Hủy");
                        scanPage.popupIsOpen = false;
                        return;
                    }
                    
                }
                else //nhập tên
                {
                    thietbi.TB_TEN = TenTB;
                    thietbi.TB_NGAY_MUA = new DateTime(0001,01,01);
                    thietbi.TB_NGAY_BH = new DateTime(0001, 01, 01);
                    thietbi.TB_NGAY_HET_BH = new DateTime(0001, 01, 01);
                    thietbi.TB_Nam_SX = 0;
                }
                
            }
            else//OFFLINE
            {
                if (Choose1) thietbi.TB_ID = MaTB;
                else thietbi.TB_TEN = TenTB;
            }
            

            using (Acr.UserDialogs.UserDialogs.Instance.Loading("Loading.."))
            {
                StockTakingView sw = new StockTakingView(thietbi);
                await App.Current.MainPage.Navigation.PushAsync(sw, true);
            }

        }

        public string CheckValidInfor()
        {
            if (Choose1 && string.IsNullOrEmpty(MaTB)) return "Mã thiết bị chưa có giá trị!";
            if (Choose2 && string.IsNullOrEmpty(TenTB)) return "Tên thiết bị chưa có giá trị!";
            return "";
        }

        public async void Goback()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
        public void Choose1Action()
        {
            Choose1 = true;
            Choose2 = false;
        }
        public void Choose2Action()
        {
            Choose1 = false;
            Choose2 = true;
        }
    }
}
