using BookStore.Application.DTOs.Book;

namespace BookStore.Application.Interfaces;

public interface IBookService
{
    Task<IReadOnlyList<BookDto>> GetAllBooksAsync();


Task<BookDto?> GetBookByIdAsync(int id);

    Task<BookDto> CreateBookAsync(CreateBook model);

    Task<bool> UpdateBookAsync(int id, CreateBook model);

    Task<bool> DeleteBookAsync(int id);


}
