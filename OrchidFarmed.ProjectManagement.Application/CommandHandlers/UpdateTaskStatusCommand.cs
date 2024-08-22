using MediatR;

using OrchidFarmed.ProjectManagement.Application.Contracts;
using OrchidFarmed.ProjectManagement.Application.Contracts.Commands;
using OrchidFarmed.ProjectManagement.Domain.Repositories;
using OrchidFarmed.ProjectManagement.Domain.Shared.Exceptions;

namespace OrchidFarmed.ProjectManagement.Application.Commands;

public class UpdateTaskStatusCommandHandler : IRequestHandler<UpdateTaskStatusCommand, TaskDto>
{
    private readonly IProjectRepository _projectRepository;

    public UpdateTaskStatusCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<TaskDto> Handle(UpdateTaskStatusCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetAsync(request.ProjectId);

        if (project == null)
            throw new ProjectNotFoundException();

        var task = project.Tasks.SingleOrDefault(t => t.Id == request.TaskId);

        if (task == null)
            throw new TaskNotFoundException();

        task.UpdateStatus(request.Status);

        await _projectRepository.UpdateAsync(project);

        await _projectRepository.SaveChangesAsync();

        return new TaskDto
        {
            ProjectId = task.ProjectId,
            Id = task.Id,
            Name = task.Name,
            Description = task.Description,
            Status = task.Status,
            DueDate = task.DueDate
        };
    }
}