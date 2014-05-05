using System.Transactions;
using Castle.DynamicProxy;

namespace AspectsMvcApplication.Interceptors
{
    public class TransactionAspect : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            using (var scope = new TransactionScope())
            {
                //reries
                var retries = 3;
                var succeeded = false;
                while (!succeeded)
                {
                    try
                    {
                        invocation.Proceed();

                        scope.Complete();
                        succeeded = true;
                    }
                    catch
                    {
                        if (retries >= 0)
                            retries--;
                        else
                            throw;
                    }
                }
            }

        }
    }
}