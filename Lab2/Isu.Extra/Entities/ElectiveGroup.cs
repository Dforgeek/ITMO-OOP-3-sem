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

    public void ChangeSchedule(Schedule newSchedule)
    {
        Schedule = newSchedule;
    }

    public ElectiveStudent? FindElectiveStudent(int id)
    {
        return _electiveStudents.FirstOrDefault(student => student.Student.Id == id);
    }

    public bool Equals(ElectiveGroup? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _electiveStudents.Equals(other._electiveStudents)
               && MegaFacultyPrefix.Equals(other.MegaFacultyPrefix)
               && Id.Equals(other.Id) && Schedule.Equals(other.Schedule);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ElectiveGroup)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_electiveStudents, MegaFacultyPrefix, Id, Schedule);
    }
}