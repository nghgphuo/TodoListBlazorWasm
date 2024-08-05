using TodoList.Models;

namespace TodoListBlazorWasm.Serivces
{
    public interface ITaskApiClient
    {
        Task<List<TaskDto>> GetTaskList();

    }
}
