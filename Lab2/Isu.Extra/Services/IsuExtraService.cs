using Isu.Entities;
using Isu.Exceptions;
using Isu.Extra.Entities;
using Isu.Extra.Exceptions;
using Isu.Extra.Tools;
using Isu.Models;
using Isu.Services;

namespace Isu.Extra.Services;

public class IsuExtraService : IIsuExtraService, IIsuService
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

    private IsuService IsuService { get; }

    public ElectiveStudent AddElectiveStudentFromExistingStudent(Student student)
    {
        if (IsuService.FindStudent(student.Id) == null)
            throw IsuException.NoStudentWithSuchId();
        _electiveStudents.Add(new ElectiveStudent(student));
        return _electiveStudents.Last();
    }

    public ElectiveStudent AddElectiveStudent(string name, Group group)
    {
        return AddElectiveStudentFromExistingStudent(IsuService.AddStudent(group, name));
    }

    public ElectiveModule AddElectiveModule(string name, MegaFacultyPrefix prefix)
    {
        var newElectiveModule = new ElectiveModule(Guid.NewGuid(), name, prefix);
        if (_electiveModules.Contains(newElectiveModule))
            throw IsuExtraException.ElectiveModuleAlreadyExists();
        _electiveModules.Add(newElectiveModule);
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
            throw IsuExtraException.NoSuchElectiveModule();
        return electiveModule;
    }

    public ElectiveGroup AddElectiveGroup(Guid electiveModuleId, Schedule schedule)
    {
        return GetElectiveModule(electiveModuleId).CreateNewElectiveGroup(schedule);
    }

    public ElectiveGroup? FindElectiveGroup(Guid electiveGroupId)
    {
        return _electiveModules
            .Select(electiveModule => electiveModule.ElectiveGroups
                .FirstOrDefault(electiveGroup => electiveGroup.Id == electiveGroupId))
            .FirstOrDefault(electiveGroup => electiveGroup != null);
    }

    public ExtraGroup AddExtraGroupFromExistingGroup(Group group, Schedule schedule)
    {
        if (IsuService.FindGroup(group.GroupName) == null)
            throw IsuException.NoSuchGroup();
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

    public ExtraGroup GetExtraGroup(GroupName groupName)
    {
        ExtraGroup? extraGroup = FindExtraGroup(groupName);
        if (extraGroup == null)
            throw IsuExtraException.NoSuchExtraGroup();
        return extraGroup;
    }

    public ExtraGroup GetExtraGroupOfStudent(Student student)
    {
        GroupName groupName = student.Group.GroupName;
        return GetExtraGroup(groupName);
    }

    public ElectiveStudent AddElectiveStudent(Student student)
    {
        if (IsuService.FindStudent(student.Id) == null)
            throw IsuExtraException.NoSuchStudent();
        _electiveStudents.Add(new ElectiveStudent(student));
        return _electiveStudents.Last();
    }

    public ElectiveStudent? FindElectiveStudent(int studentId)
    {
        return _electiveStudents.FirstOrDefault(electiveStudent => electiveStudent.Id == studentId);
    }

    public void AddElectiveStudentToElectiveGroup(ElectiveStudent electiveStudent, ElectiveGroup electiveGroup)
    {
        if (electiveStudent.ElectiveSchedulesOverlap(electiveGroup.Schedule))
            throw IsuExtraException.ElectiveSchedulesOverlap();

        if (GetExtraGroup(electiveStudent.Group.GroupName).Schedule.ScheduleOverlap(electiveGroup.Schedule))
            throw IsuExtraException.ElectiveSchedulesOverlap();
        electiveGroup.AddStudent(electiveStudent);
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
            throw IsuExtraException.NoSuchElectiveModule();

        return electiveModule.ElectiveGroups.ToList();
    }

    public List<ElectiveStudent> GetElectiveStudents(Guid electiveGroupId)
    {
        ElectiveGroup? electiveGroup = FindElectiveGroup(electiveGroupId);
        if (electiveGroup == null)
            throw IsuExtraException.NoSuchElectiveGroup();

        return electiveGroup.ElectiveStudents.ToList();
    }

    public List<ElectiveStudent> GetStudentsWithoutElectives()
    {
        return _electiveStudents.Where(student => student.Electives.Count == 0).ToList();
    }

    public Group AddGroup(GroupName name)
    {
        return IsuService.AddGroup(name);
    }

    public Student AddStudent(Group group, string name)
    {
        return IsuService.AddStudent(group, name);
    }

    public Student GetStudent(int id)
    {
        return IsuService.GetStudent(id);
    }

    public Student? FindStudent(int id)
    {
        return IsuService.FindStudent(id);
    }

    public List<Student> FindStudents(GroupName groupName)
    {
        return IsuService.FindStudents(groupName);
    }

    public List<Student> FindStudents(CourseNumber courseNumber)
    {
        return IsuService.FindStudents(courseNumber);
    }

    public Group? FindGroup(GroupName groupName)
    {
        return IsuService.FindGroup(groupName);
    }

    public List<Group> FindGroups(CourseNumber courseNumber)
    {
        return IsuService.FindGroups(courseNumber);
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        ChangeStudentExtraGroupValidation(student, GetExtraGroup(newGroup.GroupName));
        IsuService.ChangeStudentGroup(student, newGroup);
    }

    private void ChangeStudentExtraGroupValidation(Student student, ExtraGroup newExtraGroup)
    {
        ElectiveStudent? electiveStudent = FindElectiveStudent(student.Id);
        if (electiveStudent == null)
            throw IsuExtraException.NoSuchStudent();

        if (electiveStudent.ElectiveSchedulesOverlap(newExtraGroup.Schedule))
            throw IsuExtraException.ElectiveSchedulesOverlap();
    }
}