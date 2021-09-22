using BusinessLogic.Interfaces;
using Kafka.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ItemWrite.Controllers.ItemWrite
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IKafkaService<string> _kafkaService;

        public ItemsController(IKafkaService<string> kafkaService)
        {
            _kafkaService = kafkaService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string item)
        {
            StatusCode statusCode = await _kafkaService.Push(item);

            return Ok(statusCode.ToString());
        }
    }
}
