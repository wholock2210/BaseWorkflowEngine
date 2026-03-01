using Core.Abstractions;
using Core.Models;

namespace Modules
{
    public class NoOperationNode : IWorkflowNode
    {
        public string Id {get;}

        public NoOperationNode(string id)
        {
            Id = id;
        }

        public NodeExecutionResult Execute(IWorkflowContext context)
        {
            return new NodeExecutionResult();
        }

        public void Notify(IWorkflowContext context)
        {
        }
    }
}