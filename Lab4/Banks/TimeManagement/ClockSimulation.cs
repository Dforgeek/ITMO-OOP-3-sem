namespace Banks.TimeManagement;

public class ClockSimulation : IClock
{
    public ClockSimulation()
    {
        Now = DateTime.Now;
    }

    public DateTime Now { get; private set; }

    public void PassOneDay()
    {
        Now = Now.AddDays(1);
    }

    public void PassOneMonth()
    {
        Now = Now.AddMonths(1);
    }
}