using Asp.Versioning;

using FluentValidation;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using OrchidFarmed.ProjectManagement.Application.Contracts;
using OrchidFarmed.ProjectManagement.Application.Contracts.Commands;
using OrchidFarmed.ProjectManagement.Application.Contracts.Queries;
using OrchidFarmed.ProjectManagement.Domain;
using OrchidFarmed.ProjectManagement.Domain.Shared.Exceptions;
using OrchidFarmed.ProjectManagement.Domain.Shared.Extensions;

using System.Net;
using System.Security.Claims;

namespace OrchidFarmed.ProjectManagement.WebApi.Controllers;

[Authorize]
[ApiVersion(1)]
[ApiController]
[Route("api/v{v:apiVersion}/[controller]")]
public class ProjectController : ControllerBase
{
    private readonly IMediator _mediator;
    private Guid _userId => GetUserId();

    public ProjectController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get a project by providing the projectId
    /// </summary>
    /// <param name="projectId"></param>
    /// <returns>The requested project</returns>
    [HttpGet("{projectId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<ProjectDto>> GetProject(Guid projectId)
    {
        var query = new GetProjectQuery(_userId, projectId);

        try
        {
            ProjectDto project = await _mediator.Send(query);

            if (project == null)
                return NotFound();

            return Ok(project);
        }
        catch (ForbiddenOperationException ex)
        {
            return StatusCode((int)HttpStatusCode.Forbidden, ex.Message);
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

    /// <summary>
    /// Get the list of user's projects
    /// </summary>
    /// <returns>A collection of projects</returns>
    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<ProjectDto>>> GetAllProjects()
    {
        var query = new GetAllProjectsQuery(_userId);

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

    /// <summary>
    /// Get the task of a specific project
    /// </summary>
    /// <param name="projectId"></param>
    /// <param name="taskId"></param>
    /// <returns></returns>
    [HttpGet("{projectId}/tasks/{taskId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaskDto>> GetTask(Guid projectId, Guid taskId)
    {
        var query = new GetTaskQuery(_userId, projectId, taskId);

        try
        {
            TaskDto task = await _mediator.Send(query);

            if (task == null)
                return NotFound();

            return Ok(task);
        }
        catch (ForbiddenOperationException ex)
        {
            return StatusCode((int)HttpStatusCode.Forbidden, ex.Message);
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

    /// <summary>
    /// Get the list of the tasks for a specific project
    /// </summary>
    /// <param name="projectId"></param>
    /// <returns></returns>
    [HttpGet("{projectId}/tasks/")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasks(Guid projectId)
    {
        var query = new GetProjectQuery(_userId, projectId);

        try
        {
            ProjectDto project = await _mediator.Send(query);

            if (project == null)
                return NotFound();

            return Ok(project.Tasks);
        }
        catch (ForbiddenOperationException ex)
        {
            return StatusCode((int)HttpStatusCode.Forbidden, ex.Message);
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

    /// <summary>
    /// Create a new project
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ProjectDto>> CreateProject(CreateProjectRequestDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = new CreateProjectCommand(_userId, request.Name, request.Description);

        try
        {
            ProjectDto project = await _mediator.Send(command);

            return CreatedAtAction(actionName: nameof(GetProject), routeValues: new { projectId = project.Id }, project);
        }
        catch (Exception ex) when (ex is BusinessException || ex is ValidationException)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    /// <summary>
    /// Delete a project
    /// </summary>
    /// <param name="projectId"></param>
    /// <returns></returns>
    [HttpDelete("{projectId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteProject(Guid projectId)
    {
        var command = new DeleteProjectCommand(_userId, projectId);

        try
        {
            await _mediator.Send(command);

            return Ok();
        }
        catch (ProjectNotFoundException ex)
        {
            return NotFound();
        }
        catch (ForbiddenOperationException ex)
        {
            return StatusCode((int)HttpStatusCode.Forbidden, ex.Message);
        }
        catch (Exception ex) when (ex is BusinessException || ex is ValidationException)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    /// <summary>
    /// Delete a task from a project
    /// </summary>
    /// <param name="projectId"></param>
    /// <param name="taskId"></param>
    /// <returns></returns>
    [HttpDelete("{projectId}/tasks/{taskId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteTask(Guid projectId, Guid taskId)
    {
        var command = new DeleteTaskCommand(_userId, projectId, taskId);

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
        catch (ForbiddenOperationException ex)
        {
            return StatusCode((int)HttpStatusCode.Forbidden, ex.Message);
        }
        catch (Exception ex) when (ex is BusinessException || ex is ValidationException)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    /// <summary>
    /// Create a task for a project
    /// </summary>
    /// <param name="projectId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("{projectId}/tasks")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TaskDto>> CreateTask(Guid projectId, CreateTaskRequestDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = new CreateTaskCommand(_userId, projectId, request.Name, request.Description, request.DueDate);

        try
        {
            TaskDto task = await _mediator.Send(command);

            return CreatedAtAction(actionName: nameof(GetTask), routeValues: new { projectId = projectId, taskId = task.Id }, task);
        }
        catch (ProjectNotFoundException ex)
        {
            return NotFound();
        }
        catch (ForbiddenOperationException ex)
        {
            return StatusCode((int)HttpStatusCode.Forbidden, ex.Message);
        }
        catch (Exception ex) when (ex is BusinessException || ex is ValidationException)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    /// <summary>
    /// Update the status of a task
    /// </summary>
    /// <param name="projectId"></param>
    /// <param name="taskId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("{projectId}/tasks/{taskId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TaskDto>> UpdateTaskStatus(Guid projectId, Guid taskId, UpdateTaskStatusRequestDto request)
    {
        var command = new UpdateTaskStatusCommand(_userId, projectId, taskId, request.Status);

        try
        {
            TaskDto task = await _mediator.Send(command);

            return Ok(task);
        }
        catch (ProjectNotFoundException ex)
        {
            return NotFound();
        }
        catch (TaskNotFoundException ex)
        {
            return NotFound();
        }
        catch (ForbiddenOperationException ex)
        {
            return StatusCode((int)HttpStatusCode.Forbidden, ex.Message);
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

    private Guid GetUserId()
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdString.IsNullOrEmptyWhiteSpace())
            throw new ProjectManagementBaseException("Current user's userId not found.");

        if (Guid.TryParse(userIdString, out Guid userId))
        {
            return userId;
        }

        throw new ProjectManagementBaseException("Current user's userId not found.");
    }
}