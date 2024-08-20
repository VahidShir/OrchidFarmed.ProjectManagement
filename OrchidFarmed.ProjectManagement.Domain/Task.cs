using OrchidFarmed.ProjectManagement.Domain.Shared.Exceptions;
using OrchidFarmed.ProjectManagement.Domain.Shared.Extensions;

using TaskStatus = OrchidFarmed.ProjectManagement.Domain.Shared.TaskStatus;

namespace OrchidFarmed.ProjectManagement.Domain;

public class Task
{
    //business rule
    private TimeSpan _minimumDueDateSpan = TimeSpan.FromMinutes(15);

    public Guid Id { get; private set; }
    public Guid ProjectId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public DateTime? DueDate { get; private set; }
    public TaskStatus Status { get; private set; }

    private Task() { }

    public Task(Guid id, string name, string description, DateTime dueDate)
    {
        Validate(id, name, description, dueDate);
        Id = id;
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

    private void Validate(Guid id, string name, string description, DateTime dueDate)
    {
        if (id == Guid.Empty)
            throw new BusinessException("id cannot be empty or default");

        if (name.IsNullOrEmptyWhiteSpace())
            throw new BusinessException($"The parameter {nameof(name)} cannot be empty or null");

        if (description.IsNullOrEmptyWhiteSpace())
            throw new ArgumentException($"The parameter {nameof(description)} cannot be empty or null");

        if (dueDate < DateTime.UtcNow.Add(_minimumDueDateSpan))
            throw new BusinessException("The task due date must be at least 15 minutes later.");
    }
}