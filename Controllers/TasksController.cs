namespace Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Api.Data;
    using Api.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ApiContext _context;

        public TasksController(ApiContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateTask(CreateTaskRequest task)
        {
            var toDoTask = new ToDoTask
            {
                Name = task.Name,
                Priority = task.Priority,
                ExpireDate = task.ExpireDate
            };

            _context.ToDoTasks.Add(toDoTask);
            _context.SaveChanges();
            return Ok(toDoTask);
        }
    }
}