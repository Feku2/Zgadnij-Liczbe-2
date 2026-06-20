# Zgadnij liczbę 2

Konsolowa gra napisana w C# (podejście obiektowe). Jest to sequel klasycznej gry "Zgadnij liczbę" — rozszerzony o tryb "Nowa gra plus", ekran ustawień oraz Hall of Fame zapamiętujące TOP5 wyników.

Start
- Uruchom projekt w Visual Studio lub z katalogu projektu: dotnet run

Krótki opis
- Gra tekstowa: losowana jest liczba z zakresu zależnego od wybranego poziomu trudności. Gracz podaje kolejne liczby aż trafi.

Wymagania obowiązkowe (implementacja)
- Ekran powitalny z opcjami: Nowa gra, Hall of Fame, Ustawienia, Wyjście.
- Ekran Ustawień dostarcza:
  - zmianę języka gry (PL / EN),
  - wyczyszczenie Hall of Fame (wymagane potwierdzenie),
  - przełącznik, czy gra ma pytać o tryb zakładu (domyślnie pytanie jest włączone). Aktualne ustawienia są widoczne na ekranie ustawień.
- Nowa gra: przed rozgrywką wybierasz poziom trudności (Łatwy 1–50, Średni 1–100, Trudny 1–250).
- Tryb zakładu: jeśli ustawienia na to zezwalają, gra zapyta, czy chcesz aktywować tryb zakładu (podaj maksymalną liczbę prób).
- Tryb "Nowa gra plus": alternatywny tryb dostępny przy starcie gry; w nim co 6/7/8 prób ukryta liczba zostaje przelosowana. W trybie Plus tryb zakładu jest niedostępny.
- Po zakończeniu gry (trafieniu) wynik jest zapisywany do Hall of Fame razem z czasem trwania rozgrywki w sekundach. Sortowanie TOP odbywa się po liczbie prób, a przy remisie po czasie (krótszy czas jest wyżej).
- Hall of Fame przechowuje TOP5 dla każdego poziomu trudności i wyróżnia wpisy pochodzące z trybu "Nowa gra plus".

Zachowanie gry (przypomnienie funkcji ze "Zgadnij liczbę 1")
- Po wejściu w nową grę: wybór poziomu trudności.
- Podczas gry na ekranie zawsze wyświetlany jest numer aktualnej próby.
- W przypadku podania nieprawidłowej liczby (nie‑liczby) program prosi o poprawne dane.
- Po każdej nieudanej próbie pokazany jest losowy komunikat mówiący, czy podana liczba jest za mała czy za duża (zbiór co najmniej 5 komunikatów).
- Po trafieniu program prosi o podanie imienia; po wpisaniu imienia gracz wraca do ekranu powitalnego.

Pliki zapisu i konfiguracji
- settings.json — zapisane ustawienia gry (język, czy pytać o tryb zakładu, flaga czy istnieją rozegrane gry).
- halloffame.json — zapis wyników (w pliku przechowywane są maksymalnie TOP5 dla każdego poziomu).

Uwagi techniczne
- Projekt stosuje zasadę: jedna klasa na plik.
- Aplikacja targetuje .NET 10.

Obsługa
1. Sterowanie: cyfry i wpisy tekstowe z klawiatury.
2. W menu wybierz opcję (cyfra) i zatwierdź Enter.
3. W ustawieniach możesz przemieszczać się po opcjach numerami i zmieniać wartości.

Licencja / Kontakt
- Projekt edukacyjny; instrukcje i kod w repozytorium.
