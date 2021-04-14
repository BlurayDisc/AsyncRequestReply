using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AsyncRequestReply.Models;
using AsyncRequestReply.Services;

namespace AsyncRequestReply.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private ITasksManager _tasksManager;

        public TaskController(ITasksManager tasksManager)
        {
            _tasksManager = tasksManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApiTaskDTO>>> ListApiTasks()
        {
            var apiTasks = await _tasksManager.ListApiTasksAsync();
            return apiTasks.Select(apiTask => ApiTaskToDTO(apiTask)).ToList();
        }

        [HttpPost]
        public async Task<ActionResult<ApiTaskDTO>> CreateApiTask(ApiTaskDTO apiTaskDTO)
        {
            var apiTask = new ApiTask
            {
                TaskId = apiTaskDTO.TaskId,
                Name = apiTaskDTO.Name,
                SubmittedAt = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds()
            };

            await _tasksManager.StoreApiTaskAsync(apiTask);

            return CreatedAtAction(
                nameof(CreateApiTask),
                new { id = apiTask.Id },
                ApiTaskToDTO(apiTask));
        }

        private static ApiTaskDTO ApiTaskToDTO(ApiTask apiTask) =>
            new ApiTaskDTO
            {
                Id = apiTask.Id,
                TaskId = apiTask.TaskId,
                Name = apiTask.Name,
                Status = apiTask.Status
            };
    }
}
