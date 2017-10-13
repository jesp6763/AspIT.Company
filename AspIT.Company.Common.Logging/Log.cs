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

        private static List<LogData> logs = new List<LogData>();

        /// <summary>
        /// Generates a log file
        /// </summary>
        public static void Create()
        {
            if(logs.Count == 0)
            {
                return;
            }

            string path = "Logs";
            StringBuilder stringBuilder = new StringBuilder();

            foreach(LogData log in logs)
            {
                stringBuilder.AppendLine($"[{log.DateTime.ToShortDateString()} {log.DateTime.ToLongTimeString()}]: {log.Message}");
            }

            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            File.WriteAllText(Path.Combine(path, $"{DateTime.Now.ToShortDateString()} log.txt"), stringBuilder.ToString());
        }

        /// <summary>
        /// Generates a log file
        /// </summary>
        /// <param name="filename">The path with filename and extension</param>
        public static void Create(string filename)
        {
            if(logs.Count == 0)
            {
                return;
            }

            StringBuilder stringBuilder = new StringBuilder();

            foreach(LogData log in logs)
            {
                stringBuilder.AppendLine($"[{log.DateTime.ToShortDateString()} {log.DateTime.ToLongTimeString()}]: {log.Message}");
            }

            if(!Directory.Exists(filename))
            {
                Directory.CreateDirectory(filename);
            }

            File.WriteAllText(filename, stringBuilder.ToString());
        }

        public static void AddLog(LogData logData)
        {
            logs.Add(logData);
        }
    }
}
