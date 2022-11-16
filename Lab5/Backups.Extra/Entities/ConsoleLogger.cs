using Backups.Extra.Interfaces;

namespace Backups.Extra.Entities;

public class ConsoleLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine($"{DateTime.Now.ToString("hh:mm:ss")} - {message}");
    }
}