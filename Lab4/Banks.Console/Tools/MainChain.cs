using Banks.BankAccounts;
using Banks.BankServices;
using Banks.Console.Tools.BankHandler;
using Banks.ValueObjects;

namespace Banks.Console.Tools;

public class MainChain : IChainOfResponsibility
{
    public MainChain(CentralBank centralBank)
    {
        CentralBank = centralBank;
    }

    public CentralBank CentralBank { get; }
    public IChainOfResponsibility? NextChainElement { get; set; }

    public void Handle()
    {
        System.Console.WriteLine(
            "Commands: addBank, clients, addBankAccount, createTransaction, spendTime");
        string? command = System.Console.ReadLine();
        switch (command)
        {
            case "clients":
            {
                var clientHandler = new ClientHandler(CentralBank);
                clientHandler.Handle();
                break;
            }

            case "addBank":
            {
                var bankHandler = new BankHandler.CreateBankHandler(CentralBank);
                bankHandler.Handle();
                break;
            }

            case "addBankAccount":
            {
                var accountHandler = new BankAccountHandler(centralBank: CentralBank);
                accountHandler.Handle();
                break;
            }

            case "createTransaction":
            {
                System.Console.WriteLine(
                    "Commands: transfer, withdraw, replenish");
                string? cmd = System.Console.ReadLine();
                switch (cmd)
                {
                    case "transfer":
                    {
                        var chooseAccount1 = new ChooseAccountHandler(CentralBank);
                        chooseAccount1.Handle();
                        var chooseAccount2 = new ChooseAccountHandler(CentralBank);
                        chooseAccount2.Handle();
                        IBankAccount from = chooseAccount1.BankAccount ?? throw new Exception();
                        IBankAccount to = chooseAccount2.BankAccount ?? throw new Exception();
                        var moneyHandler = new BalanceHandler();
                        moneyHandler.Handle();
                        PosOnlyMoney money = moneyHandler.Balance ?? throw new Exception();
                        CentralBank.Transfer(from.Bank.Id, from.Id, to.Id, money);
                        break;
                    }

                    case "withdraw":
                    {
                        var chooseAccount1 = new ChooseAccountHandler(CentralBank);
                        chooseAccount1.Handle();
                        IBankAccount from = chooseAccount1.BankAccount ?? throw new Exception();
                        var moneyHandler = new BalanceHandler();
                        moneyHandler.Handle();
                        PosOnlyMoney money = moneyHandler.Balance ?? throw new Exception();
                        CentralBank.Withdraw(from.Bank.Id, from.Id, money);
                        break;
                    }

                    case "replenish":
                    {
                        var chooseAccount1 = new ChooseAccountHandler(CentralBank);
                        chooseAccount1.Handle();
                        IBankAccount from = chooseAccount1.BankAccount ?? throw new Exception();
                        var moneyHandler = new BalanceHandler();
                        moneyHandler.Handle();
                        PosOnlyMoney money = moneyHandler.Balance ?? throw new Exception();
                        CentralBank.Replenish(from.Bank.Id, from.Id, money);
                        break;
                    }
                }

                break;
            }

            case "spendTime":
            {
                var timeHandler = new TimeHandler(CentralBank);
                timeHandler.Handle();
                break;
            }
        }

        if (NextChainElement != null)
            NextChainElement.Handle();
        else
            Handle();
    }
}