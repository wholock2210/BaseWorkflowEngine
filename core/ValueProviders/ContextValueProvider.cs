using Core.Abstractions;

namespace Core.ValueProviders
{
    public class ContextValueProvider : IValueProvider
    {
        public string Key {get;}

        public ContextValueProvider(string key)
        {
            Key = key;
        }
        public int GetValue(IWorkflowContext context)
        {
            if(!int.TryParse(context.Data[Key]?.ToString(), out var value))
                throw new Exception($"ValueIncreate [{context.Data[Key]}] is not a number");
            return value;
        }
    }
}