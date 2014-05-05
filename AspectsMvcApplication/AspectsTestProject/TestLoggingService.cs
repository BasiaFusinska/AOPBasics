using System.Collections.Generic;
using AspectsMvcApplication.Services;

namespace AspectsTestProject
{
    public class TestLoggingService : ILoggingService
    {
        private readonly IList<string> _logs = new List<string>();
 
        public void Log(string message)
        {
            _logs.Add(message);
        }

        public string this[int index]
        {
            get { return _logs[index]; }
        }

        public int Count
        {
            get { return _logs.Count; }
        }
    }
}
