using System;
using AspectsMvcApplication.Models;
using AspectsMvcApplication.Services;
using Castle.DynamicProxy;

namespace AspectsMvcApplication.Interceptors
{
    public class ServiceAspect : IInterceptor
    {
        private readonly ILoggingService _loggingService;

        public ServiceAspect(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        public void Intercept(IInvocation invocation)
        {
            GamePrices returnValue = null;
            var succeed = false;
            while (!succeed)
            {
                try
                {
                    invocation.Proceed();

                    returnValue = invocation.ReturnValue as GamePrices;
                    if (returnValue != null)
                        succeed = true;
                }
                catch (Exception ex)
                {
                    _loggingService.Log(string.Format("Exception: {0}", ex.Message));
                }
            }

            invocation.ReturnValue = returnValue;
        }
    }
}