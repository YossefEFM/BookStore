namespace BookStore.Application.DTOs.Book;

public class BookDto
{
    public int Id { get; set; }


public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int AuthorId { get; set; }

    public string AuthorName { get; set; } = string.Empty;

    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = string.Empty;

    public int PublishingHouseId { get; set; }

    public string PublishingHouseName { get; set; } = string.Empty;

}
