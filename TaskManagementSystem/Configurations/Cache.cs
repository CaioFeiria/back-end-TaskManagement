using System.Runtime.Caching;

namespace TaskManagementSystem.Configurations
{
    public class Cache
    {
        public static int GetDefaultCacheTimeInSeconds()
        {
            return int.Parse(System.Configuration.ConfigurationManager.AppSettings["DefaultCacheTimeInSeconds"]);
        }
    }
}