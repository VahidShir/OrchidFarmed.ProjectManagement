using OrchidFarmed.ProjectManagement.Domain.Shared.Exceptions;
using OrchidFarmed.ProjectManagement.Domain.Shared.Extensions;

using System;

using TaskStatus = OrchidFarmed.ProjectManagement.Domain.Shared.TaskStatus;

namespace OrchidFarmed.ProjectManagement.Domain;

public class Task
{
    //business rule
    private TimeSpan _minimumDueDateSpan = TimeSpan.FromMinutes(15);

    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid ProjectId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public DateTime? DueDate { get; private set; }
    public TaskStatus Status { get; private set; }

    private Task() { }

    public Task(Guid userId, Guid projectId, Guid id, string name, string description, DateTime dueDate)
    {
        Validate(userId, projectId, id, name, description, dueDate);

        ProjectId = projectId;
        Id = id;
        UserId = userId;
        Name = name;
        Description = description;
        DueDate = dueDate;
        Status = TaskStatus.ToDo;
    }

    public void SetAsInProgress()
    {
        Status = TaskStatus.InProgress;
    }

    public void SetAsDone()
    {
        Status = TaskStatus.Done;
    }

    public void SetAsToDo()
    {
        Status = TaskStatus.ToDo;
    }

    public void UpdateStatus(TaskStatus newStatus)
    {
        Status = newStatus;
    }

    private void Validate(Guid userId, Guid projectId, Guid id, string name, string description, DateTime dueDate)
    {
        if (id == Guid.Empty)
            throw new BusinessException("id cannot be empty or default");

        if (projectId == Guid.Empty)
            throw new BusinessException("projectId cannot be empty or default");

        if (userId == Guid.Empty)
            throw new BusinessException("userId cannot be empty or default");

        if (name.IsNullOrEmptyWhiteSpace())
            throw new BusinessException($"The parameter {nameof(name)} cannot be empty or null");

        if (description.IsNullOrEmptyWhiteSpace())
            throw new BusinessException($"The parameter {nameof(description)} cannot be empty or null");

        if (dueDate < DateTime.UtcNow.Add(_minimumDueDateSpan))
            throw new BusinessException("The task due date must be at least 15 minutes later.");
    }
}