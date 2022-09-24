using System.Security.Cryptography;
using Isu.Exceptions;
using Isu.Models;

namespace Isu.Entities;

public class Student
{
    public Student(string name, int id, Group group)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw StudentException.EmptyName();
        Name = name;
        Id = id;
        Group = group;
    }

    public Group Group { get; private set; }
    public string Name { get; }

    public int Id { get; }

    public void ChangeGroup(Group group)
    {
        group.AddStudent(this);
        Group.DeleteStudent(this);
        Group = group;
    }
}