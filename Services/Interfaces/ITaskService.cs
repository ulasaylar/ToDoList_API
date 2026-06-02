using Api.Models;

namespace Api.Services.Interfaces
{
    public interface ITaskService
    {
        Task<ToDoTask> CreateTask(CreateTaskRequest task, int userId);
        Task<List<ToDoTask>> GetAllTasks(int userId);
    }
}