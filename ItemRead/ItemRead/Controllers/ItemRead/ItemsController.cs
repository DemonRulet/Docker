using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using BusinessLogic.BackgroundServices;
using DataAccess.Interfaces;

namespace ItemRead.Controllers.ItemRead
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemRepository<string> _itemRepository;

        public ItemsController(IItemRepository<string> itemRepository)
        {
            _itemRepository = itemRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_itemRepository.GetAll());
        }
    }
}
