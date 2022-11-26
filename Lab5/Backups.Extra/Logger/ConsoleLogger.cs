using Backups.Extra.Interfaces;

namespace Backups.Extra.Logger;

public class ConsoleLogger : ILogger
{
    public ConsoleLogger(ILoggerConfiguration loggerConfiguration)
    {
        LoggerConfiguration = loggerConfiguration;
    }

    public ILoggerConfiguration LoggerConfiguration { get; }
    public void Log(string message)
    {
        Console.WriteLine($"{LoggerConfiguration.Prefix()} - {message}");
    }
}