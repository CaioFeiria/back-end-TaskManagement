namespace TaskManagementSystem.Configurations
{
    public class Database
    {
        public static string GetConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["TaskManagement"].ConnectionString;
        }
    }
}