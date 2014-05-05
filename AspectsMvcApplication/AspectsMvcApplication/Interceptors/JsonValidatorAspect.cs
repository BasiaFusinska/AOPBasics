using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.DynamicProxy;

namespace AspectsMvcApplication.Interceptors
{
    public class JsonValidatorAspect : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var jsonText = invocation.Arguments[0] as string;

            if (!Validate(jsonText)) 
                invocation.ReturnValue = null;
            else
                invocation.Proceed();
        }

        private bool Validate(string jsonText)
        {
            return jsonText != "Some wrong data";
        }
    }
}