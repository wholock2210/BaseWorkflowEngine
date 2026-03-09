

using Core.Abstractions;
using Core.Context;
using MySqlConnector;

namespace Pack.Database.Utilities
{
    public static class Connection
    {
        public static string connectionString {get;set;} = default!;

        public static (bool, string) GetConnection(IWorkflowContext context)
        {
            if (String.IsNullOrEmpty(connectionString))
                return (false, "please add connection string");
            try
            {
                using(var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    var idNodeConnection = context.Data["currentNode"].ToString();
                    if (!string.IsNullOrEmpty(idNodeConnection))
                    {
                        context.Data[idNodeConnection] = true;
                    };
                }
                return (true, "connected");
            }
            catch (Exception ex)
            {
                var idNodeConnection = context.Data["currentNode"].ToString();
                if (!string.IsNullOrEmpty(idNodeConnection))
                {
                    context.Data[idNodeConnection] = true;
                };
                return (false, "connect error : " + ex.Message);
            }  
        }
    }
}