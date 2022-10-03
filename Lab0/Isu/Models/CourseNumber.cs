using Isu.Exceptions;

namespace Isu.Models;

public class CourseNumber
{
    private const int MinCourse = 1;
    private const int MaxCourse = 4;

    public CourseNumber(int num)
    {
        if (num is > MaxCourse or < MinCourse)
            throw CourseNumberException.InvalidCourseNumber();
        CourseNum = num;
    }

    public int CourseNum { get; }

    public override bool Equals(object? obj)
    {
        if (obj is not CourseNumber courseNumber)
        {
            return false;
        }

        return CourseNum == courseNumber.CourseNum;
    }

    public override int GetHashCode()
    {
        return CourseNum.GetHashCode();
    }
}