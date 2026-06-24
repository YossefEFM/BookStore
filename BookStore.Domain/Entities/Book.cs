using BookStore.Domain.Exceptions;

namespace BookStore.Domain.Entities;

public class Book
{
    public int Id { get; private set; }


public string Title { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    public decimal Price { get; private set; }

    // Foreign Keys
    public int AuthorId { get; private set; }

    public int CategoryId { get; private set; }

    public int PublishingHouseId { get; private set; }

    // Navigation Properties
    public Author? Author { get; private set; }

    public Category? Category { get; private set; }

    public PublishingHouse? PublishingHouse { get; private set; }

    // Required by EF Core
    private Book()
    {
    }

    public Book(
        string title,
        string description,
        decimal price,
        int authorId,
        int categoryId,
        int publishingHouseId)
    {
        SetTitle(title);
        SetDescription(description);
        SetPrice(price);
        SetAuthor(authorId);
        SetCategory(categoryId);
        SetPublishingHouse(publishingHouseId);
    }

    public void Update(
        string title,
        string description,
        decimal price,
        int authorId,
        int categoryId,
        int publishingHouseId)
    {
        SetTitle(title);
        SetDescription(description);
        SetPrice(price);
        SetAuthor(authorId);
        SetCategory(categoryId);
        SetPublishingHouse(publishingHouseId);
    }

    public void UpdatePrice(decimal newPrice)
    {
        SetPrice(newPrice);
    }

    private void SetTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new DomainException("Book title is required.");
        }

        if (title.Trim().Length > 200)
        {
            throw new DomainException("Book title cannot exceed 200 characters.");
        }

        Title = title.Trim();
    }

    private void SetDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new DomainException("Book description is required.");
        }

        if (description.Trim().Length > 2000)
        {
            throw new DomainException("Book description cannot exceed 2000 characters.");
        }

        Description = description.Trim();
    }

    private void SetPrice(decimal price)
    {
        if (price <= 0)
        {
            throw new DomainException("Book price must be greater than zero.");
        }

        Price = price;
    }

    private void SetAuthor(int authorId)
    {
        if (authorId <= 0)
        {
            throw new DomainException("A valid author is required.");
        }

        AuthorId = authorId;
    }

    private void SetCategory(int categoryId)
    {
        if (categoryId <= 0)
        {
            throw new DomainException("A valid category is required.");
        }

        CategoryId = categoryId;
    }

    private void SetPublishingHouse(int publishingHouseId)
    {
        if (publishingHouseId <= 0)
        {
            throw new DomainException("A valid publishing house is required.");
        }

        PublishingHouseId = publishingHouseId;
    }


}
