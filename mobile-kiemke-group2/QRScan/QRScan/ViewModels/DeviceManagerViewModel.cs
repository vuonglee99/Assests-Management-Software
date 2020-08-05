using Acr.UserDialogs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QRScan.Models;
using QRScan.Services;
using QRScan.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;

namespace QRScan.ViewModels
{
    public class DeviceManagerViewModel:BaseViewModel
    {
        public KiemKe kiemke { get; set; }
        private ObservableCollection<CTBKKItem> _records;
        public ObservableCollection<CTBKKItem> Records
        {
            get { return _records; }
            set { _records = value; OnPropertyChanged(nameof(Records)); }
        }
        public ICommand DeleteCommand { get; set; }
        public ICommand XacNhanKKCommand { get; set; }
        public DeviceManagerViewModel()
        {
            //NO USE
        }
        public DeviceManagerViewModel(BanKiemKe banKiemKe)
        {
            this.kiemke = DataProvider.GetKiemKe(banKiemKe);
            LoadData();
            DeleteCommand = new Command<CTBKKItem>(Delete);
            XacNhanKKCommand = new Command(XacNhanKK);
        }

        public async void XacNhanKK()
        {
            List<string> maTBs = new List<string>();
            //xác nhận kk server
            BanKiemKe BKK = kiemke.BKK;



            using (UserDialogs.Instance.Loading("Updating.."))
            {
                try
                {
                    //ChiTietBanKiemKe ct3 = new ChiTietBanKiemKe()
                    //{
                    //    CTBKK_MA_TB = "COD_00ghfg10",
                    //    CTBKK_TEN_TB = "i.CTBKK_TEN_TB",
                    //    CTBKK_TT_SAU = "Hư hỏng",
                    //    CTBKK_THOI_GIAN=DateTime.Now
                    //};
                    //            public string DONVI_ID { get; set; }
                    //public string BKK_CODE { get; set; }
                    //public DateTime NGAY_TAO { get; set; }
                    //public string LISTTB { get; set; }

                    var listRecordStr = JsonConvert.SerializeObject(kiemke.RECORDS);
                    var x = new 
                    {
                        DONVI_ID= BKK.KK_MADONVI,
                        BKK_CODE=BKK.KK_CODE,
                        NGAY_TAO= BKK.KK_NGAYTAO,
                        LISTTB= listRecordStr
                    };
                    //List<ChiTietBanKiemKe> lst = DataProvider.CloneRecordsFromKiemKe(kiemke);
                    
                    
                    var httpClient = new HttpClient();
                    var response = await httpClient.PostAsJsonAsync(Definitions.Localhost + "api/KiemKe/KiemKeOfflineProcess", x);

                    var content = await response.Content.ReadAsStringAsync();
                    JObject rss = JObject.Parse(content);
                    var validStr = rss["result"].ToString();
                    //string validStr = (resultAPI as JArray).First["result"];
                    if (validStr == "Bản kiểm kê ko tồn tại")
                    {
                        await NavigationService.GetTopPage().DisplayAlert("Lỗi", "Bản kiểm kê ko tồn tại!", "OK");
                        return;
                    }
                    string[] parts = validStr.Split('|');

                    if (parts[1] == "")
                    {
                        
                    }
                    else// trả về mã các thiết bị kk sai
                    {
                        string list = parts[1].Substring(0, parts[1].Length - 1);
                        maTBs = list.Split('#').ToList();
                    }
                }
                catch
                {
                    await NavigationService.GetTopPage().DisplayAlert("Lỗi", "Kết nối server bị lỗi, kiểm tra lại đường dẫn!", "OK");
                    return;
                }


                //update data on view
                List<ChiTietBanKiemKe> wrongRecords = new List<ChiTietBanKiemKe>();
                foreach (string ma in maTBs)
                {
                    foreach(ChiTietBanKiemKe item in kiemke.RECORDS)
                        if (item.CTBKK_MA_TB == ma)
                        {
                            wrongRecords.Add(item);
                            break;
                        }
                }
                kiemke.RECORDS = wrongRecords;

                LoadData();
                //update data source
                DataUpdater.UpdateRecordInKiemKeInLocalData(kiemke);

                //update data file
                DataUpdater.UpdateDataToFile();
            }

            DependencyService.Get<IMessage>().Shorttime("Kiểm kê thành công");

            if (Records.Count == 0)
            {
                DataUpdater.RemoveEmptyKiemKe();
                if (DataProvider.GetListBKK().Count == 0)
                {
                    await App.Current.MainPage.Navigation.PopToRootAsync();
                    return;
                }
                var VM = ListBKKView.GetInstance().BindingContext as ListBKKViewModel;
                VM.LoadData();
                await App.Current.MainPage.Navigation.PopAsync();

            }
                
        }

