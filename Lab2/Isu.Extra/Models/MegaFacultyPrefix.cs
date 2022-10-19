using Isu.Models;

namespace Isu.Extra.Tools;

public class MegaFacultyPrefix : IEquatable<MegaFacultyPrefix>
{
    public MegaFacultyPrefix(GroupName groupName)
    {
        Value = groupName.Name[0];
    }

    public char Value { get; }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public virtual bool Equals(MegaFacultyPrefix? other)
    {
        return other is { } && Value.Equals(other.Value);
    }
}