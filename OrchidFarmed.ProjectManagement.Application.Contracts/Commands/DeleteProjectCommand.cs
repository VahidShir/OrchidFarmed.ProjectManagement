using MediatR;

namespace OrchidFarmed.ProjectManagement.Application.Contracts.Commands;

public record DeleteProjectCommand : IRequest
{
    public Guid UserId { get; set; }
    public Guid ProjectId { get; set; }

    public DeleteProjectCommand(Guid userId, Guid projectId)
    {
        UserId = userId;
        ProjectId = projectId;
    }
}