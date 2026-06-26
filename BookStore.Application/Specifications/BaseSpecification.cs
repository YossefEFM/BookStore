using System.Linq.Expressions;

namespace BookStore.Application.Specifications;

public abstract class BaseSpecification<T> : ISpecification<T>
{
    public Expression<Func<T, bool>>? Criteria { get; protected set; }

public List<Expression<Func<T, object>>> Includes { get; } = new();

    protected BaseSpecification()
    {
    }

    protected BaseSpecification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }

    protected void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }


}
