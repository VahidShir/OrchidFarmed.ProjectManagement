using MediatR;

using OrchidFarmed.ProjectManagement.Application.Contracts;
using OrchidFarmed.ProjectManagement.Domain.Repositories;
using OrchidFarmed.ProjectManagement.Domain.Shared.Exceptions;

namespace OrchidFarmed.ProjectManagement.Application.Queries;

public record GetTaskQuery : IRequest<TaskDto>
{
    public Guid ProjectId { get; set; }
    public Guid TaskId { get; set; }

    public GetTaskQuery(Guid projectId, Guid taskId)
    {
        ProjectId = projectId;
        TaskId = taskId;
    }
}

public class GetTaskQueryHandler : IRequestHandler<GetTaskQuery, TaskDto>
{
    private readonly IProjectRepository _projectRepository;

    public GetTaskQueryHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<TaskDto> Handle(GetTaskQuery request, CancellationToken cancellationToken)
    {
        var taskEntity = await _projectRepository.GetTaskAsync(request.TaskId);

        if (taskEntity == null)
            return null;

        if (request.ProjectId != taskEntity.ProjectId)
            throw new BusinessException("The task doesn't belong to the project.");

        return new TaskDto()
        {
            ProjectId = taskEntity.ProjectId,
            Id = taskEntity.Id,
            Name = taskEntity.Name,
            Description = taskEntity.Description,
            DueDate = taskEntity.DueDate,
            Status = taskEntity.Status
        };
    }
}
