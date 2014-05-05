using System;

namespace AspectsMvcApplication.Services
{
    public class ExternalServiceSimulator
    {
        private int _counter;

        public string GetCurrentPrices()
        {
            _counter++;

            if (_counter%3 == 0)
            {
                return @"{'GamePrice':10, 'PremiumGamePrice':15}";
            }
            if (_counter % 2 == 0)
            {
                return "Some wrong data";
            }

            throw new Exception("Service unavailable");
        }
    }
}