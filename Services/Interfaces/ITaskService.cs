using Api.Models;
using Api.DTOs;

namespace Api.Services.Interfaces
{
    public interface ITaskService
    {
        Task<ToDoTask> CreateTask(CreateTaskRequest task, int userId);
        Task<List<TaskDto>> GetAllTasks(int userId);
        Task ToggleTask(int taskId, int userId);
        Task DeleteTask(int taskId, int userId);
    }
}