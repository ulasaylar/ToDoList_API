using Api.Models;

namespace Api.Services.Interfaces
{
    public interface ITaskService
    {
        ToDoTask CreateTask(CreateTaskRequest task);
        Task<List<ToDoTask>> GetAllTasks();
    }
}