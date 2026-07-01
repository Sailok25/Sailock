using System;
using System.Collections.Generic;
using System.Windows.Input;
using Sailock.Helpers;
using Sailock.Models;
using Sailock.Services;

namespace Sailock.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly StorageService _storage;
        private string _masterPassword;
        private readonly AppData _appData;

        private bool _is2FAEnabled;
        public bool Is2FAEnabled
        {
            get => _is2FAEnabled;
            set => SetProperty(ref _is2FAEnabled, value);
        }

        private string _selectedAutoLockTimeout;
        public string SelectedAutoLockTimeout
        {
            get => _selectedAutoLockTimeout;
            set
            {
                SetProperty(ref _selectedAutoLockTimeout, value);
                bool enabled = value != "Disabled";
                OnAutoLockChanged?.Invoke(enabled, value);
                PersistSettings();
            }
        }

        public bool AutoLockEnabled => _selectedAutoLockTimeout != "Disabled";

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
                OnLanguageChanged?.Invoke();
            }
        }

        public Action OnLanguageChanged { get; set; }

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

        private string _statusMessage;
        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        // --- Change Master Password ---
        private bool _isChangeMasterPasswordModalOpen;
        public bool IsChangeMasterPasswordModalOpen
        {
            get => _isChangeMasterPasswordModalOpen;
            set => SetProperty(ref _isChangeMasterPasswordModalOpen, value);
        }

        private string _masterPasswordErrorMessage;
        public string MasterPasswordErrorMessage
        {
            get => _masterPasswordErrorMessage;
            set => SetProperty(ref _masterPasswordErrorMessage, value);
        }

        public string CurrentMasterPasswordInput { get; set; }
        public string NewMasterPasswordInput { get; set; }
        public string ConfirmMasterPasswordInput { get; set; }

        // --- Re-login required modal ---
        private bool _isReLoginModalOpen;
        public bool IsReLoginModalOpen
        {
            get => _isReLoginModalOpen;
            set => SetProperty(ref _isReLoginModalOpen, value);
        }

        public Action? OnDataImported { get; set; }
        public Action<SetupTotpViewModel>? OnOpen2FASetup { get; set; }
        public Action<bool>? OnThemeChanged { get; set; }
        public Action<bool, string>? OnAutoLockChanged { get; set; }
        public Action<string>? OnMasterPasswordChanged { get; set; }

        /// <summary>
        /// This action is provided by the host (MainViewModel) so SettingsViewModel can request a logout/relogin.
        /// </summary>
        public Action? OnRequestLogout { get; set; }

        public ICommand Enable2FACommand { get; }
        public ICommand ChangeMasterPassCommand { get; }
        public ICommand OpenChangeMasterPasswordCommand { get; }
        public ICommand ConfirmChangeMasterPasswordCommand { get; }
        public ICommand CancelChangeMasterPasswordCommand { get; }
        public ICommand ImportCommand { get; }
        public ICommand ExportCommand { get; }
        public ICommand OpenDeleteModalCommand { get; }
        public ICommand ConfirmDeleteCommand { get; }
        public ICommand CancelDeleteCommand { get; }
        public ICommand ConfirmDisable2FACommand { get; }
        public ICommand CancelDisable2FACommand { get; }

        // Command to accept and logout on re-login modal
        public ICommand AcceptReLoginCommand { get; }

        public SettingsViewModel(AppData appData, StorageService storage, string masterPassword)
        {
            _appData = appData;
            _storage = storage;
            _masterPassword = masterPassword;

            _is2FAEnabled = _appData.Settings.Is2FAEnabled;
            _isDarkTheme = _appData.Settings.IsDarkTheme;
            _isHighContrast = _appData.Settings.IsHighContrast;
            _selectedLanguage = _appData.Settings.Language;
            _selectedTextSize = _appData.Settings.TextSize;

            if (!string.IsNullOrEmpty(_appData.Settings.AutoLockTimeout))
                _selectedAutoLockTimeout = _appData.Settings.AutoLockTimeout;
            else
                _selectedAutoLockTimeout = _appData.Settings.AutoLockEnabled ? "2 min" : "Disabled";

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

            OpenChangeMasterPasswordCommand = new RelayCommand(_ =>
            {
                CurrentMasterPasswordInput = string.Empty;
                NewMasterPasswordInput = string.Empty;
                ConfirmMasterPasswordInput = string.Empty;
                MasterPasswordErrorMessage = null;
                IsChangeMasterPasswordModalOpen = true;
            });

            CancelChangeMasterPasswordCommand = new RelayCommand(_ =>
            {
                IsChangeMasterPasswordModalOpen = false;
            });

            ConfirmChangeMasterPasswordCommand = new RelayCommand(_ => ChangeMasterPassword());

            ChangeMasterPassCommand = new RelayCommand(_ => ChangeMasterPassword());
            ImportCommand = new RelayCommand(_ => Import());
            ExportCommand = new RelayCommand(_ => Export());
            OpenDeleteModalCommand = new RelayCommand(_ => IsDeleteModalOpen = true);
            ConfirmDeleteCommand = new RelayCommand(_ => DeleteAllData());
            CancelDeleteCommand = new RelayCommand(_ => IsDeleteModalOpen = false);

            AcceptReLoginCommand = new RelayCommand(_ =>
            {
                // User clicked accept on re-login modal — logout now
                OnRequestLogout?.Invoke();
            });
        }

        private void ChangeMasterPassword()
        {
            if (string.IsNullOrEmpty(CurrentMasterPasswordInput) ||
                CurrentMasterPasswordInput != _masterPassword)
            {
                MasterPasswordErrorMessage = "Current password is incorrect.";
                return;
            }

            if (string.IsNullOrWhiteSpace(NewMasterPasswordInput) || NewMasterPasswordInput.Length < 4)
            {
                MasterPasswordErrorMessage = "New password is too short.";
                return;
            }

            if (NewMasterPasswordInput != ConfirmMasterPasswordInput)
            {
                MasterPasswordErrorMessage = "New passwords do not match.";
                return;
            }

            // Persist using the new password (re-encrypt vault)
            _storage.Save(_appData, NewMasterPasswordInput);

            // Update local stored master password inside this VM so further operations here (if any) use the new pass
            _masterPassword = NewMasterPasswordInput;

            MasterPasswordErrorMessage = null;
            IsChangeMasterPasswordModalOpen = false;

            // Show re-login required modal
            IsReLoginModalOpen = true;
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
            System.Windows.Application.Current.Shutdown();
        }

        private void PersistSettings()
        {
            _appData.Settings.Is2FAEnabled = _is2FAEnabled;
            _appData.Settings.AutoLockEnabled = _selectedAutoLockTimeout != "Disabled";
            _appData.Settings.AutoLockTimeout = _selectedAutoLockTimeout;
            _appData.Settings.IsDarkTheme = _isDarkTheme;
            _appData.Settings.IsHighContrast = _isHighContrast;
            _appData.Settings.Language = _selectedLanguage;
            _appData.Settings.TextSize = _selectedTextSize;
            _storage.Save(_appData, _masterPassword);
        }
    }
}