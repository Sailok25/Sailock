# Changelog — Sailock
Alle Änderungen an Sailock werden hier dokumentiert.

## [1.5.0] - 2026-07-16

### Hinzugefügt
* Neuer Import-Assistent mit den Modi Zusammenführen, Ersetzen und Benutzerdefiniert
* Konfigurierbare Behandlung doppelter Einträge beim Import (Beibehalten, Überschreiben oder Umbenennen)
* Neue Spalten Erstellungsdatum und Änderungsdatum im Dashboard
* Vollständige Lokalisierung aller Dropdown-Werte in den Einstellungen (Englisch, Spanisch, Deutsch und Französisch)

### Geändert
* Importvorgang mit Bestätigung vor dem Anwenden der Änderungen neu gestaltet
* Das Dashboard verwendet jetzt ein kompaktes Drei-Punkte-Kontextmenü anstelle der Schaltfläche Bearbeiten
* Dashboard-Layout und Datumsformatierung verbessert
* Der Anmeldebildschirm startet jetzt immer im dunklen Design und übernimmt das ausgewählte Design nach dem Entsperren des Tresors
* Die Benutzeroberfläche verwendet jetzt eine einheitliche Satzschreibung
* Das integrierte Änderungsprotokoll wurde entfernt; Versionshinweise sind jetzt im GitHub-Repository verfügbar

### Behoben
* Angenehmere Farben im hellen Design für bessere Lesbarkeit
* Sichtbarkeit der Auto-Sperren-Beschriftung im hellen Design korrigiert
* Farbe des Beschreibungstextes für die Auto-Sperre in den Einstellungen korrigiert
* Fehlende Übersetzungen im Dialog zum Ändern des Master-Passworts korrigiert
* Problem mit der Übernahme des Designs beim Start der Anwendung behoben

---

## [1.4.2] - 2026-07-01

### Fixed
* Cache-Problem beim Aktualisieren des Master-Passworts behoben

---

## [1.4.1] - 2026-06-28

### Fixed
* Übersetzungen für Englisch, Spanisch, Deutsch und Französisch im Dialog „Master-Passwort ändern" hinzugefügt

---

## [1.4.0] - 2026-06-21

### Added
* Neuer Kategoriefilter im Dashboard, um Einträge nach Kategorie oder alle Einträge anzuzeigen.
* Feld URL/Website in Passworteinträgen zur einfacheren Identifizierung von Konten.
* Unterstützung für die deutsche Sprache.
* Unterstützung für die französische Sprache (Français).

### Changed
* Das Dashboard zeigt jetzt eine Website-Spalte mit der zu jedem Eintrag gehörenden URL.
* Die Suche schließt jetzt Treffer in URLs und Websites ein.
* Der Sprachbereich in den Einstellungen enthält neue Lokalisierungsoptionen.

---

## [1.3.1] - 2026-06-15

### Added
* Konfigurierbare automatische Sperrzeit: 30 Sekunden, 1 Minute, 2 Minuten, 5 Minuten oder vollständig deaktiviert

### Changed
* Die Auto-Lock-Auswahl in den Einstellungen ist jetzt ein Dropdown, wie auch Sprache und Textgröße
* Die Beschreibung von Auto-Lock in den Einstellungen ist jetzt neutral formuliert

---

## [1.3.0] - 2026-06-08

### Added
* Vollständiges helles Design in der gesamten Anwendung implementiert
* Funktionierender Master-Passwortwechsel mit Sicherheitsvalidierungen
* Einheitliche Scrollbalken in der gesamten App mit konsistentem Stil
* Verbesserter Hover-Effekt bei Fensterschaltflächen (minimieren, maximieren, schließen)

### Changed
* Scrollbalken haben jetzt denselben visuellen Stil in Dashboard, Settings, Generator und Changelog
* Die Settings-Oberfläche verwendet jetzt einen ScrollViewer für bessere Navigation auf kleinen Bildschirmen
* Verbesserte Sidebar mit deutlicherem Hover im hellen Modus
* Latest/Legacy-Badge im Changelog mit anpassbaren Farben
* ScrollViewer im Dashboard befindet sich jetzt außerhalb der Tabelle für ein besseres Erlebnis

### Fixed
* Weißer Text im hellen Modus ist jetzt sichtbar (schwarz/dunkelgrün)
* Checkboxen und Toggles im hellen Modus jetzt sichtbar
* Fensterschaltflächen mit deutlicherem Hover in beiden Themes
* ScrollBar im Generator erscheint jetzt, wenn das Fenster klein ist
* Badges im Changelog haben jetzt einen angemessenen Kontrast

---

## [1.2.2] - 2026-06-05

### Fixed
* Sichtbarkeit der Versions-Schaltfläche in der Seitenleiste wiederhergestellt
* Problem behoben, durch das der Zugriff auf den Versionsverlauf kaum sichtbar war

---

## [1.2.1] - 2026-06-04

### Fixed
* Desynchronisation zwischen sichtbarem und verstecktem Passwortfeld behoben
* Visuelles Caching-Problem im Master-Passwortfeld behoben
* Ausrichtung des Passwortfelds beim Umschalten der Sichtbarkeit korrigiert
* Rechtschreibfehler in spanischen Texten korrigiert

### Changed
* Schriftart auf JetBrains Mono aktualisiert
* Größen Small, Default und Large mit deutlicherem Unterschied angepasst
* Die Skalierung wirkt sich nicht mehr auf Modals oder Formulare aus
* Die Seitenleiste skaliert jetzt zusammen mit dem Inhalt

