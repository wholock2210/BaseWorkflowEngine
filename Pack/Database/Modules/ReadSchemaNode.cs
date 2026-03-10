using System.Dynamic;
using Core.Abstractions;
using Core.Models;
using Dapper;
using MySqlConnector;
using Pack.Database.Utilities;

namespace Pack.Database.Modules
{
    public class ReadSchemaNode : IWorkflowNode
    {
        public string Id {get;}

        public ReadSchemaNode(string id)
        {
            Id = id;
        }

        public NodeExecutionResult Execute(IWorkflowContext context)
        {
            string sql = "DESCRIBE OtoParking.AppUser";
            using(var conn = new MySqlConnection(Connection.connectionString))
            {
                var result = conn.Query(sql);
                foreach(var row in result)
                {
                    Console.WriteLine(row);
                }
            }
            return new NodeExecutionResult();
        }

        public void Notify(IWorkflowContext context)
        {
            
        }
    }
}