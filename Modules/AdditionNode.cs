using System.Net.NetworkInformation;
using Core.Abstractions;
using Core.Models;
using Core.ValueProviders;

namespace Modules
{
    public class AdditionNode<T> : IWorkflowNode
    {
        public string Id {get;}
        private IValueProvider<T> left {get;}
        private IValueProvider<T> right {get;}
        private string Result = default!;

        public AdditionNode(string id,IValueProvider<T> left, IValueProvider<T> right)
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
                if(valueInc is int leftInt && plusValue is int RightInt)
                {
                    var result = leftInt + RightInt;
                    context.Data[Increase.Key] = result;
                    Result = result.ToString();
                }
            }
            else
            {
                var valueInc = left.GetValue(context);
                if(valueInc is int leftInt && plusValue is int rightInt)
                {
                    var result = leftInt + rightInt;
                    context.Data[Id] = result;
                    Result = result.ToString();
                }
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