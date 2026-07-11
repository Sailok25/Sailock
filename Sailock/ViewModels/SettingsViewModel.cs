using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Sailock.Helpers;
using Sailock.Models;
using Sailock.Services;

namespace Sailock.ViewModels
{
    public enum ImportMode
    {
        Merge,
        ReplaceAll,
    }

    public enum DuplicateStrategy
    {
        KeepExisting,
        OverwriteExisting,
        RenameImported
    }

    public class ImportResult
    {
        public int Added { get; set; }
        public int Updated { get; set; }
        public int Skipped { get; set; }
        public int Errors { get; set; }
    }

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
                OnPropertyChanged(nameof(AutoLockEnabled));
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

        // ===== IMPORT WIZARD =====
        private bool _isImportWizardOpen;
        public bool IsImportWizardOpen
        {
            get => _isImportWizardOpen;
            set => SetProperty(ref _isImportWizardOpen, value);
        }

        private int _importWizardStep = 1;
        public int ImportWizardStep
        {
            get => _importWizardStep;
            set
            {
                if (SetProperty(ref _importWizardStep, value))
                {
                    OnPropertyChanged(nameof(IsImportStep1));
                    OnPropertyChanged(nameof(IsImportStep2));
                    OnPropertyChanged(nameof(IsImportStep3));
                    OnPropertyChanged(nameof(IsImportStep4));
                    OnPropertyChanged(nameof(IsImportStep5));
                }
            }
        }

        public bool IsImportStep1 => ImportWizardStep == 1;
        public bool IsImportStep2 => ImportWizardStep == 2;
        public bool IsImportStep3 => ImportWizardStep == 3;
        public bool IsImportStep4 => ImportWizardStep == 4;
        public bool IsImportStep5 => ImportWizardStep == 5;

        private string _selectedImportFilePath;
        public string SelectedImportFilePath
        {
            get => _selectedImportFilePath;
            set => SetProperty(ref _selectedImportFilePath, value);
        }

        private string _selectedImportFileName;
        public string SelectedImportFileName
        {
            get => _selectedImportFileName;
            set => SetProperty(ref _selectedImportFileName, value);
        }

        private long _selectedImportFileSizeBytes;
        public long SelectedImportFileSizeBytes
        {
            get => _selectedImportFileSizeBytes;
            set
            {
                if (SetProperty(ref _selectedImportFileSizeBytes, value))
                    OnPropertyChanged(nameof(SelectedImportFileSizeLabel));
            }
        }

        public string SelectedImportFileSizeLabel
        {
            get
            {
                if (SelectedImportFileSizeBytes <= 0) return "0 KB";
                double kb = SelectedImportFileSizeBytes / 1024.0;
                if (kb < 1024) return $"{kb:0.#} KB";
                double mb = kb / 1024.0;
                return $"{mb:0.##} MB";
            }
        }

        private ImportMode _selectedImportMode = ImportMode.Merge;
        public ImportMode SelectedImportMode
        {
            get => _selectedImportMode;
            set => SetProperty(ref _selectedImportMode, value);
        }

        private DuplicateStrategy _selectedDuplicateStrategy = DuplicateStrategy.KeepExisting;
        public DuplicateStrategy SelectedDuplicateStrategy
        {
            get => _selectedDuplicateStrategy;
            set => SetProperty(ref _selectedDuplicateStrategy, value);
        }

        private int _importPreviewNewCount;
        public int ImportPreviewNewCount
        {
            get => _importPreviewNewCount;
            set => SetProperty(ref _importPreviewNewCount, value);
        }

        private int _importPreviewDuplicatesCount;
        public int ImportPreviewDuplicatesCount
        {
            get => _importPreviewDuplicatesCount;
            set => SetProperty(ref _importPreviewDuplicatesCount, value);
        }

        private ImportResult _lastImportResult = new ImportResult();
        public ImportResult LastImportResult
        {
            get => _lastImportResult;
            set => SetProperty(ref _lastImportResult, value);
        }

