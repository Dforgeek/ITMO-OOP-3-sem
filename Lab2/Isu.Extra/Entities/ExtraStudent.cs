using Isu.Entities;

namespace Isu.Extra.Entities;

public class ExtraStudent
{
    private const int MaxAmountOfElectives = 2;
    private readonly List<ElectiveGroup> _electives;

    public ExtraStudent(Student student)
    {
        Student = student;
        _electives = new List<ElectiveGroup>();
    }

    public Student Student { get; }
    public IReadOnlyList<ElectiveGroup> Electives => _electives.AsReadOnly();

    public void AddElective(ElectiveGroup electiveGroup)
    {
        if (_electives.Contains(electiveGroup))
            throw new Exception();
        _electives.Add(electiveGroup);
        if (!electiveGroup.ExtraStudents.Contains(this))
            electiveGroup.AddStudent(this);
    }

    public void DeleteElective(ElectiveGroup electiveGroup)
    {
        if (!_electives.Contains(electiveGroup))
            throw new Exception();
        _electives.Remove(electiveGroup);
        if (electiveGroup.ExtraStudents.Contains(this))
            electiveGroup.DeleteStudent(this);
    }
}