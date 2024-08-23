using FluentValidation;

using MediatR;

using OrchidFarmed.ProjectManagement.Application.Contracts;
using OrchidFarmed.ProjectManagement.Application.Contracts.Commands;
using OrchidFarmed.ProjectManagement.Domain.Repositories;
using OrchidFarmed.ProjectManagement.Domain.Shared.Exceptions;

namespace OrchidFarmed.ProjectManagement.Application.Commands;

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, TaskDto>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IValidator<CreateTaskCommand> _validator;

    public CreateTaskCommandHandler(IProjectRepository projectRepository, IValidator<CreateTaskCommand> validator)
    {
        _projectRepository = projectRepository;
        _validator = validator;
    }

    public async Task<TaskDto> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(request);

        var project = await _projectRepository.GetAsync(request.ProjectId);

        if (project == null)
            throw new ProjectNotFoundException();

        if (project.UserId != request.UserId)
            throw new ForbiddenOperationException();

        var task = new Domain.Task(request.UserId, project.Id, Guid.NewGuid(), request.Name, request.Description, request.DueDate);

        project.AddTask(task);

        await _projectRepository.UpdateAsync(project);

        await _projectRepository.SaveChangesAsync();

        return new TaskDto
        {
            UserId = request.UserId,
            ProjectId = project.Id,
            Id = task.Id,
            Name = request.Name,
            Description = request.Description,
            DueDate = request.DueDate,
            Status = task.Status
        };
    }
}