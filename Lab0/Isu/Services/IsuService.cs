using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;

namespace Isu.Services;

public class IsuService : IIsuService
{
    private Dictionary<GroupName, Group> _groups = new Dictionary<GroupName, Group>();

    public IsuService() { }

    public Group AddGroup(GroupName name)
    {
        var newGroup = new Group(name);
        _groups[name] = newGroup;
        return newGroup;
    }

    public Student AddStudent(Group group, string name)
    {
        if (group == null)
        {
            throw new IsuException("Group is a null reference.");
        }

        var newStudent = new Student(name);
        group.AddStudent(newStudent);
        return newStudent;
    }

    public Student GetStudent(int id)
    {
        foreach (Student? searched in from Group @group in _groups
                 select @group.GetStudent(id)
                 into searched
                 where searched != null
                 select searched)
        {
            return searched;
        }

        throw new IsuException("No student with such Id in the Isu Service.");
    }

    public Student? FindStudent(int id)
    {
        return (from Group @group in _groups
            select @group.GetStudent(id)).FirstOrDefault();
    }

    public List<Student> FindStudents(GroupName groupName)
    {
        return _groups[groupName].Students;
    }

    public List<Student> FindStudents(CourseNumber courseNumber)
    {
        var answer = new List<Student>();
        foreach (Group group in _groups.Values
                     .Where(group => group.CourseNumberValue == courseNumber))
        {
            answer.AddRange(group.Students);
        }

        return answer;
    }

    public Group? FindGroup(GroupName groupName)
    {
        return _groups.ContainsKey(groupName) ? _groups[groupName] : null;
    }

    public List<Group> FindGroups(CourseNumber courseNumber)
    {
        return _groups.Values
            .Where(group => group.CourseNumberValue == courseNumber)
            .ToList();
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        Group? oldGroup = _groups.Values.FirstOrDefault(group => group.Students.Contains(student));
        if (oldGroup == null)
        {
            throw new IsuException("This student doesn't exist");
        }

        oldGroup.DeleteStudent(student);
        newGroup.AddStudent(student);
    }
}