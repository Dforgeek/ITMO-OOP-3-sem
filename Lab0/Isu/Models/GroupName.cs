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

    public string Name
    {
        get => _name;
        private set
        {
            if (value.Length != 5 || value[2] > MaxCourse + '0' ||
                value[2] < MinCourse + '0' || value[0] < 'A' || value[0] > 'Z')
            {
                throw new IsuException("Invalid group name");
            }

            _name = value;
        }
    }
}