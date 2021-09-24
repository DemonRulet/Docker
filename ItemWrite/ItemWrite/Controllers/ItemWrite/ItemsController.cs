using BusinessLogic.ForegroundServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ItemWrite.Controllers.ItemWrite
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService<string> _itemService;

        public ItemsController(IItemService<string> itemService)
        {
            _itemService = itemService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string item)
        {
             int status = await _itemService.Add(item);
             return Ok(status == 1 ? "Success" : "Error");
        }
    }
}
