using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Threading.Tasks;
using TodoList.Models;
using TodoList.Models.Enums;
using TodoListBlazorWasm.Services;

namespace TodoListBlazorWasm.Pages
{
    public partial class TaskEdit
    {
        [Parameter]
        public string TaskId { get; set; }

        private TaskUpdateRequest Task;

        protected async override Task OnInitializedAsync()
        {
            var taskDto = await TaskApiClient.GetTaskDetail(TaskId);
            Task = new TaskUpdateRequest
            {
                Name = taskDto.Name,
                Priority = taskDto.Priority
            };
        }

        private async Task SubmitTask(EditContext context)
        {
            var result = await TaskApiClient.UpdateTask(Guid.Parse(TaskId), Task);
            if (result)
            {
                ToastService.ShowSuccess($"{Task.Name} has been updated successfully.", "Success");
                NavigationManager.NavigateTo("/taskList");
            }
            else
            {
                ToastService.ShowError($"An error occurred in progress. Please contact to administrator.", "Error");
            }
        }
    }
}
