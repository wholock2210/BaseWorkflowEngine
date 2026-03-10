using System.Dynamic;
using Core.Abstractions;
using Core.Models;
using Dapper;
using MySqlConnector;
using Pack.Database.Utilities;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Schema;

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
            string sql = "SHOW TABLES";
            using(var conn = new MySqlConnection(Connection.connectionString))
            {
                var tables = conn.Query(sql);

                using(StreamWriter steam = new StreamWriter(GetPath()))
                {
                    Dictionary<string, List<dynamic>> database = new Dictionary<string, List<dynamic>>();
                    foreach(var table in tables)
                    {
                        string query = $"DESCRIBE {table.Tables_in_OtoParking}";
                        var tableSchema = conn.Query(query).ToList();
                        database.Add(table.Tables_in_OtoParking,tableSchema);
                    }
                    string json = JsonSerializer.Serialize(database, new JsonSerializerOptions
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