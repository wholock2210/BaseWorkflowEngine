using Core.Abstractions;
using Core.Context;
using Core.Engine;
using Core.Models;

namespace Modules
{
    public class LoopNode<T> : IWorkflowNode
    {
        public string Id {get;}
        private readonly IfNode<T> CONDITION;
        private string Branch = default!;

        public LoopNode(string id,IfNode<T> condition)
        {
            Id = id;
            CONDITION = condition;
        }


        public NodeExecutionResult Execute(IWorkflowContext context)
        {
            var result = CONDITION.Execute(context);

            Branch = (result.Branch == "true") ? "loop" : "exit";

            return new NodeExecutionResult
            {
                Branch = Branch
            };
        }

        public void Notify(IWorkflowContext context)
        {
            var originalColor = Console.ForegroundColor;

            if (Branch.Equals("loop"))
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("┌──────────────────────────────────────┐");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("│            [ LOOP NODE ]             │");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("├──────────────────────────────────────┤");
                Console.Write("│ ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Id   : ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{Id}");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("│ ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Status   : ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{Branch}");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("└──────────────────────────────────────┘");
                Console.ForegroundColor = originalColor;
                Console.WriteLine();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("┌──────────────────────────────────────┐");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("│            [ LOOP NODE ]             │");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("├──────────────────────────────────────┤");
                Console.Write("│ ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Id   : ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{Id}");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("│ ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Status   : ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{Branch}");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("└──────────────────────────────────────┘");
                Console.ForegroundColor = originalColor;
                Console.WriteLine();
            }

            
        }
    }


}