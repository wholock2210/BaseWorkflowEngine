using Core.Abstractions;
using Core.Conditions;
using Core.Models;

namespace Modules
{
    public class IfNode : IWorkflowNode
    {
        public string Id {get;}
        private readonly CompareCondition CONDITION;
        private string Result = default!;

        public IfNode(string id, CompareCondition condition)
        {
            Id = id;
            CONDITION = condition;
        }

        public NodeExecutionResult Execute(IWorkflowContext context)
        {
            var boolCondition = CONDITION.Evaluate(context);

            Result = boolCondition ? "true" : "false";

            return new NodeExecutionResult
            {
                Branch = Result
            };
        }

        public void Notify(IWorkflowContext context)
        {
            var originalColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("┌──────────────────────────────────────┐");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("│             [ IF NODE ]              │");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("├──────────────────────────────────────┤");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("│ Id   : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{Id}");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("│ Bool   : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{CONDITION.Left} [{CONDITION.Operator}] {CONDITION.Right}");
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