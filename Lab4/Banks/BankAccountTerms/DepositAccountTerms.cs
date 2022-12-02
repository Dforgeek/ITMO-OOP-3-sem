using Banks.ValueObjects;

namespace Banks.BankAccountTerms;

public record DepositAccountTerms : IBankAccountTerms
{
    private readonly List<DepositChangeRate> _depositChangeRates;

    private DepositAccountTerms(PosOnlyMoney unreliableClientLimit, List<DepositChangeRate> depositChangeRates)
    {
        UnreliableClientLimit = unreliableClientLimit;
        _depositChangeRates = depositChangeRates;
    }

    public PosOnlyMoney UnreliableClientLimit { get; }

    public IReadOnlyCollection<DepositChangeRate> DepositAccountTermsList => _depositChangeRates.AsReadOnly();

    public Percent GetPercentForConcreteBalance(PosOnlyMoney balance)
    {
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

        public DepositAccountTermsBuilder()
        {
            _changeRates = new List<DepositChangeRate>();
            _unreliableClientLimit = null;
        }

        public void SetLimit(PosOnlyMoney limit)
        {
            _unreliableClientLimit = limit;
        }

        public void AddDepositChangeRate(DepositChangeRate depositChangeRate)
        {
            _changeRates.Add(depositChangeRate);
        }

        public void Reset()
        {
            _changeRates = new List<DepositChangeRate>();
            _unreliableClientLimit = null;
        }

        public DepositAccountTerms Build()
        {
            if (_unreliableClientLimit == null)
                throw new Exception();
            var newDepositAccountTerms = new DepositAccountTerms(_unreliableClientLimit, _changeRates);
            Reset();
            return newDepositAccountTerms;
        }
    }
}