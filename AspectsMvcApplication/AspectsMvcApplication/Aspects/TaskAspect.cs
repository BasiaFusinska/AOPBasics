using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using PostSharp.Aspects;

namespace AspectsMvcApplication.Aspects
{
    [Serializable]
    public class TaskAspect : MethodInterceptionAspect
    {
        public override void OnInvoke(MethodInterceptionArgs args)
        {
            Task.Run(() => args.Proceed());
        }
    }
}