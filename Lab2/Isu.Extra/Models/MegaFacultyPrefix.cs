using Isu.Extra.Exceptions;
using Isu.Models;

namespace Isu.Extra.Tools;

public class MegaFacultyPrefix : IEquatable<MegaFacultyPrefix>
{
    private const char MinFacultyLetter = 'A';
    private const char MaxFacultyLetter = 'Z';
    public MegaFacultyPrefix(GroupName groupName)
    {
        Value = groupName.Name[0];
    }

    public MegaFacultyPrefix(char prefix)
    {
        if (prefix is < MinFacultyLetter or > MaxFacultyLetter)
            throw MegaFacultyPrefixException.InvalidPrefix();
        Value = prefix;
    }

    public char Value { get; }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public virtual bool Equals(MegaFacultyPrefix? other)
    {
        return other?.Value.Equals(Value) ?? false;
    }
}