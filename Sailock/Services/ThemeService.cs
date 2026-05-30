using System.Windows;

namespace Sailock.Services
{
    public static class ThemeService
    {
        public static void ApplyContrast(bool highContrast)
        {
            var resources = Application.Current.Resources;

            if (highContrast)
            {
                // Bordes marcados, separadores visibles
                resources["ContrastBorderBrush"] = ColorFromHex("#2A2A2A");
                resources["ContrastBorderThickness"] = new Thickness(1);
                resources["ContrastSeparatorBrush"] = ColorFromHex("#1A1A1A");
                resources["ContrastShadowOpacity"] = 0.0;
            }
            else
            {
                // Bordes sutiles, separadores como sombra
                resources["ContrastBorderBrush"] = ColorFromHex("#141414");
                resources["ContrastBorderThickness"] = new Thickness(0.5);
                resources["ContrastSeparatorBrush"] = ColorFromHex("#0A0A0A");
                resources["ContrastShadowOpacity"] = 0.0;
            }
        }

        private static System.Windows.Media.SolidColorBrush ColorFromHex(string hex)
        {
            var color = (System.Windows.Media.Color)
                System.Windows.Media.ColorConverter.ConvertFromString(hex);
            return new System.Windows.Media.SolidColorBrush(color);
        }
    }
}