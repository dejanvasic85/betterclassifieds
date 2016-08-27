using System;

namespace Paramount.Betterclassifieds.Console
{
    internal interface ILogger
    {
        void Info(string message);
        void Error(Exception exception);
        void Error(string message);
        void Warn(string message);
        void Progress(char p = '.');
    }

    public class ConsoleLogger : ILogger
    {
        public void Info(string message)
        {
            System.Console.WriteLine();
            System.Console.ForegroundColor = ConsoleColor.Cyan;
            System.Console.WriteLine(message);
            System.Console.ResetColor();
        }

        public void Progress(char p = '.')
        {
            System.Console.Write(p);
        }

        public void Error(Exception exception)
        {
            System.Console.WriteLine();
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine(exception.Message);
            System.Console.WriteLine(exception.StackTrace);

            var inner = exception.InnerMostException();
            if (inner != null)
            {
                System.Console.WriteLine("-- INNER EXCEPTION --");
                System.Console.WriteLine(inner.Message);
                System.Console.WriteLine(inner.StackTrace);
            }

            System.Console.ResetColor();
        }

        public void Error(string message)
        {
            System.Console.WriteLine();
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine(message);
            System.Console.ResetColor();
        }

        public void Warn(string message)
        {
            System.Console.WriteLine();
            System.Console.ForegroundColor = ConsoleColor.DarkYellow;
            System.Console.WriteLine(message);
            System.Console.ResetColor();
        }
    }
}
