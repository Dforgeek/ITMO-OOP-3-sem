using System.Security.Cryptography.X509Certificates;

namespace Isu.Extra.Exceptions;

public class ScheduleBuilderException : Exception
{
    private ScheduleBuilderException(string message)
        : base(message) { }

    public static ScheduleBuilderException LessonOverlap()
    {
        return new ScheduleBuilderException("Lesson is overlapping other lessons in schedule");
    }
}