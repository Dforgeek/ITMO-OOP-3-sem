using System.Text.RegularExpressions;

namespace Isu.Extra.Entities;

public class DecoratorGroup
{
    public DecoratorGroup(Group group, Shedule groupShedule)
    {
        Group = group;
        Shedule = groupShedule;
    }

    public Shedule Shedule { get; }
    public Group Group { get; }
}