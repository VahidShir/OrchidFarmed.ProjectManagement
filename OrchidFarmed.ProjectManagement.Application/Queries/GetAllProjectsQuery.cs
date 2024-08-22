using MediatR;

using OrchidFarmed.ProjectManagement.Application.Contracts;
using OrchidFarmed.ProjectManagement.Domain;
using OrchidFarmed.ProjectManagement.Domain.Repositories;

namespace OrchidFarmed.ProjectManagement.Application.Queries;

public record GetAllProjectsQuery : IRequest<IEnumerable<ProjectDto>>
{

}

public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, IEnumerable<ProjectDto>>
{
    private readonly IProjectRepository _projectRepository;

    public GetAllProjectsQueryHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<IEnumerable<ProjectDto>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Project> projectEntities = await _projectRepository.GetAllAsync();

        if (projectEntities == null)
            return Enumerable.Empty<ProjectDto>();

        return projectEntities.Select(p => new ProjectDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Descroption,
            Tasks = p.Tasks.Select(t => new TaskDto
            {
                ProjectId = p.Id,
                Id = t.Id,
                Name = t.Name,
                Description = t.Description,
                DueDate = t.DueDate,
                Status = t.Status
            }).ToList()
        }).ToList();
    }
}
