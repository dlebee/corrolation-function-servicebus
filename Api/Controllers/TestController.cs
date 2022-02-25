using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        [HttpGet("{message}")]
        public async Task<IActionResult> SendMessageAsync([FromServices]IConfiguration configuration, string message)
        {
            Activity.Current?.AddTag("TestCustoTag", "true");
            var connectionString = configuration["ServiceBusConnection"];
            var client = new ServiceBusClient(connectionString);
            var sender = client.CreateSender("message-queue");
            await sender.SendMessageAsync(new ServiceBusMessage(message));
            return Ok();
        }
    }
}
