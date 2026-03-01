using System.Net.NetworkInformation;
using Core.Abstractions;
using Core.Models;
using Core.ValueProviders;

namespace Modules
{
    public class PlusValueNode : IWorkflowNode
    {
        public string Id {get;}
        public IValueProvider ValueIncrease {get;}
        public IValueProvider PlusValue {get;}
        private string Result = default!;

        public PlusValueNode(string id,IValueProvider valueIncrease, IValueProvider plusValue)
        {
            Id = id;
            ValueIncrease = valueIncrease;
            PlusValue = plusValue;
        }

        public NodeExecutionResult Execute(IWorkflowContext context)
        {
            var plusValue = PlusValue.GetValue(context);
            

            if(ValueIncrease is ContextValueProvider Increase){
                var valueInc = Increase.GetValue(context);
                Console.WriteLine($"Plus {plusValue} to {valueInc}");
                var result = valueInc + plusValue;
                context.Data[Increase.Key] = result;
                Result = result.ToString();
            }
            else
            {
                var valueInc = ValueIncrease.GetValue(context);
                Console.WriteLine($"Plus {plusValue} to {valueInc}");
                var result = valueInc + plusValue;
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
            Console.WriteLine("│       [ INCREASE VALUE NODE ]        │");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("├──────────────────────────────────────┤");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("│ Id   : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{Id}");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("│ Increase   : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{ValueIncrease.GetValue(context)} - {PlusValue.GetValue(context)}");
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