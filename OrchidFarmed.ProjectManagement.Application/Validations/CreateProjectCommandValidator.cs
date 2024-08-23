using FluentValidation;

using OrchidFarmed.ProjectManagement.Application.Contracts.Commands;

namespace OrchidFarmed.ProjectManagement.Application.Validations;

public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("The parameter Name must be NOT null or empty.")
            .MaximumLength(200).WithMessage($"The length of the parameter Name must be maximum 200.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("The parameter Description must be NOT null or empty.")
            .MaximumLength(200).WithMessage($"The length of the parameter Description must be maximum 500.");
    }
}
