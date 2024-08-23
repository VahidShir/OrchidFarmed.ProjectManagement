using OrchidFarmed.ProjectManagement.Domain.Shared.Exceptions;
using OrchidFarmed.ProjectManagement.Domain.Shared.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;

namespace OrchidFarmed.ProjectManagement.Domain;

public class Project
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Name { get; private set; }
    public string Descroption { get; private set; }

    private readonly IList<Task> _tasks;
    public IReadOnlyCollection<Task> Tasks => _tasks.AsReadOnly();

    private Project() { }

    public Project(Guid id, Guid userId, string name, string description)
    {
        Validate(id, userId, name, description);

        Id = id;
        UserId = userId;
        Name = name;
        Descroption = description;
        _tasks = new List<Task>();
    }

    public void AddTask(Guid userId, Guid id, string name, string description, DateTime dueDate)
    {
        ThrowIfTaskIsDuplicate(id);

        var newTask = new Task(userId, this.Id, id, name, description, dueDate);

        _tasks.Add(newTask);
    }

    public void AddTask(Task task)
    {
        ThrowIfTaskIsDuplicate(task.Id);

        _tasks.Add(task);
    }

    public void DeleteTask(Guid taskId)
    {
        var task = _tasks.SingleOrDefault(t => t.Id == taskId);

        if (task == null)
            throw new TaskNotFoundException();

        _tasks.Remove(task);
    }

    private void ThrowIfTaskIsDuplicate(Guid id)
    {
        //business rule: unique only by id. Could be also by name.
        if (_tasks.Any(t => t.Id == id))
            throw new BusinessException("Another task with the same id already exists.");
    }

    private void Validate(Guid id, Guid userId, string name, string description)
    {
        if (id == Guid.Empty)
            throw new BusinessException("id cannot be empty or default");

        if (userId == Guid.Empty)
            throw new BusinessException("userId cannot be empty or default");

        if (name.IsNullOrEmptyWhiteSpace())
            throw new BusinessException($"The parameter {nameof(name)} cannot be empty or null");

        if (description.IsNullOrEmptyWhiteSpace())
            throw new BusinessException($"The parameter {nameof(description)} cannot be empty or null");
    }
}