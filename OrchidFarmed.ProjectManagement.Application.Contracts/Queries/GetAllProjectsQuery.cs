using MediatR;

namespace OrchidFarmed.ProjectManagement.Application.Contracts.Queries;

public record GetAllProjectsQuery : IRequest<IEnumerable<ProjectDto>>
{

}