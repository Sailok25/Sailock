using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Sailock.Helpers;
using Sailock.Models;
using Sailock.Services;

namespace Sailock.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        private readonly StorageService _storage;
        private readonly string _masterPassword;
        private readonly AppData _appData;

        private ObservableCollection<PasswordEntry> _allEntries;

        private ObservableCollection<PasswordEntry> _entries;
        public ObservableCollection<PasswordEntry> Entries
        {
            get => _entries;
            set => SetProperty(ref _entries, value);
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                ApplyFilter();
            }
        }

        private PasswordEntry _selectedEntry;
        public PasswordEntry SelectedEntry
        {
            get => _selectedEntry;
            set => SetProperty(ref _selectedEntry, value);
        }

        private bool _isAddModalOpen;
        public bool IsAddModalOpen
        {
            get => _isAddModalOpen;
            set => SetProperty(ref _isAddModalOpen, value);
        }

        private bool _isEditModalOpen;
        public bool IsEditModalOpen
        {
            get => _isEditModalOpen;
            set => SetProperty(ref _isEditModalOpen, value);
        }

        private PasswordEntry _editingEntry;
        public PasswordEntry EditingEntry
        {
            get => _editingEntry;
            set => SetProperty(ref _editingEntry, value);
        }

        public ICommand OpenAddModalCommand { get; }
        public ICommand SaveNewEntryCommand { get; }
        public ICommand CancelAddCommand { get; }
        public ICommand OpenEditModalCommand { get; }
        public ICommand SaveEditCommand { get; }
        public ICommand DeleteEntryCommand { get; }
        public ICommand CancelEditCommand { get; }
        public ICommand CopyPasswordCommand { get; }

        public DashboardViewModel(AppData appData, StorageService storage, string masterPassword)
        {
            _appData = appData;
            _storage = storage;
            _masterPassword = masterPassword;

            _allEntries = new ObservableCollection<PasswordEntry>(_appData.Entries);
            Entries = new ObservableCollection<PasswordEntry>(_allEntries);

            OpenAddModalCommand = new RelayCommand(_ => OpenAddModal());
            SaveNewEntryCommand = new RelayCommand(_ => SaveNewEntry());
            CancelAddCommand = new RelayCommand(_ => IsAddModalOpen = false);
            OpenEditModalCommand = new RelayCommand(e => OpenEditModal(e as PasswordEntry));
            SaveEditCommand = new RelayCommand(_ => SaveEdit());
            DeleteEntryCommand = new RelayCommand(_ => DeleteEntry());
            CancelEditCommand = new RelayCommand(_ => IsEditModalOpen = false);
            CopyPasswordCommand = new RelayCommand(e => CopyPassword(e as PasswordEntry));
        }

        private void ApplyFilter()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                Entries = new ObservableCollection<PasswordEntry>(_allEntries);
                return;
            }

            var term = SearchText.ToLower();
            var filtered = _allEntries.Where(e =>
                (e.Title != null && e.Title.ToLower().Contains(term)) ||
                (e.Username != null && e.Username.ToLower().Contains(term)) ||
                (e.Email != null && e.Email.ToLower().Contains(term)) ||
                (e.Category != null && e.Category.ToLower().Contains(term))
            );

            Entries = new ObservableCollection<PasswordEntry>(filtered);
        }

        private void OpenAddModal()
        {
            EditingEntry = new PasswordEntry();
            IsAddModalOpen = true;
        }

        private void SaveNewEntry()
        {
            if (string.IsNullOrWhiteSpace(EditingEntry?.Title)) return;

            _allEntries.Add(EditingEntry);
            _appData.Entries.Add(EditingEntry);
            Persist();
            ApplyFilter();
            IsAddModalOpen = false;
        }

        private void OpenEditModal(PasswordEntry entry)
        {
            if (entry == null) return;

            EditingEntry = new PasswordEntry
            {
                Id = entry.Id,
                Category = entry.Category,
                Title = entry.Title,
                Email = entry.Email,
                Username = entry.Username,
                Password = entry.Password,
                Note = entry.Note
            };

            SelectedEntry = entry;
            IsEditModalOpen = true;
        }

        private void SaveEdit()
        {
            if (SelectedEntry == null || EditingEntry == null) return;

            SelectedEntry.Category = EditingEntry.Category;
            SelectedEntry.Title = EditingEntry.Title;
            SelectedEntry.Email = EditingEntry.Email;
            SelectedEntry.Username = EditingEntry.Username;
            SelectedEntry.Password = EditingEntry.Password;
            SelectedEntry.Note = EditingEntry.Note;

            var index = _allEntries.IndexOf(SelectedEntry);
            if (index >= 0)
            {
                _allEntries.RemoveAt(index);
                _allEntries.Insert(index, SelectedEntry);
            }

            var dataIndex = _appData.Entries.FindIndex(e => e.Id == SelectedEntry.Id);
            if (dataIndex >= 0)
                _appData.Entries[dataIndex] = SelectedEntry;

            Persist();
            ApplyFilter();
            IsEditModalOpen = false;
        }

        private void DeleteEntry()
        {
            if (SelectedEntry == null) return;

            _allEntries.Remove(SelectedEntry);
            _appData.Entries.RemoveAll(e => e.Id == SelectedEntry.Id);
            Persist();
            ApplyFilter();
            IsEditModalOpen = false;
        }

        private void CopyPassword(PasswordEntry entry)
        {
            if (entry?.Password != null)
                System.Windows.Clipboard.SetText(entry.Password);
        }

        private void Persist() => _storage.Save(_appData, _masterPassword);
    }
}