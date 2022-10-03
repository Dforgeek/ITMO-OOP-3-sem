using System.Runtime.CompilerServices;
using Isu.Entities;

namespace Isu.Exceptions;

public class StudentException : Exception
{
    private StudentException() { }

    private StudentException(string message)
        : base(message)
    {
    }

    public static StudentException EmptyName()
    {
        return new StudentException("Name for student is empty or null.");
    }

    public static StudentException IsNull()
    {
        return new StudentException("Student is null.");
    }
}