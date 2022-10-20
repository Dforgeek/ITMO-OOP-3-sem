using System.Security.Cryptography.X509Certificates;
using Isu.Extra.Entities;

namespace Isu.Extra.Exceptions;

public class ElectiveStudentException : Exception
{
    private ElectiveStudentException(string message)
        : base(message) { }

    public static ElectiveStudentException LimitOfElectiveGroupsExceeded()
    {
        return new ElectiveStudentException("Limit Of Elective Groups Exceeded");
    }

    public static ElectiveStudentException StudentAlreadyHasThisElective()
    {
        return new ElectiveStudentException("Student Already Has This Elective");
    }

    public static ElectiveStudentException NoSuchElective()
    {
        return new ElectiveStudentException("Nop such elective");
    }
}