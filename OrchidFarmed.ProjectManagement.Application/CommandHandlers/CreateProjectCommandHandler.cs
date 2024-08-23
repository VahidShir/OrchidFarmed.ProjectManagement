using FluentValidation;

using MediatR;

using OrchidFarmed.ProjectManagement.Application.Contracts;
using OrchidFarmed.ProjectManagement.Application.Contracts.Commands;
using OrchidFarmed.ProjectManagement.Domain;
using OrchidFarmed.ProjectManagement.Domain.Repositories;

namespace OrchidFarmed.ProjectManagement.Application.Commands;


public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, ProjectDto>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IValidator<CreateProjectCommand> _validator;

    public CreateProjectCommandHandler(IProjectRepository projectRepository, IValidator<CreateProjectCommand> validator)
    {
        _projectRepository = projectRepository;
        _validator = validator;
    }

    public async Task<ProjectDto> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(request);

        var project = new Project(Guid.NewGuid(), request.Name, request.Description);

        await _projectRepository.AddAsync(project);

        await _projectRepository.SaveChangesAsync();

        return new ProjectDto
        {
            Id = project.Id,
            Name = request.Name,
            Description = request.Description
        };
    }
}