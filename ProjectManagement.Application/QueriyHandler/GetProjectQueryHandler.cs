using MediatR;

using ProjectManagement.Application.Contracts;
using ProjectManagement.Application.Contracts.Queries;
using ProjectManagement.Domain.Repositories;
using ProjectManagement.Domain.Shared.Exceptions;

namespace ProjectManagement.Application.Queries;

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
