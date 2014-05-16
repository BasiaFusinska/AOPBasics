using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace AspectsMvcApplication.Services
{
    public class PaymentsReportingService : IPaymentsReportingService
    {
        private readonly ILoggingService _loggingService;

        public PaymentsReportingService(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        public void ReportPayment()
        {
            Thread.Sleep(3000);
            _loggingService.Log("Reporting payment");
        }
    }
}