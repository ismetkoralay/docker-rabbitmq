using System;
using Microsoft.AspNetCore.Mvc;
using Queue.Api.Models;
using Queue.Service;

namespace Queue.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class QueueController :  ControllerBase
    {
        private readonly IRabbitMqService _rabbitMqService;

        public QueueController(IRabbitMqService rabbitMqService)
        {
            _rabbitMqService = rabbitMqService;
        }

        [HttpPost]
        public IActionResult SendMessage([FromBody] SendMessageRequestModel request)
        {
            Console.WriteLine($"");
            _rabbitMqService.Publish(request.Message, request.QueueName);
            return Ok();
        }
    }
}