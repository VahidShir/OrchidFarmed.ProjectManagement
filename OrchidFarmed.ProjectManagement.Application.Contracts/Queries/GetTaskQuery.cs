using MediatR;

namespace OrchidFarmed.ProjectManagement.Application.Contracts.Queries;

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