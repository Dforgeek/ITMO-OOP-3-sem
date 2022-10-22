using Isu.Entities;
using Isu.Extra.Tools;

namespace Isu.Extra.Entities;

public class ExtraGroup
{
    public ExtraGroup(Group group, Schedule groupSchedule)
    {
        Group = group;
        Schedule = groupSchedule;
        MegaFacultyPrefix = new MegaFacultyPrefix(group.GroupName);
    }

    public Schedule Schedule { get; }
    public Group Group { get; }

    public MegaFacultyPrefix MegaFacultyPrefix { get; }
}