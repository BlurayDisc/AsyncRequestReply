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
            
            return apiTasks.Select(apiTask => {
                var epochSeconds = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
                return ApiTaskToDTO(apiTask, epochSeconds);
            }).ToList();
        }

        [HttpPost]
        public async Task<ActionResult<ApiTaskDTO>> CreateApiTask(ApiTaskDTO apiTaskDTO)
        {
            var apiTask = new ApiTask
            {
                Name = apiTaskDTO.Name,
                SubmittedAt = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds()
            };

            await _tasksManager.StoreApiTaskAsync(apiTask);

            return CreatedAtAction(
                nameof(CreateApiTask),
                new { id = apiTask.Id },
                ApiTaskToDTO(apiTask));
        }

        private static ApiTaskDTO ApiTaskToDTO(ApiTask apiTask, long epochSeconds = 0) =>
            new ApiTaskDTO
            {
                Id = apiTask.Id,
                Name = apiTask.Name,
                Status = epochSeconds - apiTask.SubmittedAt > 8 ?
                                                                ApiTaskStatus.COMPLETED :
                                                                ApiTaskStatus.CREATED
            };
    }
}
