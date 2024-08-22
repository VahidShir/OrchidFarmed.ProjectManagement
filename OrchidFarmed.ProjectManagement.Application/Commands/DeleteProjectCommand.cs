using MediatR;

using OrchidFarmed.ProjectManagement.Domain.Repositories;

namespace OrchidFarmed.ProjectManagement.Application.Commands;

public record DeleteProjectCommand : IRequest
{
    public Guid ProjectId { get; set; }

    public DeleteProjectCommand(Guid projectId)
    {
        ProjectId = projectId;
    }
}

public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand>
{
    private readonly IProjectRepository _projectRepository;

    public DeleteProjectCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        await _projectRepository.DeleteAsync(request.ProjectId);

        await _projectRepository.SaveChangesAsync();
    }
}