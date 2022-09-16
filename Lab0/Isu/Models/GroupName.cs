using System.Runtime.CompilerServices;
using Isu.Exceptions;

namespace Isu.Models;

public class GroupName
{
    private const int LengthOfGroupName = 5;
    private const char MinFacultyLetter = 'A';
    private const char MaxFacultyLetter = 'Z';
    private const char BachelorDigit = '3';
    public GroupName(string name)
    {
        if (!ValidateGroupName(name))
        {
            throw new IsuException($"Invalid group name: {name}");
        }

        Name = name;
        CourseNumber = new CourseNumber(name[2] - '0');
    }

    public string Name { get; }
    public CourseNumber CourseNumber { get; }

    public override bool Equals(object? obj)
    {
        if (obj is not GroupName groupName)
            return false;
        return groupName.Name == Name;
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }

    private bool ValidateGroupName(string name)
    {
        return name.Length == LengthOfGroupName && char.IsDigit(name[2]) &&
               name[0] is > MinFacultyLetter and < MaxFacultyLetter && name[1] == BachelorDigit;
    }
}