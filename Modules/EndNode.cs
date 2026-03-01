using Core.Abstractions;
using Core.Models;

namespace Modules
{
    public class EndNode : IWorkflowNode
    {
        public string Id {get;}
        private readonly string MESSAGE;

        public EndNode(string id,string message)
        {
            Id = id;
            MESSAGE = message;
        }
        public NodeExecutionResult Execute(IWorkflowContext context)
        {
            return new NodeExecutionResult();
        }

        public void Notification(IWorkflowContext context)
        {
            var originalColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("┌──────────────────────────────────────┐");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("│             [ END NODE ]             │");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("├──────────────────────────────────────┤");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("│ Id   : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{Id}");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("│ Message   : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{MESSAGE}");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("└──────────────────────────────────────┘");
            Console.ForegroundColor = originalColor;
            Console.WriteLine();
        }
    }
}