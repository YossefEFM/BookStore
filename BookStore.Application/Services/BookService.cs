using BookStore.Application.DTOs.Book;
using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;

namespace BookStore.Application.Services;

public class BookService : IBookService
{
    private readonly IUnitOfWork _unitOfWork;

public BookService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<BookDto>> GetAllBooksAsync()
    {
        var books = await _unitOfWork.Repository<Book>().GetAllAsync();

        return books.Select(MapToDto).ToList();
    }

    public async Task<BookDto?> GetBookByIdAsync(int id)
    {
        var book = await _unitOfWork.Repository<Book>().GetByIdAsync(id);

        return book is null ? null : MapToDto(book);
    }

    public async Task<BookDto> CreateBookAsync(CreateBook model)
    {
        var book = new Book(
            model.Title,
            model.Description,
            model.Price,
            model.AuthorId,
            model.CategoryId,
            model.PublishingHouseId);

        await _unitOfWork.Repository<Book>().AddAsync(book);
        await _unitOfWork.CompleteAsync();

        return MapToDto(book);
    }

    public async Task<bool> UpdateBookAsync(int id, CreateBook model)
    {
        var book = await _unitOfWork.Repository<Book>().GetByIdAsync(id);

        if (book is null)
        {
            return false;
        }

        book.Update(
            model.Title,
            model.Description,
            model.Price,
            model.AuthorId,
            model.CategoryId,
            model.PublishingHouseId);

        _unitOfWork.Repository<Book>().Update(book);
        await _unitOfWork.CompleteAsync();

        return true;
    }

    public async Task<bool> DeleteBookAsync(int id)
    {
        var book = await _unitOfWork.Repository<Book>().GetByIdAsync(id);

        if (book is null)
        {
            return false;
        }

        _unitOfWork.Repository<Book>().Delete(book);
        await _unitOfWork.CompleteAsync();

        return true;
    }

    private static BookDto MapToDto(Book book)
    {
        return new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Description = book.Description,
            Price = book.Price,
            AuthorId = book.AuthorId,
            CategoryId = book.CategoryId
        };
    }


}
