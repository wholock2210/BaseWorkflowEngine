using System.Dynamic;
using Core.Abstractions;
using Core.Models;
using Dapper;
using MySqlConnector;
using Pack.Database.Utilities;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Pack.Database.Modules
{
    public class ReadSchemaNode : IWorkflowNode
    {
        public string Id {get;}
        private string DirectoryTemp = Path.Combine(Directory.GetCurrentDirectory(),"temp");

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

                using(StreamWriter steam = new StreamWriter(GetPath()))
                {
                    
                    string json = JsonSerializer.Serialize(result, new JsonSerializerOptions
                    {
                        WriteIndented = true
                    });
                    steam.Write(json);
                }

            }
            return new NodeExecutionResult();
        }

        private string GetPath()
        {
            if (!Directory.Exists(DirectoryTemp))
            {
                Directory.CreateDirectory(DirectoryTemp);
            }
            return Path.Combine(DirectoryTemp, "Schema.json");
        }

        public void Notify(IWorkflowContext context)
        {
            
        }
    }
}