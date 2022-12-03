using Banks.BankAccountTerms;
using Banks.BankServices;
using Banks.ValueObjects;

namespace Banks.Console.Tools.BankHandler;

public class CreateBankHandler : IChainOfResponsibility
{
    public CreateBankHandler(CentralBank centralBank)
    {
        CentralBank = centralBank;
    }

    public CentralBank CentralBank { get; }
    public IChainOfResponsibility? NextChainElement { get; set; }

    public void Handle()
    {
        System.Console.WriteLine(
            "Commands: presetBank1, presetBank2, exit");
        string? command = System.Console.ReadLine();
        switch (command)
        {
            case "presetBank1":
            {
                var limit = new PosOnlyMoney(100);
                var creditTerms = new CreditAccountTerms(limit, new PosNegMoney(-1000), new PosOnlyMoney(20));
                var debitTerms = new DebitAccountTerms(limit, new Percent(365));
                DepositAccountTerms.DepositAccountTermsBuilder deposBuilder = DepositAccountTerms.Builder();
                deposBuilder.SetLimit(limit);
                deposBuilder.AddWithdrawUnavailableTimeSpan(new TimeSpan(30, 0, 0, 0));
                deposBuilder.AddDepositChangeRate(new PosOnlyMoney(300), new Percent(500));
                DepositAccountTerms deposTerms = deposBuilder.Build();
                CentralBank.CreateBank("OOPBank", creditTerms, debitTerms, deposTerms);
                break;
            }

            case "presetBank2":
            {
                var limit = new PosOnlyMoney(200);
                var creditTerms = new CreditAccountTerms(limit, new PosNegMoney(-2000), new PosOnlyMoney(20));
                var debitTerms = new DebitAccountTerms(limit, new Percent(100));
                DepositAccountTerms.DepositAccountTermsBuilder deposBuilder = DepositAccountTerms.Builder();
                deposBuilder.SetLimit(limit);
                deposBuilder.AddWithdrawUnavailableTimeSpan(new TimeSpan(30, 0, 0, 0));
                deposBuilder.AddDepositChangeRate(new PosOnlyMoney(100), new Percent(35));
                deposBuilder.AddDepositChangeRate(new PosOnlyMoney(300), new Percent(50));
                DepositAccountTerms deposTerms = deposBuilder.Build();
                CentralBank.CreateBank("Bedniy Bank", creditTerms, debitTerms, deposTerms);
                break;
            }

            case "exit":
                return;
        }

        if (NextChainElement != null)
            NextChainElement.Handle();
        else
            Handle();
    }
}