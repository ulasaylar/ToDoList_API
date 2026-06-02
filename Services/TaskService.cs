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

    public async Task<List<TaskDto>> GetAllTasks(int userId)
    {
        return await _context.ToDoTasks
            .Where(x => x.UserId == userId)
            .Select(x => new TaskDto
            {
                Id = x.Id,
                Name = x.Name,
                Priority = x.Priority,
                ExpireDate = x.ExpireDate,
                Status = x.Status,
                Category = x.Category == null ? null : new CategoryDto
                {
                    Id = x.Category.Id,
                    Name = x.Category.Name
                }
            })
            .ToListAsync();
    }
    public async Task<ToDoTask> CreateTask(CreateTaskRequest request, int userId)
    {

        if (request.CategoryId != null)
        {
            var categoryExists = await _context.Categories
                .AnyAsync(x => x.Id == request.CategoryId && x.UserId == userId);

            if (!categoryExists)
                throw new Exception("Invalid category");
        }

        var task = new ToDoTask
        {
            Name = request.Name,
            Priority = request.Priority,
            ExpireDate = request.ExpireDate,
            CategoryId = request.CategoryId,
            UserId = userId,
        };

        _context.ToDoTasks.Add(task);
        await _context.SaveChangesAsync();

        return task;
    }

    public async Task ToggleTask(int taskId, int userId)
    {
        var task = await _context.ToDoTasks
            .FirstOrDefaultAsync(x => x.Id == taskId && x.UserId == userId);

        if (task == null)
            throw new Exception("Task not found");

        task.Status = task.Status == 1 ? 2 : 1;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteTask(int taskId, int userId)
    {
        var task = await _context.ToDoTasks
            .FirstOrDefaultAsync(x => x.Id == taskId && x.UserId == userId);

        if (task == null)
            throw new Exception("Task not found");

        _context.ToDoTasks.Remove(task);
        await _context.SaveChangesAsync();
    }
}