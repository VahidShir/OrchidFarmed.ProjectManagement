using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;

using OrchidFarmed.ProjectManagement.Application.Contracts;

namespace OrchidFarmed.ProjectManagement.WebApi.Controllers;

[ApiVersion(1)]
[ApiController]
[Route("api/v{v:apiVersion}/[controller]")]
public class ProjectController : ControllerBase
{

    public ProjectController()
    {
    }

    [HttpGet("{projectId}")]
    public async Task<ActionResult<ProjectDto>> GetProject(Guid projectId)
    {
        return Ok();
    }

    [HttpGet()]
    public async Task<ActionResult<IList<ProjectDto>>> GetAllProjects()
    {
        return Ok();
    }

    [HttpGet("{projectId}/tasks/{taskId}")]
    public async Task<ActionResult<ProjectDto>> GetTask(Guid taskId)
    {
        return Ok();
    }

    [HttpGet("{projectId}/tasks/")]
    public async Task<ActionResult<ProjectDto>> GetAllTasks()
    {
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<ProjectDto>> CreateProject(CreateProjectRequestDto request)
    {
        return Ok();
    }

    [HttpDelete("{projectId}")]
    public async Task<ActionResult<ProjectDto>> DeleteProject(Guid projectId)
    {
        return Ok();
    }

    [HttpDelete("{projectId}/tasks/{taskId}")]
    public async Task<ActionResult<ProjectDto>> DeleteTask(Guid projectId, Guid taskId)
    {
        return Ok();
    }

    [HttpPost("{projectId}/tasks")]
    public async Task<ActionResult<TaskDto>> CreateTask(Guid projectId, CreateTaskRequestDto request)
    {
        return Ok();
    }

    [HttpPut("{projectId}/tasks/{taskId}")]
    public async Task<ActionResult<TaskDto>> UpdateTaskStatus(Guid projectId, Guid taskId, UpdateTaskStatusRequestDto request)
    {
        return Ok();
    }
}