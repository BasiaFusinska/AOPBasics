using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspectsMvcApplication.Services;
using Castle.DynamicProxy;

namespace AspectsMvcApplication.Interceptors
{
    public class ExceptionsAspect : IInterceptor
    {
        private readonly ILoggingService _loggingService;

        public ExceptionsAspect(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        public void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                //handle exception
                _loggingService.Log(string.Format("Exception: {0}", ex.Message));
            }

        }
    }
}