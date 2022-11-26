namespace Backups.Extra.Exceptions;

public class RestorePointControlException : Exception
{
    private RestorePointControlException() { }

    public static RestorePointControlException NegativeLimit()
    {
        return new RestorePointControlException();
    }

    public static RestorePointControlException NoRestorePointControlsWereProvidedForHybrid()
    {
        return new RestorePointControlException();
    }

    public static RestorePointControlException IncorrectCriteriaForHybrid()
    {
        return new RestorePointControlException();
    }
}