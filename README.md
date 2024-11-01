# Wpf-Addressverwaltungs-Tool
======================

<!-- TOC -->
* [# Wpf-Addressverwaltungs-Tool](#-wpf-addressverwaltungs-tool)
  * [## Übersicht](#-übersicht)
  * [## Funktionen](#-funktionen)
  * [## Anforderungen](#-anforderungen)
  * [## Installation](#-installation)
  * [## Sicherheit](#-sicherheit)
  * [## Suchfunktion](#-suchfunktion)
    * [Such-Parameter:](#such-parameter)
  * [## Lizenz](#-lizenz)
  * [## Kontakt](#-kontakt)
<!-- TOC -->

Eine Windows Presentation Foundation (WPF)-Anwendung zur Verwaltung von Adressen und Telefonnummern.

## Übersicht
-----------

WpfAddressverwaltung ist eine Desktop-Anwendung, die mit WPF und C# entwickelt wurde. Sie bietet eine einfache und intuitive Oberfläche zur Verwaltung von Adressen und Telefonnummern.

## Funktionen
------------

*   **Adressverwaltung**: Erstellen, bearbeiten und löschen von Adressen
*   **Telefonnummernverwaltung**: Erstellen, bearbeiten und löschen von Telefonnummern
*   **Datenverschlüsselung**: Verwendung von AES-Verschlüsselung zur Sicherung von Daten
*   **Benutzerfreundliche Oberfläche**: Einfache Bedienung für die Verwaltung von Adressen und Telefonnummern

## Anforderungen
------------

*   .NET 8.0 oder höher
*   Windows 10 oder höher

## Installation
------------

1.  Klone das Repository mit Git: `git clone https://github.com/dein-username/WpfAddressverwaltung.git`
2.  Öffne die Solution in Visual Studio
3.  Build und Programm ausführen

## Sicherheit
---------

WpfAddressverwaltung verwendet AES-Verschlüsselung zur Sicherung von Daten. Der Verschlüsselungsschlüssel wird sicher gespeichert und ist nur autorisierten Benutzern zugänglich.

## Suchfunktion
----------

Die Liste ist mit einer Suchfunktion versehen, die die Adressen und Telefonnummern filtern kann. Dies erleichtert das Suchen von Adressen und Telefonnummern.
### Such-Parameter:
- **#:\<id>** Sucht nach Mitarbeitern mit der angegebenen ID
- **addr:\<string>** Sucht nach Mitarbeitern mit dem angegebenen String in der Adresse
- **pos:\<string>** Sucht nach Mitarbeitern mit dem angegebenen String in der Position

Ist der Suchparameter leer, werden alle Mitarbeiter angezeigt.


## Lizenz
-------

Dieses Projekt ist unter der MIT-Lizenz lizenziert.

## Kontakt
--------

Für Fragen oder Anregungen kannst du dich gerne an mich wenden.
