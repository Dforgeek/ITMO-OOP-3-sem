namespace Isu.Models;

public class NumberFactory
{
    private static int _studentsCnt = 0;

    public static int GetNewNumber()
    {
        return _studentsCnt++;
    }
}