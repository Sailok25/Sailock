using System.Collections.Generic;
using Sailock.Models;

namespace Sailock.Models
{
    public class AppData
    {
        public List<PasswordEntry> Entries { get; set; } = new List<PasswordEntry>();
        public AppSettings Settings { get; set; } = new AppSettings();
    }

    public class AppSettings
    {
        public bool Is2FAEnabled { get; set; } = false;
        public string TotpSecret { get; set; } = null;
        public bool AutoLockEnabled { get; set; } = true;
        public string AutoLockTimeout { get; set; } = "2 min";
        public bool IsDarkTheme { get; set; } = true;
        public bool IsHighContrast { get; set; } = true;
        public string Language { get; set; } = "English";
        public string TextSize { get; set; } = "Default";
    }
}