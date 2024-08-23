using MediatR;

namespace OrchidFarmed.ProjectManagement.Application.Contracts.Queries;

public record GetAllProjectsQuery : IRequest<IEnumerable<ProjectDto>>
{
    public Guid UserId { get; set; }
    public GetAllProjectsQuery(Guid userId)
    {
        UserId = userId;
    }
}