
using Core.Abstractions;

namespace Core.Context
{
    public class WorkflowContext : IWorkflowContext
    {
        public Dictionary<string, object> Data {get;} = new();
    }
}
