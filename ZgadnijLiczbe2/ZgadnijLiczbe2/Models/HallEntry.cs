using System;

namespace ZgadnijLiczbe2.Models
{
    public class HallEntry
    {
        public string PlayerName { get; set; }
        public int Attempts { get; set; }
        public int DifficultyMax { get; set; }
        public double DurationSeconds { get; set; }
        public DateTime Date { get; set; }
        public bool IsPlusMode { get; set; }
    }
}
