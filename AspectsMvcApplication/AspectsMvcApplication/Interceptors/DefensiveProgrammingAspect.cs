using System;
using Castle.DynamicProxy;

namespace AspectsMvcApplication.Interceptors
{
    public class DefensiveProgrammingAspect : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var parameters = invocation.Method.GetParameters();
            var arguments = invocation.Arguments;
            for (var i = 0; i < arguments.Length; i++)
            {
                if (arguments[i] == null)
                    throw new ArgumentNullException(parameters[i].Name);
                if (arguments[i] is int && (int)arguments[i] <= 0)
                    throw new ArgumentException("", parameters[i].Name);
            }

            invocation.Proceed();
        }
    }
}