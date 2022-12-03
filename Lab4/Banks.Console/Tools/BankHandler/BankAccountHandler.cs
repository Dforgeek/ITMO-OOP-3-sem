using Banks.BankServices;
using Banks.Entities;
using Banks.ValueObjects;

namespace Banks.Console.Tools.BankHandler;

public class BankAccountHandler : IChainOfResponsibility
{
    public BankAccountHandler(CentralBank centralBank)
    {
        CentralBank = centralBank;
        NextChainElement = null;
    }

    public CentralBank CentralBank { get; }

    public IChainOfResponsibility? NextChainElement { get; set; }

    public void Handle()
    {
        var chooseClientHandler = new ChooseClientHandler(CentralBank);
        chooseClientHandler.Handle();
        var chooseBankHandler = new ChooseBankHandler(CentralBank);
        chooseBankHandler.Handle();
        Bank bank = chooseBankHandler.DesiredBank ?? throw new Exception();
        Client client = chooseClientHandler.DesiredClient ?? throw new Exception();
        System.Console.WriteLine("Commands: depositAccount, debitAccount, creditAccount");
        string? command = System.Console.ReadLine();
        switch (command)
        {
            case "depositAccount":
            {
                var balanceHandler = new BalanceHandler();
                balanceHandler.Handle();
                PosOnlyMoney balance = balanceHandler.Balance ?? throw new Exception();
                CentralBank.AddDepositAccount(bank.Id, client.Id, balance);
                break;
            }

            case "debitAccount":
            {
                var balanceHandler = new BalanceHandler();
                balanceHandler.Handle();
                PosOnlyMoney balance = balanceHandler.Balance ?? throw new Exception();
                CentralBank.AddDebitAccount(bank.Id, client.Id, balance);
                break;
            }

            case "creditAccount":
            {
                var balanceHandler = new BalanceHandler();
                balanceHandler.Handle();
                PosOnlyMoney balance = balanceHandler.Balance ?? throw new Exception();
                CentralBank.AddCreditAccount(bank.Id, client.Id, balance);
                break;
            }
        }
    }
}