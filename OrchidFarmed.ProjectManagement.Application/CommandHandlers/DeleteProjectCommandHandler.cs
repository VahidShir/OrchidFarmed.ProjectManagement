using FluentValidation;

using MediatR;

using OrchidFarmed.ProjectManagement.Application.Contracts.Commands;
using OrchidFarmed.ProjectManagement.Domain.Repositories;
using OrchidFarmed.ProjectManagement.Domain.Shared.Exceptions;

namespace OrchidFarmed.ProjectManagement.Application.Commands;

public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IValidator<DeleteProjectCommand> _validator;

    public DeleteProjectCommandHandler(IProjectRepository projectRepository, IValidator<DeleteProjectCommand> validator)
    {
        _projectRepository = projectRepository;
        _validator = validator;
    }

    public async Task Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(request);

        var project = await _projectRepository.GetAsync(request.ProjectId);

        if (project == null)
            throw new ProjectNotFoundException();

        if (project.UserId != request.UserId)
            throw new ForbiddenOperationException();

        await _projectRepository.DeleteAsync(request.ProjectId);

        await _projectRepository.SaveChangesAsync();
    }
}