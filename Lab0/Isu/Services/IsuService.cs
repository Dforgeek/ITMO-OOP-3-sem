using Isu.Entities;
using Isu.Models;

namespace Isu.Services;

public class IsuService : IIsuService
{
    private List<Group> _groups = new List<Group>();

    public IsuService() { }

    public Group AddGroup(GroupName name)
    {
        var newGroup = new Group(name);
        _groups.Add(new Group(name));
        return newGroup;
    }

    public Student AddStudent(Group group, string name)
    {
        GroupName desiredGroup = new GroupName(group.)
        var newStudent = new Student(name);
        foreach (var i in _groups)
        {
            
        }

        return newStudent;
    }

}