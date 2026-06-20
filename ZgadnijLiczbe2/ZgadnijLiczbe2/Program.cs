using ZgadnijLiczbe2.UI;
using ZgadnijLiczbe2.Services;

namespace ZgadnijLiczbe2
{
    class Program
    {
        static void Main(string[] args)
        {
            var settingsService = new SettingsService();
            var hall = new HallOfFame();
            var ui = new ConsoleUI(settingsService, hall);
            ui.Run();
        }
    }
}
