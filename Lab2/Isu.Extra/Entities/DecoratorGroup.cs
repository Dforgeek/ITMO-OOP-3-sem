using Isu.Entities;
using Isu.Extra.Tools;

namespace Isu.Extra.Entities;

public class DecoratorGroup
{
    public DecoratorGroup(Group group, Shedule groupShedule)
    {
        Group = group;
        Shedule = groupShedule;
        MegaFacultyPrefix = new MegaFacultyPrefix(group.GroupName);
    }

    public Shedule Shedule { get; }
    public Group Group { get; }

    public MegaFacultyPrefix MegaFacultyPrefix { get; }
}