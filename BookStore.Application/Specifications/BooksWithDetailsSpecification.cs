using BookStore.Domain.Entities;

namespace BookStore.Application.Specifications;

public class BooksWithDetailsSpecification : BaseSpecification<Book>
{
    public BooksWithDetailsSpecification()
    {
        AddInclude(x => x.Author);
        AddInclude(x => x.Category);
        AddInclude(x => x.PublishingHouse);
    }


public BooksWithDetailsSpecification(int id)
    : base(x => x.Id == id)
    {
        AddInclude(x => x.Author);
        AddInclude(x => x.Category);
        AddInclude(x => x.PublishingHouse);
    }


}
