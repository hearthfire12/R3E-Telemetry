namespace Infrastructure.Logging
{
    using System;
    using LoggingProvider;

    public static class Logger
    {
        public static bool IsInitialized { get; private set; }

        private static ILoggingProvider _loggingProvider;

        public static void Initialize(ILoggingProvider loggingProvider)
        {
            _loggingProvider = loggingProvider;
            _loggingProvider.Info("Logger was initialized.");
            IsInitialized = true;
        }

        private static void EnsureInitialized()
        {
            if (!IsInitialized)
            {
                throw new Exception("Logger is not initialized, call Initialize() first.");
            }
        }

        public static void Info(string message)
        {
            EnsureInitialized();
            _loggingProvider.Info(message);
        }

        public static void Warn(string message)
        {
            EnsureInitialized();
            _loggingProvider.Warn(message);
        }

        public static void Fatal(string message, Exception ex)
        {
            EnsureInitialized();
            _loggingProvider.Fatal(message, ex);
        }
    }
}