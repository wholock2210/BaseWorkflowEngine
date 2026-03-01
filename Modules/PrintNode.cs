using Core.Abstractions;
using Core.Models;

namespace Modules
{
    public class PrintNode : IWorkflowNode
    {
        public string Id {get;}
        public IValueProvider ValueProvider {get;}

        public PrintNode(string id, IValueProvider valueProvider)
        {
            Id = id;
            ValueProvider = valueProvider;
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
            Console.WriteLine("│             [ PRINT NODE ]           │");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("├──────────────────────────────────────┤");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("│ Id   : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{Id}");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("│ Value   : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{ValueProvider.GetValue(context)}");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("└──────────────────────────────────────┘");
            Console.ForegroundColor = originalColor;
            Console.WriteLine();
        }
    }
}