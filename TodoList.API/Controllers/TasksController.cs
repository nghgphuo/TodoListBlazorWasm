﻿using Microsoft.AspNetCore.Mvc;
using TodoList.API.Repositories;
using TodoList.Models;
using TodoList.Models.Enums;
using Task = TodoList.API.Entities.Task;

namespace TodoList.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public TasksController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _taskRepository.GetTaskList();
            var taskDtos = tasks.Select(x => new TaskDto()
            {
                Status = x.Status,
                Name = x.Name,
                AssigneeId = x.AssigneeId,
                CreatedDate = x.CreatedDate,
                Priority = x.Priority,
                Id = x.Id,
                AssigneeName = x.Assignee != null ? x.Assignee.FirstName + ' ' + x.Assignee.LastName : "N/A"
            });
            return Ok(taskDtos);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var task = await _taskRepository.Create(new Task()
            {
                Name = request.Name,
                Priority = request.Priority.HasValue ? request.Priority.Value : Priority.Low,
                Status = Status.Open,
                CreatedDate = DateTime.Now,
                Id = request.Id
            });
            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TaskUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var taskFromDb = await _taskRepository.GetById(id);

            if (taskFromDb == null)
            {
                return NotFound($"{id} is not found");
            }

            taskFromDb.Name = request.Name;
            taskFromDb.Priority = request.Priority;

            var taskResult = await _taskRepository.Update(taskFromDb);

            return Ok(new TaskDto()
            {
                Name = taskResult.Name,
                Status = taskResult.Status,
                Id = taskResult.Id,
                AssigneeId = taskResult.AssigneeId,
                Priority = taskResult.Priority,
                CreatedDate = taskResult.CreatedDate
            });
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var task = await _taskRepository.GetById(id);
            if (task == null) return NotFound($"{id} is not found.");
            return Ok(new TaskDto
            {
                Name = task.Name,
                Status = task.Status,
                Id = task.Id,
                AssigneeId = task.AssigneeId,
                Priority = task.Priority,
                CreatedDate = task.CreatedDate,
            });
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var taskFromDb = await _taskRepository.GetById(id);
            await _taskRepository.Delete(taskFromDb);
            return NoContent();
        }
    }
}
