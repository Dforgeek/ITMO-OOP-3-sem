using Isu.Entities;
using Isu.Models;
using Isu.Services;

namespace Isu.Extra.Services;

public class IsuExtraService : IIsuService
{
    private IsuService _isuService;

    public IsuExtraService()
    {
        _isuService = new IsuService();
    }

    public Group AddGroup(GroupName name)
    {
        return _isuService.AddGroup(name);
    }

    public Student AddStudent(Group group, string name)
    {
        return _isuService.AddStudent(group, name);
    }

    public Student GetStudent(int id)
    {
        return _isuService.GetStudent(id);
    }

    public Student? FindStudent(int id)
    {
        return _isuService.FindStudent(id);
    }

    public List<Student> FindStudents(GroupName groupName)
    {
        return _isuService.FindStudents(groupName);
    }

    public List<Student> FindStudents(CourseNumber courseNumber)
    {
        return _isuService.FindStudents(courseNumber);
    }

    public Group? FindGroup(GroupName groupName)
    {
        return _isuService.FindGroup(groupName);
    }

    public List<Group> FindGroups(CourseNumber courseNumber)
    {
        return _isuService.FindGroups(courseNumber);
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        _isuService.ChangeStudentGroup(student, newGroup);
    }
}