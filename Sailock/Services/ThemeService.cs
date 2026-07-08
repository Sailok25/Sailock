using System.Windows;
using System.Windows.Media;

namespace Sailock.Services
{
    public static class ThemeService
    {
        public static void ApplyContrast(bool highContrast)
        {
            var resources = Application.Current.Resources;

            if (highContrast)
            {
                resources["ContrastBorderBrush"] = ColorFromHex("#2A2A2A");
                resources["ContrastBorderThickness"] = new Thickness(1);
                resources["ContrastSeparatorBrush"] = ColorFromHex("#1A1A1A");
                resources["ContrastShadowOpacity"] = 0.0;
            }
            else
            {
                resources["ContrastBorderBrush"] = ColorFromHex("#141414");
                resources["ContrastBorderThickness"] = new Thickness(0.5);
                resources["ContrastSeparatorBrush"] = ColorFromHex("#0A0A0A");
                resources["ContrastShadowOpacity"] = 0.0;
            }
        }

        private static SolidColorBrush ColorFromHex(string hex)
        {
            var color = (Color)ColorConverter.ConvertFromString(hex);
            return new SolidColorBrush(color);
        }

        public static void ApplyTextSize(string size)
        {
            double scale = size switch
            {
                "Small" => 1.0,
                "Large" => 1.40,
                _ => 1.15
            };

            Application.Current.Resources["GlobalScale"] = scale;
            Application.Current.Resources["GlobalScaleInverse"] = 1.0 / scale;
        }

