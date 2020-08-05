using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace QRScan.Services
{
    public class NavigationService
    {
        public static Page GetTopPage()
        {
            var stack = App.Current.MainPage.Navigation.NavigationStack;
            return stack[stack.Count - 1];
        }
    }
}
