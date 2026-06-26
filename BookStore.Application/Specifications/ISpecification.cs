using System.Linq.Expressions;

namespace BookStore.Application.Specifications;

public interface ISpecification<T>
{
    Expression<Func<T, bool>>? Criteria { get; }

List<Expression<Func<T, object>>> Includes { get; }


}
