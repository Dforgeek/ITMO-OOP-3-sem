namespace Isu.Exceptions;

public class IsuException : Exception
{
    private IsuException() { }

    private IsuException(string message)
        : base(message) { }

    public static IsuException GroupAlreadyExists()
    {
        return new IsuException();
    }

    public static IsuException NoStudentWithSuchId()
    {
        return new IsuException();
    }

    public static IsuException NoSuchGroup()
    {
        return new IsuException();
    }

    public static IsuException NoSuchStudent()
    {
        return new IsuException();
    }
}
