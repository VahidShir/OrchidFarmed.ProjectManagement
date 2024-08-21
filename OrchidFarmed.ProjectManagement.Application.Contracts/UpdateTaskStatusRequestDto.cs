using TaskStatus = OrchidFarmed.ProjectManagement.Domain.Shared.TaskStatus;

namespace OrchidFarmed.ProjectManagement.Application.Contracts;

public record UpdateTaskStatusRequestDto
{
    public TaskStatus Status { get; set; }
}