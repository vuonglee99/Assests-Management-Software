using Acr.UserDialogs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QRScan.Models;
using QRScan.Services;
using QRScan.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace QRScan.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScannerView : ZXingScannerPage
    {
        public bool popupIsOpen = false;

        private static ScannerView _instance;

        public static ScannerView GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ScannerView();
            }
            return _instance;
        }

        private ScannerView()
        {
            InitializeComponent();
            

        }

        public void SetBKKInfor()
        {
            MaDV.Text = AppValue.banKiemKe.KK_MADONVI;
            MaKK.Text = AppValue.banKiemKe.KK_CODE;
            NgayTao.Text = AppValue.banKiemKe.KK_NGAYTAO.ToString("dd/MM/yyyy");
        }

        public string GetValueInString(string input)
        {
            try
            {
                string[] items = input.Split('"');
                return items[items.Length - 2];
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }

        private async void Scanner_OnScanResult(ZXing.Result result)
        {

            if (popupIsOpen) return;

            

            //XỬ LÝ CHUỖI QR
            //{"MaTB":"TB001","TenTB":"TV","DonVi":"KTX A"}
            string MaTB = "";
            string TenTB = "";
            string Donvi = "";
            bool error = false;
            try
            {
                string qrStr = result.Text;
                string content = qrStr.Substring(1, qrStr.Length - 2);
                string[] parts = content.Split(',');
                MaTB = GetValueInString(parts[0]);
                TenTB = GetValueInString(parts[1]);
                Donvi = GetValueInString(parts[2]);
            }
            catch(Exception e)
            {
                //DisplayAlert("Lỗi", "Mã QR bị lỗi, vui lòng kiểm tra lại!", "OK");
               
                return;

            }

            popupIsOpen = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                popupIsOpen = await DisplayAlert("Thiết bị", "Mã thiết bị: "+MaTB+"\nTên thiết bị: "+TenTB+"\nĐơn vị: "+Donvi, "OK", "Hủy");
                if (popupIsOpen == true)
                {
                    ThietBi thietbi = new ThietBi();
                    if (AppValue.KKType == 1)//ONLINE
                    {
                        var httpClient = new HttpClient();
                        JsonSerializer serializer = new JsonSerializer();
                        var response = await httpClient.GetStringAsync(Definitions.Localhost + "api/ThietBi/ThietBiById?thietbiId=" + MaTB);

                        JObject rss2 = JObject.Parse(response);
                        var resultAPI2 = rss2["result"];
                        thietbi = (ThietBi)serializer.Deserialize(new JTokenReader(resultAPI2), typeof(ThietBi));
                        
                        if (thietbi == null)
                        {
                            await DisplayAlert("Lỗi", "Thiết bị không tồn tại hoặc đã bị xóa!", "OK", "Hủy");
                            popupIsOpen = false;
                            return;
                        }
                        thietbi.TB_TEN_DV = Donvi;
                    }
                    else //OFFLINE
                    {
                        thietbi.TB_ID = MaTB;
                        thietbi.TB_TEN = TenTB;
                        thietbi.TB_TEN_DV = Donvi;

                    }
                    

                    using (UserDialogs.Instance.Loading("Waiting.."))
                    {
                        StockTakingView sw = new StockTakingView(thietbi);
                        await App.Current.MainPage.Navigation.PushAsync(sw, true);
                    }

                    
                    
                }

            });
        }

        
    }
}