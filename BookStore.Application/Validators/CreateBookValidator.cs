using BookStore.Application.DTOs.Book;
using FluentValidation;

namespace BookStore.Application.Validators;

public class CreateBookValidator : AbstractValidator<CreateBook>
{
    public CreateBookValidator()
    {
        RuleFor(x => x.Title)
        .NotEmpty()
        .MaximumLength(200);


    RuleFor(x => x.Description)
        .NotEmpty();

        RuleFor(x => x.Price)
            .GreaterThan(0);

        RuleFor(x => x.AuthorId)
            .GreaterThan(0);

        RuleFor(x => x.CategoryId)
            .GreaterThan(0);

        RuleFor(x => x.PublishingHouseId)
            .GreaterThan(0);
    }


}
