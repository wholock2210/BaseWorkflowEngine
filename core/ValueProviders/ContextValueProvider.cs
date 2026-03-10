using Core.Abstractions;

namespace Core.ValueProviders
{
    public class ContextValueProvider : IValueProvider<int>
    {
        public string Key { get; }

        public ContextValueProvider(string key)
        {
            Key = key;
        }
        public int GetValue(IWorkflowContext context)
        {
            if (!int.TryParse(context.Data[Key]?.ToString(), out var value))
                throw new Exception($"Value [{context.Data[Key]}] is not a number");
            return value;
        }
        public string ToString(IWorkflowContext context)
        {
            if (!context.Data.TryGetValue(Key, out var value))
                throw new Exception($"Key '{Key}' not found");

            return value?.ToString() ?? "";
        }
    }
}