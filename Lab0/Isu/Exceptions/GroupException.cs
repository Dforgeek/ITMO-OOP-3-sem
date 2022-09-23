using System.Reflection.Metadata.Ecma335;
using Isu.Entities;

namespace Isu.Exceptions;

public class GroupException : Exception
{
    private GroupException() { }

    private GroupException(string message)
        : base(message)
    {
    }

    public static GroupException LimitOfStudentsExceeded()
    {
        return new GroupException();
    }

    public static GroupException IsNull()
    {
        return new GroupException();
    }

    public static GroupException NoSuchStudent()
    {
        return new GroupException("No such student in group");
    }
}