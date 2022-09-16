namespace Isu.Models;

public class NumberFactory
{
    private static int _studentsCnt = 1;

    public static int GetNewNumber()
    {
        return _studentsCnt++;
    }
}