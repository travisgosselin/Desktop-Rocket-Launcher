using System.Configuration;

namespace RocketLauncher.Client.Console
{
    public class AppSettings
    {
        public static string HostUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["HostUrl"];
            }
        }

        public static string LauncherName
        {
            get
            {
                return ConfigurationManager.AppSettings["LauncherName"];
            }
        }

        public static string Mode
        {
            get
            {
                return ConfigurationManager.AppSettings["Mode"];
            }
        }
    }
}
