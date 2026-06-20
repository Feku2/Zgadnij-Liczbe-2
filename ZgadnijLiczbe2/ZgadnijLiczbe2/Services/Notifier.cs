using System;
using System.IO;

namespace ZgadnijLiczbe2.Services
{
    public abstract class Notifier
    {
        public abstract void Notify(string message);
    }
    //dziedziczenie i polimorfizm
    public class ConsoleNotifier : Notifier
    {
        public override void Notify(string message)
        {
            Console.WriteLine(message);
        }
    }

    public class FileNotifier : Notifier
    {
        private readonly string filePath = "notifications.log";
        public override void Notify(string message)
        {
            try
            {
                File.AppendAllText(filePath, DateTime.Now + ": " + message + Environment.NewLine);
            }
            catch { }
        }
    }
}
