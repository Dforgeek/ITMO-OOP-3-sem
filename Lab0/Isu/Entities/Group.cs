using Isu.Exceptions;
using Isu.Models;

namespace Isu.Entities;

public class Group
{
    private const int MaxAmountOfStudents = 30;
    private readonly List<Student> _students = new ();

    public Group(GroupName groupName)
    {
        GroupName = groupName;
        CourseNumber = groupName.CourseNumber;
    }

    public Group(GroupName groupName, List<Student> students)
    {
        if (students.Count > MaxAmountOfStudents)
            throw new IsuException("Limit of students exceeded");
        _students = new List<Student>(students);
        GroupName = groupName;
        CourseNumber = groupName.CourseNumber;
    }

    public GroupName GroupName { get; }

    public CourseNumber CourseNumber { get; }

    public IReadOnlyCollection<Student> Students => _students.AsReadOnly();

    public void AddStudent(Student newStudent)
    {
        if (_students.Count == MaxAmountOfStudents)
            throw new IsuException("Student limit exceeded");
        _students.Add(newStudent);
    }

    public void DeleteStudent(Student oldStudent)
    {
        if (!_students.Contains(oldStudent))
        {
            throw new IsuException("No such student to delete");
        }

        _students.Remove(oldStudent);
    }

    public override bool Equals(object? obj)
    {
        if (obj is Group group)
            return group.GroupName.Equals(GroupName);
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_students, GroupName, CourseNumber);
    }
}