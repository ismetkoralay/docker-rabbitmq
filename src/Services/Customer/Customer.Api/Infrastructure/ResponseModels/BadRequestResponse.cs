namespace Customer.Api.Infrastructure.ResponseModels
{
    public class BadRequestResponse : ApiResponse
    {
        public BadRequestResponse(object message = null) : base(400, message)
        {
        }
    }
}