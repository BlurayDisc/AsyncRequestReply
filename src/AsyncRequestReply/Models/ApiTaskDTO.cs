using System.Text.Json.Serialization;

namespace AsyncRequestReply.Models
{
    public class ApiTaskDTO
    {
        public long Id { get; set; }
        public string TaskId { get; set; }
        public string Name { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ApiTaskStatus Status { get; set; }
    }
}