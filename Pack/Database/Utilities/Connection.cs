

using MySqlConnector;

namespace Pack.Database.Utilities
{
    public static class Connection
    {
        public static string connectionString {get;set;} = default!;

        public static (bool, string) GetConnection()
        {
            if (String.IsNullOrEmpty(connectionString))
                return (false, "please add connection string");
            try
            {
                using(var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                }
                return (true, "connected");
            }
            catch (Exception ex)
            {
                return (false, "connect error : " + ex.Message);
            }  
        }
    }
}