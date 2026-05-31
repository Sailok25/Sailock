# Sailock

**Sailock** is a local-first password manager for Windows. No cloud, no internet required — your data never leaves your device.

Built with .NET 8, WPF and C#, following an MVVM architecture.

---

## Features

- AES-256 encrypted local storage (`.slock` file format)
- Master password authentication
- Two-factor authentication (TOTP — compatible with Google Authenticator, Authy and any standard TOTP app)
- Auto-lock after inactivity
- Password generator with configurable charset, length, exclusions and prefix
- Dark and light theme
- High contrast mode
- Configurable interface scale (Small, Default, Large)
- Import and export of encrypted `.slock` backups
- Full changelog accessible from within the app

---

## Screenshots

<img src="Images/Login.png" width="49%"/> <img src="Images/Dashboard.png" width="49%"/>
<img src="Images/Generator.png" width="49%"/> <img src="Images/Settings.png" width="49%"/>
<img src="Images/Changelog.png" width="49%"/>
---

## Tech Stack

| Layer        | Technology          |
|-------------|---------------------|
| Language     | C# 12 / .NET 8      |
| UI Framework | WPF (Windows only)  |
| Architecture | MVVM                |
| Encryption   | AES-256 + PBKDF2    |
| 2FA          | TOTP via Otp.NET    |
| QR Codes     | QRCoder             |

---

## Project Structure
Sailock/
├── Helpers/        # RelayCommand, ViewModelBase, Converters, QR helper
├── Models/         # PasswordEntry, AppData, AppSettings, ChangelogEntry
├── Resources/      # Icons and logo assets
├── Services/       # Crypto, Storage, Navigation, Theme, TOTP, AutoLock, Changelog, Version
├── ViewModels/     # One ViewModel per screen
├── Views/          # XAML UserControls
└── Fonts/          # Supermolot Light

---

## Getting Started

### Requirements

- Windows 10 or later
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (for building from source)

### Build from source

```bash
git clone https://github.com/YOUR_USERNAME/sailock.git
cd sailock/Sailock
dotnet build
dotnet run
```

### First run

On first launch, Sailock will prompt you to create a master password. This password is used to derive the encryption key for your `.slock` data file. **There is no recovery mechanism — do not forget your master password.**

---

## Data & Security

- All data is stored in a single encrypted file (`sailock.slock`) on your local machine.
- The encryption key is derived from your master password using PBKDF2 with 100,000 iterations and a random salt.
- Each save generates a new salt and IV, so no two encrypted files are identical even with the same data.
- Sailock never sends any data over the network.

---

## Changelog

See [CHANGELOG.md](CHANGELOG.md) for the full version history.

---

## License

This project is currently unlicensed. All rights reserved by the author.

---

## Author

Developed by Alba — [GitHub](https://github.com/Sailok25)
