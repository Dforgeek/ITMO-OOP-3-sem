namespace Isu.Models;

public class CourseNumber
{
    private const int MaxCourse = 4;
    private const int MinCourse = 1;

    private int _courseNum;

    public CourseNumber(int num)
    {
        CourseNum = num;
    }

    public int CourseNum
    {
        get => _courseNum;

        set
        {
            if (value < MinCourse || value > MaxCourse)
                throw new Exception("Incorrect course number");
            _courseNum = value;
        }
    }
}