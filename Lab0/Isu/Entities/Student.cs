using System.Security.Cryptography;
using Isu.Models;

namespace Isu.Entities;

public class Student
{
    private Group _group;
    public Student(string name, Group group)
    {
        Name = name;
        Id = NumberFactory.GetNewNumber(); // group
        _group = group;
    }

    public string Name { get; }

    public int Id { get; }
}