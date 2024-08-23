using FluentValidation;

using OrchidFarmed.ProjectManagement.Application.Contracts.Commands;

namespace OrchidFarmed.ProjectManagement.Application.Validations;

public class DeleteProjectCommandValidator : AbstractValidator<DeleteProjectCommand>
{
    public DeleteProjectCommandValidator()
    {
        RuleFor(x => x.ProjectId)
            .Must(x => x.ToString().ToLower() != Guid.Empty.ToString())
            .WithMessage("The parameter ProjectId must NOT be null or empty or default.");
    }
}
