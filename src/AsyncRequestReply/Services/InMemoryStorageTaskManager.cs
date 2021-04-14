using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AsyncRequestReply.Models;

namespace AsyncRequestReply.Services
{
    public class InMemoryStorageTaskManager : ITasksManager
    {
        private long id = 0;
        private ConcurrentQueue<ApiTask> _backgroundTaskQueue = new ConcurrentQueue<ApiTask>();

        public Task StoreApiTaskAsync(ApiTask apiTask)
        {
            apiTask.Id = Interlocked.Increment(ref id);
            return Task.Run(() => _backgroundTaskQueue.Enqueue(apiTask));
        }
        
        public Task<List<ApiTask>> ListApiTasksAsync()
        {
            var epochSeconds = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            
            var apiTasks = _backgroundTaskQueue
                .Select(task => 
                    {
                        // Acting as the 'getStatus' endpoint in an Asynchronous Request-Reply pattern.
                        // But this is just called internally for the simple variant.
                        task.Status = calculateStatus(epochSeconds, task);
                        return task;
                    })
                .ToList();

            return Task<List<ApiTask>>.FromResult(apiTasks);
        }

        private ApiTaskStatus calculateStatus(long epochSeconds, ApiTask apiTask)
        {
            return epochSeconds - apiTask.SubmittedAt > 8 ? 
                ApiTaskStatus.COMPLETED : 
                ApiTaskStatus.CREATED;
        }
    }
}
