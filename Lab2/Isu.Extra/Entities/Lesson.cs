using System.Text.RegularExpressions;
using System.Xml;
using Isu.Models;

namespace Isu.Extra.Entities;

public class Lesson
{
    private const int MinDayOfLesson = 1;
    private const int MaxDayOfLesson = 7;
    private readonly TimeOnly _minTime = new (5, 0, 0);
    private readonly TimeOnly _maxTime = new (23, 0, 0);

    public Lesson(TimeOnly startTime, int dayOfLesson, Teacher teacher, ExtraGroup group)
    {
        if (!ValidateNumOfLessonAndDayOfLesson(startTime, dayOfLesson))
            throw new Exception();
        StartingTimeOfLesson = startTime;
        DayOfLesson = dayOfLesson;
        Teacher = teacher;
        Group = group;
    }

    public TimeOnly StartingTimeOfLesson { get; }
    public int DayOfLesson { get; }
    public Teacher Teacher { get; }
    public ExtraGroup Group { get; }

    private bool ValidateNumOfLessonAndDayOfLesson(TimeOnly startTime, int dayOfLesson)
    {
        return startTime <= _maxTime && startTime >= _minTime &&
               dayOfLesson is <= MaxDayOfLesson and >= MinDayOfLesson;
    }
}