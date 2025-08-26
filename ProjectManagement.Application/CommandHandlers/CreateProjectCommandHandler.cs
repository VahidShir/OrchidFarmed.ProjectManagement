using FluentValidation;

using MediatR;

using ProjectManagement.Application.Contracts;
using ProjectManagement.Application.Contracts.Commands;
using ProjectManagement.Domain;
using ProjectManagement.Domain.Repositories;

namespace ProjectManagement.Application.Commands;


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

        var project = new Project(id: Guid.NewGuid(), userId:request.UserId, request.Name, request.Description);

        await _projectRepository.AddAsync(project);

        await _projectRepository.SaveChangesAsync();

        return new ProjectDto
        {
            UserId = request.UserId,
            Id = project.Id,
            Name = request.Name,
            Description = request.Description
        };
    }
}