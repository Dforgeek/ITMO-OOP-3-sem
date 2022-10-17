using Isu.Extra.Tools;

namespace Isu.Extra.Entities;

public class ElectiveModule
{
    private readonly List<ElectiveGroup> _electiveGroups;

    private ElectiveModule(Guid id, string name, MegaFacultyPrefix prefix, List<ElectiveGroup> electiveGroups)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new Exception();
        Name = name;
        Id = id;
        MegaFacultyPrefix = prefix;
        _electiveGroups = electiveGroups;
    }

    public Guid Id { get; }
    public string Name { get; }
    public MegaFacultyPrefix MegaFacultyPrefix { get; }

    public IReadOnlyList<ElectiveGroup> ElectiveGroups => _electiveGroups.AsReadOnly();
}