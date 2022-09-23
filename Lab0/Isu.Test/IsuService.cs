using Isu.Entities;
using Isu.Exceptions;
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
        Student coolStudent = isu.AddStudent(coolGroup, "Michael Lopatin");

        Assert.Contains(coolStudent, coolGroup.Students);
    }

    [Fact]
    public void ReachMaxStudentPerGroup_ThrowException()
    {
        var isu = new Services.IsuService();
        var maxStudentGroupName = new GroupName("M3204");
        Group maxStudentGroup = isu.AddGroup(maxStudentGroupName);
        Assert.Throws<GroupException>(() =>
        {
            for (int i = 0; i < 31; i++)
            {
                isu.AddStudent(maxStudentGroup, $"Stas Baretsky number-{i}");
            }
        });
    }

    [Fact]
    public void CreateGroupWithInvalidName_ThrowException()
    {
        var isu = new Services.IsuService();
        Assert.Throws<GroupNameException>(() => isu.AddGroup(new GroupName("S4403")));
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        var isu = new Services.IsuService();
        Group oldGroup = isu.AddGroup(new GroupName("M3201"));
        Group newGroup = isu.AddGroup(new GroupName("M3200"));
        Student edgar = isu.AddStudent(oldGroup, "Edgar Saratovtsev");
        isu.ChangeStudentGroup(edgar, newGroup);
        Assert.DoesNotContain(edgar, oldGroup.Students);
        Assert.Contains(edgar, newGroup.Students);
    }
}