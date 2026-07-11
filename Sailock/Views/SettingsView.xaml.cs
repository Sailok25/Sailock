using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Sailock.ViewModels;

namespace Sailock.Views
{
    public partial class SettingsView : UserControl
    {
        public static List<string> TextSizeOptions { get; } =
            new List<string> { "Small", "Default", "Large" };

        public static List<string> LanguageOptions { get; } =
            new List<string> { "English", "Español", "Deutsch", "Français" };

        public static List<string> AutoLockOptions { get; } =
            new List<string> { "Disabled", "30 sec", "1 min", "2 min", "5 min" };

        public string SelectedTextSize { get; set; } = "Default";

        public SettingsView()
        {
            InitializeComponent();
        }

        private void CurrentMasterPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is SettingsViewModel vm)
                vm.CurrentMasterPasswordInput = ((PasswordBox)sender).Password;
        }

        private void NewMasterPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is SettingsViewModel vm)
                vm.NewMasterPasswordInput = ((PasswordBox)sender).Password;
        }

        private void ConfirmMasterPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is SettingsViewModel vm)
                vm.ConfirmMasterPasswordInput = ((PasswordBox)sender).Password;
        }
    }
}