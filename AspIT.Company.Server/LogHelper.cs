using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspIT.Company.Common.Logging;

namespace AspIT.Company.Server
{
    public static class LogHelper
    {
        public static void AddLog(string message)
        {
            Console.WriteLine(message);
            Log.AddLog(new Log.LogData(message));
        }
    }
}
