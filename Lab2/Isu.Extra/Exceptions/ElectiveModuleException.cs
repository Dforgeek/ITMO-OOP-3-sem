namespace Isu.Extra.Exceptions;

public class ElectiveModuleException : Exception
{
    private ElectiveModuleException(string message)
        : base(message) { }

    public static ElectiveModuleException NameIsNullOrEmpty()
    {
        return new ElectiveModuleException("Name of elective module is empty or null");
    }

    public static ElectiveModuleException ElectiveGroupAlreadyExists()
    {
        return new ElectiveModuleException("Such elective group already exists");
    }
}