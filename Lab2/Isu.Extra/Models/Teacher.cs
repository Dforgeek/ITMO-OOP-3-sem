using Isu.Extra.Exceptions;

namespace Isu.Extra.Entities;

public record Teacher
{
    public Teacher(Guid id, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw TeacherException.InvalidTeacherName(name);
        Name = name;
        Id = id;
    }

    public string Name { get; }
    public Guid Id { get; }
}
