using FluentValidation;
using LibraryManagement.Application.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Application.Validators
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryRequest>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name required")
                .MaximumLength(50).WithMessage("the name can`t be Bigger than 50");

            RuleFor(x => x.Description)
                .MaximumLength(200).WithMessage("the Description can`t be Bigger than 200");
        }
    }
}
