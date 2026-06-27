using BookStore.Domain.Entities;
using BookStore.Domain.Exceptions;
using FluentAssertions;
using Xunit;

namespace BookStore.Application.Tests.Domain;

public class BookTests
{
	[Fact]
	public void Constructor_Should_Create_Book_When_Data_Is_Valid()
	{
		// Arrange & Act
		var book = new Book(
		"Clean Architecture",
		"A great software architecture book.",
		250,
		1,
		1,
		1);


    // Assert
    book.Title.Should().Be("Clean Architecture");
		book.Description.Should().Be("A great software architecture book.");
		book.Price.Should().Be(250);
		book.AuthorId.Should().Be(1);
		book.CategoryId.Should().Be(1);
		book.PublishingHouseId.Should().Be(1);
	}

	[Fact]
	public void Constructor_Should_Throw_DomainException_When_Title_Is_Empty()
	{
		// Act
		Action action = () => new Book(
			"",
			"Description",
			100,
			1,
			1,
			1);

		// Assert
		action.Should()
			  .Throw<DomainException>()
			  .WithMessage("Book title is required.");
	}

	[Fact]
	public void Constructor_Should_Throw_DomainException_When_Price_Is_Less_Than_Or_Equal_To_Zero()
	{
		// Act
		Action action = () => new Book(
			"Book",
			"Description",
			0,
			1,
			1,
			1);

		// Assert
		action.Should()
			  .Throw<DomainException>()
			  .WithMessage("Book price must be greater than zero.");
	}

	[Fact]
	public void UpdatePrice_Should_Update_Price_When_Value_Is_Valid()
	{
		// Arrange
		var book = new Book(
			"Book",
			"Description",
			100,
			1,
			1,
			1);

		// Act
		book.UpdatePrice(300);

		// Assert
		book.Price.Should().Be(300);
	}

	[Fact]
	public void UpdatePrice_Should_Throw_DomainException_When_Value_Is_Invalid()
	{
		// Arrange
		var book = new Book(
			"Book",
			"Description",
			100,
			1,
			1,
			1);

		// Act
		Action action = () => book.UpdatePrice(-50);

		// Assert
		action.Should()
			  .Throw<DomainException>()
			  .WithMessage("Book price must be greater than zero.");
	}

	[Fact]
	public void Update_Should_Update_All_Properties_When_Data_Is_Valid()
	{
		// Arrange
		var book = new Book(
			"Old Title",
			"Old Description",
			100,
			1,
			1,
			1);

		// Act
		book.Update(
			"New Title",
			"New Description",
			500,
			2,
			3,
			4);

		// Assert
		book.Title.Should().Be("New Title");
		book.Description.Should().Be("New Description");
		book.Price.Should().Be(500);
		book.AuthorId.Should().Be(2);
		book.CategoryId.Should().Be(3);
		book.PublishingHouseId.Should().Be(4);
	}


}
