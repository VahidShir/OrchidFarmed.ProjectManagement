using MediatR;

namespace OrchidFarmed.ProjectManagement.Application.Contracts.Commands;

public record DeleteTaskCommand : IRequest
{
    public Guid UserId { get; set; }
    public Guid ProjectId { get; set; }
    public Guid TaskId { get; set; }

    public DeleteTaskCommand(Guid userId, Guid projectId, Guid taskId)
    {
        UserId = userId;
        ProjectId = projectId;
        TaskId = taskId;
    }
}