namespace OrchidFarmed.ProjectManagement.Application.Contracts;

public record ProjectDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public IList<TaskDto> Tasks { get; set; }
}