using MediatR;

using OrchidFarmed.ProjectManagement.Application.Contracts;
using OrchidFarmed.ProjectManagement.Domain;
using OrchidFarmed.ProjectManagement.Domain.Repositories;

namespace OrchidFarmed.ProjectManagement.Application.Commands;

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

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, ProjectDto>
{
    private readonly IProjectRepository _projectRepository;

    public CreateProjectCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ProjectDto> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        //validation
        var project = new Project(Guid.NewGuid(), request.Name, request.Description);

        await _projectRepository.AddAsync(project);

        await _projectRepository.SaveChangesAsync();

        return new ProjectDto
        {
            Id = project.Id,
            Name = request.Name,
            Description = request.Description
        };
    }
}