using QRScan.Models;
using QRScan.Services;
using QRScan.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace QRScan.ViewModels
{
    public class ListBKKViewModel:BaseViewModel
    {

        private ObservableCollection<BanKiemKe> _banKiemKes;
        public ObservableCollection<BanKiemKe> BanKiemKes
        {
            get { return _banKiemKes; }
            set { _banKiemKes = value; OnPropertyChanged(nameof(BanKiemKes)); }
        }
        public ICommand ShowListCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ListBKKViewModel()
        {
            LoadData();
            DeleteCommand = new Command<BanKiemKe>(Delete);
            ShowListCommand = new Command<BanKiemKe>(ShowList);
        }

        public async void Delete(BanKiemKe deletedBKK)
        {
            
            //Update data ở LocalData
            DataUpdater.RemoveKiemKeByBKK(deletedBKK);

            // Update data ở UI
            LoadData();

            //Update data xuống file
            DataUpdater.UpdateDataToFile();

            DependencyService.Get<IMessage>().Shorttime("Xóa thành công");
            //Nếu không còn item nào thì thoát
            if (BanKiemKes.Count == 0) await App.Current.MainPage.Navigation.PopAsync();
        }

        public void LoadData()
        {
            BanKiemKes = new ObservableCollection<BanKiemKe>(DataProvider.GetListBKK());

        }

        public async void ShowList(BanKiemKe banKiemKe)
        {
            using (Acr.UserDialogs.UserDialogs.Instance.Loading("Loading.."))
            {
                var DeviceManager = new DeviceManagerView();
                var VM = new DeviceManagerViewModel(banKiemKe);
                DeviceManager.BindingContext = VM;
                await App.Current.MainPage.Navigation.PushAsync(DeviceManager);
            }
            
        }
    }
}
