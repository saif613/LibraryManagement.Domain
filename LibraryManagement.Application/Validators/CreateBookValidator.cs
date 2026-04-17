using FluentValidation;
using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Application.Interfaces.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Application.Validators
{
    public class CreateBookValidator : AbstractValidator<CreateBookRequest>
    {

        public CreateBookValidator()
        {
            
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Book title required")
                .MaximumLength(100).WithMessage("the title can`t be Bigger than 100");

            RuleFor(x => x.Author)
                .NotEmpty().WithMessage("Author name required")
                .MaximumLength(100).WithMessage("the author name can`t be Bigger than 100");

            RuleFor(x => x.ISBN)
                 .NotEmpty().WithMessage("ISBN is required.")
                 .Length(13).WithMessage("ISBN must be exactly 13 characters long.")
                 .Matches(@"^\d{13}$").WithMessage("ISBN must contain numbers only.");


            RuleFor(x => x.Url)
                  .NotEmpty().WithMessage("Book URL is required.")
                  .MaximumLength(200).WithMessage("The URL cannot exceed 200 characters.")
                  .Must(url => string.IsNullOrEmpty(url) || Uri.IsWellFormedUriString(url, UriKind.Absolute))
                  .WithMessage("The URL provided is not a valid absolute URL (e.g., https://example.com).");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("A valid Category ID must be selected.");
        }
    }
}
