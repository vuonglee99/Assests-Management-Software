using Acr.UserDialogs;
using Newtonsoft.Json.Linq;
using QRScan.Models;
using QRScan.Services;
using QRScan.Views;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QRScan.ViewModels
{
    public class StartUpViewModel:BaseViewModel
    {
        HttpClient httpClient = new HttpClient();
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

        private List<string> _maBKKs;
        public List<string> MaBKKs
        {
            get { return _maBKKs; }
            set { _maBKKs = value; OnPropertyChanged(nameof(MaBKKs)); }
        }
        private string _currentBKK;
        public string CurrentBKK
        {
            get { return _currentBKK; }
            set { _currentBKK = value; OnPropertyChanged(nameof(CurrentBKK)); }
        }

        private bool _canKK;
        public bool CanKK
        {
            get { return _canKK; }
            set { _canKK = value; OnPropertyChanged(nameof(CanKK)); }
        }
        private bool _valueNull;
        public bool ValueNull
        {
            get { return _valueNull; }
            set { _valueNull = value; OnPropertyChanged(nameof(ValueNull)); }
        }
        private bool _valueNotNull;
        public bool ValueNotNull
        {
            get { return _valueNotNull; }
            set { _valueNotNull = value; OnPropertyChanged(nameof(ValueNotNull)); }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(nameof(Title)); }
        }

        public string Username
        {
            get { return Preferences.Get("UsernameLogin", string.Empty); }
        }

        public string BranchId
        {
            get { return Preferences.Get("BranchId", string.Empty); }
        }

        public ICommand Choose1Command { get; set; } //online
        public ICommand Choose2Command { get; set; } //offline
        public ICommand ScanCommand { get; set; }
        public ICommand ManagerCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        public StartUpViewModel()
        {
            LoadData();
            
            Choose1Command = new Command(Choose1Action);
            Choose2Command = new Command(Choose2Action);
            ScanCommand = new Command(Scan);
            ManagerCommand = new Command(Manager);
            LogoutCommand = new Command(Logout);
        }

        public void Logout()
        {
            using(UserDialogs.Instance.Loading("Logging out.."))
            {
                Preferences.Set("IsLogining", false);
                App.Current.MainPage = LoginView.GetInstance();
            }
            
        }

        public void LoadData()
        {
            CanKK = true;
            ValueNotNull = true;
            ValueNull = false;
            Title = "Nhập thông tin để bắt đầu kiểm kê";
            Choose1 = false;
            Choose2 = true;
            MaBKKs = new List<string>();
            if (AppValue.banKiemKes != null)
            {
                foreach (BanKiemKe item in AppValue.banKiemKes)
                    MaBKKs.Add(item.KK_CODE);
            }
            

            if (MaBKKs.Count > 0)
            {
                CurrentBKK = MaBKKs[0];
                
            }
            else
            {
                CanKK = false;
                ValueNotNull = false;
                ValueNull = true;
                Title = "Đơn vị chưa có bản kiểm kê nào";
            }
                
        }

        public async void Scan()
        {
            try
            {
                FileService.CreateFileData();
            }
            catch
            {
                await NavigationService.GetTopPage().DisplayAlert("Lỗi", "App chưa được quyền truy cập dữ liệu trong máy", "OK");
                return;
            }
            BanKiemKe banKiemKe = new BanKiemKe();
            foreach(BanKiemKe item in AppValue.banKiemKes)
                if(CurrentBKK==item.KK_CODE)
                {
                    banKiemKe = item;
                    break;
                }

            AppValue.banKiemKe = banKiemKe;
            if (Choose1) //online
            {
                AppValue.KKType = 1;
            }
            else //offline
            {
                AppValue.KKType = 2;
            }

            using (Acr.UserDialogs.UserDialogs.Instance.Loading("Loading.."))
            {
                if (Choose1) //KK ONLINE
                {
                    
                }
                else //KK OFFLINE
                {
                    //LẤY RA KIEMKE TRONG LOCAL DATA
                    KiemKe kiemke = DataProvider.GetKiemKe(banKiemKe);
                    if (kiemke == null)
                    {
                        kiemke = new KiemKe
                        {
                            BKK = banKiemKe,
                            RECORDS = new List<ChiTietBanKiemKe>()
                        };
                        LocalData.KiemKes.Add(kiemke);
                    }

                    //Lưu lại thông tin kiểm kê hiện tại
                    AppValue.kiemKe = kiemke;
                }

                //chuyển sang page quét mã
                var scanPage = ScannerView.GetInstance();
                scanPage.SetBKKInfor();
                await App.Current.MainPage.Navigation.PushAsync(scanPage);
            }
            
        }


        public async void Manager()
        {
            
            using (Acr.UserDialogs.UserDialogs.Instance.Loading("Loading.."))
            {
                try
                {
                    FileService.CreateFileData();
                }
                catch
                {
                    await NavigationService.GetTopPage().DisplayAlert("Lỗi", "App chưa được quyền truy cập dữ liệu trong máy", "OK");
                    return;
                }
                
                DataUpdater.RemoveEmptyKiemKe();
                if(LocalData.KiemKes.Count==0)
                {
                    await App.Current.MainPage.DisplayAlert("Danh sách rỗng", "Chưa có bản kiểm kê offline nào!", "OK");
                    return;
                }
                var page = ListBKKView.GetInstance();
                var VM =new ListBKKViewModel();
                page.BindingContext = VM;
                await App.Current.MainPage.Navigation.PushAsync(page);
            }
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
