namespace Isu.Exceptions;

public class CourseNumberException : Exception
{
    private CourseNumberException() { }

    private CourseNumberException(string message)
        : base(message)
    {
    }

    public static CourseNumberException InvalidCourseNumber()
    {
        return new CourseNumberException("Invalid number of course.");
    }
}