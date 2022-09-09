using System.Security.Cryptography;
using Isu.Models;

namespace Isu.Entities;

public class Student
{
    public Student(string name)
    {
        Name = name;
        Id = NumberFactory.GetNewNumber(); // group
    }

    public string Name { get; }

    public int Id { get; }
}