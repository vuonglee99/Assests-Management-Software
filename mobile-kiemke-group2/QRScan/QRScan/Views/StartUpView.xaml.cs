using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QRScan.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartUpView : ContentPage
    {
        public StartUpView()
        {
            InitializeComponent();
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {
                datePicker.Focus();
            };
            tapGestureRecognizer.NumberOfTapsRequired = 1;
            dateCard.GestureRecognizers.Add(tapGestureRecognizer);
        }
    }
}