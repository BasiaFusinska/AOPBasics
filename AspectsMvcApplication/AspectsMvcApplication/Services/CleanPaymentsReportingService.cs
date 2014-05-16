using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using AspectsMvcApplication.Aspects;

namespace AspectsMvcApplication.Services
{
    public class CleanPaymentsReportingService : IPaymentsReportingService
    {
        private readonly ILoggingService _loggingService;

        public CleanPaymentsReportingService(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        [TaskAspect]
        public void ReportPayment()
        {
            Thread.Sleep(3000);
            _loggingService.Log("Reporting payment");
        }
    }
}