using TaskStatus = ProjectManagement.Domain.Shared.TaskStatus;

namespace ProjectManagement.Application.Contracts;

public record UpdateTaskStatusRequestDto
{
    public TaskStatus Status { get; set; }
}