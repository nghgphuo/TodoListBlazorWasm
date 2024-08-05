using Microsoft.AspNetCore.Components;
using TodoListBlazorWasm.Serivces;
using TodoList.Models;

namespace TodoListBlazorWasm.Pages
{
    public partial class TodoList
    {
        [Inject] private ITaskApiClient TaskApiClient { set; get; }
        private List<TaskDto> Tasks;

        protected override async Task OnInitializedAsync()
        {
            Tasks = await TaskApiClient.GetTaskList();
        }
    }
}
