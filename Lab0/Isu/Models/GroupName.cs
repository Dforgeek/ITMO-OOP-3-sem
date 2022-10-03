using System.Runtime.CompilerServices;
using Isu.Exceptions;

namespace Isu.Models;

public record class GroupName
{
    private const int LengthOfGroupName = 5;
    private const int IndexOfFaculty = 2;
    private const char MinFacultyLetter = 'A';
    private const char MaxFacultyLetter = 'Z';
    private const char BachelorDigit = '3';

    public GroupName(string name)
    {
        if (!ValidateGroupName(name))
        {
            throw GroupNameException.InvalidName();
        }

        Name = name;
        CourseNumber = new CourseNumber(name[IndexOfFaculty] - '0');
    }

    public string Name { get; }
    public CourseNumber CourseNumber { get; }

    private bool ValidateGroupName(string name)
    {
        return name.Length == LengthOfGroupName && char.IsDigit(name[IndexOfFaculty]) &&
               name[0] is > MinFacultyLetter and < MaxFacultyLetter && name[1] == BachelorDigit;
    }
}