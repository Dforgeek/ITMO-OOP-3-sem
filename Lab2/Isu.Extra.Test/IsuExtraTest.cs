using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;
using Isu.Extra.Services;
using Isu.Extra.Tools;
using Isu.Models;
using Xunit;
using Xunit.Abstractions;

namespace Isu.Extra.Test;

public class IsuExtraTest
{
    private readonly ITestOutputHelper _testOutputHelper;
    private IsuExtraService _isuExtraService = new IsuExtraService();

    public IsuExtraTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void CheckStudentsWithoutElectives_ReachMaxStudentPerElectiveGroup_ThrowException()
    {
        Schedule.ScheduleBuilder scheduleBuilder = Schedule.Builder;
        for (int i = 1; i < 7; i++)
        {
            var lesson =
                new Lesson(new TimeOnly(10, 0, 0), (DayOfWeek)i, ParityOfWeek.Even, 100, new Teacher(Guid.NewGuid(), $"Trifanov-{i}"));
            scheduleBuilder.AddLesson(lesson);
        }

        Schedule mainSchedule = scheduleBuilder.Build();

        ExtraGroup extraGroup1 = _isuExtraService.AddExtraGroup(new GroupName("M3201"), mainSchedule);
        ExtraGroup extraGroup2 = _isuExtraService.AddExtraGroup(new GroupName("M3202"), mainSchedule);
        for (int i = 0; i < 25; i++)
        {
            _isuExtraService.AddElectiveStudent($"Ikromjon Pukinsky-{i}", extraGroup1.Group);
            _isuExtraService.AddElectiveStudent($"George-The-Major-Mentor-{i}", extraGroup2.Group);
        }

        Assert.Equal(50, _isuExtraService.GetStudentsWithoutElectives().Count);

        for (int i = 0; i < 7; i++)
        {
            var lesson = new Lesson(new TimeOnly(15, 0, 0), (DayOfWeek)i, ParityOfWeek.Even, 100, new Teacher(Guid.NewGuid(), $"Maytin-{i}"));
            scheduleBuilder.AddLesson(lesson);
        }

        Schedule electiveSchedule = scheduleBuilder.Build();

        ElectiveModule clowns = _isuExtraService.AddElectiveModule("Infobez", new MegaFacultyPrefix('I'));

        ElectiveGroup electiveGroup = _isuExtraService.AddElectiveGroup(clowns.Id, electiveSchedule);
        Assert.Throws<IsuExtraException>(() =>
        {
            foreach (ElectiveStudent electiveStudent in _isuExtraService.GetStudentsWithoutElectives())
            {
                _isuExtraService.AddElectiveStudentToElectiveGroup(electiveStudent, electiveGroup);
            }
        });
    }

    [Fact]
    public void AddElectiveStudentsToElective_AssertThatTheyAreThere_DeleteElectiveStudentFromElective()
    {
        Schedule.ScheduleBuilder scheduleBuilder = Schedule.Builder;

        var lesson1 = new Lesson(new TimeOnly(10, 0, 0), DayOfWeek.Monday, ParityOfWeek.Even, 100, new Teacher(Guid.NewGuid(), $"Trifanov"));
        scheduleBuilder.AddLesson(lesson1);

        Schedule mainSchedule = scheduleBuilder.Build();

        ExtraGroup extraGroup = _isuExtraService.AddExtraGroup(new GroupName("M3201"), mainSchedule);

        ElectiveStudent ikromjon = _isuExtraService.AddElectiveStudent($"Ikromjon Pukinsky", extraGroup.Group);

        var lesson2 = new Lesson(new TimeOnly(15, 0, 0), DayOfWeek.Friday, ParityOfWeek.Even, 100, new Teacher(Guid.NewGuid(), $"Maytin"));
        scheduleBuilder.AddLesson(lesson2);

        Schedule electiveSchedule = scheduleBuilder.Build();
        ElectiveModule clowns = _isuExtraService.AddElectiveModule("Infobez", new MegaFacultyPrefix('I'));
        ElectiveGroup electiveGroup = _isuExtraService.AddElectiveGroup(clowns.Id, electiveSchedule);

        foreach (ElectiveStudent electiveStudent in _isuExtraService.GetStudentsWithoutElectives())
        {
            _isuExtraService.AddElectiveStudentToElectiveGroup(electiveStudent, electiveGroup);
        }

        _testOutputHelper.WriteLine(electiveGroup.ElectiveStudents.Last().Name);
        Assert.Contains(ikromjon, electiveGroup.ElectiveStudents);

        _isuExtraService.DeleteElectiveStudentFromElectiveGroup(ikromjon, electiveGroup);
        Assert.Equal(0, ikromjon.Electives.Count);
        Assert.Empty(_isuExtraService.GetElectiveStudents(electiveGroup.Id));
    }

    [Fact]
    public void AddElectiveToStudent_ThrowOverlapException()
    {
        Schedule.ScheduleBuilder scheduleBuilder = Schedule.Builder;
        for (int i = 1; i < 7; i++)
        {
            var lesson = new Lesson(new TimeOnly(10, 0, 0), (DayOfWeek)i, ParityOfWeek.Even, 100, new Teacher(Guid.NewGuid(), $"Trifanov"));
            scheduleBuilder.AddLesson(lesson);
        }

        Schedule schedule = scheduleBuilder.Build();
        ExtraGroup extraGroup = _isuExtraService.AddExtraGroup(new GroupName("M3201"), schedule);
        ElectiveStudent ikromjon = _isuExtraService.AddElectiveStudent($"Ikromjon Pukinsky", extraGroup.Group);

        ElectiveModule clowns = _isuExtraService.AddElectiveModule("Infobez", new MegaFacultyPrefix('I'));
        ElectiveGroup electiveGroup = _isuExtraService.AddElectiveGroup(clowns.Id, schedule);
        Assert.Throws<IsuExtraException>(() =>
        {
            _isuExtraService.AddElectiveStudentToElectiveGroup(ikromjon, electiveGroup);
        });
    }
}