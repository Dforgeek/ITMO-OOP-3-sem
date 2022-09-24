using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;

namespace Isu.Services;

public class IsuService : IIsuService
{
    private readonly List<Group> _groups;
    private readonly NumberFactory _numberFactory = new NumberFactory();
    public IsuService()
    {
        _groups = new List<Group>();
    }

    public Group AddGroup(GroupName name)
    {
        if (_groups.Exists(group => group.GroupName.Equals(name)))
        {
            throw IsuException.GroupAlreadyExists();
        }

        var newGroup = new Group(name);
        _groups.Add(newGroup);
        return newGroup;
    }

    public Student AddStudent(Group group, string name)
    {
        if (group == null)
        {
            throw GroupException.IsNull();
        }

        var newStudent = new Student(name, _numberFactory.GetNewNumber(), group);
        group.AddStudent(newStudent);
        return newStudent;
    }

    public Student GetStudent(int id)
    {
        Student? student = FindStudent(id);
        if (student == null)
            throw IsuException.NoStudentWithSuchId();
        return student;
    }

    public Student? FindStudent(int id)
    {
        return _groups.SelectMany(group => group.Students).FirstOrDefault(student => student.Id == id);
    }

    public List<Student> FindStudents(GroupName groupName)
    {
        return FindGroup(groupName)?.Students.ToList() ?? new List<Student>();
    }

    public List<Student> FindStudents(CourseNumber courseNumber)
    {
        return FindGroups(courseNumber).SelectMany(group => group.Students).ToList();
    }

    public Group? FindGroup(GroupName groupName)
    {
        return _groups.FirstOrDefault(group => group.GroupName.Equals(groupName));
    }

    public List<Group> FindGroups(CourseNumber courseNumber)
    {
        return _groups.Where(group => group.CourseNumber.Equals(courseNumber)).ToList();
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        if (!_groups.Contains(newGroup))
        {
            throw IsuException.NoSuchGroup();
        }

        Group? oldGroup = _groups.FirstOrDefault(group => group.Students.Contains(student));
        if (oldGroup == null)
        {
            throw IsuException.NoSuchStudent();
        }

        student.ChangeGroup(newGroup);
    }
}