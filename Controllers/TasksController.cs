using Api.DTOs;
using Api.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TasksController : ControllerBase
{
    private readonly ITaskService _service;
    private readonly IValidator<CreateTaskRequest> _validator;

    public TasksController(
        ITaskService service,
        IValidator<CreateTaskRequest> validator)
    {
        _service = service;
        _validator = validator;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskRequest task)
    {
        var validationResult = await _validator.ValidateAsync(task);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }

        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var result = await _service.CreateTask(task, userId);

        return Ok(result);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllTasks()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var tasks = await _service.GetAllTasks(userId);

        return Ok(tasks);
    }
}