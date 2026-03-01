
using core.Abstractions;
using Core.Models;

namespace Core.Abstractions
{
    public interface IWorkflowNode : INotification
    {
        string Id {get;}
        NodeExecutionResult Execute(IWorkflowContext context);
    }
}
