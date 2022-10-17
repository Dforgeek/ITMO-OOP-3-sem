using System.Text.RegularExpressions;
using Isu.Models;

namespace Isu.Extra.Entities;

public class Lesson
{
    private const int MinNumOfLesson = 1;
    private const int MaxNumOfLesson = 7;
    private const int MinDayOfLesson = 1;
    private const int MaxDayOfLesson = 7;

    public Lesson(int numOfLesson, int dayOfLesson, Teacher teacher, Group group)
    {
        if (!ValidateNumOfLessonAndDayOfLesson(numOfLesson, dayOfLesson))
            throw new Exception();
        NumOfLesson = numOfLesson;
        DayOfLesson = dayOfLesson;
        Teacher = teacher;
        Group = group;
    }

    public int NumOfLesson { get; }
    public int DayOfLesson { get; }
    public Teacher Teacher { get; }
    public Group Group { get; }
    private bool ValidateNumOfLessonAndDayOfLesson(int numOfLesson, int dayOfLesson)
    {
        return numOfLesson is <= MaxNumOfLesson and >= MinNumOfLesson &&
               dayOfLesson is <= MaxDayOfLesson and >= MinDayOfLesson;
    }
}