using System.Diagnostics.CodeAnalysis;
using Isu.Extra.Entities;

namespace Isu.Extra.Exceptions;

public class IsuExtraException : Exception
{
    private IsuExtraException(string message)
        : base(message)
    {
    }

    public static IsuExtraException ElectiveModuleAlreadyExists()
    {
        return new IsuExtraException("Elective module already in IsuExtra service");
    }

    public static IsuExtraException NoSuchElectiveModule()
    {
        return new IsuExtraException("No such elective module in service");
    }

    public static IsuExtraException NoSuchExtraGroup()
    {
        return new IsuExtraException("No such extra group in service");
    }

    public static IsuExtraException NoSuchStudent()
    {
        return new IsuExtraException("No such student in service");
    }

    public static IsuExtraException MainScheduleAndElectiveScheduleOverlap()
    {
        return new IsuExtraException("Main schedule is overlapping with elective schedule of student");
    }

    public static IsuExtraException ElectiveSchedulesOverlap()
    {
        return new IsuExtraException("Elective schedule is overlapping with another elective schedule of student");
    }

    public static IsuExtraException NoSuchElectiveGroup()
    {
        return new IsuExtraException("No such elective group in service");
    }
}