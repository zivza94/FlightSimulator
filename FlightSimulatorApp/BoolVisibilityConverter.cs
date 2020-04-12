using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace FlightSimulatorApp
{
    class BoolVisibilityConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility vis = new Visibility();
            if (value is bool b)
            {
                if (b == true)
                {
                    //change to false -> hidden
                    vis = Visibility.Hidden;
                }
                else
                {
                    //change to true -> visible
                    vis = Visibility.Visible;
                }
                return vis;
            }
            throw new InvalidCastException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
