using System.Net.NetworkInformation;
using Core.Abstractions;
using Core.Models;
using Core.ValueProviders;

namespace Modules
{
    public class SubtractionNode<T> : IWorkflowNode
    {
        public string Id {get;}
        private IValueProvider<T> left {get;}
        private IValueProvider<T> right {get;}

        private String Result = default!;

        public SubtractionNode(string id,IValueProvider<T> left, IValueProvider<T> right)
        {
            Id = id;
            this.left = left;
            this.right = right;
        }

        public NodeExecutionResult Execute(IWorkflowContext context)
        {
            var minusValue = right.GetValue(context);
            

            if(left is ContextValueProvider Reduce){
                var valueRed = Reduce.GetValue(context);
                if (valueRed is int leftInt && minusValue is int rightInt)
                {
                    var result = leftInt + rightInt;
                    context.Data[Reduce.Key] = result;
                    Result = result.ToString();
                }
            }
            else
            {
                var valueRed = left.GetValue(context);
                if (valueRed is int leftInt && minusValue is int rightInt)
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
            Console.WriteLine("│      [ SUBTRACTION VALUE NODE ]      │");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("├──────────────────────────────────────┤");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("│ Id   : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{Id}");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("│ Reduce   : ");
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