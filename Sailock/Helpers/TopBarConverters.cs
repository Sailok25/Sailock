using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using Sailock.ViewModels;

namespace Sailock.Helpers
{
    public class TopBarColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is LoginViewModel)
            {
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0A0A0A"));
            }

            try
            {
                return Application.Current.Resources["TopBarBackground"] as SolidColorBrush
                    ?? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0A0A0A"));
            }
            catch
            {
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0A0A0A"));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class TopBarForegroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is LoginViewModel)
            {
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#888888"));
            }

            try
            {
                return Application.Current.Resources["AppForeground"] as SolidColorBrush
                    ?? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
            }
            catch
            {
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}