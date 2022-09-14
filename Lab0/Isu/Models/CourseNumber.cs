using Isu.Exceptions;

namespace Isu.Models;

public enum Degree
{
    Bachelor,
    Postgraduate,
    Magistracy,
}

public class CourseNumber
{
    private const int MinCourse = 1;

    private static Dictionary<Degree, int> _degreeMaxCourse = new Dictionary<Degree, int>()
    {
        { Degree.Bachelor, 4 },
        { Degree.Magistracy, 2 },
        { Degree.Postgraduate, 3 },
    };

    private int _courseNum;
    private Degree _degree;

    public CourseNumber(Degree degree, int num)
    {
        _degree = degree;
        CourseNum = num;
    }

    public CourseNumber(GroupName groupName)
    {
        _degree = groupName.GetDegree();
        CourseNum = groupName.GetCourseNum();
    }

    public int CourseNum
    {
        get => _courseNum;

        private set
        {
            if (value < MinCourse || value > _degreeMaxCourse[_degree])
                throw new IsuException("Invalid course number");
            _courseNum = value;
        }
    }

    public Degree GetDegree()
    {
        return _degree;
    }
}