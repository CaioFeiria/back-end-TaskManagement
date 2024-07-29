using System;
using System.IO;

namespace TaskManagementSystem.Configurations
{
    public class Logger
    {
        private static string GetPath()
        {
            return System.Configuration.ConfigurationManager.AppSettings["logPath"];
        }

        private static string GetFileName()
        {
            return $"{DateTime.Now.ToString("yyyy-MM-dd")}.txt";
        }

        public static string GetFullPath()
        {
            return Path.Combine(GetPath(), GetFileName());
        }
    }
}