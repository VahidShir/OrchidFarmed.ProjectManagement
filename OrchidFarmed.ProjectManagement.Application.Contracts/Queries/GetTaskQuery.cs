using MediatR;

namespace OrchidFarmed.ProjectManagement.Application.Contracts.Queries;

public record GetTaskQuery : IRequest<TaskDto>
{
    public Guid UserId { get; set; }
    public Guid ProjectId { get; set; }
    public Guid TaskId { get; set; }

    public GetTaskQuery(Guid userId, Guid projectId, Guid taskId)
    {
        UserId = userId;
        ProjectId = projectId;
        TaskId = taskId;
    }
}