        public static void ApplyTheme(bool isDark)
        {
            var res = Application.Current.Resources;

            if (isDark)
            {
                // TEMA OSCURO
                res["AppBackground"] = BrushFromHex("#000000");
                res["AppSurface"] = BrushFromHex("#0A0A0A");
                res["AppSurface2"] = BrushFromHex("#111111");
                res["AppForeground"] = BrushFromHex("#FFFFFF");
                res["AppForegroundDim"] = BrushFromHex("#666666");
                res["AppAccent"] = BrushFromHex("#00FF41");
                res["AppInputBackground"] = BrushFromHex("#0D0D0D");
                res["SidebarLogoSource"] = "pack://application:,,,/Resources/sailock_logo.png";

                res["TextBlockForeground"] = BrushFromHex("#FFFFFF");
                res["TextBoxBackground"] = BrushFromHex("#0D0D0D");
                res["TextBoxForeground"] = BrushFromHex("#00FF41");
                res["TextBoxBorderBrush"] = BrushFromHex("#333333");
                res["PasswordBoxBackground"] = BrushFromHex("#0D0D0D");
                res["PasswordBoxForeground"] = BrushFromHex("#00FF41");
                res["PasswordBoxBorderBrush"] = BrushFromHex("#00ab26");
                res["ButtonForeground"] = BrushFromHex("#00FF41");
                res["ButtonBorderBrush"] = BrushFromHex("#00FF41");
                res["ButtonHoverBackground"] = BrushFromHex("#00FF41");
                res["ButtonHoverForeground"] = BrushFromHex("#000000");
                res["ModalBackground"] = BrushFromHex("#111111");
                res["ModalBorderBrush"] = BrushFromHex("#00ab26");
                res["SidebarBackground"] = BrushFromHex("#080808");
                res["TopBarBackground"] = BrushFromHex("#0A0A0A");
                res["WindowControlButtonHover"] = BrushFromHex("#1A1A1A");
                res["WindowControlButtonCloseHover"] = BrushFromHex("#E81123");
                res["PlaceholderForeground"] = BrushFromHex("#4D4D4D");

                // Colores para ScrollBar (tema oscuro - muy visible)
                res["ScrollBarBackground"] = BrushFromHex("#1A1A1A");
                res["ScrollBarThumbBackground"] = BrushFromHex("#333333");

                // Colores adicionales para controles
                res["CheckBoxBorder"] = BrushFromHex("#444444");
                res["CheckBoxBorderHover"] = BrushFromHex("#00ab26");
                res["CheckBoxCheckMark"] = BrushFromHex("#00FF41");
                res["CheckBoxBackground"] = BrushFromHex("#0D1F0D");

                res["SliderBackground"] = BrushFromHex("#1A1A1A");
                res["SliderThumbBackground"] = BrushFromHex("#00FF41");
                res["SliderThumbBackgroundHover"] = BrushFromHex("#00ab26");

                res["ToggleTrackBackground"] = BrushFromHex("#2A2A2A");
                res["ToggleTrackBackgroundChecked"] = BrushFromHex("#003d10");
                res["ToggleThumbBackground"] = BrushFromHex("#555555");
                res["ToggleThumbBackgroundChecked"] = BrushFromHex("#00FF41");
            }
            else
            {
                // TEMA CLARO
                res["AppBackground"] = BrushFromHex("#E0E0E0");
                res["AppSurface"] = BrushFromHex("#F0F0F0");
                res["AppSurface2"] = BrushFromHex("#E8E8E8");
                res["AppForeground"] = BrushFromHex("#1A1A1A");
                res["AppForegroundDim"] = BrushFromHex("#555555");
                res["AppAccent"] = BrushFromHex("#007A1F");
                res["AppInputBackground"] = BrushFromHex("#FFFFFF");
                res["SidebarLogoSource"] = "pack://application:,,,/Resources/sailock_logo_dark.png";

                res["TextBlockForeground"] = BrushFromHex("#1A1A1A");
                res["TextBoxBackground"] = BrushFromHex("#EDEDED");
                res["TextBoxForeground"] = BrushFromHex("#1A1A1A");
                res["TextBoxBorderBrush"] = BrushFromHex("#CCCCCC");
                res["PasswordBoxBackground"] = BrushFromHex("#FFFFFF");
                res["PasswordBoxForeground"] = BrushFromHex("#1A1A1A");
                res["PasswordBoxBorderBrush"] = BrushFromHex("#007A1F");
                res["ButtonForeground"] = BrushFromHex("#007A1F");
                res["ButtonBorderBrush"] = BrushFromHex("#007A1F");
                res["ButtonHoverBackground"] = BrushFromHex("#007A1F");
                res["ButtonHoverForeground"] = BrushFromHex("#FFFFFF");
                res["ModalBackground"] = BrushFromHex("#FFFFFF");
                res["ModalBorderBrush"] = BrushFromHex("#007A1F");
                res["SidebarBackground"] = BrushFromHex("#D4D4D4");
                res["TopBarBackground"] = BrushFromHex("#CDCDCD");
                res["WindowControlButtonHover"] = BrushFromHex("#B8B8B8");
                res["WindowControlButtonCloseHover"] = BrushFromHex("#E81123");
                res["PlaceholderForeground"] = BrushFromHex("#9A9A9A");

                // Colores para ScrollBar
                res["ScrollBarBackground"] = BrushFromHex("#C0C0C0");
                res["ScrollBarThumbBackground"] = BrushFromHex("#808080");

                // Colores adicionales para controles
                res["CheckBoxBorder"] = BrushFromHex("#888888");
                res["CheckBoxBorderHover"] = BrushFromHex("#007A1F");
                res["CheckBoxCheckMark"] = BrushFromHex("#007A1F");
                res["CheckBoxBackground"] = BrushFromHex("#E8F5E9");

                res["SliderBackground"] = BrushFromHex("#D0D0D0");
                res["SliderThumbBackground"] = BrushFromHex("#007A1F");
                res["SliderThumbBackgroundHover"] = BrushFromHex("#005A15");

                res["ToggleTrackBackground"] = BrushFromHex("#BBBBBB");
                res["ToggleTrackBackgroundChecked"] = BrushFromHex("#C8E6C9");
                res["ToggleThumbBackground"] = BrushFromHex("#888888");
                res["ToggleThumbBackgroundChecked"] = BrushFromHex("#007A1F");
            }

            bool isHighContrast = (bool?)res["IsHighContrast"] ?? true;
            ApplyContrast(isHighContrast);
        }

        private static SolidColorBrush BrushFromHex(string hex)
        {
            var color = (Color)ColorConverter.ConvertFromString(hex);
            return new SolidColorBrush(color);
        }
    }
}
