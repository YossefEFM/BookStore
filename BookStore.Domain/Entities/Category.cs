using BookStore.Domain.Exceptions;

namespace BookStore.Domain.Entities;

public class Category
{
    public int Id { get; private set; }

public string Name { get; private set; } = string.Empty;

    public ICollection<Book> Books { get; private set; } = new List<Book>();

    private Category()
    {
        // Required by EF Core
    }

    public Category(string name)
    {
        SetName(name);
    }

    public void Update(string name)
    {
        SetName(name);
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException("Category name is required.");
        }

        if (name.Trim().Length > 100)
        {
            throw new DomainException("Category name cannot exceed 100 characters.");
        }

        Name = name.Trim();
    }


}
