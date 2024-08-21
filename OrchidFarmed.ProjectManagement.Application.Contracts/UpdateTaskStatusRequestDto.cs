namespace OrchidFarmed.ProjectManagement.Application.Contracts;

public record UpdateTaskStatusRequestDto
{
    public TaskStatus Status { get; set; }
}