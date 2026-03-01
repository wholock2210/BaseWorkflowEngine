using System.Net.NetworkInformation;
using Core.Abstractions;
using Core.Models;
using Core.ValueProviders;

namespace Modules
{
    public class AdditionNode : IWorkflowNode
    {
        public string Id {get;}
        private IValueProvider left {get;}
        private IValueProvider right {get;}
        private string Result = default!;

        public AdditionNode(string id,IValueProvider left, IValueProvider right)
        {
            Id = id;
            this.left = left;
            this.right = right;
        }

        public NodeExecutionResult Execute(IWorkflowContext context)
        {
            var plusValue = right.GetValue(context);
            

            if(left is ContextValueProvider Increase){
                var valueInc = Increase.GetValue(context);
                Console.WriteLine($"Plus {plusValue} to {valueInc}");
                var result = valueInc + plusValue;
                context.Data[Increase.Key] = result;
                Result = result.ToString();
            }
            else
            {
                var valueInc = left.GetValue(context);
                Console.WriteLine($"Plus {plusValue} to {valueInc}");
                var result = valueInc + plusValue;
                context.Data[Id] = result;
                Result = result.ToString();

            }
            
            return new NodeExecutionResult();

        }

        public void Notify(IWorkflowContext context)
        {
            var originalColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("┌──────────────────────────────────────┐");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("│       [ ADDITION VALUE NODE ]        │");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("├──────────────────────────────────────┤");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("│ Id   : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{Id}");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("│ Increase   : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{left.GetValue(context)} - {right.GetValue(context)}");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("│ Result   : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{Result}");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("└──────────────────────────────────────┘");
            Console.ForegroundColor = originalColor;
            Console.WriteLine();
        }
    }
}