namespace Banks.Console.Tools.TermsHandler;

public class TermsHandler : IChainOfResponsibility
{
    public IChainOfResponsibility? NextChainElement { get; set; }
    public void Handle()
    {
    }
}