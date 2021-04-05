using Microsoft.AspNetCore.Mvc;

namespace Customer.Service
{
    public class BaseResponseModel : ObjectResult
    {
        public string Message { get; set; }
        public BaseResponseModel(int statusCode, object value = null, string message = null) : base(value)
        {
            Message = message;
            StatusCode = statusCode;
        }
    }
}