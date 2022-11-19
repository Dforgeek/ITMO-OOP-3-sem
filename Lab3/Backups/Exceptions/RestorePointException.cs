namespace Backups.Exceptions;

public class RestorePointException : Exception
{
    private RestorePointException() { }

    public static RestorePointException BackupObjectAlreadyInRestorePoint()
    {
        return new RestorePointException();
    }
}