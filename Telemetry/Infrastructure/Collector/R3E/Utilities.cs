// Taken from https://github.com/sector3studios/r3e-api/blob/master/sample-csharp/src/Utilities.cs
namespace Infrastructure.Collector.R3E
{
    using System;
    using System.Diagnostics;

    namespace R3E
    {
        public class Utilities
        {
            public static Single RpsToRpm(Single rps)
            {
                return rps * (60 / (2 * (Single)Math.PI));
            }

            public static Single MpsToKph(Single mps)
            {
                return mps * 3.6f;
            }

            public static bool IsRrreRunning()
            {
                return Process.GetProcessesByName("RRRE").Length > 0;
            }
        }
    }
}