using System;
using System.Linq;
using ZgadnijLiczbe2.Models;
using ZgadnijLiczbe2.Services;
using ZgadnijLiczbe2.Utils;

namespace ZgadnijLiczbe2.UI
{
    public class ConsoleUI
    {
        private readonly SettingsService settings;
        private readonly HallOfFame hall;
        private readonly Notifier notifier;

        public ConsoleUI(SettingsService settings, HallOfFame hall)
        {
            this.settings = settings;
            this.hall = hall;
            //polimorfizm
            this.notifier = new Services.ConsoleNotifier();
        }

        private void SettingsMenu()
        {
            var lang = settings.Settings.Language;
            while (true)
            {
                Console.Clear();
                var content = new[] {
                    L.SettingsTitle(lang),
                    L.LangOption(lang) + " " + settings.Settings.Language,
                    L.AskBetOption(lang) + " " + (settings.Settings.AskForBet ? "tak" : "nie"),
                    "",
                    L.SettingsChangeLanguage(lang),
                    L.SettingsToggleBet(lang),
                    L.SettingsClearHall(lang),
                    L.SettingsBack(lang)
                };
                PrintBox(content);
                Console.Write(L.Choose(lang) + " ");
                var c = Console.ReadLine();
                if (c == "1")
                {
                    Console.Write("PL or EN: ");
                    var v = Console.ReadLine();
                    if (v?.ToUpper() == "EN") settings.Settings.Language = Models.Language.EN;
                    else settings.Settings.Language = Models.Language.PL;
                    settings.Save();
                    lang = settings.Settings.Language;
                }
                else if (c == "2")
                {
                    settings.Settings.AskForBet = !settings.Settings.AskForBet;
                    settings.Save();
                    lang = settings.Settings.Language;
                }
                else if (c == "3")
                {
                    Console.Write(L.Confirm(lang) + " ");
                    var v = Console.ReadLine();
                    if (v?.ToLower() == "tak" || v?.ToLower() == "yes")
                    {
                        hall.Clear();
                    }
                }
                else if (c == "4")
                {
                    return;
                }
            }
        }

        public void Run()
        {
            WelcomeMenu();
        }

        private void PrintBox(string[] lines, int width = 60)
        {
            var pad = width - 2;
            var border = new string('=', width);
            Console.WriteLine();
            Console.WriteLine(border);
            foreach (var raw in lines)
            {
                var line = raw ?? string.Empty;
                if (line.Length > pad) line = line.Substring(0, pad);
                var left = (pad - line.Length) / 2;
                var right = pad - line.Length - left;
                Console.WriteLine("|" + new string(' ', left) + line + new string(' ', right) + "|");
            }
            Console.WriteLine(border + "\n");
        }

        private void WelcomeMenu()
        {
            while (true)
            {
                Console.Clear();
                var lang = settings.Settings.Language;
                var menuLines = new[] { L.Welcome(lang) }.Concat(L.MenuOptions(lang).Split('\n')).ToArray();
                PrintBox(menuLines);
                Console.Write(L.Choose(lang) + " ");
                var choice = Console.ReadLine();

                if (choice == "1")
                {
                    DifficultyMenu();
                }
                else if (choice == "2")
                {
                    if (hall.TopForDifficulty(50).Any() || hall.TopForDifficulty(100).Any() || hall.TopForDifficulty(250).Any())
                        ScoreboardMenu();
                }
                else if (choice == "3")
                {
                    SettingsMenu();
                }
                else if (choice == "4" || choice == "0")
                {
                    return;
                }
            }
        }

        private void DifficultyMenu()
        {
            while (true)
            {
                Console.Clear();
                var lang = settings.Settings.Language;
                var lines = new[] { L.DifficultyTitle(lang), "", L.EasyOption(lang), L.MediumOption(lang), L.HardOption(lang), L.BackOption(lang) };
                PrintBox(lines);
                var choice = Console.ReadLine();

                if (choice == "1" || choice == "2" || choice == "3")
                {
                    int attempts = PlayGame(choice);
                    if (attempts < 0) return;

                    Console.Clear();
                    var levels = new[] { "Easy", "Medium", "Hard" };
                    Console.WriteLine(L.WinMessage(lang, attempts, levels[int.Parse(choice) - 1]));
                    Console.WriteLine(L.ResultSaved(lang));
                    Console.WriteLine(L.PressEnter(lang));
                    Console.ReadLine();
                }
                if (choice == "4")
                    break;
            }
        }

