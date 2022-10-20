using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Exceptions;
using Isu.Extra.Services;
using Isu.Extra.Tools;
using Isu.Models;
using Xunit;

namespace Isu.Extra.Test;

public class IsuExtraTest
{
    private IsuExtraService _isuExtraService = new IsuExtraService();

    [Fact]
    public void CheckStudentsWithoutElectives_ReachMaxStudentPerElectiveGroup_ThrowException()
    {
        Schedule.ScheduleBuilder scheduleBuilder = Schedule.Builder;
        for (int i = 1; i < 7; i++)
        {
            var lesson = new Lesson(new TimeOnly(10, 0, 0), i, 100, new Teacher(Guid.NewGuid(), $"Trifanov-{i}"));
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

        for (int i = 1; i < 7; i++)
        {
            var lesson = new Lesson(new TimeOnly(15, 0, 0), i, 100, new Teacher(Guid.NewGuid(), $"Maytin-{i}"));
            scheduleBuilder.AddLesson(lesson);
        }

        Schedule electiveSchedule = scheduleBuilder.Build();

        ElectiveModule clowns = _isuExtraService.AddElectiveModule("Infobez", new MegaFacultyPrefix('I'));

        ElectiveGroup electiveGroup = _isuExtraService.AddElectiveGroup(clowns.Id, electiveSchedule);
        Assert.Throws<ElectiveGroupException>(() =>
        {
            foreach (ElectiveStudent electiveStudent in _isuExtraService.GetStudentsWithoutElectives())
            {
                _isuExtraService.AddElectiveStudentToElectiveGroup(electiveStudent, electiveGroup);
            }
        });
    }
}