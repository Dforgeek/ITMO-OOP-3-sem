using System.Text.RegularExpressions;
using System.Xml;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;
using Isu.Models;

namespace Isu.Extra.Entities;

public record Lesson
{
    private readonly TimeOnly _classicLessonTime = new (1, 30);

    public Lesson(TimeOnly startTime, DayOfWeek dayOfLesson, ParityOfWeek parityOfWeek, int classroomNumber, Teacher teacher)
    {
        StartingTimeOfLesson = startTime;
        DayOfLesson = dayOfLesson;
        ClassroomNumber = classroomNumber;
        Teacher = teacher;
        ParityOfWeek = parityOfWeek;
    }

    public TimeOnly StartingTimeOfLesson { get; }
    public DayOfWeek DayOfLesson { get; }
    public ParityOfWeek ParityOfWeek { get; }
    public int ClassroomNumber { get; }
    public Teacher Teacher { get; }

    public bool LessonOverlap(Lesson otherLesson)
    {
        return DayOfLesson == otherLesson.DayOfLesson ||
               (Math.Abs(StartingTimeOfLesson.Hour - otherLesson.StartingTimeOfLesson.Hour) >=
                _classicLessonTime.Hour &&
                Math.Abs(StartingTimeOfLesson.Minute - StartingTimeOfLesson.Minute) >=
                _classicLessonTime.Minute);
    }
}