using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using ZgadnijLiczbe2.Models;

namespace ZgadnijLiczbe2.Services
{
    public class HallOfFame
    {
        private const string FileName = "halloffame.json";
        private List<HallEntry> entries = new List<HallEntry>();

        public HallOfFame()
        {
            Load();
        }

        public void Add(HallEntry e)
        {
            entries.Add(e);
            entries = entries
                .GroupBy(x => x.DifficultyMax)
                .SelectMany(g => g
                    .OrderBy(x => x.Attempts)
                    .ThenBy(x => x.DurationSeconds)
                    .ThenBy(x => x.Date)
                    .Take(5))
                .ToList();
            Save();
        }

        public void Clear()
        {
            entries.Clear();
            Save();
        }

        public bool Any() => entries.Any();

        public IEnumerable<HallEntry> TopForDifficulty(int maxNumber, int top = 5)
        {
            return entries
                .Where(x => x.DifficultyMax == maxNumber)
                .OrderBy(x => x.Attempts)
                .ThenBy(x => x.DurationSeconds)
                .ThenBy(x => x.Date)
                .Take(top)
                .ToList();
        }

        public void Load()
        {
            try
            {
                if (File.Exists(FileName))
                {
                    var txt = File.ReadAllText(FileName);
                    entries = JsonSerializer.Deserialize<List<HallEntry>>(txt) ?? new List<HallEntry>();
                }
            }
            catch
            {
                entries = new List<HallEntry>();
            }
        }

        public void Save()
        {
            try
            {
                var txt = JsonSerializer.Serialize(entries, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(FileName, txt);
            }
            catch { }
        }
    }
}
