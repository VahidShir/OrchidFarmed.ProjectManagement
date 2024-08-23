using MediatR;

using OrchidFarmed.ProjectManagement.Application.Contracts;
using OrchidFarmed.ProjectManagement.Application.Contracts.Queries;
using OrchidFarmed.ProjectManagement.Domain.Repositories;
using OrchidFarmed.ProjectManagement.Domain.Shared.Exceptions;

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
        var project = await _projectRepository.GetAsync(request.ProjectId);

        if (project == null)
            return null;

        if (project.UserId != request.UserId)
            throw new ForbiddenOperationException();

        return new ProjectDto()
        {
            UserId = request.UserId,
            Id = project.Id,
            Name = project.Name,
            Description = project.Descroption,
            Tasks = project.Tasks.Select(x => new TaskDto
            {
                UserId = request.UserId,
                ProjectId = project.Id,
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                DueDate = x.DueDate,
                Status = x.Status
            }).ToList()
        };
    }
}
