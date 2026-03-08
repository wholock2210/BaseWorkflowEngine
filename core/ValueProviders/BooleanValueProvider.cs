using Core.Abstractions;

namespace Core.ValueProviders
{
    public class BooleanValueProvider : IValueProvider<Boolean>
    {
        public bool GetValue(IWorkflowContext context)
        {
            throw new NotImplementedException();
        }
    }
}