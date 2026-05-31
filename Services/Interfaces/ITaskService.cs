using Api.Models;

namespace Api.Services.Interfaces
{
    public interface ITaskService
    {
        Task<ToDoTask> CreateTask(CreateTaskRequest task);
        Task<List<ToDoTask>> GetAllTasks();
    }
}