---

## [1.2.0] - 2026-06-01

### Added
* Vollständige Unterstützung der Anwendung für: Englisch und Spanisch hinzugefügt
* Die Sprachauswahl ist jetzt in den Einstellungen verfügbar und bleibt sitzungsübergreifend erhalten
* Alle Ansichten, modalen Fenster und Systemmeldungen wurden vollständig übersetzt

### Changed
* Stil der Scrollbalken in der Changelog-Ansicht verbessert
* Die Versions-Schaltfläche in der Seitenleiste verwendet jetzt einen Stil, der mit dem Anmeldebildschirm übereinstimmt

### Fixed
* Problem behoben, durch das einige Oberflächentexte nicht korrekt angezeigt wurden
* Problem im Sprachsystem behoben, das die korrekte Anzeige bestimmter Übersetzungen verhindern konnte

---

## [1.1.3] - 2026-05-30

### Added
* Zwei-Faktor-Verifizierung (2FA) für mehr Sicherheit beim Anmelden hinzugefügt
* Die Deaktivierung von 2FA kann jetzt mit einer Sicherheitsmeldung bestätigt werden
* Vor dem Bearbeiten eines gespeicherten Passworts wird jetzt eine Passwortbestätigung verlangt
* Option zum Ein-/Ausblenden von Passwörtern in den Bearbeitungsfenstern hinzugefügt
* Die Oberflächengröße kann jetzt geändert werden (klein, normal oder groß)
* Heller Modus zusätzlich zum dunklen Modus hinzugefügt
* Der visuelle Kontrast der Anwendung kann jetzt zur besseren Lesbarkeit angepasst werden
* Das Fenster zum Hinzufügen eines neuen Passworts ist jetzt einfacher und schneller zu bedienen
* Das Bearbeitungsfenster zeigt jetzt nur die für die vorgenommenen Änderungen relevanten Optionen an

### Changed
* Die allgemeine Erfahrung beim Bearbeiten von Passwörtern wurde verbessert
* Die Darstellung von Elementen in der gesamten Anwendung wurde optimiert

### Fixed
* Problem behoben, bei dem sich das Fenster zum Hinzufügen eines Passworts nicht korrekt schloss
* Doppeltes Logo in der Seitenleiste behoben
* Das Feld für den Verifizierungscode wird jetzt korrekt zentriert angezeigt
* Eine unnötige visuelle Linie am oberen Rand der Anwendung wurde entfernt
* Das Laden des Logos in der gesamten Oberfläche wurde verbessert

---

## [1.1.2] - 2026-05-28

### Added
* Daten können jetzt aus einer Sicherungsdatei importiert werden
* Daten können jetzt sicher in eine .slock-Datei exportiert werden
* Option zum Löschen aller Anwendungsdaten hinzugefügt
* Automatische Sperre der Anwendung nach einer Inaktivitätsphase hinzugefügt
* Bestätigungsmeldungen beim Importieren oder Exportieren von Daten

### Fixed
* Problem behoben, das das korrekte Laden importierter Daten verhinderte
* Fehler beim Exportieren von Daten in bestimmten Fällen behoben

---

## [1.1.1] - 2026-05-27

### Added
* Neuer Einstellungsbildschirm mit allen wichtigen Optionen:
  * Sicherheit (2FA, Passwortwechsel, automatische Sperre)
  * Erscheinungsbild (helles/dunkles Design, Kontrast, Textgröße)
  * Datenverwaltung (Import und Export)
  * Gefahrenzone (vollständiges Löschen mit Bestätigung)
* Die App kann jetzt zwischen hellem und dunklem Design wechseln
* Unterstützung zum Anpassen der Oberflächengröße hinzugefügt
* Das System wurde für zukünftige visuelle Verbesserungen vorbereitet

### Changed
* Die Art und Weise, wie die App Daten speichert, wurde stabiler gestaltet
* Die Anmeldung verwendet jetzt das echte Passwort des Benutzers
* Beim ersten Start können Benutzer ihr Master-Passwort in einem geführten Ablauf erstellen

### Fixed
* Stabilitätsprobleme beim Speichern von Daten behoben
* Navigationsfehler zwischen Bildschirmen behoben

---

## [1.0.2] - 2026-05-25

### Added
* Hauptbereich, in dem alle gespeicherten Passwörter angezeigt werden
* Fenster zum einfachen Hinzufügen neuer Passwörter
* Fenster zum Bearbeiten und Löschen vorhandener Passwörter
* Passwortgenerator mit Anpassungsoptionen
* Navigation zwischen Bereichen (Start, Generator und Einstellungen)
* Benutzerdefinierte Fensterschaltflächen (minimieren, maximieren und schließen)
* Vollständiges Grunddesign der Anwendung
* Innerhalb der Anwendung sichtbare Versionsnummer

### Changed
* Navigation zwischen Bereichen für einen flüssigeren Ablauf verbessert
* Allgemeine Stabilität der Anwendung erhöht

### Fixed
* Anmeldesystem korrigiert
* Probleme mit der Reaktion von Schaltflächen und Oberflächenelementen behoben

---

## [1.0.0] - 2026-05-23

### Added
* Anmeldebildschirm
* Anfängliche Anwendungsstruktur
* Grunddesign aller Hauptbildschirme
* Navigationssystem zwischen Bereichen
* Anfängliches Logo und visuelles Design
