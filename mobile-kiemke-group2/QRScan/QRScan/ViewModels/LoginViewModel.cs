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
    public class LoginViewModel : BaseViewModel
    {
        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(nameof(Username)); }
        }

        private bool _hide;
        public bool Hide
        {
            get { return _hide; }
            set { _hide = value; OnPropertyChanged(nameof(Hide)); }
        }

        private bool _remember;
        public bool Remember
        {
            get { return _remember; }
            set { _remember = value; OnPropertyChanged(nameof(Remember)); }
        }
        private string _error;
        public string Error
        {
            get { return _error; }
            set { _error = value; OnPropertyChanged(nameof(Error)); }
        }
        public ICommand HideCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand RememberCommand { get; set; }

        public LoginViewModel()
        {
            
            bool alreadyLogined = Preferences.Get("IsLogining", false);
            if (alreadyLogined) LoginDirectly();

            Username = Preferences.Get("Username", string.Empty);
            Password = Preferences.Get("Password", string.Empty);
            Remember = Preferences.Get("Remember", false);
            HideCommand = new Command(ChangeHide);
            LoginCommand = new Command(Login);
            RememberCommand = new Command(ChangeRemember);
        }

        public async void LoginDirectly()
        {
            string BrandId= Preferences.Get("BranchId", string.Empty);
            using (UserDialogs.Instance.Loading("Loading.."))
            {
                //get list bkk
                List<BanKiemKe> banKiemKes = new List<BanKiemKe>();
                try
                {
                    var httpClient = new HttpClient();
                    var response = await httpClient.PostAsJsonAsync(Definitions.Localhost + "api/KiemKe/GetKiemKeDateAndCode?branch_ID=" + BrandId, new { });

                    var content = await response.Content.ReadAsStringAsync();
                    JObject rss = JObject.Parse(content);
                    var resultAPI = rss["result"];
                    JArray arr = (resultAPI as JArray);
                    foreach (var elem in arr)
                    {
                        DateTime date = DateTime.ParseExact(elem["KK_DATE"].ToString(), "yyyy/MM/dd", null);
                        BanKiemKe newBKK = new BanKiemKe
                        {
                            KK_CODE = elem["KK_CODE"].ToString(),
                            KK_NGAYTAO = date,
                            KK_MADONVI = BrandId
                        };
                        banKiemKes.Add(newBKK);
                    }
                    AppValue.banKiemKes = banKiemKes;
                }
                catch
                {
                    await App.Current.MainPage.DisplayAlert("Lỗi", "Kết nối server bị lỗi, kiểm tra lại đường dẫn!", "OK");
                    return;
                }

                App.Current.MainPage = new NavigationPage(new StartUpView());
            }
            

            
        }

        public void ChangeRemember()
        {
            Remember = !Remember;
        }

        public async void Login()
        {
            Error = "";
            string errMessage = CheckValidInfor();
            if (!string.IsNullOrEmpty(errMessage))
            {
                Error = errMessage;
                return;
            }
            using (UserDialogs.Instance.Loading("Logging.."))
            {
                //CALL API LOGIN 
                JToken token;
                try
                {
                    //group2@gmail.com
                    //123qwe
                    var httpClient = new HttpClient();
                    var response = await httpClient.PostAsJsonAsync(Definitions.Localhost + "api/TokenAuth/Authenticate", new
                    {
                        userNameOrEmailAddress = Username,
                        password = Password,
                        twoFactorVerificationCode = "",
                        rememberClient = false,
                        twoFactorRememberClientToken = "",
                        singleSignIn = false,
                        returnUrl = ""
                    });

                    var content = await response.Content.ReadAsStringAsync();
                    JObject rss = JObject.Parse(content);
                    var resultAPI = rss["result"];
                    if (string.IsNullOrEmpty(resultAPI.ToString()))
                    {
                        await NavigationService.GetTopPage().DisplayAlert("Lỗi", "Thông tin Tài khoản hoặc mật khẩu không đúng, vui lòng kiểm tra lại!", "OK");
                        return;
                    }
                    token = resultAPI;

                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Lỗi", "Kết nối server bị lỗi, kiểm tra lại đường dẫn!", "OK");
                    return;
                }


                //lấy ra branchID từ userId
                string UserID = token["userId"].ToString();
                string BranchID;
                try
                {
                    var httpClient = new HttpClient();
                    var response = await httpClient.PostAsJsonAsync(Definitions.Localhost + "api/KiemKe/GetBranchIdFromUserId?id=" + UserID, new { });

                    var content = await response.Content.ReadAsStringAsync();
                    JObject rss = JObject.Parse(content);
                    var resultAPI = rss["result"];
                    BranchID = (resultAPI as JArray).First.ToString();
                }
                catch
                {
                    await App.Current.MainPage.DisplayAlert("Lỗi", "Kết nối server bị lỗi, kiểm tra lại đường dẫn!", "OK");
                    return;
                }

                //get list bkk
                List<BanKiemKe> banKiemKes = new List<BanKiemKe>();
                try
                {
                    var httpClient = new HttpClient();
                    var response = await httpClient.PostAsJsonAsync(Definitions.Localhost + "api/KiemKe/GetKiemKeDateAndCode?branch_ID=" + BranchID, new { });

                    var content = await response.Content.ReadAsStringAsync();
                    JObject rss = JObject.Parse(content);
                    var resultAPI = rss["result"];
                    JArray arr = (resultAPI as JArray);
                    foreach (var elem in arr)
                    {
                        DateTime date = DateTime.ParseExact(elem["KK_DATE"].ToString(), "yyyy/MM/dd", null);
                        BanKiemKe newBKK = new BanKiemKe
                        {
                            KK_CODE = elem["KK_CODE"].ToString(),
                            KK_NGAYTAO = date,
                            KK_MADONVI = BranchID
                        };
                        banKiemKes.Add(newBKK);
                    }
                    AppValue.banKiemKes = banKiemKes;
                }
                catch
                {
                    await App.Current.MainPage.DisplayAlert("Lỗi", "Kết nối server bị lỗi, kiểm tra lại đường dẫn!", "OK");
                    return;
                }

                //KHI ĐÃ LOGIN THÀNH CÔNG
                //set giá trị login
                Preferences.Set("UsernameLogin", Username);
                Preferences.Set("PasswordLogin", Password);
                Preferences.Set("UserId", UserID);
                Preferences.Set("BranchId", BranchID);
                Preferences.Set("IsLogining", true);

                if (Remember)
                {
                    Preferences.Set("Username", Username);
                    Preferences.Set("Password", Password);
                    Preferences.Set("Remember", true);
                }
                else
                {
                    Preferences.Set("Username", "");
                    Preferences.Set("Password", "");
                    Preferences.Set("Remember", false);
                }


                App.Current.MainPage = new NavigationPage(new StartUpView());
            }
            
        }

        public string CheckValidInfor()
        {
            if (string.IsNullOrEmpty(Username)) return "Tài khoản không được để trống";
            if (string.IsNullOrEmpty(Password)) return "Mật khẩu không được để trống";
            return "";
        }

        public void ChangeHide()
        {
            Hide = !Hide;
        }

    }
}
