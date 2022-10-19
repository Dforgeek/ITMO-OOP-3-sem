using System.Text.RegularExpressions;
using System.Xml;
using Isu.Models;

namespace Isu.Extra.Entities;

public record Lesson
{
    private const int MinDayOfLesson = 1;
    private const int MaxDayOfLesson = 7;
    private readonly TimeOnly _minTime = new (5, 0, 0);
    private readonly TimeOnly _maxTime = new (23, 0, 0);

    public Lesson(TimeOnly startTime, int dayOfLesson, int classroomNumber, Teacher teacher)
    {
        if (!ValidateStartTimeAndDayOfLesson(startTime, dayOfLesson))
            throw new Exception();
        StartingTimeOfLesson = startTime;
        DayOfLesson = dayOfLesson;
        ClassroomNumber = classroomNumber;
        Teacher = teacher;
    }

    public TimeOnly StartingTimeOfLesson { get; }
    public int DayOfLesson { get; }
    public int ClassroomNumber { get; }
    public Teacher Teacher { get; }

    private bool ValidateStartTimeAndDayOfLesson(TimeOnly startTime, int dayOfLesson)
    {
        return startTime <= _maxTime && startTime >= _minTime &&
               dayOfLesson is <= MaxDayOfLesson and >= MinDayOfLesson;
    }
}