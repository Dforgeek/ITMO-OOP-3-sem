namespace Isu.Exceptions;

public class IsuException : Exception
{
    private IsuException() { }

    private IsuException(string message)
        : base(message) { }

    public static IsuException GroupAlreadyExists()
    {
        return new IsuException("Such group already exists.");
    }

    public static IsuException NoStudentWithSuchId()
    {
        return new IsuException("There is no student with such Id.");
    }

    public static IsuException NoSuchGroup()
    {
        return new IsuException("There is no such group in Isu service.");
    }

    public static IsuException NoSuchStudent()
    {
        return new IsuException("The is no such student in Isu service.");
    }
}
