

using Core.Abstractions;
using Core.Models;

namespace Modules
{
    public class CreateValNode<T> : IWorkflowNode
    {
        public string Id {get;}
        private string ValueName {get;}
        private IValueProvider<T> Value {get;}

        public CreateValNode(string id,string valueName, IValueProvider<T> value)
        {
            Id = id;
            ValueName = valueName;
            Value = value;
        }

        public NodeExecutionResult Execute(IWorkflowContext context)
        {

            context.Data[ValueName] = Value.GetValue(context);
            return new NodeExecutionResult();
        }

        public void Notify(IWorkflowContext context)
        {
            var originalColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("┌──────────────────────────────────────┐");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("│        [ CREATE VALUE NODE ]         │");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("├──────────────────────────────────────┤");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("│ Id   : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{Id}");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("│ Key   : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{ValueName}");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("│ Value : ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{Value}");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("└──────────────────────────────────────┘");
            Console.ForegroundColor = originalColor;
            Console.WriteLine();
        }
    }
}