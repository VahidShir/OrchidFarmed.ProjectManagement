using MediatR;

using OrchidFarmed.ProjectManagement.Application.Contracts.Commands;
using OrchidFarmed.ProjectManagement.Domain.Repositories;
using OrchidFarmed.ProjectManagement.Domain.Shared.Exceptions;

namespace OrchidFarmed.ProjectManagement.Application.Commands;

public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand>
{
    private readonly IProjectRepository _projectRepository;

    public DeleteTaskCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetAsync(request.ProjectId);

        if (project == null)
            throw new ProjectNotFoundException();

        project.DeleteTask(request.TaskId);        

        await _projectRepository.UpdateAsync(project);

        await _projectRepository.SaveChangesAsync();
    }
}