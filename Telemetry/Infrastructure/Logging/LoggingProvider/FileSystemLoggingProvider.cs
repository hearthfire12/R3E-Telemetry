namespace Infrastructure.Logging.LoggingProvider
{
    using System;
    using System.IO;
    using System.IO.Abstractions;

    public abstract class FileSystemLoggingProvider : ILoggingProvider
    {
        private IFileSystem _fileSystem;
        private const string RootFolder = @"D:\raceroom-telemetry\logs\log.txt";
        private const string MessageFormat = "[@dateTime]:[@messageType]:[@message]\n[@]";

        private void AppendMessage(string message)
        {
            var logFile = _fileSystem.FileInfo.FromFileName(RootFolder);

            Stream fileStream = null;

            if (!logFile.Exists)
            {
                if (!logFile.Directory.Exists)
                {
                    logFile.Directory.Create();
                }

                fileStream = logFile.Create();
            }

            if (fileStream == null)
            {
                fileStream = logFile.Open(FileMode.Append, FileAccess.Write);
            }

            using (var stream = new StreamWriter(fileStream))
            {
                stream.WriteLineAsync($"\n{message}");
            }
        }

        public void Warn(string message)
        {
            AppendMessage(message);
        }

        public void Info(string message)
        {
            AppendMessage(message);
        }

        public void Fatal(string message, Exception ex)
        {
            AppendMessage(message);
        }
    }
}