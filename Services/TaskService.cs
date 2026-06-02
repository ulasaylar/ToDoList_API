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

    public async Task<List<ToDoTask>> GetAllTasks(int userId)
    {
        return await _context.ToDoTasks.Where(x => x.UserId == userId).ToListAsync();
    }
    public async Task<ToDoTask> CreateTask(CreateTaskRequest request, int userId)
    {
        var task = new ToDoTask
        {
            Name = request.Name,
            Priority = request.Priority,
            ExpireDate = request.ExpireDate,
            UserId = userId
        };

        _context.ToDoTasks.Add(task);
        await _context.SaveChangesAsync();

        return task;
    }
}