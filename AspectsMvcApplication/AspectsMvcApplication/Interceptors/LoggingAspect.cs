using System;
using AspectsMvcApplication.Services;
using Castle.DynamicProxy;

namespace AspectsMvcApplication.Interceptors
{
    public class LoggingAspect : IInterceptor
    {
        private readonly ILoggingService _loggingService;

        public LoggingAspect(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        public void Intercept(IInvocation invocation)
        {
            _loggingService.Log(string.Format("{0} started", invocation.Method.Name));

            invocation.Proceed();

            _loggingService.Log(string.Format("{0} completed", invocation.Method.Name));
        }
    }
}