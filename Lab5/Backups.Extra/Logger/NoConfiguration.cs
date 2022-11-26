using Backups.Extra.Interfaces;

namespace Backups.Extra.Logger;

public class NoConfiguration : ILoggerConfiguration
{
    public string Prefix()
    {
        return string.Empty;
    }
}