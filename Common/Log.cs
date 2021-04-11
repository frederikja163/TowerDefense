using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace TowerDefense.Common
{
    public enum MessageSeverity
    {
        Info,
        Warning,
        Error
    }
    
    public static class Log
    {
        static void ConsoleLog(string message, MessageSeverity severity, int lineNumber, string filePath)
        {
            switch (severity)
            {
                case MessageSeverity.Info:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                
                case MessageSeverity.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                
                case MessageSeverity.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
            }

            Console.WriteLine(message);
        }
        
        static void FileLog(string message, MessageSeverity severity, int lineNumber, string filePath)
        {
            String timeStamp = DateTime.Now.ToString();
            String severityString = "?";

            switch (severity)
            {
                case MessageSeverity.Info:
                    severityString = "INFO";
                    break;
                
                case MessageSeverity.Warning:
                    severityString = "WARNING";
                    break;
                
                case MessageSeverity.Error:
                    severityString = "ERROR";
                    break;
            }
            
            File.WriteAllTextAsync("log.txt",
                $"{timeStamp} | {severityString} | {message} | in {filePath} at line {lineNumber}");
        }

        public static void Info(string message, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "")
        {
            ConsoleLog(message, MessageSeverity.Info, lineNumber, filePath);
            FileLog(message, MessageSeverity.Info, lineNumber, filePath);
        }
        
        public static void Warning(string message, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "")
        {
            ConsoleLog(message, MessageSeverity.Warning, lineNumber, filePath);
            FileLog(message, MessageSeverity.Warning, lineNumber, filePath);
        }
        
        public static void Error(string message, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "")
        {
            ConsoleLog(message, MessageSeverity.Error, lineNumber, filePath);
            FileLog(message, MessageSeverity.Error, lineNumber, filePath);
        }
    }
}