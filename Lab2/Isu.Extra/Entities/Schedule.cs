using System.Reflection.Metadata.Ecma335;
using Isu;

namespace Isu.Extra.Entities;

public class Schedule
{
    private readonly TimeOnly _classicLessonTimeSpan = new (1, 30);
    private readonly List<Lesson> _lessons;

    private Schedule(List<Lesson> lessons)
    {
        _lessons = lessons;
    }

    public static ScheduleBuilder Builder => new ScheduleBuilder();

    public IReadOnlyList<Lesson> Lessons => _lessons.AsReadOnly();

    public bool ScheduleOverlap(Schedule schedule)
    {
        return _lessons
            .Any(firstLesson => !schedule._lessons
                .All(secondLesson => firstLesson.DayOfLesson != secondLesson.DayOfLesson ||
                (Math.Abs(firstLesson.StartingTimeOfLesson.Hour - secondLesson.StartingTimeOfLesson.Hour) >= _classicLessonTimeSpan.Hour &&
                Math.Abs(firstLesson.StartingTimeOfLesson.Minute - secondLesson.StartingTimeOfLesson.Minute) >= _classicLessonTimeSpan.Minute)));
    }

    public class ScheduleBuilder
    {
        private readonly List<Lesson> _lessons;

        public ScheduleBuilder()
        {
            _lessons = new List<Lesson>();
        }

        public void AddLesson(Lesson lesson)
        {
            if (!ValidateLesson(lesson))
                throw new Exception();
            _lessons.Add(lesson);
        }

        public Schedule Build()
        {
            return new Schedule(_lessons);
        }

        public bool ValidateLesson(Lesson newLesson)
        {
            return _lessons.All(lesson =>
                lesson.DayOfLesson != newLesson.DayOfLesson ||
                lesson.StartingTimeOfLesson != newLesson.StartingTimeOfLesson);
        }
    }
}