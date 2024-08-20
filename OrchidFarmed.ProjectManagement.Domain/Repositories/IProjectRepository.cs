using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OrchidFarmed.ProjectManagement.Domain.Repositories;

public interface IProjectRepository
{
    System.Threading.Tasks.Task AddAsync(Project project);
    System.Threading.Tasks.Task DeleteAsync(Guid projectId);
    System.Threading.Tasks.Task<Project> GetAsync(Guid projectId);
    System.Threading.Tasks.Task<Project> GetAsync(Expression<Func<Project, bool>> predicate);
    System.Threading.Tasks.Task<IEnumerable<Project>> GetListAsync(Expression<Func<Project, bool>> predicate);
    System.Threading.Tasks.Task<Project> UpdateAsync(Project project);
    System.Threading.Tasks.Task<bool> ExistsAsync(Expression<Func<Project, bool>> predicate);
    System.Threading.Tasks.Task SaveChangesAsync(Project project);
}