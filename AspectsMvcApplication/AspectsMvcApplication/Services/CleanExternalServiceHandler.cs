using AspectsMvcApplication.Models;

namespace AspectsMvcApplication.Services
{
    public class CleanExternalServiceHandler : IExternalServiceHandler
    {
        private readonly ExternalServiceSimulator _runServiceSimulator;
        private readonly IJsonToGamePricesParser _serviceParser;

        public CleanExternalServiceHandler(ExternalServiceSimulator runServiceSimulator,
                              IJsonToGamePricesParser serviceParser)
        {
            _runServiceSimulator = runServiceSimulator;
            _serviceParser = serviceParser;
        }

        public GamePrices GetPricesFromService()
        {
            var jsonText = _runServiceSimulator.GetCurrentPrices();
            return _serviceParser.Parse(jsonText);
        }
    }
}