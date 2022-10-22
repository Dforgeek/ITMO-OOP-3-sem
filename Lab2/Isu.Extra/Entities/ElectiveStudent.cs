using Isu.Entities;
using Isu.Extra.Exceptions;
using Isu.Extra.Tools;

namespace Isu.Extra.Entities;

public class ElectiveStudent
{
    private const int MaxAmountOfElectives = 2;
    private readonly List<ElectiveGroup> _electives;
    private readonly Student _student;

    public ElectiveStudent(Student student)
    {
        _student = student;
        _electives = new List<ElectiveGroup>();
    }

    public int Id => _student.Id;
    public Group Group => _student.Group;
    public string Name => _student.Name;
    public MegaFacultyPrefix MegaFacultyPrefix => new (Group.GroupName);
    public IReadOnlyList<ElectiveGroup> Electives => _electives.AsReadOnly();

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

    public bool ElectiveSchedulesOverlap(Schedule schedule)
    {
        return _electives
            .Any(electiveStudentElective =>
                electiveStudentElective.Schedule.ScheduleOverlap(schedule));
    }
}