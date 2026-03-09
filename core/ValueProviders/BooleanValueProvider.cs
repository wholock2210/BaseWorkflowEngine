using Core.Abstractions;

namespace Core.ValueProviders
{
    public class BooleanValueProvider : IValueProvider<Boolean>
    {
        public string? Key {get;}
        public bool Value {get;}

        public BooleanValueProvider(string key)
        {
            Key = key;
        }
        public BooleanValueProvider(Boolean value)
        {
            Value = value;
        }
        public bool GetValue(IWorkflowContext context)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(Key))
            {
                Object rawValue = context.Data[Key];
                if(rawValue is bool)
                {
                    result = (bool)rawValue;
                }
            }
            else
            {
                result = Value;
            }
            return Value;
        }
    }
}