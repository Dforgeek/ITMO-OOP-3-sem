using Isu.Entities;

namespace Isu.Exceptions;

public class GroupNameException : Exception
{
    private GroupNameException(string message)
        : base(message) { }

    public static GroupNameException InvalidName()
    {
        return new GroupNameException("Invalid name for group.");
    }

    public static GroupNameException IsNull()
    {
        return new GroupNameException("GroupName is null.");
    }
}