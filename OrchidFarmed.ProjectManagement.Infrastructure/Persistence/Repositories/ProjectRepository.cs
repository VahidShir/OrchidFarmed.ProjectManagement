using Microsoft.EntityFrameworkCore;

using OrchidFarmed.ProjectManagement.Domain;
using OrchidFarmed.ProjectManagement.Domain.Repositories;
using OrchidFarmed.ProjectManagement.Domain.Shared.Exceptions;

using System.Linq.Expressions;

namespace OrchidFarmed.ProjectManagement.Infrastructure.Persistence.Repositories;
public class ProjectRepository : IProjectRepository
{
    private readonly AppDbContext _dbContext;

    public ProjectRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async System.Threading.Tasks.Task AddAsync(Project project)
    {
        await _dbContext.Projects.AddAsync(project);
    }

    public async System.Threading.Tasks.Task DeleteAsync(Guid projectId)
    {
        var project = await GetAsync(projectId);

        if (project == null)
            throw new ProjectNotFoundException();

        _dbContext.Projects.Remove(project);
    }

    public async System.Threading.Tasks.Task<bool> ExistsAsync(Expression<Func<Project, bool>> predicate)
    {
        return await _dbContext.Projects.AnyAsync(predicate);
    }

    public async System.Threading.Tasks.Task<Project> GetAsync(Guid projectId)
    {
        return await _dbContext.Projects.FindAsync(projectId);
    }

    public async Task<Project> GetAsync(Expression<Func<Project, bool>> predicate)
    {
        return await _dbContext.Projects.FirstOrDefaultAsync(predicate);
    }

    public async System.Threading.Tasks.Task<IEnumerable<Project>> GetListAsync(Expression<Func<Project, bool>> predicate)
    {
        var projects = await _dbContext.Projects.Where(predicate).ToListAsync();

        if (projects?.Count > 0)
            return projects;

        return Enumerable.Empty<Project>();
    }

    public async System.Threading.Tasks.Task<Project> UpdateAsync(Project project)
    {
        var result = _dbContext.Projects.Update(project);

        return result?.Entity;
    }

    public async System.Threading.Tasks.Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async System.Threading.Tasks.Task<IEnumerable<Project>> GetAllAsync()
    {
        return await _dbContext.Projects.ToListAsync();
    }

    public async System.Threading.Tasks.Task<Domain.Task> GetTaskAsync(Guid taskId)
    {
        return await _dbContext.Tasks.FindAsync(taskId);
    }
}