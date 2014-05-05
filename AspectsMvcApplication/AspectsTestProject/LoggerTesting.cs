using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspectsTestProject
{
    public interface ILoggerTesting
    {
        void TestLoggedMethod();
    }

    public class LoggerTesting: ILoggerTesting
    {
        public void TestLoggedMethod()
        {
        }
    }
}
