# Changelog — Sailock
All changes to Sailock are documented here.

## [1.5.0] - 2026-07-16

### Added
* New import wizard with support for Merge, Replace, and Custom import modes
* Configurable duplicate handling during import (Keep existing, Overwrite, or Rename)
* Date Created and Date Modified columns in the Dashboard
* Full localization for all Settings dropdown values (English, Spanish, German, and French)

### Changed
* Redesigned import workflow with confirmation before applying changes
* Dashboard now uses a compact three-dot context menu instead of the Edit button
* Improved Dashboard layout and date formatting
* Login screen now always starts in dark mode and applies the selected theme after unlocking
* Updated interface text to use sentence capitalization
* Removed the in-app Changelog view; release notes are now available in the GitHub repository

### Fixed
* Softer light theme colors for improved readability
* Fixed Auto-Lock label visibility in the light theme
* Fixed Auto-Lock description text color in Settings
* Fixed missing translations in the Change Master Password dialog
* Fixed theme persistence when launching the application

---

## [1.4.2] - 2026-07-01

### Fixed
* Fixed cache issue when updating the master password

---

## [1.4.1] - 2026-06-28

### Fixed
* Added English, Spanish, German and French translations for the Change Master Password dialog

---

## [1.4.0] - 2026-06-21

### Added
* New category filter in the Dashboard to view entries by category or show all entries.
* URL/Website field in password entries for easier account identification.
* German (Deutsch) language support.
* French (Français) language support.

### Changed
* Dashboard now shows a Website column with the URL associated to each entry.
* Search now includes matches in URLs and websites.
* The language section in Settings includes new localization options.

---

## [1.3.1] - 2026-06-15

### Added
* Configurable auto-lock time: 30 seconds, 1 minute, 2 minutes, 5 minutes, or disabled entirely

### Changed
* The Auto-Lock selector in Settings is now a dropdown, matching the language and text-size selectors
* The Auto-Lock description in Settings is now neutral

---

## [1.3.0] - 2026-06-08

### Added
* Full light theme implemented across the entire application
* Functional master password change with security validations
* Unified scrollbars across the whole app with a consistent style
* Improved hover effect on window buttons (minimize, maximize, close)

### Changed
* Scrollbars now share the same visual style across Dashboard, Settings, Generator and Changelog
* Settings interface now uses a ScrollViewer for better navigation on smaller screens
* Improved sidebar with more visible hover in light mode
* Latest/Legacy badge in Changelog with customizable colors
* Dashboard ScrollViewer now sits outside the table for a better experience

### Fixed
* White text in light mode is now visible (black/dark green)
* Checkboxes and toggles now visible in light mode
* Window buttons now have more visible hover in both themes
* ScrollBar in Generator now appears when the window is small
* Changelog badges now have proper contrast

---

## [1.2.2] - 2026-06-05

### Fixed
* Restored visibility of the version button in the sidebar
* Fixed an issue making the version history hard to access

---

## [1.2.1] - 2026-06-04

### Fixed
* Fixed desync between the visible and hidden password fields
* Fixed a visual caching issue in the master password field
* Fixed password field alignment when toggling visibility
* Fixed spelling errors in the Spanish text

### Changed
* Updated font to JetBrains Mono
* Adjusted Small, Default and Large sizes with a more noticeable difference
* Scaling no longer affects modals or forms
* The sidebar now scales along with content

---

## [1.2.0] - 2026-06-01

### Added
* Full application support added for: English and Spanish
* Language selection is now available in Settings and persists between sessions
* All views, modal windows and system messages have been fully translated

### Changed
* Improved the style of the scrollbars in the Changelog view
* The sidebar version button now uses a style consistent with the login screen

### Fixed
* Fixed an issue preventing some interface text from displaying correctly
* Fixed an issue with the language system that could prevent certain translations from displaying correctly

---

## [1.1.3] - 2026-05-30

### Added
* Added two-factor verification (2FA) for extra security when logging in
* You can now confirm disabling 2FA with a security message
* Password confirmation is now required before editing any saved password
* Added an option to show or hide passwords in the edit windows
* You can now change the interface size (small, normal or large)
* Added light mode in addition to dark mode
* You can now adjust the app's visual contrast for better readability
* The add-new-password window is now simpler and faster to use
* The edit window now shows only the options relevant to the changes made

### Changed
* Improved the overall password editing experience
* Optimized how elements are displayed throughout the application

### Fixed
* Fixed an issue where the add-password window would not close correctly
* Fixed a duplicated logo in the sidebar
* The verification code field is now correctly centered
* Removed an unnecessary visual line at the top of the application
* Improved logo loading throughout the interface

---

## [1.1.2] - 2026-05-28

### Added
* You can now import your data from a backup file
* You can now securely export your data to a .slock file
* Added the option to delete all application data
* Added automatic app lock after a period of inactivity
* Confirmation messages when importing or exporting data

### Fixed
* Fixed an issue that prevented imported data from loading correctly
* Fixed an error when exporting data in some cases

---

## [1.1.1] - 2026-05-27

### Added
* New Settings screen with all main options:
  * Security (2FA, password change, auto-lock)
  * Appearance (light/dark theme, contrast, text size)
  * Data Management (import and export)
  * Danger Zone (full data wipe with confirmation)
* The app can now switch between light and dark theme
* Added support for adjusting the interface size
* Prepared the system for future visual improvements

### Changed
* Improved how the app saves data more reliably
* Login now uses the user's real password
* On first launch, users can create their master password through a guided flow

### Fixed
* Fixed stability issues when saving data
* Fixed navigation errors between screens

---

## [1.0.2] - 2026-05-25

### Added
* Main panel where you can view all your saved passwords
* Window to easily add new passwords
* Window to edit and delete existing passwords
* Password generator with customization options
* Navigation between sections (home, generator and settings)
* Custom window buttons (minimize, maximize and close)
* Complete base design of the application
* Version number visible within the application

### Changed
* Improved navigation between sections for a smoother flow
* Reinforced overall application stability

### Fixed
* Fixed the login system
* Fixed issues with button and interface element responsiveness

---

## [1.0.0] - 2026-05-23

### Added
* Login screen
* Initial application structure
* Base design of all main screens
* Navigation system between sections
* Initial logo and visual style