        public string SelectedImportModeLabel =>
            SelectedImportMode switch
            {
                ImportMode.Merge => "Merge",
                ImportMode.ReplaceAll => "Replace all",
                _ => "Merge"
            };

        public string SelectedDuplicateStrategyLabel =>
            SelectedDuplicateStrategy switch
            {
                DuplicateStrategy.KeepExisting => "Keep existing",
                DuplicateStrategy.OverwriteExisting => "Overwrite existing",
                DuplicateStrategy.RenameImported => "Rename imported",
                _ => "Keep existing"
            };

        // ===== callbacks =====
        public Action? OnDataImported { get; set; }
        public Action<SetupTotpViewModel>? OnOpen2FASetup { get; set; }
        public Action<bool>? OnThemeChanged { get; set; }
        public Action<bool, string>? OnAutoLockChanged { get; set; }
        public Action<string>? OnMasterPasswordChanged { get; set; }

        /// <summary>
        /// This action is provided by the host (MainViewModel) so SettingsViewModel can request a logout/relogin.
        /// </summary>
        public Action? OnRequestLogout { get; set; }

        // ===== commands =====
        public ICommand Enable2FACommand { get; }
        public ICommand ChangeMasterPassCommand { get; }
        public ICommand OpenChangeMasterPasswordCommand { get; }
        public ICommand ConfirmChangeMasterPasswordCommand { get; }
        public ICommand CancelChangeMasterPasswordCommand { get; }

        // Import Wizard commands
        public ICommand ImportCommand { get; }
        public ICommand CancelImportWizardCommand { get; }
        public ICommand NextImportWizardStepCommand { get; }
        public ICommand BackImportWizardStepCommand { get; }
        public ICommand ConfirmImportWizardCommand { get; }
        public ICommand FinishImportWizardCommand { get; }

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

            ImportCommand = new RelayCommand(_ => StartImportWizard());
            CancelImportWizardCommand = new RelayCommand(_ => CloseImportWizard());
            NextImportWizardStepCommand = new RelayCommand(_ => NextImportWizardStep());
            BackImportWizardStepCommand = new RelayCommand(_ => BackImportWizardStep());
            ConfirmImportWizardCommand = new RelayCommand(_ => ConfirmImportWizard());
            FinishImportWizardCommand = new RelayCommand(_ => CloseImportWizard());

            ExportCommand = new RelayCommand(_ => Export());
            OpenDeleteModalCommand = new RelayCommand(_ => IsDeleteModalOpen = true);
            ConfirmDeleteCommand = new RelayCommand(_ => DeleteAllData());
            CancelDeleteCommand = new RelayCommand(_ => IsDeleteModalOpen = false);

            AcceptReLoginCommand = new RelayCommand(_ =>
            {
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

            _storage.Save(_appData, NewMasterPasswordInput);
            _masterPassword = NewMasterPasswordInput;

            MasterPasswordErrorMessage = null;
            IsChangeMasterPasswordModalOpen = false;
            IsReLoginModalOpen = true;
        }

        // ===== IMPORT WIZARD FLOW =====
        private void StartImportWizard()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Sailock files (*.slock)|*.slock",
                Title = "Import Sailock file"
            };

            if (dialog.ShowDialog() != true) return;

            var info = new System.IO.FileInfo(dialog.FileName);

            SelectedImportFilePath = dialog.FileName;
            SelectedImportFileName = info.Name;
            SelectedImportFileSizeBytes = info.Exists ? info.Length : 0;

            SelectedImportMode = ImportMode.Merge;
            SelectedDuplicateStrategy = DuplicateStrategy.KeepExisting;
            ImportWizardStep = 1;
            LastImportResult = new ImportResult();

            BuildImportPreview();

