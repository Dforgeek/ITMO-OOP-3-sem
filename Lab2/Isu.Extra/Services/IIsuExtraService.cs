using System.Data.Common;
using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Tools;
using Isu.Models;

namespace Isu.Extra.Services;

public interface IIsuExtraService
{
    public ElectiveStudent AddElectiveStudentFromExistingStudent(Student student);

    public ElectiveStudent AddElectiveStudent(string name, Group group);

    public ElectiveModule AddElectiveModule(string name, MegaFacultyPrefix prefix);

    public ElectiveModule? FindElectiveModule(Guid electiveModuleId);

    public ElectiveModule GetElectiveModule(Guid electiveModuleId);

    public ElectiveGroup AddElectiveGroup(Guid electiveModuleId, Schedule schedule);

    public ElectiveGroup? FindElectiveGroup(Guid electiveGroupId);

    public ExtraGroup AddExtraGroupFromExistingGroup(Group group, Schedule schedule);

    public ExtraGroup AddExtraGroup(GroupName groupName, Schedule schedule);

    public ExtraGroup? FindExtraGroup(GroupName groupName);

    public ExtraGroup GetExtraGroup(GroupName groupName);

    public ExtraGroup GetExtraGroupOfStudent(Student student);

    public ElectiveStudent AddElectiveStudent(Student student);

    public ElectiveStudent? FindElectiveStudent(int studentId);

    public void AddElectiveStudentToElectiveGroup(ElectiveStudent electiveStudent, ElectiveGroup electiveGroup);

    public void DeleteElectiveStudentFromElectiveGroup(ElectiveStudent electiveStudent, ElectiveGroup electiveGroup);

    public List<ElectiveGroup> GetElectiveGroups(Guid electiveModuleId);

    public List<ElectiveStudent> GetElectiveStudents(Guid electiveGroupId);

    public List<ElectiveStudent> GetStudentsWithoutElectives();
}