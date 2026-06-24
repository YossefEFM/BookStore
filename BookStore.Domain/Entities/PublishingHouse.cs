using BookStore.Domain.Exceptions;

namespace BookStore.Domain.Entities;

public class PublishingHouse
{
    public int Id { get; private set; }

public string Name { get; private set; } = string.Empty;

    public string Address { get; private set; } = string.Empty;

    public ICollection<Book> Books { get; private set; } = new List<Book>();

    private PublishingHouse()
    {
        // Required by EF Core
    }

    public PublishingHouse(string name, string address)
    {
        SetName(name);
        SetAddress(address);
    }

    public void Update(string name, string address)
    {
        SetName(name);
        SetAddress(address);
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException("Publishing house name is required.");
        }

        if (name.Trim().Length > 150)
        {
            throw new DomainException("Publishing house name cannot exceed 150 characters.");
        }

        Name = name.Trim();
    }

    private void SetAddress(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
        {
            throw new DomainException("Publishing house address is required.");
        }

        if (address.Trim().Length > 300)
        {
            throw new DomainException("Publishing house address cannot exceed 300 characters.");
        }

        Address = address.Trim();
    }

}
