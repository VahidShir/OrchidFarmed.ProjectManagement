using MediatR;

using TaskStatus = OrchidFarmed.ProjectManagement.Domain.Shared.TaskStatus;

namespace OrchidFarmed.ProjectManagement.Application.Contracts.Commands;

public record UpdateTaskStatusCommand : IRequest<TaskDto>
{
    public Guid ProjectId { get; set; }
    public Guid TaskId { get; set; }
    public TaskStatus Status { get; set; }

    public UpdateTaskStatusCommand(Guid projectId, Guid taskId, TaskStatus status)
    {
        ProjectId = projectId;
        TaskId = taskId;
        Status = status;
    }
}