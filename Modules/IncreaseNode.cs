using Core.Abstractions;
using Core.Models;
using Core.ValueProviders;

namespace Modules
{
    public class IncreaseNode : IWorkflowNode
    {
        public string Id {get;}
        public IValueProvider ValueIncrease {get;} 
        private String Result = default!;

        public IncreaseNode(string id, IValueProvider valueIncrease)
        {
            Id = id;
            ValueIncrease = valueIncrease;
        }

        public NodeExecutionResult Execute(IWorkflowContext context)
        {
            if(ValueIncrease is ContextValueProvider Increase){
                var valueInc = Increase.GetValue(context);
                var result = valueInc + 1;
                context.Data[Increase.Key] = result;
                Result = result.ToString();
            }
            else
            {
                var valueInc = ValueIncrease.GetValue(context);
                var result = valueInc + 1;
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
            Console.WriteLine("│          [ INCREASE NODE ]           │");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("├──────────────────────────────────────┤");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("│ Id   : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{Id}");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("│ Increase   : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{ValueIncrease.GetValue(context)}");
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