using System.Linq.Expressions;

namespace CafeManagementApp.DAL.Model
{
    //public class IncludesModel<T>
    //{
    //    protected Expression<Func<TInput, object>> ConvertToTObjectExpression<TInput, TOutput>(Expression<Func<TInput, TOutput>> expression)
    //    {
    //        if (expression == null)
    //            return null;

    //        var parameter = expression.Parameters[0];
    //        var body = Expression.Convert(expression.Body, typeof(object));
    //        return Expression.Lambda<Func<TInput, object>>(body, parameter);
    //    }
    //    protected Expression<Func<object, object>> ConvertToObjectObjectExpression<TInput, TOutput>(Expression<Func<TInput, TOutput>> expression)
    //    {
    //        if (expression == null)
    //            return null;

    //        var parameter = Expression.Parameter(typeof(object), "obj");
    //        var castedParameter = Expression.Convert(parameter, typeof(TInput));

    //        var body = Expression.Invoke(expression, castedParameter);
    //        var convertedBody = Expression.Convert(body, typeof(object));

    //        // Return the new lambda expression
    //        return Expression.Lambda<Func<object, object>>(convertedBody, parameter);
    //    }

    //}

    //public class IncludesModel<T, T2> : IncludesModel<T>
    //{
    //    public IncludesModel(Expression<Func<T, T2>> include,
    //        Expression<Func<T2>> thenInclude = null)
    //    {
    //        IncludeExpression = new IncludesExpressionChain<T, T2>
    //        {
    //            Include = include,
    //            ThenInclude = thenInclude
    //        };
    //    }

    //    public IncludesExpressionChain<T, T2> IncludeExpression { get; set; }

    //    public IncludesExpressionChain<T, object> GetGenericObjectExpression
    //    {
    //        get
    //        {
    //            if (IncludeExpression == null)
    //                return null;

    //            return new IncludesExpressionChain<T, object, object>
    //            {
    //                Include = ConvertToTObjectExpression(IncludeExpression.Include),
    //                ThenInclude = ConvertToObjectObjectExpression(IncludeExpression.ThenInclude)
    //            };
    //        }
    //    }
    //}

    //public class IncludesExpressionChain<T, T2, T3>()
    //{
    //    public Expression<Func<T, T2>> Include { get; set; }
    //    public Expression<Func<T2, T3>> ThenInclude { get; set; }

    //    public ChainThenInclude<T2, T3> ChainThenInclude { get; set; }
    //}
    
    //public class ChainThenInclude<T, T2>()
    //{
    //    public Expression<Func<T, T2>> ThenInclude { get; set; }
    //    ChainThenInclude<T2, object> NextChainThenInclude { get; set; }
    //}

    public class IncludesExpressionChain<T>
    {
        public Expression<Func<T, object>> Include { get; set; }
        public Expression<Func<object, object>>[] ThenIncludes { get; set; }
    }
}
