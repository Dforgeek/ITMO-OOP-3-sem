namespace Backups.Exceptions;

public class BackupTaskException : Exception
{
    private BackupTaskException() { }

    public static BackupTaskException NoSuchBackupObject()
    {
        return new BackupTaskException();
    }
}