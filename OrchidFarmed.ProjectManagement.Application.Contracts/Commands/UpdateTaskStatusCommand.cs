using MediatR;

using TaskStatus = OrchidFarmed.ProjectManagement.Domain.Shared.TaskStatus;

namespace OrchidFarmed.ProjectManagement.Application.Contracts.Commands;

public record UpdateTaskStatusCommand : IRequest<TaskDto>
{
    public Guid UserId { get; set; }
    public Guid ProjectId { get; set; }
    public Guid TaskId { get; set; }
    public TaskStatus Status { get; set; }

    public UpdateTaskStatusCommand(Guid userId, Guid projectId, Guid taskId, TaskStatus status)
    {
        UserId = userId;
        ProjectId = projectId;
        TaskId = taskId;
        Status = status;
    }
}