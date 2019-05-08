namespace Infrastructure.Logging.LoggingProvider
{
    using System;

    public class ConsoleLoggingProvider : ILoggingProvider
    {
        private void Write(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine($"Logger: {message}");
            Console.ResetColor();
        }

        public void Warn(string message)
        {
            Write(message, ConsoleColor.Yellow);
        }

        public void Info(string message)
        {
            Write(message, Console.ForegroundColor);
        }

        public void Fatal(string message, Exception ex)
        {
            Write($"{message}\n{ex}", ConsoleColor.Red);
        }
    }
}