using MediatR;

using OrchidFarmed.ProjectManagement.Application.Contracts;
using OrchidFarmed.ProjectManagement.Application.Contracts.Queries;
using OrchidFarmed.ProjectManagement.Domain;
using OrchidFarmed.ProjectManagement.Domain.Repositories;
using OrchidFarmed.ProjectManagement.Domain.Shared.Exceptions;

namespace OrchidFarmed.ProjectManagement.Application.Queries;

public class GetTaskQueryHandler : IRequestHandler<GetTaskQuery, TaskDto>
{
    private readonly IProjectRepository _projectRepository;

    public GetTaskQueryHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<TaskDto> Handle(GetTaskQuery request, CancellationToken cancellationToken)
    {
        var task = await _projectRepository.GetTaskAsync(request.TaskId);

        if (task == null)
            return null;

        if (request.ProjectId != task.ProjectId)
            throw new BusinessException("The task doesn't belong to the project.");

        if (task.UserId != request.UserId)
            throw new ForbiddenOperationException();

        return new TaskDto()
        {
            UserId = request.UserId,
            ProjectId = task.ProjectId,
            Id = task.Id,
            Name = task.Name,
            Description = task.Description,
            DueDate = task.DueDate,
            Status = task.Status
        };
    }
}
