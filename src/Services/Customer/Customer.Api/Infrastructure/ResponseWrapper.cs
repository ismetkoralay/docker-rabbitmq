using Customer.Api.Infrastructure.ResponseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Customer.Api.Infrastructure
{
    public class ResponseWrapper : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ObjectResult objectResult)
            {
                if (context.Result is OkObjectResult)
                    objectResult.Value = new OkResponse(objectResult.Value);
                else if (objectResult.StatusCode.HasValue)
                {
                    var message = context.Result.GetPropertyValue("Value");
                    objectResult.Value = objectResult.StatusCode.Value switch
                    {
                        StatusCodes.Status200OK => new OkResponse(objectResult.Value),
                        StatusCodes.Status400BadRequest => new BadRequestResponse(message),
                        StatusCodes.Status404NotFound => new NotFoundResponse(message),
                        _ => new BadRequestResponse(message)
                    };
                }
            }
            base.OnActionExecuted(context);
        }
    }
}