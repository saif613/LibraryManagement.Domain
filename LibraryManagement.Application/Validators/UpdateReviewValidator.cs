using FluentValidation;
using LibraryManagement.Application.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Application.Validators
{
    public class UpdateReviewValidator : AbstractValidator<UpdateReviewRequest>
    {
        public UpdateReviewValidator()
        {
            RuleFor(x => x.Rating)
             .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5 stars.");

            RuleFor(x => x.Comment)
                .MaximumLength(500).WithMessage("Comment cannot exceed 500 characters.")
                .Must(c => string.IsNullOrEmpty(c) || c.Trim().Length > 0)
                .WithMessage("Comment cannot be empty whitespace.");
        }
    }
}
