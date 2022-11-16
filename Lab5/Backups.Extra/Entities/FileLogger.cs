using Backups.Extra.Interfaces;

namespace Backups.Extra.Entities;

public class FileLogger : ILogger
{
    public FileLogger(string path)
    {
        Path = path;
    }

    public string Path { get; }

    public void Log(string message)
    {
        File.AppendAllText(Path, $"{DateTime.Now.ToString("hh:mm:ss")} - {message}");
    }
}