        public async void Delete(CTBKKItem cTBKKItem)
        {
            //xóa item trên source
            DataUpdater.RemoveRecordInKiemKe(this.kiemke, cTBKKItem.CTBKK);
            //update lại data vô file
            DataUpdater.UpdateDataToFile();

            //update data kiemke
            DataProvider.RemoveRecordFromKiemKe(kiemke, cTBKKItem.CTBKK);
            //reload view
            Records.Remove(cTBKKItem);
            ReloadSTT();

            if (Records.Count == 0)
            {
                DataUpdater.RemoveEmptyKiemKe();
                if (DataProvider.GetListBKK().Count == 0)
                {
                    await App.Current.MainPage.Navigation.PopToRootAsync();
                    return;
                }
                var VM = ListBKKView.GetInstance().BindingContext as ListBKKViewModel;
                VM.LoadData();
                await App.Current.MainPage.Navigation.PopAsync();

            }

        }

        public void ReloadSTT()
        {
            for (int i = 0; i < Records.Count; i++)
                Records[i].STT = i+1;
            Records = new ObservableCollection<CTBKKItem>(Records);
        }

        public void LoadData()
        {
            List<CTBKKItem> records = new List<CTBKKItem>();
            int count = 0;
            foreach(ChiTietBanKiemKe ctbkk in kiemke.RECORDS)
            {
                count++;
                CTBKKItem newItem = new CTBKKItem
                {
                    STT = count,
                    CTBKK = new ChiTietBanKiemKe
                    {
                        CTBKK_MA_TB = ctbkk.CTBKK_MA_TB,
                        CTBKK_TEN_TB = ctbkk.CTBKK_TEN_TB,
                        CTBKK_TT_SAU = ctbkk.CTBKK_TT_SAU,
                        CTBKK_THOI_GIAN = ctbkk.CTBKK_THOI_GIAN
                    }
                };
                records.Add(newItem);
            }

            Records = new ObservableCollection<CTBKKItem>(records);
            
        }
    }
}

/*
            {
                new CTBKKItem
                {
                    STT=1,
                    CTBKK=new ChiTietBanKiemKe
                    {
                        CTBKK_MA_TB="TB001",
                        CTBKK_TEN_TB="tivi",
                        CTBKK_TT="Bình thường",
                        CTBKK_THOI_GIAN=DateTime.Now
                    }
                },
                new CTBKKItem
                {
                    STT=2,
                    CTBKK=new ChiTietBanKiemKe
                    {
                        CTBKK_MA_TB="---",
                        CTBKK_TEN_TB="tủ lạnh",
                        CTBKK_TT="hư hỏng",
                        CTBKK_THOI_GIAN=DateTime.Now
                    }
                },
                new CTBKKItem
                {
                    STT=3,
                    CTBKK=new ChiTietBanKiemKe
                    {
                        CTBKK_MA_TB="TB034",
                        CTBKK_TEN_TB="xe máy",
                        CTBKK_TT="Đang sửa chữa",
                        CTBKK_THOI_GIAN=DateTime.Now
                    }
                },
                new CTBKKItem
                {
                    STT=4,
                    CTBKK=new ChiTietBanKiemKe
                    {
                        CTBKK_MA_TB="TB056",
                        CTBKK_TEN_TB="---",
                        CTBKK_TT="Bình thường",
                        CTBKK_THOI_GIAN=DateTime.Now
                    }
                },
                new CTBKKItem
                {
                    STT=5,
                    CTBKK=new ChiTietBanKiemKe
                    {
                        CTBKK_MA_TB="TB031",
                        CTBKK_TEN_TB="---",
                        CTBKK_TT="hư hỏng",
                        CTBKK_THOI_GIAN=DateTime.Now
                    }
                },
                new CTBKKItem
                {
                    STT=6,
                    CTBKK=new ChiTietBanKiemKe
                    {
                        CTBKK_MA_TB="TB0231",
                        CTBKK_TEN_TB="quạt",
                        CTBKK_TT="Bình thường",
                        CTBKK_THOI_GIAN=DateTime.Now
                    }
                },
                new CTBKKItem
                {
                    STT=7,
                    CTBKK=new ChiTietBanKiemKe
                    {
                        CTBKK_MA_TB="---",
                        CTBKK_TEN_TB="ghế",
                        CTBKK_TT="Bình thường",
                        CTBKK_THOI_GIAN=DateTime.Now
                    }
                }
            };
            */
