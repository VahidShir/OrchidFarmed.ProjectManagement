﻿using MediatR;

using OrchidFarmed.ProjectManagement.Application.Contracts;
using OrchidFarmed.ProjectManagement.Application.Contracts.Queries;
using OrchidFarmed.ProjectManagement.Domain;
using OrchidFarmed.ProjectManagement.Domain.Repositories;

namespace OrchidFarmed.ProjectManagement.Application.Queries;

public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, IEnumerable<ProjectDto>>
{
    private readonly IProjectRepository _projectRepository;

    public GetAllProjectsQueryHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<IEnumerable<ProjectDto>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Project> projectEntities = await _projectRepository.GetListAsync(p => p.UserId == request.UserId);

        if (projectEntities == null)
            return Enumerable.Empty<ProjectDto>();

        return projectEntities.Select(p => new ProjectDto
        {
            UserId = request.UserId,
            Id = p.Id,
            Name = p.Name,
            Description = p.Descroption,
            Tasks = p.Tasks.Select(t => new TaskDto
            {
                UserId = request.UserId,
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
