using System;
using System.IO;
using System.Text.Json;
using ZgadnijLiczbe2.Models;

namespace ZgadnijLiczbe2.Services
{
    public class SettingsService
    {
        private const string FileName = "settings.json";
        public GameSettings Settings { get; private set; }

        public SettingsService()
        {
            Load();
        }

        public void Load()
        {
            try
            {
                if (File.Exists(FileName))
                {
                    var text = File.ReadAllText(FileName);
                    Settings = JsonSerializer.Deserialize<GameSettings>(text) ?? new GameSettings();
                }
                else
                {
                    Settings = new GameSettings();
                }
            }
            catch
            {
                Settings = new GameSettings();
            }
        }

        public void Save()
        {
            try
            {
                var txt = JsonSerializer.Serialize(Settings, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(FileName, txt);
            }
            catch { }
        }
    }
}
