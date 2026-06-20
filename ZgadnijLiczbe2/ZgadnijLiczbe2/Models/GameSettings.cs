using System;

namespace ZgadnijLiczbe2.Models
{
    public enum Language { PL, EN }

    public class GameSettings
    {
        public Language Language { get; set; } = Language.PL;
        public bool AskForBet { get; set; } = true;
        public bool HasAnyGames { get; set; } = false;
    }
}
