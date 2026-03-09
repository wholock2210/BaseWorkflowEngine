
using Core.Abstractions;
using Core.Models;
using Pack.Database;
using Pack.Database.Utilities;

namespace Pack.Database.Modules
{
    public class ConnectionNode : IWorkflowNode
    {
        public string Id {get;}
        private String MessageResult = default!;

        public ConnectionNode(string id, string connectionString)
        {
            Id = id;
            Connection.connectionString = connectionString;
        }

        public NodeExecutionResult Execute(IWorkflowContext context)
        {
            var isConnected = Connection.GetConnection();
            if (isConnected.Item1)
            {
                MessageResult = isConnected.Item2;
                return new NodeExecutionResult();
            }
            else
            {
                MessageResult = isConnected.Item2;
                return new NodeExecutionResult
                {
                    IsCallBack = true
                };
            }
            
        }

        public void Notify(IWorkflowContext context)
        {
            var originalColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("┌──────────────────────────────────────┐");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("│           [ CONNECTION NODE]         │");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("├──────────────────────────────────────┤");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("│ Id   : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{Id}");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("│ status   : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{MessageResult}");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("└──────────────────────────────────────┘");
            Console.ForegroundColor = originalColor;
            Console.WriteLine();
        }
    }
}