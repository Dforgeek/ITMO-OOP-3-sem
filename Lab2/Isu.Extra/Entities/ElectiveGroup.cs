using Isu.Entities;

namespace Isu.Extra.Entities;

public class ElectiveGroup
{
    private const int MaxAmountOfStudents = 40;
    private readonly List<ExtraStudent> _extraStudents;

    public ElectiveGroup(Schedule schedule)
    {
        _extraStudents = new List<ExtraStudent>();
        Schedule = schedule;
    }

    public Schedule Schedule { get; }

    public IReadOnlyList<ExtraStudent> ExtraStudents => _extraStudents.AsReadOnly();

    public void AddStudent(ExtraStudent newExtraStudent)
    {
        if (_extraStudents.Count == MaxAmountOfStudents)
            throw new Exception();
        _extraStudents.Add(newExtraStudent);
    }

    public void DeleteStudent(ExtraStudent oldExtraStudent)
    {
        if (!_extraStudents.Remove(oldExtraStudent))
            throw new Exception();
    }
}