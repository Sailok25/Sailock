using System;
using System.Windows.Input;
using Sailock.Helpers;
using Sailock.Models;
using Sailock.Services;

namespace Sailock.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly StorageService _storage;
        private readonly TotpService _totp = new TotpService();

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _totpCode;
        public string TotpCode
        {
            get => _totpCode;
            set => SetProperty(ref _totpCode, value);
        }

        private bool _loginFailed;
        public bool LoginFailed
        {
            get => _loginFailed;
            set => SetProperty(ref _loginFailed, value);
        }

        private bool _isFirstRun;
        public bool IsFirstRun
        {
            get => _isFirstRun;
            set => SetProperty(ref _isFirstRun, value);
        }

        // Paso 1: contraseña correcta, esperando TOTP
        private bool _awaitingTotp;
        public bool AwaitingTotp
        {
            get => _awaitingTotp;
            set => SetProperty(ref _awaitingTotp, value);
        }

        private AppData _loadedData;

        public Action<string, AppData> OnLoginSuccess { get; set; }

        public ICommand LoginCommand { get; }
        public ICommand VerifyTotpCommand { get; }

        public LoginViewModel(StorageService storage)
        {
            _storage = storage;
            IsFirstRun = !_storage.DataFileExists();
            LoginCommand = new RelayCommand(_ => TryLogin());
            VerifyTotpCommand = new RelayCommand(_ => VerifyTotp());
        }

        public void TryLogin()
        {
            if (string.IsNullOrEmpty(Password))
            {
                LoginFailed = true;
                return;
            }

            if (IsFirstRun)
            {
                var newData = new AppData();
                _storage.Save(newData, Password);
                LoginFailed = false;
                OnLoginSuccess?.Invoke(Password, newData);
                return;
            }

            var data = _storage.Load(Password);

            if (data == null)
            {
                LoginFailed = true;
                return;
            }

            LoginFailed = false;

            // Comprobar si 2FA está activado
            if (data.Settings.Is2FAEnabled &&
                !string.IsNullOrEmpty(data.Settings.TotpSecret))
            {
                _loadedData = data;
                AwaitingTotp = true; // mostrar segundo paso
            }
            else
            {
                OnLoginSuccess?.Invoke(Password, data);
            }
        }

        public void VerifyTotp()
        {
            if (_loadedData == null) return;

            if (!_totp.Validate(_loadedData.Settings.TotpSecret, TotpCode))
            {
                LoginFailed = true;
                return;
            }

            LoginFailed = false;
            OnLoginSuccess?.Invoke(Password, _loadedData);
        }
    }
}