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
        _electives.Add(electiveGroup); // валидация что уже не добавлен на этот поток
    }

    public void DeleteElective(ElectiveGroup electiveGroup)
    {
        _electives.Remove(electiveGroup); // валидация
    }
}