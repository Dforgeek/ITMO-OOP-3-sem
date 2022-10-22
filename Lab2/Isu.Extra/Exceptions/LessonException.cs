namespace Isu.Extra.Exceptions;

public class LessonException : Exception
{
    private LessonException(string message)
        : base(message) { }

    public static LessonException InvalidDayOrStartTime()
    {
        return new LessonException("Invalid day or start time of lesson");
    }
}