namespace Infrastructure.Logging.LoggingProvider
{
    using System;

    public interface ILoggingProvider
    {
        void Warn(string message);
        void Info(string message);
        void Fatal(string message, Exception ex);
    }
}