using Banks.Interfaces;

namespace Banks.Models;

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