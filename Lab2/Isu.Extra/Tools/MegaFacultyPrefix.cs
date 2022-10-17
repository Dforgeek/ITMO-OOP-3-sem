using Isu.Models;

namespace Isu.Extra.Tools;

public record MegaFacultyPrefix
{
    public MegaFacultyPrefix(GroupName groupName)
    {
        Value = groupName.Name[0];
    }

    public char Value { get; }
}