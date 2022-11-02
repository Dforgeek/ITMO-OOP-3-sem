using Backups.Entities;

namespace Backups.Exceptions;

public class BackupException : Exception
{
    private BackupException() { }

    private BackupException(string message)
        : base(message) { }

    public BackupException RestorePointAlreadyExists()
    {
        throw new BackupException();
    }
}