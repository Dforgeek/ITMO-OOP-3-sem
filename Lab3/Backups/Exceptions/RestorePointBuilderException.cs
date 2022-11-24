namespace Backups.Exceptions;

public class RestorePointBuilderException : Exception
{
    private RestorePointBuilderException() { }

    public static RestorePointBuilderException DateTimeIsNull()
    {
        return new RestorePointBuilderException();
    }

    public static RestorePointBuilderException StorageIsNull()
    {
        return new RestorePointBuilderException();
    }
}