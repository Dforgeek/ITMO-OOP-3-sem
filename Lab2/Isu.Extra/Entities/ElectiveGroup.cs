using Isu.Entities;
using Isu.Extra.Exceptions;
using Isu.Extra.Tools;
using Isu.Models;

namespace Isu.Extra.Entities;

public class ElectiveGroup : IEquatable<ElectiveGroup>
{
    private const int MaxAmountOfStudents = 40;
    private readonly List<ElectiveStudent> _electiveStudents;

    public ElectiveGroup(Guid id, MegaFacultyPrefix prefix, Schedule schedule)
    {
        MegaFacultyPrefix = prefix;
        Id = id;
        _electiveStudents = new List<ElectiveStudent>();
        Schedule = schedule;
    }

    public MegaFacultyPrefix MegaFacultyPrefix { get; }
    public Guid Id { get; }
    public Schedule Schedule { get; private set; }
    public IReadOnlyList<ElectiveStudent> ElectiveStudents => _electiveStudents.AsReadOnly();

    public void AddStudent(ElectiveStudent newElectiveStudent)
    {
        if (_electiveStudents.Count == MaxAmountOfStudents)
            throw ElectiveGroupException.LimitOfElectiveStudentsExceeded();

        if (newElectiveStudent.MegaFacultyPrefix.Equals(MegaFacultyPrefix))
            throw ElectiveGroupException.MegaFacultyIsTheSameForStudentAndElective();

        if (_electiveStudents.Contains(newElectiveStudent))
            throw ElectiveGroupException.ElectiveGroupAlreadyHasThisStudent();

        _electiveStudents.Add(newElectiveStudent);
    }

    public void DeleteStudent(ElectiveStudent oldElectiveStudent)
    {
        if (!_electiveStudents.Remove(oldElectiveStudent))
            throw ElectiveGroupException.NoSuchStudent();
    }

    public bool Equals(ElectiveGroup? other)
    {
        if (ReferenceEquals(null, other)) return false;
        return ReferenceEquals(this, other) || Id.Equals(other.Id);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((ElectiveGroup)obj);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}