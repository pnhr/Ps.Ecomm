using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ps.Ecomm.DataAccess;

namespace Ps.Ecomm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderClientController : ControllerBase
    {
        private readonly IOrderDetailsProvider _orderDetailsProvider;

        public OrderClientController(IOrderDetailsProvider orderDetailsProvider)
        {
            this._orderDetailsProvider = orderDetailsProvider;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var orderDetails = await _orderDetailsProvider.Get();
            return Ok(orderDetails);
        }
    }
}
