using Core.Abstractions;

namespace Core.Conditions
{
    public class CompareCondition<T> : ICondition
    {
        public IValueProvider<T> Left { get; }
        public IValueProvider<T> Right { get; }
        public CompareOperator Operator { get; }

        public CompareCondition(
            IValueProvider<T> left,
            IValueProvider<T> right,
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

            if (l is int L && r is int R)
            {
                return Operator switch
                {
                    CompareOperator.GreaterThan => L > R,
                    CompareOperator.LessThan => L < R,
                    CompareOperator.Equal => L == R,
                    CompareOperator.GreaterOfEqual => L >= R,
                    CompareOperator.LessOfEqual => L <= R,
                    CompareOperator.Difference => L != R,
                    _ => throw new NotSupportedException()
                };
            }
            else if(l is Boolean Lb && r is Boolean Rb){
                return Operator switch
                {
                    CompareOperator.Equal => Lb == Rb,
                    CompareOperator.Difference => Lb != Rb,
                    _ => throw new NotSupportedException()
                };
            }
            else
            {
                return false;
            }

        }
    }
}