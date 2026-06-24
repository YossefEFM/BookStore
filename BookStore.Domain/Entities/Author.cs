using BookStore.Domain.Exceptions;

namespace BookStore.Domain.Entities;

public class Author
{
    public int Id { get; private set; }

public string Name { get; private set; } = string.Empty;

    public string Bio { get; private set; } = string.Empty;

    public ICollection<Book> Books { get; private set; } = new List<Book>();

    private Author()
    {
        // Required by EF Core
    }

    public Author(string name, string bio)
    {
        SetName(name);
        SetBio(bio);
    }

    public void Update(string name, string bio)
    {
        SetName(name);
        SetBio(bio);
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException("Author name is required.");
        }

        if (name.Trim().Length > 150)
        {
            throw new DomainException("Author name cannot exceed 150 characters.");
        }

        Name = name.Trim();
    }

    private void SetBio(string bio)
    {
        if (string.IsNullOrWhiteSpace(bio))
        {
            throw new DomainException("Author bio is required.");
        }

        if (bio.Trim().Length > 2000)
        {
            throw new DomainException("Author bio cannot exceed 2000 characters.");
        }

        Bio = bio.Trim();
    }


}
