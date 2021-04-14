namespace AsyncRequestReply.Models
{
    public class ApiTask
    {
        public long Id { get; set; }
        public string TaskId { get; set; }
        public string Name { get; set; }
        public ApiTaskStatus Status { get; set; }
        public long SubmittedAt { get; set; }
    }
}