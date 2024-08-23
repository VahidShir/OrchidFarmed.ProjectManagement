using FluentValidation;

using MediatR;

using OrchidFarmed.ProjectManagement.Application.Contracts.Commands;
using OrchidFarmed.ProjectManagement.Domain.Repositories;
using OrchidFarmed.ProjectManagement.Domain.Shared.Exceptions;

namespace OrchidFarmed.ProjectManagement.Application.Commands;

public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IValidator<DeleteTaskCommand> _validator;

    public DeleteTaskCommandHandler(IProjectRepository projectRepository, IValidator<DeleteTaskCommand> validator)
    {
        _projectRepository = projectRepository;
        _validator = validator;
    }

    public async Task Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(request);

        var project = await _projectRepository.GetAsync(request.ProjectId);

        if (project == null)
            throw new ProjectNotFoundException();

        if (project.UserId != request.UserId)
            throw new ForbiddenOperationException();

        var task = project.Tasks.SingleOrDefault(t => t.Id == request.TaskId);

        if (task == null)
            throw new TaskNotFoundException();

        project.DeleteTask(request.TaskId);

        await _projectRepository.UpdateAsync(project);

        await _projectRepository.SaveChangesAsync();
    }
}