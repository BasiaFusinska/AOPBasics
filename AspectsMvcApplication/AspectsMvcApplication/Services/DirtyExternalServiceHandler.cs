using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspectsMvcApplication.Models;
using Newtonsoft.Json.Linq;

namespace AspectsMvcApplication.Services
{
    public class DirtyExternalServiceHandler : IExternalServiceHandler
    {
        private readonly ExternalServiceSimulator _runServiceSimulator;
        private readonly IJsonToGamePricesParser _serviceParser;
        private readonly ILoggingService _loggingService;

        public DirtyExternalServiceHandler(ExternalServiceSimulator runServiceSimulator,
                                           IJsonToGamePricesParser serviceParser,
                                           ILoggingService loggingService)
        {
            _runServiceSimulator = runServiceSimulator;
            _serviceParser = serviceParser;
            _loggingService = loggingService;
        }

        public GamePrices GetPricesFromService()
        {
            GamePrices returnValue = null;
            var succeed = false;
            while (!succeed)
            {
                try
                {
                    //--------------------------------- exception may happen
                    var jsonText = _runServiceSimulator.GetCurrentPrices();

                    //-----------json may be wrong
                    returnValue = _serviceParser.Parse(jsonText);

                    if (returnValue != null)
                        succeed = true;
                }
                catch (Exception ex)
                {
                    _loggingService.Log(string.Format("Exception: {0}", ex.Message));
                }
            }

            return returnValue;
        }

    }
}