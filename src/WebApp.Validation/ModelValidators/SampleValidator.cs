using FluentValidation;
using WebApp.Model.Entities;

namespace WebApp.Models
{
    public class SampleValidator : AbstractValidator<Sample>
    {
        public SampleValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title field cannot be empty!")
                .MinimumLength(3)
                .WithMessage("Title field must be more than 2 characters!")
                .MaximumLength(100)
                .WithMessage("Title field is too long. Maximum charachters should be 100!");
        }
    }
}