using Isu.Extra.Exceptions;
using Isu.Extra.Tools;

namespace Isu.Extra.Entities;

public class ElectiveModule : IEquatable<ElectiveModule>
{
    private readonly List<ElectiveGroup> _electiveGroups;

    public ElectiveModule(Guid id, string name, MegaFacultyPrefix prefix)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw ElectiveModuleException.NameIsNullOrEmpty();
        Name = name;
        Id = id;
        MegaFacultyPrefix = prefix;
        _electiveGroups = new List<ElectiveGroup>();
    }

    public Guid Id { get; }
    public string Name { get; }
    public MegaFacultyPrefix MegaFacultyPrefix { get; }

    public IReadOnlyList<ElectiveGroup> ElectiveGroups => _electiveGroups.AsReadOnly();

    public ElectiveGroup CreateNewElectiveGroup(Schedule schedule)
    {
        var electiveGroup = new ElectiveGroup(Guid.NewGuid(), MegaFacultyPrefix, schedule);
        _electiveGroups.Add(electiveGroup);
        return electiveGroup;
    }

    public void AddNewElectiveGroup(ElectiveGroup electiveGroup)
    {
        if (_electiveGroups.Contains(electiveGroup))
            throw ElectiveModuleException.ElectiveGroupAlreadyExists();

        _electiveGroups.Add(electiveGroup);
    }

    public bool Equals(ElectiveModule? other)
    {
        if (ReferenceEquals(null, other)) return false;
        return ReferenceEquals(this, other) || Id.Equals(other.Id);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((ElectiveModule)obj);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}