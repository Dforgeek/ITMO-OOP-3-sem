using Banks.BankAccounts;
using Banks.BankAccountTerms;
using Banks.BankServices;
using Banks.Entities;
using Banks.Notifications;
using Banks.TimeManagement;
using Banks.ValueObjects;
using Xunit;
using Xunit.Abstractions;

namespace Banks.Test;

public class BanksTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public BanksTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void ValidateThatMyBanksWorkCorrectly()
    {
        var cb = new CentralBank(new ClockSimulation(DateTime.MinValue));
        Client client = Client.Builder().SetName("Vasya").SetSurname("Pupkin").SetAddress("Pushkina 10")
            .SetPassport(new Passport(1111, 222222)).SetId(Guid.NewGuid()).Build();
        cb.AddClient(client);

        var limit = new PosOnlyMoney(100);
        var creditTerms = new CreditAccountTerms(limit, new PosNegMoney(-1000), new PosOnlyMoney(20));
        var debitTerms = new DebitAccountTerms(limit, new Percent(365));
        DepositAccountTerms.DepositAccountTermsBuilder deposBuilder = DepositAccountTerms.Builder();
        deposBuilder.SetLimit(limit);
        deposBuilder.AddWithdrawUnavailableTimeSpan(new TimeSpan(30, 0, 0, 0));
        deposBuilder.AddDepositChangeRate(new PosOnlyMoney(300), new Percent(500));
        DepositAccountTerms deposTerms = deposBuilder.Build();

        Bank bank = cb.CreateBank("Bank OLEG", creditTerms, debitTerms, deposTerms);
        DebitAccount debitAccount = cb.AddDebitAccount(bank.Id, client.Id, new PosOnlyMoney(500));
        cb.Subscribe(bank.Id, debitAccount.Id, new ConsoleNotificationStrategy());
        cb.PassOneMonth();

        Assert.Equal(655, debitAccount.Balance.Value);

        var newDebitTerms = new DebitAccountTerms(limit, new Percent(420));
        cb.UpdateAccountTerms(bank.Id, creditTerms, newDebitTerms, deposTerms);
        Assert.Equal(420, debitAccount.DebitAccountTerms?.PercentPerAnnum.Value);
    }
}