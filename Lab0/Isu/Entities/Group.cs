using Isu.Exceptions;
using Isu.Models;

namespace Isu.Entities;

public class Group
{
    private const int MaxAmountOfStudents = 30;
    private List<Student> _students = new List<Student>();

    public Group(GroupName groupName)
    {
        GroupNameValue = new GroupName(groupName);
        NumberOfCourse = new CourseNumber(groupName);
    }

    public Group(GroupName groupName, List<Student> students)
    {
        Students = new List<Student>(students);
        GroupNameValue = new GroupName(groupName);
        NumberOfCourse = new CourseNumber(groupName);
    }

    public GroupName? GroupNameValue
    {
        get;
    }

    public CourseNumber NumberOfCourse { get; }

    public List<Student> Students
    {
        get => _students;

        private set
        {
            if (value.Count > MaxAmountOfStudents)
            {
                throw new IsuException("Student limit exceeded");
            }

            _students = new List<Student>(value);
        }
    }

    public void AddStudent(Student newStudent)
    {
        if (_students.Count + 1 > MaxAmountOfStudents)
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
}