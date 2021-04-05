namespace Customer.Api.Infrastructure.ResponseModels
{
    public class NotFoundResponse : ApiResponse
    {
        public NotFoundResponse(object message = null) : base(404, message)
        {
        }
    }
}