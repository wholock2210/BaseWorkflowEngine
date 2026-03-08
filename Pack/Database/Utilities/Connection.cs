

using MySqlConnector;

namespace Pack.Database.Utilities
{
    public static class Connection
    {
        public static string connectionString {get;set;} = default!;

        public static bool GetConnection()
        {
            if (String.IsNullOrEmpty(connectionString))
                return false;
            try
            {
                using(var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
    }
}