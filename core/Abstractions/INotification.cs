using Core.Abstractions;

namespace core.Abstractions
{
    public interface INotification
    {
        void Notification(IWorkflowContext context);
    }
}