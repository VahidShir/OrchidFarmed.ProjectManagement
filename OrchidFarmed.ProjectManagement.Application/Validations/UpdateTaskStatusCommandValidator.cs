using FluentValidation;

using OrchidFarmed.ProjectManagement.Application.Contracts.Commands;

namespace OrchidFarmed.ProjectManagement.Application.Validations;

public class UpdateTaskStatusCommandValidator : AbstractValidator<UpdateTaskStatusCommand>
{
    public UpdateTaskStatusCommandValidator()
    {
        RuleFor(x => x.ProjectId)
            .Must(x => x.ToString().ToLower() != Guid.Empty.ToString())
            .WithMessage("The parameter ProjectId must NOT be null or empty or default.");

        RuleFor(x => x.TaskId)
            .Must(x => x.ToString().ToLower() != Guid.Empty.ToString())
            .WithMessage("The parameter TaskId must NOT be null or empty or default.");
    }
}