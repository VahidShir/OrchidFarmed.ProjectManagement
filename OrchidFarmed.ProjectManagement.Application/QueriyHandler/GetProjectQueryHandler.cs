using MediatR;

using OrchidFarmed.ProjectManagement.Application.Contracts;
using OrchidFarmed.ProjectManagement.Application.Contracts.Queries;
using OrchidFarmed.ProjectManagement.Domain.Repositories;

namespace OrchidFarmed.ProjectManagement.Application.Queries;

public class GetProjectQueryHandler : IRequestHandler<GetProjectQuery, ProjectDto>
{
    private readonly IProjectRepository _projectRepository;

    public GetProjectQueryHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ProjectDto> Handle(GetProjectQuery request, CancellationToken cancellationToken)
    {
        var projectEntity = await _projectRepository.GetAsync(request.ProjectId);

        if (projectEntity == null)
            return null;

        return new ProjectDto()
        {
            Id = projectEntity.Id,
            Name = projectEntity.Name,
            Description = projectEntity.Descroption,
            Tasks = projectEntity.Tasks.Select(x => new TaskDto
            {
                ProjectId = projectEntity.Id,
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                DueDate = x.DueDate,
                Status = x.Status
            }).ToList()
        };
    }
}
