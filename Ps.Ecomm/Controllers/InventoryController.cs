using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ps.Ecomm.DataAccess;
using Ps.Ecomm.Models;

namespace Ps.Ecomm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryProvider _inventoryProvider;

        public InventoryController(IInventoryProvider inventoryProvider)
        {
            _inventoryProvider = inventoryProvider;
        }
        // GET: api/<InventoryController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            Inventory[] inventory = await _inventoryProvider.Get();
            return Ok(inventory);
        }
    }
}
