using Core.Abstractions;
using Core.Models;

namespace Modules
{
    public class NothingNode : IWorkflowNode
    {
        public string Id {get;}

        public NothingNode(string id)
        {
            Id = id;
        }

        public NodeExecutionResult Execute(IWorkflowContext context)
        {
            return new NodeExecutionResult();
        }

        public void Notification(IWorkflowContext context)
        {
        }
    }
}