using System.Net.NetworkInformation;
using Core.Abstractions;
using Core.Models;
using Core.ValueProviders;

namespace Modules
{
    public class MinusValueNode : IWorkflowNode
    {
        public string Id {get;}
        public IValueProvider ValueReduce {get;}
        public IValueProvider MinusValue {get;}

        private String Result = default!;

        public MinusValueNode(string id,IValueProvider valueReduce, IValueProvider minusValue)
        {
            Id = id;
            ValueReduce = valueReduce;
            MinusValue = minusValue;
        }

        public NodeExecutionResult Execute(IWorkflowContext context)
        {
            var minusValue = MinusValue.GetValue(context);
            

            if(ValueReduce is ContextValueProvider Reduce){
                var valueRed = Reduce.GetValue(context);
                var result = valueRed + minusValue;
                context.Data[Reduce.Key] = result;
                Result = result.ToString();
            }
            else
            {
                var valueRed = ValueReduce.GetValue(context);
                var result = valueRed + minusValue;
                context.Data[Id] = result;
                Result = result.ToString();
            }
            

            return new NodeExecutionResult();

        }

        public void Notification(IWorkflowContext context)
        {
            var originalColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("┌──────────────────────────────────────┐");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("│         [ MINUS VALUE NODE ]         │");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("├──────────────────────────────────────┤");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("│ Id   : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{Id}");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("│ Reduce   : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{ValueReduce.GetValue(context)} - {MinusValue.GetValue(context)}");
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