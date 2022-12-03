using Banks.ValueObjects;

namespace Banks.BankAccountTerms;

public record DepositAccountTerms : IBankAccountTerms
{
    private readonly List<DepositChangeRate> _depositChangeRates;

    private DepositAccountTerms(PosOnlyMoney unreliableClientLimit, TimeSpan timeSpan, List<DepositChangeRate> depositChangeRates)
    {
        UnreliableClientLimit = unreliableClientLimit;
        _depositChangeRates = depositChangeRates;
        WithdrawUnavailableTimeSpan = timeSpan;
    }

    public PosOnlyMoney UnreliableClientLimit { get; }

    public IReadOnlyCollection<DepositChangeRate> DepositAccountTermsList => _depositChangeRates.AsReadOnly();

    public TimeSpan WithdrawUnavailableTimeSpan { get; }

    public Percent GetPercentForConcreteBalance(PosOnlyMoney balance)
    {
        if (_depositChangeRates[0].Threshold.Value > balance.Value)
            return new Percent(0);
        for (int i = 0; i < _depositChangeRates.Count - 1; i++)
        {
            if (_depositChangeRates[i].Threshold.Value < balance.Value
                && balance.Value < _depositChangeRates[i + 1].Threshold.Value)
                return _depositChangeRates[i].Percent;
        }

        return _depositChangeRates.Last().Percent;
    }

    public static DepositAccountTermsBuilder Builder() => new DepositAccountTermsBuilder();

    public class DepositAccountTermsBuilder
    {
        private PosOnlyMoney? _unreliableClientLimit;
        private List<DepositChangeRate> _changeRates;
        private TimeSpan _timeSpan;

        public DepositAccountTermsBuilder()
        {
            _changeRates = new List<DepositChangeRate>();
            _unreliableClientLimit = null;
            _timeSpan = TimeSpan.Zero;
        }

        public void SetLimit(PosOnlyMoney limit)
        {
            _unreliableClientLimit = limit;
        }

        public void AddDepositChangeRate(PosOnlyMoney threshold, Percent percent)
        {
            _changeRates.Add(new DepositChangeRate(threshold, percent));
        }

        public void AddWithdrawUnavailableTimeSpan(TimeSpan timeSpan)
        {
            _timeSpan = timeSpan;
        }

        public void Reset()
        {
            _changeRates = new List<DepositChangeRate>();
            _unreliableClientLimit = null;
            _timeSpan = TimeSpan.Zero;
        }

        public DepositAccountTerms Build()
        {
            if (_unreliableClientLimit == null || _timeSpan == TimeSpan.Zero)
                throw new Exception();
            var newDepositAccountTerms = new DepositAccountTerms(_unreliableClientLimit, _timeSpan, _changeRates);
            Reset();
            return newDepositAccountTerms;
        }
    }
}