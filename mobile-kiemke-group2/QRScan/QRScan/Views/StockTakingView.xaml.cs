using QRScan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using QRScan.ViewModels;
using QRScan.Services;
using Acr.UserDialogs;

namespace QRScan.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StockTakingView : ContentPage
    {
        List<string> states = new List<string> { "Bình thường", "Hư hỏng", "Sửa chữa" };
        ThietBi thietBi;

        public StockTakingView()
        {
            InitializeComponent();
            //this.BKK_ID = bkk_ID;
            //this.DV_QL = dv_QL;
            //this.NgayTao = ngayTao;
            //this.thietBi = thietBi;
            LoadDataPicker();
            //SetEvent();
        }

        public StockTakingView(ThietBi thietBi)
        {
            InitializeComponent();
            this.thietBi = thietBi;
            LoadDataPicker();
            SetData();
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += async (s, e) => {
                await App.Current.MainPage.Navigation.PopAsync();
            };
            backLabel.GestureRecognizers.Add(tapGestureRecognizer);
        }

        public void SetData()
        {
            Ma.Text = thietBi.TB_ID;
            Ten.Text = thietBi.TB_TEN;
            TinhTrang.Text = ConvertTinhTrang(thietBi.TB_TT_HOAT_DONG);
            DonViQL.Text = thietBi.TB_TEN_DV;
            CKBD.Text = thietBi.TB_CHU_KY_BAO_DUONG;
            if(!CheckDateNull(thietBi.TB_NGAY_MUA))
                NgayMua.Text = thietBi.TB_NGAY_MUA.ToString("dd/MM/yyyy");
            if (!CheckDateNull(thietBi.TB_NGAY_BH))
                NgayBH.Text = thietBi.TB_NGAY_BH.ToString("dd/MM/yyyy");
            if (!CheckDateNull(thietBi.TB_NGAY_HET_BH))
                NgayHetBH.Text = thietBi.TB_NGAY_HET_BH.ToString("dd/MM/yyyy");
            if(thietBi.TB_Nam_SX!=0)
                NamSX.Text = thietBi.TB_Nam_SX.ToString();
            NSX.Text = thietBi.TB_NSX;
            Serial.Text = thietBi.TB_SERIAL;
            MoTa.Text = thietBi.TB_MO_TA;

            if (AppValue.KKType == 2)//OFFLINE
            {
                TinhTrang.IsVisible = false;
                TinhTrangTitle.IsVisible = false;
                TinhTrangContainer.IsVisible = false;

                CKBD.IsVisible = false;
                CKBDTitle.IsVisible = false;
                CKBDContainer.IsVisible = false;

                NgayMua.IsVisible = false;
                NgayMuaTitle.IsVisible = false;
                NgayMuaContainer.IsVisible = false;

                NgayBH.IsVisible = false;
                NgayBHTitle.IsVisible = false;
                NgayBHContainer.IsVisible = false;

                NgayHetBH.IsVisible = false;
                NgayHetBHTitle.IsVisible = false;
                NgayHetBHContainer.IsVisible = false;

                NamSX.IsVisible = false;
                NamSXTitle.IsVisible = false;
                NamSXContainer.IsVisible = false;

                NSX.IsVisible = false;
                NSXTitle.IsVisible = false;
                NSXContainer.IsVisible = false;

                Serial.IsVisible = false;
                SerialTitle.IsVisible = false;
                SerialContainer.IsVisible = false;

                MoTa.IsVisible = false;
                MoTaTitle.IsVisible = false;
                MoTaContainer.IsVisible = false;
            }

            if (AppValue.KKType == 1)
            {
                close1.TextColor = Color.FromHex("#2196F3");
                close2.TextColor = Color.FromHex("#2196F3");
                text.TextColor = Color.FromHex("#2196F3");
                text.Text = "Online";
                image.Source = "online";
            }
            else
            {
                close1.TextColor = Color.Gray;
                close2.TextColor = Color.Gray;
                text.TextColor = Color.Gray;
                text.Text = "Offline";
                image.Source = "offline";
            }
        }

        public string ConvertTinhTrang(string tinhTrang)
        {
            switch (tinhTrang)
            {
                case "0": return "Hư hỏng";
                case "1": return "Bình thường";
                default: return "Sửa chữa";
            }
        }

        public void LoadDataPicker()
        {
            List<string> states = new List<string>
            {
                "Bình thường",
                "Hư hỏng",
                "Sửa chữa"
            };
            State_Picker.ItemsSource = states;
            State_Picker.SelectedItem = State_Picker.ItemsSource[0];

        }

        private async void Confirm_Clicked(object sender, EventArgs e)
        {
            var httpClient = new HttpClient();
            using (Acr.UserDialogs.UserDialogs.Instance.Loading("Updating..")) {
                if (AppValue.KKType == 1) //ONLINE
                {
                    BanKiemKe banKiemKe = AppValue.banKiemKe;
                    try
                    {
                        HttpResponseMessage response;
                        if (!string.IsNullOrEmpty(thietBi.TB_ID))
                            response = await httpClient.PostAsJsonAsync(Definitions.Localhost + "api/ChiTietBanKiemKe/CTBKK_XacNhanKK?dv_QL=" + banKiemKe.KK_MADONVI + "&bkk_ID=" + banKiemKe.KK_CODE + "&ngay_TAO=" + banKiemKe.KK_NGAYTAO.ToString("yyyy-MM-dd") + "&ma_TB=" + this.thietBi.TB_ID + "&tt_SAU=" + State_Picker.SelectedItem.ToString(), new { });
                        else response = await httpClient.PostAsJsonAsync(Definitions.Localhost + "api/ChiTietBanKiemKe/CTBKK_XacNhanKK_Ten?dv_QL=" + banKiemKe.KK_MADONVI + "&bkk_ID=" + banKiemKe.KK_CODE + "&ngay_TAO=" + banKiemKe.KK_NGAYTAO.ToString("yyyy-MM-dd") + "&ten_TB=" + this.thietBi.TB_TEN + "&tt_SAU=" + State_Picker.SelectedItem.ToString(), new { });
                        
                        var content = await response.Content.ReadAsStringAsync();
                        JObject rss = JObject.Parse(content);
                        var resultAPI = rss["result"];
                        string validStr = (resultAPI as JArray).First["result"].ToString();
                        if (validStr == "Lỗi" || validStr== "Bản kiểm kê ko tồn tại")
                        {
                            await DisplayAlert("Lỗi", "Kiểm kê thất bại, kiểm tra lại thông tin kiểm kê!", "OK");
                            return;
                        }
                    }
                    catch
                    {
                        await DisplayAlert("Lỗi", "Kết nối server bị lỗi, kiểm tra lại đường dẫn!", "OK");
                        return;
                    }

                }
                else //OFFLINE
                {
                    using (UserDialogs.Instance.Loading("Updating.."))
                    {
                        //tạo record mới
                        ChiTietBanKiemKe chiTietBanKiemKe = new ChiTietBanKiemKe
                        {
                            CTBKK_MA_TB = thietBi.TB_ID,
                            CTBKK_TEN_TB = thietBi.TB_TEN,
                            CTBKK_TT_SAU = State_Picker.SelectedItem.ToString(),
                            CTBKK_THOI_GIAN = DateTime.Now
                        };

                        //thêm record vào list  (trên source) appvalue
                        DataUpdater.AddNewRecordToKiemKe(chiTietBanKiemKe);
                        //update record vào LocalData(trên source)
                        DataUpdater.UpdateKiemKeInLocalData();
                        //update data xuống file 
                        DataUpdater.UpdateDataToFile();
                    }


                }


                var scanPage = ScannerView.GetInstance();
                scanPage.popupIsOpen = false;

                await App.Current.MainPage.Navigation.PopAsync();
            }
            

            var message = "Kiểm kê thành công";
            DependencyService.Get<IMessage>().Shorttime(message);

        }

        protected override bool OnBackButtonPressed()
        {
            var scanPage = ScannerView.GetInstance();
            scanPage.popupIsOpen = false;
            return base.OnBackButtonPressed();

        }

        public bool CheckDateNull(DateTime date)
        {
            if (date.Year == 1 && date.Month == 1 && date.Day == 1) return true;
            return false;
        }
    }
}