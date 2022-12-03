namespace Banks.Console.Tools;

public interface IChainOfResponsibility
{
    IChainOfResponsibility? NextChainElement { get; set; }

    void Handle();
}