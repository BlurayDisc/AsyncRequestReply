using System.Collections.Generic;
using System.Collections.Concurrent;
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
            return Task<List<ApiTask>>.Factory.StartNew(
                () => new List<ApiTask>(_backgroundTaskQueue.ToArray())
            );
        }
    }
}
