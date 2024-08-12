using Microsoft.AspNetCore.Components;
using TodoList.Models;
using TodoList.Models.SeedWork;
using TodoListBlazorWasm.Components;
using TodoListBlazorWasm.Pages.Components;
using TodoListBlazorWasm.Services;

namespace TodoListBlazorWasm.Pages
{
    public partial class TaskList
    {
        [Inject] private ITaskApiClient TaskApiClient { set; get; }
        protected Confirmation DeleteConfirmation { set; get; }
        protected AssignTask AssignTaskDialog { set; get; }

        private Guid DeleteId { get; set; }

        private List<TaskDto> Tasks;
        private MetaData MetaData { get; set; } = new MetaData();
        private TaskListSearch TaskListSearch = new TaskListSearch();
        protected override async Task OnInitializedAsync()
        {
            await GetTasks();
        }
        public async Task SearchTask(TaskListSearch taskListSearch)
        {
            TaskListSearch = taskListSearch;
            await GetTasks();
        }
        public void OnDeleteTask(Guid deleteId)
        {
            DeleteId = deleteId;
            DeleteConfirmation.Show();
        }
        public async Task OnConfirmDeleteTask(bool deleteConfirmed)
        {
            if(deleteConfirmed == true)
            {
                await TaskApiClient.DeleteTask(DeleteId);
                await GetTasks();
            }
        }
        public void OpenAssignPopup(Guid id)
        {
            AssignTaskDialog.Show(id);
        }

        public async Task AssignTaskSuccess(bool result)
        {
            if (result)
                await GetTasks();
        }


        private async Task GetTasks()
        {
            var pagingResponse = await TaskApiClient.GetTaskList(TaskListSearch);
            Tasks = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
        }

        private async Task SelectedPage(int page)
        {
            TaskListSearch.PageNumber = page;
            await GetTasks();
        }
    }
}

