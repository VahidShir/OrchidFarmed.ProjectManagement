using MediatR;

namespace OrchidFarmed.ProjectManagement.Application.Contracts.Commands;

public record DeleteTaskCommand : IRequest
{
    public Guid ProjectId { get; set; }
    public Guid TaskId { get; set; }

    public DeleteTaskCommand(Guid projectId, Guid taskId)
    {
        ProjectId = projectId;
        TaskId = taskId;
    }
}