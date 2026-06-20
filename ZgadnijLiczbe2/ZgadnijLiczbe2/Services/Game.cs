using System;
using System.Diagnostics;
using ZgadnijLiczbe2.Models;
using ZgadnijLiczbe2.Utils;

namespace ZgadnijLiczbe2.Services
{
    public class Game
    {
        //abstrakcja
        private readonly int maxNumber;
        private readonly Random rnd = new Random();
        private int secret;
        private int attempts;
        private readonly Stopwatch sw = new Stopwatch();
        private readonly int? maxAttemptsIfBet;
        private readonly bool isPlusMode;
        private int plusInterval;
        private bool lastRerolled = false;
        private readonly Models.Language language;

        public Game(int maxNumber, int? maxAttemptsIfBet = null, bool isPlusMode = false, Models.Language language = Models.Language.PL)
        {
            this.maxNumber = maxNumber;
            this.maxAttemptsIfBet = maxAttemptsIfBet;
            this.isPlusMode = isPlusMode;
            this.language = language;
            plusInterval = isPlusMode ? new int[] { 6, 7, 8 }[rnd.Next(3)] : -1;
            ResetSecret();
        }

        private void ResetSecret()
        {
            secret = rnd.Next(1, maxNumber + 1);
        }

        public (bool finished, string message, int attempts, double duration) Play(Func<string> readLine, Action<string, bool> writeLine, Func<string> readNamePrompt, Func<string, bool> confirm)
        {
            int? previousGuess = null;
            attempts = 0;
            sw.Restart();
            while (true)
            {
                attempts++;

                if (previousGuess.HasValue)
                {
                    string hint;
                    if (lastRerolled)
                    {
                        hint = L.NumberChanged(language);
                        lastRerolled = false;
                    }
                    else
                    {
                        hint = previousGuess.Value > secret ? RandomHighMessage() : RandomLowMessage();
                    }
                    var combined = hint + "\n" + L.Attempt(language, attempts);
                    writeLine(combined, true);
                }
                else
                {
                    writeLine(L.Attempt(language, attempts), true);
                }

                var input = readLine();
                if (!int.TryParse(input, out var guess))
                {
                    writeLine(L.InvalidNumber(language), false);
                    attempts--;
                    continue;
                }

                if (guess == secret)
                {
                    sw.Stop();
                    var duration = sw.Elapsed.TotalSeconds;
                    writeLine(L.CorrectResult(language, attempts, duration), false);
                    writeLine(L.EnterName(language), false);
                    var name = readNamePrompt();
                    return (true, name, attempts, duration);
                }

                if (isPlusMode && plusInterval > 0 && (attempts % plusInterval) == 0)
                {
                    ResetSecret();
                    lastRerolled = true;
                    plusInterval = new int[] { 6, 7, 8 }[rnd.Next(3)];
                }

                previousGuess = guess;

                if (maxAttemptsIfBet.HasValue && attempts >= maxAttemptsIfBet.Value)
                {
                    sw.Stop();
                    writeLine(L.ReachedMax(language), false);
                    return (false, string.Empty, attempts, sw.Elapsed.TotalSeconds);
                }
            }
        }

        private string RandomLowMessage()
        {
            var msgs = L.RandomLows(language);
            return msgs[rnd.Next(msgs.Length)];
        }

        private string RandomHighMessage()
        {
            var msgs = L.RandomHighs(language);
            return msgs[rnd.Next(msgs.Length)];
        }
    }
}
