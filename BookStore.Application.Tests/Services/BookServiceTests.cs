using BookStore.Application.DTOs.Book;
using BookStore.Application.Interfaces;
using BookStore.Application.Services;
using BookStore.Application.Specifications;
using BookStore.Domain.Entities;
using FluentAssertions;
using Moq;
using Xunit;

namespace BookStore.Application.Tests.Services;

public class BookServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IGenericRepository<Book>> _repositoryMock;
    private readonly BookService _bookService;


public BookServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _repositoryMock = new Mock<IGenericRepository<Book>>();

        _unitOfWorkMock
            .Setup(x => x.Repository<Book>())
            .Returns(_repositoryMock.Object);

        _bookService = new BookService(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task GetAllBooksAsync_Should_Return_All_Books()
    {
        // Arrange
        var books = new List<Book>
    {
        new Book(
            "Clean Architecture",
            "Description 1",
            200,
            1,
            1,
            1),

        new Book(
            "Clean Code",
            "Description 2",
            150,
            1,
            1,
            1)
    };

        _repositoryMock
            .Setup(x => x.GetAllWithSpecAsync(It.IsAny<ISpecification<Book>>()))
            .ReturnsAsync(books);

        // Act
        var result = await _bookService.GetAllBooksAsync();

        // Assert
        result.Should().HaveCount(2);

        result.First().Title.Should().Be("Clean Architecture");

        _repositoryMock.Verify(
            x => x.GetAllWithSpecAsync(It.IsAny<ISpecification<Book>>()),
            Times.Once);
    }

    [Fact]
    public async Task GetBookByIdAsync_Should_Return_Book_When_Book_Exists()
    {
        // Arrange
        var book = new Book(
            "DDD",
            "Domain Driven Design",
            300,
            1,
            1,
            1);

        _repositoryMock
            .Setup(x => x.GetWithSpecAsync(It.IsAny<ISpecification<Book>>()))
            .ReturnsAsync(book);

        // Act
        var result = await _bookService.GetBookByIdAsync(1);

        // Assert
        result.Should().NotBeNull();

        result!.Title.Should().Be("DDD");

        result.Price.Should().Be(300);

        _repositoryMock.Verify(
            x => x.GetWithSpecAsync(It.IsAny<ISpecification<Book>>()),
            Times.Once);
    }

    [Fact]
    public async Task GetBookByIdAsync_Should_Return_Null_When_Book_Does_Not_Exist()
    {
        // Arrange
        _repositoryMock
            .Setup(x => x.GetWithSpecAsync(It.IsAny<ISpecification<Book>>()))
            .ReturnsAsync((Book?)null);

        // Act
        var result = await _bookService.GetBookByIdAsync(99);

        // Assert
        result.Should().BeNull();

        _repositoryMock.Verify(
            x => x.GetWithSpecAsync(It.IsAny<ISpecification<Book>>()),
            Times.Once);
    }

   
[Fact]
    public async Task CreateBookAsync_Should_Add_New_Book()
    {
        // Arrange
        var model = new CreateBook
        {
            Title = "New Book",
            Description = "Description",
            Price = 250,
            AuthorId = 1,
            CategoryId = 1,
            PublishingHouseId = 1
        };

        _repositoryMock
            .Setup(x => x.AddAsync(It.IsAny<Book>()))
            .Returns(Task.CompletedTask);

        _unitOfWorkMock
            .Setup(x => x.CompleteAsync())
            .ReturnsAsync(1);

        // Act
        var result = await _bookService.CreateBookAsync(model);

        // Assert
        result.Should().NotBeNull();
        result.Title.Should().Be("New Book");

        _repositoryMock.Verify(
            x => x.AddAsync(It.IsAny<Book>()),
            Times.Once);

        _unitOfWorkMock.Verify(
            x => x.CompleteAsync(),
            Times.Once);
    }

    [Fact]
    public async Task UpdateBookAsync_Should_Return_True_When_Book_Exists()
    {
        // Arrange
        var book = new Book(
            "Old Title",
            "Old Description",
            100,
            1,
            1,
            1);

        var model = new CreateBook
        {
            Title = "Updated Title",
            Description = "Updated Description",
            Price = 300,
            AuthorId = 2,
            CategoryId = 2,
            PublishingHouseId = 2
        };

        _repositoryMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(book);

        _unitOfWorkMock
            .Setup(x => x.CompleteAsync())
            .ReturnsAsync(1);

        // Act
        var result = await _bookService.UpdateBookAsync(1, model);

        // Assert
        result.Should().BeTrue();

        book.Title.Should().Be("Updated Title");
        book.Price.Should().Be(300);

        _repositoryMock.Verify(
            x => x.Update(It.IsAny<Book>()),
            Times.Once);

        _unitOfWorkMock.Verify(
            x => x.CompleteAsync(),
            Times.Once);
    }

    [Fact]
    public async Task UpdateBookAsync_Should_Return_False_When_Book_Does_Not_Exist()
    {
        // Arrange
        var model = new CreateBook
        {
            Title = "Book",
            Description = "Description",
            Price = 100,
            AuthorId = 1,
            CategoryId = 1,
            PublishingHouseId = 1
        };

        _repositoryMock
            .Setup(x => x.GetByIdAsync(100))
            .ReturnsAsync((Book?)null);

        // Act
        var result = await _bookService.UpdateBookAsync(100, model);

        // Assert
        result.Should().BeFalse();

        _repositoryMock.Verify(
            x => x.Update(It.IsAny<Book>()),
            Times.Never);

        _unitOfWorkMock.Verify(
            x => x.CompleteAsync(),
            Times.Never);
    }

    [Fact]
    public async Task DeleteBookAsync_Should_Return_True_When_Book_Exists()
    {
        // Arrange
        var book = new Book(
            "Book",
            "Description",
            100,
            1,
            1,
            1);

        _repositoryMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(book);

        _unitOfWorkMock
            .Setup(x => x.CompleteAsync())
            .ReturnsAsync(1);

        // Act
        var result = await _bookService.DeleteBookAsync(1);

        // Assert
        result.Should().BeTrue();

        _repositoryMock.Verify(
            x => x.Delete(It.IsAny<Book>()),
            Times.Once);

        _unitOfWorkMock.Verify(
            x => x.CompleteAsync(),
            Times.Once);
    }

    [Fact]
    public async Task DeleteBookAsync_Should_Return_False_When_Book_Does_Not_Exist()
    {
        // Arrange
        _repositoryMock
            .Setup(x => x.GetByIdAsync(500))
            .ReturnsAsync((Book?)null);

        // Act
        var result = await _bookService.DeleteBookAsync(500);

        // Assert
        result.Should().BeFalse();

        _repositoryMock.Verify(
            x => x.Delete(It.IsAny<Book>()),
            Times.Never);

        _unitOfWorkMock.Verify(
            x => x.CompleteAsync(),
            Times.Never);
    }


}
