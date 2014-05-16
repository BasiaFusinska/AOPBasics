using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PostSharp.Aspects;
using PostSharp.Aspects.Advices;

namespace AspectsMvcApplication.Aspects
{
    [Serializable]
    public class CacheAspect : OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            var cachedValue = HttpContext.Current.Cache[(string) args.Arguments[0]];
            if (cachedValue == null) return;
            args.ReturnValue = cachedValue;

            args.FlowBehavior = FlowBehavior.Return;
        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            HttpContext.Current.Cache[(string) args.Arguments[0]] = args.ReturnValue;
        }
    }
}