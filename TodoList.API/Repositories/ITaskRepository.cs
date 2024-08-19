using TodoList.Models;
using TodoList.Models.SeedWork;
using Task = TodoList.API.Entities.Task;

namespace TodoList.API.Repositories
{
    public interface ITaskRepository
    {
        Task<PagedList<Task>> GetTaskList(TaskListSearch taskListSearch);
        Task<Task> Create(Task task); 
        Task<Task> Update(Task task);
        Task<Task> Delete(Task task);
        Task<Task> GetById(Guid id);
    }
}
