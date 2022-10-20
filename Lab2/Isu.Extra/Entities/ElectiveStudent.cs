using Isu.Entities;
using Isu.Extra.Exceptions;
using Isu.Extra.Tools;

namespace Isu.Extra.Entities;

public class ElectiveStudent
{
    private const int MaxAmountOfElectives = 2;
    private readonly List<ElectiveGroup> _electives;

    public ElectiveStudent(Student student)
    {
        Student = student;
        _electives = new List<ElectiveGroup>();
    }

    public Group Group => Student.Group;
    public MegaFacultyPrefix MegaFacultyPrefix => new (Group.GroupName);
    public IReadOnlyList<ElectiveGroup> Electives => _electives.AsReadOnly();
    public Student Student { get; }

    public void AddElective(ElectiveGroup electiveGroup)
    {
        if (_electives.Count == MaxAmountOfElectives)
            throw ElectiveStudentException.LimitOfElectiveGroupsExceeded();
        if (_electives.Contains(electiveGroup))
            throw ElectiveStudentException.StudentAlreadyHasThisElective();
        _electives.Add(electiveGroup);
    }

    public void DeleteElective(ElectiveGroup electiveGroup)
    {
        if (!_electives.Contains(electiveGroup))
            throw ElectiveStudentException.NoSuchElective();
        _electives.Remove(electiveGroup);
    }
}