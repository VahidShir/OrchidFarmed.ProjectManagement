using MediatR;

namespace OrchidFarmed.ProjectManagement.Application.Contracts.Commands;

public record CreateTaskCommand : IRequest<TaskDto>
{
    public Guid UserId { get; set; }
    public Guid ProjectId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }

    public CreateTaskCommand(Guid userId, Guid projectId, string name, string description, DateTime dueTime)
    {
        UserId = userId;
        ProjectId = projectId;
        Name = name;
        Description = description;
        DueDate = dueTime;
    }
}