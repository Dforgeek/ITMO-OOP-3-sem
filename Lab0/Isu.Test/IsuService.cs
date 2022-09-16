using Isu.Entities;
using Isu.Models;
using Xunit;

namespace Isu.Test;

public class IsuService
{
    [Fact]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
    {
        var isu = new Services.IsuService();
        var coolGroupName = new GroupName("M3203");
        Group coolGroup = isu.AddGroup(coolGroupName);
        var coolStudent = isu.AddStudent(coolGroup, "Michael Lopatin");

        Assert.Contains(coolStudent, coolGroup.Students);
    }

    [Fact]
    public void ReachMaxStudentPerGroup_ThrowException()
    {
        var isu = new Services.IsuService();
        isu.AddGroup()
    }

    [Fact]
    public void CreateGroupWithInvalidName_ThrowException() { }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged() { }
}