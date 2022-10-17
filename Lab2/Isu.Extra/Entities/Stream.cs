namespace Isu.Extra.Entities;

public class Stream
{
    public Stream(Shedule shedule)
    {
        Shedule = shedule;
    }

    public Shedule Shedule { get; }
}