using MediatR;

using OrchidFarmed.ProjectManagement.Application.Contracts;
using OrchidFarmed.ProjectManagement.Domain.Repositories;
using OrchidFarmed.ProjectManagement.Domain.Shared.Exceptions;

namespace OrchidFarmed.ProjectManagement.Application.Commands;

public record CreateTaskCommand : IRequest<TaskDto>
{
    public Guid ProjectId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }

    public CreateTaskCommand(Guid projectId, string name, string description, DateTime dueTime)
    {
        ProjectId = projectId;
        Name = name;
        Description = description;
        DueDate = dueTime;
    }
}

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, TaskDto>
{
    private readonly IProjectRepository _projectRepository;

    public CreateTaskCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<TaskDto> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetAsync(request.ProjectId);

        if (project == null)
            throw new ProjectNotFoundException();

        var task = new Domain.Task(project.Id, Guid.NewGuid(), request.Name, request.Description, request.DueDate);

        project.AddTask(task);

        await _projectRepository.UpdateAsync(project);

        await _projectRepository.SaveChangesAsync();

        return new TaskDto
        {
            ProjectId = project.Id,
            Id = task.Id,
            Name = request.Name,
            Description = request.Description,
            DueDate = request.DueDate,
            Status = task.Status
        };
    }
}