using Core.Abstractions;

namespace core.Abstractions
{
    public interface INotification
    {
        void Notify(IWorkflowContext context);
    }
}