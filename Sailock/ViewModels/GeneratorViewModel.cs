using System;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Sailock.Helpers;

namespace Sailock.ViewModels
{
    public class GeneratorViewModel : ViewModelBase
    {
        private static readonly Random _random = new Random();

        // --- Contraseña generada ---
        private string _generatedPassword;
        public string GeneratedPassword
        {
            get => _generatedPassword;
            set => SetProperty(ref _generatedPassword, value);
        }

        // --- Longitud ---
        private int _length = 16;
        public int Length
        {
            get => _length;
            set => SetProperty(ref _length, value);
        }

        // --- Opciones de caracteres ---
        private bool _useUppercase = true;
        public bool UseUppercase
        {
            get => _useUppercase;
            set => SetProperty(ref _useUppercase, value);
        }

        private bool _useLowercase = true;
        public bool UseLowercase
        {
            get => _useLowercase;
            set => SetProperty(ref _useLowercase, value);
        }

        private bool _useNumbers = true;
        public bool UseNumbers
        {
            get => _useNumbers;
            set => SetProperty(ref _useNumbers, value);
        }

        private bool _useSymbols = true;
        public bool UseSymbols
        {
            get => _useSymbols;
            set => SetProperty(ref _useSymbols, value);
        }

        // --- Toggles ---
        private bool _allowRepeat = true;
        public bool AllowRepeat
        {
            get => _allowRepeat;
            set => SetProperty(ref _allowRepeat, value);
        }

        private bool _allowSequential = true;
        public bool AllowSequential
        {
            get => _allowSequential;
            set => SetProperty(ref _allowSequential, value);
        }

        // --- Excluir caracteres ---
        private string _excludeCharacters;
        public string ExcludeCharacters
        {
            get => _excludeCharacters;
            set => SetProperty(ref _excludeCharacters, value);
        }

        // --- Empezar con ---
        private string _startWith;
        public string StartWith
        {
            get => _startWith;
            set
            {
                // Solo permitir un carácter
                if (value != null && value.Length > 1)
                    value = value.Substring(0, 1);
                SetProperty(ref _startWith, value);
            }
        }

        public ICommand GenerateCommand { get; }
        public ICommand CopyCommand { get; }

        public GeneratorViewModel()
        {
            GenerateCommand = new RelayCommand(_ => GeneratePassword());
            CopyCommand = new RelayCommand(_ => CopyPassword(),
                                           _ => !string.IsNullOrEmpty(GeneratedPassword));
        }

        private void GeneratePassword()
        {
            // Construir el pool de caracteres disponibles
            const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lower = "abcdefghijklmnopqrstuvwxyz";
            const string numbers = "0123456789";
            const string symbols = "!@#$%^&*()-_=+[]{}|;:,.<>?/";

            var pool = new StringBuilder();
            if (UseUppercase) pool.Append(upper);
            if (UseLowercase) pool.Append(lower);
            if (UseNumbers) pool.Append(numbers);
            if (UseSymbols) pool.Append(symbols);

            // Eliminar caracteres excluidos
            string availableChars = pool.ToString();
            if (!string.IsNullOrWhiteSpace(ExcludeCharacters))
            {
                availableChars = new string(
                    availableChars.Where(c => !ExcludeCharacters.Contains(c)).ToArray()
                );
            }

            // Si no hay pool válido, no generar
            if (availableChars.Length == 0)
            {
                GeneratedPassword = "NO VALID CHARACTERS";
                return;
            }

            var result = new StringBuilder();

            // Añadir el carácter inicial si se especificó
            if (!string.IsNullOrEmpty(StartWith))
            {
                result.Append(StartWith[0]);
            }

            int attempts = 0;
            int maxAttempts = Length * 100;

            while (result.Length < Length && attempts < maxAttempts)
            {
                attempts++;

                char candidate = availableChars[_random.Next(availableChars.Length)];

                // Comprobar repetición
                if (!AllowRepeat && result.ToString().Contains(candidate))
                    continue;

                // Comprobar secuencialidad (caracteres consecutivos en ASCII)
                if (!AllowSequential && result.Length > 0)
                {
                    char last = result[result.Length - 1];
                    if (Math.Abs(candidate - last) == 1)
                        continue;
                }

                result.Append(candidate);
            }

            GeneratedPassword = result.ToString();
        }

        private void CopyPassword()
        {
            if (!string.IsNullOrEmpty(GeneratedPassword))
            {
                System.Windows.Clipboard.SetText(GeneratedPassword);
            }
        }
    }
}