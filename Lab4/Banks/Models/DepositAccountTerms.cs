using Banks.Interfaces;

namespace Banks.Models;

public record DepositAccountTerms
{
    private List<DepositChangeRate> _changeRates;

    private DepositAccountTerms(PosOnlyMoney limit, List<DepositChangeRate> changeRates)
    {
        Limit = limit;
        _changeRates = changeRates;
    }

    public PosOnlyMoney Limit { get; }

    public static DepositAccountTermsBuilder Builder() => new DepositAccountTermsBuilder();

    public class DepositAccountTermsBuilder
    {
        private PosOnlyMoney? _limit;
        private List<DepositChangeRate> _changeRates;

        public DepositAccountTermsBuilder()
        {
            _changeRates = new List<DepositChangeRate>();
            _limit = null;
        }

        public void SetLimit(PosOnlyMoney limit)
        {
            _limit = limit;
        }

        public void AddDepositChangeRate(DepositChangeRate depositChangeRate)
        {
            _changeRates.Add(depositChangeRate);
        }

        public void Reset()
        {
            _changeRates = new List<DepositChangeRate>();
            _limit = null;
        }

        public DepositAccountTerms Build()
        {
            if (_limit == null)
                throw new Exception();
            var newDepositAccountTerms = new DepositAccountTerms(_limit, _changeRates);
            Reset();
            return newDepositAccountTerms;
        }
    }
}