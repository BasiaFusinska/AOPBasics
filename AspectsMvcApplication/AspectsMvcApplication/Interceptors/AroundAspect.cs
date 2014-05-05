using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspectsMvcApplication.Interceptors
{
    public class AroundAspect
    {
        void OnInvoke()
        {
            //entry code

            try
            {
                // ACTUALL CODE

                //success code
            }
            catch
            {
                //exception code
            }
            finally
            {
                // exit code
            }
        }

    }
}