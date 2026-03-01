namespace Core.Abstractions
{
    public interface ICondition
    {
        bool Evaluate(IWorkflowContext context);
    }
}