using MediatR;

namespace OrchidFarmed.ProjectManagement.Application.Contracts.Commands;

public record CreateProjectCommand : IRequest<ProjectDto>
{
    public string Name { get; set; }
    public string Description { get; set; }

    public CreateProjectCommand(string name, string description)
    {
        Name = name;
        Description = description;
    }
}