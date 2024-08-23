using MediatR;

namespace OrchidFarmed.ProjectManagement.Application.Contracts.Commands;

public record CreateProjectCommand : IRequest<ProjectDto>
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public CreateProjectCommand(Guid userId, string name, string description)
    {
        UserId = userId;
        Name = name;
        Description = description;
    }
}