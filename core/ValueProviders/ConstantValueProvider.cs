using Core.Abstractions;

namespace Core.ValueProviders
{
    public class ConstantValueProvider : IValueProvider<int>
    {
        public int Value {get;}

        public ConstantValueProvider(int value)
        {
            Value = value;
        }
        public int GetValue(IWorkflowContext context)
        {
            return Value;
        }
    }
}