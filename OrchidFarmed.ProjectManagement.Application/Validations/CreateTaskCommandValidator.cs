using FluentValidation;

using OrchidFarmed.ProjectManagement.Application.Contracts.Commands;

namespace OrchidFarmed.ProjectManagement.Application.Validations;

public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("The parameter Name must be NOT null or empty.")
            .MaximumLength(200).WithMessage($"The length of the parameter Name must be maximum 200.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("The parameter Description must be NOT null or empty.")
            .MaximumLength(200).WithMessage($"The length of the parameter Description must be maximum 500.");

        RuleFor(x => x.ProjectId)
            .Must(x => x.ToString().ToLower() != Guid.Empty.ToString()).WithMessage("The parameter ProjectId must NOT be null or empty or default.");

        RuleFor(x => x.DueDate)
            .NotNull().WithMessage("The parameter DueDate must be NOT null or empty.")
            .Must(x => x >= DateTime.UtcNow.Add(TimeSpan.FromMinutes(15))).WithMessage("The parameter task DueDate must be at least 15 minutes later.");

        RuleFor(x => x.UserId)
            .Must(x => x.ToString().ToLower() != Guid.Empty.ToString()).WithMessage("The parameter UserId must NOT be null or empty or default.");
    }
}
