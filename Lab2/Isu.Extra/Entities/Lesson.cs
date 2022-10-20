using System.Text.RegularExpressions;
using System.Xml;
using Isu.Extra.Exceptions;
using Isu.Models;

namespace Isu.Extra.Entities;

public record Lesson
{
    private const int MinDayOfLesson = 1;
    private const int MaxDayOfLesson = 7;
    private readonly TimeOnly _minTime = new (5, 0, 0);
    private readonly TimeOnly _maxTime = new (23, 0, 0);
    private readonly TimeOnly _classicLessonTimeSpan = new (1, 30);

    public Lesson(TimeOnly startTime, int dayOfLesson, int classroomNumber, Teacher teacher)
    {
        if (!ValidateStartTimeAndDayOfLesson(startTime, dayOfLesson))
            throw LessonException.InvalidDayOrStartTime();
        StartingTimeOfLesson = startTime;
        DayOfLesson = dayOfLesson;
        ClassroomNumber = classroomNumber;
        Teacher = teacher;
    }

    public TimeOnly StartingTimeOfLesson { get; }
    public int DayOfLesson { get; }
    public int ClassroomNumber { get; }
    public Teacher Teacher { get; }

    public bool LessonOverlap(Lesson otherLesson)
    {
        return DayOfLesson != otherLesson.DayOfLesson ||
               (Math.Abs(StartingTimeOfLesson.Hour - otherLesson.StartingTimeOfLesson.Hour) >=
                _classicLessonTimeSpan.Hour &&
                Math.Abs(StartingTimeOfLesson.Minute - StartingTimeOfLesson.Minute) >=
                _classicLessonTimeSpan.Minute);
    }

    private bool ValidateStartTimeAndDayOfLesson(TimeOnly startTime, int dayOfLesson)
    {
        return startTime <= _maxTime && startTime >= _minTime &&
               dayOfLesson is <= MaxDayOfLesson and >= MinDayOfLesson;
    }
}