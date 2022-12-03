using System.Net.Http.Headers;
using System.Security.Principal;
using Banks.BankAccounts;
using Banks.BankAccountTerms;
using Banks.Entities;
using Banks.Notifications;
using Banks.TimeManagement;
using Banks.Transactions;
using Banks.ValueObjects;

namespace Banks.BankServices;

public class CentralBank
{
    private readonly List<Client> _clients;
    private readonly List<Bank> _banks;
    private IClock _clockSimulation;

    public CentralBank(IClock clock)
    {
        _banks = new List<Bank>();
        _clients = new List<Client>();
        _clockSimulation = clock;
    }

    public IReadOnlyCollection<Bank> Banks => _banks.AsReadOnly();

    public IReadOnlyCollection<Client> Clients => _clients.AsReadOnly();

    public IReadOnlyCollection<ITransaction> Transactions(Guid bankId) => GetBank(bankId).Transactions;

    public Bank CreateBank(string name, CreditAccountTerms credAT, DebitAccountTerms debAT, DepositAccountTerms depAT)
    {
        var visitor = new AccountTermsVisitor(credAT, debAT, depAT);
        var bank = new Bank(name, _clockSimulation, visitor, Guid.NewGuid());
        _banks.Add(bank);
        return bank;
    }

    public void AddClient(Client client)
    {
        if (_clients.Contains(client))
            throw new Exception();
        _clients.Add(client);
    }

    public Client GetClient(Guid clientId)
    {
        return _clients.FirstOrDefault(client => clientId == client.Id) ?? throw new Exception();
    }

    public Bank GetBank(Guid bankId)
    {
        return _banks.FirstOrDefault(bank => bank.Id == bankId) ?? throw new Exception();
    }

    public IBankAccount GetAccount(Guid bankAccountId)
    {
        foreach (IBankAccount? ba in _banks.Select(bank => bank.BankAccounts
                     .FirstOrDefault(ba => ba.Id == bankAccountId)).Where(ba => ba != null))
            return ba ?? throw new Exception();

        throw new Exception();
    }

    public ITransaction GetTransaction(Guid bankId, Guid transactionId)
    {
        return GetBank(bankId).GetTransaction(transactionId);
    }

    public CreditAccount AddCreditAccount(Guid bankId, Guid clientId, IMoney balance)
    {
        var factory = new CreditAccountFactory();
        return (CreditAccount)GetBank(bankId).CreateBankAccount(factory, GetClient(clientId), balance);
    }

    public DebitAccount AddDebitAccount(Guid bankId, Guid clientId, IMoney balance)
    {
        var factory = new DebitAccountFactory();
        return (DebitAccount)GetBank(bankId).CreateBankAccount(factory, GetClient(clientId), balance);
    }

    public DepositAccount AddDepositAccount(Guid bankId, Guid clientId, IMoney balance)
    {
        var factory = new DepositAccountFactory(_clockSimulation);
        return (DepositAccount)GetBank(bankId).CreateBankAccount(factory, GetClient(clientId), balance);
    }

    public WithdrawTransaction Withdraw(Guid bankId, Guid bankAccountId, PosOnlyMoney money)
    {
        return GetBank(bankId).Withdraw(bankAccountId, money);
    }

    public ReplenishTransaction Replenish(Guid bankId, Guid bankAccountId, PosOnlyMoney money)
    {
        return GetBank(bankId).Replenish(bankAccountId, money);
    }

    public TransferTransaction Transfer(Guid bankId, Guid fromAccId, Guid toAccId, PosOnlyMoney money)
    {
        return GetBank(bankId).Transfer(fromAccId, GetAccount(toAccId), money);
    }

    public ITransaction UndoTransfer(Guid bankId, Guid transactionId)
    {
        return GetBank(bankId).UndoInBankTransfer(GetTransaction(bankId, transactionId));
    }

    public void UpdateAccountTerms(Guid bankId, CreditAccountTerms credAT, DebitAccountTerms debAT, DepositAccountTerms depAT)
    {
        var visitor = new AccountTermsVisitor(credAT, debAT, depAT);
        GetBank(bankId).UpdateAccountTerms(visitor);
    }

    public void Subscribe(Guid bankId, Guid accountId, INotificationStrategy notificationStrategy)
    {
        GetBank(bankId).SubscribeToNotifications(accountId, notificationStrategy);
    }

    public void PassOneDay()
    {
        DateTime before = _clockSimulation.Now;

        _clockSimulation.PassOneDay();

        foreach (Bank bank in _banks)
        {
            bank.UpdateAllAccruals();
        }

        if (_clockSimulation.Now.Day != 1) return;
        foreach (Bank bank in _banks)
        {
            bank.WriteOffAllAccruals();
        }
    }

    public void PassOneMonth()
    {
        for (int i = 0; i < 32; i++)
        {
            PassOneDay();
        }
    }
}