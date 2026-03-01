using Core.Abstractions;

namespace Core.Conditions
{
    public class CompareCondition : ICondition
    {
        public IValueProvider Left {get;}
        public IValueProvider Right {get;}
        public CompareOperator Operator {get;}

        public CompareCondition(
            IValueProvider left, 
            IValueProvider right,
            CompareOperator op)
        {
            Left = left;
            Right = right;
            Operator = op;
        }

        public bool Evaluate(IWorkflowContext context)
        {
            var l = Left.GetValue(context);
            var r = Right.GetValue(context);


            return Operator switch
            {
                CompareOperator.GreaterThan => l > r,
                CompareOperator.LessThan => l < r,
                CompareOperator.Equal => l == r,
                CompareOperator.GreaterOfEqual => l >= r,
                CompareOperator.LessOfEqual => l <= r,
                CompareOperator.Difference => l != r,
                _ => throw new NotSupportedException()
            };


        }
    }
}