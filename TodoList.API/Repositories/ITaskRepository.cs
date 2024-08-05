using TodoList.Models;
using Task = TodoList.API.Entities.Task;

namespace TodoList.API.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Task>> GetTaskList();
        Task<Task> Create(Task task); 
        Task<Task> Update(Task task);
        Task<Task> Delete(Task task);
        Task<Task> GetById(Guid id);
    }
}
