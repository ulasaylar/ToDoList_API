using Api.DTOs;
using FluentValidation;

namespace Api.Validation;

public class TaskValidator : AbstractValidator<CreateTaskRequest>
{
    public TaskValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Priority).InclusiveBetween(1, 3).WithMessage("Priority must be between 1 and 3.");
        RuleFor(x => x.ExpireDate).GreaterThan(DateTime.Now).WithMessage("Expire date must be in the future.");
    }
}
