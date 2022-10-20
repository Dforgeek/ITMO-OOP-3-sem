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

    public ElectiveGroup CreateNewElectiveGroup(Guid id, Schedule schedule)
    {
        var electiveGroup = new ElectiveGroup(id, MegaFacultyPrefix, schedule);
        if (_electiveGroups.Contains(electiveGroup))
            throw ElectiveModuleException.ElectiveGroupAlreadyExists();

        _electiveGroups.Add(new ElectiveGroup(id, MegaFacultyPrefix, schedule));
        return _electiveGroups.Last();
    }

    public void AddNewElectiveGroup(ElectiveGroup electiveGroup)
    {
        if (_electiveGroups.Contains(electiveGroup))
            throw ElectiveModuleException.ElectiveGroupAlreadyExists();

        _electiveGroups.Add(electiveGroup);
    }

    public ElectiveGroup? FindElectiveGroup(Guid electiveGroupId)
    {
        return _electiveGroups.FirstOrDefault(electiveGroup => electiveGroup.Id == electiveGroupId);
    }

    public bool Equals(ElectiveModule? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _electiveGroups.Equals(other._electiveGroups)
               && Id.Equals(other.Id) && Name == other.Name
               && MegaFacultyPrefix.Equals(other.MegaFacultyPrefix);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ElectiveModule)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_electiveGroups, Id, Name, MegaFacultyPrefix);
    }
}