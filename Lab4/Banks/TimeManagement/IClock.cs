namespace Banks.TimeManagement;

public interface IClock
{
    DateTime Now { get; }
}