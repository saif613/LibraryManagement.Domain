using FluentValidation;
using LibraryManagement.Application.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Application.Validators
{
    public class BorrowBookValidator: AbstractValidator<BorrowRequest>
    {
        public BorrowBookValidator()
        {
            RuleFor(x => x.BookId)
                .GreaterThan(0).WithMessage("BookId must be greater than 0.")
                .NotEmpty().WithMessage("BookId Can not be null");
        }
    }
}
