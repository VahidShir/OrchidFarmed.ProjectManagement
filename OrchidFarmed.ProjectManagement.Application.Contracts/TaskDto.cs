using TaskStatus = OrchidFarmed.ProjectManagement.Domain.Shared.TaskStatus;

namespace OrchidFarmed.ProjectManagement.Application.Contracts;

public record TaskDto
{
    public Guid UserId { get; set; }
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime? DueDate { get; set; }
    public TaskStatus Status { get; set; }
}