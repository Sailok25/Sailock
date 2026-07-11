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

        // Import mode combobox (step 2)
        private void ImportModeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is not SettingsViewModel vm) return;
            if (sender is not ComboBox cb) return;
            if (cb.SelectedIndex < 0) return;

            vm.SelectedImportMode = cb.SelectedIndex switch
            {
                0 => ImportMode.Merge,
                1 => ImportMode.ReplaceAll,
            };
        }

        // Duplicate strategy combobox (step 3)
        private void DuplicateModeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is not SettingsViewModel vm) return;
            if (sender is not ComboBox cb) return;
            if (cb.SelectedIndex < 0) return;

            vm.SelectedDuplicateStrategy = cb.SelectedIndex switch
            {
                0 => DuplicateStrategy.KeepExisting,
                1 => DuplicateStrategy.OverwriteExisting,
                _ => DuplicateStrategy.RenameImported
            };
        }
    }
}