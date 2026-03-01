namespace Core.Abstractions
{
    public interface IValueProvider
    {
        int GetValue(IWorkflowContext context);
    }
}