        private int PlayGame(string difficulty)
        {
            int max = 100;
            if (difficulty == "1") max = 50;
            else if (difficulty == "2") max = 100;
            else if (difficulty == "3") max = 250;
            var lang = settings.Settings.Language;
            Console.Clear();
            PrintBox(new[] { L.ChooseGameMode(lang) });
            var mode = Console.ReadLine();
            bool isPlus = mode == "2";

            int? maxTries = null;
            if (!isPlus && settings.Settings.AskForBet)
            {
                Console.Write(L.AskForBet(lang) + " ");
                var betChoice = Console.ReadLine();
                if (int.TryParse(betChoice, out var mv)) maxTries = mv + 1;
            }

            Console.Clear();
            var gameInstance = new Services.Game(max, maxTries, isPlus, lang);
            var result = gameInstance.Play(
                readLine: () => Console.ReadLine() ?? string.Empty,
                writeLine: (s, clear) => {
                    if (clear)
                    {
                        Console.Clear();
                        var cleaned = (s ?? string.Empty).Replace("\r", "");
                        var parts = cleaned.Split('\n');
                        PrintBox(parts, 60);
                    }
                    else
                    {
                        Console.WriteLine(s);
                    }
                },
                readNamePrompt: () => Console.ReadLine() ?? "Anonymous",
                confirm: s => true
            );

            if (result.finished)
            {
                var entry = new HallEntry
                {
                    PlayerName = result.message,
                    Attempts = result.attempts,
                    DifficultyMax = max,
                    DurationSeconds = result.duration,
                    Date = DateTime.Now,
                    IsPlusMode = isPlus
                };
                SaveResult(entry);
                return result.attempts;
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine(L.PressAny(lang));
                Console.ReadLine();
                return -1;
            }
        }

        private void SaveResult(HallEntry entry)
        {
            hall.Add(entry);
            settings.Settings.HasAnyGames = true;
            settings.Save();
            var lang = settings.Settings.Language;
            //polimorfizm w użyciu
            notifier.Notify(L.ResultSaved(lang));
        }

        private void ShowTop5(int level)
        {
            Console.Clear();
            var levels = new[] { "Easy  ", "Medium", "Hard  " };
            var lang = settings.Settings.Language;
            var headerLines = new[] { L.HallTitle(lang), levels[level - 1] };
            PrintBox(headerLines);

            var max = level == 1 ? 50 : level == 2 ? 100 : 250;
            var temp_vect = hall.TopForDifficulty(max).ToList();
            if (temp_vect.Any())
            {
                Console.WriteLine(L.PlaceHeaderWithTime(lang) + "\n");
                // columns: Place(3) Attempts(8) Time(s)(8) Name( -20 ) Mark
                for (int i = 0; i < temp_vect.Count; i++)
                {
                    var entry = temp_vect[i];
                    var mark = entry.IsPlusMode ? "*" : " ";
                    var place = (i + 1).ToString().PadLeft(2) + ".";
                    var attemptsStr = entry.Attempts.ToString().PadLeft(6);
                    var timeStr = entry.DurationSeconds.ToString("F1").PadLeft(8);
                    var name = entry.PlayerName ?? "";
                    if (name.Length > 20) name = name.Substring(0, 20);
                    var nameFormatted = name.PadRight(20);
                    Console.WriteLine($"{place}   {attemptsStr}   {timeStr}   {nameFormatted} {mark}");
                }
                Console.WriteLine("\n" + L.PlusDenote(lang));
            }
            else
            {
                Console.WriteLine("\n" + L.NoRecordsMsg(lang) + "\n");
            }
            Console.WriteLine("\n\n" + L.PressAny(lang));
            Console.ReadLine();
        }

        private void ScoreboardMenu()
        {
            while (true)
            {
                Console.Clear();
                var lang = settings.Settings.Language;
                var lines = new[] { L.DifficultyTitle(lang), "", L.EasyOption(lang), L.MediumOption(lang), L.HardOption(lang), L.BackOption(lang) };
                PrintBox(lines);
                var scoreboardChoice = Console.ReadLine();

                if (scoreboardChoice == "4")
                {
                    return;
                }
                if (scoreboardChoice == "1" || scoreboardChoice == "2" || scoreboardChoice == "3")
                {
                    ShowTop5(int.Parse(scoreboardChoice));
                }
            }
        }
    }
}
