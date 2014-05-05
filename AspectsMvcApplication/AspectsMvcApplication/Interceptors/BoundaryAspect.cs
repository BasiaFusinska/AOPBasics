using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspectsMvcApplication.Interceptors
{
    public class BoundaryAspect
    {
        void OnEntry()
        {
            //entry code
        }

        void OnSuccess()
        {
            //success code
        }

        void OnException()
        {
            //exception code
        }

        void OnExit()
        {
            //exit code
        }
    }
}