using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineAPI
{
    internal enum LogLevel
    {
        TRACE,
        INFO,
        WARN,
        ERROR,
        FATAL
    }

    internal readonly struct LogData
    {
        public readonly string message;
        public readonly LogLevel level;

        public LogData(string message, LogLevel level)
        {
            this.message = message;
            this.level = level;
        }
    }

    internal static class Debugger
    {
        private static readonly string outputPath = Path.GetFullPath("log.data");
        private static readonly object obj = new object();
        private static readonly ConsoleColor[] logColors = { ConsoleColor.White, ConsoleColor.Green, ConsoleColor.Yellow, ConsoleColor.Red, ConsoleColor.Magenta };

        public static void Log(LogData data)
        {
            lock (obj) {
                string format = $"<ENGINE-LOG>({DateTime.Now}) [{data.level}]: {data.message}\n";
                Console.ForegroundColor = logColors[(int)data.level];
                Console.Write(format);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public static void Log(string message)
        {
            lock (obj)
            {
                string format = $"<ENGINE-LOG>({DateTime.Now}) [{LogLevel.INFO}]: {message}\n";
                Console.Write(format);
            }
        }

        public static void Log(object data)
        {
            lock (obj)
            {
                string format = $"<ENGINE-LOG>({DateTime.Now}) [{LogLevel.INFO}]: {data}\n";
                Console.Write(format);
            }
        }

        public static void Log(string message, LogLevel logLevel)
        {
            lock (obj)
            {
                string format = $"<ENGINE-LOG>({DateTime.Now}) [{logLevel}]: {message}\n";
                Console.ForegroundColor = logColors[(int)logLevel];
                Console.Write(format);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
