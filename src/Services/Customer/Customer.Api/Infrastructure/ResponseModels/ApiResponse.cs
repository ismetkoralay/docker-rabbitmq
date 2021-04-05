namespace Customer.Api.Infrastructure.ResponseModels
{
    public class ApiResponse
    {
        public object Message { get; set; }

        public ApiResponse(int statusCode, object message = null)
        {
            Message = message ?? GetDefaultResponseMessage(statusCode);
        }

        private string GetDefaultResponseMessage(int statusCode)
        {
            return statusCode switch
            {
                200 => "Ok",
                400 => "Bad Request",
                403 => "Unauthorized",
                404 => "NotFound",
                _ => "Server Error"
            };
        }
    }
}