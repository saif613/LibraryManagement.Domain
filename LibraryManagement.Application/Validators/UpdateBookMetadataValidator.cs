using FluentValidation;
using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Application.Interfaces.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Application.Validators
{
    public class UpdateBookMetadataValidator : AbstractValidator<UpdateBookMetadataRequest>
    {
        public UpdateBookMetadataValidator()
        {

            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Invalid Book ID.");

            RuleFor(x => x.ISBN)
                .Length(13).WithMessage("ISBN must be exactly 13 characters long.")
                .Matches(@"^\d{13}$").WithMessage("ISBN must contain numbers only.")
                .When(x => !string.IsNullOrEmpty(x.ISBN));

            RuleFor(x => x.Url)
                .MaximumLength(200).WithMessage("The URL cannot exceed 200 characters.")
                .Must(url => string.IsNullOrEmpty(url) || Uri.IsWellFormedUriString(url, UriKind.Absolute))
                .WithMessage("The URL provided is not a valid absolute URL.")
                .When(x => !string.IsNullOrEmpty(x.Url));
        }
    }
}