            IsImportWizardOpen = true;
        }

        private void CloseImportWizard()
        {
            IsImportWizardOpen = false;
            ImportWizardStep = 1;
        }

        private void NextImportWizardStep()
        {
            if (ImportWizardStep == 1)
            {
                ImportWizardStep = 2;
                return;
            }

            if (ImportWizardStep == 2)
            {
                // Si es ReplaceAll, saltamos duplicados
                ImportWizardStep = SelectedImportMode == ImportMode.ReplaceAll ? 4 : 3;
                return;
            }

            if (ImportWizardStep == 3)
            {
                ImportWizardStep = 4;
                return;
            }

            if (ImportWizardStep < 5)
                ImportWizardStep++;
        }

        private void BackImportWizardStep()
        {
            if (ImportWizardStep == 4 && SelectedImportMode == ImportMode.ReplaceAll)
            {
                ImportWizardStep = 2;
                return;
            }

            if (ImportWizardStep > 1)
                ImportWizardStep--;
        }

        private void BuildImportPreview()
        {
            ImportPreviewNewCount = 0;
            ImportPreviewDuplicatesCount = 0;

            var imported = _storage.Import(SelectedImportFilePath, _masterPassword);
            if (imported == null)
            {
                StatusMessage = "Error: password does not match the file.";
                return;
            }

            foreach (var incoming in imported.Entries)
            {
                bool duplicate = _appData.Entries.Any(e =>
                    string.Equals(e.Title?.Trim(), incoming.Title?.Trim(), StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(e.Username?.Trim(), incoming.Username?.Trim(), StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(e.Email?.Trim(), incoming.Email?.Trim(), StringComparison.OrdinalIgnoreCase));

                if (duplicate) ImportPreviewDuplicatesCount++;
                else ImportPreviewNewCount++;
            }
        }

        private void ConfirmImportWizard()
        {
            var imported = _storage.Import(SelectedImportFilePath, _masterPassword);
            if (imported == null)
            {
                StatusMessage = "Error: password does not match the file.";
                return;
            }

            var result = ApplyImport(imported, SelectedImportMode, SelectedDuplicateStrategy);
            LastImportResult = result;

            _storage.Save(_appData, _masterPassword);
            OnDataImported?.Invoke();

            StatusMessage = $"Import complete. Added: {result.Added}, Updated: {result.Updated}, Skipped: {result.Skipped}, Errors: {result.Errors}";

            OnPropertyChanged(nameof(SelectedImportModeLabel));
            OnPropertyChanged(nameof(SelectedDuplicateStrategyLabel));

            ImportWizardStep = 5;
        }

        private ImportResult ApplyImport(AppData imported, ImportMode mode, DuplicateStrategy strategy)
        {
            var result = new ImportResult();

            if (imported == null)
            {
                result.Errors++;
                return result;
            }

            if (mode == ImportMode.ReplaceAll)
            {
                _appData.Entries.Clear();
                _appData.Entries.AddRange(imported.Entries);
                result.Added = imported.Entries.Count;
                return result;
            }

            foreach (var incoming in imported.Entries)
            {
                if (incoming == null)
                {
                    result.Errors++;
                    continue;
                }

                var existing = _appData.Entries.FirstOrDefault(e =>
                    string.Equals(e.Title?.Trim(), incoming.Title?.Trim(), StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(e.Username?.Trim(), incoming.Username?.Trim(), StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(e.Email?.Trim(), incoming.Email?.Trim(), StringComparison.OrdinalIgnoreCase));

                if (existing == null)
                {
                    _appData.Entries.Add(incoming);
                    result.Added++;
                    continue;
                }

                switch (strategy)
                {
                    case DuplicateStrategy.KeepExisting:
                        result.Skipped++;
                        break;

                    case DuplicateStrategy.OverwriteExisting:
                        existing.Category = incoming.Category;
                        existing.Title = incoming.Title;
                        existing.Email = incoming.Email;
                        existing.Username = incoming.Username;
                        existing.Password = incoming.Password;
                        existing.Url = incoming.Url;
                        existing.Note = incoming.Note;
                        existing.UpdatedAt = DateTime.Now;
                        result.Updated++;
                        break;

                    case DuplicateStrategy.RenameImported:
                        incoming.Title = $"{incoming.Title} (imported)";
                        incoming.Id = Guid.NewGuid();
                        _appData.Entries.Add(incoming);
                        result.Added++;
                        break;
                }
            }

            return result;
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