using Backups.Extra.Interfaces;

namespace Backups.Extra.Logger;

public class DateTimeConfiguration : ILoggerConfiguration
{
    public string Prefix()
    {
        return DateTime.Now.ToString("hh:mm:ss");
    }
}