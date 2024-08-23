using MediatR;

namespace OrchidFarmed.ProjectManagement.Application.Contracts.Queries;

public record GetProjectQuery : IRequest<ProjectDto>
{
    public Guid UserId { get; set; }
    public Guid ProjectId { get; set; }

    public GetProjectQuery(Guid userId, Guid projectId)
    {
        UserId = userId;
        ProjectId = projectId;
    }
}