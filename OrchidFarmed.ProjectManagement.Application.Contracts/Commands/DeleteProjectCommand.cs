using MediatR;

namespace OrchidFarmed.ProjectManagement.Application.Contracts.Commands;

public record DeleteProjectCommand : IRequest
{
    public Guid ProjectId { get; set; }

    public DeleteProjectCommand(Guid projectId)
    {
        ProjectId = projectId;
    }
}