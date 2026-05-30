using Sailock.Helpers;
using Sailock.Models;
using Sailock.Services;
using System.Windows;
using System.Windows.Input;

namespace Sailock.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly StorageService _storage = new StorageService();

        // Contraseña maestra activa durante la sesión (en memoria, nunca en disco)
        private string _masterPassword;

        // Datos cargados en memoria
        private AppData _appData;

        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set
            {
                SetProperty(ref _currentView, value);
                OnPropertyChanged(nameof(SidebarVisibility));
            }
        }

        public Visibility SidebarVisibility =>
            CurrentView is LoginViewModel ? Visibility.Collapsed : Visibility.Visible;

        public ICommand NavigateDashboardCommand { get; }
        public ICommand NavigateGeneratorCommand { get; }
        public ICommand NavigateSettingsCommand { get; }
        public ICommand LogoutCommand { get; }

        private DashboardViewModel _dashboardVM;

        public MainViewModel()
        {
            NavigateDashboardCommand = new RelayCommand(_ => ShowDashboard());
            NavigateGeneratorCommand = new RelayCommand(_ => ShowGenerator());
            NavigateSettingsCommand = new RelayCommand(_ => ShowSettings());
            LogoutCommand = new RelayCommand(_ => Logout());

            ShowLogin();
        }

        private void ShowLogin()
        {
            _masterPassword = null;
            _appData = null;
            _dashboardVM = null;

            var loginVM = new LoginViewModel(_storage);
            loginVM.OnLoginSuccess = OnLoginSuccess;
            CurrentView = loginVM;
        }

        private void OnLoginSuccess(string password, AppData data)
        {
            _masterPassword = password;
            _appData = data;
            ShowDashboard();
        }

        private void ShowDashboard()
        {
            _dashboardVM ??= new DashboardViewModel(_appData, _storage, _masterPassword);
            CurrentView = _dashboardVM;
        }

        private void ShowGenerator()
        {
            CurrentView = new GeneratorViewModel();
        }

        private void ShowSettings()
        {
            var settingsVM = new SettingsViewModel(_appData, _storage, _masterPassword);

            settingsVM.OnDataImported = () => _dashboardVM = null;

            settingsVM.OnOpen2FASetup = setupVM =>
            {
                var previousSettings = CurrentView;

                setupVM.OnSetupComplete = () =>
                {
                    settingsVM.Is2FAEnabled = true;
                    settingsVM.StatusMessage = "2FA activado correctamente.";
                    CurrentView = previousSettings;
                };

                setupVM.OnCancelled = () => CurrentView = previousSettings;
                CurrentView = setupVM;
            };

            CurrentView = settingsVM;
        }

        private void Logout()
        {
            _dashboardVM = null;
            ShowLogin();
        }
    }
}