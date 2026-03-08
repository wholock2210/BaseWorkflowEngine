namespace Core.Abstractions
{
    public interface IValueProvider<T>
    {
        T GetValue(IWorkflowContext context);
    }
}