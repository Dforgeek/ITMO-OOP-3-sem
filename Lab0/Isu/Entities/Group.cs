using Isu.Exceptions;
using Isu.Models;

namespace Isu.Entities;

public class Group
{
    private const int MaxAmountOfStudents = 30;
    private List<Student> _students = new List<Student>();
    private GroupName _groupName;
    private CourseNumber _courseNumber;

    public Group(List<Student> students, GroupName groupName, CourseNumber courseNumber)
    {
        Students = students;
        _groupName = groupName;
        _courseNumber = courseNumber;
    }

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
}