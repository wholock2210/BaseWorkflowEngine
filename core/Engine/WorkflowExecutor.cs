using Core.Abstractions;
using Core.Context;
using Core.Models;

namespace Core.Engine
{
    public class WorkflowExecutor
    {
        public void Run(
            WorkflowDefinition workflow,
            Dictionary<string, IWorkflowNode> nodes,
            IWorkflowContext context
        )
        {
            var currentNodeId = workflow.StartNodeId;

            while(currentNodeId != null)
            {
                var node =  nodes[currentNodeId];
                var result = node.Execute(context);

                var originalColor = Console.ForegroundColor;

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("\t\t[]");
                Console.WriteLine("\t\t[]");
                Console.WriteLine("\t\t[]");
                Console.WriteLine("\t\t\\/");
                Console.ForegroundColor = originalColor;

                node.Notification(context);



                currentNodeId = workflow.ResolveNext(
                    currentNodeId,
                    result.Branch
                );
            }
        }
}
}
