using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace QRScan.Views.Converters
{
    public class ChoosedLabelColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                bool input = (bool)value;
                if (input) return Color.Black;
                return Color.Gray;
            }
            catch
            {
                int input2 = (int)value;
                if (input2 % 2 == 1) return Color.White;
                return Color.FromHex("#cceaff");
            }
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
