using Isu.Exceptions;
using Isu.Models;

namespace Isu.Entities;

public class Group : IEquatable<Group>
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
            throw GroupException.LimitOfStudentsExceeded();
        _students = new List<Student>(students);
        GroupName = groupName;
        CourseNumber = groupName.CourseNumber;
    }

    public GroupName GroupName { get; }

    public CourseNumber CourseNumber { get; }

    public IReadOnlyCollection<Student> Students => _students.AsReadOnly();

    public void AddStudent(Student newStudent)
    {
        if (newStudent == null)
            throw StudentException.IsNull();
        if (_students.Count == MaxAmountOfStudents)
            throw GroupException.LimitOfStudentsExceeded();

        _students.Add(newStudent);
    }

    public void DeleteStudent(Student oldStudent)
    {
        if (!_students.Remove(oldStudent))
            throw GroupException.NoSuchStudent();
    }

    public bool Equals(Group? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _students.Equals(other._students) && GroupName.Equals(other.GroupName) &&
               CourseNumber.Equals(other.CourseNumber);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Group)obj);
    }

    public override int GetHashCode()
    {
        return GroupName.GetHashCode();
    }
}