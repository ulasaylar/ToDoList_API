using Api.Data;
using Microsoft.AspNetCore.Mvc;
using Api.Services.Interfaces;
using FluentValidation;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class TasksController : ControllerBase
{
    private readonly ITaskService _service;
    private readonly IValidator<CreateTaskRequest> _validator;

    public TasksController(ITaskService service, IValidator<CreateTaskRequest> validator)
    {
        _service = service;
        _validator = validator;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateTask(CreateTaskRequest task)
    {
        var validationResult = _validator.Validate(task);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }

        var result = await _service.CreateTask(task);

        return Ok(result);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllTasks()
    {
        var tasks = await _service.GetAllTasks();
        return Ok(tasks);
    }
}
