using System.Data.Common;
using System.Runtime.CompilerServices;
using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Tools;
using Isu.Models;
using Isu.Services;

namespace Isu.Extra.Services;

public class IsuExtraService
{
    private readonly List<ElectiveModule> _electiveModules;
    private readonly List<ExtraGroup> _extraGroups;
    private readonly List<ElectiveStudent> _electiveStudents;

    public IsuExtraService(IsuService isuService)
    {
        IsuService = isuService;
        _electiveModules = new List<ElectiveModule>();
        _extraGroups = new List<ExtraGroup>();
        _electiveStudents = new List<ElectiveStudent>();
    }

    public IsuExtraService()
    {
        IsuService = new IsuService();
        _electiveModules = new List<ElectiveModule>();
        _extraGroups = new List<ExtraGroup>();
        _electiveStudents = new List<ElectiveStudent>();
    }

    public IsuService IsuService { get; }

    public ElectiveModule AddElectiveModule(string name, MegaFacultyPrefix prefix)
    {
        var newElectiveModule = new ElectiveModule(Guid.NewGuid(), name, prefix);
        if (_electiveModules.Contains(newElectiveModule))
            throw new Exception();
        return newElectiveModule;
    }

    public ElectiveModule? FindElectiveModule(Guid electiveModuleId)
    {
        return _electiveModules.FirstOrDefault(module => module.Id == electiveModuleId);
    }

    public ElectiveModule GetElectiveModule(Guid electiveModuleId)
    {
        ElectiveModule? electiveModule = FindElectiveModule(electiveModuleId);
        if (electiveModule == null)
            throw new Exception();
        return electiveModule;
    }

    public ElectiveGroup AddElectiveGroup(Guid electiveModuleId, Schedule schedule)
    {
        return GetElectiveModule(electiveModuleId).CreateNewElectiveGroup(Guid.NewGuid(), schedule);
    }

    public ElectiveGroup? FindElectiveGroup(Guid electiveGroupId)
    {
        return _electiveModules.Select(electiveModule => electiveModule.FindElectiveGroup(electiveGroupId))
            .FirstOrDefault(electiveGroup => electiveGroup != null);
    }

    public ExtraGroup AddExtraGroupFromExistingGroup(Group group, Schedule schedule)
    {
        _extraGroups.Add(new ExtraGroup(group, schedule));
        return _extraGroups.Last();
    }

    public ExtraGroup AddExtraGroup(GroupName groupName, Schedule schedule)
    {
        return AddExtraGroupFromExistingGroup(IsuService.AddGroup(groupName), schedule);
    }

    public ExtraGroup? FindExtraGroup(GroupName groupName)
    {
        return _extraGroups.FirstOrDefault(extraGroup => extraGroup.Group.GroupName.Name == groupName.Name);
    }

    public ElectiveStudent AddElectiveStudent(Student student)
    {
        if (IsuService.FindStudent(student.Id) == null)
            throw new Exception();
        _electiveStudents.Add(new ElectiveStudent(student));
        return _electiveStudents.Last();
    }

    public ElectiveStudent? FindElectiveStudent(int studentId)
    {
        return _electiveStudents.FirstOrDefault(electiveStudent => electiveStudent.Student.Id == studentId);
    }

    public void AddElectiveStudentToElectiveGroup(ElectiveStudent electiveStudent, ElectiveGroup electiveGroup)
    {
        electiveGroup.AddStudent(electiveStudent); // TODO: Add validation of overlapping
        electiveStudent.AddElective(electiveGroup);
    }

    public void DeleteElectiveStudentFromElectiveGroup(ElectiveStudent electiveStudent, ElectiveGroup electiveGroup)
    {
        electiveGroup.DeleteStudent(electiveStudent);
        electiveStudent.DeleteElective(electiveGroup);
    }

    public List<ElectiveGroup> GetElectiveGroups(Guid electiveModuleId)
    {
        ElectiveModule? electiveModule = FindElectiveModule(electiveModuleId);
        if (electiveModule == null)
            throw new Exception();
        return electiveModule.ElectiveGroups.ToList();
    }

    public List<ElectiveStudent> GetElectiveStudents(Guid electiveGroupId)
    {
        ElectiveGroup? electiveGroup = FindElectiveGroup(electiveGroupId);
        if (electiveGroup == null)
            throw new Exception();
        return electiveGroup.ElectiveStudents.ToList();
    }

    public List<ElectiveStudent> GetStudentsWithoutElectives()
    {
        return _electiveStudents.Where(student => student.Electives.Count == 0).ToList();
    }
}