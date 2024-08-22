using MediatR;

namespace OrchidFarmed.ProjectManagement.Application.Contracts.Queries;

public record GetProjectQuery : IRequest<ProjectDto>
{
    public Guid ProjectId { get; set; }

    public GetProjectQuery(Guid projectId)
    {
        ProjectId = projectId;
    }
}
