using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Sailock.ViewModels;

namespace Sailock.Views
{
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void PasswordInput_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel vm)
                vm.Password = PasswordInput.Password;
        }

        private void PasswordInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                ExecuteLogin();
        }

        private void TotpInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (DataContext is LoginViewModel vm)
                    vm.VerifyTotp();
            }
        }

        private void ExecuteLogin()
        {
            if (DataContext is not LoginViewModel vm) return;
            vm.TryLogin();
            if (vm.LoginFailed)
                ShowErrorTemporarily();
        }

        private void ShowErrorTemporarily()
        {
            ErrorText.Visibility = System.Windows.Visibility.Visible;

            var timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(2)
            };

            timer.Tick += (s, e) =>
            {
                ErrorText.Visibility = System.Windows.Visibility.Collapsed;
                timer.Stop();
            };

            timer.Start();
        }
    }
}