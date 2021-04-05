namespace Queue.Api.Models
{
    public class SendMessageRequestModel
    {
        public string Message { get; set; }
        public string QueueName { get; set; }
    }
}