using Backups.Extra.Interfaces;

namespace Backups.Extra.Logger;

public class FileLogger : ILogger
{
    public FileLogger(string path, ILoggerConfiguration loggerConfiguration)
    {
        Path = path;
        LoggerConfiguration = loggerConfiguration;
    }

    public string Path { get; }
    public ILoggerConfiguration LoggerConfiguration { get; }

    public void Log(string message)
    {
        File.AppendAllText(Path, $"{LoggerConfiguration.Prefix()} - {message}");
    }
}