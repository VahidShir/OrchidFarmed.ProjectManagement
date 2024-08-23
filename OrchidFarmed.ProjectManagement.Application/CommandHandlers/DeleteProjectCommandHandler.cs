using FluentValidation;

using MediatR;

using OrchidFarmed.ProjectManagement.Application.Contracts.Commands;
using OrchidFarmed.ProjectManagement.Domain.Repositories;

namespace OrchidFarmed.ProjectManagement.Application.Commands;

public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IValidator<DeleteProjectCommand> _validator;

    public DeleteProjectCommandHandler(IProjectRepository projectRepository, IValidator<DeleteProjectCommand> validator)
    {
        _projectRepository = projectRepository;
        _validator = validator;
    }

    public async Task Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(request);

        await _projectRepository.DeleteAsync(request.ProjectId);

        await _projectRepository.SaveChangesAsync();
    }
}