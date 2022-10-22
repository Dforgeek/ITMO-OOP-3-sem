using Isu.Extra.Tools;

namespace Isu.Extra.Exceptions;

public class MegaFacultyPrefixException : Exception
{
    private MegaFacultyPrefixException()
        : base()
    {
    }

    public static MegaFacultyPrefixException InvalidPrefix()
    {
        return new MegaFacultyPrefixException();
    }
}