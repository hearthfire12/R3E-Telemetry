namespace TelemetryComponent
{
    using System.Windows;
    using Infrastructure.Logging;
    using Infrastructure.Logging.LoggingProvider;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Logger.Initialize(new ConsoleLoggingProvider());
        }
    }
}
