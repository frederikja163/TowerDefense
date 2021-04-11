using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace TowerDefense.Common
{
    public enum MessageSeverity
    {
        Debug,
        Info,
        Warn,
        Error
    }
    
    public static class Log
    {
        static Log()
        {
            // TODO: Dont do this for final version. Instead create a new file to write to.
            File.Delete("log.txt");
        }
        
        private static void ConsoleLog(string message, MessageSeverity severity)
        {
            switch (severity)
            {
                case MessageSeverity.Debug:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case MessageSeverity.Info:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case MessageSeverity.Warn:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case MessageSeverity.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
            }

            Console.WriteLine(message);
        }
        
        private static void FileLog(string message)
        {
            using StreamWriter stream = File.AppendText("log.txt");
            stream.WriteLine(message);
            stream.Flush();
        }

        public static void Debug(object? obj1, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "")
        {
#if DEBUG
            LogMessage(obj1?.ToString(), MessageSeverity.Debug, lineNumber, filePath);
#endif
        }
        public static void Debug(object? obj1, object? obj2, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "")
        {
#if DEBUG
            LogMessage(obj1?.ToString() + obj2?.ToString(), MessageSeverity.Debug, lineNumber, filePath);
#endif
        }

        public static void Info(object? obj1, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "")
            => LogMessage(obj1?.ToString(), MessageSeverity.Info, lineNumber, filePath);
        public static void Info(object? obj1, object? obj2, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "")
            => LogMessage(obj1?.ToString() + obj2?.ToString(), MessageSeverity.Info, lineNumber, filePath);
        
        public static void Warning(object? obj1, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "")
            => LogMessage(obj1?.ToString(), MessageSeverity.Warn, lineNumber, filePath);
        
        public static void Error(object? obj1, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "")
            => LogMessage(obj1?.ToString(), MessageSeverity.Error, lineNumber, filePath);

        private static void LogMessage(string? message, MessageSeverity severity, int lineNumber, string filePath)
        {
            if (message == null)
            {
                message = "";
            }
            string fileName = Path.GetFileName(filePath);
            String timeStamp = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff");
            string fullMessage = $"[{severity} {timeStamp} {fileName}#{lineNumber}]\t{message}";
            ConsoleLog(fullMessage, severity);
            FileLog(fullMessage);
        }
    }
}