using Isu.Exceptions;

namespace Isu.Models;

public class GroupName
{
    private const int MaxCourse = 4;
    private const int MinCourse = 1;
    private string _name = string.Empty;

    public GroupName(string name)
    {
        Name = name;
    }

    public GroupName(GroupName groupName)
    {
        Name = groupName.Name;
    }

    public string Name
    {
        get => _name;
        private set
        {
            if (value.Length != 5 || value[2] > MaxCourse + '0' ||
                value[2] < MinCourse + '0' || value[0] < 'A' || value[0] > 'Z' ||
                value[1] < '3' || value[1] > '5')
            {
                throw new IsuException("Invalid group name");
            }

            _name = value;
        }
    }

    public Degree GetDegree()
    {
        if (_name[1] == 3)
        {
            return Degree.Bachelor;
        }
        else if (_name[1] == 4)
        {
            return Degree.Magistracy;
        }
        else
        {
            return Degree.Postgraduate;
        }
    }

    public int GetCourseNum()
    {
        return _name[2] - '0';
    }

    public override bool Equals(object? obj)
    {
        if (obj is not GroupName groupName)
            return false;
        return groupName._name == _name;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}