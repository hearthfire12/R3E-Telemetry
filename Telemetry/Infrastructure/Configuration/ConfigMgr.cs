namespace Infrastructure.Configuration
{
    using System.Configuration;

    public static class ConfigMgr
    {
        public static bool IsSearchManual
        {
            get
            {
                string result = ConfigurationManager.AppSettings[nameof(IsSearchManual)];
                return string.IsNullOrEmpty(result) ? false : bool.Parse(result);
            }
        }

        public static string MapsRootFolder
        {
            get
            {
                string result = ConfigurationManager.AppSettings[nameof(MapsRootFolder)];
                return string.IsNullOrEmpty(result) ? null : result;
            }
        }

        public static string PublisherIp
        {
            get
            {
                string result = ConfigurationManager.AppSettings[nameof(PublisherIp)];
                return string.IsNullOrEmpty(result) ? null : result;
            }
        }

        public static int PublisherPort
        {
            get
            {
                string result = ConfigurationManager.AppSettings[nameof(PublisherPort)];
                return string.IsNullOrEmpty(result) ? -1 : int.Parse(result);
            }
        }

        public static int DistributorPort
        {
            get
            {
                string result = ConfigurationManager.AppSettings[nameof(DistributorPort)];
                return string.IsNullOrEmpty(result) ? -1 : int.Parse(result);
            }
        }
    }
}
