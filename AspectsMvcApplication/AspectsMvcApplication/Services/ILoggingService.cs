﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspectsMvcApplication.Services
{
    public interface ILoggingService
    {
        void Log(string message);
    }
}
