using System.Collections.Generic;
using System.Threading.Tasks;
using AsyncRequestReply.Models;

namespace AsyncRequestReply.Services
{
    public interface ITasksManager
    {
        Task StoreApiTaskAsync(ApiTask apiTask);
        Task<List<ApiTask>> ListApiTasksAsync();
    }
}
