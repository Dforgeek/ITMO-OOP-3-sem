using System.Reflection.Metadata.Ecma335;
using Isu;

namespace Isu.Extra.Entities;

public class Shedule
{
    private readonly List<Lesson> _lessons;

    private Shedule(List<Lesson> lessons)
    {
        _lessons = lessons;
    }

    public static SheduleBuilder Builder => new SheduleBuilder();

    public IReadOnlyList<Lesson> Lessons => _lessons.AsReadOnly();

    public class SheduleBuilder
    {
        private readonly List<Lesson> _lessons;

        public SheduleBuilder()
        {
            _lessons = new List<Lesson>();
        }

        public void AddLesson(Lesson lesson)
        {
            if (!ValidateLesson(lesson))
                throw new Exception();
            _lessons.Add(lesson);
        }

        public Shedule Build()
        {
            return new Shedule(_lessons);
        }

        public bool ValidateLesson(Lesson newLesson)
        {
            return _lessons.All(lesson =>
                lesson.DayOfLesson != newLesson.DayOfLesson ||
                lesson.NumOfLesson != newLesson.NumOfLesson);
        }
    }
}