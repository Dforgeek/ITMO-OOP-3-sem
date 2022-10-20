namespace Isu.Extra.Exceptions;

public class ElectiveGroupException : Exception
{
    private ElectiveGroupException(string message)
        : base(message) { }

    public static ElectiveGroupException LimitOfElectiveStudentsExceeded()
    {
        return new ElectiveGroupException("Limit of Elective Students exceeded");
    }

    public static ElectiveGroupException MegaFacultyIsTheSameForStudentAndElective()
    {
        return new ElectiveGroupException("MegaFaculty Is The Same For Student And Elective");
    }

    public static ElectiveGroupException ElectiveGroupAlreadyHasThisStudent()
    {
        return new ElectiveGroupException("Elective Group Already Has This Student");
    }

    public static ElectiveGroupException NoSuchStudent()
    {
        return new ElectiveGroupException("No such student in elective group");
    }
}