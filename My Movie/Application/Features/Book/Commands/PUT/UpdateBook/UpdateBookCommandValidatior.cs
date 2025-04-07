using FluentValidation;
using My_Movie.Application.BookFeatures.Commands;

namespace My_Movie.Application.Features.Book.Commands.PUT.UpdateBook;

public class UpdateBookCommandValidatior : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidatior()
    {
        RuleFor(x => x.title).NotEmpty().WithMessage("Title is required.");

        RuleFor(x => x.isbn)
            .NotEmpty().WithMessage("isbn is required.")
            .MaximumLength(50).WithMessage("isbn cannot exceed 50 characters");

        RuleFor(x => x.pageCount)
            .NotEmpty().WithMessage("pageCount is required.")
            .GreaterThan(0).WithMessage("pageCount must be greater than 0.")
            .LessThanOrEqualTo(10000).WithMessage("pageCount must be less than or equal to 10,000.");
        
        RuleFor(x => x.Authors)
            .NotEmpty().WithMessage("Authors confirmation is required.")
            .Must(authors => authors != null && authors.All(a => !string.IsNullOrWhiteSpace(a)))
            .WithMessage("Each author name must be non-empty and not just whitespace.")
            .Must(authors => authors.Count > 0)
            .WithMessage("At least one author is required.");
            
        RuleForEach(x => x.Authors)
            .MinimumLength(2).WithMessage("Author name must be at least 2 characters long.");
    }
}