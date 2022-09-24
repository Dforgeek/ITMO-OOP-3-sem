namespace Isu.Models;

public class NumberFactory
{
    private int _studentsCnt = 1;

    public int GetNewNumber()
    {
        return _studentsCnt++;
    }
}