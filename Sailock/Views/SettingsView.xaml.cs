using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Sailock.Views
{
    public partial class SettingsView : UserControl
    {
        public static List<string> TextSizeOptions { get; } =
            new List<string> { "Small", "Default", "Large" };

        public static List<string> LanguageOptions { get; } =
            new List<string> { "English", "Español" };

        public string SelectedTextSize { get; set; } = "Default";

        public SettingsView()
        {
            InitializeComponent();
        }

        private void CurrentMasterPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModels.SettingsViewModel vm)
                vm.CurrentMasterPasswordInput = CurrentMasterPasswordBox.Password;
        }

        private void NewMasterPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModels.SettingsViewModel vm)
                vm.NewMasterPasswordInput = NewMasterPasswordBox.Password;
        }

        private void ConfirmMasterPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModels.SettingsViewModel vm)
                vm.ConfirmMasterPasswordInput = ConfirmMasterPasswordBox.Password;
        }
    }
}