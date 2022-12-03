namespace Banks.TimeManagement;

public interface IClock
{
    DateTime Now { get; }

    void PassOneDay();

    void PassOneMonth();
}