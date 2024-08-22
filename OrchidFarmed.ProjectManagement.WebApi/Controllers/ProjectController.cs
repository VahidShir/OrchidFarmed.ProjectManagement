using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using OrchidFarmed.ProjectManagement.Application.Contracts;
using OrchidFarmed.ProjectManagement.Application.Contracts.Commands;
using OrchidFarmed.ProjectManagement.Application.Contracts.Queries;
using OrchidFarmed.ProjectManagement.Domain.Shared.Exceptions;

using System.Net;

namespace OrchidFarmed.ProjectManagement.WebApi.Controllers;

[ApiVersion(1)]
[ApiController]
[Route("api/v{v:apiVersion}/[controller]")]
public class ProjectController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{projectId}")]
    public async Task<ActionResult<ProjectDto>> GetProject(Guid projectId)
    {
        var query = new GetProjectQuery(projectId);

        try
        {
            ProjectDto project = await _mediator.Send(query);

            if (project == null)
                return NotFound();

            return Ok(project);
        }
        catch (BusinessException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    [HttpGet()]
    public async Task<ActionResult<IList<ProjectDto>>> GetAllProjects()
    {
        var query = new GetAllProjectsQuery();

        try
        {
            var projects = await _mediator.Send(query);

            return Ok(projects);
        }
        catch (BusinessException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    [HttpGet("{projectId}/tasks/{taskId}")]
    public async Task<ActionResult<TaskDto>> GetTask(Guid projectId, Guid taskId)
    {
        var query = new GetTaskQuery(projectId, taskId);

        try
        {
            TaskDto task = await _mediator.Send(query);

            if (task == null)
                return NotFound();

            return Ok(task);
        }
        catch (BusinessException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    [HttpGet("{projectId}/tasks/")]
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasks(Guid projectId)
    {
        var query = new GetProjectQuery(projectId);

        try
        {
            ProjectDto project = await _mediator.Send(query);

            if (project == null)
                return NotFound();

            return Ok(project.Tasks);
        }
        catch (BusinessException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<ProjectDto>> CreateProject(CreateProjectRequestDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = new CreateProjectCommand(request.Name, request.Description);

        try
        {
            ProjectDto project = await _mediator.Send(command);

            return CreatedAtAction(actionName: nameof(GetProject), routeValues: new { projectId = project.Id }, project);
        }
        catch (BusinessException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    [HttpDelete("{projectId}")]
    public async Task<ActionResult> DeleteProject(Guid projectId)
    {
        var command = new DeleteProjectCommand(projectId);

        try
        {
            await _mediator.Send(command);

            return Ok();
        }
        catch (ProjectNotFoundException ex)
        {
            return NotFound();
        }
        catch (BusinessException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    [HttpDelete("{projectId}/tasks/{taskId}")]
    public async Task<ActionResult> DeleteTask(Guid projectId, Guid taskId)
    {
        var command = new DeleteTaskCommand(projectId, taskId);

        try
        {
            await _mediator.Send(command);

            return Ok();
        }
        catch (ProjectNotFoundException ex)
        {
            return NotFound();
        }
        catch (TaskNotFoundException ex)
        {
            return NotFound();
        }
        catch (BusinessException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    [HttpPost("{projectId}/tasks")]
    public async Task<ActionResult<TaskDto>> CreateTask(Guid projectId, CreateTaskRequestDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = new CreateTaskCommand(projectId, request.Name, request.Description, request.DueDate);

        try
        {
            TaskDto result = await _mediator.Send(command);

            return Ok();
        }
        catch (ProjectNotFoundException ex)
        {
            return NotFound();
        }
        catch (BusinessException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    [HttpPut("{projectId}/tasks/{taskId}")]
    public async Task<ActionResult<TaskDto>> UpdateTaskStatus(Guid projectId, Guid taskId, UpdateTaskStatusRequestDto request)
    {
        var command = new UpdateTaskStatusCommand(projectId, taskId, request.Status);

        try
        {
            TaskDto result = await _mediator.Send(command);

            return Ok();
        }
        catch (ProjectNotFoundException ex)
        {
            return NotFound();
        }
        catch (TaskNotFoundException ex)
        {
            return NotFound();
        }
        catch (BusinessException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}