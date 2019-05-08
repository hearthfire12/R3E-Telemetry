namespace Infrastructure.Configuration
{
    using System.Diagnostics;

    public static class SupportedGames
    {
        public static bool IsR3ERunning =>
            Process.GetProcessesByName("RRRE").Length > 0
            || Process.GetProcessesByName("RRRE64").Length > 0;
    }
}