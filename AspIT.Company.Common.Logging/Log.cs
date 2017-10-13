using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AspIT.Company.Common.Logging
{
    public class Log
    {
        public struct LogData
        {
            public LogData(string message)
            {
                DateTime = DateTime.Now;
                Message = message;
            }

            public LogData(DateTime dateTime, string message)
            {
                DateTime = dateTime;
                Message = message;
            }

            /// <summary>
            /// Gets the date and time the log was made
            /// </summary>
            public DateTime DateTime { get; }
            /// <summary>
            /// Gets the log message
            /// </summary>
            public string Message { get; }
        }

        private static List<LogData> logs;

        /// <summary>
        /// Generates a log file
        /// </summary>
        public static void Create()
        {
            string path = "Logs";
            StringBuilder stringBuilder = new StringBuilder();

            foreach(LogData log in logs)
            {
                stringBuilder.AppendLine($"{log.DateTime.ToShortDateString()} {log.DateTime.ToShortTimeString()}: {log.Message}");
            }

            if(Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            File.AppendAllText(Path.Combine(path, $"{DateTime.Now.ToString()} logs.txt"), stringBuilder.ToString());
        }

        public static void AddLog(LogData logData)
        {
            logs.Add(logData);
        }
    }
}
