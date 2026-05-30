using System;
using System.Windows.Input;
using Sailock.Helpers;
using Sailock.Models;
using Sailock.Services;

namespace Sailock.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly StorageService _storage;
        private readonly string _masterPassword;
        private readonly AppData _appData;

        private bool _is2FAEnabled;
        public bool Is2FAEnabled
        {
            get => _is2FAEnabled;
            set => SetProperty(ref _is2FAEnabled, value);
        }

        private bool _autoLockEnabled;
        public bool AutoLockEnabled
        {
            get => _autoLockEnabled;
            set { SetProperty(ref _autoLockEnabled, value); PersistSettings(); }
        }

        private bool _isDarkTheme;
        public bool IsDarkTheme
        {
            get => _isDarkTheme;
            set { SetProperty(ref _isDarkTheme, value); PersistSettings(); }
        }

        private bool _isHighContrast;
        public bool IsHighContrast
        {
            get => _isHighContrast;
            set
            {
                SetProperty(ref _isHighContrast, value);
                ThemeService.ApplyContrast(value);
                PersistSettings();
            }
        }

        private string _selectedLanguage;
        public string SelectedLanguage
        {
            get => _selectedLanguage;
            set { SetProperty(ref _selectedLanguage, value); PersistSettings(); }
        }

        private string _selectedTextSize;
        public string SelectedTextSize
        {
            get => _selectedTextSize;
            set { SetProperty(ref _selectedTextSize, value); PersistSettings(); }
        }

        private bool _isDeleteModalOpen;
        public bool IsDeleteModalOpen
        {
            get => _isDeleteModalOpen;
            set => SetProperty(ref _isDeleteModalOpen, value);
        }

        private string _statusMessage;
        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        // Callbacks hacia MainViewModel
        public Action OnDataImported { get; set; }
        public Action<SetupTotpViewModel> OnOpen2FASetup { get; set; }

        public ICommand Enable2FACommand { get; }
        public ICommand ChangeMasterPassCommand { get; }
        public ICommand ToggleAutoLockCommand { get; }
        public ICommand ImportCommand { get; }
        public ICommand ExportCommand { get; }
        public ICommand OpenDeleteModalCommand { get; }
        public ICommand ConfirmDeleteCommand { get; }
        public ICommand CancelDeleteCommand { get; }

        public SettingsViewModel(AppData appData, StorageService storage, string masterPassword)
        {
            _appData = appData;
            _storage = storage;
            _masterPassword = masterPassword;

            // Cargar valores desde AppData
            _is2FAEnabled = _appData.Settings.Is2FAEnabled;
            _autoLockEnabled = _appData.Settings.AutoLockEnabled;
            _isDarkTheme = _appData.Settings.IsDarkTheme;
            _isHighContrast = _appData.Settings.IsHighContrast;
            _selectedLanguage = _appData.Settings.Language;
            _selectedTextSize = _appData.Settings.TextSize;

            // Aplicar contraste guardado al arrancar
            ThemeService.ApplyContrast(_isHighContrast);

            Enable2FACommand = new RelayCommand(_ =>
            {
                if (Is2FAEnabled)
                {
                    // Desactivar 2FA
                    Is2FAEnabled = false;
                    _appData.Settings.Is2FAEnabled = false;
                    _appData.Settings.TotpSecret = null;
                    PersistSettings();
                }
                else
                {
                    // Abrir flujo de activación
                    var setupVM = new SetupTotpViewModel(_appData, _storage, _masterPassword);
                    setupVM.OnSetupComplete = () =>
                    {
                        Is2FAEnabled = true;
                        StatusMessage = "2FA activado correctamente.";
                    };
                    setupVM.OnCancelled = () => { };
                    OnOpen2FASetup?.Invoke(setupVM);
                }
            });

            ChangeMasterPassCommand = new RelayCommand(_ => ChangeMasterPassword());
            ToggleAutoLockCommand = new RelayCommand(_ => AutoLockEnabled = !AutoLockEnabled);
            ImportCommand = new RelayCommand(_ => Import());
            ExportCommand = new RelayCommand(_ => Export());
            OpenDeleteModalCommand = new RelayCommand(_ => IsDeleteModalOpen = true);
            ConfirmDeleteCommand = new RelayCommand(_ => DeleteAllData());
            CancelDeleteCommand = new RelayCommand(_ => IsDeleteModalOpen = false);
        }

        private void ChangeMasterPassword()
        {
            // Implementación futura
        }

        private void Import()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Sailock files (*.slock)|*.slock",
                Title = "Import Sailock file"
            };

            if (dialog.ShowDialog() != true) return;

            var imported = _storage.Import(dialog.FileName, _masterPassword);

            if (imported == null)
            {
                StatusMessage = "Error: la contraseña no coincide con el archivo.";
                return;
            }

            _appData.Entries.Clear();
            _appData.Entries.AddRange(imported.Entries);
            _storage.Save(_appData, _masterPassword);
            StatusMessage = $"Importadas {imported.Entries.Count} entradas correctamente.";
            OnDataImported?.Invoke();
        }

        private void Export()
        {
            var dialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "Sailock files (*.slock)|*.slock",
                FileName = "sailock_backup",
                Title = "Export Sailock file"
            };

            if (dialog.ShowDialog() != true) return;

            _storage.Export(_appData, dialog.FileName, _masterPassword);
            StatusMessage = "Exportado correctamente.";
        }

        private void DeleteAllData()
        {
            _storage.DeleteAll();
            IsDeleteModalOpen = false;
            System.Windows.Application.Current.Shutdown();
        }

        private void PersistSettings()
        {
            _appData.Settings.Is2FAEnabled = _is2FAEnabled;
            _appData.Settings.AutoLockEnabled = _autoLockEnabled;
            _appData.Settings.IsDarkTheme = _isDarkTheme;
            _appData.Settings.IsHighContrast = _isHighContrast;
            _appData.Settings.Language = _selectedLanguage;
            _appData.Settings.TextSize = _selectedTextSize;
            _storage.Save(_appData, _masterPassword);
        }
    }
}