using System;

namespace AR.Hft.Process.Domain.Signals
{
    public class RandomSignal : ISignal
    {
        private readonly string _symbol;
        private readonly Random _random;

        public RandomSignal(string symbol)
        {
            _symbol = symbol;
            _random = new Random();
        }

        public Assessment Assess()
        {
            return new Assessment
            {
                Symbol = _symbol,
                Recommendation = _random.NextDouble() * 2 - 1
            };
        }
    }
}