using Api.Data;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

public class TaskService : ITaskService
{
    private readonly ApiContext _context;

    public TaskService(ApiContext context)
    {
        _context = context;
    }

    public async Task<List<ToDoTask>> GetAllTasks()
    {
        return await _context.ToDoTasks.ToListAsync();
    }
    public ToDoTask CreateTask(CreateTaskRequest request)
    {
        var task = new ToDoTask
        {
            Name = request.Name,
            Priority = request.Priority,
            ExpireDate = request.ExpireDate
        };

        _context.ToDoTasks.Add(task);
        _context.SaveChanges();

        return task;
    }
}