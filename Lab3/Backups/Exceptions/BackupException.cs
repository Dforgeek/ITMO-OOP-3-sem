namespace Backups.Exceptions;

public class BackupException : Exception
{
    private BackupException() { }

    public static BackupException RestorePointAlreadyInBackup()
    {
        return new BackupException();
    }

    public static BackupException NoSuchRestorePoint()
    {
        return new BackupException();
    }
}