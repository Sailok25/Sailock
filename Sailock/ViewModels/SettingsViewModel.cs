using System;
using System.Windows;
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
            set
            {
                SetProperty(ref _autoLockEnabled, value);
                OnAutoLockChanged?.Invoke(value);
                PersistSettings();
            }
        }

        private bool _isDarkTheme;
        public bool IsDarkTheme
        {
            get => _isDarkTheme;
            set
            {
                SetProperty(ref _isDarkTheme, value);
                ThemeService.ApplyTheme(value);
                OnThemeChanged?.Invoke(value);
                PersistSettings();
            }
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
            set
            {
                SetProperty(ref _selectedLanguage, value);
                LocalizationService.ApplyLanguage(value);
                PersistSettings();
            }
        }

        private string _selectedTextSize;
        public string SelectedTextSize
        {
            get => _selectedTextSize;
            set
            {
                SetProperty(ref _selectedTextSize, value);
                ThemeService.ApplyTextSize(value);
                PersistSettings();
            }
        }

        private bool _isDeleteModalOpen;
        public bool IsDeleteModalOpen
        {
            get => _isDeleteModalOpen;
            set => SetProperty(ref _isDeleteModalOpen, value);
        }

        private bool _isDisable2FAModalOpen;
        public bool IsDisable2FAModalOpen
        {
            get => _isDisable2FAModalOpen;
            set => SetProperty(ref _isDisable2FAModalOpen, value);
        }

        private bool _isChangeMasterPasswordModalOpen;
        public bool IsChangeMasterPasswordModalOpen
        {
            get => _isChangeMasterPasswordModalOpen;
            set => SetProperty(ref _isChangeMasterPasswordModalOpen, value);
        }

        private string _currentMasterPasswordInput;
        public string CurrentMasterPasswordInput
        {
            get => _currentMasterPasswordInput;
            set => SetProperty(ref _currentMasterPasswordInput, value);
        }

        private string _newMasterPasswordInput;
        public string NewMasterPasswordInput
        {
            get => _newMasterPasswordInput;
            set => SetProperty(ref _newMasterPasswordInput, value);
        }

        private string _confirmMasterPasswordInput;
        public string ConfirmMasterPasswordInput
        {
            get => _confirmMasterPasswordInput;
            set => SetProperty(ref _confirmMasterPasswordInput, value);
        }

        private string _masterPasswordErrorMessage;
        public string MasterPasswordErrorMessage
        {
            get => _masterPasswordErrorMessage;
            set => SetProperty(ref _masterPasswordErrorMessage, value);
        }

        private string _statusMessage;
        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        public Action? OnDataImported { get; set; }
        public Action<SetupTotpViewModel>? OnOpen2FASetup { get; set; }
        public Action<bool>? OnThemeChanged { get; set; }
        public Action<bool>? OnAutoLockChanged { get; set; }

        public ICommand Enable2FACommand { get; }
        public ICommand ChangeMasterPassCommand { get; }
        public ICommand ToggleAutoLockCommand { get; }
        public ICommand ImportCommand { get; }
        public ICommand ExportCommand { get; }
        public ICommand OpenDeleteModalCommand { get; }
        public ICommand ConfirmDeleteCommand { get; }
        public ICommand CancelDeleteCommand { get; }
        public ICommand ConfirmDisable2FACommand { get; }
        public ICommand CancelDisable2FACommand { get; }
        public ICommand OpenChangeMasterPasswordCommand { get; }
        public ICommand ConfirmChangeMasterPasswordCommand { get; }
        public ICommand CancelChangeMasterPasswordCommand { get; }

        public SettingsViewModel(AppData appData, StorageService storage, string masterPassword)
        {
            _appData = appData;
            _storage = storage;
            _masterPassword = masterPassword;

            _is2FAEnabled = _appData.Settings.Is2FAEnabled;
            _autoLockEnabled = _appData.Settings.AutoLockEnabled;
            _isDarkTheme = _appData.Settings.IsDarkTheme;
            _isHighContrast = _appData.Settings.IsHighContrast;
            _selectedLanguage = _appData.Settings.Language;
            _selectedTextSize = _appData.Settings.TextSize;

            ThemeService.ApplyTheme(_isDarkTheme);
            ThemeService.ApplyContrast(_isHighContrast);
            ThemeService.ApplyTextSize(_selectedTextSize);
            LocalizationService.ApplyLanguage(_selectedLanguage);

            ConfirmDisable2FACommand = new RelayCommand(_ =>
            {
                Is2FAEnabled = false;
                _appData.Settings.Is2FAEnabled = false;
                _appData.Settings.TotpSecret = null;
                IsDisable2FAModalOpen = false;
                PersistSettings();
            });

            CancelDisable2FACommand = new RelayCommand(_ => IsDisable2FAModalOpen = false);

            Enable2FACommand = new RelayCommand(_ =>
            {
                if (Is2FAEnabled)
                {
                    IsDisable2FAModalOpen = true;
                }
                else
                {
                    var setupVM = new SetupTotpViewModel(_appData, _storage, _masterPassword);
                    setupVM.OnSetupComplete = () =>
                    {
                        Is2FAEnabled = true;
                        StatusMessage = "2FA enabled successfully.";
                    };
                    setupVM.OnCancelled = () => { };
                    OnOpen2FASetup?.Invoke(setupVM);
                }
            });

            ChangeMasterPassCommand = new RelayCommand(_ => OpenChangeMasterPasswordModal());
            ToggleAutoLockCommand = new RelayCommand(_ => AutoLockEnabled = !AutoLockEnabled);
            ImportCommand = new RelayCommand(_ => Import());
            ExportCommand = new RelayCommand(_ => Export());
            OpenDeleteModalCommand = new RelayCommand(_ => IsDeleteModalOpen = true);
            ConfirmDeleteCommand = new RelayCommand(_ => DeleteAllData());
            CancelDeleteCommand = new RelayCommand(_ => IsDeleteModalOpen = false);

            OpenChangeMasterPasswordCommand = new RelayCommand(_ => OpenChangeMasterPasswordModal());
            ConfirmChangeMasterPasswordCommand = new RelayCommand(_ => ConfirmChangeMasterPassword());
            CancelChangeMasterPasswordCommand = new RelayCommand(_ => CancelChangeMasterPassword());
        }

        private void OpenChangeMasterPasswordModal()
        {
            CurrentMasterPasswordInput = null;
            NewMasterPasswordInput = null;
            ConfirmMasterPasswordInput = null;
            MasterPasswordErrorMessage = null;
            IsChangeMasterPasswordModalOpen = true;
        }

        private void ConfirmChangeMasterPassword()
        {
            // Validar que la contraseña actual sea correcta
            if (CurrentMasterPasswordInput != _masterPassword)
            {
                MasterPasswordErrorMessage = "Current password is incorrect.";
                return;
            }

            // Validar que las nuevas contraseñas sean iguales
            if (NewMasterPasswordInput != ConfirmMasterPasswordInput)
            {
                MasterPasswordErrorMessage = "New passwords do not match.";
                return;
            }

            // Validar que la nueva contraseña no esté vacía
            if (string.IsNullOrWhiteSpace(NewMasterPasswordInput))
            {
                MasterPasswordErrorMessage = "New password cannot be empty.";
                return;
            }

            // Validar que la nueva contraseña sea diferente a la actual
            if (NewMasterPasswordInput == _masterPassword)
            {
                MasterPasswordErrorMessage = "New password must be different from the current one.";
                return;
            }

            try
            {
                // Re-cifrar todos los datos con la nueva contraseña
                _storage.Save(_appData, NewMasterPasswordInput);

                // Actualizar la contraseña maestra en memoria (si fuera necesario, se propagaría a MainViewModel)
                StatusMessage = "Master password changed successfully.";
                IsChangeMasterPasswordModalOpen = false;

                // Limpiar campos
                CurrentMasterPasswordInput = null;
                NewMasterPasswordInput = null;
                ConfirmMasterPasswordInput = null;
                MasterPasswordErrorMessage = null;
            }
            catch (Exception ex)
            {
                MasterPasswordErrorMessage = "Error changing password: " + ex.Message;
            }
        }

        private void CancelChangeMasterPassword()
        {
            IsChangeMasterPasswordModalOpen = false;
            CurrentMasterPasswordInput = null;
            NewMasterPasswordInput = null;
            ConfirmMasterPasswordInput = null;
            MasterPasswordErrorMessage = null;
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
                StatusMessage = "Error: password does not match the file.";
                return;
            }

            _appData.Entries.Clear();
            _appData.Entries.AddRange(imported.Entries);
            _storage.Save(_appData, _masterPassword);
            StatusMessage = $"{imported.Entries.Count} entries imported successfully.";
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
            StatusMessage = "Exported successfully.";
        }

        private void DeleteAllData()
        {
            _storage.DeleteAll();
            IsDeleteModalOpen = false;
            Application.Current.Shutdown();
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