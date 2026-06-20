using System;
using ZgadnijLiczbe2.Models;

namespace ZgadnijLiczbe2.Utils
{
    public static class L
    {
        public static string Welcome(Language lang) => lang==Language.PL? "Zgadnij Liczbę 2 - Powitanie" : "Guess Number 2 - Welcome";
        public static string MenuOptions(Language lang) => lang==Language.PL? "1) Nowa gra\n2) Hall of Fame\n3) Ustawienia\n4) Wyjście" : "1) New Game\n2) Hall of Fame\n3) Settings\n4) Exit";
        public static string Choose(Language lang) => lang==Language.PL? "Wybierz opcję:" : "Choose an option:";
        public static string PressEnter(Language lang) => lang==Language.PL? "Naciśnij Enter..." : "Press Enter...";
        public static string SettingsTitle(Language lang) => lang==Language.PL? "Ustawienia" : "Settings";
        public static string LangOption(Language lang) => lang==Language.PL? "Język (PL/EN):" : "Language (PL/EN):";
        public static string AskBetOption(Language lang) => lang==Language.PL? "Czy pytać o tryb zakładu (tak/nie):" : "Ask for bet mode (yes/no):";
        public static string ClearHallOption(Language lang) => lang==Language.PL? "Wyczyść Hall of Fame" : "Clear Hall of Fame";
        public static string Confirm(Language lang) => lang==Language.PL? "Potwierdź (tak/nie):" : "Confirm (yes/no):";
        public static string NoRecords(Language lang) => lang==Language.PL? "Brak wyników." : "No records.";
        public static string ChooseDifficulty(Language lang) => lang==Language.PL? "Wybierz poziom: 1) Łatwy(1-50) 2) Średni(1-100) 3) Trudny(1-250)" : "Choose difficulty: 1) Easy(1-50) 2) Medium(1-100) 3) Hard(1-250)";
        public static string DifficultyTitle(Language lang) => lang==Language.PL? "Wybierz poziom trudnosci" : "Choose difficulty";
        public static string AskForBet(Language lang) => lang==Language.PL? "Czy chcesz ustawić tryb zakładu? Podaj maksymalną ilość prób (pusta = brak):" : "Do you want bet mode? Enter max attempts (empty = none):";
        public static string ChooseGameMode(Language lang) => lang==Language.PL? "1) Standardowa 2) Nowa gra plus" : "1) Standard 2) New Game Plus";
        public static string EnterMaxTries(Language lang) => lang==Language.PL? "Podaj maksymalna liczbe prob:" : "Enter maximum attempts:";
        public static string EnterName(Language lang) => lang==Language.PL? "Podaj imie:" : "Enter your name:";
        public static string InvalidNumber(Language lang) => lang==Language.PL? "Podaj poprawna liczbe!" : "Please enter a valid number!";
        public static string Attempt(Language lang, int n) => lang==Language.PL? $"Numer twojej proby: {n}" : $"Attempt {n}:";
        public static string PlusReshuffled(Language lang) => lang==Language.PL? "(Tryb Plus) Sekret zostal przelosowany." : "(Plus mode) The secret number was reshuffled.";
        public static string NumberChanged(Language lang) => lang==Language.PL? "Liczba została zmieniona" : "Number has been changed";
        public static string CorrectResult(Language lang, int attempts, double duration) => lang==Language.PL? $"Trafiles! Proby: {attempts}, Czas: {duration:F1}s" : $"Correct! Attempts: {attempts}, Time: {duration:F1}s";
        public static string ReachedMax(Language lang) => lang==Language.PL? "Przekroczono maksymalna liczbe prob. Koniec gry." : "Reached maximum attempts. Game over.";
        public static string ResultSaved(Language lang) => lang==Language.PL? "Wynik zapisano do Hall of Fame." : "Result saved to Hall of Fame.";
        public static string NoRecordsMsg(Language lang) => lang==Language.PL? "Nie ma jeszcze zadnych zapisanych wynikow" : "There are no recorded results yet";
        public static string PressAny(Language lang) => lang==Language.PL? "Wpisz jakikolwiek symbol aby wyjsc" : "Type any key to exit";
        public static string PlaceHeader(Language lang) => lang==Language.PL? "Miejsce    Proby   Nick" : "Place    Attempts   Name";
        public static string PlaceHeaderWithTime(Language lang) => lang==Language.PL? "Miejsce    Proby   Czas(s)   Nick" : "Place    Attempts   Time(s)   Name";
        public static string PlusDenote(Language lang) => lang==Language.PL? "* oznacza wpis z trybu Nowa gra plus" : "* denotes entry from New Game Plus mode";
        public static string WinMessage(Language lang, int attempts, string level) => lang==Language.PL? $"Udalo Ci sie wygrac w {attempts} probie na poziomie trudnosci {level}." : $"You won in {attempts} attempts on difficulty {level}.";
        public static string[] RandomLows(Language lang) => lang==Language.PL? new[] {"Za malo!","Mysle o wiekszej liczbie!","Sprobuj wiecej!","To za nisko!","Nie trafione - za nisko."} : new[] {"Too low!","I think of a higher number!","Try higher!","Still too low!","Not correct - too low."};
        public static string[] RandomHighs(Language lang) => lang==Language.PL? new[] {"Za duzo!","Mysle o mniejszej liczbie!","Przesadziles!","Sprobuj mniej!","Nie trafione - za wysoko."} : new[] {"Too high!","I think of a smaller number!","You overshot!","Try lower!","Not correct - too high."};
        public static string HallTitle(Language lang) => lang==Language.PL? "Hall of Fame" : "Hall of Fame";
        public static string SettingsChangeLanguage(Language lang) => lang==Language.PL? "1) Zmień język" : "1) Change language";
        public static string SettingsToggleBet(Language lang) => lang==Language.PL? "2) Przełącz pytanie o zakład" : "2) Toggle ask for bet";
        public static string SettingsClearHall(Language lang) => lang==Language.PL? "3) Wyczyść Hall of Fame" : "3) Clear Hall of Fame";
        public static string SettingsBack(Language lang) => lang==Language.PL? "4) Wróć" : "4) Back";
        public static string EasyOption(Language lang) => lang==Language.PL? "1) Łatwy: liczby 1-50" : "1) Easy: numbers 1-50";
        public static string MediumOption(Language lang) => lang==Language.PL? "2) Średni: liczby 1-100" : "2) Medium: numbers 1-100";
        public static string HardOption(Language lang) => lang==Language.PL? "3) Trudny: liczby 1-250" : "3) Hard: numbers 1-250";
        public static string BackOption(Language lang) => lang==Language.PL? "4) Cofnij" : "4) Back";
        public static string LanguageInput(Language lang) => lang==Language.PL? "Wpisz PL lub EN:" : "Type PL or EN:";
    }
}
