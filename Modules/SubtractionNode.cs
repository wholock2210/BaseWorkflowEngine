using System.Net.NetworkInformation;
using Core.Abstractions;
using Core.Models;
using Core.ValueProviders;

namespace Modules
{
    public class SubtractionNode : IWorkflowNode
    {
        public string Id {get;}
        private IValueProvider left {get;}
        private IValueProvider right {get;}

        private String Result = default!;

        public SubtractionNode(string id,IValueProvider left, IValueProvider right)
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
                var result = valueRed + minusValue;
                context.Data[Reduce.Key] = result;
                Result = result.ToString();
            }
            else
            {
                var valueRed = left.GetValue(context);
                var result = valueRed + minusValue;
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